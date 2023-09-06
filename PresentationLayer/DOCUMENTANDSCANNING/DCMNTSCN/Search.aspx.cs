using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.UI.WebControls.Adapters;


using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class DCMNTSCN_Search : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Documentation obj = new Documentation();
    DocumentController objC = new DocumentController();

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 

                //Page Authorization
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
                pnlDetails.Visible = false;
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                PopulateRoot();

            }
        }
        else
        {
            
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
     {
        try
        {
            if (rbtn.SelectedValue == "0")
            {
                //if (hfSearch.Value == string.Empty)
                    if (txtSearch.Text.Trim() == string.Empty)
                {
                    objCommon.DisplayMessage("No Data Found", this.Page);
                    clear();
                    return;
                }
                obj.TYPE = 0;
               // obj.UPLNO = Convert.ToInt32(hfSearch.Value);
                obj.TITLE = txtSearch.Text.Trim();
                BindListview(obj.TITLE, obj.TYPE);
            }
            else if (rbtn.SelectedValue == "2")
            {
                obj.KEYWORD = txtKey.Text.Trim();
                //obj.DEPARTMENTS = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["USERNO"]));
                obj.UA_NO = Convert.ToInt32(Session["USERNO"]);

                DataSet ds = new DataSet();
                ds = objC.SearchDocumentKeyword(obj);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvSearch.DataSource = ds;
                    lvSearch.DataBind();
                }
                else
                {
                    lvSearch.DataSource = null;
                    lvSearch.DataBind();
                }
                
            }
            pnlDetails.Visible = false;

            
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DCMNTSCN_Search.btnSearch_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindListview(string Title, int type)
    {
         try
         {
             obj.TITLE = Title;
            //obj.DEPARTMENTS = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["USERNO"]));
            obj.TYPE = Convert.ToInt32(type);
            obj.UA_NO = Convert.ToInt32(Session["USERNO"]);

            DataSet ds = new DataSet();
            ds = objC.SearchDocument(obj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSearch.DataSource = ds;
                lvSearch.DataBind();
            }
            else
            {
                lvSearch.DataSource = null;
                lvSearch.DataBind();
            }
         }
         catch (Exception ex)
         {
            if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "DCMNTSCN_Search.BindListview --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable");
         }
    }

    private void PopulateRoot()
    {
        try
        {
            DataSet ds = objC.PopulateTree(Convert.ToInt32(Session["USERNO"]));
            DataTable dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("DNO", typeof(int)));
                dt.Columns.Add(new DataColumn("DOCUMENTNAME", typeof(string)));
                dt.Columns.Add(new DataColumn("CHILDNODECOUNT", typeof(int)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["DNO"] = ds.Tables[0].Rows[i]["DNO"].ToString();
                    dr["DOCUMENTNAME"] = ds.Tables[0].Rows[i]["DOCUMENTNAME"].ToString();
                    dr["CHILDNODECOUNT"] = ds.Tables[0].Rows[i]["CHILDNODECOUNT"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            PopulatNode(dt, tv.Nodes);
        }
        catch (Exception ex)
        {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "DCMNTSCN_Search.PopulateRoot --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
   
    private void PopulatNode(DataTable dt, TreeNodeCollection nd)
    {
        try
        {
            foreach (DataRow d in dt.Rows)
            {
                TreeNode x = new TreeNode();
                x.Text = d["DOCUMENTNAME"].ToString();
                x.Value = d["DNO"].ToString();
                nd.Add(x);
                x.PopulateOnDemand = (Convert.ToInt32(d["CHILDNODECOUNT"])>0);
            }
        }
        catch (Exception ex)
        {
            
            if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "DCMNTSCN_Search.PopulatNode --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

   // ON LIST VIEW DATAITEM DATABOUND
    protected void pp(object sender, TreeNodeEventArgs e)
    {
        try
        {
            PopulateChild(Convert.ToInt32(e.Node.Value), e.Node);
        }
        catch (Exception ex)
        {
          if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "DCMNTSCN_Search.pp --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void PopulateChild(int pid, TreeNode pnode)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = objC.PopulateChild(pid,Convert.ToInt32(Session["USERNO"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("DNO", typeof(int)));
                dt.Columns.Add(new DataColumn("DOCUMENTNAME", typeof(string)));
                dt.Columns.Add(new DataColumn("CHILDNODECOUNT", typeof(int)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["DNO"] = ds.Tables[0].Rows[i]["DNO"].ToString();
                    dr["DOCUMENTNAME"] = ds.Tables[0].Rows[i]["DOCUMENTNAME"].ToString();
                    dr["CHILDNODECOUNT"] = ds.Tables[0].Rows[i]["CHILDNODECOUNT"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            PopulatNode(dt, pnode.ChildNodes);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "DCMNTSCN_Search.PopulateChild --> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button btnSelect = sender as Button;
        if (btnSelect.CommandArgument == string.Empty)
            return;
        obj.TITLE = txtSearch.Text.Trim();
        obj.UPLNO=  Convert.ToInt32(btnSelect.CommandArgument);
        //obj.DNO= Convert.ToInt32(objCommon.LookUp("acd_document_upload","dno","uplno="+Convert.ToInt32(no)));
        //obj.DEPARTMENTS = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["USERNO"]));
        obj.UA_NO = Convert.ToInt32(Session["USERNO"]);
        obj.TYPE = 0;
       
        DataSet ds = new DataSet();
        ds = objC.SearchDocumenttitlewise(obj);

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlDetails.Visible = true;
                lblCategory.Text = ds.Tables[0].Rows[0]["DOCUMENTNAME"].ToString();
                lblDate.Text = ds.Tables[0].Rows[0]["CREATED_DATE"].ToString() == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");
                lblDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                lblTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                lblUploadBy.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
            }
            obj.TYPE = 1;
            //DataSet d = objC.SearchDocument(obj);

            DataSet d = objC.SearchDocumentByUPDNo(obj);
            if (d.Tables[0].Rows.Count > 0)
            {
                //lvAttachments.DataSource = ds;
                lvAttachments.DataSource = d;
                lvAttachments.DataBind();
            }
            else
            {
                lvAttachments.DataSource = null;
                lvAttachments.DataBind();
            }
	        //BindListview(UPLNO);
    }

    private void clear()
    {
        pnlDetails.Visible = false;
        lvAttachments.DataSource = null;
        lvAttachments.DataBind();
        lvSearch.DataSource = null;
        lvSearch.DataBind();
        txtSearch.Text = string.Empty;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    //GET THE DOCUMENTS ON THE SELECTED NODE
    protected void tv_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //get the page link from tooltip of the selected node, so that we can redirect to it.
            
            obj.DNO = Convert.ToInt32(tv.SelectedNode.Value);
            //BindListview(obj.DNO,0);

            obj.TYPE = 0;
          //obj.DEPARTMENTS =objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["USERNO"]));
            obj.UA_NO = Convert.ToInt32(Session["USERNO"]);
            
            DataSet ds = new DataSet();
            ds = objC.SearchDocumentTreeView(obj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSearch.DataSource = ds;
                lvSearch.DataBind();
            }
            else
            {
                lvSearch.DataSource = null;
                lvSearch.DataBind();
            }
        }
        pnlDetails.Visible = false;
    }
    protected void rbtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtn.SelectedValue == "0")
        {
           // HtmlTableRow Tr_Main = (HtmlTableRow)this.Page.FindControl("TR_Main");
          
            TrCat.Visible = false;
            TrTitle.Visible = true;
            TrKey.Visible = false;
            clear();
            btnSearch.Visible = true;
        }
        else if (rbtn.SelectedValue == "1")
        {
            TrCat.Visible = true;
            TrTitle.Visible = false;
            TrKey.Visible = false;
            clear();
            btnSearch.Visible = false;
        }
        else
        {
            TrCat.Visible = false;
            TrTitle.Visible = false;
            TrKey.Visible = true;
            clear();
            btnSearch.Visible = true;
        }
    }
    
}
