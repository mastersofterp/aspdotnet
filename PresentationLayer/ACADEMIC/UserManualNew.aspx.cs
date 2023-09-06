using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class ACADEMIC_UserManualNew : System.Web.UI.Page
{
    Common objCommon = new Common();
    College objCollege = new College();
    CollegeController objController = new CollegeController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objstud = new StudentController();

    string FileName = string.Empty;
    public string Docpath = string.Empty;
    string DirPath = string.Empty;
    public int count = 0;
    public string studid = string.Empty;
    public string filename = string.Empty;
    public string enrollno = string.Empty;
    public string fname = string.Empty;
    public int i = 0;
    public string btnfilename = string.Empty;

    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    //PATH TO EXTRACT IMAGES
    private string DirPath1 = string.Empty;
    //private string DirPath1 = System.Configuration.ConfigurationManager.AppSettings["DirPath1"].ToString();

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
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.CheckPageAuthorization();
                }
                BindLVDetails();
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (Session["usertype"].ToString() != "1")
            {
                lblHeading.Text = "View User Manual";
                divNote.Visible = false;
            }
            else
            {
                lblHeading.Text = "User Manual Upload / Download";
                divNote.Visible = true;
            }

            //BindLV();
            //BindLVDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_DOCUMENT_SUBMISSION.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

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

    private void BindLVDetails()
    {
        try
        {
            DataSet ds = null;

            ds = objstud.GetAllUserManualByUserNo(Convert.ToInt32(Session["userno"]));

            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                lvDetailsList.DataSource = ds;
                lvDetailsList.DataBind();
            }
            else
            {
                lvDetailsList.DataSource = null;
                lvDetailsList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_BindLVDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void BindLV()
    //{
    //    try
    //    {
    //        DataSet ds = null;

    //        ds = objstud.GetAllUserManual(Convert.ToInt32(Session["userno"]));

    //        string isUploaded = string.Empty;
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataTable dt = ds.Tables[0];

    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                isUploaded += dr["AS_No"].ToString() + ", ";
    //            }

    //            string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
    //            ViewState["UploadedString"] = realUploaded;

    //            lvUserManualList.DataSource = ds;
    //            lvUserManualList.DataBind();

    //            foreach (ListViewDataItem item in lvUserManualList.Items)
    //            {
    //                //Label lblStatus = item.FindControl("lblStatus") as Label;
    //                FileUpload fu = item.FindControl("fuFile") as FileUpload;
    //                Button btnSubmit = item.FindControl("btnSubmit") as Button;
    //                LinkButton lnkDownloadDoc = item.FindControl("lnkDownloadDoc") as LinkButton;
    //                Label lblDownload = item.FindControl("lblDownload") as Label;


    //                if (Session["usertype"].ToString() != "1")
    //                {
    //                    //lblStatus.Visible = false;
    //                    fu.Visible = false;
    //                    btnSubmit.Visible = false;
    //                    lvUserManualList.FindControl("lblUserManual").Visible = false;
    //                    lvUserManualList.FindControl("lblUpload").Visible = false;
    //                    //lvUserManualList.FindControl("lblThStatus").Visible = false;

    //                }
    //                else
    //                {
    //                    //lblStatus.Visible = true;
    //                    fu.Visible = true;
    //                    btnSubmit.Visible = true;
    //                    lvUserManualList.FindControl("lblUserManual").Visible = true;
    //                    lvUserManualList.FindControl("lblUpload").Visible = true;
    //                    //lvUserManualList.FindControl("lblThStatus").Visible = true;
    //                }
    //            }

    //            //btnCancel.Visible = true;
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updUserManual, "No Record Found", this.Page);
    //        }

    //        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
    //        {
    //            ImageButton ImgPhoto = lvUserManualList.Items[i].FindControl("lnkDownload") as ImageButton;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACDEMIC_DOCUMENT_SUBMISSION.Page_Load -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void lvUserManualList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;
                string compareString = ViewState["UploadedString"].ToString();
                string[] cs = compareString.Split(',');
                string stringToCompare = dr["ASNo"].ToString();
                filename = dr["AS_Title"].ToString();
                int consist = 0;
                string data = string.Empty;
                //consist = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + studid + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + studid + "'"));
                data = objCommon.LookUp("ACD_ADM_USERMANUAL_LIST", "isnull(UMNO,0)UMNO", "");
                if (String.IsNullOrEmpty(data))
                    consist = 0;
                else
                    consist = Convert.ToInt32(objCommon.LookUp("ACD_ADM_USERMANUAL_LIST", "isnull(UMNO,0)UMNO", ""));

                DataSet ds = objCommon.FillDropDown("ACD_ADM_USERMANUAL_LIST", "DISTINCT ASNO", "", "", "");
              
                if (!String.IsNullOrEmpty(stringToCompare))
                {
                    if (compareString.Contains(stringToCompare) && consist != 0)
                    {                      
                        ((LinkButton)e.Item.FindControl("lnkDownloadDoc")).Visible = true;
                        ((Label)e.Item.FindControl("lblDownload")).Visible = false;

                        ((LinkButton)e.Item.FindControl("lnkView")).Visible = true;
                        ((Label)e.Item.FindControl("lblPreview")).Visible = false;
                    }
                }
                else
                {                   
                    ((LinkButton)e.Item.FindControl("lnkDownloadDoc")).Visible = false;
                    ((Label)e.Item.FindControl("lblDownload")).Visible = true;
                    ((Label)e.Item.FindControl("lblDownload")).Text = "File Not Uploaded";
                    ((Label)e.Item.FindControl("lblDownload")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblDownload")).Font.Bold = true;

                    ((LinkButton)e.Item.FindControl("lnkView")).Visible = false;
                    ((Label)e.Item.FindControl("lblPreview")).Visible = true;
                    ((Label)e.Item.FindControl("lblPreview")).Text = "Preview not available";
                    ((Label)e.Item.FindControl("lblPreview")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblPreview")).Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC_UserManual.lvUserManualList_ItemDataBound-> " + ex.ToString());
        }
    }

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        //string[] Ext = { ".JPEG", ".BMP", ".GIF", ".PDF", ".PNG", ".TIFF", "ICO", ".JPG" };
        string[] Ext = { ".PDF", ".PNG",".JPEG",".JPG" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention.ToUpper() == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    public byte[] GetImageDataForDocumentation(FileUpload fu)
    {
        if (fu.HasFile)
        {
            int ImageSize = fu.PostedFile.ContentLength;
            Stream ImageStream = fu.PostedFile.InputStream as Stream;
            byte[] ImageContent = new byte[ImageSize];
            int intStatus = ImageStream.Read(ImageContent, 0, ImageSize);
            //ImageStream.Close();
            // ImageStream.Dispose();
            return ImageContent;
        }
        else
        {
            FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
            int ImageSize = (int)ff.Length;
            byte[] ImageContent = new byte[ff.Length];
            ff.Read(ImageContent, 0, ImageSize);
            ff.Close();
            ff.Dispose();
            return ImageContent;
        }
    }

    private void CheckFileAndSave(string filename, string pth)
    {

        int AS_NO = 0;
        string filenames = string.Empty;

        int flag = 0;

        foreach (ListViewDataItem dataitem in lvDetailsList.Items)
        {
            ListView lv = dataitem.FindControl("lvUserManualList") as ListView;
            foreach (ListViewDataItem items in lv.Items)
            {

                filenames = (items.FindControl("lblUMNo") as Label).Text;

                if (filenames == btnfilename)
                {
                    FileUpload fuStudPhoto = items.FindControl("fuFile") as FileUpload;
                    string name = fuStudPhoto.ToolTip;

                    HiddenField hdno = items.FindControl("HiddenField1") as HiddenField;
                    AS_NO = Convert.ToInt32(hdno.Value);
                    HiddenField hdnSMID = items.FindControl("HiddenField2") as HiddenField;
                    int SMID = Convert.ToInt32(hdnSMID.Value);

                    string fileType = System.IO.Path.GetExtension(fname);
                    byte[] image;

                    if (fuStudPhoto.HasFile)
                    {
                        if (i == 1)
                        {
                            i = i + 1;
                            image = null;//objCommon.GetImageData(fuStudPhoto);
                            flag = 2;
                            CustomStatus cs = (CustomStatus)objstud.InsertUserManual(AS_NO, fileType, filename, pth, flag, SMID);
                            objCommon.DisplayMessage("Document Uploaded Successfully !", this);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updUserManual, "Please select only one file at a time.", this.Page);
                            //BindLV();
                            BindLVDetails();
                            return;
                        }
                    }
                    else
                    {
                        image = null;
                        flag = 1;
                    }
                }
                else
                {

                }
            }
        }

        //BindLV();
        BindLVDetails();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            DataSet ds = null;
            

            foreach (ListViewDataItem dataitem in lvDetailsList.Items)
            {
                ListView lv = dataitem.FindControl("lvUserManualList") as ListView;
                foreach (ListViewDataItem Items in lv.Items)
                {
                    HiddenField hdno = Items.FindControl("HiddenField1") as HiddenField;
                    //HiddenField hdnSMID = Items.FindControl("HiddenField2") as HiddenField;
                    
                    int docid1 = Convert.ToInt32(hdno.Value);
                    //int SMID = Convert.ToInt32(hdnSMID.Value);

                    string filename = (Items.FindControl("lblUMNo") as Label).Text;
                    btnfilename = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
                    FileUpload Fu1 = Items.FindControl("fuFile") as FileUpload;

                    if (filename == btnfilename)
                    {
                        if (Fu1.HasFile)
                        {
                            if (filename == btnfilename)
                            {
                                FileUpload Fu = new FileUpload();
                                Byte[] UserManual = null;
                                string path = MapPath("~/UPLOAD_FILES/USER_MANUAL");
                                Fu = Items.FindControl("fuFile") as FileUpload;
                                //int umno = Convert.ToInt32(objCommon.LookUp("ACC_SECTION", "AS_No", "AS_Title='" + filename + "'"));
                                int umno = Convert.ToInt32(objCommon.LookUp("ACC_SECTION A INNER JOIN ACD_SUBMODULE_MASTER B ON A.AS_NO = B.MID", "SMID", "SUB_MODULE_NAME='" + filename + "'"));

                                if (Fu.HasFile)
                                {
                                    filename = (Items.FindControl("lblUMNo") as Label).Text;
                                    Button submit = Items.FindControl("btnSubmit") as Button;
                                    string filesname = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
                                    if (filename == filesname)
                                    {
                                        if (i == 0)
                                        {
                                            i = i + 1;
                                            count++;
                                            //fname = idno + "_" + docno + "_" + Fu.FileName;
                                            fname = umno + "_" + Fu.FileName;
                                            //fname = Fu.FileName;
                                            Session["FileUpload1"] = fname;
                                            string fileType = System.IO.Path.GetExtension(fname);
                                            if (!FileTypeValid(fileType))
                                            {
                                                objCommon.DisplayMessage(this.Page, "Please Upload Valid Files like .pdf, .doc, .docx, .PNG, .JPEG, .JPG file format", this.Page);
                                                Fu.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                UserManual = GetImageDataForDocumentation(Fu);
                                            }

                                            string existpath = path + "\\" + fname;

                                            string[] array1 = Directory.GetFiles(path);
                                            foreach (string str in array1)
                                            {
                                                if ((existpath).Equals(str))
                                                {
                                                    objCommon.DisplayMessage("File with similar name already exists!", this);
                                                    return;
                                                }
                                            }

                                            double UserManualLength = UserManual.Length;
                                            if ((UserManualLength / 1024) > 10000.0 || (UserManualLength / 1024) < 0.01)
                                            {
                                                objCommon.DisplayMessage(this.Page, "File Size Required Between 0 kb - 10 MB!!", this.Page);
                                                Fu.Focus();
                                                return;
                                            }
                                            if (!(Directory.Exists(MapPath("~/PresentationLayer/UPLOAD_FILES/USER_MANUAL"))))
                                                Directory.CreateDirectory(path);

                                            string fileName = Path.GetFileName(fname);
                                            Fu.PostedFile.SaveAs((Server.MapPath("~//UPLOAD_FILES//USER_MANUAL//")) + fname);

                                            ViewState["FileName"] = fname;

                                            CheckFileAndSave(fname, path);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        //BindLV();
                                        BindLVDetails();
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updUserManual, "Please Select File To Upload", this);
                            return;
                        }
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updUserManual, "Please Select File To Upload", this);
                        //return;
                    }

                }
            }
            return;

            if (count <= 0)
            {
                objCommon.DisplayMessage(updUserManual, "Please Select File For Uploaded", this);
            }
            else
            {
                objCommon.DisplayMessage(updUserManual, "Record Updated Successfully", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_UserManual.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkDownloadDoc_Click(object sender, EventArgs e)
    {
        try
        {
            //string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
            int as_no = Convert.ToInt32(((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument);
            int smid = Convert.ToInt32(((System.Web.UI.WebControls.LinkButton)(sender)).CommandName);

            string ContentType = string.Empty;
            //int as_no = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + lblRegno.Text + "' OR ROLLNO='" + lblRegno.Text + "'");
            string documentname = objCommon.LookUp("ACD_ADM_USERMANUAL_LIST", "FILE_NAME", "ASNO=" + as_no +" AND SMID="+ smid);
            string filepath = Server.MapPath("~//UPLOAD_FILES//USER_MANUAL/");

            string file = filepath + documentname;

            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo myfile = new FileInfo(filepath + documentname);

            // Checking if file exists
            if (myfile.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + documentname);
                Response.TransmitFile(file);
                Response.Flush();
                Response.End();
            }
            else
            {
                //foreach (ListViewDataItem lvitem in lvUserManualList.Items)
                //{
                //    LinkButton lnkDownloadDoc = lvitem.FindControl("lnkDownloadDoc") as LinkButton;
                //    Label lblDownload = lvitem.FindControl("lblDownload") as Label;
                //    lnkDownloadDoc.Visible = false;
                //    lblDownload.Visible = true;
                //    lblDownload.Text = "File Not Uploaded";
                //    lblDownload.ForeColor = System.Drawing.Color.Red;
                //    lblDownload.Font.Bold = true;
                //}
                objCommon.DisplayMessage(updUserManual, "User Manual Not Uploaded !!!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_UserManual.lnkDownloadDoc_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void lvDetailsList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        DataSet ds = null;
        //Label IoNO = dataitem.FindControl("lbIoNo") as Label;
        Label lblAsTitle = dataitem.FindControl("lblAsTitle") as Label;
        string as_no = lblAsTitle.ToolTip;
        //Label lblsessionnm = dataitem.FindControl("lblSessionname") as Label;
        //int sessionno = Convert.ToInt32(rdolistsession.SelectedValue);
        //int idno = Convert.ToInt32(IoNO.ToolTip);
        //int semesterno = Convert.ToInt32(sem.ToolTip);
        ListView lv = dataitem.FindControl("lvUserManualList") as ListView;
        try
        {
            ds = objstud.GetAllUserManual(Convert.ToInt32(Session["userno"]), Convert.ToInt32(as_no));
            //lv.DataSource = ds;
            //lv.DataBind();
            string isUploaded = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    isUploaded += dr["AS_No"].ToString() + ", ";
                }

                string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
                ViewState["UploadedString"] = realUploaded;

                lv.DataSource = ds;
                lv.DataBind();

                foreach (ListViewDataItem item in lv.Items)
                {
                    //Label lblStatus = item.FindControl("lblStatus") as Label;
                    FileUpload fu = item.FindControl("fuFile") as FileUpload;
                    Button btnSubmit = item.FindControl("btnSubmit") as Button;
                    LinkButton lnkDownloadDoc = item.FindControl("lnkDownloadDoc") as LinkButton;
                    Label lblDownload = item.FindControl("lblDownload") as Label;


                    if (Session["usertype"].ToString() != "1")
                    {
                        //lblStatus.Visible = false;
                        fu.Visible = false;
                        btnSubmit.Visible = false;
                        lv.FindControl("lblUserManual").Visible = false;
                        lv.FindControl("lblUpload").Visible = false;
                        //lvUserManualList.FindControl("lblThStatus").Visible = false;

                    }
                    else
                    {
                        //lblStatus.Visible = true;
                        fu.Visible = true;
                        btnSubmit.Visible = true;
                        lv.FindControl("lblUserManual").Visible = true;
                        lv.FindControl("lblUpload").Visible = true;
                        //lvUserManualList.FindControl("lblThStatus").Visible = true;
                    }
                }

                //btnCancel.Visible = true;
                //lv.DataSource = ds;
                //lv.DataBind();
            }
            else
            {
                //objCommon.DisplayMessage(updUserManual, "No Record Found", this.Page);
            }

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                ImageButton ImgPhoto = lv.Items[i].FindControl("lnkDownload") as ImageButton;
            }
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_Comprehensive_Stud_Report.lvCollege_ItemDatabound() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkView = (LinkButton)(sender);
        string path = lnkView.CommandArgument;

       iframeView.Attributes.Add("src", path);
       
        mpeViewDocument.Show();
    }
}