using System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

public partial class ACADEMIC_POSTADMISSION_ADMPTestDetails_Score_Assign_Verify : System.Web.UI.Page
{

    Common objCommon = new Common();
    ADMPTestDetails_Score_Assign_VerifyController ObjTestVerify = new ADMPTestDetails_Score_Assign_VerifyController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameADMP"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int isverify = Convert.ToInt32(Request.QueryString["isverify"]);

            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                Page.Title = Session["coll_name"].ToString();
                ViewState["action"] = "add";
                PopulateDropDownList();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDAdmissionDemo.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDAdmissionDemo.PopulateDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlTestName, "ACD_ADMP_TESTSCORE_MASTER", "SCOREID", "TESTNAME", "ACTIVE_STATUS=1 AND SCOREID>0 AND DEGREE_TYPE LIKE '%" + ddlProgramType.SelectedValue + "%'", "SCOREID");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDAdmissionDemo.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND ACTIVESTATUS=1", "UA_SECTION");
        ddlTestName.SelectedIndex = 0;
    }

    public void BindListView()
    {
        try
        {
            int ScoreId = Convert.ToInt32(ddlTestName.SelectedValue);
            int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int Payment_status = Convert.ToInt32(ddlpayment.SelectedValue);
            DataSet ds = ObjTestVerify.GetTestScoreDataList(ScoreId, BatchNo, ProgramType, 0, ua_no, Payment_status);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["TestScoreTbl"] = ds.Tables[0];
                DataTable dt = ViewState["TestScoreTbl"] as DataTable;

                lvTestScore.Visible = true;
                pnlTestScore.Visible = true;
                lvTestScore.DataSource = ds;
                lvTestScore.DataBind();
                if (Session["usertype"].ToString() == "1")
                {
                    btnLock.Visible = true;
                }
            }
            else
            {
                pnlTestScore.Visible = false;
                lvTestScore.DataSource = null;
                lvTestScore.DataBind();
                objCommon.DisplayMessage(this.Page, "Record not found.", Page);
                return;
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDAdmissionDemo.BindListView() --> " + ex.Message + " " + ex.StackTrace);
        }

    }

    private DataTable CreateTable_TestScore()
    {
        DataTable dtTestScore = new DataTable();
        dtTestScore.Columns.Add("USERNO", typeof(int));
        dtTestScore.Columns.Add("SCOREID", typeof(int));
        dtTestScore.Columns.Add("SCORE_OBTAINED", typeof(string));
        dtTestScore.Columns.Add("INDIA_RANK", typeof(string));
        dtTestScore.Columns.Add("ISVERIFY", typeof(int));
        dtTestScore.Columns.Add(new DataColumn("BLOB_CERTIFICATE_NAME", typeof(string)));
        dtTestScore.Columns.Add(new DataColumn("ORIGINAL_CERTIFICATE_NAME", typeof(string)));
        dtTestScore.Columns.Add(new DataColumn("CATEGORYNO", typeof(int)));
        dtTestScore.Columns.Add("IS_LOCK", typeof(int));
        return dtTestScore;
    }

    private void clear()
    {
        txtAllIndiaRank.Text = string.Empty;
        txtObtainedMarks.Text = string.Empty;
        ddlcategory.SelectedIndex = 0;

    }

    private void clearmodal()
    {
        txtAllIndiaRank.Text = string.Empty;
        txtObtainedMarks.Text = string.Empty;
        ddlcategory.SelectedIndex = 0;
        lblCandidateName.Text = string.Empty;
        lblregno.Text = string.Empty;
        lblVerifyBy.Text = string.Empty;
        pnlPopup.Visible = false;

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
        int pagesize = Convert.ToInt32(DataPager1.PageSize);
        NumberDropDown.SelectedValue = pagesize.ToString();
        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
        
     
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgramType.SelectedIndex = 0;
        ddlTestName.SelectedIndex = 0;
        lvTestScore.Visible = false;
        btnLock.Visible = false;
        lvTestScore.DataSource = null;
        lvTestScore.DataBind();
        DataPager1.Visible = false;
        pnlTestScore.Visible = false;
        ddlpayment.SelectedIndex = 0;
    }

    protected void btnPrevDoc_Click(object sender, System.EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string img = lbldocument.Text;
            PreviewAndDownload(img);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QualificationDetails.imgbtnPrevDoc_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DataTable dt = CreateTable_TestScore();
        DataRow dr = dt.NewRow();

        string Certificate = string.Empty;
        string filePath = string.Empty, fileName = string.Empty;
        string FILE = futestdoc.FileName;


        if (!futestdoc.HasFile && lbldocument.Text != string.Empty)
        {

            Certificate = lbldocument.Text;
        }

        if (futestdoc.HasFile)
        {
            if (!futestdoc.PostedFile.ContentLength.Equals(string.Empty) || futestdoc.PostedFile.ContentLength != null)
            {
                int fileSize = futestdoc.PostedFile.ContentLength;
                int KB = fileSize / 1024;
                if (KB >= 500)
                {
                    objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);
                    return;
                }

                string ext = System.IO.Path.GetExtension(futestdoc.FileName).ToLower();
                if (ext == ".pdf")
                {

                }
                else
                {
                    objCommon.DisplayMessage("Please Upload only Pdf", this.Page);
                    return;
                }
            }

            filePath = hdfuserno.Value + "_" + Convert.ToInt32(ddlTestName.SelectedValue) + "_" + Convert.ToString(ddlTestName.SelectedItem.Text) + "_ADMP_TESTSCORE_Certificate_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            hdforiginalname.Value = futestdoc.FileName;
            fileName = uploadDocsCommon(futestdoc, filePath);

            if (fileName == string.Empty)
            {
                return;
            }
            else
            {
                Certificate = fileName;
            }

        }
        dr["USERNO"] = hdfuserno.Value;
        dr["SCOREID"] = ddlTestName.SelectedValue;
        dr["SCORE_OBTAINED"] = txtObtainedMarks.Text.Trim();
        dr["INDIA_RANK"] = txtAllIndiaRank.Text.Trim();
        dr["BLOB_CERTIFICATE_NAME"] = Certificate;
        dr["ORIGINAL_CERTIFICATE_NAME"] = hdforiginalname.Value;
        dr["CATEGORYNO"] = Convert.ToInt32(ddlcategory.SelectedValue);
        dr["ISVERIFY"] = 1;
        dt.Rows.Add(dr);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        string TestScore = ds.GetXml();
        int status = Convert.ToInt32(ObjTestVerify.SaveTestScore(Convert.ToInt32(Session["userno"].ToString()), TestScore, 0));
        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Record saved successfully.", Page);
            bindmarkentry(Convert.ToInt32(hdfuserno.Value));
            BindListView();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
            return;
        }

    }

    protected void btnclear_Click(object sender, System.EventArgs e)
    {
        clear();
     
    }

    protected void btnLock_Click(object sender, System.EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = CreateTable_TestScore();
            int rowIndex = 0;
            int countlock = 0;
            foreach (var row in lvTestScore.Items)
            {
                DataRow dRow = dt.NewRow();
                
                    HiddenField hfdScoreId = row.FindControl("hfdScoreId") as HiddenField;
                    HiddenField hfdUserNo = row.FindControl("hfdUserNo") as HiddenField;
                    CheckBox chkIsVerify = row.FindControl("chkIsVerify") as CheckBox;
                    Label lbllock = row.FindControl("lbllock") as Label;

                     if (chkIsVerify.Checked == true && lbllock.Text != "Lock")
                    {
                        countlock++;
                        if (chkIsVerify.Checked == true && lbllock.Text == "Verified")
                        {
                            dRow["USERNO"] = hfdUserNo.Value;
                            dRow["SCOREID"] = hfdScoreId.Value;
                            dRow["IS_LOCK"] = 1;
                            dt.Rows.Add(dRow);
                            rowIndex = rowIndex + 1;
                        }
                    }
                
            }
            ds.Tables.Add(dt);
            string TestScore = ds.GetXml();
            int Check = 1;
            if (dt.Rows.Count == 0 && countlock > 0)
            {
                objCommon.DisplayMessage(this.Page, "Please select a verified record to lock.", Page);
                return;
            }
            if (dt.Rows.Count == 0 && countlock == 0)  
            {
                objCommon.DisplayMessage(this.Page, "Please select atleast one record to lock.", Page);
                return;
            }

            int status = Convert.ToInt32(ObjTestVerify.SaveTestScore(Convert.ToInt32(Session["userno"].ToString()), TestScore, Check));

            if (status == 1)
            {
                objCommon.DisplayMessage(this.Page, "Record locked successfully.", Page);
                BindListView();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error", Page);
                return;
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDAdmissionDemo.Submit_TestScore-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnmarkentry_Click(object sender, System.EventArgs e)
    {
        clearmodal();
        int userno = Convert.ToInt32(((System.Web.UI.WebControls.Button)(sender)).ToolTip.ToString());
        Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "OpenPreview();", true);
        bindmarkentry(userno);
        

    }

    public void bindmarkentry(int userno)
    {
        int ScoreId = Convert.ToInt32(ddlTestName.SelectedValue);
        int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
        pnlPopup.Visible = false;
        int ua_no = Convert.ToInt32(Session["userno"].ToString());
        int Payment_status = Convert.ToInt32(ddlpayment.SelectedValue);
        DataSet ds = ObjTestVerify.GetTestScoreDataList(ScoreId, BatchNo, ProgramType, userno, ua_no, Payment_status);

        clear();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtMaxScore.Text = ds.Tables[0].Rows[0]["MAXSCORE"].ToString();
            txtObtainedMarks.Text = ds.Tables[0].Rows[0]["SCORE_OBTAINED"].ToString();
            txtAllIndiaRank.Text = ds.Tables[0].Rows[0]["INDIA_RANK"].ToString();
            objCommon.FillDropDownList(ddlcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "ALLOW_JEE = 1", "CATEGORYNO");
            ddlcategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
            lbldocument.Text = ds.Tables[0].Rows[0]["BLOB_CERTIFICATE_NAME"].ToString();
            hdforiginalname.Value = ds.Tables[0].Rows[0]["ORIGINAL_CERTIFICATE_NAME"].ToString();
            lblCandidateName.Text = ds.Tables[0].Rows[0]["CANDIDATE_NAME"].ToString();
            lblregno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblVerifyBy.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
            lbltest.Text = ddlTestName.SelectedItem.Text;
            lbldob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd-MM-yyyy");
            hdfuserno.Value = userno.ToString();
            if (ds.Tables[0].Rows[0]["IS_LOCK"].ToString() == "Lock")
            {
                txtObtainedMarks.Enabled = false;
                txtAllIndiaRank.Enabled = false;
                ddlcategory.Enabled = false;
                futestdoc.Enabled = false;
                btnUpload.Enabled = false;
                btnclear.Enabled = false;
            }
            else
            {
                txtObtainedMarks.Enabled = true;
                txtAllIndiaRank.Enabled = true;
                ddlcategory.Enabled = true;
                futestdoc.Enabled = true;
                btnUpload.Enabled = true;
                btnclear.Enabled = true;
            }
            if (lbldocument.Text != "")
            {
                string img = lbldocument.Text;
             
                PreviewAndDownload(img);
            }

        }
        else
        {
            txtObtainedMarks.Enabled = true;
            txtAllIndiaRank.Enabled = true;
            ddlcategory.Enabled = true;
            futestdoc.Enabled = true;
            btnUpload.Enabled = true;
            lblCandidateName.Text = string.Empty;
            lblregno.Text = string.Empty;
            lblVerifyBy.Text = string.Empty;
            lbltest.Text = string.Empty;
        }
    }

    private void PreviewAndDownload(string img)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameADMP"].ToString();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
           
            var ImageName = img;
            if (img == null || img == "")
            {

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
                string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
                iframeView.Src = urlpath + ImageName;
                pnlPopup.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.PreviewAndDownload-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvTestScore_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindListView();
    }

    protected void NumberDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
        DataPager1.PageSize = Convert.ToInt32(NumberDropDown.SelectedValue);
    }

    protected void ddlpayment_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindListView();
        int pagesize = Convert.ToInt32(DataPager1.PageSize);
        NumberDropDown.SelectedValue = pagesize.ToString();
        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
    }

    #region BlogStorage

    protected string uploadDocsCommon(FileUpload ctrlFileUpload, string fileNamePath)
    {
        try
        {

            byte[] imgData;
            string fileName = "";
            if (ctrlFileUpload.HasFile)
            {
                if (!ctrlFileUpload.PostedFile.ContentLength.Equals(string.Empty) || ctrlFileUpload.PostedFile.ContentLength != null)
                {
                    int fileSize = ctrlFileUpload.PostedFile.ContentLength;

                    int KB = fileSize / 1024;
                    if (KB >= 500)
                    {
                        objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);

                        return null;
                    }
                }
                imgData = objCommon.GetImageData(ctrlFileUpload);
                fileName = imgData.ToString();
                string filename_Certificate = Path.GetFileName(ctrlFileUpload.PostedFile.FileName);

                string Ext = Path.GetExtension(ctrlFileUpload.FileName);

                int retval = Blob_UploadDepositSlip(fileNamePath, ctrlFileUpload, imgData);

                if (retval == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);

                    return "";
                }

                fileName = fileNamePath + Ext;
            }
            else
            {
                fileName = null;
            }

            return fileName;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something went wrong." + ex.Message, this.Page);
            return "";
        }
    }

    public int Blob_UploadDepositSlip(string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            
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

    public CloudBlockBlob DownloadFile(string FileName, string directoryPath)
    {
        CloudBlockBlob Newblob;
        try
        {
            string Url = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
           
            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);

            string img = FileName;
            string extension = Path.GetExtension(img.ToString());
            var ImageName = img;
            if (img != null || img != "")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (extension == ".pdf")
                {

                    Newblob = blobContainer.GetBlockBlobReference(ImageName);
                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                }
                else
                {
                   
                    Newblob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }



                }
                return Newblob;
            }
            return null;
        }

        catch (Exception ex)
        {
            return null;
        }

    }

    #endregion BlogStorage

    
}

