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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Admin_Responsibilities : System.Web.UI.Page
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
        BindListViewAdminResponsiblities();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Admin_Responsibilities.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Admin_Responsibilities.aspx");
        }
    }

    private void BindListViewAdminResponsiblities()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllAdminResponsibilities(_idnoEmp);
            lvAdminResponsibilities.DataSource = ds;
            lvAdminResponsibilities.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvAdminResponsibilities.FindControl("divFolder");
                Control ctrHead1 = lvAdminResponsibilities.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvAdminResponsibilities.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvAdminResponsibilities.FindControl("divFolder");
                Control ctrHead1 = lvAdminResponsibilities.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvAdminResponsibilities.Items)
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
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.BindListViewAdminResponsiblities-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.RESPONSIBILITY = txtResponsibility.Text;
            objSevBook.ORGANIZATION = txtOrganization.Text;
            objSevBook.IsCurrent = chkIsCurrent.Checked == true ? 1 : 0;
            objSevBook.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
            //objSevBook.TODATE = Convert.ToDateTime(txtToDate.Text);
            if (txtToDate.Text.Trim() == string.Empty)
            {
                objSevBook.TODATE = Convert.ToDateTime("9999/12/31"); // DateTime.MinValue;
            }
            else
            {
                objSevBook.TODATE = Convert.ToDateTime(txtToDate.Text);
            }
            if (chkIsCurrent.Checked == false)
            {
                if (txtToDate.Text.Trim() == string.Empty)
                {
                    MessageBox("Please Select To Date");
                    txtToDate.Text = string.Empty;
                    txtToDate.Focus();
                    return;
                }
                if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                {
                    MessageBox("To Date Should be Greater than  or equal to From Date");
                    txtToDate.Text = string.Empty;
                    txtToDate.Focus();
                    return;
                }
            }
            objSevBook.REMARK = txtReMarks.Text;
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
                    string name = txtResponsibility.Text.Replace(" ", "");
                    filename = IdNo + "_adminresp_" + name + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_adminresp_" + name, flupld);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddAdminResponsibilities(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //this.BindListViewAdminResponsiblities();
                        MessageBox("Record Saved Successfully");
                        this.Clear();
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("ADMIN_RESPONSIBLITY", _idnoEmp, "ADMINTRXNO", "PAYROLL_SB_ADMIN_RESPONSIBILITIES", "ADM_", flupld);
                        }
                        
                        this.BindListViewAdminResponsiblities();
                       // this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        //MessageBox("Record Saved Successfully");
                    }
                    else if(cs.Equals(CustomStatus.RecordExist))
                    {
                        this.Clear();
                        this.BindListViewAdminResponsiblities();
                       
                        MessageBox("Record Already Exist");
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["ADMINTRXNO"] != null)
                    {
                        objSevBook.ADMINTRXNO = Convert.ToInt32(ViewState["ADMINTRXNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateAdminResponsibilities(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            this.Clear();
                            this.BindListViewAdminResponsiblities();
                            objServiceBook.update_upload("ADMIN_RESPONSIBLITY", Convert.ToInt32(objSevBook.ADMINTRXNO), ViewState["attachment"].ToString(), _idnoEmp, "ADM_", flupld);
                            ViewState["action"] = "add";
                      
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                           
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            this.Clear();
                            this.BindListViewAdminResponsiblities();

                            MessageBox("Record Already Exist");
                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ADMINTRXNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(ADMINTRXNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int ADMINTRXNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleAdminResponsibilities(ADMINTRXNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["ADMINTRXNO"] = ADMINTRXNO.ToString();
                txtResponsibility.Text = ds.Tables[0].Rows[0]["Responsibility"].ToString();
                txtOrganization.Text = ds.Tables[0].Rows[0]["ORGANIZATION"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FROMDATE"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["TODATE"].ToString();
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                chkIsCurrent.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Iscurrent"]);
                if (chkIsCurrent.Checked == true)
                {
                    txtToDate.Enabled = false;
                }
                else
                {                
                    txtToDate.Enabled = true;
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
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int ADMINTRXNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_ADMIN_RESPONSIBILITIES", "*", "", "ADMINTRXNO=" + ADMINTRXNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteAdminResponsibilities(ADMINTRXNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewAdminResponsiblities();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtResponsibility.Text = string.Empty;
        txtOrganization.Text = string.Empty;
        txtReMarks.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        ViewState["action"] = "add";
        chkIsCurrent.Checked = false;
    }

    public string GetFileNamePath(object filename, object ADMINTRXNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/ADMIN_RESPONSIBLITY/" + idno.ToString() + "/ADM_" + ADMINTRXNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo,Test;
        if (DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromDate.Text);
                DtTo = Convert.ToDateTime(txtToDate.Text);
                if (DtTo < DtFrom)
                {
                    MessageBox("To Date Should be Greater than  or equal to From Date");
                    txtToDate.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtToDate.Text = string.Empty;
        }
    }
    protected void chkIsCurrent_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkIsCurrent.Checked)
        {
            txtToDate.Text = string.Empty;
            txtToDate.Enabled = true;
        }
        else
        {
            txtToDate.Enabled = false;
            txtToDate.Text = string.Empty;
        }
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