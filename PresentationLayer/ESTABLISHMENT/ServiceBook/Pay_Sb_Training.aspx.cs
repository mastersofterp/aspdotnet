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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Training : System.Web.UI.Page
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
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewTraining();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Training.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Training.aspx");
        }
    }

    private void BindListViewTraining()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllTrainingDetailsOfEmployee(_idnoEmp);
            lvTraning.DataSource = ds;
            lvTraning.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvTraning.FindControl("divFolder");
                Control ctrHead1 = lvTraning.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvTraning.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvTraning.FindControl("divFolder");
                Control ctrHead1 = lvTraning.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvTraning.Items)
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.BindListViewTraining-> " + ex.Message + " " + ex.StackTrace);
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
            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.COURSE = txtCourse.Text;
            objSevBook.INST = txtInstitute.Text;
            objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
            objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);

            if (ddlProgramLevel.SelectedIndex > 0)
            {
                objSevBook.PROGRAM_LEVEL = Convert.ToInt32(ddlProgramLevel.SelectedValue);
            }
            else
            {
                objSevBook.PROGRAM_LEVEL = 0;
            }


            if (ddlProgramType.SelectedIndex > 0)
            {
                objSevBook.PROGRAM_TYPE = Convert.ToInt32(ddlProgramType.SelectedValue);
            }
            else
            {
                objSevBook.PROGRAM_TYPE = 0;
            }


            if (ddlParticipationType.SelectedIndex > 0)
            {
                objSevBook.PARTICIPATION_TYPE = Convert.ToInt32(ddlParticipationType.SelectedValue);
            }
            else
            {
                objSevBook.PARTICIPATION_TYPE = 0;
            }

            if (txtPresentation.Text != string.Empty)
            {
                objSevBook.PRESENTATION_DETAILS = txtPresentation.Text;
            }
            else
            {
                objSevBook.PRESENTATION_DETAILS = string.Empty;
            }

            objSevBook.DURATION = txtDuration.Text;
            objSevBook.SPONSORED_BY = txtSponsoredBy.Text;

            objSevBook.LOCATION = txtinstituteadd.Text;

            if (txtsponsoredamt.Text != string.Empty)
            {
                objSevBook.SPONSORED_AMOUNT = Convert.ToDecimal(txtsponsoredamt.Text);
            }
            else
            {
                objSevBook.SPONSORED_AMOUNT = 0;
            }

            if (txtCostInvolved.Text != string.Empty)
            {
                objSevBook.COST_INVOLVED = Convert.ToDecimal(txtCostInvolved.Text);
            }
            else
            {
                objSevBook.COST_INVOLVED = 0;
            }

            if (rblEligible.SelectedValue == "0")
            {
                objSevBook.EligibleCandidate = true;
            }
            else
            {
                objSevBook.EligibleCandidate = false;
            }

            if (rblcriteria.SelectedValue == "0")
            {
                objSevBook.fulfilservice = true;
            }
            else
            {
                objSevBook.fulfilservice = false;
            }

            objSevBook.CertificationType = TxtCertification.Text;

            if (ddltraining.SelectedIndex > 0)
            {
                objSevBook.ThemeOfTraining = Convert.ToInt32(ddltraining.SelectedValue);
            }
            else
            {
                objSevBook.ThemeOfTraining = 0;
            }



            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                objCommon.DisplayMessage("To Date Should be Greater than  or equal to From Date", this.Page);
                txtToDate.Text = string.Empty;
                txtToDate.Focus();
                return;
            }

            if (txtReMarks.Text != string.Empty)
            {
                objSevBook.REMARK = txtReMarks.Text;
            }
            else
            {
                objSevBook.REMARK = string.Empty;
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
                    string name = txtCourse.Text.Replace(" ", "");
                    filename = IdNo + "_trainingattended_" + name + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_trainingattended_" + name, flupld);
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

            if (rdbTMode.SelectedValue == "0")
            {
                objSevBook.MODE = "Online";
            }
            else
            {
                objSevBook.MODE = "Offline";
            }

            if (txtAcadYear.Text != string.Empty)
            {
                objSevBook.YEAR = Convert.ToInt32(txtAcadYear.Text);
            }
            else
            {
                objSevBook.YEAR = null;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                #region Checking duplicate rows
                //int chk = Convert.ToInt32(objCommon.LookUp("PAYROLL_SB_TRAINING", "COUNT(*)", " COURSE='" + txtCourse.Text + "' AND INST='" + txtInstitute.Text + "'AND FDT='" + objSevBook.FDT.ToString() + "' AND TDT='" + objSevBook.TDT.ToString() + "' AND IDNO=" + _idnoEmp.ToString()));
                //if (chk > 0)
                //{
                //    MessageBox("Record Already Exist");
                //    return;
                //}
                #endregion

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddTraining(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("TRAINING", _idnoEmp, "TNO", "PAYROLL_SB_TRAINING", "TRA_", flupld);
                        }
                        this.Clear();
                        this.BindListViewTraining();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    else
                    {
                        this.Clear();
                        this.BindListViewTraining();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Already Exist");
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["tNo"] != null)
                    {
                        objSevBook.TNO = Convert.ToInt32(ViewState["tNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateTraining(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("TRAINING", objSevBook.TNO, ViewState["attachment"].ToString(), _idnoEmp, "TRA_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewTraining();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                        else
                        {
                            this.Clear();
                            this.BindListViewTraining();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int tNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(tNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int tNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleTrainingDetailsOfEmployee(tNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["tNo"] = tNo.ToString();
                txtCourse.Text = ds.Tables[0].Rows[0]["course"].ToString();
                txtInstitute.Text = ds.Tables[0].Rows[0]["inst"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

                txtinstituteadd.Text = ds.Tables[0].Rows[0]["INSTLOCATION"].ToString();
                txtsponsoredamt.Text = ds.Tables[0].Rows[0]["SPONSORED_AMOUNT"].ToString();

                ddlProgramLevel.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM_LEVEL"].ToString();
                ddlProgramType.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM_TYPE"].ToString();
                ddlParticipationType.SelectedValue = ds.Tables[0].Rows[0]["PARTICIPATION_TYPE"].ToString();
                txtPresentation.Text = ds.Tables[0].Rows[0]["PRESENTATION_DETAILS"].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();
                txtSponsoredBy.Text = ds.Tables[0].Rows[0]["SPONSORED_BY"].ToString();
                txtCostInvolved.Text = ds.Tables[0].Rows[0]["COST_INVOLVED"].ToString();
                TxtCertification.Text = ds.Tables[0].Rows[0]["Certificationtype"].ToString();
                ddltraining.SelectedValue = ds.Tables[0].Rows[0]["Theme_Of_Training"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["ELIGIBLECANDIDATE"].ToString().ToString()) == true)
                {
                    //chkEligible.Checked = true;
                    rblEligible.SelectedValue = "0";
                }
                else
                {
                    //chkEligible.Checked = false;
                    rblEligible.SelectedValue = "1";
                }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["SERVICECRITERIA"].ToString().ToString()) == true)
                {
                    //chkcriteria.Checked = true;
                    rblcriteria.SelectedValue = "0";
                }
                else
                {
                    //chkcriteria.Checked = false;
                    rblcriteria.SelectedValue = "1";
                }

                string mode = ds.Tables[0].Rows[0]["MODE"].ToString();
                if (mode == "Online")
                {
                    rdbTMode.SelectedValue = "0";
                }
                else
                {
                    rdbTMode.SelectedValue = "1";
                }
                txtAcadYear.Text = ds.Tables[0].Rows[0]["Acad_Year"].ToString();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int tNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_TRAINING", "*", "", "TNO=" + tNo, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteTraining(tNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewTraining();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtReMarks.Text = string.Empty;
        txtInstitute.Text = string.Empty;
        txtCourse.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;

        rblEligible.SelectedValue = "0";
        rblcriteria.SelectedValue = "0";

        txtDuration.Text = string.Empty;
        txtSponsoredBy.Text = string.Empty;
        txtCostInvolved.Text = string.Empty;
        ddlProgramLevel.SelectedIndex = 0;
        ddlProgramType.SelectedIndex = 0;
        ddlParticipationType.SelectedIndex = 0;
        txtPresentation.Text = string.Empty;
        txtsponsoredamt.Text = string.Empty;
        txtinstituteadd.Text = string.Empty;
        ddltraining.SelectedIndex = 0;
        TxtCertification.Text = string.Empty;
        rdbTMode.SelectedIndex = 0;
        txtAcadYear.Text = string.Empty;

        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object TNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/TRAINING/" + idno.ToString() + "/TRA_" + TNO + "." + extension[1].ToString().Trim());
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
}