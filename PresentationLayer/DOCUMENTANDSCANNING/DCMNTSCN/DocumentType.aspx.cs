using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DOCUMENTANDSCANNING_DCMNTSCN_DocumentType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DocumentType objDocType = new DocumentType();
    DocumentTypeController objDocC = new DocumentTypeController();

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
            CheckPageAuthorization();
                //Set the Page Title
            Page.Title = Session["coll_name"].ToString();
            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
               
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                BindListView();

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



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objDocType.DOCID = 0;
            objDocType.DOCUMENTTYPE = txtdoctype.Text.Trim();

            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataSet ds = objCommon.FillDropDown("ADMN_DC_DOCUMENT_STORAGE_DOCUMENT_TYPE", "DOC_ID", "ADMN_DC_DOCUMENT_STORAGE_DOCUMENT_TYP", "DOCUMENT_TYPE='" + txtdoctype.Text + "' AND DOC_ID!=" + Convert.ToInt32(ViewState["docid"]), "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayMessage("Record Already Exist.", this.Page);
                        Clear();
                        return;
                    }
                    CustomStatus cs = (CustomStatus)(objDocC.AddDocumentType(objDocType));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Save Successfully.", this.Page);

                        ViewState["action"] = "add";
                        BindListView();
                        Clear();

                    }
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        DataSet ds = objCommon.FillDropDown("ADMN_DC_DOCUMENT_STORAGE_DOCUMENT_TYPE", "DOC_ID", "DOCUMENT_TYPE", "DOCUMENT_TYPE='" + txtdoctype.Text + "' AND DOC_ID!=" + Convert.ToInt32(ViewState["docid"]), "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage("Record Already Exist.", this.Page);
                            Clear();
                            return;
                        }
                        objDocType.DOCID = Convert.ToInt32(ViewState["docid"]);
                        CustomStatus cs = (CustomStatus)objDocC.AddDocumentType(objDocType);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("Record Updated successfully.", this.Page);
                            Clear();
                            ViewState["action"] = "add";
                            BindListView();
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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        ViewState["docid"] = int.Parse(btnEdit.CommandArgument);
        ViewState["action"] = "edit";
        ShowData();
    }


    private void ShowData()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ADMN_DC_DOCUMENT_STORAGE_DOCUMENT_TYPE", "DOC_ID", "DOCUMENT_TYPE", "DOC_ID=" + Convert.ToInt32(ViewState["docid"]), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtdoctype.Text = ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Clear()
    {
        txtdoctype.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void BindListView()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ADMN_DC_DOCUMENT_STORAGE_DOCUMENT_TYPE", "DOC_ID", "DOCUMENT_TYPE", "", "DOC_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDoc.DataSource = ds;
                lvDoc.DataBind();
            }
            else
            {
                lvDoc.DataSource = null;
                lvDoc.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
           
        }
    }
}