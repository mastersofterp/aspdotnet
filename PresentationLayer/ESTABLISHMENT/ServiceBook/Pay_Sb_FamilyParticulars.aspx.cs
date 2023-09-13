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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_FamilyParticulars : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;
    BlobController objBlob = new BlobController();

    protected void Page_Load(object sender, EventArgs e)
    {

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

            objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "COLLEGE_CODE>0 AND ACTIVESTATUS =" + 1, "BLOODGRPNAME");
            objCommon.FillDropDownList(ddlrelationship, "PAYROLL_RELATIONSHIP_MASTER", "RelationshipId", "Relationship", "", "");
            // objCommon.FillDropDownList(ddlCity, "PAYROLL_CITY", "CITYNO", "CITY", "COLLEGE_CODE>0", "CITY");
        }

        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewFamilyInfo();

    }

    private void BindListViewFamilyInfo()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllFamilyDetailsOfEmployee(_idnoEmp);
            lvFamilyInfo.DataSource = ds;
            lvFamilyInfo.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvFamilyInfo.FindControl("divFolder");
                Control ctrHead1 = lvFamilyInfo.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvFamilyInfo.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvFamilyInfo.FindControl("divFolder");
                Control ctrHead1 = lvFamilyInfo.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvFamilyInfo.Items)
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

    //protected Boolean chkDuplicate()
    //{
    //    DataSet ds = null;
    //    if (ViewState["action"].Equals("add"))
    //    {
    //        ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "*", " ", "MEMNAME='" + txtFamilyMemberName.Text + "'", "");
    //    }
    //    else
    //    {
    //        ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "*", " ", "MEMNAME='" + txtFamilyMemberName.Text + "' AND IDNO !=" + Convert.ToInt32(ViewState["serviceIdNo"]), "");
    //    }

    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

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
            objSevBook.MEMNAME = txtFamilyMemberName.Text;
            // objSevBook.ADDRESS = txtAddress.Text;

            objSevBook.RelationshipId = ddlrelationship.SelectedIndex;
            objSevBook.RELATION = ddlrelationship.SelectedItem.Text;
            objSevBook.AGE = Convert.ToInt32(txtAge.Text);
            objSevBook.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            objSevBook.OFFICER = txtOfficer.Text;
            objSevBook.EDUCATION = txtEducation.Text;
            //objSevBook.EMPLOYMENT = txtEmployment.Text;
            if (txtAdharno.Text != string.Empty)
            {
                objSevBook.ADHARNO = txtAdharno.Text;
            }
            else
            {
                objSevBook.ADHARNO = string.Empty;
            }
            objSevBook.MOBNO = txtMob.Text;
            //objSevBook.BLOODGROUP = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            //objSevBook.CITY = Convert.ToInt32(ddlCity.SelectedValue);

            if (txtAddress.Text != string.Empty)
            {
                objSevBook.ADDRESS = txtAddress.Text;
            }
            else
            {
                objSevBook.ADDRESS = string.Empty;
            }

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

            if (ddlgender.SelectedIndex > 0)
            {
                objSevBook.GENDER = Convert.ToInt32(ddlgender.SelectedValue);
            }
            else
            {
                objSevBook.GENDER = 0;
            }


            if (ddlMarital.SelectedIndex > 0)
            {
                objSevBook.MaritalStatus = Convert.ToInt32(ddlMarital.SelectedValue);
            }
            else
            {
                objSevBook.MaritalStatus = 0;
            }

            if (ddlBloodGroup.SelectedIndex > 0)
            {
                objSevBook.BLOODGROUP = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            }
            else
            {
                objSevBook.BLOODGROUP = 0;
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

            if (rdbNewAddress.Checked == true || rdbAsPermanentAdd.Checked == true || rdbAsLocalAdd.Checked == true)
            {
                if (rdbNewAddress.Checked == true)
                {
                    objSevBook.ADDRESS = txtAddress.Text;
                    objSevBook.ADDRESS_FLAG = 1;
                }
                if (rdbAsPermanentAdd.Checked == true)
                {
                    objSevBook.ADDRESS = txtAddress.Text;
                    objSevBook.ADDRESS_FLAG = 2;
                }
                if (rdbAsLocalAdd.Checked == true)
                {
                    objSevBook.ADDRESS = txtAddress.Text;
                    objSevBook.ADDRESS_FLAG = 3;
                }
            }
            else
            {
                objSevBook.ADDRESS = string.Empty;
                //objSevBook.ADDRESS_FLAG = 0;
            }


            if (rdbEmployment.SelectedValue == "0")
            {
                // objSevBook.EMPLOYMENT = txtEmployment.Text;
                if (txtEmployment.Text != string.Empty)
                {
                    objSevBook.EMPLOYMENT = txtEmployment.Text;
                }
                else
                {
                    objSevBook.EMPLOYMENT = string.Empty;
                }
                //objSevBook.POSTNAME = txtPost.Text;
                if (txtPost.Text != string.Empty)
                {
                    objSevBook.POSTNAME = txtPost.Text;
                }
                else
                {
                    objSevBook.POSTNAME = string.Empty;
                }
                // objSevBook.ORGNAME_ADDRESS = txtOrgName.Text;
                if (txtOrgName.Text != string.Empty)
                {
                    objSevBook.ORGNAME_ADDRESS = txtOrgName.Text;
                }
                else
                {
                    objSevBook.ORGNAME_ADDRESS = string.Empty;
                }
                //objSevBook.LASTSALARY = Convert.ToDecimal(txtSal.Text);
                if (txtSal.Text != string.Empty)
                {
                    objSevBook.LASTSALARY = Convert.ToDecimal(txtSal.Text);
                }
                else
                {
                    objSevBook.LASTSALARY = null;
                }
                objSevBook.EMPSTATUS = "YES";
            }
            else
            {
                objSevBook.EMPLOYMENT = string.Empty;
                objSevBook.POSTNAME = string.Empty;
                objSevBook.ORGNAME_ADDRESS = string.Empty;
                objSevBook.LASTSALARY = 0;
                objSevBook.EMPSTATUS = "NO";
            }



            if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update

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
                    //string name = txtFamilyMemberName.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_familyinfo_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flupld.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_familyinfo_" + time, flupld);
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
                    //DataSet ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "*", "", "(MEMNAME='" + txtleavename.Text + "' and RELATION='" + txtshortname.Text + "' AND LVNO<>" + objLeaves.LEAVENO + ") ", "");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    //objCommon.DisplayUserMessage(updAll, "Record already exists", this);
                    //    MessageBox("Record already exists");
                    //    return;
                    //}

                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddFamilyInfo(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("FAMILY_INFO", _idnoEmp, "FNNO", "PAYROLL_SB_FAMILYINFO", "FAI_", flupld);
                        }
                        //lblFamilymsg.Text = "Record Saved Successfully";
                        this.Clear();
                        this.BindListViewFamilyInfo();
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
                    if (ViewState["fnNo"] != null)
                    {
                        objSevBook.FNNO = Convert.ToInt32(ViewState["fnNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateFamilyInfo(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("FAMILY_INFO", objSevBook.FNNO, ViewState["attachment"].ToString(), _idnoEmp, "FAI_", flupld);
                            }
                            ViewState["action"] = "add";
                            //lblFamilymsg.Text = "Record Updated Successfully";
                            this.Clear();
                            this.BindListViewFamilyInfo();
                            // this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordFound))
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
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // lblFamilymsg.Text = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int fnNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(fnNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int fnNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleFamilyDetailsOfEmployee(fnNo);
            //To show employee family details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["fnNo"] = fnNo.ToString();
                txtFamilyMemberName.Text = ds.Tables[0].Rows[0]["memname"].ToString();
                ddlrelationship.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["RelationshipId"]);
                txtAge.Text = ds.Tables[0].Rows[0]["age"].ToString();
                txtDateOfBirth.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                txtOfficer.Text = ds.Tables[0].Rows[0]["officer"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                txtEducation.Text = ds.Tables[0].Rows[0]["EDUCATION"].ToString();
                txtEmployment.Text = ds.Tables[0].Rows[0]["EMPLOYMENT"].ToString();
                // txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();

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
                //if (ds.Tables[0].Rows[0]["ADDRESS_FLAG"].ToString() == "" || ds.Tables[0].Rows[0]["ADDRESS_FLAG"].ToString() == string.Empty)
                //{
                //    txtAddress.Enabled = true;
                //    txtAddress.Text = string.Empty;
                //}


                ddlgender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                ddlMarital.SelectedValue = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
                // ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                txtCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
                ddlBloodGroup.SelectedValue = ds.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                txtAdharno.Text = ds.Tables[0].Rows[0]["ADHARNO"].ToString();
                txtMob.Text = ds.Tables[0].Rows[0]["MOBNO"].ToString();
                txtTaluka.Text = ds.Tables[0].Rows[0]["TALUKA"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                txtPincode.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                txtState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                string empstatus = ds.Tables[0].Rows[0]["EMPSTATUS"].ToString();
                if (empstatus == "YES")
                {
                    rdbEmployment.SelectedValue = "0";
                    divEmployment.Visible = true;
                    divOrgName.Visible = true;
                    divPost.Visible = true;
                    divSalary.Visible = false;
                    txtEmployment.Text = ds.Tables[0].Rows[0]["EMPLOYMENT"].ToString();
                    txtPost.Text = ds.Tables[0].Rows[0]["POSTNAME"].ToString();
                    txtOrgName.Text = ds.Tables[0].Rows[0]["ORGNAME_ADDRESS"].ToString();
                    txtSal.Text = ds.Tables[0].Rows[0]["SALARY"].ToString();

                }
                else
                {
                    rdbEmployment.SelectedValue = "1";
                    divEmployment.Visible = false;
                    divOrgName.Visible = false;
                    divPost.Visible = false;
                    divSalary.Visible = false;
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
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lblFamilymsg.Text = string.Empty;
            ImageButton btnDel = sender as ImageButton;
            int fnNo = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "*", "", "FNNO=" + fnNo, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteFamilyInfo(fnNo);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewFamilyInfo();
                    // lblFamilymsg.Text = "Record Deleted Successfully";
                    MessageBox("Record Deleted Successfully");
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        // lblFamilymsg.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtFamilyMemberName.Text = string.Empty;
        txtOfficer.Text = string.Empty;
        //txtRelationShip.Text = string.Empty;
        ddlrelationship.SelectedIndex = 0;
        rdbAsLocalAdd.Checked = false;
        rdbNewAddress.Checked = false;
        rdbAsPermanentAdd.Checked = false;
        txtEducation.Text = string.Empty;
        txtEmployment.Text = string.Empty;
        ddlgender.SelectedIndex = 0;
        ddlMarital.SelectedIndex = 0;
        ViewState["action"] = "add";
        txtPincode.Text = string.Empty;
        txtAdharno.Text = string.Empty;
        txtDistrict.Text = string.Empty;
        txtMob.Text = string.Empty;
        // ddlCity.SelectedIndex = 0;
        txtCity.Text = string.Empty;
        ddlBloodGroup.SelectedIndex = 0;
        txtTaluka.Text = string.Empty;
        rdbEmployment.SelectedIndex = 1;
        txtEmployment.Text = string.Empty;
        txtOrgName.Text = string.Empty;
        txtSal.Text = string.Empty;
        txtPost.Text = string.Empty;
        txtCountry.Text = string.Empty;
        txtState.Text = string.Empty;

    }

    public string GetFileNamePath(object filename, object FNNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/FAMILY_INFO/" + idno.ToString() + "/FAI_" + FNNO + "." + extension[1].ToString().Trim());
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
    protected void rdbEmployment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbEmployment.SelectedValue == "0")
        {
            divEmployment.Visible = true;
            divOrgName.Visible = true;
            divPost.Visible = true;
            //divSalary.Visible = true;
        }
        else
        {
            divEmployment.Visible = false;
            divOrgName.Visible = false;
            divPost.Visible = false;
            // divSalary.Visible = false;
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