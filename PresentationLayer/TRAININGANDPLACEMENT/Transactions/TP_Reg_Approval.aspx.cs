//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : TRAINING AND PLACEMENT
// PAGE NAME     : TP REGISTRATION APPROOVAL
// CREATION DATE : 05-AUG-2019
// CREATED BY    : SWAPNIL PRACHAND
// MODIFIED DATE : 10-SEP-2019
// MODIFIED DESC : UPDATED AS PER STUDENT REGISTATION
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

public partial class TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
    BlobController objBlob = new BlobController();
    Panel panelfordropdown;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    decimal File_size;
   
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
        if (!Page.IsPostBack)
        {
           
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                this.CheckPageAuthorization(); 

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillDropDown();
                pnllist.Visible = false;
                BlobDetails();
            }

            //lnkGroup.Attributes.Add("onClick", "return ShowBiodata();");

            //ddlBranch.SelectedValue = Convert.ToString(ViewState["userBranch"]);
            //btnShow_Click(sender, e);
            //Submit.Enabled = false;
            ViewState["userBranch"] = null;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TP_Reg_Approval.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TP_Reg_Approval.aspx");
        }
    }

    protected void FillDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO NOT IN(0)", "DEGREENO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND ActiveStatus=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlBranch,"ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO NOT IN(0)", "BRANCHNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.BindStudents ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void BindStudents()
    {
        try
        {
            DataSet ds = objTP.GetStudentListToApprove(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), 'N');
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                pnllist.Visible = true;
                //Submit.Enabled=true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No more Student to approve", this.Page);
                pnllist.Visible = false;
                //Submit.Enabled=false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.BindStudents ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindStudents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.btnShow_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string IDNOs = string.Empty;
            string RegNos = string.Empty;
            int cnt = 0;
            
            foreach (ListViewDataItem lvItem in lvStudent.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                HiddenField txtRegno = lvItem.FindControl("txtRegNo") as HiddenField;
                if (chkBox.Checked == true)
                {
                    if (txtRegno.Value.ToString().Trim().Equals(string.Empty))
                    {
                        objCommon.DisplayMessage(this.Page, "Reg. No. can not Blank", this.Page);
                        txtRegno.Focus();
                        return;
                    }
                    if (IDNOs.Equals(string.Empty))
                        IDNOs = chkBox.ToolTip;
                    else
                        IDNOs += "," + chkBox.ToolTip;


                    if (RegNos.Equals(string.Empty))
                        RegNos = txtRegno.Value.ToString();
                    else
                        RegNos += "," + txtRegno.Value.ToString();
                    cnt += 1;
                }



            }
            if (IDNOs.Equals(string.Empty))
            {
                objCommon.DisplayMessage(this.Page, "Please Select At least one Student", this.Page);
                return;
            }
            if (!(IDNOs.Equals(string.Empty)))
            {
                int org = Convert.ToInt32(Session["OrgId"]);
                CustomStatus cs = (CustomStatus)objTP.UpdateStudRegStaus(IDNOs, RegNos, org);

                //if (Convert.ToInt32(cs) != -99)
                //{
                //    Response.Redirect(Request.Url.ToString());
                //}
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Records Approved Successfully..!", this.Page);

                    ddlDegree.SelectedIndex = 0;
                    ddlBranch.SelectedIndex = 0;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    pnllist.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.btnSubmit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        
        //Button btnPreview = sender as Button;
        ////Session["userpreview"] = Convert.ToString(btnPreview.CommandArgument);
        //Session["userpreview"] = Convert.ToString(btnPreview.CommandName);

        //Session["EditStatus"] = "N";
        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));
        ////url += "TRAININGANDPLACEMENT/Transactions/Biodata.aspx";
        //url += "TRAININGANDPLACEMENT/Transactions/Biodata.aspx?i=" + Session["EditStatus"];

        ////works fine but not gives schrollbar
        ////ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>");
        
        // //string EditStatus = "N";
        // //Response.Redirect("~/TRAININGANDPLACEMENT/Transactions/Biodata.aspx?i=" + EditStatus);

        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
        ////string newWin = "window.open('" + url + "');";
        ////ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);


        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            //string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/ACADEMIC\\Resume" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            // string img = Convert.ToString(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "FILE_PATH", "ROUTEID='" + routeid + "' and BUSSTR_ID='" + seating + "'"));
            var ImageName = img;
            if (img == null || img == "")
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                //ltEmbed.Text = "Image Not Found....!";


            }
            else
            {
                if (img != "")
                {
                    DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    if (dtBlobPic.Rows.Count == 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Resume is Not Available For This Student.", this.Page);
                        return;
                    }

                    var blob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;

                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    //embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    // DownloadFile(Server.MapPath("~/ACADEMIC/Resume/"), ImageName);
                    string FILENAME = img;
                    string filePath1 = Server.MapPath("~/ACADEMIC/Resume/" + ImageName);


                    string filee = Server.MapPath("~/Transactions/TP_PDF_Reader.aspx");
                    FileInfo file = new FileInfo(filePath1);

                    if (file.Exists)
                    {
                        Session["sb"] = filePath.ToString();
                      
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

                        url += "ACADEMIC/RESUME/" + FILENAME;
                        //string url = filePath;


                        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                        divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        divMsg.InnerHtml += " </script>";

                    }
                }

            }
        }

        catch (Exception ex)
        {
            throw;
        }

    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNametandpdoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvStudent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");// AND DEGREENO="+ddlDegree.SelectedValue, "BRANCHNO");
        }

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string filename = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;

        //To Get the physical Path of the file(test.txt)
        string filepath = Server.MapPath("~/Students_Resume/");

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath + filename);

        // Checking if file exists
        if (myfile.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", myfile.Length.ToString());

            // Set the ContentType
            Response.ContentType = ReturnExtension(myfile.Extension.ToLower());

            // Write the file into the  response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(myfile.FullName);

            // End the response
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Resume is Not Available For This Student.", this.Page);
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
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "DEGREENAME");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE B  INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO) ", "DISTINCT(CD.DEGREENO)", "B.DEGREENAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "CD.DEGREENO");
    }
    //protected void btncareer_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void btnSend_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnCanceladdcompany_Click(object sender, EventArgs e)
    //{

    //}
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        try
        {
            int studentId = 0;
            foreach (ListViewDataItem lvItem in lvStudent.Items)
            {
             
                HiddenField txtidno = lvItem.FindControl("hdidno") as HiddenField;
                studentId = Convert.ToInt32(txtidno.Value);
               
            }
          //  int studentId = (int)Session["studentId"];
            //ViewForm.Src = "/PresentationLayer/TRAININGANDPLACEMENT/Transactions/TP_Career_Profile.aspx?studentId="+ studentId;

           // ,'width=600,height=400,addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + "TP_Career_Profile.aspx?studentId=" + studentId + "','mywindow', 'width=1000,height=1000,fullscreen=yes, scrollbars=auto');";
            divMsg.InnerHtml += " </script>";
            //hfValue.Value = "1";
            // TRAININGANDPLACEMENT/Transactions/
            //mdlopenPage.Show();
        }
        catch (Exception ex)
        {
                       return;
        }
    }
}
