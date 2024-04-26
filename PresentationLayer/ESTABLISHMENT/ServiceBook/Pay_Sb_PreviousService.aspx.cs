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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_PreviousService : System.Web.UI.Page
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
        BindListViewPreService();
        //btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
        GetConfigForEditAndApprove();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_PreviousService.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PreviousService.aspx");
        }
    }

    private void BindListViewPreService()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllPreServiceDetailsOfEmployee(_idnoEmp);
            lvPrvService.DataSource = ds;
            lvPrvService.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvPrvService.FindControl("divFolder");
                Control ctrHead1 = lvPrvService.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvPrvService.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvPrvService.FindControl("divFolder");
                Control ctrHead1 = lvPrvService.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvPrvService.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = true;
                    ckattach.Visible = false;
                }
            }

            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvPrvService.FindControl("divFolder1");
                Control ctrHead1 = lvPrvService.FindControl("divBlob1");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvPrvService.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvPrvService.FindControl("divFolder1");
                Control ctrHead1 = lvPrvService.FindControl("divBlob1");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvPrvService.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                    ckBox.Visible = true;
                    ckattach.Visible = false;
                }
            }

            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvPrvService.FindControl("divFolder2");
                Control ctrHead1 = lvPrvService.FindControl("divBlob2");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvPrvService.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder2");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob2");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvPrvService.FindControl("divFolder2");
                Control ctrHead1 = lvPrvService.FindControl("divBlob2");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvPrvService.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder2");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob2");
                    ckBox.Visible = true;
                    ckattach.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.BindListViewPreService-> " + ex.Message + " " + ex.StackTrace);
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
            //  Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");


            if (txtPostName.Text == string.Empty || txtFromDate.Text == string.Empty || txtToDate.Text == string.Empty || txtExperience.Text == string.Empty || txtInstitute.Text == string.Empty || txtReasonForTerminationOfService.Text == string.Empty)
            {
                MessageBox("Please Enter All Mandatory fields.");
                return;
            }


            DateTime DtFrom, DtTo;
            DtFrom = Convert.ToDateTime(txtFromDate.Text);
            DtTo = Convert.ToDateTime(txtToDate.Text);
            if (DtTo < DtFrom)
            {
                MessageBox("To Date Should be Greater than  or equal to From Date");
                return;
            }
            else
            {
                ServiceBook objSevBook = new ServiceBook();
                objSevBook.IDNO = _idnoEmp;
                objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
                objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);


                if (txtReMarks.Text != string.Empty)
                {
                    objSevBook.REMARK = txtReMarks.Text;
                }
                else
                {
                    objSevBook.REMARK = string.Empty;
                }

                objSevBook.INST = txtInstitute.Text;
                objSevBook.POSTNAME = txtPostName.Text;
                objSevBook.EXPERIENCE = txtExperience.Text;
                objSevBook.TERMINATION = txtReasonForTerminationOfService.Text;
                objSevBook.OFFICER = txtDesignationOfAttestingOfficer.Text;

                // ADD ON 21-01-2021
                objSevBook.EXPERIENCETYPE = ddlexptype.SelectedValue;

                if (txtuniversity.Text != string.Empty)
                {
                    objSevBook.NAMEOFUNI = txtuniversity.Text;
                }
                else
                {
                    objSevBook.NAMEOFUNI = string.Empty;
                }

                objSevBook.NATUREOFWORK = ddlnaturework.SelectedValue;

                if (txtnaturework.Text != string.Empty)
                {
                    objSevBook.NatureOfWorkText = txtnaturework.Text;
                }
                else
                {
                    objSevBook.NatureOfWorkText = string.Empty;
                }


                if (txtDepartment.Text != string.Empty)
                {
                    objSevBook.Department = txtDepartment.Text;
                }
                else
                {
                    objSevBook.Department = string.Empty;
                }

                if (txtpayscale.Text != string.Empty)
                {
                    objSevBook.PAYSCALE = txtpayscale.Text;
                }
                else
                {
                    objSevBook.PAYSCALE = string.Empty;
                }

                if (txtsalary.Text != string.Empty)
                {
                    objSevBook.LASTSALARY = Convert.ToDecimal(txtsalary.Text);
                }
                else
                {
                    objSevBook.LASTSALARY = 0;
                }


                objSevBook.ADDRESS = txtAddress.Text.Trim();

                if (txtRefName.Text != string.Empty)
                {
                    objSevBook.NAME = txtRefName.Text;
                }
                else
                {
                    objSevBook.NAME = string.Empty;
                }

                if (txtDes.Text != string.Empty)
                {
                    objSevBook.DESIGNATION = txtDes.Text;
                }
                else
                {
                    objSevBook.DESIGNATION = string.Empty;
                }

                if (txtEmail.Text != string.Empty)
                {
                    objSevBook.EMAIL = txtEmail.Text;
                }
                else
                {
                    objSevBook.EMAIL = string.Empty;
                }

                if (txtMob.Text != string.Empty)
                {
                    objSevBook.MOBNO = txtMob.Text;
                }
                else
                {
                    objSevBook.MOBNO = string.Empty;
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
                        // string name = txtPostName.Text.Replace(" ", "");
                        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                        filename = IdNo + "_preservice_" + time + ext;
                        objSevBook.ATTACHMENTS = filename;
                        objSevBook.FILEPATH = "Blob Storage";

                        if (flupld.FileContent.Length <= 1024 * 10000)
                        {
                            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                            bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                            if (result == true)
                            {

                                int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_preservice_" + time, flupld);
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
                if (rdbStatus.SelectedValue == "0")
                {
                    objSevBook.UNIVERSITYAPPNO = txtApprovalno.Text;
                    if (txtDate.Text != string.Empty)
                    {
                        objSevBook.UNIAPPDT = Convert.ToDateTime(txtDate.Text);
                    }
                    else
                    {
                        objSevBook.UNIAPPDT = null;
                    }
                    objSevBook.UNIAPPSTATUS = "YES";

                    if (objSevBook.ISBLOB == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        string IdNo = _idnoEmp.ToString();
                        if (flupuniv.HasFile)
                        {
                            string contentType = contentType = flupuniv.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(flupuniv.PostedFile.FileName);
                            //HttpPostedFile file = flupld.PostedFile;
                            //filename = objSevBook.IDNO + "_familyinfo" + ext;
                            //string name = txtPostName.Text.Replace(" ", "");
                            string timeuni = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = IdNo + "_preserviceuni_" + timeuni + ext;
                            objSevBook.UNIVERSITYATACHMENT = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (flupuniv.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_preserviceuni_" + timeuni, flupuniv);
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
                            if (ViewState["universityattachment"] != null)
                            {
                                objSevBook.UNIVERSITYATACHMENT = ViewState["universityattachment"].ToString();
                            }
                            else
                            {
                                objSevBook.UNIVERSITYATACHMENT = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        if (flupuniv.HasFile)
                        {
                            objSevBook.UNIVERSITYATACHMENT = Convert.ToString(flupuniv.PostedFile.FileName.ToString());
                        }
                        else
                        {
                            if (ViewState["universityattachment"] != null)
                            {
                                objSevBook.UNIVERSITYATACHMENT = ViewState["universityattachment"].ToString();
                            }
                            else
                            {
                                objSevBook.UNIVERSITYATACHMENT = string.Empty;
                            }
                        }
                    }

                }
                else
                {
                    objSevBook.UNIVERSITYAPPNO = string.Empty;
                    objSevBook.UNIAPPDT = null;
                    objSevBook.UNIVERSITYATACHMENT = string.Empty;
                    objSevBook.UNIAPPSTATUS = "NO";
                }


                //
                if (rdbTeacher.SelectedValue == "0")
                {
                    objSevBook.PGAPPNO = txtteachno.Text;
                    if (txtappdt.Text != string.Empty)
                    {
                        objSevBook.PGTAPPDT = Convert.ToDateTime(txtappdt.Text);
                    }
                    else
                    {
                        objSevBook.PGTAPPDT = null;
                    }
                   
                    objSevBook.PGTAPPSTATUS = "YES";

                    if (objSevBook.ISBLOB == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        string IdNo = _idnoEmp.ToString();
                        if (flupteach.HasFile)
                        {
                            string contentType = contentType = flupteach.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(flupteach.PostedFile.FileName);
                            //HttpPostedFile file = flupld.PostedFile;
                            //filename = objSevBook.IDNO + "_familyinfo" + ext;
                            // string name = txtPostName.Text.Replace(" ", "");
                            string timenew = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = IdNo + "_preserviceteach_" + timenew + ext;
                            objSevBook.PGTATTACHMENT = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (flupteach.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_preserviceteach_" + timenew, flupteach);
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
                            if (ViewState["pgtattachment"] != null)
                            {
                                objSevBook.PGTATTACHMENT = ViewState["pgtattachment"].ToString();
                            }
                            else
                            {
                                objSevBook.PGTATTACHMENT = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        if (flupteach.HasFile)
                        {
                            objSevBook.PGTATTACHMENT = Convert.ToString(flupteach.PostedFile.FileName.ToString());
                        }
                        else
                        {
                            if (ViewState["pgtattachment"] != null)
                            {
                                objSevBook.PGTATTACHMENT = ViewState["pgtattachment"].ToString();
                            }
                            else
                            {
                                objSevBook.PGTATTACHMENT = string.Empty;
                            }
                        }
                    }
                }

                else
                {
                    objSevBook.PGAPPNO = string.Empty;
                    objSevBook.PGTAPPDT = null;
                    objSevBook.PGTATTACHMENT = string.Empty;
                    objSevBook.PGTAPPSTATUS = "NO";
                }

                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //Add New Help
                        CustomStatus cs = (CustomStatus)objServiceBook.AddPreService(objSevBook);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (flupld.HasFile)
                                {
                                    objServiceBook.upload_new_files("PREVIOUS_SERVICE", _idnoEmp, "PSNO", "PAYROLL_SB_PRESERVICE", "PRE_", flupld);
                                }
                                if (flupuniv.HasFile)
                                {
                                    objServiceBook.upload_new_files("PREVIOUS_SERVICE", _idnoEmp, "PSNO", "PAYROLL_SB_PRESERVICE", "PRE_", flupuniv);
                                }
                                if (flupteach.HasFile)
                                {
                                    objServiceBook.upload_new_files("PREVIOUS_SERVICE", _idnoEmp, "PSNO", "PAYROLL_SB_PRESERVICE", "PRE_", flupteach);
                                }
                            }
                            this.Clear();
                            this.BindListViewPreService();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                            MessageBox("Record Saved Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordFound))
                        {
                            MessageBox("Record Already Exist");
                            this.Clear();
                        }
                    }
                    else
                    {
                        //Edit
                        if (ViewState["psNo"] != null)
                        {
                            objSevBook.PSNO = Convert.ToInt32(ViewState["psNo"].ToString());
                            CustomStatus cs = (CustomStatus)objServiceBook.UpdatePreService(objSevBook);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                if (objSevBook.ISBLOB == 0)
                                {
                                    if (flupld.HasFile)
                                    {
                                        objServiceBook.update_upload("PREVIOUS_SERVICE", objSevBook.PSNO, ViewState["attachment"].ToString(), _idnoEmp, "PRE_", flupld);
                                    }
                                    if (flupuniv.HasFile)
                                    {
                                        objServiceBook.update_upload("PREVIOUS_SERVICE", objSevBook.PSNO, ViewState["universityattachment"].ToString(), _idnoEmp, "PRE_", flupuniv);
                                    }
                                    if (flupteach.HasFile)
                                    {
                                        objServiceBook.update_upload("PREVIOUS_SERVICE", objSevBook.PSNO, ViewState["pgtattachment"].ToString(), _idnoEmp, "PRE_", flupteach);
                                    }
                                }
                                //objServiceBook.update_upload("PREVIOUS_SERVICE", objSevBook.PSNO, ViewState["attachment"].ToString(), _idnoEmp, "PRE_", flupld);
                                ViewState["action"] = "add";
                                this.Clear();
                                this.BindListViewPreService();
                                //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                                MessageBox("Record Updated Successfully");
                            }
                            else
                            {
                                ViewState["action"] = "add";
                                MessageBox("Record Already Exist");
                                this.Clear();
                                this.BindListViewPreService();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int psNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(psNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DateTime Test;
        DateTime DtFrom, DtTo;
        //DtFrom = Convert.ToDateTime(txtFromDate.Text);
        //DtTo = Convert.ToDateTime(txtToDate.Text);
        if (DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            DtFrom = Convert.ToDateTime(txtFromDate.Text);
            DtTo = Convert.ToDateTime(txtToDate.Text);
            if (DtTo < DtFrom)
            {
                MessageBox("To Date Should be Greater than  or equal to From Date");
                txtToDate.Text = string.Empty;
                return;
            }
            else
            {
                if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
                {
                    DtFrom = Convert.ToDateTime(txtFromDate.Text);
                    DtTo = Convert.ToDateTime(txtToDate.Text);
                    DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtExperience.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();

                    }
                }
            }
        }
        else
        {
            txtToDate.Text = string.Empty;
        }

    }
    //protected void txtFromDate_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
    //    {
    //        DateTime DtFrom = Convert.ToDateTime(txtFromDate.Text);
    //        DateTime DtTo = Convert.ToDateTime(txtToDate.Text);
    //        DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtExperience.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();
    //            //txtExperience.Text = ds.Tables[0].Rows[0]["Experience"].ToString();

    //        }
    //    }
    //}
    private void ShowDetails(int psNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePreServiceDetailsOfEmployee(psNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["psNo"] = psNo.ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                txtDesignationOfAttestingOfficer.Text = ds.Tables[0].Rows[0]["officer"].ToString();
                txtInstitute.Text = ds.Tables[0].Rows[0]["inst"].ToString();
                txtPostName.Text = ds.Tables[0].Rows[0]["POST"].ToString();
                txtReasonForTerminationOfService.Text = ds.Tables[0].Rows[0]["termination"].ToString();
                txtExperience.Text = ds.Tables[0].Rows[0]["EXPERIENCE"].ToString();

                ddlexptype.SelectedValue = ds.Tables[0].Rows[0]["EXPERIENCETYPE"].ToString();
                txtuniversity.Text = ds.Tables[0].Rows[0]["NAMEOFUNI"].ToString();
                ddlnaturework.SelectedValue = ds.Tables[0].Rows[0]["NATUREOFWORK"].ToString();

                if (Convert.ToString(ds.Tables[0].Rows[0]["Natureofworktext"].ToString()) != string.Empty)
                {
                    txtnaturework.Text = ds.Tables[0].Rows[0]["Natureofworktext"].ToString();
                }
                else
                {
                    txtnaturework.Text = string.Empty;
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["Department"].ToString()) != string.Empty)
                {
                    txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                else
                {
                    txtDepartment.Text = string.Empty;
                }
                if (Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS"].ToString()) != string.Empty)
                {
                    txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                }
                else
                {
                    txtAddress.Text = string.Empty;
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["PAYSCALE"].ToString()) != string.Empty)
                {
                    txtpayscale.Text = ds.Tables[0].Rows[0]["PAYSCALE"].ToString();
                }
                else
                {
                    txtpayscale.Text = string.Empty;
                }
                txtsalary.Text = ds.Tables[0].Rows[0]["LastSalary"].ToString();


                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                ViewState["universityattachment"] = ds.Tables[0].Rows[0]["UNIVERSITYATACHMENT"].ToString();
                ViewState["pgtattachment"] = ds.Tables[0].Rows[0]["PGTATTACHMENT"].ToString();

                string uniappstatus = ds.Tables[0].Rows[0]["UNIAPPSTATUS"].ToString();
                if (uniappstatus == "YES")
                {
                    rdbStatus.SelectedValue = "0";
                    //appstatus.Visible = true;
                    divapp.Visible = true;
                    divdate.Visible = true;
                    divdoc.Visible = true;
                    txtApprovalno.Text = ds.Tables[0].Rows[0]["UNIVERSITYAPPNO"].ToString();
                    txtDate.Text = ds.Tables[0].Rows[0]["UNIAPPDT"].ToString();
                }
                else
                {
                    rdbStatus.SelectedValue = "1";
                    // appstatus.Visible = false;
                    divapp.Visible = false;
                    divdate.Visible = false;
                    divdoc.Visible = false;
                }

                string pgappstatus = ds.Tables[0].Rows[0]["PGTAPPSTATUS"].ToString();
                if (pgappstatus == "YES")
                {
                    rdbTeacher.SelectedValue = "0";
                    // divpgteacher.Visible = true;
                    divpg.Visible = true;
                    divpgdt.Visible = true;
                    divpgdoc.Visible = true;
                    txtteachno.Text = ds.Tables[0].Rows[0]["PGAPPNO"].ToString();
                    txtappdt.Text = ds.Tables[0].Rows[0]["PGTAPPDT"].ToString();
                }
                else
                {
                    rdbTeacher.SelectedValue = "1";
                    // divpgteacher.Visible = false;
                    divpg.Visible = false;
                    divpgdt.Visible = false;
                    divpgdoc.Visible = false;
                }

                txtRefName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtDes.Text = ds.Tables[0].Rows[0]["DESIGNATION"].ToString();
                txtMob.Text = ds.Tables[0].Rows[0]["MOBNO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();

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
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int psNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_PRESERVICE", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "PSNO=" + psNo, "");
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
                CustomStatus cs = (CustomStatus)objServiceBook.DeletePreService(psNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    Clear();
                    BindListViewPreService();
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtReMarks.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtExperience.Text = string.Empty;
        txtPostName.Text = string.Empty;
        txtDesignationOfAttestingOfficer.Text = string.Empty;
        txtInstitute.Text = string.Empty;
        txtReasonForTerminationOfService.Text = string.Empty;
        ddlexptype.SelectedIndex = 0;
        txtuniversity.Text = string.Empty;
        ddlnaturework.SelectedIndex = 0;
        txtDepartment.Text = txtnaturework.Text = string.Empty;
        txtpayscale.Text = string.Empty;
        txtsalary.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtappdt.Text = string.Empty;
        txtApprovalno.Text = string.Empty;
        txtDate.Text = string.Empty;
        rdbStatus.SelectedIndex = 1;
        rdbTeacher.SelectedIndex = 1;
        txtteachno.Text = string.Empty;
        txtRefName.Text = string.Empty;
        txtDes.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtMob.Text = string.Empty;


        ViewState["action"] = "add";
        ViewState["attachment"] = null;
        divpg.Visible = false;
        divpgdt.Visible = false;
        divpgdoc.Visible = false;

        divapp.Visible = false;
        divdate.Visible = false;
        divdoc.Visible = false;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    public string GetFileNamePath(object filename, object PSNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PREVIOUS_SERVICE/" + idno.ToString() + "/PRE_" + PSNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    protected void rdbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbStatus.SelectedValue == "0")
        {
            // appstatus.Visible = true;
            divapp.Visible = true;
            divdate.Visible = true;
            divdoc.Visible = true;
        }
        else
        {
            //appstatus.Visible = false;
            divapp.Visible = false;
            divdate.Visible = false;
            divdoc.Visible = false;
        }
    }

    protected void rdbTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbTeacher.SelectedValue == "0")
        {
            // divpgteacher.Visible = true;
            divpg.Visible = true;
            divpgdt.Visible = true;
            divpgdoc.Visible = true;
        }
        else
        {
            // divpgteacher.Visible = false;
            divpg.Visible = false;
            divpgdt.Visible = false;
            divpgdoc.Visible = false;
        }
    }


    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                txtToDate.Text = string.Empty;
                txtExperience.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
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

    protected void imgbtnPreview2_Click(object sender, ImageClickEventArgs e)
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
            string Command = "Previous Experience";
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