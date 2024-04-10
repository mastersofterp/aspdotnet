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

public partial class PAYROLL_SERVICEBOOK_Pay_Sb_publication_Details : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    DataTable dt = new DataTable();
    DataTable dtBindLV = new DataTable();
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
            //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
            //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
            if (Session["serviceIdNo"] != null)
            {
                _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
            }
            BlobDetails();
            BindListViewPublicationDetails();
            txtAuthor.Text = (objCommon.LookUp("payroll_empmas", "fname+ ' '+mname+' '+lname", "idno=" + _idnoEmp)).ToString().Trim();
            dt = setGridViewDataset(dt, "EMPMAS").Clone();
            dtBindLV = setGridViewDatasetBindLV(dtBindLV, "EMPMAS").Clone();
            ViewState["dtBindLV"] = dtBindLV;
            ViewState["dt"] = dt;

        }
        else
        {
            if (Session["serviceIdNo"] != null)
            {
                _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (ViewState["dtBindLV"] != null && ViewState["dtBindLV"] != string.Empty)
                    {
                        dtBindLV = (DataTable)ViewState["dtBindLV"];
                        dt = (DataTable)ViewState["dt"];
                    }
                    else
                    {
                        dt = setGridViewDataset(dt, "EMPMAS").Clone();
                        dtBindLV = setGridViewDatasetBindLV(dtBindLV, "EMPMAS").Clone();
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))//24-04-2018
                {
                    dt = setGridViewDataset(dt, "EMPMAS").Clone();
                    dtBindLV = setGridViewDatasetBindLV(dtBindLV, "EMPMAS").Clone();
                    foreach (ListViewItem lvitem in lvAuthorList.Items)
                    {
                        Label lblAuthName = lvitem.FindControl("lblAuthName") as Label;
                        string EnameItem = lblAuthName.Text;
                        Label lblAuthorrole = lvitem.FindControl("lblAuthorrole") as Label;
                        string EroleItem = lblAuthorrole.Text;
                        Label lblAuthoraffi = lvitem.FindControl("lblAuthoraffi") as Label;
                        string EaffiItem = lblAuthoraffi.Text;
                        HiddenField hdnsrno = lvitem.FindControl("hdnsrno") as HiddenField;
                        int srnoItem = Convert.ToInt32(hdnsrno.Value);

                        DataRow dr = dtBindLV.NewRow();
                        //dr["Name"] = EnameItem;
                        dr["SRNO"] = srnoItem;
                        dr["Name"] = EnameItem;
                        dr["Author_Role"] = EroleItem;
                        dr["Affiliation"] = EaffiItem;
                        dtBindLV.Rows.Add(dr);
                        dtBindLV.AcceptChanges();

                        DataRow dr2 = dt.NewRow();
                        //dr2["Name"] = EnameItem;
                        dr2["SRNO"] = srnoItem;
                        dr2["Name"] = EnameItem;
                        dr2["Author_Role"] = EroleItem;
                        dr2["Affiliation"] = EaffiItem;
                        dt.Rows.Add(dr2);
                        dt.AcceptChanges();


                    }
                    /*foreach (DataRow dr in dtBindLV.Rows)
                    {
                        if (dr["Idno"].ToString().Equals(Idno.ToString()))
                        {
                            dr.Delete();
                        }
                    }
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        if (dr2["Idno"].ToString().Equals(Idno.ToString()))
                        {
                            dr2.Delete();
                        }
                    }*/
                    ViewState["dtBindLV"] = dtBindLV; ViewState["dt"] = dt;
                }
            }
        }
        BindListViewPublicationDetails();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
        //if (rblConferenceJournal.SelectedValue == "0")
        //{
        //    divPublicationStatus.Visible = true;
        //    divVolumeNo.Visible = true;
        //    divIssueNo.Visible = true;
        //    lblName.InnerText = "Name Of Journal";
        //    lblSub.InnerText = "Title Of Book";
        //    divLocation.Visible = false;
        //    divISBN.Visible = false;
        //    divPublisher.Visible = false; divSub.Visible = false;

        //}
        GetConfigForEditAndApprove();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_publication_Details.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_publication_Details.aspx");
        }
    }

    private void BindListViewPublicationDetails()
    {
        try
        {
            int IsConference;
            if (rblConferenceJournal.SelectedValue == "0")
            {
                IsConference = 0;
            }
            else if (rblConferenceJournal.SelectedValue == "1")
            {
                IsConference = 1;
            }
            else if (rblConferenceJournal.SelectedValue == "2")
            {
                IsConference = 0;
            }
            else
            {

                IsConference = 2;
            }
            DataSet ds = objServiceBook.GetAllPublicationDetails(_idnoEmp);
            lvPublicationDetails.DataSource = ds;
            lvPublicationDetails.DataBind();
            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvPublicationDetails.FindControl("divFolder");
                Control ctrHead1 = lvPublicationDetails.FindControl("divBlob");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;

                foreach (ListViewItem lvRow in lvPublicationDetails.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdFolder");
                    Control ckattach = (Control)lvRow.FindControl("tdBlob");
                    ckBox.Visible = false;
                    ckattach.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvPublicationDetails.FindControl("divFolder");
                Control ctrHead1 = lvPublicationDetails.FindControl("divBlob");
                ctrHeader.Visible = true;
                ctrHead1.Visible = false;

                foreach (ListViewItem lvRow in lvPublicationDetails.Items)
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
                objUCommon.ShowError(Page, "Pay_publication_Details .BindListViewPublicationDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public bool CheckFileTypeAndSize()
    {
        try
        {
            //if (fu.HasFile)
            if (flPUB.HasFile)
            {
                string ext = System.IO.Path.GetExtension(flPUB.FileName);
                HttpPostedFile file = flPUB.PostedFile;
                if ((file != null) && (file.ContentLength > 0))
                {
                    int iFileSize = file.ContentLength;
                    if (iFileSize > 5242880)  // 40kb 5120
                    {
                        MessageBox("Please Select valid document file(upto 5 MB)");
                        return false;
                    }
                }

                string[] ValidExt = { ".PDF", ".DOC", ".DOCX", ".Pdf", ".pdf", ".doc", ".docx" };
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
                    MessageBox("Please Select valid document file(e.g. pdf,doc)");
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
            //if (flPUB.HasFile)
            //{
            //    if (flPUB.FileContent.Length >= 1024 * 10000)
            //    {

            //        MessageBox("File Size Should Not Be Greater Than 10 Mb");
            //        flPUB.Dispose();
            //        flPUB.Focus();
            //        return;
            //    }
            //}
            if (ViewState["Author"] != null)
            {
                dt = (DataTable)ViewState["Author"];
            }
            else
            {
                if (ViewState["dtBindLV"] != null)
                {

                }
                else
                {
                    MessageBox("Please add at least one Author.");
                    return;
                }
            }

            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.PUBLICATION_TYPE = ddlPublicationType.SelectedValue;
            objSevBook.TITLE = txttitle.Text;
            objSevBook.SUBJECT = txtSubject.Text;
            if (!txtPublicationDate.Text.Trim().Equals(string.Empty)) objSevBook.PUBLICATIONDATE = Convert.ToDateTime(txtPublicationDate.Text);
            //objSevBook.PUBLICATIONDATE = Convert.ToDateTime(txtPublicationDate.Text);
            objSevBook.DETAILS = txtDetails.Text;
            if (txtspons.Text != string.Empty)
            {
                objSevBook.SPONSORED_AMOUNT = Convert.ToDecimal(txtspons.Text);
            }
            else
            {
                objSevBook.SPONSORED_AMOUNT = 0;
            }
            objSevBook.ISBN = txtIsbn.Text != (string.Empty) ? txtIsbn.Text : string.Empty;
            objSevBook.CONFERENCE_NAME = txtName.Text != (string.Empty) ? txtName.Text : string.Empty;
            objSevBook.ORGANISOR = txtOrg.Text != (string.Empty) ? txtOrg.Text : string.Empty;
            objSevBook.PAGENO = txtPage.Text != (string.Empty) ? txtPage.Text : string.Empty;

            objSevBook.EISSN = txtEISSN.Text != (string.Empty) ? txtEISSN.Text : string.Empty;
            //objSevBook.ISBN = txtIsbn.Text != (string.Empty) ? txtIsbn.Text : string.Empty;
            if (txtpublisheradd.Text != string.Empty)
            {
                objSevBook.PUB_ADD = txtpublisheradd.Text;
            }
            else
            {
                objSevBook.PUB_ADD = string.Empty;
            }
            //===========Add new field as per client requiremnet====================
            objSevBook.VOLUME_NO = txtVolumeNo.Text != (string.Empty) ? txtVolumeNo.Text : string.Empty;
            objSevBook.ISSUE_NO = txtIssueNo.Text != (string.Empty) ? txtIssueNo.Text : string.Empty;
            objSevBook.PUB_STATUS = ddlPublicationStatus.SelectedValue;
            objSevBook.YEAR = txtYear.Text != (string.Empty) ? Convert.ToInt32(txtYear.Text) : 0;
            objSevBook.LOCATION = txtLoctaion.Text != (string.Empty) ? txtLoctaion.Text : string.Empty;
            objSevBook.PUBLISHER = txtPublisher.Text != (string.Empty) ? txtPublisher.Text : string.Empty;
            objSevBook.MONTH = txtMonth.Text != (string.Empty) ? txtMonth.Text : string.Empty;
            //if (ddlCollection.SelectedValue == "1")
            //{
            //    objSevBook.IsJournalScopus = true;
            //}
            //else if (ddlCollection.SelectedValue == "2")
            //{
            //    objSevBook.IsJournalScopus = false;
            //}
            objSevBook.IsJournalScopus = Convert.ToInt32(ddlCollection.SelectedValue);
            if (rblConferenceJournal.SelectedValue == "0")
            {
                objSevBook.IS_CONFERENCE = 0;
                objSevBook.PUBLICATION = "Journal";
            }
            else if (rblConferenceJournal.SelectedValue == "1")
            {
                objSevBook.IS_CONFERENCE = 1;
                objSevBook.PUBLICATION = "Conference";
            }
            else if (rblConferenceJournal.SelectedValue == "2")
            {
                objSevBook.IS_CONFERENCE = 2;
                objSevBook.PUBLICATION = "Books Chapter";
            }
            else
            {
                objSevBook.IS_CONFERENCE = 3;
                objSevBook.PUBLICATION = "Books";
            }

            if (txtimpactfactors.Text != string.Empty)
            {
                objSevBook.IMPACTFACTORS = txtimpactfactors.Text;
            }
            else
            {
                objSevBook.IMPACTFACTORS = string.Empty;
            }
            if (txtcitation.Text != string.Empty)
            {
                //objSevBook.DOIN = txtcitation.Text;
                objSevBook.CITATIONINDEX = txtcitation.Text;

            }
            else
            {
                //objSevBook.DOIN = string.Empty;
                objSevBook.CITATIONINDEX = string.Empty;
            }
            if (txtDOI.Text != string.Empty)
            {
                //objSevBook.CITATIONINDEX = txtDOI.Text.Trim();
                objSevBook.DOIN = txtDOI.Text.Trim();
            }
            else
            {
                // objSevBook.CITATIONINDEX = string.Empty;
                objSevBook.DOIN = string.Empty;
            }

            if (ddlIndexFac.SelectedIndex > 0)
            {
                objSevBook.IndexingFactors = Convert.ToInt32(ddlIndexFac.SelectedValue);
            }
            else
            {
                objSevBook.IndexingFactors = 0;
            }

            if (txtindexVal.Text != string.Empty)
            {
                objSevBook.IndexingFactorValue = txtindexVal.Text;
            }
            else
            {
                objSevBook.IndexingFactorValue = string.Empty;
            }
            if (!txtIndexDt.Text.Trim().Equals(string.Empty)) objSevBook.IndexingDATE = Convert.ToDateTime(txtIndexDt.Text);

            //==================================================================================           
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //string filename = objSevBook.ATTACHMENTS;

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
                if (flPUB.HasFile)
                {
                    string contentType = contentType = flPUB.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(flPUB.PostedFile.FileName);
                    //HttpPostedFile file = flPUB.PostedFile;
                    //filename = objSevBook.IDNO + "_familyinfo" + ext;
                    //string name = txtName.Text.Replace(" ", "");
                    string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                    filename = IdNo + "_pubdetails_" + time + ext;
                    objSevBook.ATTACHMENTS = filename;
                    objSevBook.FILEPATH = "Blob Storage";

                    if (flPUB.FileContent.Length <= 1024 * 10000)
                    {
                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_pubdetails_" + time, flPUB);
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
                if (flPUB.HasFile)
                {
                    objSevBook.ATTACHMENTS = Convert.ToString(flPUB.PostedFile.FileName.ToString());
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

            string indexingdetail = string.Empty;
            foreach (ListItem items in cblIndexing.Items)
            {
                if (items.Selected)
                {
                    if (indexingdetail == string.Empty)
                    {
                        indexingdetail = items.Value;
                    }
                    else
                    {
                        indexingdetail = indexingdetail + "," + items.Value;
                    }
                }
            }

            objSevBook.INDEXING_TYPE = indexingdetail.Trim();
            objSevBook.WEBLINK = txtweblink.Text;




            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    //dt = (DataTable)ViewState["Author"];

                    objSevBook.PUBTRXNO = 0;
                    CustomStatus cs = (CustomStatus)objServiceBook.AddUpdPublicationDetails(objSevBook, dt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            objServiceBook.upload_new_files("PUBLICATION", _idnoEmp, "PUBTRXNO", "PAYROLL_SB_PUBLICATION_DETAILS", "PUB_", flPUB);
                        }
                        this.Clear();
                        ViewState["action"] = "add";
                        this.BindListViewPublicationDetails();
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
                    if (ViewState["PUBTRXNO"] != null)
                    {
                        objSevBook.PUBTRXNO = Convert.ToInt32(ViewState["PUBTRXNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.AddUpdPublicationDetails(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                objServiceBook.update_upload("PUBLICATION", Convert.ToInt32(ViewState["PUBTRXNO"].ToString()), ViewState["attachment"].ToString(), _idnoEmp, "PUB_", flPUB);
                            }
                            ViewState["action"] = "add";

                            this.Clear();
                            this.BindListViewPublicationDetails();
                            ViewState["action"] = "add";
                            MessageBox("Record Updated Successfully");

                            //this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
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
                objUCommon.ShowError(Page, "Pay_publication_Details .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PUBTRXNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PUBTRXNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int PUBTRXNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePublicationDetails(PUBTRXNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["PUBTRXNO"] = PUBTRXNO.ToString();
                ddlPublicationType.SelectedValue = ds.Tables[0].Rows[0]["PUBLICATION_TYPE"].ToString();
                rblConferenceJournal.SelectedValue = ds.Tables[0].Rows[0]["IsConference"].ToString();
                rblConferenceJournal.SelectedValue = ds.Tables[0].Rows[0]["IsConference"].ToString();
                ddlCollection.SelectedValue = ds.Tables[0].Rows[0]["IsJournalScopusCheck"].ToString();

                txtweblink.Text = ds.Tables[0].Rows[0]["WEB_LINK"].ToString();

                //IsJournalScopus
                if (rblConferenceJournal.SelectedValue == "0")
                {
                    divPublicationStatus.Visible = true;
                    divVolumeNo.Visible = true;
                    divIssueNo.Visible = true;
                    lblName.InnerText = "Name of Journal";
                    lblSub.InnerText = "Title of Journal";
                    divLocation.Visible = false;
                    divISBN.Visible = true;
                    divPublisher.Visible = true;
                    divName.Visible = true;
                    lblTitlePaper.InnerText = "Title of the Paper";
                    divsitension.Visible = true;
                    divimpact.Visible = true;
                    divweblink.Visible = true;

                }
                else if (rblConferenceJournal.SelectedValue == "1")
                {
                    divPublicationStatus.Visible = false;
                    divVolumeNo.Visible = false;
                    divIssueNo.Visible = false;
                    lblName.InnerText = "Name of Conference";
                    divLocation.Visible = true;
                    divISBN.Visible = true;
                    divSub.Visible = false;
                    divPublisher.Visible = true;
                    divName.Visible = true;
                    divsitension.Visible = false;
                    divimpact.Visible = false;
                    divweblink.Visible = false;

                }
                else if (rblConferenceJournal.SelectedValue == "2")
                {
                    divPublicationStatus.Visible = false;
                    divVolumeNo.Visible = false;
                    divIssueNo.Visible = false;
                    divLocation.Visible = false;
                    divSub.Visible = false;
                    divName.Visible = false;
                    lblTitlePaper.InnerText = "Title of the Book Chapter";
                    divISBN.Visible = true;
                    divPublisher.Visible = true;
                    divsitension.Visible = false;
                    divimpact.Visible = false;
                    divweblink.Visible = false;
                }
                else
                {
                    divPublicationStatus.Visible = false;
                    divVolumeNo.Visible = false;
                    divIssueNo.Visible = false;
                    divLocation.Visible = false;
                    divSub.Visible = false;
                    divName.Visible = false;
                    lblTitlePaper.InnerText = "Title of the Book";
                    divISBN.Visible = true;
                    divPublisher.Visible = true;
                    divsitension.Visible = false;
                    divimpact.Visible = false;
                    divweblink.Visible = false;
                }

                txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtOrg.Text = ds.Tables[0].Rows[0]["ORGANISOR"].ToString();
                txtPage.Text = ds.Tables[0].Rows[0]["PAGENO"].ToString();
                txttitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                txtPublicationDate.Text = ds.Tables[0].Rows[0]["PUBLICATIONDATE"].ToString();
                txtDetails.Text = ds.Tables[0].Rows[0]["DETAILS"].ToString();
                txtIsbn.Text = ds.Tables[0].Rows[0]["ISBN"].ToString();
                txtEISSN.Text = ds.Tables[0].Rows[0]["EISSNNO"].ToString();
                txtpublisheradd.Text = ds.Tables[0].Rows[0]["PUB_ADD"].ToString();
                // Add New Column as per client requirement
                txtVolumeNo.Text = ds.Tables[0].Rows[0]["VolumeNo"].ToString();
                txtIssueNo.Text = ds.Tables[0].Rows[0]["IssueNo"].ToString();
                txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                txtLoctaion.Text = ds.Tables[0].Rows[0]["Location"].ToString();
                txtPublisher.Text = ds.Tables[0].Rows[0]["Publisher"].ToString();
                txtspons.Text = ds.Tables[0].Rows[0]["SPONSORED_AMOUNT"].ToString();
                ddlPublicationStatus.SelectedValue = ds.Tables[0].Rows[0]["Publication_status"].ToString();
                txtimpactfactors.Text = ds.Tables[0].Rows[0]["IMPACTFACTORS"].ToString();
                txtcitation.Text = ds.Tables[0].Rows[0]["CITATIONINDEX"].ToString();
                txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                txtDOI.Text = ds.Tables[0].Rows[0]["DOINO"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();



                string indexingdetails = ds.Tables[0].Rows[0]["INDEXING_TYPE"].ToString();

                ddlIndexFac.SelectedValue = ds.Tables[0].Rows[0]["IndexingFactors"].ToString();
                txtindexVal.Text = ds.Tables[0].Rows[0]["IndexingFactorValue"].ToString();
                txtIndexDt.Text = ds.Tables[0].Rows[0]["IndexingDATE"].ToString();

                string[] Indexing = indexingdetails.Split(new[] { "," }, StringSplitOptions.None);

                foreach (ListItem li in cblIndexing.Items)
                {
                    foreach (var item in Indexing)
                    {
                        if (li.Value == item)
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }


                if (ds.Tables[1].Rows.Count > 0)
                {
                    //ddlAuthor.SelectedValue = ds.Tables[1].Rows[0]["Idno"].ToString();
                    //ddlAuthor.SelectedValue = ds.Tables[1].Rows[1]["Ename"].ToString();
                    lvAuthorList.DataSource = ds.Tables[1];
                    lvAuthorList.DataBind();
                    dtBindLV = ds.Tables[1];
                    Session["dtBindLV"] = ds.Tables[1];
                    ViewState["dtBindLV"] = dtBindLV;
                    lvAuthorList.Visible = true;


                }
                else
                {
                    lvAuthorList.DataSource = null;
                    lvAuthorList.DataBind();
                }

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
                objUCommon.ShowError(Page, "Pay_publication_Details .ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int PUBTRXNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_PUBLICATION_DETAILS", "*", "", "PUBTRXNO=" + PUBTRXNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Edit.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeletePublicationDetails(PUBTRXNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewPublicationDetails();
                    ViewState["action"] = "add";
                    Clear();
                    MessageBox("Record Deleted Successfully");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtDetails.Text = string.Empty;
        txtPublicationDate.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txttitle.Text = string.Empty;
        ddlPublicationType.SelectedIndex = 0;
        //rblConferenceJournal.SelectedIndex = 0;
        txtPage.Text = string.Empty;
        txtOrg.Text = string.Empty;
        txtName.Text = string.Empty;
        ViewState["action"] = "add";
        txtIsbn.Text = string.Empty;
        txtVolumeNo.Text = string.Empty;
        txtIssueNo.Text = string.Empty;
        txtYear.Text = string.Empty;
        txtLoctaion.Text = string.Empty;
        txtPublisher.Text = string.Empty;
        txtspons.Text = string.Empty;
        ddlPublicationStatus.SelectedValue = "0";
        lvAuthorList.Visible = false;
        //ViewState.Remove("Author");
        ddlCollection.SelectedValue = "0";

        txtcitation.Text = string.Empty;
        txtimpactfactors.Text = string.Empty;
        txtEISSN.Text = string.Empty;
        txtpublisheradd.Text = string.Empty;
        ViewState["attachment"] = null;
        txtMonth.Text = string.Empty;
        txtDOI.Text = string.Empty;
        txtweblink.Text = string.Empty;

        ddlIndexFac.SelectedValue = "0";
        txtindexVal.Text = string.Empty;
        txtIndexDt.Text = string.Empty;


        //ViewState.Clear();
        txtAuthor.Text = (objCommon.LookUp("payroll_empmas", "fname+ ' '+mname+' '+lname", "idno=" + _idnoEmp)).ToString();
        ViewState["Author"] = null;
        ViewState["dtBindLV"] = null;

        cblIndexing.SelectedValue = null;
        rblConferenceJournal.SelectedIndex = 0;
        btnSubmit.Enabled = true;
        ddlAuthorRole.SelectedValue = "0";
        txtAffiliation.Text = string.Empty;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }
    public string GetFileNamePath(object filename, object pubtrxno, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PUBLICATION/" + idno.ToString() + "/PUB_" + pubtrxno + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }


    protected void rblConferenceJournal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblConferenceJournal.SelectedValue == "0")
        {
            divPublicationStatus.Visible = true;
            divVolumeNo.Visible = true;
            divIssueNo.Visible = true;
            lblName.InnerText = "Name of Journal";
            divSub.Visible = false;
            divLocation.Visible = false;
            divISBN.Visible = true;
            divsponsore.Visible = true;
            divName.Visible = divCollection.Visible = true;

            txttitle.ToolTip = "Enter Title of the Paper";
            lblTitlePaper.InnerText = "Title of the Paper";
            rfvtitle.ErrorMessage = "Please Enter Title of the Paper";

            divimpact.Visible = true;
            divsitension.Visible = true;
            divweblink.Visible = true;

        }
        else if (rblConferenceJournal.SelectedValue == "1")
        {
            divPublicationStatus.Visible = false;
            divVolumeNo.Visible = false;
            divIssueNo.Visible = false;
            lblName.InnerText = "Name of Conference";
            divLocation.Visible = true;
            divISBN.Visible = true;
            divsponsore.Visible = true;
            divSub.Visible = false;
            //divCollection.Visible = false;
            divCollection.Visible = true;
            divName.Visible = true;
            divimpact.Visible = false;
            divsitension.Visible = false;
            divweblink.Visible = false;

        }
        else if (rblConferenceJournal.SelectedValue == "2")
        {
            divPublicationStatus.Visible = false;
            divVolumeNo.Visible = false;
            divIssueNo.Visible = false;
            divLocation.Visible = false;
            divSub.Visible = false;
            divName.Visible = false;
            //divCollection.Visible = false;
            divCollection.Visible = true;

            lblTitlePaper.InnerText = "Title of the Book Chapter";
            txttitle.ToolTip = "Enter Title of the Book Chapter";
            rfvtitle.ErrorMessage = "Please Enter Title of the Book Chapter";

            divISBN.Visible = true;
            divsponsore.Visible = true;
            // divPublisher.Visible = true;
            divimpact.Visible = false;
            divsitension.Visible = false;
            divweblink.Visible = false;
        }
        else
        {
            divPublicationStatus.Visible = false;
            divVolumeNo.Visible = false;
            divIssueNo.Visible = false;
            divLocation.Visible = false;
            divSub.Visible = false;
            divName.Visible = false;
            //divCollection.Visible = false;
            divCollection.Visible = true;

            lblTitlePaper.InnerText = "Title of the Book";
            txttitle.ToolTip = "Enter Title of the Book";
            rfvtitle.ErrorMessage = "Please Enter Title of the Book";

            divISBN.Visible = true;
            divsponsore.Visible = true;
            // divPublisher.Visible = true;
            divimpact.Visible = false;
            divsitension.Visible = false;
            divweblink.Visible = false;
        }
    }

    #region Authhor

    protected DataTable setGridViewDataset(DataTable dt, string tabName)
    {
        dt.TableName.Equals(tabName);
        dt.Columns.Add("SRNO");
        dt.Columns.Add("Name");//Added for AuthorName
        dt.Columns.Add("Author_Role");
        dt.Columns.Add("Affiliation");
        return dt;
    }

    protected DataTable setGridViewDatasetBindLV(DataTable dtBindLV, string tabName)
    {
        dtBindLV.TableName.Equals(tabName);
        dtBindLV.Columns.Add("SRNO");
        dtBindLV.Columns.Add("Name");
        dtBindLV.Columns.Add("Author_Role");
        dtBindLV.Columns.Add("Affiliation");
        return dtBindLV;
    }
    protected void btnDeleteAuthor_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnDelAuthor = sender as ImageButton;
        string Name = (btnDelAuthor.CommandArgument);
        dtBindLV = (DataTable)ViewState["dtBindLV"];
        string EnameItem = string.Empty;


        foreach (DataRow dr1 in dtBindLV.Rows)
        {
            if (dr1["SRNO"].ToString().Equals(Name.ToString()))
            {
                dr1.Delete();
            }
        }
        MessageBox("Record Deleted Successfully");
        dtBindLV.AcceptChanges();
        ViewState["dt"] = dtBindLV;
        ViewState["dtBindLV"] = dtBindLV;
        lvAuthorList.DataSource = dtBindLV;
        lvAuthorList.DataBind();
        lvAuthorList.Visible = true;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string Ename = string.Empty;
        Ename = txtAuthor.Text.Trim();
        DataRow dr = dtBindLV.NewRow();
        if (txtAuthor.Text != string.Empty) //&& txtAffiliation.Text != string.Empty
        {
            if (CheckDuplicateName(dtBindLV, txtAffiliation.Text.Trim(), txtAuthor.Text.Trim(), ddlAuthorRole.SelectedItem.Text.Trim()))
            {
                MessageBox("Record Already Exist!");
                return;
            }
            dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]);
            dr["Author_Role"] = ddlAuthorRole.SelectedItem.Text.Trim();
            dr["Name"] = txtAuthor.Text.Trim();
            dr["Affiliation"] = txtAffiliation.Text.Trim();

            dtBindLV.Rows.Add(dr);
            dtBindLV.AcceptChanges();
            ViewState["Author"] = dtBindLV;
            lvAuthorList.DataSource = dtBindLV;
            lvAuthorList.DataBind();
            txtAuthor.Text = string.Empty; txtAffiliation.Text = string.Empty;
            ddlAuthorRole.SelectedIndex = 0;
            ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            ViewState["dtBindLV"] = dtBindLV;
            lvAuthorList.Visible = true;
        }

        //string Ename = string.Empty;
        //Ename = txtAuthor.Text;
        //DataRow dr = dtBindLV.NewRow();
        //if (txtAuthor.Text != string.Empty)
        //{
        //    dr["AuthorRole"] = ddlAuthorRole.SelectedItem.Text;
        //    dr["Name"] = txtAuthor.Text;
        //    dr["Affiliation"] = txtAffiliation.Text.Trim();
        //    dtBindLV.Rows.Add(dr);
        //    dtBindLV.AcceptChanges();
        //    txtAuthor.Text = string.Empty; txtAffiliation.Text = string.Empty;
        //    ddlAuthorRole.SelectedIndex = 0;
        //}
        //ViewState["Author"] = dtBindLV;
        //lvAuthorList.DataSource = dtBindLV;
        //lvAuthorList.DataBind();
        //lvAuthorList.Visible = true;
        //ViewState["dtBindLV"] = dtBindLV;

    }
    private bool CheckDuplicateName(DataTable dt, string value, string value1, string value2)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Author_Role"].ToString() == value2 && dr["Name"].ToString().Trim() == value1 && dr["Affiliation"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return retVal;
    }

    #endregion

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
            string Command = "Publication Details";
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
