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
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_DepartmentalExamNew : System.Web.UI.Page
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
                // CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
        }

        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewDeptExam();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
        GetConfigForEditAndApprove();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_DepartmentalExam.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_DepartmentalExam.aspx");
        }
    }

    private void BindListViewDeptExam()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllDeptExamDetailsOfEmployee(_idnoEmp);
            lvDeptExam.DataSource = ds;
            lvDeptExam.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvDeptExam.FindControl("divFolder");
                Control ctrHead1 = lvDeptExam.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvDeptExam.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvDeptExam.FindControl("divFolder");
                Control ctrHead1 = lvDeptExam.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvDeptExam.Items)
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
                objUCommon.ShowError(Page, "PayRoll_Pay_DepartmentalExam.BindListViewDeptExam-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //    try
        //    {
        //        Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
        //        ServiceBook objSevBook = new ServiceBook();
        //        objSevBook.IDNO = _idnoEmp;
        //        objSevBook.EXAM = txtNameOfExam.Text;
        //        objSevBook.PASSYEAR = txtYearOfPassing.Text;
        //        objSevBook.OFFICER = txtAttestOfficer.Text;
        //        objSevBook.REGNO = txtRegNo.Text;
        //        objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
        //        if (flupld.HasFile)
        //        {
        //            objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
        //        }
        //        else
        //        {
        //            if (ViewState["attachment"] != null)
        //            {
        //                objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
        //            }
        //            else
        //            {
        //                objSevBook.ATTACHMENTS = string.Empty;
        //            }

        //        }
        //        //Check whether to add or update
        //        if (ViewState["action"] != null)
        //        {
        //            if (ViewState["action"].ToString().Equals("add"))
        //            {
        //                //Add New Help
        //                CustomStatus cs = (CustomStatus)objServiceBook.AddDeptExam(objSevBook);

        //                if (cs.Equals(CustomStatus.RecordSaved))
        //                {
        //                    objServiceBook.upload_new_files("DEPARTMENT_EXAMINATION", _idnoEmp, "DENO", "PAYROLL_SB_DEPTEXAM", "DEX_", flupld);
        //                    this.Clear();
        //                    this.BindListViewDeptExam();
        //                    this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
        //                }
        //            }
        //            else
        //            {
        //                //Edit
        //                if (ViewState["deNO"] != null)
        //                {
        //                    objSevBook.DENO = Convert.ToInt32(ViewState["deNO"].ToString());
        //                    CustomStatus cs = (CustomStatus)objServiceBook.UpdateDeptExam(objSevBook);
        //                    if (cs.Equals(CustomStatus.RecordUpdated))
        //                    {
        //                        objServiceBook.update_upload("DEPARTMENT_EXAMINATION", objSevBook.DENO, ViewState["attachment"].ToString(), _idnoEmp, "DEX_", flupld);
        //                        ViewState["action"] = "add";
        //                        this.Clear();
        //                        this.BindListViewDeptExam();
        //                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToBoolean(Session["error"]) == true)
        //            objUCommon.ShowError(Page, "PayRoll_Pay_DepartmentalExam.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
        //        else
        //            objUCommon.ShowError(Page, "Server UnAvailable");
        //    }




        try
        {
            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");

            if (flupld.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
                {
                    if (flupld.FileContent.Length >= 1024 * 10000)
                    {

                        MessageBox("File Size Should Not Be Greater Than 10 Mb");
                        flupld.Dispose();
                        flupld.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox("Please Upload Valid Files[.jpg,.pdf,.doc,.txt]");
                    flupld.Focus();
                }
            }

            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.EXAM = txtNameOfExam.Text;
            objSevBook.PASSYEAR = txtYearOfPassing.Text;
            objSevBook.OFFICER = txtAttestOfficer.Text;
            objSevBook.REGNO = txtRegNo.Text;
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            objSevBook.EXAMID = Convert.ToInt32(ddlexam.SelectedIndex);
            objSevBook.EXAMname = ddlexam.Text;

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
                    //string name = txtNameOfExam.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_deptexam_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_deptexam_" + time, flupld);
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddDeptExam(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("DEPARTMENT_EXAMINATION", _idnoEmp, "DENO", "PAYROLL_SB_DEPTEXAM", "DEX_", flupld);
                        }
                        this.Clear();
                        this.BindListViewDeptExam();
                        // this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        this.Clear();
                        this.BindListViewDeptExam();
                        MessageBox("Record Already Exist");
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["deNO"] != null)
                    {
                        objSevBook.DENO = Convert.ToInt32(ViewState["deNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateDeptExam(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("DEPARTMENT_EXAMINATION", objSevBook.DENO, ViewState["attachment"].ToString(), _idnoEmp, "DEX_", flupld);
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewDeptExam();
                            //  this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            this.Clear();
                            this.BindListViewDeptExam();
                            MessageBox("Record Already Exist");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_DepartmentalExam.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int deNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(deNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_DepartmentalExam.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int deNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleDeptExamDetailsOfEmployee(deNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["deNO"] = deNO.ToString();
                txtNameOfExam.Text = ds.Tables[0].Rows[0]["exam"].ToString();
                txtRegNo.Text = ds.Tables[0].Rows[0]["regno"].ToString();
                txtAttestOfficer.Text = ds.Tables[0].Rows[0]["officer"].ToString();
                txtYearOfPassing.Text = ds.Tables[0].Rows[0]["passyear"].ToString();
                ddlexam.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["EXAMID"]);
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

                if (Convert.ToBoolean(ViewState["IsApprovalRequire"]) == true)
                {
                    string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                    if (STATUS == "A")
                    {
                        MessageBox("Your Details Are Approved You Cannot Edit.");
                        btnSubmit.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                    GetConfigForEditAndApprove();
                }
                else
                {
                    btnSubmit.Enabled = true;
                    GetConfigForEditAndApprove();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_DepartmentalExam.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int deNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_DEPTEXAM", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "DENO=" + deNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved You Cannot Delete.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteDeptExam(deNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewDeptExam();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_DepartmentalExam.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        GetConfigForEditAndApprove();
    }

    private void Clear()
    {
        txtAttestOfficer.Text = string.Empty;
        txtNameOfExam.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtYearOfPassing.Text = string.Empty;
        ddlexam.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    public string GetFileNamePath(object filename, object DENO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/DEPARTMENT_EXAMINATION/" + idno.ToString() + "/DEX_" + DENO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
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

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Department Examination";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.GetConfigForEditAndApprove-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    #endregion
}