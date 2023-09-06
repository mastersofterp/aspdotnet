//======================================================================================
// PROJECT NAME  :RFC_CODE                                                                
// MODULE NAME   : Moocs Certification                   
// CREATION DATE : 11-03-2022                                                        
// CREATED BY    : DIKSHA NANDURKAR  
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//=============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
public partial class ACADEMIC_StudentAchievement_Moocs_Certification : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MoocsCertificationEntity objMCE = new MoocsCertificationEntity();
    MoocsCertificationController objMCC = new MoocsCertificationController();
    string PageId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDown();
            BindListView();
            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                
                //ViewState["ipAddress"] = IPADDRESS;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
              

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {


                    BindListView();

                }
                else
                {
                    objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);

                    div2.Visible = false;
                    lvMoocs.Visible = false;

                    return;

                    Page.Title = Session["coll_name"].ToString();

                    PageId = Request.QueryString["pageno"];

                }
            }


        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Moocs_Certification.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Moocs_Certification.aspx");
        }
    }

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID>0", "ACADMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlPlatform, "ACD_ACHIEVEMENT_MOOCS_PLATFORM", "MOOCS_PLATFORM_ID", "MOOCS_PLATFORM", "MOOCS_PLATFORM_ID>0", "MOOCS_PLATFORM_ID");
            objCommon.FillDropDownList(ddlDuration, "ACD_ACHIEVEMENT_DURATION", "DURATION_ID", "DURATION", "DURATION_ID>0", "DURATION_ID");
         }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MoocsCertification.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }


    protected void btnSubmitMoocsCertification_Click(object sender, System.EventArgs e)
    {
        objMCE.uno = Convert.ToInt32(Session["userno"]);
        objMCE.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        objMCE.idno = Convert.ToInt32(Session["idno"]);
        objMCE.acadamic_year_id = Convert.ToInt32(ddlAcademicYear.SelectedValue);
        objMCE.course_name = txtNameofCourse.Text.Trim();
        objMCE.moocs_platform_id = Convert.ToInt32(ddlPlatform.SelectedValue);
        objMCE.institute_university = txtOfferedByInstitute.Text.Trim();
        if (hfdActive.Value == "true")
        {
            objMCE.fa_status = true;
        }
        else
        {
            objMCE.fa_status = false;
        }


        
        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage(this, "Please select Start Date End Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];
        //DateTime dateTime10 = Convert.ToDateTime(a);
        DateTime dtStartDate = DateTime.Parse(StartDate);
        objMCE.SDate = DateTime.Parse(StartDate);
        DateTime dtEndDate = DateTime.Parse(EndDate);
        objMCE.EDate = DateTime.Parse(EndDate);

        objMCE.duration_id = Convert.ToInt32(ddlDuration.SelectedValue);
        objMCE.amount = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? 0 : Convert.ToDecimal(txtAmount.Text);
        objMCE.OrganizationId = Convert.ToInt32(Session["OrgId"]);
        objMCE.file_name = FileUpload1.FileName.ToString();
        string filename = FileUpload1.FileName.ToString();
        objMCE.file_name = FileUpload1.FileName.ToString();
        string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
        if (FileUpload1.HasFile)
        {
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ACADEMIC/FileUpload/" + FileUpload1.FileName));
            string path = Server.MapPath("~/ACADEMIC/FileUpload/");
            path = path + filename;
            decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
            Double FileSize = FileUpload1.PostedFile.ContentLength;
            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (ext.ToUpper().Trim() == ".PDF")
            {

                byte[] ImagePhotoByte;
                double PhotoFileSize = FileUpload1.PostedFile.ContentLength;
                 if (PhotoFileSize > 1000000)
                {

                    byte[] resizephoto = ResizePhoto(FileUpload1);
                    Response.Write("<script>alert('File Size Must Not Exceed 1 MB')</script>");
                    return;
                }
                else
                {
                    using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                    {
                        ImagePhotoByte = FileUpload1.FileBytes;
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(this, "Only PDF files are allowed!", this.Page);
                return;
            }


        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objMCE.moocs_certification_id = Convert.ToInt32(ViewState["mpid"]);
            CustomStatus cs = (CustomStatus)objMCC.UpdateMoocsData(objMCE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))


            BindListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ClearMoocsData();

        }

        else
        {

            CustomStatus cs = (CustomStatus)objMCC.InsertMoocsCertificationData(objMCE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListView();
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearMoocsData();
                
            }
        }

    }
        
    protected void lvMoocs_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }

    private void ShowDetail(int MOOCD_CERTIFICATION_ID)
    {
        DataSet ds = objMCC.EditMoocsplatform(MOOCD_CERTIFICATION_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            ddlDuration.SelectedValue = ds.Tables[0].Rows[0]["DURATION_ID"].ToString();
            ddlPlatform.SelectedValue = ds.Tables[0].Rows[0]["MOOCS_PLATFORM_ID"].ToString();
            txtNameofCourse.Text = ds.Tables[0].Rows[0]["NAME_OF_COURSE"].ToString();
            txtOfferedByInstitute.Text = ds.Tables[0].Rows[0]["OFFERED_BY_INSTITUTE_UNIVERSITY"].ToString();
            txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
            ddlAcademicYear.SelectedValue = ds.Tables[0].Rows[0]["ACADMIC_YEAR_ID"].ToString();
            txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy");
            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            hdnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            if (ds.Tables[0].Rows[0]["FA_STATUS"].ToString() == "True")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetMoocs(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetMoocs(false);", true);
            }
        }
 }
    protected void btnEditMoocs_Click(object sender, System.EventArgs e)
    {
        try
        {

            LinkButton btnEditMoocs = sender as LinkButton;
            int MOOCD_CERTIFICATION_ID = Convert.ToInt32(btnEditMoocs.CommandArgument);
            ViewState["mpid"] = Convert.ToInt32(btnEditMoocs.CommandArgument);
            ShowDetail(MOOCD_CERTIFICATION_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "MoocsCertification.btnEditMoocs_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void BindListView()
    {
        try
        {

            DataSet ds = objMCC.MoocsListView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlMoocs.Visible = true;
                lvMoocs.DataSource = ds.Tables[0];
                lvMoocs.DataBind();


            }
            else
            {
                pnlMoocs.Visible = true;
                lvMoocs.DataSource = null;
                lvMoocs.DataBind();

            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AchievementBasicDetails.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelMoocsCertification_Click(object sender, System.EventArgs e)
    {
        ClearMoocsData();
        
    }
    public void ClearMoocsData()
    {
        ViewState["action"] = null;
        txtNameofCourse.Text = "";
        ddlPlatform.SelectedIndex = 0;
        txtOfferedByInstitute.Text = "";
        ddlDuration.SelectedIndex = 0;
        txtAmount.Text = "";
        ddlAcademicYear.SelectedIndex = 0;
    }
    protected void btnDownload_Click(object sender, System.EventArgs e)
    {
        Button btnDownload = sender as Button;
             string filetemp = (sender as Button).CommandArgument;
             //string FILE_NAME = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
             string FILE_NAME = btnDownload.ToolTip;
             string filePath = Server.MapPath("~/ACADEMIC/FileUpload/" + FILE_NAME);
             FileInfo file = new FileInfo(filePath);

             if (file.Exists)
             {
                 Response.Clear();
                 Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                 Response.AddHeader("Content-Length", file.Length.ToString());
                 Response.ContentType = "application/octet-stream";
                 Response.Flush();
                 Response.TransmitFile(file.FullName);
                 Response.End();
             }
             else
             {
                 objCommon.DisplayUserMessage(this, "Requested file is not available to download", this.Page);
                 return;
             }
         }
    
}    

