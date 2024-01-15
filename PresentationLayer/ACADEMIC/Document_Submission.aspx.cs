//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DOCUMENT_SUBMISSION 
// CREATION DATE : 10-APRIL-2019
// CREATED BY    : RITA MUNDE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
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
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Text;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;

public partial class ACADEMIC_Document_Submission : System.Web.UI.Page
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
    public string docname = string.Empty;
    public string enrollno = string.Empty;
    public string fname = string.Empty;
    public int i = 0;
    public string btndocname = string.Empty;

    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();


    private string DirPath1 = string.Empty;

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
                    BindDocument();
                    this.CheckPageAuthorization();
                    txtIDNo.Visible = false;
                    ShowStudentDetails();

                    if (Session["usertype"].ToString() == "2")
                    {
                        pnlSearch.Visible = false;
                        bindStudentInfo();
                    }
                    else
                    {
                        pnlSearch.Visible = true;
                    }

                }
              
            }
         
            else
            {
                if (Session["flag"] == "Downloaded")
                {
                    Session["flag"] = null;
                }
                else
                {
                    Session["flag"] = Session["flag"];
                }
            }
            string Error = Request.QueryString["error"];
            //authorization code after successful authorization   

            string Code = Request.QueryString["code"];
            string State = Request.QueryString["state"];
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_DOCUMENT_SUBMISSION.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Button lnkDwn = (sender as Button);
            string uri = lnkDwn.CommandName;
            string actoken = lnkDwn.CommandArgument;

            
            if (Session["flag"] == null)
            {
                if (Session["flag"] != uri)
                {
                   
                    Session["flag"] = uri;
                }
                else
                {
                    Session["flag"] = "Downloaded";
                }
            }
            else
            {
                Session["flag"] = null;
                objCommon.DisplayMessage(this.Page, "Document Downloaded Successfully. Please Check Download folder of your System.", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
    }

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
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                //Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            // Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindStudentInfo();
    }

    private void bindStudentInfo()
    {
        try
        {
            string searchBy = string.Empty;
            //string searchText = txtTempIdno.Text.Trim();
            string searchText = string.Empty;
            string errorMsg = string.Empty;
            string idno1 = string.Empty;
            //string idno1 = Convert.ToString((txtTempIdno.Text)).ToString();
            //studid = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + idno1 + "' OR ROLLNO='" + idno1 + "'");

            //added on 10-04-2020 by Vaishali
            if (Session["usertype"].ToString() != "2")
            {
                searchText = txtTempIdno.Text.Trim();
                idno1 = Convert.ToString((txtTempIdno.Text)).ToString();
                studid = objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + idno1 + "' OR ROLLNO='" + idno1 + "'");
                Session["studid"] = studid;
                if (rdoEnrollmentNo.Checked)
                {
                    searchBy = "enrollno";
                    errorMsg = "having Enrollment no. : " + txtTempIdno.Text;
                }
                else if (rdoRollNo.Checked)
                {
                    searchBy = "rollno";
                    errorMsg = "having Rollno no. : " + txtTempIdno.Text;
                }
            }
            else
            {
                searchText = Session["idno"].ToString();
                studid = Session["idno"].ToString();
                searchBy = "idno";
                errorMsg = string.Empty;
            }

            ShowStudents(searchBy, searchText, errorMsg);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowStudents(string searchBy, string searchText, string errorMsg)
    {
        //bind the student search in text box......
        DataSet ds = objstud.SearchStudents(searchText, searchBy);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Show Student Details..
                lblDegree.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                lblName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblName.ToolTip = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblAdmbatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                //lblBranch.Text = ds.Tables[0].Rows[0]["CODE"].ToString() + " / " + ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblBranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                lblSemester.Text = ds.Tables[0].Rows[0]["SEM_NAME"].ToString();
                lblDegree.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                
                divDetails.Visible = true;
                //BindLV();
            }
        }
        else
        {
            //lvDocumentList.DataSource = null;
            //lvDocumentList.DataBind();
            divDetails.Visible = false;
            btnCancel.Visible = false;
            objCommon.DisplayMessage(this.Page, "No Student Found " + errorMsg, this.Page);
        }
    }

  

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void btnExtract_Click(object sender, EventArgs e)
    {

    }

 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtTempIdno.Text = string.Empty;
        //lvDocumentList.DataSource = null;
        //lvDocumentList.DataBind();
        lblSemester.Text = string.Empty;
        lblYear.Text = string.Empty;
        lblRegno.Text = string.Empty;
        lblDegree.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblAdmbatch.Text = string.Empty;
        lblName.Text = string.Empty;
        divDetails.Visible = false;
        btnCancel.Visible = false;
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

    
    public DataSet GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataSet ds = null;
        try
        {
            //DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
            ds = JObject.Parse(JSONData)["items"].ToObject<DataSet>();

        }
        catch (Exception ex)
        {
        }
        return ds;
    }
    private DataTable ToDataTable<T>(T entity) where T : class
    {
        var properties = typeof(T).GetProperties();
        var table = new DataTable();

        foreach (var property in properties)
        {
            table.Columns.Add(property.Name, property.PropertyType);
        }

        table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
        return table;
    }
    private static void PlayResponeAsync(IAsyncResult asyncResult)
    {
        long total = 0;
        long received = 0;
        HttpWebRequest webRequest = (HttpWebRequest)asyncResult.AsyncState;

        try
        {
            using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asyncResult))
            {

                int length = Convert.ToInt32(webResponse.Headers.Get("Content-Length"));
                string mimetype = webResponse.Headers.Get("Content-Type");
                string ext = GetDefaultExtension(mimetype);
                string cpString = webResponse.Headers.Get("Content-Disposition");
                ContentDisposition contentDisposition = new ContentDisposition(cpString);
                string filename = contentDisposition.FileName + ext;
                string fname = "C:\\Users\\Admin\\AppData\\Local\\Temp\\" + filename;
                FileStream fileStream = File.OpenWrite(fname);
                byte[] buffer = new byte[100000];

                using (Stream input = webResponse.GetResponseStream())
                {
                    total = length;

                    int size = input.Read(buffer, 0, buffer.Length);
                    while (size > 0)
                    {
                        fileStream.Write(buffer, 0, size);
                        received += size;

                        size = input.Read(buffer, 0, buffer.Length);
                    }
                }
                fileStream.Flush();
                fileStream.Close();
        
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void downloadFile(string filepath, string fname)
    {
        try
        {
            WebClient wb = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            byte[] data = wb.DownloadData(filepath);
            response.BinaryWrite(data);
            response.End();
        }
        catch (Exception ex)
        {
        }


    }
    static string calcHmac(string data, string secretkey)
    {
        //byte[] key = Encoding.ASCII.GetBytes(secretkey);
        //HMACSHA256 myhmacsha256 = new HMACSHA256(key);
        //byte[] byteArray = Encoding.ASCII.GetBytes(data);
        //MemoryStream stream = new MemoryStream(byteArray);
        //string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        //Console.WriteLine(result);
        //return result;

        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secretkey);
        byte[] messageBytes = encoding.GetBytes(data);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }

    }

    protected void btnCloseModal_Click(object sender, EventArgs e)
    {
        mp1.Hide();
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
    protected void btnPhotoUpload_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            string ext = System.IO.Path.GetExtension(fuPhotoUpload.PostedFile.FileName);
            byte[] image = null;
            byte[] imageaftercompress = null;
            string IdNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]));


            if (fuPhotoUpload.HasFile)
            {
                if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG" || ext.ToUpper().Trim() == ".GIF")
                {
                    if (fuPhotoUpload.PostedFile.ContentLength < 150000)
                    {

                        using (Stream fs = fuPhotoUpload.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                image = br.ReadBytes((Int32)fs.Length);
                                imageaftercompress = ImageCompression.CompressImage(image, 150);
                            }
                        }

                        // Check compressed image size
                        if (imageaftercompress != null && imageaftercompress.LongLength >= 150000)
                        {
                            objCommon.DisplayMessage(this, "File size must be less or equal to 150kb", this.Page);
                            return;
                        }
                        objstud.StudPhoto = imageaftercompress;
                        objstud.IdNo = Convert.ToInt32(IdNo);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "File size must be less or equal to 150kb", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Only JPG,JPEG,PNG files are allowed!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please select file!", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objEc.UpdateStudPhoto(objstud);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Photo uploaded Successfully!!", this.Page);
                ViewState["StudPhoto"] = 1;
                ShowStudentDetails();

            }
            else
            {
                ViewState["StudPhoto"] = 0;
                objCommon.DisplayMessage("Error!!", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void showstudentphoto(string IdNo)
    {

       // string IdNo = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
        if (IdNo != "")
        {
            string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(Session["studid"]));

            if (imgphoto == string.Empty)
            {
              //  imgPhoto.ImageUrl = "~/images/nophoto.jpg";
               imgPhoto.ImageUrl =Page.ResolveClientUrl("~/images/nophoto.jpg");
            }
            else
            {
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(IdNo).ToString() + "&type=STUDENT";
            }

        }
        else
        {
            imgPhoto.ImageUrl = null;

        }
    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        string IdNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]));

        dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["IdNo"]));
        if (dtr != null)
        {
            if (dtr.Read())
            {

                if (IdNo != "")
                {
                    string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(IdNo));
                    string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(IdNo));
                    if (imgphoto == string.Empty)
                    {
                        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
                        ViewState["StudPhoto"] = 0;
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IdNo"].ToString() + "&type=STUDENT";
                        ViewState["StudPhoto"] = 1;

                    }
                    if (signphoto == string.Empty)
                    {
                        ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
                        ViewState["StudSign"] = 0;
                    }
                    else
                    {
                        ImgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IdNo"].ToString() + "&type=STUDENTSIGN";
                        ViewState["StudSign"] = 1;
                    }
                    // 
                }
                else
                {
                    imgPhoto.ImageUrl = null;
                    ImgSign.ImageUrl = null;
                }

            }
        }
    }

    private void showstudentsignature(string IdNo)
    {
       // string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
        if (IdNo != "")
        {
            string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(Session["studid"]));

            if (signphoto == string.Empty)
            {

                ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
            }
            else
            {
                ImgSign.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(IdNo).ToString() + "&type=STUDENTSIGN";
            }
        }
        else
        {
            ImgSign.ImageUrl = null;
        }
    }

    protected void btnSignUpload_Click(object sender, EventArgs e)
    {
        try
        {
           
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            string ext = System.IO.Path.GetExtension(this.fuSignUpload.PostedFile.FileName);

            string IdNo = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]));

            if (fuSignUpload.HasFile)
            {
                if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG")
                {
                   

                    //if (fuSignUpload.PostedFile.ContentLength < 25600)
                    if (fuSignUpload.PostedFile.ContentLength < 150000)
                    {

                        byte[] resizephoto = ResizePhoto(fuSignUpload);

                        //if (resizephoto.LongLength >= 25600)
                        if (resizephoto.LongLength >= 150000)
                        {
                            objCommon.DisplayMessage(this, "File size must be less or equal to 150kb", this.Page);
                            return;
                        }
                        else
                        {
                            objstud.StudSign = this.ResizePhoto(fuSignUpload);
                           
                            objstud.IdNo = Convert.ToInt32(IdNo);

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "File size must be less or equal to 150kb", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Only JPG,JPEG,PNG files are allowed!", this.Page);
                    return;
                }

            }
            else
            {
               
                objCommon.DisplayMessage(this, "Please select file!", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objEc.UpdateStudSign(objstud);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Signature uploaded Successfully!!", this.Page);
                ViewState["StudSign"] = 1;
                ShowStudentDetails();
            }
            else
            {
                ViewState["StudSign"] = 0;
                objCommon.DisplayMessage("Error!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

  
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
        Session["SelectedDocumentIndex"] = lvdi.DisplayIndex; 
        uploadDocument();
    }



    protected void uploadDocument()
    {
        try
        {

            int idno = 0;
            idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);   

            string IdNo = Session["idno"].ToString();
            int Userno = Convert.ToInt32(Session["userno"].ToString());
            //string idno = Session["idno"].ToString();
            string studentname = Session["userfullname"].ToString();
            // string IdNo = Session["stuinfoidno"].ToString();
            int selectedDocumentIndex = Convert.ToInt32(Session["SelectedDocumentIndex"]);
            // string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
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
                    if (Fileext == ".pdf" || Fileext == ".jpg" || Fileext == ".jpeg")
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

    private void commonSaveUpdate(int idno, int hiddtudocno, string extension, string contentType, string filename, string path, string certificateno, string district, string issuedate, string Authority, int COMMAND_TYPE, int userno)
    {
       try
        {
            //   return;
            CustomStatus cs = (CustomStatus)objstud.AddUpdateStudentDocumentsDetailNew(idno, hiddtudocno, extension, contentType, filename, path, certificateno, district, issuedate, Authority, COMMAND_TYPE, userno);
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


    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
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
            DeleteIFExits(FileName);
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

}

