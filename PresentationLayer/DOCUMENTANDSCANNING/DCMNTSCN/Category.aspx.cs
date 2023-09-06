//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : DOCUMENT & SCANING
// PAGE NAME     : DCMNTSCN_Category.ASPX                                                    
// CREATION DATE : 18-JAN-2011                                                        
// CREATED BY    : PRAKASH RADHWANI 
// MODIFIED DATE : 22-JAN-2015
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DESC : CHANGE THE NAME OF ALL THE TABLES USED IN FILLDROPDOWN.
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;



public partial class DCMNTSCN_Category : System.Web.UI.Page
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
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        CreateChildControls();
    }

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
                BindConrols();
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                BindListView();
           }
        }
    }
    private void BindConrols()
    {
        objCommon.FillDropDownList(ddlCategory, "ADMN_DC_DOCUMENT_TYPE_DOC", "DNO", "DOCUMENTNAME", "DNO>0", "DNO");
    }
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int no = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objCommon.DeleteRow("ADMN_DC_DOCUMENT_TYPE_DOC", "dno=" + Convert.ToInt32(no));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record deleted successfully.", this.Page);
                Clear();
                BindListView();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DCMNTSCN_Category.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int no = int.Parse(btnEdit.CommandArgument);
        ViewState["action"] = "edit";
        Session["DNO"] = Convert.ToInt32(no);
        ShowDetails(no);
    }

  private void ShowDetails(int no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ADMN_DC_DOCUMENT_TYPE_DOC", "SUB_HEAD", "DOCUMENTNAME", "DNO=" + Convert.ToInt32(no), "");
            if (ds.Tables[0].Rows.Count>0)
            {
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["SUB_HEAD"].ToString();
                txtTitle.Text=ds.Tables[0].Rows[0]["DOCUMENTNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            
             if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DCMNTSCN_Category.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
   private void Clear()
    {
        ddlCategory.SelectedIndex = 0;
        txtTitle.Text = string.Empty;
    }

    private string ValCategory()
    {
        string msg = string.Empty;

        string title = objCommon.LookUp("ADMN_DC_DOCUMENT_TYPE_DOC", "DOCUMENTNAME", "LOWER(DOCUMENTNAME) LIKE '" + txtTitle.Text + "' AND SUB_HEAD=" + ddlCategory.SelectedValue);
        if (title != string.Empty)
            msg = "Category already exist please specify a different name";
        return msg;
    }
   protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitle.Text.Equals(""))
            {
                objCommon.DisplayMessage("Please Enter Category.", this.Page);
                return;
            }
            if (ValCategory() != string.Empty)
            {
                objCommon.DisplayMessage(ValCategory(), this);
                return;
            }

            obj.SUB_HEAD = Convert.ToInt32(ddlCategory.SelectedValue);
            obj.CAT_NAME = txtTitle.Text.Trim();
            obj.COLLEGE_CODE = Session["colcode"].ToString();
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            
            if (ViewState["action"] != null)
            {
                
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)(objC.AddCategory(obj));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                        
                       ViewState["action"] = "add";
                                       
                        string path = Server.MapPath("../DOCUMENTANDSCANNING");
                        string[]ff =path.Split('\\');
                        string mpath = "";
                        for (int i = 0; i < ff.Length-1; i++)
                        {
                            mpath = mpath + "\\" + ff[i]; 
                        }
                        mpath = mpath + "\\FILEUPLOAD\\";
                        mpath = mpath.Substring(1);
                        path = mpath;
                        DirectoryInfo d = new DirectoryInfo(path);
                     
                        string path1 = path;
                      DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlCategory.SelectedValue));
                      if (ds.Tables[0].Rows.Count > 0)
                      {
                          path1 = path1 + ds.Tables[0].Rows[0]["PATH"].ToString();
                      }

                      DirectoryInfo dSub = new DirectoryInfo(path1);
                      dSub.CreateSubdirectory(@""+txtTitle.Text.Trim()+"");
                      BindConrols();
                      BindListView();
                      Clear();

                    }
                }
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    obj.DNO = Convert.ToInt32(Session["DNO"].ToString());
                    CustomStatus cs = (CustomStatus)objC.UpdateCategory(obj);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage("Record Updated successfully.", this.Page);
                        Clear();
                        ViewState["action"] = "add";
                        BindConrols();
                        BindListView();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DCMNTSCN_Category.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListView()
    {
        try
        {
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            DataSet ds = objC.Retrieve_Category_ByUANO(obj);
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
            
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DCMNTSCN_Category.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Clear();
    }
     
}
