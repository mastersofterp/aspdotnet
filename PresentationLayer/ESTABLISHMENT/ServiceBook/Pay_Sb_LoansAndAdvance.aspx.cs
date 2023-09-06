﻿using System;
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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_LoansAndAdvance : System.Web.UI.Page
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

            FillDropDown();


        }
        int user_type = 0;
        user_type = Convert.ToInt32(Session["usertype"].ToString());
        if (user_type != 1)
        {
            btnSubmit.Visible = false; btnCancel.Visible = false;
            btnSubmit.Enabled = false;
            ddlLoanName.Enabled = false;
            txtLoanDate.Enabled = false;
            txtReMarks.Enabled = false;
            txtAmount.Enabled = false;
            txtOrderNo.Enabled = false;
            txtRateOfInterest.Enabled = false;
            txtNoOfInstallMent.Enabled = false;
        }
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewLoansAndAdvance();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_LoansAndAdvance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_LoansAndAdvance.aspx");
        }
    }

    private void BindListViewLoansAndAdvance()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllLoanDetailsOfEmployee(_idnoEmp);
            lvLoanAndAdvance.DataSource = ds;
            lvLoanAndAdvance.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvLoanAndAdvance.FindControl("divFolder");
                Control ctrHead1 = lvLoanAndAdvance.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvLoanAndAdvance.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvLoanAndAdvance.FindControl("divFolder");
                Control ctrHead1 = lvLoanAndAdvance.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvLoanAndAdvance.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = true;
                    ckattach.Visible = false;
                }
            }
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvLoanAndAdvance.FindControl("divFolder1");
                Control ctrHead1 = lvLoanAndAdvance.FindControl("divBlob1");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvLoanAndAdvance.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvLoanAndAdvance.FindControl("divFolder1");
                Control ctrHead1 = lvLoanAndAdvance.FindControl("divBlob1");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvLoanAndAdvance.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                    ckBox.Visible = true;
                    ckattach.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.BindListViewLoansAndAdvance-> " + ex.Message + " " + ex.StackTrace);
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

                if (flupAFF.HasFile)
               {
                  if (flupAFF.FileContent.Length >= 1024 * 10000)
                  {

                    MessageBox("File Size Should Not Be Greater Than 10 Mb");
                    flupAFF.Dispose();
                    flupAFF.Focus();
                    return;
                     }
                 }
            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.LOANDT = Convert.ToDateTime(txtLoanDate.Text);
            objSevBook.REMARK = txtReMarks.Text;
            objSevBook.LOANNO = Convert.ToInt32(ddlLoanName.Text);
            objSevBook.ORDERNO = txtOrderNo.Text;
            objSevBook.AMOUNT = Convert.ToDecimal(txtAmount.Text);
            objSevBook.INTEREST = Convert.ToDecimal(txtRateOfInterest.Text);
            objSevBook.INSTAL = Convert.ToDecimal(txtNoOfInstallMent.Text);
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
                    string name = ddlLoanName.SelectedItem.Text.Replace(" ", "");
                    filename = IdNo + "_loanadvund_" + name + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_loanadvund_" + name, flupld);
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

            //Changes done for Blob
            //if (lblBlobConnectiontring.Text == "")
            //{
            //    objSevBook.ISBLOB = 0;
            //}
            //else
            //{
            //    objSevBook.ISBLOB = 1;
            //}
            if (objSevBook.ISBLOB == 1)
            {
                string filename = string.Empty;
                string FilePath = string.Empty;
                string IdNo = _idnoEmp.ToString();
                if (flupAFF.HasFile)
                {
                    string contentType = contentType = flupAFF.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(flupAFF.PostedFile.FileName);
                    //HttpPostedFile file = flupAFF.PostedFile;
                    //filename = objSevBook.IDNO + "_familyinfo" + ext;
                    string name = ddlLoanName.SelectedItem.Text.Replace(" ", "");
                    filename = IdNo + "_loanadvaff_" + name + ext;
                    objSevBook.AFFIDAVITATTACH = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupAFF.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_loanadvaff_" + name, flupAFF);
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
                if (flupAFF.HasFile)
                {
                    objSevBook.AFFIDAVITATTACH = Convert.ToString(flupAFF.PostedFile.FileName.ToString());
                }
                else
                {
                    if (ViewState["affidavitattach"] != null)
                    {
                        objSevBook.AFFIDAVITATTACH = ViewState["affidavitattach"].ToString();
                    }
                    else
                    {
                        objSevBook.AFFIDAVITATTACH = string.Empty;
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddLoan(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (flupld.HasFile)
                            {
                                objServiceBook.upload_new_files("LOAN_N_ADVANCE", _idnoEmp, "LNO", "PAYROLL_SB_LOAN", "LNA_", flupld);
                            }
                            if (flupAFF.HasFile)
                            {
                                objServiceBook.upload_new_files("LOAN_N_ADVANCE", _idnoEmp, "LNO", "PAYROLL_SB_LOAN", "LNA_", flupAFF);
                            }
                        }
                        this.Clear();
                        this.BindListViewLoansAndAdvance();
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
                    if (ViewState["lNo"] != null)
                    {
                        objSevBook.LNO = Convert.ToInt32(ViewState["lNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateLoan(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("LOAN_N_ADVANCE", objSevBook.LNO, ViewState["attachment"].ToString(), _idnoEmp, "LNA_", flupld);

                            objServiceBook.update_upload("LOAN_N_ADVANCE", objSevBook.LNO, ViewState["affidavitattach"].ToString(), _idnoEmp, "LNA_", flupAFF);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewLoansAndAdvance();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
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
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int lNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(lNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int lNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleLoanDetailsOfEmployee(lNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lno,idno,loanno,orderno,amount,interest,instal,remark,loandt
                ViewState["lNo"] = lNo.ToString();
                ddlLoanName.SelectedValue = ds.Tables[0].Rows[0]["loanno"].ToString();
                txtLoanDate.Text = ds.Tables[0].Rows[0]["loandt"].ToString();
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["orderno"].ToString();
                txtRateOfInterest.Text = ds.Tables[0].Rows[0]["interest"].ToString();
                txtNoOfInstallMent.Text = ds.Tables[0].Rows[0]["instal"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                ViewState["affidavitattach"] = ds.Tables[0].Rows[0]["AFFIDAVITATTACH"].ToString();

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
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int lNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_LOAN", "*", "", "LNO=" + lNo, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteLoan(lNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindListViewLoansAndAdvance();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlLoanName.SelectedValue = "0";
        txtLoanDate.Text = string.Empty;
        txtReMarks.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtOrderNo.Text = string.Empty;
        txtRateOfInterest.Text = string.Empty;
        txtNoOfInstallMent.Text = string.Empty;
        ViewState["action"] = "add";
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlLoanName, "payroll_LoanType", "LOANNO", "LOANNAME", "LOANNO >  0", "LOANNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object LNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/LOAN_N_ADVANCE/" + idno.ToString() + "/LNA_" + LNO + "." + extension[1].ToString().Trim());
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
    

    protected void imgbtnPreview1_Click(object sender, ImageClickEventArgs e)
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
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string Script = string.Empty;

                string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
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