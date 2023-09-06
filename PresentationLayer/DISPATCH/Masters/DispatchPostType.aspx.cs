using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

public partial class Dispatch_Masters_DispatchPostType : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DispatchPostType objposttype = new DispatchPostType();
    IOTRAN objio = new IOTRAN();
    DispatchPostTypeController objposttypecontroller = new DispatchPostTypeController();
    IOTranController objiocontroller = new IOTranController();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Bind the ListView with Case Types
                BindListViewPostType();

                //Disabled add panel
                pnlAdd.Visible = false;

                //Enabled listview panel
                pnlList.Visible = true;
            }
            trmsg.Visible = false;
        }

        else
        {
            trmsg.Visible = false;
        }
    }

    private void BindListViewPostType()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ADMN_IO_POST_TYPE", "*", "", "", "POSTTYPENO");
            lvPostType.DataSource = ds;
            lvPostType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LegalMatters_CaseType.BindListViewCaseType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            trmsg.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int posttype_no = int.Parse(btnEdit.CommandArgument);
            ShowDetails(posttype_no);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LegalMatters_CaseType.BindListViewCaseType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int posttype_no)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ADMN_IO_POST_TYPE", "*", "", "POSTTYPENO=" + posttype_no, "");

            //to show created user details
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ViewState["posttype_no"] = ds.Tables[0].Rows[0]["POSTTYPENO"].ToString();
                    txtPostTypeName.Text = ds.Tables[0].Rows[0]["POSTTYPENAME"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LegalMatters_CaseType.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public bool CheacKCode()
    {
        bool result = false;

        DataSet dsCode = new DataSet();
        dsCode = objCommon.FillDropDown("ADMN_IO_POST_TYPE", "POSTTYPENAME", "POSTTYPENO", "POSTTYPENAME='" + txtPostTypeName.Text + "'", "");
        if (dsCode.Tables[0].Rows.Count > 0)
        {
            result = true;
        }
        return result;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            if (CheacKCode())
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Record Already Exist", this.Page);
                txtPostTypeName.Text = string.Empty;
            }
            else
            {

                objposttype.PostType = txtPostTypeName.Text;
                objio.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                objio.CREATOR = (Session["userno"].ToString());
                objio.CREATED_DATE = System.DateTime.Now;

                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //Add New Case Type
                        CustomStatus cs = (CustomStatus)objposttypecontroller.AddUpdatePostType(objposttype, objio);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayUserMessage(UpdatePanel1, "Record Saved Successfully", this.Page);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                    else
                    {
                        //Edit
                        if (ViewState["posttype_no"] != null)
                        {
                            CustomStatus cs = (CustomStatus)objposttypecontroller.AddUpdatePostType(objposttype, objio);

                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayUserMessage(UpdatePanel1, "Record Updated Successfully", this.Page);
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                ViewState["action"] = null;
                                Clear();
                            }
                        }
                    }
                }

                //BindListViewCreateUser();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objUCommon.ShowError(Page, "Dispatch_PostType.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else

                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        ViewState["action"] = null;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain
        BindListViewPostType();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        trmsg.Visible = true;
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";
    }

    // clear method
    private void Clear()
    {
        txtPostTypeName.Text = "";
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=casetype.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=casetype.aspx");
        }
    }

}