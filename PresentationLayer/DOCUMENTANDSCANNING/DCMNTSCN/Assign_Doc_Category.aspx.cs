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
using IITMS.UAIMS;

public partial class DOCUMENTANDSCANNING_DCMNTSCN_Assign_Doc_Category : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DocumentController objC = new DocumentController();
    //ConnectionStrings
    private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                  //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //else
                    //lblHelp.Text = "No Help Added";

                //Fill the TreeView with Links
                //Fill_TreeLinks(tvLinks, string.Empty);

                //Populate the dropdownlist with user types
                PopulateDropDownList();
                PopulateRoot();
                tv.Attributes.Add("onclick", "OnTreeClick(event)");
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void BindListView(int usertype)
    {
        try
        {
            User_AccController objUACC = new User_AccController();
            DataSet ds = objUACC.GetUsersByUserType(usertype);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvUsers.DataSource = ds;
                    lvUsers.DataBind();
                    pnlListMain.Visible = true;

                    //Fill the treeview
                    object links = objUACC.GetUserLinksByUserType(usertype);
                    //Fill_TreeLinks(tvLinks, links == null ? string.Empty : links.ToString());
                    //tvLinks.ExpandAll();
                    hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lvUsers.DataSource = null;
                    lvUsers.DataBind();
                    pnlListMain.Visible = false;
                    hdfTot.Value = "0";
                }
            }
            else
            {
                lvUsers.DataSource = null;
                lvUsers.DataBind();
                pnlListMain.Visible = false;
                hdfTot.Value = "0";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "assign_link.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    //public void Fill_TreeLinks(TreeView tvLinks, string links)
    //{
    //    SqlDataReader drLinks = null;

    //    int linkno = 0;
    //    int mastno = 0;
    //    int url_idno = 0;
    //    TreeNode xx = null;
    //    TreeNode yy = null;
    //    TreeNode zz = null;

    //    tvLinks.Nodes.Clear();

    //    try
    //    {
    //        SQLHelper objSH = new SQLHelper(uaims_constr);
    //        SqlParameter[] objParams = new SqlParameter[0];

    //        //Get all user links
    //        drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS", objParams);

    //        //loop thru links
    //        while (drLinks.Read())
    //        {
    //            if (drLinks["al_link"].ToString().Trim() != "")
    //            {
    //                if (drLinks["url_idno"] != null & drLinks["url_idno"].ToString() != "")
    //                    url_idno = int.Parse(drLinks["url_idno"].ToString());

    //                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != "" & int.Parse(drLinks["al_asno"].ToString()) != linkno)
    //                {
    //                    xx = new TreeNode();  // this is defination of the node.
    //                    xx.Text = drLinks["as_title"].ToString();
    //                    xx.NavigateUrl = "";
    //                    xx.SelectAction = TreeNodeSelectAction.Expand;

    //                    // adding node to root
    //                    tvLinks.Nodes.Add(xx);

    //                    if (drLinks["al_url"].ToString().Trim() == "")
    //                    {
    //                        zz = new TreeNode();    // this is defination of the node.
    //                        zz.Text = drLinks["al_link"].ToString();
    //                        zz.Value = drLinks["al_no"].ToString();
    //                        zz.ShowCheckBox = true;
    //                        zz.NavigateUrl = "";
    //                        zz.SelectAction = TreeNodeSelectAction.Expand;

    //                        if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
    //                            zz.Checked = true;

    //                        mastno = int.Parse(drLinks["mastno"].ToString());
    //                        // adding node as child of node xx.
    //                        xx.ChildNodes.Add(zz);
    //                    }
    //                    else
    //                    {
    //                        yy = new TreeNode();
    //                        yy.Text = drLinks["al_link"].ToString();
    //                        yy.Value = drLinks["al_no"].ToString();
    //                        yy.ShowCheckBox = true;

    //                        if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
    //                            yy.Checked = true;

    //                        if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != ""))
    //                        {
    //                            if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
    //                            {
    //                                // adding node as child of node xx.
    //                                zz.ChildNodes.Add(yy);
    //                            }
    //                            else
    //                            {
    //                                // adding node as child of node xx.
    //                                xx.ChildNodes.Add(yy);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            // adding node as child of node xx.
    //                            xx.ChildNodes.Add(yy);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    if (drLinks["al_url"].ToString() == "")
    //                    {
    //                        zz = new TreeNode();   // this is defination of the node.
    //                        zz.Text = drLinks["al_link"].ToString();
    //                        zz.ShowCheckBox = true;
    //                        zz.Value = drLinks["al_no"].ToString();
    //                        zz.NavigateUrl = "";
    //                        zz.SelectAction = TreeNodeSelectAction.Expand;

    //                        if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
    //                            zz.Checked = true;

    //                        if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != "")
    //                            mastno = int.Parse(drLinks["mastno"].ToString());

    //                        // adding node as child of node xx.
    //                        xx.ChildNodes.Add(zz);
    //                    }
    //                    else
    //                    {
    //                        yy = new TreeNode();
    //                        yy.Text = drLinks["al_link"].ToString();
    //                        yy.Value = drLinks["al_no"].ToString();
    //                        yy.ShowCheckBox = true;
    //                        //if (links.Contains(drLinks["al_no"].ToString())) 
    //                        if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
    //                            yy.Checked = true;

    //                        if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != ""))
    //                        {
    //                            if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
    //                            {
    //                                // adding node as child of node xx.
    //                                zz.ChildNodes.Add(yy);
    //                            }
    //                            else
    //                            {
    //                                // adding node as child of node xx.
    //                                xx.ChildNodes.Add(yy);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            // adding node as child of node xx.
    //                            xx.ChildNodes.Add(yy);
    //                        }
    //                    }
    //                }
    //            }

    //            if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != "")
    //                linkno = int.Parse(drLinks["al_asno"].ToString());
    //            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != "")
    //                mastno = int.Parse(drLinks["mastno"].ToString());
    //        }

    //        //Change Password for All
    //        xx = new TreeNode();
    //        //xx.ID = "ChPw";
    //        xx.Text = "Change Password";
    //        xx.Target = "main";
    //        xx.NavigateUrl = "changepassword.aspx?pageno=500";
    //        tvLinks.Nodes.Add(xx);

    //        //Log Out for All
    //        xx = new TreeNode();
    //        //xx.ID = "lout"
    //        xx.Text = "Logout";
    //        xx.Target = "_parent";
    //        xx.NavigateUrl = "logout.aspx";
    //        tvLinks.Nodes.Add(xx);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "user_rights.Page_Load-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    finally
    //    {
    //        //close all objects
    //        if (drLinks != null) drLinks.Close();
    //    }
    //}

    /// <summary>
    /// Populates the Domain DropDownList
    /// </summary>
    private void PopulateDropDownList()
    {
        DataSet ds = objCommon.GetDropDownData("PKG_USER_ACC_SP_RET_USERTYPES");
        ddlUserType.DataSource = ds;
        ddlUserType.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddlUserType.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddlUserType.DataBind();
    }

    public bool SearchLink(string lnks, string linkno)
    {
        char sp = ',';  //separator
        string[] links = lnks.Split(sp);

        for (int i = 0; i < links.Length; i++)
        {
            if (links[i].Equals(linkno))
                return true;
        }

        return false;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView(Convert.ToInt32(ddlUserType.SelectedValue));
        ViewState["EditUserId"] = ddlUserType.SelectedValue;
        PopulateRoot();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lvUsers.DataSource = null;
        lvUsers.DataBind();
        pnlListMain.Visible = false;
        ddlUserType.SelectedIndex = 0;
        hdfTot.Value = "0";
        ViewState["EditUserId"] = null;
        PopulateRoot();
        tv.CollapseAll();

    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            User_AccController objUACC = new User_AccController();
            string uanos = string.Empty;
           // string links = objCommon.GetLinks(tvLinks);
            string links = objCommon.GetCatLinks(tv);
            //get selected users
            foreach (ListViewDataItem lvItem in lvUsers.Items)
            {
                CheckBox chkAccept = lvItem.FindControl("chkAccept") as CheckBox;
                if (chkAccept.Checked == true)
                    uanos += chkAccept.ToolTip + ",";
            }

            CustomStatus cs = (CustomStatus)objUACC.UpdateUserCategory(Convert.ToInt32(ddlUserType.SelectedValue), links, uanos);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                Clear();
                ShowMessage("Category Assigned to user Successfully!!!");
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "assign_link.btnAssign_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createexamname.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createexamname.aspx");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    //#region Populate Category Tree
    //private void PopulateRoot()
    //{
    //    try
    //    {
    //        tv.Nodes.Clear();
    //        DataSet ds = objC.PopulateTree(0);
    //        DataTable dt = new DataTable();
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            dt.Columns.Add(new DataColumn("DNO", typeof(int)));
    //            dt.Columns.Add(new DataColumn("DOCUMENTNAME", typeof(string)));
    //            dt.Columns.Add(new DataColumn("CHILDNODECOUNT", typeof(int)));

    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                DataRow dr = dt.NewRow();
    //                dr["DNO"] = ds.Tables[0].Rows[i]["DNO"].ToString();
    //                dr["DOCUMENTNAME"] = ds.Tables[0].Rows[i]["DOCUMENTNAME"].ToString();
    //                dr["CHILDNODECOUNT"] = ds.Tables[0].Rows[i]["CHILDNODECOUNT"].ToString();
    //                dt.Rows.Add(dr);
    //            }
    //        }
    //        PopulatNode(dt, tv.Nodes);

    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.PopulateRoot --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}

    //private void PopulatNode(DataTable dt, TreeNodeCollection nd)
    //{
    //    try
    //    {
    //        string catLinks = string.Empty;
    //        User_AccController objACC = new User_AccController();
    //        DataTableReader dtr = objACC.GetUserByUANo(Convert.ToInt32(ViewState["EditUserId"]));
    //        if (dtr != null)
    //        {
    //            if (dtr.Read())
    //            {
    //                catLinks = dtr["UA_CAT"] == String.Empty ? "" : dtr["UA_CAT"].ToString();
    //            }
    //        }

    //        foreach (DataRow d in dt.Rows)
    //        {
    //            TreeNode x = new TreeNode();
    //            x.Text = d["DOCUMENTNAME"].ToString();
    //            x.Value = d["DNO"].ToString();
    //            //x.ShowCheckBox = true;
    //            //x.SelectAction = TreeNodeSelectAction.Select;
    //            nd.Add(x);
    //            if (!catLinks.Equals(string.Empty) && SearchLink(catLinks, x.Value.ToString()))
    //                x.Checked = true;
    //            x.PopulateOnDemand = (Convert.ToInt32(d["CHILDNODECOUNT"]) > 0);
    //            x.CollapseAll();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.PopulatNode --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}
    //// ON LIST VIEW DATAITEM DATABOUND
    //protected void pp(object sender, TreeNodeEventArgs e)
    //{
    //    try
    //    {
    //        PopulateChild(Convert.ToInt32(e.Node.Value), e.Node);
    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.pp --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}
    //private void PopulateChild(int pid, TreeNode pnode)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        DataSet ds = objC.PopulateChild(pid, 0);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            dt.Columns.Add(new DataColumn("DNO", typeof(int)));
    //            dt.Columns.Add(new DataColumn("DOCUMENTNAME", typeof(string)));
    //            dt.Columns.Add(new DataColumn("CHILDNODECOUNT", typeof(int)));

    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                DataRow dr = dt.NewRow();
    //                dr["DNO"] = ds.Tables[0].Rows[i]["DNO"].ToString();
    //                dr["DOCUMENTNAME"] = ds.Tables[0].Rows[i]["DOCUMENTNAME"].ToString();
    //                dr["CHILDNODECOUNT"] = ds.Tables[0].Rows[i]["CHILDNODECOUNT"].ToString();
    //                dt.Rows.Add(dr);

    //            }
    //        }
    //        PopulatNode(dt, pnode.ChildNodes);
    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.PopulateChild --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}


    //protected void tv_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    //{
    //    if (e.Node.Checked)
    //    {
    //        if (e.Node.ChildNodes.Count > 0)
    //        {
    //            for (int i = 0; i < e.Node.ChildNodes.Count; i++)
    //            {
    //                e.Node.Checked = true;
    //            }
    //        }
    //    }
    //}
    //#endregion

    #region Populate Category Tree
    private void PopulateRoot()
    {
        try
        {
            tv.Nodes.Clear();
            DataSet ds = objC.PopulateTree(0);
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
                objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.PopulateRoot --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void PopulatNode(DataTable dt, TreeNodeCollection nd)
    {
        try
        {
            string catLinks = string.Empty;
            User_AccController objACC = new User_AccController();
            DataTableReader dtr = objACC.GetUsertype(Convert.ToInt32(ViewState["EditUserId"]));
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    catLinks = dtr["UA_CAT"] == String.Empty ? "" : dtr["UA_CAT"].ToString();
                }
            }

            foreach (DataRow d in dt.Rows)
            {
                TreeNode x = new TreeNode();
                x.Text = d["DOCUMENTNAME"].ToString();
                x.Value = d["DNO"].ToString();
                //x.ShowCheckBox = true;
                //x.SelectAction = TreeNodeSelectAction.Select;
                nd.Add(x);
                if (!catLinks.Equals(string.Empty) && SearchLink(catLinks, x.Value.ToString()))
                    x.Checked = true;
                x.PopulateOnDemand = (Convert.ToInt32(d["CHILDNODECOUNT"]) > 0);
                //x.CollapseAll();
                x.ExpandAll();
                x.CollapseAll();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.PopulatNode --> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.pp --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void PopulateChild(int pid, TreeNode pnode)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = objC.PopulateChild(pid, 0);
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
                objUCommon.ShowError(Page, "DCMNTSCN_CreateUser.PopulateChild --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void tv_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Checked)
        {
            if (e.Node.ChildNodes.Count > 0)
            {
                for (int i = 0; i < e.Node.ChildNodes.Count; i++)
                {
                    e.Node.Checked = true;
                }
            }

            //if (e.Node.Parent.p)
            //{
            //    for (int i = 0; i < e.Node.ChildNodes.Count; i++)
            //    {
            //        e.Node.Checked = true;
            //    }
            //}
        }
    }
    #endregion


   


}
