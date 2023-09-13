using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Globalization;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Invited_Talks : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    BlobController objBlob = new BlobController();

    public int _idnoEmp;

    protected void Page_Load(object sender, EventArgs e)
    {

        //string empno = ViewState["idno"].ToString();

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
                //CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
        }
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewInvitedTalks();

    }

    private void BindListViewInvitedTalks()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllInvitedTalk(_idnoEmp);
            lvInvitedTalks.DataSource = ds;
            lvInvitedTalks.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvInvitedTalks.FindControl("divFolder");
                Control ctrHead1 = lvInvitedTalks.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvInvitedTalks.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvInvitedTalks.FindControl("divFolder");
                Control ctrHead1 = lvInvitedTalks.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvInvitedTalks.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = true;
                    ckattach.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Invited_Talks .BindListViewInvitedTalks-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (flupld.HasFile)
            {
                if (flupld.FileContent.Length >= 1024 * 10000)
                {

                    MessageBox("File Size Should Not Be Greater Than 10 Mb");
                    flupld.Dispose();
                    flupld.Focus();
                    return;
                }
            }
           // Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.SUBJECT = txtSubject.Text;
            objSevBook.VENU = txtVenu.Text;

            if (txtDuration.Text != string.Empty)
            {
                objSevBook.DURATION = txtDuration.Text;
            }
            else
            {
                objSevBook.DURATION = string.Empty;
            }
            objSevBook.DATEOFTALK = Convert.ToDateTime(txtDateOftalk.Text);
            objSevBook.REMARK = txtRemark.Text;

            if (rdbMode.SelectedValue == "0")
            {
                objSevBook.MODE = "Online";
            }
            else
            {
                objSevBook.MODE = "Offline";
            }

            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Changes done for Blob
            if (lblBlobConnectiontring.Text == "")
            {
                objSevBook.ISBLOB = 0;
            }
            else
            {
                objSevBook.ISBLOB = 1;
            }
            if (objSevBook.ISBLOB == 1)
            {
                string filename = string.Empty;
                string FilePath = string.Empty;
                string IdNo = _idnoEmp.ToString();
                if (flupld.HasFile)
                {
                    string contentType = contentType = flupld.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(flupld.PostedFile.FileName);
                    //HttpPostedFile file = flupld.PostedFile;
                    //filename = objSevBook.IDNO + "_familyinfo" + ext;
                    //string name = txtSubject.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_guestlecture_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_guestlecture_" + time, flupld);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (ViewState["attachment"] != null)
                    {
                        objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
                    }
                    else
                    {
                        objSevBook.ATTACHMENTS = string.Empty;
                    }

                }
            }
            else
            {
                if (flupld.HasFile)
                {
                    objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
                }
                else
                {
                    if (ViewState["attachment"] != null)
                    {
                        objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
                    }
                    else
                    {
                        objSevBook.ATTACHMENTS = string.Empty;
                    }

                }
            }
            //
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddInvitedTalk(objSevBook);


                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (flupld.HasFile)
                            {
                                objServiceBook.upload_new_files("INVITED_TALK", _idnoEmp, "INVTRXNO", "PAYROLL_SB_INVITED_TALK", "INV_", flupld);
                            }
                        }
                        this.Clear();
                        this.BindListViewInvitedTalks();
                       // this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exist ");
                        this.Clear();
                    } 
                }
                else
                {
                    //Edit
                    if (ViewState["INVTRXNO"] != null)
                    {
                        objSevBook.INVTRXNO = Convert.ToInt32(ViewState["INVTRXNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateInvitedTalk(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("INVITED_TALK", Convert.ToInt32(objSevBook.INVTRXNO), ViewState["attachment"].ToString(), _idnoEmp, "INV_", flupld);
                            }
                                ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewInvitedTalks();
                           // this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            MessageBox("Record Already Exist ");
                            this.Clear();
                        } 
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Invited_Talks .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int INVTRXNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(INVTRXNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Invited_Talks .btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int INVTRXNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleInvitedTalk(INVTRXNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["INVTRXNO"] = INVTRXNO.ToString();
                txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                txtVenu.Text = ds.Tables[0].Rows[0]["VENU"].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();
                txtDateOftalk.Text = ds.Tables[0].Rows[0]["DATEOFTALK"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

                string mode = ds.Tables[0].Rows[0]["MODE"].ToString();
                if (mode == "Online")
                {
                    rdbMode.SelectedValue = "0";
                }
                else
                {
                    rdbMode.SelectedValue = "1";
                }
                string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                if (STATUS == "A")
                {
                    MessageBox("Your Details are Approved you cannot edit.");
                    return;
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Invited_Talks .ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            //ds.Clear();
            //ds.Dispose();
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int INVTRXNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_INVITED_TALK", "*", "", "INVTRXNO=" + INVTRXNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteInvitedTalk(INVTRXNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    BindListViewInvitedTalks();
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Invited_Talks .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtSubject.Text = string.Empty;
        txtVenu.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtDateOftalk.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ViewState["action"] = "add";
        rdbMode.SelectedIndex = 0;
    }

    public string GetFileNamePath(object filename, object INVTRXNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/INVITED_TALK/" + idno.ToString() + "/INV_" + INVTRXNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    #region Blob
    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameEmployee";
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

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blobpath = dtBlobPic.Rows[0]["Uri"].ToString();
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string Script = string.Empty;

                string DocLink = blobpath;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
}