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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

public partial class ACADEMIC_PHD_PhD_Publication_Detail : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhdcon = new PhdController();
    DataTable dt = new DataTable();
    DataTable dtBindLV = new DataTable();
    public int _idnoEmp;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_PHD"].ToString();


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
                CheckPageAuthorization();
                //CheckPageAuthorization();
                if (Session["usertype"].ToString() == "2")
                {
                    //objCommon.FillDropDownList(ddlAcdYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=PhD_Publication_Detail.aspx");
                }
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
            //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
            //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
            if (Session["IDNO"] != null)
            {
                _idnoEmp = Convert.ToInt32(Session["IDNO"].ToString().Trim());
            }
            //BindListViewPublicationDetails();
            txtAuthor.Text = (objCommon.LookUp("payroll_empmas", "fname+ ' '+mname+' '+lname", "idno=" + _idnoEmp)).ToString().Trim();
            //dt = setGridViewDataset(dt, "EMPMAS").Clone();
            //dtBindLV = setGridViewDatasetBindLV(dtBindLV, "EMPMAS").Clone();
            ViewState["dtBindLV"] = dtBindLV;
            ViewState["dt"] = dt;

        }
        else
        {
            if (Session["IDNO"] != null)
            {
                _idnoEmp = Convert.ToInt32(Session["IDNO"].ToString().Trim());
            }
            if (ViewState["action"] != null)
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
                //}
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
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhD_Publication_Detail.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhD_Publication_Detail.aspx");
        }
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
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
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
        }
        catch (Exception ex)
        {

        }
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
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txttitle.Text == string.Empty || txtName.Text == string.Empty || txtYear.Text == string.Empty || txtMonth.Text == string.Empty)
            { }
            else
            {
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

                Phd objPhd = new Phd();
                objPhd.IdNo = Convert.ToInt32(Session["IDNO"].ToString());
                int idno = Convert.ToInt32(Session["IDNO"].ToString());
                objPhd.DegreeNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Session["IDNO"].ToString()));
                objPhd.BranchNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Session["IDNO"].ToString()));
                objPhd.AdmBatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO=" + Session["IDNO"].ToString()));
                objPhd.PUBLICATION_TYPE = ddlPublicationType.SelectedValue;
                objPhd.TITLE = txttitle.Text;
                objPhd.SUBJECT = txtSubject.Text;
                if (!txtPublicationDate.Text.Trim().Equals(string.Empty)) objPhd.PUBLICATIONDATE = Convert.ToDateTime(txtPublicationDate.Text);
                objPhd.DETAILS = txtDetails.Text;
                if (txtspons.Text != string.Empty)
                {
                    objPhd.SPONSORED_AMOUNT = Convert.ToDecimal(txtspons.Text);
                }
                else
                {
                    objPhd.SPONSORED_AMOUNT = 0;
                }
                objPhd.ISBN = txtIsbn.Text != (string.Empty) ? txtIsbn.Text : string.Empty;
                objPhd.CONFERENCE_NAME = txtName.Text != (string.Empty) ? txtName.Text : string.Empty;
                objPhd.ORGANISOR = txtOrg.Text != (string.Empty) ? txtOrg.Text : string.Empty;
                objPhd.PAGENO = txtPage.Text != (string.Empty) ? txtPage.Text : string.Empty;

                objPhd.EISSN = txtEISSN.Text != (string.Empty) ? txtEISSN.Text : string.Empty;
                if (txtpublisheradd.Text != string.Empty)
                {
                    objPhd.PUB_ADD = txtpublisheradd.Text;
                }
                else
                {
                    objPhd.PUB_ADD = string.Empty;
                }
                //===========Add new field as per client requiremnet====================
                objPhd.VOLUME_NO = txtVolumeNo.Text != (string.Empty) ? txtVolumeNo.Text : string.Empty;
                objPhd.ISSUE_NO = txtIssueNo.Text != (string.Empty) ? txtIssueNo.Text : string.Empty;
                objPhd.PUB_STATUS = ddlPublicationStatus.SelectedValue;
                objPhd.YEAR = txtYear.Text != (string.Empty) ? Convert.ToInt32(txtYear.Text) : 0;
                objPhd.LOCATION = txtLoctaion.Text != (string.Empty) ? txtLoctaion.Text : string.Empty;
                objPhd.PUBLISHER = txtPublisher.Text != (string.Empty) ? txtPublisher.Text : string.Empty;
                objPhd.MONTH = txtMonth.Text != (string.Empty) ? txtMonth.Text : string.Empty;
                objPhd.IsJournalScopus = Convert.ToInt32(ddlCollection.SelectedValue);
                if (rblConferenceJournal.SelectedValue == "0")
                {
                    objPhd.IS_CONFERENCE = 0;
                    objPhd.PUBLICATION = "Journal";
                }
                else if (rblConferenceJournal.SelectedValue == "1")
                {
                    objPhd.IS_CONFERENCE = 1;
                    objPhd.PUBLICATION = "Conference";
                }
                if (txtimpactfactors.Text != string.Empty)
                {
                    objPhd.IMPACTFACTORS = txtimpactfactors.Text;
                }
                else
                {
                    objPhd.IMPACTFACTORS = string.Empty;
                }
                if (txtcitation.Text != string.Empty)
                {
                    objPhd.CITATIONINDEX = txtcitation.Text;
                }
                else
                {
                    objPhd.CITATIONINDEX = string.Empty;
                }
                if (txtDOI.Text != string.Empty)
                {
                    objPhd.DOIN = txtDOI.Text.Trim();
                }
                else
                {
                    objPhd.DOIN = string.Empty;
                }

                if (ddlIndexFac.SelectedIndex > 0)
                {
                    objPhd.IndexingFactors = Convert.ToInt32(ddlIndexFac.SelectedValue);
                }
                else
                {
                    objPhd.IndexingFactors = 0;
                }

                if (txtindexVal.Text != string.Empty)
                {
                    objPhd.IndexingFactorValue = txtindexVal.Text;
                }
                else
                {
                    objPhd.IndexingFactorValue = string.Empty;
                }
                if (!txtIndexDt.Text.Trim().Equals(string.Empty)) objPhd.IndexingDATE = Convert.ToDateTime(txtIndexDt.Text);

                //==================================================================================           
                objPhd.COLLEGE_CODE = Session["colcode"].ToString();

                string filename = "";
                string docname = "";


                if (flPUB.HasFile)
                {
                    bool flag = CheckFileTypeAndSize();
                    if (flag == false)
                    {
                        return;
                    }
                    string contentType = contentType = flPUB.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(flPUB.PostedFile.FileName);
                    HttpPostedFile file = flPUB.PostedFile;
                    filename = flPUB.PostedFile.FileName;
                    docname = idno + "_doc_" + rblConferenceJournal.SelectedItem.Text + filename + ext;

                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + rblConferenceJournal.SelectedItem.Text + "", flPUB);
                    if (retval == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                        return;
                    }
                    objPhd.ATTACHMENTS = Convert.ToString(flPUB.PostedFile.FileName.ToString()); ;
                }
                else
                {
                    if (ViewState["attachment"] != null)
                    {
                        objPhd.ATTACHMENTS = ViewState["attachment"].ToString();
                    }
                    else
                    {
                        objPhd.ATTACHMENTS = string.Empty;
                    }
                }

                // docname = objPhd.ATTACHMENTS;

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

                objPhd.INDEXING_TYPE = indexingdetail.Trim();
                objPhd.WEBLINK = txtweblink.Text;

                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        objPhd.PUBTRXNO = 0;
                        CustomStatus cs = (CustomStatus)objPhdcon.AddUpdPhdPublicationDetails(objPhd, dt);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //objPhdcon.upload_new_files("PUBLICATION", _idnoEmp, "PUBTRXNO", "PAYROLL_SB_PUBLICATION_DETAILS", "PUB_", flPUB);
                            this.Clear();
                            ViewState["action"] = "add";
                            this.BindListViewPublicationDetails();
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
                            objPhd.PUBTRXNO = Convert.ToInt32(ViewState["PUBTRXNO"].ToString());
                            CustomStatus cs = (CustomStatus)objPhdcon.AddUpdPhdPublicationDetails(objPhd, dt);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                //objPhdcon.update_upload("PUBLICATION", Convert.ToInt32(ViewState["PUBTRXNO"].ToString()), ViewState["attachment"].ToString(), _idnoEmp, "PUB_", flPUB);
                                ViewState["action"] = "add";

                                this.Clear();
                                this.BindListViewPublicationDetails();
                                ViewState["action"] = "add";
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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PhD_Publication_Detail .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        Response.Redirect(Request.Url.ToString());
    }
    //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    //protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    //{

    //}
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
            DataSet ds = objPhdcon.GetAllPublicationDetails(_idnoEmp);
            lvPublicationDetails.DataSource = ds;
            lvPublicationDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PhD_Publication_Detail .BindListViewPublicationDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
}