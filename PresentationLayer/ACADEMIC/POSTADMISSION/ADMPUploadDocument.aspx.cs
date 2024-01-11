using BusinessLogicLayer.BusinessLogic.PostAdmission;
using DynamicAL_v2;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMPUploadDocument : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPDocumentUploadController objstud = new ADMPDocumentUploadController();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                FillDropdown();
               // BindDocument();
                //pnlBind.Visible = false;
            }
        }
    }

    private void BindDocument(int idno,int DegreeNo)
    {
        DataSet dslist = null;        
        //idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);
        dslist = objstud.GetDocumentList(idno, DegreeNo);

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
            if (FinalSubmit == 1)
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

    protected void uploadDocument()
    {
        try
        {

            //string IdNo = Session["stuinfoidno"].ToString();
            int idno = Convert.ToInt32(hdnUserNo.Value);
            //idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);   //Added by sachin on 27-07-2022
          
            string IdNo = hdnUserNo.Value;
            //string idno = Session["idno"].ToString();
            string studentname = Session["userfullname"].ToString();

            //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
            {

                //string CertificateNo = (lvitem.FindControl("txtDocNo") as TextBox).Text;
                //string district = (lvitem.FindControl("txtDistrict") as TextBox).Text;
                //string Issuedate = (lvitem.FindControl("txtIssueDate") as TextBox).Text;
                //string Authority = (lvitem.FindControl("ddlAuthority") as DropDownList).SelectedValue;


                FileUpload fuStudPhoto = lvitem.FindControl("fuFile") as FileUpload;
                //HiddenField hidstudocno = lvitem.FindControl("HiddenField1") as HiddenField;
                //int no = int.Parse(hidstudocno.Value.TrimStart());
                Button btndocno = lvitem.FindControl("btnSubmit") as Button;
                int docno = int.Parse(btndocno.CommandArgument);
                string Docno = btndocno.ToolTip;
                string FUToll = fuStudPhoto.ToolTip;
                //int docno = Convert.ToInt32(Docno);



                if (fuStudPhoto.HasFile)
                {
                    string contentType = contentType = fuStudPhoto.PostedFile.ContentType;
                    //if (!Directory.Exists(folderPath))
                    //{
                    //    Directory.CreateDirectory(folderPath);
                    //}

                    string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                    if (ext == ".pdf")
                    {
                        HttpPostedFile file = fuStudPhoto.PostedFile;
                        string filename = IdNo + "_doc_" + docno + ext;   //Path.GetFileName(fuStudPhoto.PostedFile.FileName);


                        if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                        {
                            //int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_doc_" + hidstudocno.Value + "", fuStudPhoto);

                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_doc_" + docno + "", fuStudPhoto);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                            //CustomStatus cs = (CustomStatus)objstud.AddUpdateStudentDocumentsDetail(Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, "Blob Storage");
                            DateTime Issuedate = DateTime.Now;
                            //string date = Issuedate == string.Empty ? "" : Convert.ToDateTime(Issuedate).ToString("yyyy-MM-dd");
                            string date = Issuedate.ToString() == string.Empty ? "" : Convert.ToString(Issuedate);

                            //   return;
                            CustomStatus cs = (CustomStatus)objstud.AddUpdateDocumentsDetail(idno, Convert.ToInt32(Docno), ext, contentType, filename, "Blob Storage", date);
                            //fuStudPhoto.PostedFile.SaveAs(folderPath + filename);
                            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
                            {
                                objCommon.DisplayMessage(this, "Upload Documents Sucessfully.... !", this);
                                BindDocument(idno,Convert.ToInt32(hdnDegreeNo.Value));
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                            }


                        }


                        else
                        {
                            objCommon.DisplayMessage(this, "Please Upload file Below or Equal to 512 Kb only !", this);
                            //lblmessageShow.ForeColor = System.Drawing.Color.Red;
                            //lblmessageShow.Text = "Please Upload file Below or Equal to 40 Kb only !";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                            return;


                            //goto outer;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Please Upload PDF File Only", this);
                    }

                }
            }
            BindDocument(idno, Convert.ToInt32(hdnDegreeNo.Value));
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        uploadDocument();
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
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

    private void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");

        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlAdmissionBatch.SelectedIndex = 0;

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objstud.GetBranch(Degree);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");        
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0", "D.DEGREENO");
        ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
        ddlDegree.SelectedIndex = 0;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        pnlBind.Visible = false;
        pnlStudent.Visible = false;
       int ADMBATCH =0;
       int ProgramType =0;
       int DegreeNo = 0;

       string SearchBy = null;
       string SearchValue = null;

        try
        {
            //if (ddlProgramType.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UpDoc, "Please Select Program Type.", this.Page);
            //    return;
            //}
            //else if (ddlDegree.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UpDoc, "Please Select Degree.", this.Page);
            //    return;
            //}
            //else if (lstProgram.SelectedValue == "")
            //{
            //    objCommon.DisplayMessage(UpDoc, "Please Select Branch/Program.", this.Page);
            //    return;
            //}
            if ( Convert.ToInt32(ddlSearch.SelectedValue) > 0)
            {
                SearchBy = ddlSearch.SelectedItem.Text;
            }
            if (txtSearch.Text !=string.Empty)
            {
                SearchValue = txtSearch.Text;
            }
            ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            branchno = branchno.TrimEnd(',').Trim();
            

            DataSet ds = null;
            ds = objstud.GetStudentsForDocumentUpload(ADMBATCH, ProgramType, DegreeNo, branchno, SearchBy, SearchValue);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlGV1.Visible = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                lvSchedule.DataBind();

            }
            else
            {
                objCommon.DisplayMessage(this, "No Record Found.", this);
                pnlGV1.Visible = false;
                lvSchedule.Visible = false;
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {       

        LinkButton lnk = sender as LinkButton;
        hdnUserNo.Value =lnk.CommandArgument;
         

        Label lblApplicationId = lnk.Parent.FindControl("lblApplicationId") as Label;
        HiddenField LvDegreeNo = lnk.Parent.FindControl("hdnLvDegreeNo") as HiddenField;
        hdnDegreeNo.Value = LvDegreeNo.Value;

        lblStudentName.Text = lnk.Text;
        lblAppId.Text = lblApplicationId.Text;

        pnlGV1.Visible = false;
        pnlBind.Visible = true;
        pnlStudent.Visible = true;

        BindDocument(Convert.ToInt32(hdnUserNo.Value), Convert.ToInt32(LvDegreeNo.Value));
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
        //var blobList = container.ListBlobs(Id, true);
        var blobList = container.ListBlobs(Id, true);
        
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
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
                BindDocument(Convert.ToInt32(hdnUserNo.Value),Convert.ToInt32(hdnDegreeNo.Value));

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
                BindDocument(Convert.ToInt32(hdnUserNo.Value),Convert.ToInt32(hdnDegreeNo.Value));
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        lstProgram.Items.Clear();
        ddlSearch.SelectedIndex = 0;
        txtSearch.Text = string.Empty;
        pnlStudent.Visible = false;
        pnlGV1.Visible = false;
        pnlBind.Visible = false;
    }
}