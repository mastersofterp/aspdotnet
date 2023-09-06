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
using Microsoft.WindowsAzure.Storage.Blob;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;

public partial class ITLE_checkStudeAssignReply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BlobController objBlob = new BlobController();


    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                //if (Session["Page"] == null)
                //{
                    CheckPageAuthorization();
                    Session["Page"] = 1;
               // } 
                //Set the Page Title

                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;

                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblCurrdate.ForeColor = System.Drawing.Color.Green;
                //lblAssignmentTopic.Text = Request.QueryString["assignment"].ToString();
                lblTotalMarks.Text = Request.QueryString["ASSIGNMENT_MARKS"];
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
            }
            BindAssignmentList();
            BlobDetails();
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=checkStudeAssignReply.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=checkStudeAssignReply.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            DataSet ds = objAM.GetStudent_Assignment_ReplyListByAs_No(Convert.ToInt32(ViewState["as_no"])); // Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));

            lblAssignmentTopic.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
            trAssignTopic.Visible = true;


            lvAssignmentReply.DataSource = ds;
            lvAssignmentReply.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetFileName(object filename, object sa_no)
    {
        if (filename != null && filename.ToString() != "")
            //return filename.ToString();
            //  return "assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/upload_files/student_assignment_reply/assignment_reply" + Convert.ToInt32(sa_no) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        setControl(false);
        lblStatus.Text = string.Empty;

    }

    private void setControl(Boolean status)
    {
        trButtons.Visible = status;
        trDesc.Visible = status;
        trReplyDate.Visible = status;
        trfile.Visible = status;
        trRemark.Visible = status;
        trMarksObtained.Visible = status;
        trTotalMarks.Visible = status;
        trStudentName.Visible = status;


        if (status == false)
        {
            //txtReplyDescription.Text = "";
            tdDate.InnerText = "";
            //linkAssingReplyFile.Text = "";
            //linkAssingReplyFile.Text = "";
            //linkAssingReplyFile.NavigateUrl = "";
            chkChecked.Checked = status;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        submitAssignReplyRemark();
        setControl(false);
        ViewState["sa_no"] = null;
        txtMarks.Text = "";
        BindListView();
        return;
    }

    void submitAssignReplyRemark()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            Assignment objAssign = new Assignment();
            objAssign.SA_NO = Convert.ToInt32(ViewState["sa_no"].ToString());
            objAssign.DESCRIPTION = txtRemark.Text.Trim();
            objAssign.ASSIGNMENTMARKS_STUDENT_OBTAINED = Convert.ToInt32(txtMarks.Text.Trim());
            objAssign.STATUS = '2';
            //objAssign.CHECKED = chkChecked.Checked == true ? '1' : '0';
            objAssign.CHECKED = '1';
            objAssign.DISPLAY_MARKS = chkDisplayMarks.Checked == true ? '1' : '0';
            CustomStatus cs = (CustomStatus)objAM.RemarkAssignmentReply(objAssign);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //lblStatus.Text = "Record Set";
                objCommon.DisplayMessage(this, "Marks Submitted Successfully!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.submitAssignReplyRemark-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //void submitAssignReplyRemark()
    //{
    //    try
    //    {
    //        AssignmentController objAM = new AssignmentController();
    //        Assignment objAssign = new Assignment();
    //        objAssign.SA_NO = Convert.ToInt32(ViewState["sa_no"].ToString());
    //        objAssign.DESCRIPTION = txtRemark.Text.Trim();
    //        objAssign.ASSIGNMENTMARKS_STUDENT_OBTAINED = Convert.ToInt32(txtMarks.Text.Trim());
    //        objAssign.STATUS = '2';
    //        objAssign.CHECKED = chkChecked.Checked == true ? '1' : '0';
    //        CustomStatus cs = (CustomStatus)objAM.RemarkAssignmentReply(objAssign);
    //        if (cs.Equals(CustomStatus.RecordSaved))
    //            lblStatus.Text = "Record Set";


    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Itle_assignmentMaster.submitAssignReplyRemark-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int sa_no = int.Parse(btnEdit.CommandArgument);

            ShowDetail(sa_no);
            GetAttachmentByAsNo(sa_no);
            ViewState["action"] = "edit";
            //trButtons.visible = true;
            // txtReplyDescription.config.toolbarStartupExpanded = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int sa_no)
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            ViewState["sa_no"] = sa_no;
            DataTableReader dtr = objAM.GetSingleAssignmentStudentReply(Convert.ToInt32(ViewState["sa_no"]));
            // txtReplyDescription.ReadOnly = true;

            setControl(false);
            if (dtr != null)
            {
                if (dtr.Read())
                {

                    setControl(true);
                    trStudentName.Visible = true;
                    txtMarks.Text = dtr["STUDENT_MARKS"].ToString();
                    lblStudName.Text = dtr["STUDNAME"].ToString();
                    divRepDesc.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    //txtReplyDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    tdDate.InnerText = dtr["REPLY_DATE"] == null ? "" : Convert.ToDateTime(dtr["REPLY_DATE"].ToString()).ToString("MM/dd/yyyy hh:mm tt"); //ATTACHMENT
                    lblTotalMarks.Text = dtr["ASSIGNMENT_MARKS"] == null ? "" : dtr["ASSIGNMENT_MARKS"].ToString();
                    txtRemark.Text = dtr["REMARKS"] == null ? "" : dtr["REMARKS"].ToString();

                    if (dtr["CHECKED"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(dtr["CHECKED"].ToString()) == 1)
                        {
                            chkChecked.Checked = true;
                        }
                    }

                    if (dtr["DISPLAY_MARKS"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(dtr["DISPLAY_MARKS"].ToString()) == 1)
                        {
                            chkDisplayMarks.Checked = true;
                        }
                    }


                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int assignno = int.Parse(btnDel.CommandArgument);

            AssignmentController objAM = new AssignmentController();
            Assignment objAssign = new Assignment();

            objAssign.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objAssign.UA_NO = Convert.ToInt32(Session["userno"]);
            objAssign.AS_NO = assignno;

            if (Convert.ToInt16(objAM.DeleteAssignment(objAssign)) == Convert.ToInt16(CustomStatus.RecordDeleted))
            {
                lblStatus.Text = "Assignment Deleted Successfully...";
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/student_assignment_reply/" + "assignment_reply" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    void GetAttachmentByAsNo(int sano)
    {

        try
        {
            AssignmentController objAM = new AssignmentController();
            DataSet ds;
            ds = objAM.GetStudentAttachments(sano);
            lvAttachments.DataSource = ds;
            lvAttachments.DataBind();

            DataSet DS1 = objCommon.FillDropDown("ACD_ISTUDASSIGNMENT", "ATTACHMENT", "", "SA_NO=" + sano, "");
            string blob = DS1.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            if (blob == "1")
            {
                foreach (ListViewItem lvRow in lvAttachments.Items)
                {
                    Control ckBox2 = (Control)lvRow.FindControl("tdattachblob");
                    Control ckbox3 = (Control)lvRow.FindControl("tdbtndownload");
                    ckBox2.Visible = true;
                    ckbox3.Visible = true;

                }
            }
            else
            {

                foreach (ListViewItem lvRow in lvAttachments.Items)
                {
                    Control ckBox2 = (Control)lvRow.FindControl("tdblob");

                    ckBox2.Visible = true;

                }
            }





        }
        catch (Exception ex)
        {

        }

    }

    private void BindAssignmentList()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            DataSet ds = objAM.GetAllAssignmentListByUaNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["OrgId"]));
            lvAssignment.DataSource = ds;
            lvAssignment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetStatus(object status)
    {
        DateTime DT = Convert.ToDateTime(status);
        if (DT < DateTime.Today)
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    protected void btnStudeReply_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int as_no = int.Parse(lnkbtn.CommandArgument);
        ViewState["as_no"] = as_no;
        BindListView();

        divAssinList.Visible = false;
        divStudList.Visible = true;

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

    public Boolean checkeEnable(object count)
    {
        if (Convert.ToInt32(count) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void imgdow_Click(object sender, ImageClickEventArgs e)
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
}
