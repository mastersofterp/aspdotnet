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

public partial class asssign_link : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                else
                {
                    //lblHelp.Text = "No Help Added";
                }
                //Fill the TreeView with Links
                Fill_TreeLinks(tvLinks, string.Empty);

                //Populate the dropdownlist with user types
                PopulateDropDownList();
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
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
                    Fill_TreeLinks(tvLinks, links == null ? string.Empty : links.ToString());
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

    /// <summary>
    /// This method Binds the Treeview with Links
    /// </summary>
    /// <param name="tvLinks"></param>
    /// <param name="links"></param>
    public void Fill_TreeLinks(TreeView tvLinks, string links)
    {
        SqlDataReader drLinks = null;

        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        TreeNode xx = null;
        TreeNode yy = null;
        TreeNode zz = null;
        TreeNode aa = null;

        tvLinks.Nodes.Clear();

        try
        {
            SQLHelper objSH = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];

            objParams[0] = new SqlParameter("@P_UA_NO", Session["userno"].ToString());
            //Get all user links
            drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS", objParams);

            //loop thru links
            while (drLinks.Read())
            {
                if (drLinks["al_link"].ToString().Trim() != "")
                {
                    if (drLinks["url_idno"] != null & drLinks["url_idno"].ToString() != "")
                        url_idno = int.Parse(drLinks["url_idno"].ToString());

                    if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != "" & int.Parse(drLinks["al_asno"].ToString()) != linkno)
                    {
                        xx = new TreeNode();  // this is defination of the node.
                        xx.Text = drLinks["as_title"].ToString();
                        xx.NavigateUrl = "";
                        xx.SelectAction = TreeNodeSelectAction.Expand;

                        // adding node to root
                        tvLinks.Nodes.Add(xx);

                        if (drLinks["al_url"].ToString().Trim() == "")
                        {
                            zz = new TreeNode();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.Value = drLinks["al_no"].ToString();
                            zz.ShowCheckBox = true;
                            zz.NavigateUrl = "";
                            zz.SelectAction = TreeNodeSelectAction.Expand;

                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                zz.Checked = true;

                            mastno = int.Parse(drLinks["mastno"].ToString());
                            // adding node as child of node xx.
                            xx.ChildNodes.Add(zz);
                        }
                        else
                        {
                            yy = new TreeNode();
                            yy.Text = drLinks["al_link"].ToString();
                            yy.Value = drLinks["al_no"].ToString();
                            yy.ShowCheckBox = true;

                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                yy.Checked = true;

                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != ""))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    // adding node as child of node xx.
                                    zz.ChildNodes.Add(yy);
                                }
                                else
                                {
                                    // adding node as child of node xx.
                                    xx.ChildNodes.Add(yy);
                                }
                            }
                            else
                            {
                                // adding node as child of node xx.
                                xx.ChildNodes.Add(yy);
                            }
                        }
                    }
                    else
                    {
                        if (drLinks["al_url"].ToString() == "")
                        {
                            if (int.Parse(drLinks["levelno"].ToString()) == 2)
                            {
                                aa = new TreeNode();
                                aa.Text = drLinks["al_link"].ToString();
                                aa.ShowCheckBox = true;
                                aa.Value = drLinks["al_no"].ToString();
                                aa.NavigateUrl = "";
                                aa.SelectAction = TreeNodeSelectAction.Expand;

                                if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                    aa.Checked = true;

                                zz.ChildNodes.Add(aa);
                            }
                            else
                            {
                                zz = new TreeNode();   // this is defination of the node.
                                zz.Text = drLinks["al_link"].ToString();
                                zz.ShowCheckBox = true;
                                zz.Value = drLinks["al_no"].ToString();
                                zz.NavigateUrl = "";
                                zz.SelectAction = TreeNodeSelectAction.Expand;

                                if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                    zz.Checked = true;

                                // adding node as child of node xx.
                                xx.ChildNodes.Add(zz);
                            }
                            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != "")
                                mastno = int.Parse(drLinks["mastno"].ToString());
                        }
                        else
                        {
                            yy = new TreeNode();
                            yy.Text = drLinks["al_link"].ToString();
                            yy.Value = drLinks["al_no"].ToString();
                            yy.ShowCheckBox = true;
                            //if (links.Contains(drLinks["al_no"].ToString())) 
                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                yy.Checked = true;

                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != ""))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    if (int.Parse(drLinks["levelno"].ToString()) == 3)
                                    {
                                        aa.ChildNodes.Add(yy);
                                    }
                                    else
                                    {
                                        // adding node as child of node xx.
                                        zz.ChildNodes.Add(yy);
                                    }
                                }
                                else
                                {
                                    if (int.Parse(drLinks["levelno"].ToString()) == 3)
                                    {
                                        aa.ChildNodes.Add(yy);
                                    }
                                    else if (int.Parse(drLinks["levelno"].ToString()) == 2)
                                    {
                                        zz.ChildNodes.Add(yy);
                                    }
                                    else
                                    {
                                        xx.ChildNodes.Add(yy);
                                    }
                                }
                            }
                            else
                            {
                                // adding node as child of node xx.
                                xx.ChildNodes.Add(yy);
                            }
                        }
                    }
                }

                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != "")
                    linkno = int.Parse(drLinks["al_asno"].ToString());
                if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != "")
                    mastno = int.Parse(drLinks["mastno"].ToString());
            }

            //Change Password for All
            xx = new TreeNode();
            //xx.ID = "ChPw";
            xx.Text = "Change Password";
            xx.Target = "main";
            xx.NavigateUrl = "changepassword.aspx?pageno=500";
            tvLinks.Nodes.Add(xx);

            //Log Out for All
            xx = new TreeNode();
            //xx.ID = "lout"
            xx.Text = "Logout";
            xx.Target = "_parent";
            xx.NavigateUrl = "logout.aspx";
            tvLinks.Nodes.Add(xx);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "user_rights.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            //close all objects
            if (drLinks != null) drLinks.Close();
        }
    }

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
        tvLinks.CollapseAll();

        User_AccController objACC = new User_AccController();
        DataTableReader dtr;

        dtr = objACC.GetUserByUANo(Convert.ToInt32(Session["userno"].ToString()));
        if (dtr != null)
        {
            if (dtr.Read())
            {
                Fill_TreeLinks(tvLinks, dtr["UA_ACC"] == DBNull.Value ? string.Empty : dtr["UA_ACC"].ToString());
            }
        }
        else
        {
            if (dtr.Read())
            {
                Session["userlinks"] = dtr["UA_ACC"].ToString();
                Fill_TreeLinks(tvLinks, dtr["UA_ACC"] == null ? string.Empty : dtr["UA_ACC"].ToString());
            }
        }
        dtr.Close();
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            User_AccController objUACC = new User_AccController();
            string uanos = string.Empty;
            string links = objCommon.GetLinks(tvLinks);

            //get selected users
            foreach (ListViewDataItem lvItem in lvUsers.Items)
            {
                CheckBox chkAccept = lvItem.FindControl("chkAccept") as CheckBox;
                if (chkAccept.Checked == true)
                    uanos += chkAccept.ToolTip + ",";
            }

            CustomStatus cs = (CustomStatus)objUACC.UpdateUserLinks(Convert.ToInt32(ddlUserType.SelectedValue), links, uanos);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                Clear();
                ShowMessage("User Links Updated Successfully!!!");
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
}
