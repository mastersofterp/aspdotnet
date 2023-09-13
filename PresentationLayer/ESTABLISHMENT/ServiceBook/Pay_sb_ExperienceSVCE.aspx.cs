//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_FamilyParticulars.ascx                                                
// CREATION DATE : 23-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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

public partial class ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE : System.Web.UI.Page
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

            FillDropDowns();


        }
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        BlobDetails();
        BindListViewExperiences();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
    }

    private void FillDropDowns()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            //ddlDepartment.Items.Insert(0, "--Please Select--");
            //ddlDepartment.SelectedIndex = 0;

            objCommon.FillDropDownList(ddlDesignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIGNO");
            //ddlDesignation.Items.Insert(0, "Please Select");
            //ddlDesignation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.FillDropDowns ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearControls()
    {
        FillDropDowns();
        ddlDepartment.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        ddlNatOfAppointment.SelectedIndex = 0;
        chkIsCurrent.Checked = false;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtEndDate.Enabled = true;
        txtDuration.Enabled = true;
        flupld.Dispose();
        ViewState["action"] = "add";
        ViewState["attachment"] = null;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void ShowDetails(int SVCNO)
    {
        try
        {
            DataSet ds = objServiceBook.GetSingleExperienceDetails(SVCNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // ViewState["ID"] = ID;
                ViewState["SVCNO"] = SVCNO.ToString();
                objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department"].ToString();
                // ddlDepartment.SelectedItem.Text = ds.Tables[0].Rows[0]["Department"].ToString();

                objCommon.FillDropDownList(ddlDesignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIGNO");
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation"].ToString();
                //ddlDesignation.SelectedItem.Text = ds.Tables[0].Rows[0]["Designation"].ToString();

                //txtNatOfApp.Text = ds.Tables[0].Rows[0]["NatureofAppointment"].ToString();
                ddlNatOfAppointment.SelectedValue = ds.Tables[0].Rows[0]["NatureofAppointment"].ToString();
                chkIsCurrent.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Iscurrent"]);
                if (chkIsCurrent.Checked == true)
                {
                    chkIsCurrent.Checked = true;
                    txtEndDate.Enabled = false;
                    txtDuration.Enabled = false;
                }
                else
                {
                    chkIsCurrent.Checked = false;
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Enabled = true;
                    txtDuration.Enabled = true;
                }
                txtStartDate.Text = ds.Tables[0].Rows[0]["START_DATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0]["Duration"].ToString();

                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewExperiences()
    {
        try
        {
            int ID = 0;
            DataSet ds = objServiceBook.GetAllExperiencesDetails(_idnoEmp);
            lvExperiences.DataSource = ds;
            lvExperiences.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvExperiences.FindControl("divFolder");
                Control ctrHead1 = lvExperiences.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvExperiences.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvExperiences.FindControl("divFolder");
                Control ctrHead1 = lvExperiences.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvExperiences.Items)
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
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.BindListViewFamilyInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private int CheckDuplicate(Pay_sb_Experiences_Entity objExpEntity)
    //{
    //    int i = 0;
    //    try
    //    {
    //        i = Convert.ToInt32(objServiceBook.CheckDuplicate(objExpEntity));
    //        if (i > 0)
    //        {
    //            MessageBox("Duplicate Record found");
    //            i = 1;
    //        }
    //        //CustomStatus cs = (CustomStatus)objServiceBook.CheckDuplicate(objExpEntity);
    //        //if (cs.Equals(CustomStatus.DuplicateRecord))
    //        //{
    //        //    MessageBox("Duplicate Record found");
    //        //    //ClearControls();
    //        //    BindListViewExperiences();
    //        //    i = 1;
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.CheckDuplicate-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    return i;
    //}

    //private bool ValidateControls()
    //{
    //    bool check = true;
    //    try
    //    {
    //        if (ddlDepartment.SelectedIndex == 0 || ddlDesignation.SelectedIndex == 0) { check = check && false; }
    //        if (txtStartDate.Text == string.Empty || txtEndDate.Text == string.Empty) { check = check && false; }
    //        if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text)) { check = check && false; }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.ValidateControls-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    return check;
    //}
    public bool CheckFileTypeAndSize()
    {
        try
        {
            //if (fu.HasFile)
            if (flupld.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flupld.FileName);
                HttpPostedFile file = flupld.PostedFile;
                if ((file != null) && (file.ContentLength > 0))
                {
                    int iFileSize = file.ContentLength;
                    if (iFileSize > 5242880)  // 40kb 5120
                    {
                        MessageBox("Please Select valid document file(upto 5 MB)");
                        return false;
                    }
                }

                string[] ValidExt = { ".PDF", ".DOC", ".DOCX", ".Pdf", ".pdf", ".doc", ".docx", ".JPEG", ".JPG", ".jpg", ".jpeg" };
                Boolean valid = false;
                foreach (string vext in ValidExt)
                {
                    if (ext.ToUpper() == vext)
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                {
                    MessageBox("Please Select valid document file(e.g. pdf,jpg,doc)");
                    return false;
                }
                else
                {

                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ValidateControls())
            //{

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
            bool flag = CheckFileTypeAndSize();
            if (flag == false)
            {
                return;
            }
            ServiceBook objSevBook = new ServiceBook();

            objSevBook.IDNO = _idnoEmp;
            objSevBook.DepID = Convert.ToInt32(ddlDepartment.SelectedValue);
            objSevBook.DesID = Convert.ToInt32(ddlDesignation.SelectedValue);

            //objSevBook.NatOfApp = Convert.ToInt32(ddlNatOfAppointment.SelectedValue);
            objSevBook.NatOfApp = ddlNatOfAppointment.SelectedValue;

            //objSevBook.NatOfApp = txtNatOfApp.Text == "" ? "" : txtNatOfApp.Text.ToString();
            objSevBook.IsCurrent = chkIsCurrent.Checked == true ? 1 : 0;
            objSevBook.StartDate = Convert.ToDateTime(txtStartDate.Text);
            //objSevBook.EndDate = Convert.ToDateTime(txtEndDate.Text);
            if (txtEndDate.Text.Trim() == string.Empty)
            {
                objSevBook.EndDate = Convert.ToDateTime("9999/12/31"); // DateTime.MinValue;
            }
            else
            {
                objSevBook.EndDate = Convert.ToDateTime(txtEndDate.Text);
            }
            objSevBook.Duration = txtDuration.Text == "" ? "" : txtDuration.Text.ToString();
            // objSevBook.Attachments = flupld.HasFile == true ? Convert.ToString(flupld.PostedFile.FileName.ToString()) : ViewState["attachmant"].ToString() == "" ? "" : ViewState["attachmant"].ToString();
            objSevBook.CollegeCode = Session["colcode"] == null ? 0 : Convert.ToInt32(Session["colcode"].ToString());

            // if (Convert.ToInt32(CheckDuplicate(objExpEntity)) == 1) { return; };

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
                    //string name = ddlDesignation.SelectedItem.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_experience_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_experience_" + time, flupld);
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

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //objExpEntity.Action = "I";
                    CustomStatus cs = (CustomStatus)objServiceBook.AddExperiencesDetails(objSevBook);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (flupld.HasFile)
                            {
                                objServiceBook.upload_new_files("EXPERIENCE", _idnoEmp, "SVCNO", "Payroll_sb_expsvce", "SCVE_", flupld);
                            }
                        }
                        MessageBox("Record Saved Successfully");
                        ClearControls();
                        BindListViewExperiences();
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exits ");
                        //this.Clear();
                    }
                }
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    //   objExpEntity.Action = "E";
                    // objExpEntity.ID = Convert.ToInt32(ViewState["ID"].ToString());   

                    objSevBook.SVCNO = Convert.ToInt32(ViewState["SVCNO"].ToString());
                    CustomStatus cs = (CustomStatus)objServiceBook.UpdateSVCEExp(objSevBook);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        // objServiceBook.update_upload("EXPERIENCE", objSevBook.SVCNO, ViewState["attachment"].ToString(), _idnoEmp, "SCVE_", flupld);
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.update_upload("EXPERIENCE", objSevBook.SVCNO, ViewState["attachment"].ToString(), _idnoEmp, "SCVE_", flupld);
                        }
                        MessageBox("Record Updated Successfully");
                        ClearControls();
                        BindListViewExperiences();
                        ViewState["action"] = "add";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SVCNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(SVCNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int SVCNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("Payroll_sb_expsvce", "*", "", "SVCNO=" + SVCNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteExperiencesByID(SVCNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    ClearControls();
                    BindListViewExperiences();
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtStartDate.Text.ToString() != string.Empty && txtStartDate.Text.ToString() != "__/__/____" && txtEndDate.Text.ToString() != string.Empty && txtEndDate.Text.ToString() != "__/__/____")
        //{
        //    DateTime fromDate = Convert.ToDateTime(txtStartDate.Text.ToString());

        //    DateTime toDate = Convert.ToDateTime(txtEndDate.Text.ToString());

        //    if (toDate < fromDate)
        //    {
        //        MessageBox("To Date Should Be Greater Than Or Equals To From Date");
        //        txtEndDate.Text = string.Empty;     
        //        return;
        //    }
        //}

        // Added  By Shrikant Bharne To Get Duration  according to From date To Date 27-11-2019

        //if (txtStartDate.Text != "" && txtEndDate.Text != "")
        //{
        //    //Storing input Dates  
        //    DateTime FromYear = Convert.ToDateTime(txtStartDate.Text);
        //    DateTime ToYear = Convert.ToDateTime(txtEndDate.Text);

        //    //Creating object of TimeSpan Class  
        //    TimeSpan objTimeSpan = ToYear - FromYear;
        //    //years  
        //    int Years = ToYear.Year - FromYear.Year;
        //    //months  
        //    int month = ToYear.Month - FromYear.Month;
        //    //TotalDays  
        //    double Days = Convert.ToDouble(objTimeSpan.TotalDays);
        //    //Total Months  
        //    int TotalMonths = (Years * 12) + month;
        //    //Total Hours  
        //    double TotalHours = objTimeSpan.TotalHours;
        //    //Total Minutes  
        //    double TotalMinutes = objTimeSpan.TotalMinutes;
        //    //Total Seconds  
        //    double TotalSeconds = objTimeSpan.TotalSeconds;
        //    //Total Mile Seconds  
        //    double TotalMileSeconds = objTimeSpan.TotalMilliseconds;
        //    //Assining values to td tags  
        //    txtDuration.Text = Years + "  Year  " + month + "  Months";
        //    //tdMonths.InnerText = Convert.ToString(TotalMonths);
        //    //tdDays.InnerText = Convert.ToString(Days);
        //    //tdHrs.InnerText = Convert.ToString(TotalHours);
        //    //tdminuts.InnerText = Convert.ToString(TotalMinutes);
        //    //tdseconds.InnerText = Convert.ToString(TotalSeconds);
        //    //tdmileSec.InnerText = Convert.ToString(TotalMileSeconds);
        //    //tblResults.Visible = true;
        //}   

    }

    public string GetFileNamePath(object filename, object SVCNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/EXPERIENCE/" + idno.ToString() + "/SCVE_" + SVCNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    protected void chkIsCurrent_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkIsCurrent.Checked)
        {
            txtEndDate.Text = string.Empty;
            txtEndDate.Enabled = true;
            txtDuration.Enabled = true;
        }
        else
        {
            txtEndDate.Enabled = false;
            txtDuration.Enabled = false;
        }
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        txtEndDate.Text = string.Empty;
    }

    protected void txtDuration_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtEndDate_TextChanged1(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo;
        DtFrom = Convert.ToDateTime(txtStartDate.Text);
        DtTo = Convert.ToDateTime(txtEndDate.Text);
        if (DtTo < DtFrom)
        {
            MessageBox("To Date Should be Greater than  or equal to From Date");
            txtEndDate.Text = string.Empty;
            return;
        }
        else
        {
            if (txtEndDate.Text != string.Empty && txtEndDate.Text != "__/__/____" && txtStartDate.Text != string.Empty && txtEndDate.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtStartDate.Text);
                DtTo = Convert.ToDateTime(txtEndDate.Text);
                DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDuration.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();

                }
            }
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