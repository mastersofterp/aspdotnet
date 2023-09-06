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

public partial class ACADEMIC_PHD_PhdMeeting : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhdC = new PhdController();


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
                    ViewState["usertype"] = ua_type;
                    ViewState["action"] = "add";

                        PopulateDropDownList();
                       
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    BindData();
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
                Response.Redirect("~/notauthorized.aspx?page=PhdMeeting.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdMeeting.aspx.aspx");
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
                objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
    }
    private void PopulateDropDownList()
    {
        objCommon.FillListBox(lboDesignation, "ACD_PHD_ALLOTED_SUPERVISOR", "IDNO", "STUDNAME", "IDNO>0 and SUPERVISOR_UANO=" + Convert.ToInt32(Session["userno"]), "IDNO");
        objCommon.FillListBox(lboSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO)", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND DESIGNATIONNO IN (1,3)", "ua_fullname");
        objCommon.FillListBox(lboextSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");
    }
    #endregion

    #region BindData
    private void BindData()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_PHD_STUDENTS_MEETING";
            string SP_Parameters2 = "@P_UANO";
            string Call_Values2 = "" + Convert.ToInt32(Session["userno"]) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                lvMeeting.DataSource = dsStudList;
                lvMeeting.DataBind();
            }
            else
            {
                //btnSubmit.Visible = false;
               // objCommon.DisplayMessage(this.updDist, "No Record Found", this.Page);
                lvMeeting.DataSource = null;
                lvMeeting.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
   #endregion

    #region Download 
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

    protected void imgbtnpfPrevDoc_Click(object sender, ImageClickEventArgs e)
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
                objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        string directoryName = "~/ODAPPLYDOCUMENT" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string doc = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var Document = doc;
        string extension = Path.GetExtension(doc.ToString());
        if (doc == null || doc == "")
        {
            objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 500 kb only !", this.Page);
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

    #endregion

    #region submit
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            string SP_Name2 = "PKG_ACD_GET_PHD_STUDENTS_edit_MEETING";
            string SP_Parameters2 = "@P_UANO,@P_MEETINGNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["userno"]) + "," +
                                 Convert.ToInt32(ID) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string did = string.Empty;
                lblFileName.Visible = true;
                txttitle.Text = ds.Tables[0].Rows[0]["MEETING_TITLE"].ToString();
                txtStartDate.Text =ds.Tables[0].Rows[0]["MEETINGDATE"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                lblFileName.Text = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                objCommon.FillListBox(lboDesignation, "ACD_PHD_ALLOTED_SUPERVISOR", "IDNO", "STUDNAME", "IDNO>0 and SUPERVISOR_UANO=" + Convert.ToInt32(Session["userno"]), "IDNO");
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    did = ds.Tables[0].Rows[j]["IDNO"].ToString();
                    for (int i = 0; i < lboDesignation.Items.Count; i++)
                    {
                        if (did.ToString() == lboDesignation.Items[i].Value)
                        {
                            lboDesignation.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhdMeeting.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
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
                    objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
            string internal_uano = string.Empty;
            string external_uano = string.Empty;
            string meeting_title = txttitle.Text;
            string IPADDRESS = Session["ipAddress"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            DateTime meeting_date = Convert.ToDateTime(txtStartDate.Text);
            string IDNO = string.Empty;
            int count = 0;
            foreach (ListItem Item in lboDesignation.Items)
            {
                if (Item.Selected)
                {
                    IDNO += Item.Value + ",";
                    count++;
                }
            }
            IDNO = IDNO.Substring(0, IDNO.Length - 1);

            //for internal
            foreach (ListItem Item in lboSupervisor.Items)
            {
                if (Item.Selected)
                {
                    internal_uano += Item.Value + ",";
                    count++;
                }
            }
            if(internal_uano != string.Empty)
            {
                internal_uano = internal_uano.Substring(0, internal_uano.Length - 1);
            }
            
            //for external
            foreach (ListItem Item in lboextSupervisor.Items)
            {
                if (Item.Selected)
                {
                    external_uano += Item.Value + ",";
                    count++;
                }
            }
            if (external_uano != string.Empty)
            {
                external_uano = external_uano.Substring(0, external_uano.Length - 1);
            }
            
            string desc = txtDescription.Text;
            int MEETING_NUMBER =Convert.ToInt32(objCommon.LookUp("ACD_PHD_MEETING_DETAILS", "ISNULL(MAX(MEETING_NUMBER),0) AS MEETING_NUMBER", ""));
            int MEETING_NUMBER1 = MEETING_NUMBER + 1;
            if (fuDoc.HasFile)
            {
                string contentType = contentType = fuDoc.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(fuDoc.PostedFile.FileName);
                string userno = Session["userno"].ToString();
                string OTP = GenerateOTP();
                if (ext == ".pdf")
                {
                    HttpPostedFile file = fuDoc.PostedFile;
                    string filename = userno + "_PHD_MOM_" + OTP;
                    ViewState["filename"] = filename + ext;
                    int fileSize = fuDoc.PostedFile.ContentLength;
                    int KB = fileSize / 1024;
                    if (KB <= 500)
                    {
                        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                        {
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_PHD_MOM_" + OTP, fuDoc);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
                        else
                        {
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_PHD_MOM_" + OTP, fuDoc);
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
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                string filename = string.Empty;
                if (ViewState["filename"] == null)
                {
                    if (lblFileName.Text == "")
                    {
                        filename = string.Empty;
                    }
                    else
                    {
                        filename = lblFileName.Text;
                    }
                }
                else
                {
                    filename = ViewState["filename"].ToString();
                }
                CustomStatus cs = (CustomStatus)objPhdC.InsertMeetingData(IDNO, 0, meeting_title, meeting_date, desc, filename, MEETING_NUMBER1, UA_NO, IPADDRESS, internal_uano, external_uano);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                    ViewState["filename"] = null;
                }
            }
            else
            {
                int id = Convert.ToInt32(ViewState["id"]);
                string filename = string.Empty;
                if (ViewState["filename"] == null)
                {
                    filename = string.Empty;
                }
                else
                {
                    filename = ViewState["filename"].ToString();
                }
                CustomStatus cs = (CustomStatus)objPhdC.InsertMeetingData(IDNO, id, meeting_title, meeting_date, desc, filename, MEETING_NUMBER1, UA_NO, IPADDRESS, internal_uano, external_uano);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    BindData();
                    ClearControls();
                    ViewState["filename"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void ClearControls()
    {
        txttitle.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtDescription.Text = string.Empty;
        lboDesignation.ClearSelection();
        lboSupervisor.ClearSelection();
        lboextSupervisor.ClearSelection();
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
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

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
                objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updDist, "Something went wrong, Blob Storage container related details not found.", this.Page);
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
        catch (Exception) { }
    }
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}