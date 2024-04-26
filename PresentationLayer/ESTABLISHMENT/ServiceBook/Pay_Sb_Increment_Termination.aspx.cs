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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Increment_Termination : System.Web.UI.Page
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

            FillDropDown();
        }

        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);

        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewServiceBook();
        GetConfigForEditAndApprove();
    }

    private void BindListViewServiceBook()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllServiceBookDetailsOfEmployee(_idnoEmp);
            lvServiceBook.DataSource = ds;
            lvServiceBook.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvServiceBook.FindControl("divFolder");
                Control ctrHead1 = lvServiceBook.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvServiceBook.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvServiceBook.FindControl("divFolder");
                Control ctrHead1 = lvServiceBook.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvServiceBook.Items)
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.BindListViewServiceBook-> " + ex.Message + " " + ex.StackTrace);
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
            // Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            //Trno,ETrno,Idno,TypeTranNo,SubDesigno,SubDeptno,Scaleno,OrderNo,GrNo,Remark,PayAllow,OrderDt,GrDt,TermiDt,OrdEffDt,Seqno

            //DataSet  dsPURPOSE = objCommon.FillDropDown("PAYROLL_SB_SERVICEBK", "*", "", "ORDERNO='" + txtOredrNo.Text + "' AND IDNO=" + _idnoEmp , "");
            //  if (dsPURPOSE.Tables[0].Rows.Count > 0)
            //  {
            //      MessageBox("Record Already Exist ");

            //  }
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.TYPETRANNO = Convert.ToInt32(ddlTransactionType.SelectedValue);
            objSevBook.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);
            objSevBook.SUBDEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            if (ddlScale.SelectedIndex > 0)
            {
                objSevBook.SCALENO = Convert.ToInt32(ddlScale.SelectedValue);
            }
            else
            {
                objSevBook.SCALENO = 0;
            }
            objSevBook.ORDERNO = txtOredrNo.Text;
            objSevBook.GRNO = txtGrNo.Text;
            objSevBook.REMARK = txtRemarks.Text;
            if (txtPayAllowance.Text == string.Empty)
            {
                objSevBook.PAYALLOW = 0;
            }
            else
            {
                objSevBook.PAYALLOW = Convert.ToDecimal(txtPayAllowance.Text);
            }
            objSevBook.ORDERDT = txtOrderDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtOrderDate.Text.Trim());

            //  objSevBook.GRDT=Convert.ToDateTime(txtGrDate.Text);
            //objSevBook.TERMIDT=Convert.ToDateTime(txtTerRet.Text);
            objSevBook.TERMIDT = txtTerRet.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtTerRet.Text.Trim());

            // objSevBook.ORDEFFDT=Convert.ToDateTime(txtOrderEffectiveDate.Text);
            objSevBook.ORDEFFDT = txtOrderEffectiveDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtOrderEffectiveDate.Text.Trim());
            if (txtSqNo.Text == string.Empty)
            {
                objSevBook.SEQNO = 0;
            }
            else
            {
                objSevBook.SEQNO = Convert.ToInt32(txtSqNo.Text);
            }
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            if (flupld.HasFile)
            {
                if (flupld.FileContent.Length >= 1024 * 10000)
                {

                    MessageBox("File Size Should Not Be Greater Than 10 Mb");
                    flupld.Dispose();
                    flupld.Focus();
                    return;
                }
                else
                {
                    if (flupld.FileName.Contains(".jpeg") || flupld.FileName.Contains(".png") || flupld.FileName.Contains(".jpg") || flupld.FileName.Contains(".JPEG") || flupld.FileName.Contains(".PNG") || flupld.FileName.Contains(".JPG") || flupld.FileName.Contains(".pdf") || flupld.FileName.Contains(".PDF"))
                    {
                        objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
                    }
                    else
                    {
                        MessageBox("Only .Jpeg/.Jpg/.Png Format is Allow");
                        flupld.Dispose();
                        flupld.Focus();
                        return;
                    }
                }
            }
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
                    // string name = txtSqNo.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_increment_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_increment_" + time, flupld);
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
                    //bool result = CheckPurpose();

                    //if (result == true)
                    //{
                    //    //objCommon.DisplayMessage("Record Already Exist", this);
                    //    MessageBox("Record Already Exist");
                    //    return;
                    //}
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddServiceBk(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (flupld.HasFile)
                            {
                                objServiceBook.upload_new_files("INCREMENT_N_TERMINATION", _idnoEmp, "TRNO", "PAYROLL_SB_SERVICEBK", "INT_", flupld);
                            }
                        }
                        ViewState["action"] = "add";
                        this.Clear();
                        this.BindListViewServiceBook();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        MessageBox("Record Already Exist ");
                        this.Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["Trno"] != null)
                    {

                        objSevBook.TRNO = Convert.ToInt32(ViewState["Trno"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateServiceBk(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("INCREMENT_N_TERMINATION", Convert.ToInt32(objSevBook.TRNO), ViewState["attachment"].ToString(), _idnoEmp, "INT_", flupld);
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewServiceBook();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Trno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(Trno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int Trno)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleServiceBookDetailsOfEmployee(Trno);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Trno"] = Trno.ToString();
                ddlTransactionType.SelectedValue = ds.Tables[0].Rows[0]["TypeTranNo"].ToString();
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["SubDesigno"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["SubDeptno"].ToString();
                ddlScale.SelectedValue = ds.Tables[0].Rows[0]["Scaleno"].ToString();
                txtOredrNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                //txtGrNo.Text = ds.Tables[0].Rows[0]["GrNo"].ToString(); 
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtPayAllowance.Text = ds.Tables[0].Rows[0]["PayAllow"].ToString();
                txtOrderDate.Text = ds.Tables[0].Rows[0]["OrderDt"].ToString();
                //txtGrDate.Text = ds.Tables[0].Rows[0]["GrDt"].ToString(); 
                txtTerRet.Text = ds.Tables[0].Rows[0]["TermiDt"].ToString();
                txtOrderEffectiveDate.Text = ds.Tables[0].Rows[0]["OrdEffDt"].ToString();
                txtSqNo.Text = ds.Tables[0].Rows[0]["Seqno"].ToString();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int Trno = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_SERVICEBK", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "IDNO=" + Trno, "");
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
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteServiceBk(Trno);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    Clear();
                    BindListViewServiceBook();
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlTransactionType.SelectedValue = "0";
        ddlDesignation.SelectedValue = "0";
        ddlDepartment.SelectedValue = "0";
        ddlScale.SelectedValue = "0";
        txtOredrNo.Text = string.Empty;
        txtGrNo.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtPayAllowance.Text = string.Empty;
        txtOrderDate.Text = string.Empty;
        txtGrDate.Text = string.Empty;
        txtTerRet.Text = string.Empty;
        txtOrderEffectiveDate.Text = string.Empty;
        txtSqNo.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    private void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlDepartment, "payroll_SubDept", "subdeptno", "subdept", "subdeptno > 0", "subdeptno");
            objCommon.FillDropDownList(ddlTransactionType, "payroll_TypeTran", "typetranno", "typetran", "typetranno > 0", "typetranno");
            objCommon.FillDropDownList(ddlDesignation, "payroll_SubDesig", "subdesigno", "subdesig", "subdesigno > 0", "subdesigno");
            objCommon.FillDropDownList(ddlScale, "payroll_scale", "scaleno", "scale", "scaleno > 0", "scaleno");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }



    }

    public string GetFileNamePath(object filename, object TRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/INCREMENT_N_TERMINATION/" + idno.ToString() + "/INT_" + TRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_SB_SERVICEBK", "*", "", "ORDERNO='" + txtOredrNo.Text + "' AND IDNO=" + _idnoEmp + "", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
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
            string Command = "Increment / Termination";
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