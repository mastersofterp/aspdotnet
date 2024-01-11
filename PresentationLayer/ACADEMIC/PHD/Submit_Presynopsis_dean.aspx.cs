using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_Submit_Presynopsis : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    PhdController objPhdC = new PhdController();
    string ua_dept = string.Empty;
    

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
                    this.CheckPageAuthorization();
                    Blob_Storage();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    if (ViewState["action"] == null)
                        ViewState["action"] = "add";
                    string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ViewState["usertype"] = ua_type;
                    ViewState["dec"] = ua_dec;
                    FillDropDown();
                   
                        DivDrops.Visible = true;
                        btnApprove.Visible = false;
                        updEdit.Visible = true;
                        divAdmBatch.Visible = false;
                        DivUpload.Visible = false;
                        btnSubmit.Visible = false;
                        pnlApprove.Visible = true;
                        txtTitle.Enabled = false;
                        pnlApprove.Visible = true;
                        btnApprove.Visible = true;
                      
                        //DivDrops.Visible = true;
                        //
                        //updEdit.Visible = true;
                        //divAdmBatch.Visible = true;
                        //divCriteria.Visible = false;
                        //btnSubmit.Visible = false;
                       
                        //DivUpload.Visible = false;
                        //txtTitle.Enabled = false;
                    
                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                    }

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Submit_Presynopsis.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Submit_Presynopsis.aspx.aspx");
        }
    }
    private void Blob_Storage()
    {

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
    }
    private void FillDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO");
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA_PHD", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Submit_Presynopsis.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ControlActivityOFF_STUDENT()
    {
        string STATUS = objCommon.LookUp("ACD_PHD_PRESYNOPSIS", "ISNULL(DEAN_APPROVAL,0)", "IDNO=" + Convert.ToInt32(Session["idno"]));
        if (STATUS == "1")
        {
           btnApprove.Enabled = false;
        }
        else
        {
             btnApprove.Enabled = true;
        }
    }
    #endregion
    #region Dynamic Search

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails_Phd(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;


                        objCommon.FillDropDownList(ddlDropdown, tablename, "DISTINCT " + column1, column2, "UGPGOT=3", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
        txtSearch.Text = string.Empty;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }
        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
            divCriteria.Visible = true;
            divpanel.Visible = true;
        
    }

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
            DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Panellistview.Visible = true;
                lvStudent.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblNoRecords.Text = "Total Records : 0";
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
        
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            if (Request.QueryString["id"] != null)
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
            else
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                lblidno.Text = dtr["IDNO"].ToString();
                lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
                lbladmbatch.Text = dtr["ADMBATCHNAME"].ToString();
                lblnames.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblDepartment.Text = dtr["BRANCHNAME"].ToString();

                if (dtr["PHDSTATUS"] == null)
                {
                    lblstatussup.Text = "";

                }
                if (dtr["PHDSTATUS"].ToString() == "1")
                {
                    lblstatussup.Text = "Full Time";

                }
                if (dtr["PHDSTATUS"].ToString() == "2")
                {
                    lblstatussup.Text = "Part Time";

                }
            }
        }
    }

    private void ShowStudent()
    {
        try
        {

            string SP_Name2 = "PKG_PHD_GET_PRE_SYNOPSIS";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) +"";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblFileName.Text = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["title"].ToString();
                lvApprove.DataSource = ds;
                lvApprove.DataBind();
                ControlActivityOFF_STUDENT();
                if (lblFileName.Text == "")
                {
                    lblFileName.Visible = false;
                    btnSubmit.Enabled = true;
                    DivDownload.Visible = false;
                }
                else
                {
                    lblFileName.Visible = true;
                    btnSubmit.Enabled = false;
                    DivDownload.Visible = true;
                }
            }
            else
            {
                lvApprove.DataSource = null;
                lvApprove.DataBind();
                objCommon.DisplayMessage(this.Page, "Student not Uploaded Pre-Synopsis Report.", this.Page);
                return;
               
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Text = string.Empty;
        divCriteria.Visible = false;
        divpanel.Visible = false;
    }

    #endregion

    #region pre-synopsis

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

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;

            string blob_ContainerName = "";
            string blob_ConStr = "";
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"] != null)
                {
                    blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"].ToString();
                    blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string FileName = lblFileName.Text;
            string directoryName = "~/PHDDOCUMENT" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string doc = lblFileName.Text;
            var Document = doc;
            string extension = Path.GetExtension(doc.ToString());
            if (doc == null || doc == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 150 kb only !", this.Page);
                return;
            }
            else
            {
                if (extension == ".pdf")
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                    var Newblob = blobContainer.GetBlockBlobReference(Document);
                    string filePath = directoryPath + "\\" + Document;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                    var Newblob = blobContainer.GetBlockBlobReference(Document);
                    string filePath = directoryPath + "\\" + Document;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    //Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string blob_ContainerName = "";
            string blob_ConStr = "";
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"] != null)
                {
                    blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"].ToString();
                    blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
            string filename = string.Empty;
            int idno = Convert.ToInt32(Session["idno"]);
            string title = txtTitle.Text;
            if (fuDoc.HasFile)
            {
                string contentType = contentType = fuDoc.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(fuDoc.PostedFile.FileName);
                string userno = Session["idno"].ToString();
                string OTP = GenerateOTP();
                if (ext == ".pdf")
                {
                    HttpPostedFile file = fuDoc.PostedFile;
                    filename = userno + "_Pre_synopsis_" + OTP;
                    ViewState["filename"] = filename + ext;
                    int fileSize = fuDoc.PostedFile.ContentLength;
                    int KB = fileSize / 1024;
                    if (KB <= 500)
                    {
                        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                        {
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_Pre_synopsis_" + OTP, fuDoc);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
                        else
                        {
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_Pre_synopsis_" + OTP, fuDoc);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 500 kb only !", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Upload file", this.Page);
                return;
            }
            filename = ViewState["filename"].ToString();
            CustomStatus cs = (CustomStatus)objPhdC.InsertSynopsisData(idno, filename, title);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                ViewState["filename"] = null;
                ControlActivityOFF_STUDENT();
                ShowStudent();
            }
        }
        catch
        {
        }
    }

    private string GenerateOTP()
    {
        string allowedChars = "";

        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string otpString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            otpString += temp;
        }
        return otpString;
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = System.IO.Path.GetExtension(FU.FileName);
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
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.UpdateProgress1, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = System.IO.Path.GetFileNameWithoutExtension(FileName);
        try
        {
            System.Threading.Tasks.Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) 
        { }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {

           string SUPERVISOR       =string.Empty;
           string JOINTSUPERVISOR1 =string.Empty;
           string INSTITUTEFACULTY =string.Empty;
           string JOINTSUPERVISOR2 =string.Empty;
           string DRC              =string.Empty;
           string DRCCHAIRMAN      =string.Empty;
            string SUPERROLE = string.Empty;
            string role = string.Empty;
            int IDNO = Convert.ToInt32(Session["idno"]);
            int UANO = Convert.ToInt32(Session["userno"]);
            string SP_Name2 = "PKG_PHD_GET_PRE_SYNOPSIS_dean";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) +"";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                  SUPERROLE         = ds.Tables[0].Rows[0]["SUPERROLE"].ToString();
                  SUPERVISOR        = ds.Tables[0].Rows[0]["APPROVAL_SUP"].ToString();
                  JOINTSUPERVISOR1  = ds.Tables[0].Rows[0]["APPROVAL_J1"].ToString();
                  INSTITUTEFACULTY  = ds.Tables[0].Rows[0]["APPROVAL_INS"].ToString();
                  JOINTSUPERVISOR2  = ds.Tables[0].Rows[0]["APPROVAL_J2"].ToString();
                  DRC               = ds.Tables[0].Rows[0]["APPROVAL_DRC"].ToString();
                  DRCCHAIRMAN       = ds.Tables[0].Rows[0]["APPROVAL_DRC_Chairman"].ToString();
            }
            if (SUPERROLE == "S")
            {
                if (SUPERVISOR =="Approve" & INSTITUTEFACULTY =="Approve"& DRC =="Approve"& DRCCHAIRMAN == "Approve")
                {
                    role = "DEAN";
                    CustomStatus cs = (CustomStatus)objPhdC.ApprovePre_Synopsis_Report(IDNO, role);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this, "Student Pre-Synopsis Report Approve sucessfully", this.Page);
                        ShowStudent();
                        ControlActivityOFF_STUDENT();
                        //btnApprove.Enabled = false;
                    } 
                }
                else
                {

                    objCommon.DisplayMessage(this, "All Supervisor Approval not Done", this.Page);
                    return;
                }
            }
            else if (SUPERROLE == "J")
            {
                if (SUPERVISOR == "Approve" & INSTITUTEFACULTY == "Approve" & DRC == "Approve" & DRCCHAIRMAN == "Approve" & JOINTSUPERVISOR1 == "Approve")
                {
                    role = "DEAN";
                    CustomStatus cs = (CustomStatus)objPhdC.ApprovePre_Synopsis_Report(IDNO, role);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this, "Student Pre-Synopsis Report Approve sucessfully", this.Page);
                        ShowStudent();
                        ControlActivityOFF_STUDENT();
                        //btnApprove.Enabled = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "All Supervisor Approval not Done", this.Page);
                    return;
                }
            }
            else if (SUPERROLE == "T")
            {
                if (SUPERVISOR == "Approve" & INSTITUTEFACULTY == "Approve" & DRC == "Approve" & DRCCHAIRMAN == "Approve" & JOINTSUPERVISOR1 == "Approve" & JOINTSUPERVISOR2 == "Approve")
                {
                    role = "DEAN";
                    CustomStatus cs = (CustomStatus)objPhdC.ApprovePre_Synopsis_Report(IDNO, role);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this, "Student Pre-Synopsis Report Approve sucessfully", this.Page);
                        ShowStudent();
                        ControlActivityOFF_STUDENT();
                        //btnApprove.Enabled = false;
                    }
                }
                else
                {

                    objCommon.DisplayMessage(this, "All Supervisor Approval not Done", this.Page);
                    return;
                }
            }
           
        }
        catch (Exception ex)
        {
        }

    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion
    protected void lnkId_Click(object sender, EventArgs e)
    {

        LinkButton lnk = sender as LinkButton;

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        Session["idno"] = Session["stuinfoidno"].ToString();
        //lvStudent.Visible = false;
        //lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        ShowStudentDetails();
        ShowStudent();
        divmain.Visible = true;
        DivSutLog.Visible = true;
        updEdit.Visible = false;
        Panellistview.Visible = false;
        ControlActivityOFF_STUDENT();
    }
}