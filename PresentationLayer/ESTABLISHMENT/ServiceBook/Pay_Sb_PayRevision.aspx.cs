using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_PayRevision : System.Web.UI.Page
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
            ddlDesignation.Enabled = false;
            ddlScale.Enabled = false;
            txtToDate.Enabled = false;
            txtFromDate.Enabled = false;
            txtRemarks.Enabled = false;
            //rdoPayrevision.Enabled = false;
        }
        else
        {
            btnSubmit.Visible = true; btnCancel.Visible = true;
        }

        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);



        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewPayRevision();
        GetConfigForEditAndApprove();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_PayRevision.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PayRevision.aspx");
        }
    }

    private void BindListViewPayRevision()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllPayRevisionOfEmployee(_idnoEmp);
            lvPayRevision.DataSource = ds;
            lvPayRevision.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvPayRevision.FindControl("divFolder");
                Control ctrHead1 = lvPayRevision.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvPayRevision.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvPayRevision.FindControl("divFolder");
                Control ctrHead1 = lvPayRevision.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvPayRevision.Items)
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
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.BindListViewPayRevision-> " + ex.Message + " " + ex.StackTrace);
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

            //if (flupld.HasFile)
            //{
            //    if (flupld.FileContent.Length >= 1024 * 10000)
            //    {

            //        MessageBox("File Size Should Not Be Greater Than 10 Mb");
            //        flupld.Dispose();
            //        flupld.Focus();
            //        return;
            //    }
            //}
            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");


            ServiceBook objSevBook = new ServiceBook();

            objSevBook.IDNO = _idnoEmp;
            objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
            objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);

            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                objCommon.DisplayMessage("To Date Should be Greater than  or equal to From Date", this.Page);
                txtToDate.Text = string.Empty;
                txtToDate.Focus();
                return;
            }


            objSevBook.REMARK = txtRemarks.Text;
            objSevBook.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);
            objSevBook.SCALENO = Convert.ToInt32(ddlScale.SelectedValue);

            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();

            //if (rdoPromotion.Checked)
            //    objSevBook.TYPE = "PA";
            //else
            //    objSevBook.TYPE = "PR";

            if (rdbPayRevision.SelectedValue == "0")
            {
                objSevBook.TYPE = "PR";
            }
            else if (rdbPayRevision.SelectedValue == "1")
            {
                objSevBook.TYPE = "PA";
            }
            else if (rdbPayRevision.SelectedValue == "2")
            {
                objSevBook.TYPE = "BO";

            }



            if (txtAmount.Text != string.Empty)
            {
                objSevBook.AMOUNT = Convert.ToDecimal(txtAmount.Text);
            }
            else
            {
                objSevBook.AMOUNT = null;
            }

            if (txtBasic.Text != string.Empty)
            {
                objSevBook.BASIC = Convert.ToDecimal(txtBasic.Text);
            }
            else
            {
                objSevBook.BASIC = null;
            }

            if (txtAGP.Text != string.Empty)
            {
                objSevBook.AGP = Convert.ToDecimal(txtAGP.Text);
            }
            else
            {
                objSevBook.AGP = null;
            }

            if (txtHRA.Text != string.Empty)
            {
                objSevBook.HRA = Convert.ToDecimal(txtHRA.Text);
            }
            else
            {
                objSevBook.HRA = null;
            }

            if (txtPost.Text != string.Empty)
            {
                objSevBook.REVISEDPOST = txtPost.Text;
            }
            else
            {
                objSevBook.REVISEDPOST = string.Empty;
            }

            if (txtGross.Text != string.Empty)
            {
                objSevBook.GROSS = Convert.ToDecimal(txtGross.Text);
            }
            else
            {
                objSevBook.GROSS = null;
            }

            if (txtNet.Text != string.Empty)
            {
                objSevBook.NET = Convert.ToDecimal(txtNet.Text);
            }
            else
            {
                objSevBook.NET = null;
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
                    // HttpPostedFile file = flupld.PostedFile;
                    //filename = objSevBook.IDNO + "_familyinfo" + ext;
                    //string name = ddlDesignation.SelectedItem.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_payrevision_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_payrevision_" + time, flupld);
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
                    int CHK = Convert.ToInt32(objCommon.LookUp("PAYROLL_SB_PAYREV", "ISNULL(COUNT(*),0) as Count", " IDNO=" + Convert.ToString(_idnoEmp) + " AND SUBDESIGNO=" + ddlDesignation.SelectedValue + " AND TYPE='" + objSevBook.TYPE + "' AND SCALENO=" + ddlScale.SelectedValue));
                    if (CHK > 0)
                    {
                        MessageBox("Record Already Existed");
                        return;
                    }
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddPayRevision(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("PAY_REVISION", _idnoEmp, "PRNO", "PAYROLL_SB_PAYREV", "PAY_", flupld);
                        }
                        this.Clear();
                        this.BindListViewPayRevision();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        this.Clear();
                        this.BindListViewPayRevision();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Already Exist");
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["prNo"] != null)
                    {
                        objSevBook.PRNO = Convert.ToInt32(ViewState["prNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdatePayRevision(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("PAY_REVISION", objSevBook.PRNO, ViewState["attachment"].ToString(), _idnoEmp, "PAY_", flupld);
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewPayRevision();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            this.Clear();
                            this.BindListViewPayRevision();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                            MessageBox("Record Already Exist");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int prNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(prNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int prNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePayRevisionOfEmployee(prNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["prNo"] = prNo.ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                ddlScale.SelectedValue = ds.Tables[0].Rows[0]["Scaleno"].ToString();
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["SubDesigno"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["MODIFIED_AMOUNT"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() == "PR")
                {
                    //rdoPromotion.Checked = true;
                    //rdoPayrevision.Checked = false;
                    rdbPayRevision.SelectedValue = "0";
                }
                else if (ds.Tables[0].Rows[0]["Type"].ToString() == "PA")
                {
                    //rdoPromotion.Checked = false;
                    //rdoPayrevision.Checked = true;
                    rdbPayRevision.SelectedValue = "1";
                }
                else if (ds.Tables[0].Rows[0]["Type"].ToString() == "BO")
                {
                    rdbPayRevision.SelectedValue = "2";
                }

                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

                txtPost.Text = ds.Tables[0].Rows[0]["REVISEDPOST"].ToString();
                txtBasic.Text = ds.Tables[0].Rows[0]["BASIC"].ToString();
                txtAGP.Text = ds.Tables[0].Rows[0]["AGP"].ToString();
                txtHRA.Text = ds.Tables[0].Rows[0]["HRA"].ToString();
                txtGross.Text = ds.Tables[0].Rows[0]["GROSS"].ToString();
                txtNet.Text = ds.Tables[0].Rows[0]["NET"].ToString();

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
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int prNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_PAYREV", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "PRNO=" + prNo, "");
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
                CustomStatus cs = (CustomStatus)objServiceBook.DeletePayRevision(prNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    Clear();
                    BindListViewPayRevision();
                    ViewState["action"] = "add";

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlDesignation.SelectedValue = "0";
        ddlScale.SelectedValue = "0";
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        //rdoPayrevision.Checked = true;
        ViewState["action"] = "add";
        txtAmount.Text = string.Empty;
        txtBasic.Text = string.Empty;
        txtAGP.Text = string.Empty;
        txtHRA.Text = string.Empty;
        rdbPayRevision.SelectedIndex = 0;
        txtPost.Text = string.Empty;
        txtGross.Text = string.Empty;
        txtNet.Text = string.Empty;
        
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDesignation, "payroll_SubDesig", "subdesigno", "subdesig", "subdesigno > 0 ", "subdesigno");
            objCommon.FillDropDownList(ddlScale, "payroll_scale", "scaleno", "scale", "scaleno > 0", "scaleno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object PRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PAY_REVISION/" + idno.ToString() + "/PAY_" + PRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
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
            string Command = "Pay Revision";
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