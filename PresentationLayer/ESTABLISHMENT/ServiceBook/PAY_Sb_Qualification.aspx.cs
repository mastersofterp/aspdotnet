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
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class ESTABLISHMENT_ServiceBook_PAY_Sb_Qualification : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    BlobController objBlob = new BlobController();

    public int _idnoEmp; public int _idnoCollege;


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

        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);

        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewQualification();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PAY_Qualification.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PAY_Qualification.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {
            //select QUALILEVELNO,QUALILEVELNAME from PAYROLL_QUALILEVEL
            objCommon.FillDropDownList(ddlLevel, "PAYROLL_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO > 0", "QUALILEVELNAME");
            objCommon.FillDropDownList(ddlunitype, "PAYROLL_UNIVERSITY", "UNIVERSITYNO", "UNIVERSITY", "UNIVERSITYNO > 0 AND ISNULL(ACTIVESTATUS,'') =" + "'Active'", "UNIVERSITYNO");
             
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Qualification.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void BindListViewQualification()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllQualificationDetailsOfEmployee(_idnoEmp);
            lvQuali.DataSource = ds;
            lvQuali.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvQuali.FindControl("divFolder");
                Control ctrHead1 = lvQuali.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvQuali.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvQuali.FindControl("divFolder");
                Control ctrHead1 = lvQuali.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvQuali.Items)
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
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.BindListViewQualification-> " + ex.Message + " " + ex.StackTrace);
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

            objSevBook.EXAMNAME = txtExam.Text;
            objSevBook.INST = txtInstituteName.Text;
            objSevBook.UNIVERSITY_NAME = txtUniversity.Text;
            objSevBook.LOCATION = txtLocation.Text;
            objSevBook.PASSYEAR = txtYearOfPassing.Text;

            objSevBook.QMONTH = Convert.ToInt32(ddlMonth.SelectedValue);


            objSevBook.QUALINO = Convert.ToInt32(ddlQualification.SelectedValue);
            objSevBook.REG_NAME = txtRegName.Text;
            objSevBook.REG_DATE = txtDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDate.Text.Trim());

            objSevBook.REGNO = txtRegNo.Text;

            objSevBook.LNO = Convert.ToInt32(ddlLevel.SelectedValue);

            objSevBook.SPECI = txtSpecialization.Text;


            if (ddlunitype.SelectedIndex > 0)
            {
                objSevBook.UNITYPE = Convert.ToInt32(ddlunitype.SelectedValue);
            }
            else
            {
                objSevBook.UNITYPE = 0;
            }
            if (ddlinstitype.SelectedIndex > 0)
            {
                objSevBook.INITYPE = Convert.ToInt32(ddlinstitype.SelectedValue);
            }
            else
            {
                objSevBook.INITYPE = 0;
            }
            if (txtgrade.Text != string.Empty)
            {
                objSevBook.Grade = txtgrade.Text;
            }
            else
            {
                objSevBook.Grade = string.Empty;
            }
            if (txtpercent.Text != string.Empty)
            {
                objSevBook.Percentage = Convert.ToDecimal(txtpercent.Text);
            }
            else
            {
                objSevBook.Percentage = 0;
            }

            if (txtCGPA.Text != string.Empty)
            {
                objSevBook.CGPA = Convert.ToDecimal(txtCGPA.Text);
            }
            else
            {
                objSevBook.CGPA = 0;
            }

            if (chkhighest.Checked == true)
            {
                objSevBook.HIGHTESTQUL = true;

            }
            else
            {
                objSevBook.HIGHTESTQUL = false;
            }
            //objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
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
                    //string name = ddlQualification.SelectedItem.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_qualification_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_qualification_" + time, flupld);
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

            // objSevBook.ENTRYDATE = Convert.ToDateTime(txtDate.Text.Trim());



            //if (rblCategory.SelectedValue == "1")
            //{
            //    objSevBook.QSTATUS = "UG";
            //}
            //else if (rblCategory.SelectedValue == "2")
            //{
            //    objSevBook.QSTATUS = "PG";
            //}
            //  if (!(Session["colcode"].ToString()==null))objSevBook.COLLEGE_CODE = Session["colcode"].ToString();


            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            objSevBook.IDCARDNO = txtIDCardNo.Text;
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddQualification(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("QUALIFICATION", _idnoEmp, "QNO", "PAYROLL_SB_QUALI", "QUA_", flupld);
                        }
                        this.Clear();
                        this.BindListViewQualification();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");

                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        this.Clear();
                        this.BindListViewQualification();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Already Exist");
                        
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["qNo"] != null)
                    {
                        objSevBook.QNO = Convert.ToInt32(ViewState["qNo"].ToString());
                        int qno = objSevBook.QNO;
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateQualification(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("QUALIFICATION", qno, ViewState["attachment"].ToString(), _idnoEmp, "QUA_", flupld);
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewQualification();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");

                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewQualification();
                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Already Exist");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int qNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(qNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int qNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleQualificationDetailsOfEmployee(qNO);
            //To show Qualification Details of the employee
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["qNo"] = qNO.ToString();
                txtExam.Text = ds.Tables[0].Rows[0]["examname"].ToString();
                txtRegNo.Text = ds.Tables[0].Rows[0]["regno"].ToString();
                txtSpecialization.Text = ds.Tables[0].Rows[0]["Speci"].ToString();
                txtInstituteName.Text = ds.Tables[0].Rows[0]["inst"].ToString();
                txtUniversity.Text = ds.Tables[0].Rows[0]["UNIVERSITY_NAME"].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();

                txtYearOfPassing.Text = ds.Tables[0].Rows[0]["passyear"].ToString();
                ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["LEVEL_NO"].ToString();

                objCommon.FillDropDownList(ddlQualification, "PAYROLL_QUALIFICATION", "QUALINO", "QUALI", "QUALILEVELNO=" + Convert.ToInt32(ddlLevel.SelectedValue) + "", "QUALI");
                ddlQualification.SelectedValue = ds.Tables[0].Rows[0]["QUALINO"].ToString();

                ddlunitype.SelectedValue = ds.Tables[0].Rows[0]["UNITYPE"].ToString();
                ddlinstitype.SelectedValue = ds.Tables[0].Rows[0]["INITYPE"].ToString();
                txtpercent.Text = ds.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                txtgrade.Text = ds.Tables[0].Rows[0]["GRADE"].ToString();
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["PASSMONTH"].ToString();

                Boolean ishighestquali = Convert.ToBoolean(ds.Tables[0].Rows[0]["isHighestQuali"].ToString());

                if (ishighestquali == true)
                {
                    chkhighest.Checked = true;
                }
                else
                {
                    chkhighest.Checked = false;
                }

               // txtIDCardNo.Text = ds.Tables[0].Rows[0]["IDCARDNO"].ToString();
                txtRegName.Text = ds.Tables[0].Rows[0]["REG_COUNCIL_NAME"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["REG_DATE"] == null ? string.Empty : ds.Tables[0].Rows[0]["REG_DATE"].ToString();

                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                txtCGPA.Text = ds.Tables[0].Rows[0]["CGPA"].ToString();

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
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int qNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_QUALI", "*", "", "QNO=" + qNo, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteQualification(qNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewQualification();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtExam.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtSpecialization.Text = string.Empty;
        txtUniversity.Text = string.Empty;
        txtYearOfPassing.Text = string.Empty;
        txtDate.Text = txtLocation.Text = txtInstituteName.Text = string.Empty;
        txtIDCardNo.Text = string.Empty;
        ddlLevel.SelectedIndex = 0;
        ddlQualification.SelectedIndex = 0;

        ddlinstitype.SelectedIndex = 0;
        ddlunitype.SelectedIndex = 0;
        txtpercent.Text = string.Empty;
        txtgrade.Text = string.Empty;
        chkhighest.Checked = false;
        ddlMonth.SelectedValue = "0";
        //ddlQualification.Items.Clear();
        //ddlQualification.SelectedValue = "0";
        txtRegName.Text = txtDate.Text = string.Empty;
        ViewState["action"] = "add";
        txtCGPA.Text = string.Empty;
    }

    public string GetFileNamePath(object filename, object QNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/QUALIFICATION/" + idno.ToString() + "/QUA_" + QNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SELECT  QUALINO,QUALI FROM PAYROLL_QUALIFICATION where QUALILEVELNO=1
        objCommon.FillDropDownList(ddlQualification, "PAYROLL_QUALIFICATION", "QUALINO", "QUALI", "QUALILEVELNO=" + Convert.ToInt32(ddlLevel.SelectedValue) + "", "QUALI");
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