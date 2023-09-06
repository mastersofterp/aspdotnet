//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : TO CREATE HELP DESCRIPTION                                      
// CREATION DATE : 14-April-2009                                                   
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class createHelp : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                CheckPageAuthorization();

                //Load Page Help

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Bind the ListView with Notice
                BindListViewHelp();
                PopulateDropDownList();

                if (Request.QueryString["action"] == null)
                {
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                }
                else
                {
                    if (Request.QueryString["action"].ToString().Equals("add"))
                    {   //add
                        pnlAdd.Visible = true;
                        pnlList.Visible = false;
                    }
                    else
                    {   //edit
                        if (Request.QueryString["helpid"] != null)
                        {
                            int helpid = Convert.ToInt32(Request.QueryString["helpid"].ToString());
                            ShowDetail(helpid);
                            pnlAdd.Visible = true;
                            pnlList.Visible = false;
                        }
                    }
                }
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLACCESS_LINKS");
            ddlPage.DataSource = ds;
            ddlPage.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlPage.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlPage.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Help.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewHelp()
    {
        try
        {
            HelpController objHc = new HelpController();
            DataSet dsHelp = objHc.GetAllHelp();

            if (dsHelp.Tables[0].Rows.Count > 0)
            {
                lvHelp.DataSource = dsHelp;
                lvHelp.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Help.BindListViewHelp-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int helpid = int.Parse(btnDel.CommandArgument);

            HelpController objHC = new HelpController();

            CustomStatus cs = (CustomStatus)objHC.Delete(helpid);
            if (cs.Equals(CustomStatus.RecordDeleted))
                Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Help.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int helpid = int.Parse(btnEdit.CommandArgument);

            ShowDetail(helpid);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            string pageurl = Request.Url.ToString() + "&action=edit&helpid=" + helpid;
            Response.Redirect(pageurl);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Help.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int helpid)
    {
        try
        {
            HelpController objHc = new HelpController();
            SqlDataReader dr = objHc.GetSingleHelp(helpid);
            //Show Help Detail
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtHelpDesc.Text = dr["HELPDESC"] == null ? "" : dr["HELPDESC"].ToString();
                    ddlPage.Text = dr["Hal_No"] == null ? "" : dr["Hal_No"].ToString();
                }
            }
            if (dr != null) dr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Help.ShowDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString() + "&action=add");
        ViewState["action"] = "add";
        txtHelpDesc.Text = "";
        //ddlPage.Text = "";
        pnlAdd.Visible = true;
        pnlList.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            HelpController objHc = new HelpController();
            Help objHelp = new Help();

            objHelp.Hal_No = Convert.ToInt32(ddlPage.SelectedValue);
            objHelp.HelpDesc = txtHelpDesc.Text.Trim();

            //Check whether to add or update
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objHc.Add(objHelp);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    }
                }
                else
                {
                    //Edit
                    if (Request.QueryString["helpid"] != null)
                    {
                        objHelp.HelpId = Convert.ToInt32(Request.QueryString["helpid"].ToString());

                        CustomStatus cs = (CustomStatus)objHc.Update(objHelp);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Help.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewHelp();
    }

    //protected void lvHelp_Sorting(object sender, ListViewSortEventArgs e)
    //{
    //    if (ViewState["order"].ToString() == "ASC")
    //    {
    //        ViewState["order"] = "DESC";
    //        e.SortDirection = SortDirection.Descending;
    //    }
    //    else
    //    {
    //        ViewState["order"] = "ASC";
    //        e.SortDirection = SortDirection.Ascending;
    //    }

    //    ViewState["sort"] = e.SortExpression.ToString().ToLower();
    //    BindData(e.SortExpression.ToString().ToLower());
    //}

}