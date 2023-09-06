using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_DocumentMapping : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            ViewState["action"] = "add";
        }
        BindListView();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DocumentMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DocumentMapping.aspx");
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int status = chkActive.Checked ? 1 : 0;
        int ck = objmp.AddDocument(Convert.ToInt32(ddlDocument.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlAdm.SelectedValue), status, Convert.ToInt32(Session["OrgId"]));
        if (ck == 1)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Sucessfully.", this.Page);
            Clear();
            BindListView();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Record already Exist.", this.Page);
            return;
        }
    }
    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);
        int output = objmp.deleteDocument(srno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Information Deleted Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this.updGradeEntry, " Information Is Not Deleted ", this.Page);
        }
        BindListView();
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objmp.GetDocumentMapping();            
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                lvlist.Visible = true;
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
                lvlist.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_DocumentMapping.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlAdm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdm.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                ddlDegree.Focus();
            }            
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDocument, "ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO>0", "DOCUMENTNO");
                ddlDocument.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void Clear()
    {
        ddlAdm.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlDocument.Items.Clear();
        ddlDocument.Items.Add(new ListItem("Please Select","0"));
        chkActive.Checked=false;
    }
}