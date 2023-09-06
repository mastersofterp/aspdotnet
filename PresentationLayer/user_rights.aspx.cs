//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : TO CREATE USER RIGHTS                                           
// CREATION DATE : 13-April-2009                                                   
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

public partial class user_rights : System.Web.UI.Page
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
                //Page Authorization
                CheckPageAuthorization();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                else
                {
                    //lblHelp.Text = "No Page Added";
                }
                    //Bind the ListView with User Types
                    BindListView();

                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"].ToString().Equals("add"))
                    {
                        //Bind the TreeView
                        Fill_TreeLinks(trvLinks, string.Empty);
                        pnlAdd.Visible = true;
                        pnlList.Visible = false;
                    }
                    else
                    {
                        if (Request.QueryString["usertypeid"] != null)
                        {
                            int usertypeid = Convert.ToInt32(Request.QueryString["usertypeid"].ToString());
                            ShowDetails(usertypeid);
                            pnlAdd.Visible = true;
                            pnlList.Visible = false;
                        }
                    }
                }
                else
                {
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;  
                }
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
        }
    }

    private void BindListView()
    {
        try
        {
            UserRightsController objURC = new UserRightsController();
            DataSet ds = objURC.GetAllUserRights();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvUserTypes.DataSource = ds;
                lvUserTypes.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "user_rights.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Check whether to add or update
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].ToString().Equals("add"))
                {
                    //get the selected links from the treeview
                    string links = objCommon.GetLinks(trvLinks);

                    //Set all properties
                    UserRightsController objURC = new UserRightsController();

                    UserRights objUR = new UserRights();
                    objUR.UserRightss = links;
                    objUR.UserDesc = txtUserDesc.Text.Trim();

                    CustomStatus cs = (CustomStatus)objURC.Add(objUR);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                        } 
                        //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    else
                        lblStatus.Text = "Error..";
                }
                else
                {
                    if (Request.QueryString["usertypeid"] != null)
                    {
                        int usertypeid = int.Parse(Request.QueryString["usertypeid"].ToString());

                        //get the selected links from the treeview
                        string links = objCommon.GetLinks(trvLinks);

                        //Set all properties
                        UserRightsController objURC = new UserRightsController();

                        UserRights objUR = new UserRights();
                        objUR.UserTypeID = usertypeid;
                        objUR.UserRightss = links;
                        objUR.UserDesc = txtUserDesc.Text.Trim();

                        CustomStatus cs = (CustomStatus)objURC.Update(objUR);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                            } 
                            //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                        else
                            lblStatus.Text = "Error..";                        
                    }
                }
            }
        }        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "user_rights.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString() + "&action=add");
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int usertypeid = int.Parse(btnEdit.CommandArgument);

            string pageurl = Request.Url.ToString() + "&action=edit&usertypeid=" + usertypeid.ToString();            
            Response.Redirect(pageurl);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "user_rights.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int usertypeid)
    {
        UserRightsController objURC = new UserRightsController();
        SqlDataReader dr = objURC.GetSingleRecord(usertypeid);

        if (dr != null)
        {
            if (dr.Read())
            {
                txtUserDesc.Text = dr["USERDESC"] == null ? "" : dr["USERDESC"].ToString();
                //ddlUserType.Text = dr["UserTypeID"] == null ? "" : dr["UserTypeID"].ToString();
                //Bind the TreeView
                string lnks = dr["USERRIGHTS"] == null ? "" : dr["USERRIGHTS"].ToString();
                Fill_TreeLinks(trvLinks, lnks);               
            }
        }

        if (dr != null) dr.Close();
    }
    
    /// <summary>
    /// This method Binds the Treeview with Links
    /// </summary>
    /// <param name="tvLinks"></param>
    /// <param name="links"></param>
    public void Fill_TreeLinks(TreeView tvLinks,string links)
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
            SqlParameter[] objParams = new SqlParameter[0];

            //Get all user links
            drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS_USER_RIGHTS", objParams);

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
                                if (ViewState["action"] == "edit")
                                    zz.Checked = true;
                                else
                                    zz.Checked = false;
                            else
                                if (Session["usertype"].ToString() != "1" && !SearchLink(Session["userlinks"].ToString(), drLinks["al_no"].ToString()))
                                    zz.ShowCheckBox = false;

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
                            {
                                if (ViewState["action"] == "edit")
                                    yy.Checked = true;
                                else
                                    yy.Checked = false;
                            }
                            else
                                if (Session["usertype"].ToString() != "1" && !SearchLink(Session["userlinks"].ToString(), drLinks["al_no"].ToString()))
                                    yy.ShowCheckBox = false;

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
                            zz = new TreeNode();   // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.ShowCheckBox = true;
                            zz.Value = drLinks["al_no"].ToString();
                            zz.NavigateUrl = "";
                            zz.SelectAction = TreeNodeSelectAction.Expand;

                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                if (ViewState["action"] == "edit")
                                    zz.Checked = true;
                                else
                                    zz.Checked = false;
                            else
                                if (Session["usertype"].ToString() != "1" && !SearchLink(Session["userlinks"].ToString(), drLinks["al_no"].ToString()))
                                    zz.ShowCheckBox = false;

                            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != "")
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
                            //if (links.Contains(drLinks["al_no"].ToString())) 
                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                if (ViewState["action"] == "edit")
                                    yy.Checked = true;
                                else
                                    yy.Checked = false;
                            else
                                if (Session["usertype"].ToString() != "1" && !SearchLink(Session["userlinks"].ToString(), drLinks["al_no"].ToString()))
                                    yy.ShowCheckBox = false;

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
                }

                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != "")
                    linkno = int.Parse(drLinks["al_asno"].ToString());
                if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != "")
                    mastno = int.Parse(drLinks["mastno"].ToString());
            }

            //Change Password for All
            //xx = new TreeNode();
            ////xx.ID = "ChPw";
            //xx.Text = "Change Password";
            //xx.Target = "main";
            //xx.NavigateUrl = "changepassword.aspx?pageno=500";
            //tvLinks.Nodes.Add(xx);

            //Log Out for All
            xx = new TreeNode();
            //xx.ID = "lout"
            xx.Text = "Logout";
            xx.Target = "_parent";
            xx.NavigateUrl = "Default.aspx";
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

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindListView();
    //}
   
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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=user_rights.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=user_rights.aspx");
        }
    }

}
