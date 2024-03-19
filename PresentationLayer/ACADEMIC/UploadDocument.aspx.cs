using DynamicAL_v2;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using ICSharpCode.SharpZipLib.Zip;


public partial class ACADEMIC_UploadDocument : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objstud = new StudentController();


    DynamicControllerAL AL = new DynamicControllerAL();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();


    string FileName = string.Empty;
    public string Docpath = string.Empty;
    string DirPath = string.Empty;
    public int count = 0;
    public string studid = string.Empty;
    public string docname = string.Empty;
    public string enrollno = string.Empty;
    public string fname = string.Empty;
    public int i = 0;
    public string btndocname = string.Empty;

    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    //PATH TO EXTRACT IMAGES
    private string DirPath1 = string.Empty;
    //private string DirPath1 = System.Configuration.ConfigurationManager.AppSettings["DirPath1"].ToString();
    //#region digilocker cred
    //string ClientSecret = "7e3d07e964cf19918a1b";
    //string ClientId = "E006C74F";
    //string RedirectUrl = "";
    //string ClientSecret = "a3adea63d589e2d8b120";
    //string ClientId = "98FB0BEC";
    //string RedirectUrl = "";

    //#endregion digilocker cred
    protected void Page_Load(object sender, EventArgs e)
    {
        // RedirectUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/ACADEMIC/UploadDocument.aspx?pageno=2697";

        if (!Page.IsPostBack)
        {

            //string directoryPath = string.Empty;

            //string directoryName = "~/DownloadImg";
            //directoryPath = Server.MapPath(directoryName);

            //if (Directory.Exists(directoryPath.ToString()))
            //{

            //    Directory.Delete(directoryPath.ToString(), true);
            //}

            string ResponseCode = Request.QueryString["ResponseCode"];
            string ResponseState = Request.QueryString["ResponseState"];
            string ResponseError = Request.QueryString["ResponseError"];
            ViewState["ResponseCode"] = ResponseCode;
            ViewState["ResponseState"] = ResponseState;
            ViewState["ResponseError"] = ResponseError;
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];
                if (ViewState["usertype"].ToString() == "2")
                {
                    //BindLV();
                    BindDocument();
                    divadmissiondetails.Visible = false;
                    divAdmissionApproval.Visible = false;
                    divhome.Visible = false;
                    int FinalSubmit = 0;
                    if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                    {
                        FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    }
                    if (FinalSubmit == 1)
                    { divPrintReport.Visible = true; }
                    else
                    { divPrintReport.Visible = false; }
                    //divPrintReport.Visible = true;
                    // btnGohome.Visible = false;

                    string status = objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "STATUS", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    DataSet dsinfo = objCommon.FillDropDown("ACD_ADM_STUD_INFO_SUBMIT_LOG", "PERSONAL_INFO,ADDRESS_INFO,DOC_INFO,QUAL_INFO,OTHER_INFO,FINAL_SUBMIT", "ADMBATCH", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", string.Empty);
                    if (dsinfo != null && dsinfo.Tables[0].Rows.Count > 0)
                    {
                        string personal_info = dsinfo.Tables[0].Rows[0]["PERSONAL_INFO"].ToString();
                        string address_info = dsinfo.Tables[0].Rows[0]["ADDRESS_INFO"].ToString();
                        string doc_info = dsinfo.Tables[0].Rows[0]["DOC_INFO"].ToString();
                        string qual_info = dsinfo.Tables[0].Rows[0]["QUAL_INFO"].ToString();
                        string other_info = dsinfo.Tables[0].Rows[0]["OTHER_INFO"].ToString();
                        string final_submit = dsinfo.Tables[0].Rows[0]["FINAL_SUBMIT"].ToString();

                        if (personal_info == "1")
                        {
                            lnkAddressDetail.Enabled = true;
                        }
                        if (address_info == "1")
                        {
                            lnkUploadDocument.Enabled = true;
                        }
                        if (doc_info == "1")
                        {
                            lnkQualificationDetail.Enabled = true;
                        }
                        if (qual_info == "1")
                        {
                            lnkotherinfo.Enabled = true;
                        }
                        if (other_info == "1")
                        {
                            lnkprintapp.Enabled = true;
                        }
                        if (final_submit == "1")
                        {
                            btnSave.Visible = false;
                        }
                    }
                    CheckFinalSubmission();  // Added By Bhagyashree on 30052023
                    //if (status.ToString() != "")
                    //{
                    //    if (status == "1")
                    //    {
                    //        btnSave.Visible = false;
                    //    }
                    //    else if (status == "2")
                    //    {
                    //        btnSave.Visible = true;
                    //    }
                    //}

                    //Added By Prafull

                    //string PhtoSign = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(PHOTO,'-')+'$'+ISNULL(STUD_SIGN,'-')", "IDNO = " + Session["idno"] + "");
                    //DataTable dtBlobPic = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[0]);
                    //if (dtBlobPic.Rows.Count != 0)
                    //{
                    //    //imgpreview.ImageUrl = Convert.ToString(dtBlobPic.Rows[0]["uri"]);
                    //}

                    //DataTable dtBlobSign = AL.Blob_GetById(blob_ConStr, blob_ContainerName, PhtoSign.Split('$')[1]);
                    //if (dtBlobSign.Rows.Count != 0)
                    //{
                    //    //imgpreviewsign.ImageUrl = Convert.ToString(dtBlobSign.Rows[0]["uri"]);
                    //}



                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    //BindLV();
                    BindDocument();
                    divadmissiondetails.Visible = false;
                    divAdmissionApproval.Visible = true;
                    divhome.Visible = true;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                }
                else
                {
                    //BindLV();
                    BindDocument();

                    divadmissiondetails.Visible = true;
                    divAdmissionApproval.Visible = true;
                    divhome.Visible = true;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                    // btnGohome.Visible = true;

                    // pnlId.Visible = true;

                }

              
            }

            // Commented For Test - Ashish
            //IsTokenExpires(ResponseCode, ResponseState, ResponseError);
            //if (ResponseCode != null)
            //{
            //    PostbackToDigiLockerServer(ResponseCode, ResponseState);

            //    HttpContext context = HttpContext.Current;
            //    DataSet issueDoc = Session["IssuedDocList"] as DataSet;
            //    DataSet selfDoc = Session["SelfUploadedDocList"] as DataSet;

            //    if (issueDoc != null)
            //    {
            //        lvIssuedDoc.DataSource = issueDoc;
            //        lvIssuedDoc.DataBind();
            //        pnlModal.Visible = true;
            //        mp1.Show();
            //    }
            //    if (selfDoc != null)
            //    {
            //        lvUploadedDoc.DataSource = selfDoc;
            //        lvUploadedDoc.DataBind();
            //        pnlModal.Visible = true;
            //        mp1.Show();
            //    }
            //    //Session["IssuedDocList"] = null;//digilocker  deepali
            //    //Session["SelfUploadedDocList"] = null;//digilocker
            //}
            // Commented For Test - Ashish

            //Session["IssuedDocList"] = null;
        }


           //**digilocker
        //else
        //{
        //    if (Session["flag"] == "Downloaded")
        //    {
        //        Session["flag"] = null;
        //    }
        //    else
        //    {
        //        Session["flag"] = Session["flag"];
        //    }
        //}
        //string Error = Request.QueryString["error"];
        //authorization code after successful authorization   

        //string Code = Request.QueryString["code"];
        //string State = Request.QueryString["state"];
        //IsTokenExpires(Code, State, Error); --deepali
        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //BindList();

        //**digilocker
    }

    private void CheckFinalSubmission()
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        DataSet dsallowprocess = objstud.GetAllowProcess(Convert.ToInt32(Session["idno"]), 3, 'E');
        int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        if (finalsubmit == "1" && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
        {
            btnSave.Visible = true;
        }
    }
    private void BindLV()
    {
        string isUploaded = string.Empty;
        // get list of select document......
        //DataSet ds = objstud.GetAllselectDocument(Convert.ToString(txtTempIdno.Text));


        DataSet ds = null;
        if (Session["usertype"].ToString() != "2")
        {
            ds = objstud.GetAllselectDocument(Session["stuinfoidno"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    isUploaded += dr["DOCUMENTNO"].ToString() + ", ";
                }

                string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
                ViewState["UploadedString"] = realUploaded;

                lvDocumentsAdmin.DataSource = ds;
                lvDocumentsAdmin.DataBind();

                //added on 10-04-2020 by Vaishali
                //if (Session["usertype"].ToString() != "2")
                //{
                //    btnCancel.Visible = true;
                //}
                //else
                //    btnCancel.Visible = false;

            }
            else
            {
                //objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
                lvDocumentList.Visible = false;
            }

            foreach (ListViewItem Item in lvDocumentsAdmin.Items)
            {
                LinkButton lnkDownloadDoc = Item.FindControl("lnkDownloadDoc") as LinkButton;
                ImageButton imgbtnPrevDoc = Item.FindControl("imgbtnPrevDoc") as ImageButton;
            }

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                ImageButton ImgPhoto = lvDocumentsAdmin.Items[i].FindControl("lnkDownloadDoc") as ImageButton;
            }
            lvDocumentList.Visible = false;
        }
        else
            ds = objstud.GetAllselectDocument(Session["idno"].ToString());


        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                isUploaded += dr["DOCUMENTNO"].ToString() + ", ";
            }

            string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
            ViewState["UploadedString"] = realUploaded;

            lvDocumentList.DataSource = ds;
            lvDocumentList.DataBind();

            //added on 10-04-2020 by Vaishali
            //if (Session["usertype"].ToString() != "2")
            //{
            //    btnCancel.Visible = true;
            //}
            //else
            //    btnCancel.Visible = false;

        }
        else
        {
            //objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
        }

    }
    protected void btnGohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/StudentInfoEntryNew.aspx?pageno=2219");
    }
    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/PersonalDetails.aspx", false);

        Response.Redirect("~/academic/PersonalDetails.aspx");

        // HttpContext.Current.RewritePath("PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AddressDetails.aspx", false);

        Response.Redirect("~/academic/AddressDetails.aspx");
    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AdmissionDetails.aspx", false);
        Response.Redirect("~/academic/AdmissionDetails.aspx");

    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/DASAStudentInformation.aspx", false);
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }
    protected void lnkUploadDocument_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/UploadDocument.aspx");
    }
    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/QualificationDetails.aspx", false);
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/OtherInformation.aspx", false);
        Response.Redirect("~/academic/OtherInformation.aspx");
    }

    protected void lnkApproveAdm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/ApproveAdmission.aspx");
    }

    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Enrollment No!", this);
                // objCommon.DisplayMessage(this.page, "Please Search Enrollment No!!", this.Page);
            }
        }
    }
    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "pagetitle=Admission Form Report " + Session["stuinfoenrollno"].ToString();
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(regno) + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updUploadDocument, this.updUploadDocument.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkGoHome_Click(object sender, EventArgs e)
    {


        if (ViewState["usertype"].ToString() == "1")
        {
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }

    protected void lnkDownloadDoc_Click(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;

        string path = context.Request.Url.OriginalString.Contains("iitms").ToString();

        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;
        studid = Session["stuinfoidno"].ToString();
        string documentname = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENT_NAME", "DOCUMENTNO='" + filename + "' AND IDNO=" + studid);
        string filepath = Server.MapPath("~//UPLOAD_FILES//STUDENT_DOCUMENT/");

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
            objCommon.DisplayMessage(this.Page, "Document Not Uploaded by Student.", this.Page);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {


            foreach (ListViewItem Items in lvDocumentList.Items)
            {
                HiddenField hdno = Items.FindControl("HiddenField1") as HiddenField;
                int docid1 = Convert.ToInt32(hdno.Value);

                string docname = (Items.FindControl("lblDocument") as Label).Text;
                btndocname = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
                FileUpload Fu1 = Items.FindControl("fuFile") as FileUpload;

                if (docname == btndocname)
                {
                    if (Fu1.HasFile)
                    {
                        if (docname == btndocname)
                        {
                            FileUpload Fu = new FileUpload();
                            Byte[] StudentPhoto = null;
                            string path = MapPath("~/UPLOAD_FILES/STUDENT_DOCUMENT");
                            Fu = Items.FindControl("fuFile") as FileUpload;
                            //string enroll = 
                            int idno = Convert.ToInt32(Session["idno"]);
                            int docno = Convert.ToInt32(objCommon.LookUp("ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME='" + docname + "'"));

                            if (Fu.HasFile)
                            {
                                docname = (Items.FindControl("lblDocument") as Label).Text;
                                Button submit = Items.FindControl("btnSubmit") as Button;
                                string documentname = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
                                if (docname == documentname)
                                {
                                    if (i == 0)
                                    {
                                        i = i + 1;
                                        count++;
                                        fname = idno + "_" + docno + "_" + Fu.FileName;
                                        //fname = Fu.FileName;
                                        Session["FileUpload1"] = fname;
                                        string fileType = System.IO.Path.GetExtension(fname);
                                        if (!FileTypeValid(fileType))
                                        {
                                            objCommon.DisplayMessage(this.Page, "Please Upload Valid Files like .jpg, .jpeg, .pdf file format", this.Page);
                                            Fu.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            StudentPhoto = GetImageDataForDocumentation(Fu);
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

                                        double StudentPhotoLength = StudentPhoto.Length;
                                        //if ((StudentPhotoLength / 1024) > 3000 || (StudentPhotoLength / 1024) < 0.01)
                                        if ((StudentPhotoLength) > 500000.00 || (StudentPhotoLength) < 0.01)
                                        {
                                            objCommon.DisplayMessage(this.Page, "File Size Should be maximum 500 KB. !!", this.Page);
                                            Fu.Focus();
                                            return;
                                        }
                                        if (!(Directory.Exists(MapPath("~/UPLOAD_FILES/STUDENT_DOCUMENT"))))
                                            Directory.CreateDirectory(path);

                                        string fileName = Path.GetFileName(fname);
                                        Fu.PostedFile.SaveAs((Server.MapPath("~/UPLOAD_FILES//STUDENT_DOCUMENT/")) + fname);

                                        ViewState["FileName"] = fname;

                                        CheckFileAndSave(fname, path);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    BindLV();
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select File To Upload", this);
                    }
                }
                else
                {

                }

            }
            return;

            if (count <= 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select File For Uploaded", this);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "File Uploaded Successfully", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".JPEG", ".BMP", ".GIF", ".PDF", ".PNG", ".TIFF", "ICO", ".JPG" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention.ToUpper() == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    private void CheckFileAndSave(string docnm, string pth)
    {
        string idno = string.Empty;
        int docid = 0;
        string docnames = string.Empty;

        int chkOriCopy1 = 0;
        int chkTrueCopy1 = 0;
        int chkNA1 = 0;
        int flag = 0;

        foreach (ListViewDataItem items in lvDocumentList.Items)
        {
            //idno = Convert.ToString((txtTempIdno.Text)).ToString();

            studid = Session["idno"].ToString();

            docnames = (items.FindControl("lblDocument") as Label).Text;

            if (docnames == btndocname)
            {
                FileUpload fuStudPhoto = items.FindControl("fuFile") as FileUpload;
                string name = fuStudPhoto.ToolTip;

                HiddenField hdno = items.FindControl("HiddenField1") as HiddenField;
                docid = Convert.ToInt32(hdno.Value);

                string fileType = System.IO.Path.GetExtension(fname);
                byte[] image;

                if (fuStudPhoto.HasFile)
                {
                    if (i == 1)
                    {
                        i = i + 1;
                        image = null;//objCommon.GetImageData(fuStudPhoto);
                        flag = 2;
                        CustomStatus cs = (CustomStatus)objstud.InsertScanDocument(studid, docid, chkOriCopy1, chkTrueCopy1, chkNA1, image, fileType, flag, docnm, pth);
                        objCommon.DisplayMessage("File Uploaded Successfully", this);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please select only one file at a time.", this.Page);
                        BindLV();
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

        BindLV();
    }

    protected void lvDocumentList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;
                string compareString = ViewState["UploadedString"].ToString();
                string stringToCompare = dr["DOCUMENTNO"].ToString();
                docname = dr["DOCUMENT_NAME"].ToString();
                Button btnupload = dataItem.FindControl("btnSubmit") as Button;
                int consist = 0;
                if (ViewState["usertype"].ToString() == "2")
                {
                    string status = objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "STATUS", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    string final_submit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    if (final_submit == "1")
                    {
                        btnupload.Enabled = false;
                    }
                    if (status.ToString() != "")
                    {
                        if (status == "1")
                        {
                            btnupload.Enabled = false;
                        }
                        else if (status == "2")
                        {
                            btnupload.Enabled = true;
                        }
                    }

                    consist = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'"));

                }
                else
                {
                    consist = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["stuinfoidno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["stuinfoidno"]) + "'"));
                }
                if (compareString.Contains(stringToCompare) && consist != 0)
                {
                    if (dr["IDNO"].ToString() != string.Empty || dr["IDNO"] != DBNull.Value)
                    {
                        ((Label)e.Item.FindControl("lblStatus")).Text = "Uploaded";
                        ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                        ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                        //((LinkButton)e.Item.FindControl("lnkDownloadDoc")).Visible = true;
                        ((ImageButton)e.Item.FindControl("imgbtnPrevDoc")).Visible = true;
                        
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                        ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                        ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                    }
                }
                else
                {
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }

                if (dr["MANDATORY"].ToString() != string.Empty || dr["MANDATORY"] != DBNull.Value)
                {
                    if (dr["MANDATORY"].ToString() == "True")
                    {
                        ((Label)e.Item.FindControl("lblStar")).Visible = true;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblStar")).Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvDocumentList1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;
                string compareString = ViewState["UploadedString"].ToString();
                string stringToCompare = dr["DOCUMENTNO"].ToString();
                docname = dr["DOCUMENT_NAME"].ToString();
                int consist = 0;
                if (ViewState["usertype"].ToString() == "2")
                {
                    consist = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'"));
                }
                else
                {
                    consist = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["stuinfoidno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["stuinfoidno"]) + "'"));
                }
                if (compareString.Contains(stringToCompare) && consist != 0)
                {
                    if (dr["IDNO"].ToString() != string.Empty || dr["IDNO"] != DBNull.Value)
                    {
                        ((Label)e.Item.FindControl("lblStatus")).Text = "Uploaded";
                        ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                        ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                        //((LinkButton)e.Item.FindControl("lnkDownloadDoc")).Visible = true;
                        ((ImageButton)e.Item.FindControl("imgbtnPrevDoc1")).Visible = true;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                        ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                        ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                    }
                }
                else
                {
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
            case ".cs":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    protected void imgbtnPrevDoc_Click(object sender, EventArgs e)
    {


        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

        ImageButton lnkView = (ImageButton)(sender);
        string path = url + "UPLOAD_FILES/STUDENT_DOCUMENT/" + lnkView.CommandArgument;

        //  iframeView.Attributes.Add("src", path);
        iframeView.Src = path;

        // iframeView.Attributes.Add("src", path);

        // mpeViewDocument.Show();
    }

    protected void imgbtnPrevDoc1_Click(object sender, EventArgs e)
    {


        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

        ImageButton lnkView = (ImageButton)(sender);
        string path = url + "UPLOAD_FILES/STUDENT_DOCUMENT/" + lnkView.CommandArgument;

        //  iframeView.Attributes.Add("src", path);
        iframeView.Src = path;

        // iframeView.Attributes.Add("src", path);

        // mpeViewDocument.Show();
    }
    protected void lnkDownloadAdmin_Click(object sender, EventArgs e)
    {
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;
        studid = Session["idno"].ToString();
        string documentname = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENT_NAME", "DOCUMENTNO='" + filename + "' AND IDNO=" + studid);
        string filepath = Server.MapPath("~//UPLOAD_FILES//STUDENT_DOCUMENT/");

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
            objCommon.DisplayMessage(this.Page, "Document Not Uploaded by Student.", this.Page);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string userType = string.IsNullOrEmpty(ViewState["usertype"].ToString()) ? "" : ViewState["usertype"].ToString();
        //1 admin and 2 student
        //if (ViewState["usertype"].ToString() == "2")
        if (userType.Equals("1") || userType.Equals("2"))
        {

            //ADDED BY PRAFULL

            //try
            //{
            //    FileUpload fuStudPhoto = fuFile as FileUpload;
            //    // FileUpload fuStudSign = hdnFile as FileUpload;
            //    HttpPostedFile filephoto = fuStudPhoto.PostedFile;
            //    //HttpPostedFile fileSign = fuStudSign.PostedFile;
            //    byte[] photo = null;
            //    byte[] sign = null;
            //    int count = 0;
            //    //if ((fuStudPhoto.HasFile && fuStudSign.HasFile) || Convert.ToInt32(ViewState["PhotoCount"]) == 2)
            //    //{

            //    //string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = " + Session["stuinfoidno"] + "");
            //    string IdNo = Session["stuinfoidno"].ToString();
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("ID");
            //    dt.Columns.Add("NAME");
            //    dt.Columns.Add("TYPE");

            //    if (fuStudPhoto.HasFile)
            //    {
            //        int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_spic", fuStudPhoto);
            //        if (retval == 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
            //            return;
            //        }
            //        dt.Rows.Add(IdNo, IdNo + "_spic" + Path.GetExtension(fuStudPhoto.FileName), 1);
            //    }
            //    //if (fuStudSign.HasFile)
            //    //{
            //    //    int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_sign", fuStudSign);
            //    //    if (retval == 0)
            //    //    {
            //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
            //    //        return;
            //    //    }
            //    //    dt.Rows.Add(IdNo, IdNo + "_sign" + Path.GetExtension(fuStudSign.FileName), 2);
            //    //}

            //    if (filephoto.ContentLength <= 51200 && fileSign.ContentLength <= 51200)
            //    {
            //        string SP_Name = "PKG_STUD_BULK_PHOTO_UPLOAD";
            //        string SP_Parameters = "@P_TBL, @P_OPERATION";
            //        string Call_Values = "0,3";
            //        count = Convert.ToInt32(AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dt, true, 2));
            //    }
            //    else
            //    {
            //        lblmessageShow.ForeColor = System.Drawing.Color.Red;
            //        lblmessageShow.Text = "Photo Size must be less than 50 kb";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //    }

            //    if (count == 1 || count == 2)
            //    {
            //        uploadDocument();
            //        if (Session["usertype"] == "2")
            //        {
            //            Response.Redirect("StudentInfo.aspx#step-5");
            //        }
            //        else
            //        {
            //            Response.Redirect("StudentInfo.aspx#step-5");
            //        }
            //    }
            //    else
            //    {
            //        lblmessageShow.ForeColor = System.Drawing.Color.Red;
            //        lblmessageShow.Text = "Failed to upload ! Please try again !";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    objCommon.DisplayMessage(this, "Something went wrong ..Please try again !!", this);
            //    return;
            //}

            int idno = 0;
            idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);   //Added by sachin on 27-07-2022

            string IdNo = Session["idno"].ToString();
            int Userno = Convert.ToInt32(Session["userno"].ToString());
            //string idno = Session["idno"].ToString();
            string studentname = Session["userfullname"].ToString();
            //string IdNo = Session["stuinfoidno"].ToString();

            //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
            {

                string CertificateNo = (lvitem.FindControl("txtDocNo") as TextBox).Text;
                string district = (lvitem.FindControl("txtDistrict") as TextBox).Text;
                string Issuedate = (lvitem.FindControl("txtIssueDate") as TextBox).Text;
                string Authority = (lvitem.FindControl("ddlAuthority") as DropDownList).SelectedValue;

                string mandatory = (lvitem.FindControl("txtMandatory") as TextBox).Text;
                string status = (lvitem.FindControl("status") as LinkButton).Text;
                FileUpload fuStudPhoto = lvitem.FindControl("fuFile") as FileUpload;
                HiddenField hidstudocno = lvitem.FindControl("HiddenField1") as HiddenField;
                int no = int.Parse(hidstudocno.Value.TrimStart());
                Button btndocno = lvitem.FindControl("btnSubmit") as Button;
                int docno = int.Parse(btndocno.CommandArgument);
                string Docno = btndocno.ToolTip;
                string FUToll = fuStudPhoto.ToolTip;

                string fileNameFromDB = string.Empty,
                    fileTypeFromDB = string.Empty,
                    fileContentTypeFromDB = string.Empty,
                    filePathFromDB = string.Empty;

                // Convert.ToInt32(Session["stuinfoidno"]) + " " + Convert.ToInt32(Session["idno"])
                DataSet dsFileName = objCommon.FillDropDown("ACD_ADM_DOCUMENT_LIST", "DOCUMENT_NAME", "DOCTYPE,IMAGES,DOC_PATH", "IDNO=" + idno + " AND DOCUMENTNO=" + hidstudocno.Value, string.Empty);
                //if (dsFileName.Tables[0].Rows.Count > 0)
                //{
                //    fileNameFromDB = dsFileName.Tables[0].Rows[0]["DOCUMENT_NAME"].ToString();
                //    fileTypeFromDB = dsFileName.Tables[0].Rows[0]["DOCTYPE"].ToString();
                //    fileContentTypeFromDB = dsFileName.Tables[0].Rows[0]["IMAGES"].ToString();
                //    filePathFromDB = dsFileName.Tables[0].Rows[0]["DOC_PATH"].ToString();
                //}

                if (dsFileName.Tables[0].Rows.Count > 0)
                {
                    fileNameFromDB = dsFileName.Tables[0].Rows[0]["DOCUMENT_NAME"].ToString();
                    fileTypeFromDB = dsFileName.Tables[0].Rows[0]["DOCTYPE"].ToString();
                    fileContentTypeFromDB = dsFileName.Tables[0].Rows[0]["IMAGES"].ToString();
                    filePathFromDB = dsFileName.Tables[0].Rows[0]["DOC_PATH"].ToString();

                    Issuedate = (lvitem.FindControl("txtIssueDate") as TextBox).Text;
                    //string date = Issuedate == string.Empty ? "" : Convert.ToDateTime(Issuedate).ToString("yyyy-MM-dd");
                    string date = Issuedate == string.Empty ? "" : Convert.ToString(Issuedate);

                    commonSaveUpdate(idno, Convert.ToInt32(hidstudocno.Value), fileTypeFromDB, fileContentTypeFromDB, fileNameFromDB, filePathFromDB, CertificateNo, district, date, Authority, 2, Userno);

                }

                if (mandatory == "Y" && status == "Pending")
                {
                    objCommon.DisplayMessage("Please Upload Mandatory Documents!", this.Page);
                    return;
                }
            }

            BindDocument();
            ////uploadDocument();
         

            int studid = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);

            //string userType = string.IsNullOrEmpty(ViewState["usertype"].ToString()) ? "" : ViewState["usertype"].ToString();  

            if (userType.Equals("1"))
            {
                // studid = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["stuinfoidno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["stuinfoidno"]) + "'"));
                studid = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);

            }
            else if (userType.Equals("2"))
            {

                // studid = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'"));
                // studid = Convert.ToInt32(Session["idno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["idno"]);
                studid = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);


                // studid = Session["idno"].ToString();
            }

            //// studid = Session["stuinfoidno"].ToString();
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + studid));
            int id_type = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDTYPE", "IDNO=" + studid));
            int natinalityno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "NATIONALITYNO", "IDNO=" + studid));
            string categoryno = objCommon.LookUp("ACD_STUDENT", "CATEGORYNO", "IDNO=" + studid);
            int studDocCount = Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST A INNER JOIN ACD_DOCUMENT_LIST B ON (A.DOCUMENTNO=B.DOCUMENTNO)", "COUNT(IDNO)", "IDNO=" + studid + "AND B.DEGREENO=" + degreeno + "AND B.ID_TYPE=" + id_type +
                "AND (NATIONALITYNO='" + natinalityno + "'OR NATIONALITYNO LIKE'" + natinalityno + ",%'" +
                "OR NATIONALITYNO LIKE '%," + natinalityno + "' OR NATIONALITYNO LIKE '%," + natinalityno + ",%')" +
                "AND (CATEGORYNO='" + categoryno + "'OR CATEGORYNO LIKE'" + categoryno + ",%'" +
                "OR CATEGORYNO LIKE '%," + categoryno + "' OR CATEGORYNO LIKE '%," + categoryno + ",%')" + "AND ACTIVE_STATUS=1" + "AND MANDATORY=1"));

            int DocMasterCount = Convert.ToInt32(objCommon.LookUp("ACD_DOCUMENT_LIST", "COUNT(DOCUMENTNO)", "DEGREENO=" + degreeno + "AND ID_TYPE=" + id_type +
                "AND (NATIONALITYNO='" + natinalityno + "'OR NATIONALITYNO LIKE'" + natinalityno + ",%'" +
                "OR NATIONALITYNO LIKE '%," + natinalityno + "' OR NATIONALITYNO LIKE '%," + natinalityno + ",%')" +
                "AND (CATEGORYNO='" + categoryno + "'OR CATEGORYNO LIKE'" + categoryno + ",%'" +
                "OR CATEGORYNO LIKE '%," + categoryno + "' OR CATEGORYNO LIKE '%," + categoryno + ",%')" + "AND ACTIVE_STATUS=1" + "AND ISNULL(MANDATORY,0)=1"));


            //if (studDocCount != DocMasterCount)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Upload all Mandatory Documents.", this.Page);
            //    return;
            //}
            //else
            //{

            CustomStatus cs = (CustomStatus)objstud.InsertUpdateStudentDocSubmitLog(Convert.ToInt32(studid));

            //}
        }
        else
        {

        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Document Details Saved Successfully!!'); location.href='QualificationDetails.aspx';", true);

        Response.Redirect("~/academic/QualificationDetails.aspx");
    }

    protected void btnFetch_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    if (Session["token"] != null)
        //    {
        //        //string Error = Request.QueryString["error"];
        //        //string Code = Request.QueryString["code"];
        //        //string State = Request.QueryString["state"];
        //        string Error = ViewState["ResponseError"].ToString();
        //        string Code = ViewState["ResponseCode"].ToString();
        //        string State = ViewState["ResponseState"].ToString();


        //        //IsTokenExpires(Code, State, Error);

        //        //Session["flagD"] = null;
        //        //lvIssuedDoc.DataSource = issueDoc;
        //        //lvIssuedDoc.DataBind();
        //        //pnlModal.Visible = true;
        //        mp1.Show();
        //    }
        //    //else
        //    //{
        //    //    ImageButton btnFetch = (sender as ImageButton);
        //    //    string docname = btnFetch.CommandName;
        //    //    string response_type = "code";
        //    //    string state = Session["userno"].ToString() + "," + docname;

        //    //    string userAuthenticationURI = "https://api.digitallocker.gov.in/public/oauth2/1/authorize?response_type=" + response_type + "&client_id=" + ClientId + "&redirect_uri=" + RedirectUrl + "&state=" + state;

        //    //    if (!string.IsNullOrEmpty(userAuthenticationURI))
        //    //    {
        //    //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        //    //        ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
        //    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userAuthenticationURI);
        //    //        request.Method = "GET";
        //    //        request.ContentType = "application/json"; //new MediaTypeHeaderValue("text/html");
        //    //        WebResponse response = request.GetResponse();
        //    //        string responseUri = response.ResponseUri.ToString();
        //    //        //Response.Redirect(responseUri);
        //    //        //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(responseUri)));
        //    //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + responseUri + "','_newtab');", true);
        //    //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "opendialog('" + responseUri + "')", true);
        //    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenWindow", "window.open('" + responseUri + "','_newtab');", true);
        //    //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open('YourPage.aspx', '_blank');", true);

        //    //    }
        //    //}

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}

    }

    //protected void IsTokenExpires(string Code, string State, string Error)
    //{

    //    //if (Error != null)
    //    //{
    //    //}
    //    if (Code != null)
    //    {
    //        HttpContext context = HttpContext.Current;
    //        DataSet issueDoc = Session["IssuedDocList"] as DataSet;
    //        DataSet selfDoc = Session["SelfUploadedDocList"] as DataSet;

    //        if (issueDoc == null)
    //        {
    //            if (selfDoc == null)
    //            {
    //                AfterSuccessfullAuthorization();
    //            }
    //            else
    //            {
    //                //mp1.Show();
    //                if (lvUploadedDoc.Items.Count == 0)
    //                {
    //                    Session["flagD"] = null;
    //                    lvUploadedDoc.DataSource = selfDoc;
    //                    lvUploadedDoc.DataBind();
    //                    pnlModal.Visible = true;
    //                    mp1.Show();
    //                }
    //                else
    //                {
    //                    if (Session["flag"] == "Downloaded")
    //                    {
    //                        Session["flag"] = null;
    //                    }
    //                    else
    //                    {
    //                        Session["flag"] = Session["flag"];
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (lvIssuedDoc.Items.Count == 0)
    //            {
    //                Session["flagD"] = null;
    //                lvIssuedDoc.DataSource = issueDoc;
    //                lvIssuedDoc.DataBind();
    //                pnlModal.Visible = true;
    //                mp1.Show();
    //            }
    //            else
    //            {
    //                if (Session["flag"] == "Downloaded")
    //                {
    //                    Session["flag"] = null;
    //                }
    //                else
    //                {
    //                    Session["flag"] = Session["flag"];
    //                }
    //            }
    //        }
    //    }
    //    else if (Code == null)
    //    {

    //        lvIssuedDoc.DataSource = null;
    //        lvIssuedDoc.DataBind();
    //        Session["flagD"] = null;
    //        lvUploadedDoc.DataSource = null;
    //        lvUploadedDoc.DataBind();
    //        // objCommon.DisplayMessage(this.Page, "DigiLocker Tocker is Expired. Please Login again.", this.Page);
    //    }
    //}

    protected void AfterSuccessfullAuthorization()
    {
        string Error = Request.QueryString["error"];
        //authorization code after successful authorization   
        string Code = string.Empty;
        string State = string.Empty;
        if (ViewState["ResponseCode"].ToString() == string.Empty || ViewState["ResponseCode"].ToString() == null)
        {
            Code = Request.QueryString["code"];
            State = Request.QueryString["state"];
        }
        else
        {
            Code = ViewState["ResponseCode"].ToString();
            State = ViewState["ResponseState"].ToString();
        }


        if (Error != null)
        {
        }
        //else if (Code != null)
        //{
        //    PostbackToDigiLockerServer(Code, State);

        //    HttpContext context = HttpContext.Current;
        //    DataSet issueDoc = Session["IssuedDocList"] as DataSet;
        //    DataSet selfDoc = Session["SelfUploadedDocList"] as DataSet;

        //    if (issueDoc != null)
        //    {
        //        lvIssuedDoc.DataSource = issueDoc;
        //        lvIssuedDoc.DataBind();
        //        pnlModal.Visible = true;
        //        mp1.Show();
        //    }
        //    if (selfDoc != null)
        //    {
        //        lvUploadedDoc.DataSource = selfDoc;
        //        lvUploadedDoc.DataBind();
        //        pnlModal.Visible = true;
        //        mp1.Show();
        //    }
        //}
        else if (Code == null)
        {

            lvIssuedDoc.DataSource = null;
            lvIssuedDoc.DataBind();

            lvUploadedDoc.DataSource = null;
            lvUploadedDoc.DataBind();
        }

    }

    //protected void PostbackToDigiLockerServer(string Code, string State)
    //{
    //    try
    //    {

    //        //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
    //        string gettoken = string.Empty;
    //        //string AccessToken = ExchangeAuthorizationCode(Convert.ToInt32(Session["userno"]), Code, out gettoken);
    //        ExchangeAuthorizationCode(Convert.ToInt32(Session["userno"]), Code, State);
    //        //saving refresh token in database    
    //        //SaveRefreshToken(Id, RefreshToken);
    //        //Get Email Id of the authorized user    
    //        //string EmailId = FetchEmailId(AccessToken);
    //        ////Saving Email Id    
    //        //SaveEmailId(UserId, EmailId);
    //        //Redirect the user to Authorize.aspx with user id    
    //        //string Url = "Authorize.aspx?UserId=" + UserId;
    //        //Response.Redirect(Url, true);
    //        //string ref_token = "";
    //        //string refreshToken = RefreshAccessToken(AccessToken, out ref_token);
    //        ////GetUserDetails(refreshToken);
    //        ////GetListOfSelfUploadedDocs(refreshToken);
    //        //splitString = State.Split(',');
    //        //GetListOfIssuedDocs(refreshToken, splitString);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //    //GetListOfSelfUploadedDocs(refreshToken);
    //}

    //protected void ExchangeAuthorizationCode(int userId, string code, string state)
    //{
    //    string accessToken = "";
    //    string[] splitString;
    //    var Content = "code=" + code + "&client_id=" + ClientId + "&client_secret=" + ClientSecret + "&redirect_uri=" + RedirectUrl + "&grant_type=authorization_code";

    //    var request = WebRequest.Create("https://api.digitallocker.gov.in/public/oauth2/1/token");
    //    request.Method = "POST";
    //    byte[] byteArray = Encoding.UTF8.GetBytes(Content);
    //    request.ContentType = "application/x-www-form-urlencoded";
    //    request.ContentLength = byteArray.Length;
    //    using (Stream dataStream = request.GetRequestStream())
    //    {
    //        dataStream.Write(byteArray, 0, byteArray.Length);
    //        dataStream.Close();
    //    }
    //    var Response = (HttpWebResponse)request.GetResponse();
    //    Stream responseDataStream = Response.GetResponseStream();
    //    StreamReader reader = new StreamReader(responseDataStream);
    //    string ResponseData = reader.ReadToEnd();
    //    reader.Close();
    //    responseDataStream.Close();
    //    Response.Close();
    //    if (Response.StatusCode == HttpStatusCode.OK)
    //    {
    //        var ReturnedToken = JsonConvert.DeserializeObject<Token>(ResponseData);
    //        if (ReturnedToken.refresh_token != null)
    //        {
    //            accessToken = ReturnedToken.access_token;
    //            //return ReturnedToken.refresh_token;

    //            Session["token"] = accessToken;

    //            //string ref_token = "";
    //            //string refreshToken = RefreshAccessToken(AccessToken, out ref_token);
    //            //GetUserDetails(refreshToken);
    //            //GetListOfSelfUploadedDocs(refreshToken);
    //            splitString = state.Split(',');
    //            GetListOfIssuedDocs(accessToken, splitString);
    //        }
    //        else
    //        {

    //        }
    //    }
    //    else
    //    {

    //    }
    //}

    //private void GetListOfIssuedDocs(string accToken, string[] stateString)
    //{
    //    try
    //    {

    //        if (accToken == null || accToken == "" || accToken == string.Empty)
    //        {
    //            //string ref_token = "";
    //            //string refreshToken = RefreshAccessToken(AccessToken, out ref_token);
    //        }
    //        else
    //        {

    //            string fileUri = "";
    //            var DocListReq = "https://api.digitallocker.gov.in/public/oauth2/2/files/issued";
    //            // Create a request for the URL.    
    //            var Request = WebRequest.Create(DocListReq);
    //            Request.Headers.Add("Authorization", "Bearer " + accToken);

    //            var Response = (HttpWebResponse)Request.GetResponse();
    //            // Get the stream containing content returned by the server.    
    //            var DataStream = Response.GetResponseStream();
    //            // Open the stream using a StreamReader for easy access.    
    //            var Reader = new StreamReader(DataStream);
    //            // Read the content.    
    //            var JsonString = Reader.ReadToEnd();
    //            // Cleanup the streams and the response.    
    //            Reader.Close();
    //            DataStream.Close();
    //            Response.Close();

    //            if (Response.StatusCode == HttpStatusCode.OK)
    //            {
    //                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(JsonString);
    //                DataSet ds = new DataSet();
    //                DataTable dtable = new DataTable();
    //                DataRow dr;
    //                dtable.Columns.Add("Name");
    //                dtable.Columns.Add("URI");
    //                dtable.Columns.Add("token");
    //                for (int i = 0; i < myDeserializedClass.items.Count; i++)
    //                {
    //                    dr = dtable.NewRow();
    //                    dr[0] = myDeserializedClass.items[i].name.ToString();
    //                    dr[1] = myDeserializedClass.items[i].uri.ToString();
    //                    dr[2] = accToken;
    //                    dtable.Rows.Add(dr);

    //                    //if (stateString[1].ToString().Any(myDeserializedClass.items[i].name.Contains))
    //                    //{
    //                    //    fileUri = myDeserializedClass.items[i].uri;
    //                    //    break;
    //                    //}
    //                }
    //                //DataTable dt = new DataTable();
    //                //DataSet ds = new DataSet();
    //                //da.Fill(dt);
    //                //con.Close();

    //                ds.Tables.Add(dtable);

    //                HttpContext context = HttpContext.Current;
    //                Session["IssuedDocList"] = ds;

    //                // IssuedDoctoDownload(accToken, fileUri);
    //                //DownloadCurrent(accToken, fileUri);
    //            }
    //            else
    //            {

    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //try
        //{


        //    Button lnkDwn = (sender as Button);
        //    string uri = lnkDwn.CommandName;
        //    string actoken = lnkDwn.CommandArgument;

        //    //IssuedDoctoDownload(actoken, uri);
        //    //if (Session["flag"] == null)
        //    //{
        //    //    if (Session["flag"] != uri)
        //    //    {
        //    //        //IssuedDoctoDownload(actoken, uri);
        //    //        Session["flag"] = uri;

        //    //    }
        //    //    else
        //    //    {
        //    //        Session["flag"] = "Downloaded";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    Session["flag"] = null;
        //    //    //objCommon.DisplayMessage(this.Page, "Document Downloaded Successfully. Please Check Download folder of your System.", this.Page);
        //    //}
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    //private void IssuedDoctoDownload(string accToken, string uri)
    //{
    //    long total = 0;
    //    long received = 0;
    //    try
    //    {
    //        int bytesToRead = 10000;

    //        // Buffer to read bytes in chunk size specified above
    //        byte[] buffer = new Byte[bytesToRead];
    //        //var Content = "uri=" + uri ;
    //        var DocListReq = "https://api.digitallocker.gov.in/public/oauth2/1/file/" + uri;
    //        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(DocListReq);
    //        webRequest.Method = "GET";
    //        webRequest.Headers.Add("Authorization", "Bearer " + accToken);
    //        //webRequest.Timeout = 3000;

    //        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();


    //        WebClient myWebClient = new WebClient();
    //        var resp = HttpContext.Current.Response;

    //        int length = Convert.ToInt32(webResponse.Headers.Get("Content-Length"));
    //        string mimetype = webResponse.Headers.Get("Content-Type");
    //        string ext = GetDefaultExtension(mimetype);
    //        string cpString = webResponse.Headers.Get("Content-Disposition");
    //        ContentDisposition contentDisposition = new ContentDisposition(cpString);
    //        string filename = contentDisposition.FileName + ".tmp";// ext;
    //        string filenameToDownload = contentDisposition.FileName + ext;
    //        string LogFileName = contentDisposition.FileName;

    //        // HttpContext context = HttpContext.Current;

    //        // string path = context.Request.Url.OriginalString.Contains("iitms").ToString();

    //        // string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    //        // string download = Path.Combine(user, "Downloads");
    //        // string fname = download + "\\" + filename;
    //        //// string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
    //        // string ContentType = string.Empty;
    //        // //string filepath = Server.MapPath("~//UPLOAD_FILES//STUDENT_DOCUMENT/");

    //        // string file = download + "\\" + filename;

    //        // // Create New instance of FileInfo class to get the properties of the file being downloaded
    //        // FileInfo myfile = new FileInfo(download + "\\" + filename);

    //        // // Checking if file exists
    //        // if (myfile.Exists)
    //        // {


    //        //     Response.Clear();
    //        //     Response.ClearHeaders();

    //        //     Response.ContentType = "application/octet-stream";
    //        //     Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
    //        //     Response.TransmitFile(file);
    //        //     Response.Flush();
    //        //     Response.End();
    //        // }
    //        // else
    //        // {
    //        //     objCommon.DisplayMessage(this.Page, "Document Not Uploaded by Student.", this.Page);
    //        // }


    //        //Added by Deepali to get Download folder path

    //        //String path = String.Empty;
    //        //RegistryKey rKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main");
    //        //if (rKey != null)
    //        //    path = (String)rKey.GetValue("Default Download Directory");
    //        //if (String.IsNullOrEmpty(path))
    //        //    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
    //        //string fname = path + "\\" + filename;


    //        string download = Path.GetTempPath();//Path.Combine(user, "Downloads");

    //        string fname = download + filename;


    //        if (ViewState["usertype"].ToString() == "2")
    //        {
    //            int idno = Convert.ToInt32(Session["idno"]);
    //            int ua_no = Convert.ToInt32(Session["userno"]);
    //            string ip_address = Request.ServerVariables["REMOTE_ADDR"];

    //            CustomStatus cs = (CustomStatus)objstud.InsertIntoDigiLockerLog(idno, ua_no, ip_address, LogFileName);
    //        }

    //        ////End
    //        // //Below Code commented by deepali
    //        ////string fname = "C:\\Users\\Welcome\\Downloads\\" + filename;  //old code by sunita

    //        //FileStream fileStream = File.OpenWrite(fname);
    //        FileStream fileStream = File.Create(fname);

    //        using (Stream input = webResponse.GetResponseStream())
    //        {
    //            total = length;

    //            int size = input.Read(buffer, 0, buffer.Length);
    //            while (size > 0)
    //            {
    //                fileStream.Write(buffer, 0, size);
    //                received += size;

    //                size = input.Read(buffer, 0, buffer.Length);
    //                // Session["flag"] = "C";
    //            }
    //        }
    //        fileStream.Flush();
    //        fileStream.Close();
    //        Response.ContentType = "application/pdf";
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filenameToDownload);

    //        // Write the file to the Response  
    //        const int bufferLength = 10000;
    //        byte[] buffer1 = new Byte[bufferLength];
    //        int length1 = 0;
    //        Stream download1 = null;
    //        try
    //        {
    //            download1 = new FileStream(fname, FileMode.Open, FileAccess.Read);//new FileStream(Server.MapPath("~/UPLOAD_FILES/Event/document.pdf"),FileMode.Open,FileAccess.Read);

    //            do
    //            {
    //                if (Response.IsClientConnected)
    //                {
    //                    length1 = download1.Read(buffer1, 0, bufferLength);
    //                    Response.OutputStream.Write(buffer1, 0, length1);
    //                    buffer1 = new Byte[bufferLength];
    //                }
    //                else
    //                {
    //                    length1 = -1;
    //                }
    //            }
    //            while (length1 > 0);
    //            Response.Flush();
    //            Response.End();
    //        }
    //        finally
    //        {
    //            if (download1 != null)
    //                download1.Close();
    //        }
    //        Response.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}

    public static string GetDefaultExtension(string mimeType)
    {
        string result;
        RegistryKey key;
        object value;

        key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
        value = key != null ? key.GetValue("Extension", null) : null;
        result = value != null ? value.ToString() : string.Empty;

        return result;
    }
    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }


    //added by prafull 23112021

    private void BindDocument()
    {
        DataSet dslist = null;
        int idno = 0;
        idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);
        dslist = objstud.GetDocumentList(idno);

        //dslist = objstud.GetDocumentList(Convert.ToInt32(Session["stuinfoidno"]));
        //dslist = objstud.GetDocumentList(Convert.ToInt32(Session["IDNO"]));                //Added by sachin on 16-07-2022

        if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
        {
            lvBinddata.Visible = true;
            lvBinddata.DataSource = dslist;
            lvBinddata.DataBind();
            
           
            //btnuploadDocuments.Enabled = true;

            // loop on list view 
            int FinalSubmit = 0;       //Added by sachin on 28-07-2022
            //FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
            if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
            {
                FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
            }
            DataSet dsallowprocess = objstud.GetAllowProcess(Convert.ToInt32(Session["idno"]), 3, 'E');
            int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
            if (FinalSubmit == 1 && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
            {
                foreach (ListViewDataItem lvitem in lvBinddata.Items)
                {
                    Button btndocno = lvitem.FindControl("btnSubmit") as Button;
                    btndocno.Enabled = true;
                }
            }
            else if (FinalSubmit == 1)
            {
                foreach (ListViewDataItem lvitem in lvBinddata.Items)
                {
                    Button btndocno = lvitem.FindControl("btnSubmit") as Button;
                    btndocno.Enabled = false;
                }
            }
        }
        else
        {
            lvBinddata.Visible = false;
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();
            //btnuploadDocuments.Enabled = false;

        }



    }
    //added by prafull for Upload images in Blob 06122021

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext.ToLower();
        try
        {
           // DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

    //added by prafull 23112021

    protected void uploadDocument()
    {
        try
        {

            int idno = 0;
            idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);   //Added by sachin on 27-07-2022

            string IdNo = Session["idno"].ToString();
            int Userno = Convert.ToInt32(Session["userno"].ToString());
            //string idno = Session["idno"].ToString();
            string studentname = Session["userfullname"].ToString();
           // string IdNo = Session["stuinfoidno"].ToString();

            int selectedDocumentIndex = Convert.ToInt32(Session["SelectedDocumentIndex"]);   // Added By Shrikant W. on 27-09-2023

           // string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
            {
                if (lvitem.DisplayIndex == selectedDocumentIndex)                            // Added By Shrikant W. on 28-09-2023
                {

                    string CertificateNo = (lvitem.FindControl("txtDocNo") as TextBox).Text;
                    string district = (lvitem.FindControl("txtDistrict") as TextBox).Text;
                    string Issuedate = (lvitem.FindControl("txtIssueDate") as TextBox).Text;
                    string Authority = (lvitem.FindControl("ddlAuthority") as DropDownList).SelectedValue;


                    FileUpload fuStudPhoto = lvitem.FindControl("fuFile") as FileUpload;
                    HiddenField hidstudocno = lvitem.FindControl("HiddenField1") as HiddenField;
                    int no = int.Parse(hidstudocno.Value.TrimStart());
                    Button btndocno = lvitem.FindControl("btnSubmit") as Button;
                    int docno = int.Parse(btndocno.CommandArgument);
                    string Docno = btndocno.ToolTip;
                    string FUToll = fuStudPhoto.ToolTip;

                    string fileNameFromDB = string.Empty,
                        fileTypeFromDB = string.Empty,
                        fileContentTypeFromDB = string.Empty,
                        filePathFromDB = string.Empty;

                    // Convert.ToInt32(Session["stuinfoidno"]) + " " + Convert.ToInt32(Session["idno"])
                    DataSet dsFileName = objCommon.FillDropDown("ACD_ADM_DOCUMENT_LIST", "DOCUMENT_NAME", "DOCTYPE,IMAGES,DOC_PATH", "IDNO=" + idno + " AND DOCUMENTNO=" + hidstudocno.Value, string.Empty);
                    //if (dsFileName.Tables[0].Rows.Count > 0)
                    //{
                    //    fileNameFromDB = dsFileName.Tables[0].Rows[0]["DOCUMENT_NAME"].ToString();
                    //    fileTypeFromDB = dsFileName.Tables[0].Rows[0]["DOCTYPE"].ToString();
                    //    fileContentTypeFromDB = dsFileName.Tables[0].Rows[0]["IMAGES"].ToString();
                    //    filePathFromDB = dsFileName.Tables[0].Rows[0]["DOC_PATH"].ToString();
                    //}

                    //bool Flag = false;
                    if (fuStudPhoto.HasFile)
                    {

                        //Flag = true;
                        HttpPostedFile FileSize = fuStudPhoto.PostedFile;
                        string Fileext = System.IO.Path.GetExtension(fuStudPhoto.FileName);
                        if (Fileext.ToLower() == ".pdf" || Fileext.ToLower() == ".jpg" || Fileext.ToLower() == ".jpeg")
                        {
                            if (FileSize.ContentLength <= 500000)
                            {
                                string contentType = contentType = fuStudPhoto.PostedFile.ContentType;
                                //if (!Directory.Exists(folderPath))
                                //{
                                //    Directory.CreateDirectory(folderPath);
                                //}

                                string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                                HttpPostedFile file = fuStudPhoto.PostedFile;
                                string fileDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                string filename = idno + "_doc_" + docno + "_" + fileDateTime + ext;   //Path.GetFileName(fuStudPhoto.PostedFile.FileName);


                                if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                {
                                    //int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_doc_" + hidstudocno.Value + "", fuStudPhoto);

                                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + docno + "_" + fileDateTime + "", fuStudPhoto);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }


                                    //CustomStatus cs = (CustomStatus)objstud.AddUpdateStudentDocumentsDetail(Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, "Blob Storage");
                                    Issuedate = (lvitem.FindControl("txtIssueDate") as TextBox).Text;
                                    //string date = Issuedate == string.Empty ? "" : Convert.ToDateTime(Issuedate).ToString("yyyy-MM-dd");
                                    string date = Issuedate == string.Empty ? "" : Convert.ToString(Issuedate);


                                    commonSaveUpdate(idno, Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, "Blob Storage", CertificateNo, district, date, Authority, 1, Userno);
                                    //   return;
                                    //CustomStatus cs = (CustomStatus)objstud.AddUpdateStudentDocumentsDetailNew(idno, Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, "Blob Storage", CertificateNo, district, date, Authority);
                                    ////fuStudPhoto.PostedFile.SaveAs(folderPath + filename);
                                    //if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
                                    //{
                                    //    objCommon.DisplayMessage(this, "Upload Documents Sucessfully.... !", this);
                                    //    BindDocument();
                                    //}
                                    //else
                                    //{
                                    //    objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                    //}

                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Please Upload file Below or Equal to 512 Kb only !", this);
                                    //lblmessageShow.ForeColor = System.Drawing.Color.Red;
                                    //lblmessageShow.Text = "Please Upload file Below or Equal to 40 Kb only !";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                                    return;

                                }
                            }

                        }

                    }

                    //else if (true)
                    //{
                    //    objCommon.DisplayMessage(this, "Please Select Atleast One File for Upload !!!", this);
                    //    return;
                    //}

                    else if (dsFileName.Tables[0].Rows.Count > 0)
                    {
                        fileNameFromDB = dsFileName.Tables[0].Rows[0]["DOCUMENT_NAME"].ToString();
                        fileTypeFromDB = dsFileName.Tables[0].Rows[0]["DOCTYPE"].ToString();
                        fileContentTypeFromDB = dsFileName.Tables[0].Rows[0]["IMAGES"].ToString();
                        filePathFromDB = dsFileName.Tables[0].Rows[0]["DOC_PATH"].ToString();

                        Issuedate = (lvitem.FindControl("txtIssueDate") as TextBox).Text;
                        //string date = Issuedate == string.Empty ? "" : Convert.ToDateTime(Issuedate).ToString("yyyy-MM-dd");
                        string date = Issuedate == string.Empty ? "" : Convert.ToString(Issuedate);

                        commonSaveUpdate(idno, Convert.ToInt32(hidstudocno.Value), fileTypeFromDB, fileContentTypeFromDB, fileNameFromDB, filePathFromDB, CertificateNo, district, date, Authority, 1, Userno);

                    }
                }
            }


            BindDocument();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //throw;
        }

    }

    private void commonSaveUpdate(int idno, int hiddtudocno, string extension, string contentType, string filename, string path, string certificateno, string district, string issuedate, string Authority, int COMMAND_TYPE,int userno)
    {

        try
        {
            //   return;
             CustomStatus cs = (CustomStatus)objstud.AddUpdateStudentDocumentsDetailNew(idno, hiddtudocno, extension, contentType, filename, path, certificateno, district, issuedate, Authority, COMMAND_TYPE,userno);
            //fuStudPhoto.PostedFile.SaveAs(folderPath + filename);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                objCommon.DisplayMessage(this, "Document Uploaded Successfully.... !", this);
                ////BindDocument();
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }


    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        Button btnSubmit = sender as Button;
        ListViewDataItem lvdi = btnSubmit.NamingContainer as ListViewDataItem;
        FileUpload fuFile = lvdi.FindControl("fuFile") as FileUpload;
        if (!fuFile.HasFile)
        {
            objCommon.DisplayMessage(this.Page, "Please Select File To Upload!", this.Page);
            return;
        }
        Session["SelectedDocumentIndex"] = lvdi.DisplayIndex;       // Added By Shrikant W. on 27-09-2023
        uploadDocument();
    }

    protected void lvBinddata_ItemDataBound(object sender, ListViewItemEventArgs e)
    {


        // ListViewDataItem dataItem = (ListViewDataItem)e.Item;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //Label status = e.Item.FindControl("status") as Label;

        }
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();

        string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
        string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
        string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
        CloudBlobClient client = acc.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        CloudBlob blob = container.GetBlobReference(filename);
        MemoryStream ms = new MemoryStream();
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        blob.DownloadToStream(ms);
        Response.ContentType = blob.Properties.ContentType;
        Response.AddHeader("Content-Disposition", "Attachment; filename=" + filename.ToString());
        Response.AddHeader("Content-Length", blob.Properties.Length.ToString());
        Response.BinaryWrite(ms.ToArray());
    }


    // Commented
    //var blobFileNames = new string[] { filename };
    //using (var zipOutputStream = new ZipOutputStream(Response.OutputStream))
    //{

    //    foreach (var blobFileName in blobFileNames)
    //    {
    //        zipOutputStream.SetLevel(0);
    //        var blob = container.GetBlobReference(blobFileName);
    //        var entry = new ZipEntry(blobFileName);
    //        zipOutputStream.PutNextEntry(entry);

    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //        blob.DownloadToStream(zipOutputStream);

    //    }
    //    zipOutputStream.Finish();
    //    zipOutputStream.Close();
    //}
    //Response.BufferOutput = false;
    //Response.AddHeader("Content-Disposition", "attachment; filename=" + "zipFileName.zip");
    //Response.ContentType = "application/octet-stream";
    //Response.Flush();
    //Response.End();
    protected void lvBinddata_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                var status = e.Item.FindControl("status") as LinkButton;
                Button upload = e.Item.FindControl("btnSubmit") as Button;

                if (status.Text == "Pending")
                {
                    status.Text = "Pending";
                    status.Enabled = false;
                    status.ForeColor = System.Drawing.Color.Red;
                    status.Font.Bold = true;
                    
                }
                else
                {
                    status.Text = "Uploaded";
                    //status.Enabled = true;
                    status.Enabled = false;
                    status.ForeColor = System.Drawing.Color.Green;
                    status.Font.Bold = true;

                    
                }

                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;


                DropDownList ddlAuthority = e.Item.FindControl("ddlAuthority") as DropDownList;
                DataSet ds = objCommon.FillDropDown("ACD_AUTHORITY", "AUTHORITYNO", "AUTHORITYNAME", "AUTHORITYNO > 0 AND ISNULL(ACTIVESTATUS,0) = 1", "AUTHORITYNO");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                    while (dtr.Read())
                    {
                        ddlAuthority.Items.Add(new ListItem(dtr["AUTHORITYNAME"].ToString(), dtr["AUTHORITYNO"].ToString()));

                    }
                }

                ddlAuthority.SelectedValue = dr["AUTHORITY"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void imgbtnPrevDoc_Click1(object sender, ImageClickEventArgs e)
    {
        HttpContext context = HttpContext.Current;

        string path = context.Request.Url.OriginalString.Contains("iitms").ToString();

        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;
        studid = Session["stuinfoidno"].ToString();
        string documentname = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENT_NAME", "DOCUMENTNO='" + filename + "' AND IDNO=" + studid);
        string filepath = Server.MapPath("~//UPLOAD_FILES//STUDENT_DOCUMENT/");

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
            objCommon.DisplayMessage(this.Page, "Document Not Uploaded by Student.", this.Page);
        }
    }

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        ////Added By Prafull
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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

                // objCommon.DisplayMessage(this, "Image not Found...", this);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";
                BindDocument();

            }
            else
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                BindDocument();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "UploadDocument.aspx-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}








