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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Nomination : System.Web.UI.Page
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
        BindListViewNominee();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Nomination.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Nomination.aspx");
        }
    }
    private void BindListViewNominee()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllNominiDetailsOfEmployee(_idnoEmp);
            lvNomination.DataSource = ds;
            lvNomination.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvNomination.FindControl("divFolder");
                Control ctrHead1 = lvNomination.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvNomination.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvNomination.FindControl("divFolder");
                Control ctrHead1 = lvNomination.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvNomination.Items)
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.BindListViewNominee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {   //nfno,idno,ntno,name,relation,per,remark,srno,dob,last,Address,Conting,Age
            // Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
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

            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.NTNO = Convert.ToInt32(ddlNominationFor.SelectedValue);
            objSevBook.NAME = txtNameOfNominee.Text;
            objSevBook.RELATION = txtRelation.Text;
            //objSevBook.PER = Convert.ToDecimal(txtPercentage.Text);
            objSevBook.REMARK = txtRemarks.Text;
            objSevBook.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            bool lastNomini;
            if (chkLastNominee.Checked)
                lastNomini = true;
            else
                lastNomini = false;

            if (txtPercentage.Text != string.Empty)
            {
                objSevBook.PER = Convert.ToDecimal(txtPercentage.Text);
            }
            else
            {
                objSevBook.PER = null;
            }

            objSevBook.LAST = lastNomini;
            //objSevBook.ADDRESS = txtAddress.Text;
            objSevBook.CONTING = txtContingencies.Text;
            objSevBook.AGE = Convert.ToDecimal(txtAge.Text);

            if (txtTaluka.Text != string.Empty)
            {
                objSevBook.TALUKA = txtTaluka.Text;
            }
            else
            {
                objSevBook.TALUKA = string.Empty;
            }
            if (txtDistrict.Text != string.Empty)
            {
                objSevBook.DISTRICT = txtDistrict.Text;
            }
            else
            {
                objSevBook.DISTRICT = string.Empty;
            }

            if (txtPincode.Text != string.Empty)
            {
                objSevBook.PINCODE = txtPincode.Text;
            }
            else
            {
                objSevBook.PINCODE = string.Empty;
            }
            if (txtCity.Text != string.Empty)
            {
                objSevBook.CITY = txtCity.Text;
            }
            else
            {
                objSevBook.CITY = string.Empty;
            }

            if (txtState.Text != string.Empty)
            {
                objSevBook.STATE = txtState.Text;
            }
            else
            {
                objSevBook.STATE = string.Empty;
            }

            if (txtCountry.Text != string.Empty)
            {
                objSevBook.COUNTRY = txtCountry.Text;
            }
            else
            {
                objSevBook.COUNTRY = string.Empty;
            }

            if (txtAddress.Text != string.Empty)
            {
                objSevBook.ADDRESS = txtAddress.Text;
            }
            else
            {
                objSevBook.ADDRESS = string.Empty;
            }

            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();


            if (rdbNewAddress.Checked == true || rdbAsPermanentAdd.Checked == true || rdbAsLocalAdd.Checked == true)
            {
                if (rdbNewAddress.Checked == true)
                {
                    objSevBook.ADDRESS = txtAddress.Text;
                    objSevBook.COUNTRY = txtCountry.Text;
                    objSevBook.STATE = txtState.Text;
                    objSevBook.CITY = txtCity.Text;
                    objSevBook.DISTRICT = txtDistrict.Text;
                    objSevBook.TALUKA = txtTaluka.Text;
                    objSevBook.PINCODE = txtPincode.Text;
                    objSevBook.ADDRESS_FLAG = 1;
                }
                if (rdbAsPermanentAdd.Checked == true)
                {
                    objSevBook.ADDRESS = txtAddress.Text;
                    objSevBook.COUNTRY = txtCountry.Text;
                    objSevBook.STATE = txtState.Text;
                    objSevBook.CITY = txtCity.Text;
                    objSevBook.DISTRICT = txtDistrict.Text;
                    objSevBook.TALUKA = txtTaluka.Text;
                    objSevBook.PINCODE = txtPincode.Text;
                    objSevBook.ADDRESS_FLAG = 2;
                }
                if (rdbAsLocalAdd.Checked == true)
                {
                    objSevBook.ADDRESS = txtAddress.Text;
                    objSevBook.COUNTRY = txtCountry.Text;
                    objSevBook.STATE = txtState.Text;
                    objSevBook.CITY = txtCity.Text;
                    objSevBook.DISTRICT = txtDistrict.Text;
                    objSevBook.TALUKA = txtTaluka.Text;
                    objSevBook.PINCODE = txtPincode.Text;
                    objSevBook.ADDRESS_FLAG = 3;
                }
            }
            else
            {
                objSevBook.ADDRESS = string.Empty;
                objSevBook.COUNTRY = string.Empty;
                objSevBook.STATE = string.Empty;
                objSevBook.CITY = string.Empty;
                objSevBook.DISTRICT = string.Empty;
                objSevBook.TALUKA = string.Empty;
                objSevBook.PINCODE = string.Empty;
                objSevBook.ADDRESS_FLAG = 0;
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
                    //string name = txtNameOfNominee.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_nomiation_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_nomiation_" + time, flupld);
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
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddNominiFor(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("NOMINATION", _idnoEmp, "NFNO", "PAYROLL_SB_NOMINIFOR", "NOM_", flupld);
                        }
                        this.Clear();
                        this.BindListViewNominee();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        this.Clear();
                        this.BindListViewNominee();
                        //this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        MessageBox("Record Already Exist");

                    }
                }
                else
                {
                    //Edit
                    if (ViewState["nfNo"] != null)
                    {
                        objSevBook.NFNO = Convert.ToInt32(ViewState["nfNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateNominiFor(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("NOMINATION", objSevBook.NFNO, ViewState["attachment"].ToString(), _idnoEmp, "NOM_", flupld);
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewNominee();
                            // this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            this.Clear();
                            this.BindListViewNominee();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int nfNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(nfNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int nfNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleNominiDetailsOfEmployee(nfNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                //nfno,idno,ntno,name,relation,per,remark,srno,dob,last,Address,Conting,Age
                ViewState["nfNo"] = nfNo.ToString();
                ddlNominationFor.SelectedValue = ds.Tables[0].Rows[0]["ntno"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                txtContingencies.Text = ds.Tables[0].Rows[0]["Conting"].ToString();
                txtDateOfBirth.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                txtNameOfNominee.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["per"].ToString();
                txtRelation.Text = ds.Tables[0].Rows[0]["relation"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                chkLastNominee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["last"].ToString());
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                //if(Convert.ToBoolean(ds.Tables[0].Rows[0]["last"].ToString()))
                //  chkLastNominee.Checked = true;
                //else
                //  chkLastNominee.Checked = false;

                if (ds.Tables[0].Rows[0]["ADDRESS_FLAG"].ToString() == "1")
                {
                    txtAddress.Enabled = true;
                    rdbNewAddress.Checked = true;
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ADDRESS_FLAG"].ToString() == "2")
                {
                    txtAddress.Enabled = true;
                    rdbAsPermanentAdd.Checked = true;
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ADDRESS_FLAG"].ToString() == "3")
                {
                    txtAddress.Enabled = true;
                    rdbAsLocalAdd.Checked = true;
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                }

                txtCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
                txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                txtState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                txtTaluka.Text = ds.Tables[0].Rows[0]["TALUKA"].ToString();
                txtPincode.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
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
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int nfNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_NOMINIFOR", "*", "", "NFNO=" + nfNo, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteNominifor(nfNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindListViewNominee();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlNominationFor.SelectedValue = "0";
        txtAddress.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtContingencies.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtNameOfNominee.Text = string.Empty;
        txtPercentage.Text = string.Empty;
        txtRelation.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        chkLastNominee.Checked = false;
        ViewState["action"] = "add";
        rdbNewAddress.Checked = false;
        rdbAsPermanentAdd.Checked = false;
        rdbAsLocalAdd.Checked = false;
        txtPincode.Text = string.Empty;
        txtState.Text = string.Empty;
        txtTaluka.Text = string.Empty;
        txtDistrict.Text = string.Empty;
        txtCountry.Text = string.Empty;
        txtCity.Text = string.Empty;
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlNominationFor, "Payroll_NominiType", "Ntno", "Nominitype", "Ntno > 0", "Ntno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object NFNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/NOMINATION/" + idno.ToString() + "/NOM_" + NFNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
    {
        DateTime Test;
        if (DateTime.TryParseExact(txtDateOfBirth.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (Convert.ToDateTime(txtDateOfBirth.Text) > System.DateTime.Now)
            {
                MessageBox("Date Of Birth Should Not be greater Than Current Date");
                txtDateOfBirth.Text = string.Empty;
                return;
            }
            var today = DateTime.Today;
            DateTime dateOfBirth;
            dateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            txtAge.Text = Convert.ToString((a - b) / 10000);
        }
        else
        {
            // MessageBox("Date Of Birth Not a Correct foramt");
            txtDateOfBirth.Text = string.Empty;
        }
    }

    protected void rdbNewAddress_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbNewAddress.Checked == true)
        {
            txtAddress.Text = string.Empty;
            txtAddress.Enabled = true;
            rdbAsPermanentAdd.Checked = false;
            rdbAsLocalAdd.Checked = false;
        }
        else
        {
            txtAddress.Enabled = false;
        }

    }
    protected void rdbAsPermanentAdd_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAsPermanentAdd.Checked == true)
        {
            txtAddress.Text = string.Empty;
            txtAddress.Enabled = true;
            rdbNewAddress.Checked = false;
            rdbAsLocalAdd.Checked = false;

            DataSet ds = null;
            try
            {
                ds = objServiceBook.GetAddressOfEmployee(_idnoEmp);
                //To show employee Permanent Address 
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //ViewState["fnNo"] = fnNo.ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["TOWNADD1"].ToString();

                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.GetPermanentAddOfEmployee-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        else
        {
            txtAddress.Enabled = false;
        }
    }
    protected void rdbAsLocalAdd_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAsLocalAdd.Checked == true)
        {
            txtAddress.Text = string.Empty;
            txtAddress.Enabled = true;
            rdbNewAddress.Checked = false;
            rdbAsPermanentAdd.Checked = false;

            DataSet ds = null;
            try
            {
                ds = objServiceBook.GetAddressOfEmployee(_idnoEmp);
                //To show employee Permanent Address 
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //ViewState["fnNo"] = fnNo.ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["RESADD1"].ToString();

                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.GetPermanentAddOfEmployee-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        else
        {
            txtAddress.Enabled = false;
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

                //string DocLink = "https://docs.google.com/viewer?url=https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = blobpath;
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