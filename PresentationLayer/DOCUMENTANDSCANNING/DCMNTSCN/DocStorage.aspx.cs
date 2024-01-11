using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DOCUMENTANDSCANNING_DCMNTSCN_DocType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DocumentType objDocType = new DocumentType();
    DocumentTypeController objDocC = new DocumentTypeController();
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    BlobController objBlob = new BlobController();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];
    static decimal File_size;
    string PageId;
    public string path = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                FillDropDown();
                BlobDetails();
                //BindListView();
                Session["Attachments"] = null;
            }
        }
    }

    //Check Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
        }
    }

    protected void FillDropDown()
    {
        //DataSet ds = objCommon.FillDropDown("DOCUMENT_TYPE", "DOC_ID", "DOCUMENT_TYPE", "", "DOC_ID");
        //ddldoctype.DataTextField = ds.Tables[0].Rows["DOCUMENT_TYPE"];
        //ddldoctype.DataValueField = ds.Tables[0].Rows["DOC_ID"];

        // Assuming objCommon is an instance of your class with the FillDropDown method
        DataSet ds = objCommon.FillDropDown("ADMN_DC_DOCUMENT_STORAGE_DOCUMENT_TYPE", "DOC_ID", "DOCUMENT_TYPE,DOC_ID", "", "");

        // Set the data source of the DropDownList
        ddldoctype.DataSource = ds.Tables[0];

        // Set the DataTextField and DataValueField properties
        ddldoctype.DataTextField = "DOCUMENT_TYPE";
        ddldoctype.DataValueField = "DOC_ID";

        // Bind the data to the DropDownList
        ddldoctype.DataBind();


    }


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (lvCompAttach.Items.Count > 0)
            {

            }
            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
               // Clear();
                return;
            }
           

            if (lblBlobConnectiontring.Text == "")
            {
                objDocType.ISBLOB = 0;
            }
            else
            {
                objDocType.ISBLOB = 1;
            }
            if (objDocType.ISBLOB == 1)
            {
                objDocType.AttachTable = ((DataTable)Session["Attachments"]);
            }

            //objDocType.DOCDATE = Convert.ToDateTime(txtDate.Text);
           // objDocType.District = string.IsNullOrEmpty(txtdistrict.Text) ? null : txtdistrict.Text.Trim();
           // objDocType.DOCNO = string.IsNullOrEmpty(txtdocnumber.Text) ? null : txtdocnumber.Text.Trim();
            //objDocType.PROP_ADDRESS = string.IsNullOrEmpty(txtpropAddress.Text) ? null : txtpropAddress.Text.Trim();
            //objDocType.SUBDIVINO = string.IsNullOrEmpty(txtsubdivnum.Text) ? null : txtsubdivnum.Text.Trim();
            //objDocType.SURVEYNO = string.IsNullOrEmpty(txtsurveyNumber.Text) ? null : txtsurveyNumber.Text.Trim();
            objDocType.DOCTYPE = ddldoctype.SelectedValue;

            if (txtpropAddress.Text != string.Empty)
            {
                objDocType.PROP_ADDRESS = txtpropAddress.Text;
            }
            else
            {
                objDocType.PROP_ADDRESS = string.Empty;
            }
            if (txtsubdivnum.Text != string.Empty)
            {
                objDocType.SUBDIVINO = txtsubdivnum.Text;
            }
            else
            {
                objDocType.SUBDIVINO = string.Empty;
            }
            if (txtsurveyNumber.Text != string.Empty)
            {
                objDocType.SURVEYNO = txtsurveyNumber.Text.Trim();
            }
            else
            {
                objDocType.SURVEYNO = string.Empty;
            }
            if (txtdocnumber.Text != string.Empty)
            {
                objDocType.DOCNO = txtdocnumber.Text.Trim();
            }
            else
            {
                objDocType.DOCNO = string.Empty;
            }
            if (txtdistrict.Text != string.Empty)
            {
                objDocType.District = txtdistrict.Text.Trim();
            }
            else
            {
                objDocType.District = string.Empty;
            }
            
            if (txtdocnumber.Text != string.Empty)
            {
                objDocType.DOCNO =txtdocnumber.Text.Trim();
            }
            else
            {
                objDocType.DOCNO = string.Empty;
            } 
            if (txtDate.Text != string.Empty)
            {
                objDocType.DOCDATE = Convert.ToDateTime(Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy"));
            }
             else
             {
                 objDocType.DOCDATE = DateTime.MinValue;
             } 

            if (txtFromDate.Text != "")
            {
                objDocType.FROM_DATE = Convert.ToDateTime(Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy"));

            }
            else
            {
                objDocType.FROM_DATE = DateTime.MinValue;
            } 
           
            if (txttodate.Text != "")
            {
                objDocType.TO_DATE = Convert.ToDateTime(Convert.ToDateTime(txttodate.Text).ToString("dd/MM/yyyy"));

            }
            else
            {
                objDocType.TO_DATE = DateTime.MinValue;
            } 
            
        
            if (txtEC.Text != string.Empty)
            {
                objDocType.EC_NO = txtEC.Text.Trim();
            }
            else
            {
                objDocType.EC_NO = string.Empty;
            }
           
         
            if (txtWest.Text!=string.Empty)
            {
                objDocType.WEST = Convert.ToDecimal(txtWest.Text.Trim());
            }
            else
            {
               
            }

            if (txtSouth.Text!=string.Empty)
            {
                objDocType.SOUTH = Convert.ToDecimal(txtSouth.Text.Trim());
            }
            else
            {
               
            }

            if (txtnorth.Text!=string.Empty)
            {
                objDocType.NORTH = Convert.ToDecimal(txtnorth.Text.Trim());
            }
            else
            {
               
            }

            if (txtEast.Text!=string.Empty)
            {
                objDocType.EAST = Convert.ToDecimal(txtEast.Text.Trim());
            }
            else
            {
               
            }

            if (txtarea.Text!=string.Empty)
            {
                objDocType.TOTAREA = Convert.ToDecimal(txtarea.Text.Trim());
            }
            else
            {
               
            }

            //if (txtdoctype.Text != string.Empty)
            //{
            //    objDocType.OTHERDOCTYPE = txtdoctype.Text.Trim();
            //}
            //else
            //{
            //    objDocType.OTHERDOCTYPE = string.Empty;
            //}

            //DataSet ds = objCommon.FillDropDown("ADMN_DC_ASSEST_DOCUMENT_STORAGE", "DOCID,DOCTYPE,DNO", "SURVEYNO,ECNO,OTHERDOCTYPE", "DOCTYPE='" + ddldoctype.SelectedValue + "'AND DNO='" + txtdocnumber + "' AND SURVEYNO='" + txtsurveyNumber.Text + "' AND ECNO='" + txtEC.Text + "' AND OTHERDOCTYPE='" + txtdoctype.Text + "' AND DOCID!="+Convert.ToInt32(ViewState["docid"]), "");
            //if(ds.Tables[0].Rows.Count>0)
            //{
            //    MessageBox("Record Already Exist");
            //    Clear();
            //    return;
            //}

            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)(objDocC.AddDocData(objDocType));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Save Successfully.", this.Page);

                        ViewState["action"] = "add";
                         BindListView(Convert.ToInt32(ddldoctype.SelectedValue));
                        Clear();

                    }
                }
                else
                {
                    objDocType.DOCDIDDATA = Convert.ToInt32(ViewState["docid"]);
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        CustomStatus cs = (CustomStatus)(objDocC.UpdateDocData(objDocType));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("Record Updated Successfully.", this.Page);

                            ViewState["action"] = "add";
                            BindListView(Convert.ToInt32(ddldoctype.SelectedValue));
                            Clear();

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divDoctype.Visible = true;
        Divdate.Visible = false;
        divDocNo.Visible = false;
        divAddress.Visible = false;
        divDistrict.Visible = false;
        DivSurveyNo.Visible = false;
        divDivisionNo.Visible = false;
        divArea.Visible = false;
        divEast.Visible = false;
        divWest.Visible = false;
        divsouth.Visible = false;
        divnorth.Visible = false;
        divAttach.Visible = false;
        divbtnAdd.Visible = false;
        divEcNo.Visible = false;
        divFDate.Visible = false;
        divTDate.Visible = false;
        //  divDoctypedatas.Visible = false;
        //lvDocStorage.Visible = false;
        Clear();
        lvDocStorage.DataSource = string.Empty;
        lvDocStorage.DataBind();
        Clear();
    }

    protected void Clear()
    {
        txtDate.Text = null;
        txtdistrict.Text = null;
        txtdocnumber.Text = null;
        txtpropAddress.Text = null;
        txtsubdivnum.Text = null;
        txtsurveyNumber.Text = null;
        ddldoctype.SelectedIndex = 0;
        txtnorth.Text = null;
        txtSouth.Text = null;
        txtEast.Text = null;
        txtWest.Text = null;
        txtEC.Text = null;
        txtFromDate.Text = null;
        txttodate.Text = null;
        lblFileName.Text = null;
        //txtdoctype.Text = null;
        txtarea.Text = null;
        lvCompAttach.DataSource = string.Empty;
        lvCompAttach.DataBind();
        Session["Attachments"] = null;
        ViewState["action"] = "add";
    {
             txtDate.Text = null;
        txtdistrict.Text = null;
        txtdocnumber.Text = null;
        txtpropAddress.Text = null;
        txtsubdivnum.Text = null;
        txtsurveyNumber.Text = null;
       // ddldoctype.SelectedIndex = 0;
        txtnorth.Text = null;
        txtSouth.Text = null;
        txtEast.Text = null;
        txtWest.Text = null;
        txtEC.Text = null;
        txtFromDate.Text = null;
        txttodate.Text = null;
        lblFileName.Text = null;
        //txtdoctype.Text = null;
        txtarea.Text = null;
        lvCompAttach.DataSource = string.Empty;
        lvCompAttach.DataBind();
        ViewState["action"] = "add";
        Session["Attachments"] = null;
    }

    protected void BindListView(int DOC_TYPE)
    {
        try
        {
            objDocType.DOCTYPE = ddldoctype.SelectedValue;
            // DataSet ds = objCommon.FillDropDown("ADMN_DC_ASSEST_DOCUMENT_STORAGE DC INNER JOIN DOCUMENT_TYPE DT ON (DT.DOC_ID=DC.DOCID)", "DOCID,DT.DOCUMENT_TYPE,DNO,DISTRICT,SURVEYNO,FILE_NAME", "FILE_PATH", "DC.DOCTYPE="+DOC_TYPE, "DOCID");
            DataSet ds = objDocC.GetData(objDocType);
            lvDocStorage.DataSource = ds;
            lvDocStorage.DataBind();
            string selectedDocumentTypeName = ddldoctype.SelectedItem.Text;
            if (string.Equals(selectedDocumentTypeName, "Encumbarence Certificate", StringComparison.OrdinalIgnoreCase))
            {
                Control ctrHeader = lvDocStorage.FindControl("thECNo");
                Control ctrHead1 = lvDocStorage.FindControl("thSurNo");
                Control ctrHead2 = lvDocStorage.FindControl("thDistrict");
                Control ctrHead3 = lvDocStorage.FindControl("thDocNo");

                if (ctrHeader != null)
                    ctrHeader.Visible = true;

                if (ctrHead1 != null)
                    ctrHead1.Visible = false;

                if (ctrHead2 != null)
                    ctrHead2.Visible = false;

                if (ctrHead3 != null)
                    ctrHead3.Visible = false;

                foreach (ListViewItem lvRow in lvDocStorage.Items)
                {
                    Control ck = (Control)lvRow.FindControl("tdECNo");
                    Control ck1 = (Control)lvRow.FindControl("tdSurNo");
                    Control ck2 = (Control)lvRow.FindControl("tdDistrict");
                    Control ck3 = (Control)lvRow.FindControl("tdDocNo");
                    Control ckblob = (Control)lvRow.FindControl("tdBlob1");

                    if (ck != null)
                        ck.Visible = true;

                    if (ck1 != null)
                        ck1.Visible = false;

                    if (ck2 != null)
                        ck2.Visible = false;

                    if (ck3 != null)
                        ck3.Visible = false;

                    if (ckblob != null)
                        ckblob.Visible = true;
                }
            }
            else
            {
                Control ctrHeader = lvDocStorage.FindControl("thECNo");
                Control ctrHead1 = lvDocStorage.FindControl("thSurNo");
                Control ctrHead2 = lvDocStorage.FindControl("thDistrict");
                Control ctrHead3 = lvDocStorage.FindControl("thDocNo");

                if (ctrHeader != null)
                    ctrHeader.Visible = false;

                if (ctrHead1 != null)
                    ctrHead1.Visible = true;

                if (ctrHead2 != null)
                    ctrHead2.Visible = true;

                if (ctrHead3 != null)
                    ctrHead3.Visible = true;

                foreach (ListViewItem lvRow in lvDocStorage.Items)
                {
                    Control ck = (Control)lvRow.FindControl("tdECNo");
                    Control ck1 = (Control)lvRow.FindControl("tdSurNo");
                    Control ck2 = (Control)lvRow.FindControl("tdDistrict");
                    Control ck3 = (Control)lvRow.FindControl("tdDocNo");
                    Control ckblob = (Control)lvRow.FindControl("tdBlob1");

                    if (ck != null)
                        ck.Visible = false;

                    if (ck1 != null)
                        ck1.Visible = true;

                    if (ck2 != null)
                        ck2.Visible = true;

                    if (ck3 != null)
                        ck3.Visible = true;

                    if (ckblob != null)
                        ckblob.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            throw;

        }

    }
  


    private void BlobDetails()
    {
        try
        {
            string Commandtype = "DocumentandScanning";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]),Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]),Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
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



    protected void BindListView_Attachments(DataTable dt)    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


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
            throw;
        }
    }

    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
     
        DateTime frmdate, todate;
        if (txttodate.Text == null || txttodate.Text == "" || txtFromDate.Text == "" || txtFromDate.Text == null)
        {
          
        }
        else
        {
            frmdate = Convert.ToDateTime(txtFromDate.Text);
            todate = Convert.ToDateTime(txttodate.Text);
            if (frmdate > todate)
            {
                txttodate.Text = string.Empty;
                objCommon.DisplayUserMessage(pnlAdd, "To Date Must be Greter then From Date ", this.Page);
            }
        }
    }


    //protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)

    //{
    //    BindListView(Convert.ToInt32(ddldoctype.SelectedValue));
    //    string selectedDocumentTypeName = ddldoctype.SelectedItem.Text;
    //    if (selectedDocumentTypeName == "Sale Deed" || selectedDocumentTypeName == "Settlement Deed" || selectedDocumentTypeName == "Sale Agreement" || selectedDocumentTypeName == "Gift Deed" || selectedDocumentTypeName == "Partition Deed" || selectedDocumentTypeName == "Power Of Attorney")
    //    {
    //        Divdate.Visible = true;
    //        divDocNo.Visible = true;
    //        divAddress.Visible = true;
    //        divDistrict.Visible = true;
    //        DivSurveyNo.Visible = true;
    //        divDivisionNo.Visible = true;
    //        divArea.Visible = true;
    //        divEast.Visible = true;
    //        divWest.Visible = true;
    //        divsouth.Visible = true;
    //        divnorth.Visible = true;
    //        divAttach.Visible = true;
    //        divbtnAdd.Visible = true;
    //        divEcNo.Visible = false;
    //        divFDate.Visible = false;
    //        divTDate.Visible = false;
    //        divDoctypedatas.Visible = false;
    //        ClearData();
    //        return;
    //    }
    //    else if (selectedDocumentTypeName == "Patta" || selectedDocumentTypeName == "Chitta" || selectedDocumentTypeName == "Adangal" || selectedDocumentTypeName == "Property Tax Receipt" )
    //    {
    //        Divdate.Visible = true;
    //        divDocNo.Visible = true;
    //        divAddress.Visible = true;
    //        divDistrict.Visible = true;
    //        DivSurveyNo.Visible = true;
    //        divDivisionNo.Visible = true;
    //        divAttach.Visible = true;
    //        divbtnAdd.Visible = true;
    //        divArea.Visible = false;
    //        divEcNo.Visible = false;
    //        divFDate.Visible = false;
    //        divTDate.Visible = false;
    //        divDoctypedatas.Visible = false;
    //        divEast.Visible = false;
    //        divWest.Visible = false;
    //        divsouth.Visible = false;
    //        divnorth.Visible = false;
    //        ClearData();
    //        return;
    //    }
    //    else if (selectedDocumentTypeName == "Encumbarence")
    //    {
    //        divEcNo.Visible = true;
    //        divFDate.Visible = true;
    //        divTDate.Visible = true;
    //        divAddress.Visible = true;
    //        divAttach.Visible = true;
    //        divbtnAdd.Visible = true;
    //        Divdate.Visible = false;
    //        divDocNo.Visible = false;
    //        divDistrict.Visible = false;
    //        DivSurveyNo.Visible = false;
    //        divDivisionNo.Visible = false;
    //        divDoctypedatas.Visible = false;
    //        divEast.Visible = false;
    //        divWest.Visible = false;
    //        divsouth.Visible = false;
    //        divnorth.Visible = false;
    //        ClearData();
    //        return;
    //    }
    //    else
    //    {
    //        divDoctypedatas.Visible = true;
    //        Divdate.Visible = true;
    //        divDocNo.Visible = true;
    //        divDistrict.Visible = true;
    //        DivSurveyNo.Visible = true;
    //        divDivisionNo.Visible = true;
    //        divAttach.Visible = true;
    //        divbtnAdd.Visible = true;
    //        divEcNo.Visible = false;
    //        divFDate.Visible = false;
    //        divTDate.Visible = false;
    //        divAddress.Visible = false;
    //        divEast.Visible = false;
    //        divWest.Visible = false;
    //        divsouth.Visible = false;
    //        divnorth.Visible = false;
    //        ClearData();
    //        return;
    //    }
        
    //}


    protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView(Convert.ToInt32(ddldoctype.SelectedValue));
        string selectedDocumentTypeName = ddldoctype.SelectedItem.Text;
        if (string.Equals(selectedDocumentTypeName, "Sale Deed", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(selectedDocumentTypeName, "Settlement Deed", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(selectedDocumentTypeName, "Sale Agreement", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(selectedDocumentTypeName, "Gift Deed", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(selectedDocumentTypeName, "Partition Deed", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(selectedDocumentTypeName, "Power Of Attorney", StringComparison.OrdinalIgnoreCase))
        {
            Divdate.Visible = true;
            divDocNo.Visible = true;
            divAddress.Visible = true;
            divDistrict.Visible = true;
            DivSurveyNo.Visible = true;
            divDivisionNo.Visible = true;
            divArea.Visible = true;
            divEast.Visible = true;
            divWest.Visible = true;
            divsouth.Visible = true;
            divnorth.Visible = true;
            divAttach.Visible = true;
            divbtnAdd.Visible = true;
            divEcNo.Visible = false;
            divFDate.Visible = false;
            divTDate.Visible = false;
           // divDoctypedatas.Visible = false;
            ClearData();
            return;
        }
        else if (string.Equals(selectedDocumentTypeName, "Patta", StringComparison.OrdinalIgnoreCase) ||
       string.Equals(selectedDocumentTypeName, "Chitta", StringComparison.OrdinalIgnoreCase) ||
       string.Equals(selectedDocumentTypeName, "Adangal", StringComparison.OrdinalIgnoreCase) ||
       string.Equals(selectedDocumentTypeName, "Property Tax Receipt", StringComparison.OrdinalIgnoreCase))
        {
            Divdate.Visible = true;
            divDocNo.Visible = true;
            divAddress.Visible = true;
            divDistrict.Visible = true;
            DivSurveyNo.Visible = true;
            divDivisionNo.Visible = true;
            divAttach.Visible = true;
            divbtnAdd.Visible = true;
            divArea.Visible = false;
            divEcNo.Visible = false;
            divFDate.Visible = false;
            divTDate.Visible = false;
          //  divDoctypedatas.Visible = false;
            divEast.Visible = false;
            divWest.Visible = false;
            divsouth.Visible = false;
            divnorth.Visible = false;
            ClearData();
            return;
        }
        else if (string.Equals(selectedDocumentTypeName, "Encumbarence Certificate", StringComparison.OrdinalIgnoreCase))
        {
            divEcNo.Visible = true;
            divFDate.Visible = true;
            divTDate.Visible = true;
            divAddress.Visible = true;
            divAttach.Visible = true;
            divbtnAdd.Visible = true;
            Divdate.Visible = false;
            divDocNo.Visible = false;
            divDistrict.Visible = false;
            DivSurveyNo.Visible = false;
            divDivisionNo.Visible = false;
           // divDoctypedatas.Visible = false;
            divEast.Visible = false;
            divWest.Visible = false;
            divsouth.Visible = false;
            divnorth.Visible = false;
            ClearData();
            return;
        }
        else
        {
          //  divDoctypedatas.Visible = true;
            Divdate.Visible = true;
            divDocNo.Visible = true;
            divDistrict.Visible = true;
            DivSurveyNo.Visible = true;
            divDivisionNo.Visible = true;
            divAttach.Visible = true;
            divbtnAdd.Visible = true;
            divEcNo.Visible = false;
            divFDate.Visible = false;
            divTDate.Visible = false;
            divAddress.Visible = false;
            divEast.Visible = false;
            divWest.Visible = false;
            divsouth.Visible = false;
            divnorth.Visible = false;
            ClearData();
            return;
        }

    }
   
    protected void btnEditDoc_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["FILE1"] = null;
        // lblFamilymsg.Text = string.Empty;
        ImageButton btnEdit = sender as ImageButton;
        ViewState["docid"] = int.Parse(btnEdit.CommandArgument);
        ViewState["action"] = "edit";
         ShowDetails();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            bool result;
            string filename = string.Empty;
            string FilePath = string.Empty;

            if (FileUpload1.HasFile)
            {

                string DOCFOLDER = file_path + "DOCUMENTANDSCANNING";

                //if (!System.IO.Directory.Exists(DOCFOLDER))
                //{
                //    System.IO.Directory.CreateDirectory(DOCFOLDER);

                //}
                string fileName = System.Guid.NewGuid().ToString() + FileUpload1.FileName.Substring(FileUpload1.FileName.IndexOf('.'));

                string contentType = contentType = FileUpload1.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                int docid = Convert.ToInt32(objCommon.LookUp("ADMN_DC_ASSEST_DOCUMENT_STORAGE", "(ISNULL(MAX(DOCID),0))+1 AS DOC_ID", ""));

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    DataTable dt1;
                    dt1 = ((DataTable)Session["Attachments"]);
                    int attachid = dt1.Rows.Count;

                    filename = docid + "_documentstorage_" + Session["userno"] + "-" + attachid;
                }
                else
                {
                    filename = docid + "_documentstorage_" + Session["userno"];
                }
                objDocType.FILENAME = filename;
                objDocType.FILEPTH = FileUpload1.FileName;

                    string filePath = file_path + "DOCUMENTANDSCANNING" + fileName;


                    if (FileUpload1.PostedFile.ContentLength <= 1024 * 10000)
                    {

                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, FileUpload1);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }

                             objDocType.FILEPTH = FileUpload1.FileName;


                        }
                        else
                        {
                            HttpPostedFile file = FileUpload1.PostedFile;
                            FileUpload1.SaveAs(filePath);

                              objDocType.FILEPTH =  file_path + "DOCUMENTANDSCANNING" + fileName;

                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                        return;
                    }

                    DataTable dt;

                    if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                    {
                        dt = ((DataTable)Session["Attachments"]);
                        DataRow dr = dt.NewRow();

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            if (result == true)
                            {
                                dr["FILE_NAME"] = filename + ext;
                            }
                            else
                            {
                                dr["FILE_NAME"] = FileUpload1.FileName;
                            }

                            dr["FILE_PATH"] =   objDocType.FILEPTH ;
                            dr["SIZE"] = (FileUpload1.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session["Attachments"] = dt;
                            this.BindListView_Attachments(dt);

                        }
                        else
                        {
                            dt = this.GetAttachmentDataTable();
                            dr = dt.NewRow();
                            dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            if (result == true)
                            {
                                dr["FILE_NAME"] = filename + ext;
                            }
                            else
                            {
                                dr["FILE_NAME"] = FileUpload1.FileName;
                            }
                            dr["FILE_PATH"] = objDocType.FILEPTH;
                            dr["SIZE"] = (FileUpload1.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session.Add("Attachments", dt);
                            this.BindListView_Attachments(dt);
                        }
                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        DataRow dr = dt.NewRow();
                        dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (result == true)
                        {
                            dr["FILE_NAME"] = filename + ext;
                        }
                        else
                        {
                            dr["FILE_NAME"] = FileUpload1.FileName;
                        }
                        dr["FILE_PATH"] = objDocType.FILEPTH;
                        dr["SIZE"] = (FileUpload1.PostedFile.ContentLength);
                        dt.Rows.Add(dr);
                        Session.Add("Attachments", dt);
                        this.BindListView_Attachments(dt);
                    }
                    
                }

            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private DataTable GetAttachmentDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ATTACH_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("FILE_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FILE_PATH", typeof(string)));
        dt.Columns.Add(new DataColumn("SIZE", typeof(int)));
        return dt;
    }

    protected void imgFile_Click(object sender, ImageClickEventArgs e)
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
            string directoryName = "~/DOCUMENTANDSCANNING" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                // ltEmbed.Text = "Image Not Found....!";


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DOCUMENTANDSCANNING/" + ImageName));

                hdnfilename.Value = filePath;
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void BTNCLOSE_Click(object sender, EventArgs e)
    {

        string directoryPath = Server.MapPath("~/DOCUMENTANDSCANNING/");

        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                if (file == hdnfilename.Value)
                {
                    File.Delete(file);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                }
            }
        }
    }
    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = Convert.ToInt32(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                if (dt.Rows.Count == 0)
                {
                    lvCompAttach.DataSource = string.Empty;
                    lvCompAttach.DataBind();
                }
                else
                {
                    this.BindListView_Attachments(dt);
                }
            }

            //to permanently delete from database in case of Edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                string count = objCommon.LookUp("ADMN_DC_ASSEST_DOCUMENT_STORAGE", "DOCID,ATTACH_ID", "DOCID =" + Convert.ToInt32(ViewState["docid"]) + "AND ATTACH_ID=" + fileId);
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ADMN_DC_ASSEST_DOCUMENT_STORAGE", "DOCID =" + Convert.ToInt32(ViewState["docid"]) + "AND ATTACH_ID=" + fileId);
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ATTACH_ID"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return dataRow;
    }

    protected void ShowDetails()
    {
        objDocType.DOCID = Convert.ToInt32(ViewState["docid"]);
        DataSet ds = objDocC.RetrieveDocumentDetails(objDocType);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldoctype.SelectedValue = ds.Tables[0].Rows[0]["DOCTYPE"].ToString();
            txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
            txtdocnumber.Text = ds.Tables[0].Rows[0]["DNO"].ToString();
            txtpropAddress.Text = ds.Tables[0].Rows[0]["PROPADDRESS"].ToString();
            txtdistrict.Text = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
            txtsurveyNumber.Text = ds.Tables[0].Rows[0]["SURVEYNO"].ToString();
            txtsubdivnum.Text = ds.Tables[0].Rows[0]["SUBDIVINO"].ToString();
            txtarea.Text = ds.Tables[0].Rows[0]["TOTAREA"].ToString();
            txtnorth.Text = ds.Tables[0].Rows[0]["NORTH_SQ_FT"].ToString();
            txtSouth.Text = ds.Tables[0].Rows[0]["SOUTH_SQ_FT"].ToString();
            txtEast.Text = ds.Tables[0].Rows[0]["EAST_SQ_FT"].ToString();
            txtWest.Text = ds.Tables[0].Rows[0]["WEST_SQ_FT"].ToString();
            txtEC.Text = ds.Tables[0].Rows[0]["ECNO"].ToString();
           // txtdoctype.Text = ds.Tables[0].Rows[0]["OTHERDOCTYPE"].ToString();
            txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
            txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();

                DataTable dt = new DataTable();

                dt = this.GetAttachmentDataTable();
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {

                    DataRow dr = dt.NewRow();
                    dr["ATTACH_ID"] = ds.Tables[0].Rows[j]["ATTACH_ID"];

                    dr["FILE_NAME"] = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                    dr["FILE_PATH"] = ds.Tables[0].Rows[j]["FILE_PATH"].ToString();
                    dr["SIZE"] = ds.Tables[0].Rows[j]["SIZE"];
                    dt.Rows.Add(dr);
                    Session["Attachments"] = dt;
                    this.BindListView_Attachments(dt);
                }


                    int blob;
                    blob = Convert.ToInt32(ds.Tables[0].Rows[0]["ISBLOB"]);


                    if (blob == 1)
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

            else
            {
                lvCompAttach.DataSource = null;
                lvCompAttach.DataBind();
            }  
        
    }

    protected void lvDocStorage_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;
            //string selectedDocumentType = rowView["DOCUMENT_TYPE"].ToString();

            HtmlTableCell tdECNo = (HtmlTableCell)e.Item.FindControl("tdECNo");
            HtmlTableCell thECNo = (HtmlTableCell)e.Item.FindControl("thECNo");
            HtmlTableCell thDocNo = (HtmlTableCell)e.Item.FindControl("thDocNo");
            HtmlTableCell thDistrict = (HtmlTableCell)e.Item.FindControl("thDistrict");
            HtmlTableCell thSurNo = (HtmlTableCell)e.Item.FindControl("thSurNo");
            HtmlTableCell tdSurNo = (HtmlTableCell)e.Item.FindControl("tdSurNo");
            HtmlTableCell tdDistrict = (HtmlTableCell)e.Item.FindControl("tdDistrict");
            HtmlTableCell tdDocNo = (HtmlTableCell)e.Item.FindControl("tdDocNo");
            // Your condition to show or hide the EC No. column
            string selectedDocumentTypeName = ddldoctype.SelectedItem.Text;

        }
    }
}