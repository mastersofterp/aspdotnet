using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_DAIICTPostAdmission_ADMPGenerateOfferLetter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPGenerateOfferLetterController objOffer = new ADMPGenerateOfferLetterController();
    
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameADMP"].ToString();

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

        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PopulateDropdown();

            }
            txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

    }


    #region User Defined Methods

    protected void PopulateDropdown()
    {
        objCommon.FillDropDownList(ddlAcadyear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
        ddlAcadyear.Items.RemoveAt(0);
        objCommon.FillDropDownList(ddlDegree, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        objCommon.FillDropDownList(ddlEntrance, "ACD_ADMP_TESTSCORE_MASTER", "SCOREID", "TESTNAME", "IsNull(ACTIVE_STATUS,0)=1", "SCOREID ASC");
        ddlEntrance.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlLetter, "ACD_ADMP_LETTER_TEMPLATE", "LETTER_TEMPLATE_ID", "LETTER_TEMPLATE_NAME", "", "LETTER_TEMPLATE_ID DESC");
        objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "IsNull(activestatus,0)=1 AND isnull(allow_jee, 0) = 1", "CATEGORYNO ASC");
        objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "ISNULL(ACTIVESTATUS,0)=1", "PAYTYPENO ASC");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GenerateOfferLetter.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GenerateOfferLetter.aspx");
        }
    }

    private string GenerateOfferLetter(string htmlContent, string applicationid, string userno)
    {

        string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
        string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameADMP"].ToString();

        CloudStorageAccount account = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient blobClient = account.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference(blob_ContainerName);


        using (MemoryStream outputStream = new MemoryStream())
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, outputStream);
            document.Open();
            using (TextReader reader = new StringReader(htmlContent))
            {
                HTMLWorker worker = new HTMLWorker(document);
                worker.Parse(reader);
            }
            document.Close();
            var byteArray = outputStream.ToArray();

            var FileName = "ADMP_OFFER_LETTER_" + userno + "_" + applicationid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(byteArray))
                {
                    cblob.UploadFromStream(stream);
                }
            }

            return FileName;

        }




    }

    private DataTable CreateTable_OfferLetter()
    {

        DataTable dtOfferLetter = new DataTable();

        dtOfferLetter.Columns.Add(new DataColumn("USERNO", typeof(int)));
        dtOfferLetter.Columns.Add(new DataColumn("APPLICATION_NO", typeof(string)));
        dtOfferLetter.Columns.Add(new DataColumn("OFFER_LETTER_FILE_NAME", typeof(string)));
        dtOfferLetter.Columns.Add(new DataColumn("OFFER_FROM_DATE", typeof(DateTime)));
        dtOfferLetter.Columns.Add(new DataColumn("OFFER_END_DATE", typeof(DateTime)));
        dtOfferLetter.Columns.Add(new DataColumn("ROUNDNO", typeof(int)));
        dtOfferLetter.Columns.Add(new DataColumn("GENERATE_OFFTER_LETTER_ID", typeof(int)));
        dtOfferLetter.Columns.Add(new DataColumn("OFFER_LETTER_TEMPLATE", typeof(string)));
        dtOfferLetter.Columns.Add(new DataColumn("PAYTYPENO", typeof(int)));
        return dtOfferLetter;

    }

    private DataTable CreateTable_OfferLock()
    {
        DataTable dtOfferLock = new DataTable();

        dtOfferLock.Columns.Add(new DataColumn("USERNO", typeof(int)));
        dtOfferLock.Columns.Add(new DataColumn("ISLOCKED", typeof(int)));
        dtOfferLock.Columns.Add(new DataColumn("GENERATE_OFFTER_LETTER_ID", typeof(int)));
        return dtOfferLock;
        
    }

    protected void BindMeritStudentList(int AcadYear, int DegreeNo, int Entrance, int Branch, int Category,int Round, int Offertype)
    {
        DataSet ds = objOffer.GetOfferLetterList(AcadYear, DegreeNo, Entrance, Branch, Category, Round, Offertype);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            
            lvOffeerLetter.DataSource = ds.Tables[0];
            lvOffeerLetter.DataBind();
            lvOffeerLetter.Visible = true;
            if (rdobtnoffer.SelectedValue == "1")
            {
                btnSubmit.Visible = false;
                btnSendEmail.Visible = false;
                btnOfferletter.Visible = true;
            }
            else if (rdobtnoffer.SelectedValue == "2")
            {
                btnSubmit.Visible = true;
                btnOfferletter.Visible = false;
                btnSendEmail.Visible = false;
               
            }
            else if (rdobtnoffer.SelectedValue == "3")
            {
                btnSubmit.Visible = false;
                btnOfferletter.Visible = false;
                btnSendEmail.Visible = true;
            }
        }
        else
        {
            objCommon.DisplayMessage("Record not found", this.Page);
            lvOffeerLetter.DataSource = null;
            lvOffeerLetter.DataBind();
            lvOffeerLetter.Visible = false;
        }
    }

    private void Clear()
    {
        ddlAcadyear.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlLetter.SelectedIndex = 0;
        ddlEntrance.SelectedIndex = 0;
        ddlRound.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        lvOffeerLetter.Visible = false;
        ddlPaymentType.SelectedIndex = 0;
        btnSendEmail.Visible = false;
        btnSubmit.Visible = false;
        btnOfferletter.Visible = false;
    }
    #endregion

    #region button

    protected void lvOffeerLetter_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        CheckBox chkAllot = dataitem.FindControl("chkAllot") as CheckBox;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnOfferletter_Click(object sender, EventArgs e)
    {
      
        string studentIds = string.Empty;
        string subjectText = string.Empty;
        string templateText = string.Empty;
        DataTable dt = CreateTable_OfferLetter();
        int Degree = Convert.ToInt32(ddlDegree.SelectedValue);
        int Batch = Convert.ToInt32(ddlAcadyear.SelectedValue);
        DateTime FromDate =Convert.ToDateTime(txtFromDate.Text);
        DateTime ToDate = Convert.ToDateTime(txtToDate.Text);
        int Branch = Convert.ToInt32(ddlBranch.SelectedValue);
        int RoundNo = Convert.ToInt32(ddlRound.SelectedValue);
        int LetterTemplateId = Convert.ToInt32(ddlLetter.SelectedValue);
        int Test = Convert.ToInt32(ddlEntrance.SelectedValue);
        int Category = Convert.ToInt32(ddlCategory.SelectedValue);
        int LetterTypeId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_LETTER_TEMPLATE", "LETTER_TYPE_ID", 
                          "LETTER_TEMPLATE_ID=" + LetterTemplateId));
       int offertype = Convert.ToInt32(rdobtnoffer.SelectedValue);
       int PaymentType = Convert.ToInt32(ddlPaymentType.SelectedValue);

        foreach (ListViewDataItem lvItem in lvOffeerLetter.Items)
        {
            CheckBox chkBox = lvItem.FindControl("ChkOffer") as CheckBox;
            HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
            HiddenField hdnOfferid = lvItem.FindControl("hdnOfferid") as HiddenField;

            if (chkBox.Checked == true && chkBox.Enabled == true)
            {
               // studentIds += "'"+chkBox.ToolTip +"'"+ "$";
               // studentIds += chkBox.ToolTip + "$";
                studentIds = chkBox.ToolTip;
                DataRow dRow = dt.NewRow();
                Session["ApplicationIDs"] = studentIds;
                Session["LetterId"] = ddlLetter.SelectedValue;

                DataSet ds_mstQry = null;
                int  UserNo = Convert.ToInt32(hdnUserNo.Value);
                ds_mstQry = objOffer.GetOfferLetterTemplate(UserNo, studentIds, LetterTemplateId, Degree, Branch, Batch, FromDate, ToDate, LetterTypeId);
                if (ds_mstQry != null)
                {
                    templateText = ds_mstQry.Tables[0].Rows[0]["LETTER_TEMPLATE_HTML_STRING"].ToString();
                }
                string FileName = GenerateOfferLetter(templateText, studentIds, hdnUserNo.Value);

               dRow["USERNO"] = UserNo;
               dRow["APPLICATION_NO"] = studentIds;
               dRow["OFFER_LETTER_FILE_NAME"] = FileName;
               dRow["OFFER_FROM_DATE"] = FromDate;
               dRow["OFFER_END_DATE"] = ToDate;
               dRow["ROUNDNO"] = RoundNo;
               dRow["GENERATE_OFFTER_LETTER_ID"] = Convert.ToInt32(hdnOfferid.Value);
               dRow["OFFER_LETTER_TEMPLATE"] = templateText;
               dRow["PAYTYPENO"] = PaymentType;
               dt.Rows.Add(dRow);
            }
        }

        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        string _OfferLetter = ds.GetXml();

        if (studentIds.Length <= 0)
        {
            objCommon.DisplayMessage("Please Select Atleast One Student.", this.Page);
            return;
        }

        int status = Convert.ToInt32(objOffer.SaveOfferLetter(Convert.ToInt32(Session["userno"].ToString()), _OfferLetter));
        if (status == 1)
        {

            objCommon.DisplayMessage(this.Page, "Offer Letter Generated.", Page);
            BindMeritStudentList(Batch, Degree, Test, Branch, Category, RoundNo, offertype);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
        }
        

    }

    protected void btnpreview_Click(object sender, EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string FileName = ((System.Web.UI.WebControls.Button)(sender)).ToolTip.ToString();
            string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);
            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }

            CloudBlockBlob Newblob = DownloadFile(FileName, directoryPath);
            string extension = Path.GetExtension(FileName.ToString());
            var ImageName = FileName;

            if (Newblob == null)
            {
                return;
            }

            if (FileName != null || FileName != "")
            {
                if (extension == ".pdf")
                {

                    string filePath = directoryPath + "\\" + ImageName;

                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
                    iframeView.Src = urlpath + ImageName;
                    mpeViewDocument.Show();
                    pnlOfferLetter.Visible = true;
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QualificationDetails.imgbtnPrevDoc_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            int Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            int Batchno = Convert.ToInt32(ddlAcadyear.SelectedValue);
            int Test = Convert.ToInt32(ddlEntrance.SelectedValue);
            int Category = Convert.ToInt32(ddlCategory.SelectedValue);
            int Round = Convert.ToInt32(ddlRound.SelectedValue);
            int offertype = Convert.ToInt32(rdobtnoffer.SelectedValue);
            BindMeritStudentList(Batchno, Degreeno, Test, Branch, Category, Round, offertype);
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_GenerateOfferLetter.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlListType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvOffeerLetter.DataSource = null;
        lvOffeerLetter.DataBind();
        lvOffeerLetter.Visible = false;
       
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {


        string studentIds = string.Empty;
        //Get Selected Students..
        ReportDocument customReport = new ReportDocument();

        foreach (ListViewDataItem lvItem in lvOffeerLetter.Items)
        {
            CheckBox chkBox = lvItem.FindControl("chkAllot") as CheckBox;
            if (chkBox.Checked == true)
            {

                studentIds = chkBox.ToolTip;
                string reportPath = Server.MapPath(@"~,Reports,Academic,OfferLetterBulk.rpt".Replace(",", "\\"));
                customReport.Load(reportPath);
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;
                crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
                crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                CrTables = customReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
                int Report_Type = 0;
                if (ddlListType.SelectedValue == "1")
                {
                    Report_Type = 1;
                }
                else if (ddlListType.SelectedValue == "2")
                {
                    Report_Type = 2;
                }
                else
                {
                    Report_Type = 3;
                }
                string clgname = Session["coll_name"].ToString();
                customReport.SetParameterValue("@P_COLLEGE_CODE", 9);
                customReport.SetParameterValue("@P_USERNO", studentIds);
                customReport.SetParameterValue("@P_REPORT_TYPE", Report_Type);
                customReport.SetParameterValue("@P_PRINT_DATE", Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy"));


                string username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Convert.ToInt32(studentIds));
                string userfullname = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME + '_'+ LASTNAME AS NAME", "USERNO=" + Convert.ToInt32(studentIds));


                string directoryPath = "E:\\OfferLetterMail\\" + ddlListType.SelectedItem.Text + "\\";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                customReport.ExportToDisk(ExportFormatType.PortableDocFormat, directoryPath + username + "_" + userfullname.Replace(" ", "") + ".pdf");


            }
        }
        objCommon.DisplayMessage("Offer Letter Download Successfully", this.Page);
        if (studentIds.Length <= 0)
        {
            objCommon.DisplayMessage("Please Select Student", this.Page);
            return;
        }


    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        int count = 0;
        string Application_no = string.Empty;
        int RoundNo = Convert.ToInt32(ddlRound.SelectedValue);
        foreach (ListViewDataItem item in lvOffeerLetter.Items)
        {
            CheckBox chkBox = item.FindControl("ChkOffer") as CheckBox;
            HiddenField hdnUserNo = item.FindControl("hdnUserNo") as HiddenField;
            HiddenField hdnOfferid = item.FindControl("hdnOfferid") as HiddenField;
            Label lblemail = item.FindControl("lblemail") as Label;
            if (chkBox.Checked == true)
            {
                Application_no = chkBox.ToolTip;
                int UserNo = Convert.ToInt32(hdnUserNo.Value);
                string templateText = objCommon.LookUp("ACD_ADMP_USER_GENERATE_OFFER_LETTER", "OFFER_LETTER_TEMPLATE", "USERNO="+ UserNo);
                SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
                int reg = objSendEmail.SendEmail(lblemail.Text.Trim(), templateText, "Offer Letter"); //Calling Method
                if (reg == 1)
                {
                    count++;

                    objOffer.UpdateOfferLetterSendStatus(Convert.ToInt32(Session["userno"].ToString()), UserNo, Application_no, RoundNo);
                }   
            }

        }
        if (count == 0)
        {
            objCommon.DisplayMessage("Please Select Atleast One Student!", this.Page);
            return;
        }
        if (count > 0)
        {
            objCommon.DisplayMessage("Email Send Successfully.", this.Page);
            return;
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "VW_ACD_COLLEGE_DEGREE_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND BRANCHNO <>2", "BRANCHNO");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = CreateTable_OfferLock();
        string studentIds = string.Empty;
        int Degree = Convert.ToInt32(ddlDegree.SelectedValue);
        int Batch = Convert.ToInt32(ddlAcadyear.SelectedValue);
        int Branch = Convert.ToInt32(ddlBranch.SelectedValue);
        int Test = Convert.ToInt32(ddlEntrance.SelectedValue);
        int Category = Convert.ToInt32(ddlCategory.SelectedValue);
        int Round = Convert.ToInt32(ddlRound.SelectedValue);
        int offertype = Convert.ToInt32(rdobtnoffer.SelectedValue);

        foreach (ListViewDataItem item in lvOffeerLetter.Items)
        {
            CheckBox chkBox = item.FindControl("ChkOffer") as CheckBox;
            Button btnpreview = item.FindControl("btnpreview") as Button;
            HiddenField hdnUserNo = item.FindControl("hdnUserNo") as HiddenField;
            HiddenField hdnOfferid = item.FindControl("hdnOfferid") as HiddenField;

            if (chkBox.Checked == true && chkBox.Enabled == true)
            {
                studentIds = chkBox.ToolTip;
                DataRow dRow = dt.NewRow();
                dRow["USERNO"] = Convert.ToInt32(hdnUserNo.Value);
                dRow["ISLOCKED"] = 1;
                dRow["GENERATE_OFFTER_LETTER_ID"] = Convert.ToInt32(hdnOfferid.Value);
                dt.Rows.Add(dRow);
            }

        }

        ds.Tables.Add(dt);
        string _OfferLock = ds.GetXml();

        if (studentIds.Length <= 0)
        {
            string alertScript = "alert('Please Select Atleast One Student.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", alertScript, true);
            return;
        }

        int status = Convert.ToInt32(objOffer.SubmitOfferLetter(Convert.ToInt32(Session["userno"].ToString()), _OfferLock));
        if (status == 1)
        {

            objCommon.DisplayMessage(this.Page, "Record Submitted Successfully.", Page);
            BindMeritStudentList(Batch, Degree, Test, Branch, Category, Round, offertype);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
        }
    }

    protected void rdobtnoffer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdobtnoffer.SelectedValue == "1")
        {
          
            divhideoffer.Visible = true;
            divPayType.Visible = true;
            Clear();
        }
        else if (rdobtnoffer.SelectedValue == "2")
        {
            
            divhideoffer.Visible = false;
            divPayType.Visible = false;
            Clear();
        }
        else if (rdobtnoffer.SelectedValue == "3")
        {
            
            divhideoffer.Visible = false;
            divPayType.Visible = false;
            Clear();
        }
    }

    #endregion

    #region BlogStorage

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