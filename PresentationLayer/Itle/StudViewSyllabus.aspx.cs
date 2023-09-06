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
using IITMS.NITPRM;
using System.Data.SqlClient;

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

public partial class Itle_StudViewSyllabus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    ISyllabusController objSC = new ISyllabusController();
    CourseControlleritle objCourse = new CourseControlleritle();
    BlobController objBlob = new BlobController();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];
                
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
               //BindTreeView();

              
               BlobDetails();
               BindListView();

               // Used to insert id,date and courseno in Log_History table
               int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
            }
        }
       
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudViewSyllabus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudViewSyllabus.aspx");
        }
    }

    private void BindListView()
    {
        ISyllabus objSub = new ISyllabus();
        ISyllabusController objSC = new ISyllabusController();

        objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
        objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

        //objSub.UA_NO = Convert.ToInt32(Session["userno"]);
        DataSet ds = objSC.GetSyllabusByUaNo(objSub);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvSyllabus.DataSource = ds;
            lvSyllabus.DataBind();
            DivSyllabusList.Visible = true;

            if ( lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvSyllabus.FindControl("divBlobDownload");
                ctrHeader.Visible = true;

                foreach (ListViewItem lvRow in lvSyllabus.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvSyllabus.FindControl("divDownload");
                ctrHeader.Visible = true;

                foreach (ListViewItem lvRow in lvSyllabus.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = true;
                }
            }
        }
        else
        {
            lvSyllabus.DataSource = null;
            lvSyllabus.DataBind();
            DivSyllabusList.Visible = false;
        }
    }    

    private void BindTreeView()
    {
        try
        {
            ISyllabus objSub = new ISyllabus();
            ISyllabusController objSC = new ISyllabusController();
           
            objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            Fill_TreeViewSyllabus(objSub); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_SyllabusMaster.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Fill_TreeViewSyllabus(ISyllabus objSub)
    {
        try
        {
            string lnksection = string.Empty;
            string lnkunit = string.Empty;
            TreeNode xx = null;
            TreeNode yy = null;
            TreeNode zz = null;
            TreeNode tt = null;

                                   
            DataTableReader dtr = objSC.GetSyllabusViewBySession(objSub);
            while (dtr.Read())
            {
                if (dtr["section"].ToString().Trim() != string.Empty && dtr["section"].ToString().Trim() != lnksection)                
                {
                    xx = new TreeNode();
                    xx.Text = dtr["section"].ToString();
                    tvSyllabus.Nodes.Add(xx);
                    if (dtr["unit_name"].ToString().Trim() != string.Empty && dtr["unit_name"].ToString().Trim() != lnkunit)
                    {
                        yy = new TreeNode();
                        yy.Text = dtr["unit_name"].ToString();
                        xx.ChildNodes.Add(yy);
                        if (dtr["topic_name"].ToString().Trim() != string.Empty)
                        {
                            zz = new TreeNode();
                            zz.Text = dtr["topic_name"].ToString();
                            zz.ToolTip = dtr["sub_no"].ToString();
                            yy.ChildNodes.Add(zz);
                        }
                    }                   
                }
                else
                {
                    if (dtr["section"].ToString().Trim() == string.Empty)
                    {
                        xx = new TreeNode();
                        xx.Text = "-";
                        tvSyllabus.Nodes.Add(xx);
                    }
                    if (dtr["unit_name"].ToString().Trim() != string.Empty && dtr["unit_name"].ToString().Trim() != lnkunit)
                    {
                        yy = new TreeNode();
                        yy.Text = dtr["unit_name"].ToString();
                        xx.ChildNodes.Add(yy);
                        if (dtr["topic_name"].ToString().Trim() != string.Empty)
                        {
                            zz = new TreeNode();
                            zz.Text = dtr["topic_name"].ToString();
                            zz.ToolTip = dtr["sub_no"].ToString();
                            yy.ChildNodes.Add(zz);
                        }
                    }
                    else
                    {
                        //yy = new TreeNode();
                        //yy.Text = dtr["unit_name"].ToString();
                        //xx.ChildNodes.Add(yy);
                        if (dtr["topic_name"].ToString().Trim() != string.Empty)
                        {
                            zz = new TreeNode();
                            zz.Text = dtr["topic_name"].ToString();
                            zz.ToolTip = dtr["sub_no"].ToString();
                            yy.ChildNodes.Add(zz);
                        }
                    }                
                }
                if (dtr["section"] != null & dtr["section"].ToString() != string.Empty)
                    lnksection = dtr["section"].ToString();
                else
                    lnksection = "-";
                if (dtr["unit_name"] != null & dtr["unit_name"].ToString() != string.Empty)
                    lnkunit = dtr["unit_name"].ToString();
            }            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
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
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Session["colcode"]; // ",COURSE_NAME=" + Session["ICourseName"] +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "StudViewSyllabus.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void btnViewSyllabus_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Itle_Syllabus", "Itle_View_Syllabus.rpt");
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();

      //  string filePath = file_path + "Itle/upload_files/SYLLABUS/" + "SYLLABUS_" + Convert.ToInt32(an_no) + System.IO.Path.GetExtension(fileName);
       // Response.Redirect("DownloadAttachment.aspx?file=" + filePath + "&filename=" + fileName);

        string filePath = file_path + "Itle/upload_files/SYLLABUS/" + "SYLLABUS_" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    #endregion

    #region Public Methods

    public string GetFileName(object filename, object announcemetnno)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/Syllabus/Syllabus_" + "SYLLABUS_" + Convert.ToInt32(announcemetnno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/Syllabus_/" + filename.ToString();
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

    #endregion

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
                embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
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
                embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
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
