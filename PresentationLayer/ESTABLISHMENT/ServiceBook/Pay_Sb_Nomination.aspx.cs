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
using System.Collections.Generic;
using System.IO;
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
    public string path = string.Empty;
    public string Docpath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/");
    public static string RETPATH = "";

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
            DeleteDirecPath(Docpath + "TEMP_CONDUCTTRAINING_FILES\\" + _idnoEmp + "\\APP_0");
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
            DataTable dt = null;
            if (ViewState["FILE1"] != null)
            {
                dt = (DataTable)ViewState["FILE1"];
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
            //Changes done for Blob
            if (lblBlobConnectiontring.Text == "")
            {
                objSevBook.ISBLOB = 0;
            }
            else
            {
                objSevBook.ISBLOB = 1;
            }

            //Changes done for Blob
            //if (lblBlobConnectiontring.Text == "")
            //{
            //    objSevBook.ISBLOB = 0;
            //}
            //else
            //{
            //    objSevBook.ISBLOB = 1;
            //}
            //if (objSevBook.ISBLOB == 1)
            //{
            //    string filename = string.Empty;
            //    string FilePath = string.Empty;
            //    string IdNo = _idnoEmp.ToString();
            //    if (flupld.HasFile)
            //    {
            //        string contentType = contentType = flupld.PostedFile.ContentType;
            //        string ext = System.IO.Path.GetExtension(flupld.PostedFile.FileName);
            //        //HttpPostedFile file = flupld.PostedFile;
            //        //filename = objSevBook.IDNO + "_familyinfo" + ext;
            //        //string name = txtNameOfNominee.Text.Replace(" ", "");
            //        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
            //        filename = IdNo + "_nomiation_" + time + ext;
            //        objSevBook.ATTACHMENTS = filename;
            //        objSevBook.FILEPATH = "Blob Storage";

            //        if (flupld.FileContent.Length <= 1024 * 10000)
            //        {
            //            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            //            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
            //            bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

            //            if (result == true)
            //            {

            //                int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_nomiation_" + time, flupld);
            //                if (retval == 0)
            //                {
            //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (ViewState["attachment"] != null)
            //        {
            //            objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
            //        }
            //        else
            //        {
            //            objSevBook.ATTACHMENTS = string.Empty;
            //        }
            //    }
            //}
            //else
            //{
            //    if (flupld.HasFile)
            //    {
            //        objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
            //    }
            //    else
            //    {
            //        if (ViewState["attachment"] != null)
            //        {
            //            objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
            //        }
            //        else
            //        {
            //            objSevBook.ATTACHMENTS = string.Empty;
            //        }
            //    }
            //}
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddNominiFor(objSevBook, dt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //if (objSevBook.ISBLOB == 0)
                        //{
                        //    objServiceBook.upload_new_files("NOMINATION", _idnoEmp, "NFNO", "PAYROLL_SB_NOMINIFOR", "NOM_", flupld);
                        //}
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string TNO = objCommon.LookUp("PAYROLL_SB_TRAINING_CONDUCTED", "MAX(TNO)", "");
                                AddDocuments(Convert.ToInt32(TNO));
                            }
                        }
                        this.Clear();
                        DeletePath();
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
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateNominiFor(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //if (objSevBook.ISBLOB == 0)
                            //{
                            //    objServiceBook.update_upload("NOMINATION", objSevBook.NFNO, ViewState["attachment"].ToString(), _idnoEmp, "NOM_", flupld);
                            //}
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string TNO = objCommon.LookUp("PAYROLL_SB_TRAINING_CONDUCTED", "MAX(TNO)", "");
                                    AddDocuments(Convert.ToInt32(TNO));
                                }
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewNominee();
                            DeletePath();
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
                //string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                //if (STATUS == "A")
                //{
                //    MessageBox("Your Details are Approved you cannot edit.");
                //    return;
                //}
                //else
                //{
                //}
                if (Convert.ToInt32(ds.Tables[1].Rows.Count) > 0)
                {
                    int rowCount = ds.Tables[1].Rows.Count;
                    CreateTable();
                    DataTable dtM = (DataTable)ViewState["FILE1"];
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow dr = dtM.NewRow();
                        dr["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                        dr["FILEPATH"] = Docpath + "TRAINING" + ViewState["idno"] + "\\APP_" + nfNo;
                        dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                        dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                        dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                        dr["FOLDER"] = "TRAINING_CONDUCTED";
                        dr["APPID"] = nfNo.ToString();
                        dr["FILENAME"] = ds.Tables[1].Rows[i]["FILENAME"].ToString();
                        dtM.Rows.Add(dr);
                        dtM.AcceptChanges();
                        ViewState["FILE1"] = dtM;
                        ViewState["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                    }
                    //lvCompAttach.DataSource = (DataTable)ViewState["FILE1"];
                    //lvCompAttach.DataBind();
                    pnlAttachmentList.Visible = true;
                    this.BindListView_Attachments(dtM);
                }
                else
                {
                    pnlAttachmentList.Visible = false;
                    lvCompAttach.DataSource = null;
                    lvCompAttach.DataBind();
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

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();
            //divAttch.Visible = true;

            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                Control ctrhead2 = lvCompAttach.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;

                }
            }
            else
            {

                Control ctrHeader = lvCompAttach.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
        btnSubmit.Enabled = true;

        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        //pnlfiles.Visible = false;
        pnlAttachmentList.Visible = false;
        ViewState["FILE1"] = null;
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

    #endregion

    protected void txtPercentage_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDouble(txtPercentage.Text) > 100.00)
        {
            MessageBox("Please enter valid percentage!");
            btnSubmit.Enabled = false;
            return;
        }
        else
        {
            btnSubmit.Enabled = true;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ServiceBook objSevBook = new ServiceBook();
            if (flupld.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
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
                    if (Session["serviceIdNo"] != null && Convert.ToInt32(Session["serviceIdNo"]) != 0)
                    {
                        idno = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
                    }
                    else
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    string FileName = flupld.FileName;
                    if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["FILE1"];
                        for (int i = 0; i < dtM.Rows.Count; i++)
                        {
                            if (dtM.Rows[i]["DisplayFileName"].ToString() == FileName)
                            {
                                MessageBox("File Already Exist!");
                                return;
                            }
                        }
                    }

                    string file = Docpath + "TEMP_CONDUCTTRAINING_FILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "TRAINING_CONDUCTED\\" + idno;
                    ViewState["DESTINATION_PATH"] = PATH;
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
                            //string name = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = IdNo + "_trainingconducted_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (flupld.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_trainingconducted_" + time, flupld);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    int tano = Addfieldstotbl(filename);
                                    //BindListView_Attachments();
                                }
                            }
                        }
                    }
                    else
                    {
                        string filename = flupld.FileName;
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                int tano = Addfieldstotbl(filename);
                                path = file + "\\TC_" + tano + System.IO.Path.GetExtension(flupld.PostedFile.FileName);
                                flupld.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]", this.Page);
                    flupld.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    public string GetFileNamePath(object filename, object TNO, object idno, object folder, object AppID)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/TC_" + TNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    private int Addfieldstotbl(string filename)
    {
        if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "TRAINING_CONDUCTED" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "TC_" + FUID + System.IO.Path.GetExtension(flupld.PostedFile.FileName);
            dr["DisplayFileName"] = flupld.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_CONDUCTTRAINING_FILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = ViewState["FILE1"];
            //LVFiles.DataBind();
            this.BindListView_Attachments(dt);
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "TRAINING_CONDUCTED" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "TC_" + FUID + System.IO.Path.GetExtension(flupld.PostedFile.FileName);
            dr["DisplayFileName"] = flupld.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_CONDUCTTRAINING_FILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
            //LVFiles.DataBind();
            //pnlfiles.Visible = true;
            pnlAttachmentList.Visible = true;
            this.BindListView_Attachments(dt);
        }
        return Convert.ToInt32(ViewState["FUID"]);
    }
    private void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn("FUID", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FILEPATH", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("GETFILE", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("IDNO", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FOLDER", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("APPID", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FILENAME", typeof(string));
        dt.Columns.Add(dc);

        ViewState["FILE1"] = dt;
    }
    private void AddDocuments(int TNO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            int idno = _idnoEmp;

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\APP_" + TNO;

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
            }
            DeleteDirectory(sourcePath);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.AddDocuments-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            //Delete all files from the Directory
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            //Delete all child Directories
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            //Delete a Directory
            Directory.Delete(path);
        }
    }
    protected void btnDelFile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            int appid = Convert.ToInt32(btnDelete.AlternateText);
            if (appid != 0)
            {
                path = Docpath + "TRAINING_CONDUCTED" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["nfNo"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_CONDUCTTRAINING_FILES" + "\\" + idno + "\\APP_" + appid;
            }

            if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
            {
                DataTable dt = (DataTable)ViewState["FILE1"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, fname));
                ViewState["FILE1"] = dt;
                //LVFiles.DataSource = dt;
                //LVFiles.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);

                if (ViewState["DELETE_BILLS"] != null && ((DataTable)ViewState["DELETE_BILLS"]) != null)
                {
                    DataTable dtD = (DataTable)ViewState["DELETE_BILLS"];
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = path;
                    dr["FILENAME"] = fname;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BILLS"] = dtD;
                }
                else
                {
                    DataTable dtD = this.CreateTableBill();
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = path;
                    dr["FILENAME"] = fname;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BILLS"] = dtD;
                }
                DataTable dtM = (DataTable)ViewState["FILE1"];
                pnlAttachmentList.Visible = true;
                this.BindListView_Attachments(dtM);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private DataTable CreateTableBill()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }
    private DataRow GetEditableDatarowBill(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["GETFILE"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return datRow;
    }
    private void DeletePath()
    {
        if (ViewState["DELETE_BILLS"] != null && ((DataTable)ViewState["DELETE_BILLS"]) != null)
        {
            int i = 0;
            DataTable DtDel = (DataTable)ViewState["DELETE_BILLS"];
            foreach (DataRow Dr in DtDel.Rows)
            {
                string filename = DtDel.Rows[i]["FILENAME"].ToString();
                string filepath = DtDel.Rows[i]["FILEPATH"].ToString();

                if (File.Exists(filepath + "\\" + filename))
                {
                    File.Delete(filepath + "\\" + filename);
                }
                i++;
            }
            ViewState["DELETE_BILLS"] = null;
        }
    }
    private void DeleteDirecPath(string FilePath)
    {
        if (System.IO.Directory.Exists(FilePath))
        {
            try
            {
                System.IO.Directory.Delete(FilePath, true);
            }

            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
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
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
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
}