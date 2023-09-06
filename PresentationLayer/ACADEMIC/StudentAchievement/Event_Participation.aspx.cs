//======================================================================================
// PROJECT NAME  :RFC_CODE                                                                
// MODULE NAME   :Event Participation                  
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
public partial class ACADEMIC_StudentAchievement_Event_Participation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventParticipationController OBJEPC = new EventParticipationController();
    EventParticipationEntity objEPE = new EventParticipationEntity();
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

              
                
                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    

                    BindListView();
                   
                }
                else
                {
                    objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);
                    
                    div3.Visible = false;
                    lvPraticipation.Visible = false;

                    return;

                    Page.Title = Session["coll_name"].ToString();

                    PageId = Request.QueryString["pageno"];

                }
            }


        }
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Event_Participation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Event_Participation.aspx");
        }
    }
     public void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID>0", "ACADMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlEventCategory, "ACD_ACHIEVEMENT_EVENT_CATEGORY", "EVENT_CATEGORY_ID", "EVENT_CATEGORY_NAME", "EVENT_CATEGORY_ID>0", "EVENT_CATEGORY_ID");
            objCommon.FillDropDownList(ddlActivityCategory, "ACD_ACHIEVEMENT_ACTIVITY_CATEGORY", "ACTIVITY_CATEGORY_ID", "ACTIVITY_CATEGORY_NAME", "ACTIVITY_CATEGORY_ID>0", "ACTIVITY_CATEGORY_ID");
            objCommon.FillDropDownList(ddlEventTitle, "ACD_ACHIEVEMENT_CREATE_EVENT", "CREATE_EVENT_ID", "EVENT_TITLE", "CREATE_EVENT_ID>0", "CREATE_EVENT_ID");
            objCommon.FillDropDownList(ddlParticipationType, "ACD_ACHIEVEMENT_PARTICIPATION_TYPE", "PARTICIPATION_TYPE_ID", "PARTICIPATION_TYPE", "PARTICIPATION_TYPE_ID>0", "PARTICIPATION_TYPE_ID");
          


        }
        catch (Exception ex)
        {
            throw;
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
     protected void btnSubmitAcademicYear_Click(object sender, System.EventArgs e)
     {
         objEPE.uno = Convert.ToInt32(Session["userno"]);
         objEPE.IPADDRESS = ViewState["ipAddress"].ToString();
         objEPE.idno = Convert.ToInt32(Session["idno"]);
         objEPE.acadamic_year_id = Convert.ToInt32(ddlAcademicYear.SelectedValue);
         objEPE.event_category_id = Convert.ToInt32(ddlEventCategory.SelectedValue);
         objEPE.activity_category_id = Convert.ToInt32(ddlActivityCategory.SelectedValue);
         objEPE.create_event_id = Convert.ToInt32(ddlEventTitle.SelectedValue);
         objEPE.participation_type_id = Convert.ToInt32(ddlParticipationType.SelectedValue);
         objEPE.event_participation_id = Convert.ToInt32(ddlAcademicYear.SelectedValue);
         
         if (hfdActive.Value == "true" )
         {
             objEPE.fc_status = true;
         }
         else
         {
             objEPE.fc_status = false;
         }

         objEPE.amount = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? 0 : Convert.ToDecimal(txtAmount.Text);
             //Convert.ToDecimal(txtAmount.Text);
         objEPE.OrganizationId = Convert.ToInt32(Session["OrgId"]);
         objEPE.file_name = FileUpload1.FileName.ToString();
         string filename = FileUpload1.FileName.ToString();
         objEPE.file_name = FileUpload1.FileName.ToString();
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

             objEPE.event_participation_id = Convert.ToInt32(ViewState["epid"]);
             CustomStatus cs = (CustomStatus)OBJEPC.UpdatEeventParticipation(objEPE);
             //Check for add or edit
             if (cs.Equals(CustomStatus.RecordUpdated))


              BindListView();
             objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
             ClearEventPraticipationData();
         
         }

         else
         {

             CustomStatus cs = (CustomStatus)OBJEPC.InsertEventParticipationData(objEPE);
             if (cs.Equals(CustomStatus.RecordSaved))
             {
                 BindListView();
                 objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                 ClearEventPraticipationData();
             }


         }

     }
     protected void lvPraticipation_ItemEditing(object sender, ListViewEditEventArgs e)
     {

     }

    protected void BindListView()
     {
         try
         {

             DataSet ds = OBJEPC.EventParticipationListView();

             if (ds.Tables[0].Rows.Count > 0)
             {
                 pnlPraticipation.Visible = true;
                 lvPraticipation.DataSource = ds.Tables[0];
                 lvPraticipation.DataBind();


             }
             else
             {
                 pnlPraticipation.Visible = true;
                 lvPraticipation.DataSource = null;
                 lvPraticipation.DataBind();

             }
            
        
         }
         catch (Exception ex)
         {
             throw;
         }
     }

     
     protected void btnEditEventPartion_Click(object sender, System.EventArgs e)
     {
         try
         {



             LinkButton btnEditEventPartion = sender as LinkButton;
             int EVENT_PARTICIPATION_ID = Convert.ToInt32(btnEditEventPartion.CommandArgument);
             ViewState["epid"] = Convert.ToInt32(btnEditEventPartion.CommandArgument);
             ShowDetail(EVENT_PARTICIPATION_ID);
             ViewState["action"] = "edit";

            

         }
         catch (Exception ex)
         {
             throw;
         }
     }
         private void ShowDetail(int EVENT_PARTICIPATION_ID)
    {
        DataSet ds = OBJEPC.EditEeventParticipation(EVENT_PARTICIPATION_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
          
            ddlAcademicYear.SelectedValue = ds.Tables[0].Rows[0]["ACADMIC_YEAR_ID"].ToString();
            ddlEventCategory.SelectedValue = ds.Tables[0].Rows[0]["EVENT_CATEGORY_ID"].ToString();
            ddlActivityCategory.SelectedValue = ds.Tables[0].Rows[0]["ACTIVITY_CATEGORY_ID"].ToString();
            ddlEventTitle.SelectedValue = ds.Tables[0].Rows[0]["CREATE_EVENT_ID"].ToString();
            ddlParticipationType.SelectedValue = ds.Tables[0].Rows[0]["PARTICIPATION_TYPE_ID"].ToString();
            txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();


            if (ds.Tables[0].Rows[0]["FA_STATUS"].ToString() == "True")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setvalidation(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setvalidation(false);", true);
            }
            
        }


    }
         protected void btnCancelAcademicYear_Click(object sender, System.EventArgs e)
         {
             ClearEventPraticipationData();
         }
         public void ClearEventPraticipationData()
         {
             ViewState["action"] = null;
             ddlAcademicYear.SelectedIndex = 0;
             ddlEventCategory.SelectedIndex = 0;
             ddlActivityCategory.SelectedIndex = 0;
             ddlEventTitle.SelectedIndex = 0;
             ddlParticipationType.SelectedIndex = 0;
             txtAmount.Text = "";
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

