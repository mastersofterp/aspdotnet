//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : USER ROLE                                                 
// CREATION DATE : 02-July-2021
// CREATED BY    : SNEHA G DOBLE                           
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
using System.Xml.Linq;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class UserRolemaster_LinkAssign : System.Web.UI.Page
{
    public int domainCount = 0;
    Activity activity = new Activity();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    User_AccController objUAC = new User_AccController();
    //ConnectionStrings
    string nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events

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
            //objCommon.GetCommonReportData("PKG_ACD_INS_DEFAULT_USER_ROLE");
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                PopulateDropDownList();
                Fill_TreeLinks(tvLinks, string.Empty);
            }
            ViewState["IPADDRESS"] = Request.ServerVariables["REMOTE_HOST"];
            ViewState["action"] = null;
            ViewState["RoleNo"] = 0;
            GetRolelist();
            BindListView();
            Fill_TreeLinks(tvLinks, string.Empty);
            BindCheckList_ForUserRole();
            lblBtotal.Text = "";

            tvLinks.Attributes.Add("onclick", "OnTreeClick(event)");   //Added by sachin 08-12-2022

            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>DisableCheckBoxes();</script>", false);
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() == "8")
                objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS R INNER JOIN USER_ACC U ON (U.UA_TYPE = R.USERTYPEID)", "DISTINCT USERTYPEID", "USERDESC", "USERTYPEID<>0  AND U.UA_DEPTNO=" + Convert.ToInt32(Session["userdeptno"]) + " AND U.UA_TYPE=3", "USERTYPEID");
            else
                objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID>0", "USERTYPEID");


            if (Session["usertype"].ToString() == "8")
                objCommon.FillDropDownList(ddlMusertype, "USER_RIGHTS R INNER JOIN USER_ACC U ON (U.UA_TYPE = R.USERTYPEID)", "DISTINCT USERTYPEID", "USERDESC", "USERTYPEID<>0  AND U.UA_DEPTNO=" + Convert.ToInt32(Session["userdeptno"]) + " AND U.UA_TYPE=3", "USERTYPEID");
            else
                objCommon.FillDropDownList(ddlMusertype, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID>0", "USERTYPEID");

            //Bulk UserLink
            if (Session["usertype"].ToString() == "8")
                objCommon.FillDropDownList(ddlBusertype, "USER_RIGHTS R INNER JOIN USER_ACC U ON (U.UA_TYPE = R.USERTYPEID)", "DISTINCT USERTYPEID", "USERDESC", "USERTYPEID<>0  AND U.UA_DEPTNO=" + Convert.ToInt32(Session["userdeptno"]) + " AND U.UA_TYPE=3", "USERTYPEID");
            else
                objCommon.FillDropDownList(ddlBusertype, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID>0", "USERTYPEID");
        }
        catch (Exception ex)
        {

        }

    }

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
            SQLHelper objSH = new SQLHelper(nitprm_constr);
            //SqlParameter[] objParams = new SqlParameter[0];

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

    #endregion

    #region UserRole

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objUAC.GetUserRoleLinkDetails(Convert.ToInt32(ddlUserType.SelectedValue), 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvrolelist.DataSource = ds;
            lvrolelist.DataBind();
        }
        else
        {
            lvrolelist.DataSource = null;
            lvrolelist.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Prevent to Save|Update Role start with 'Default_'
        if (txtUserRole.Text.Trim().ToString().ToLower().StartsWith("default"))
        {
            objCommon.DisplayMessage(this.Page, "You are not allow to generate or modify default roles.", this.Page);
            return;
        }

        int flock = 0; //Modified and Added By Rishabh on 29/11/2021
        if (hfdStat.Value == "true")
        {
            flock = 1;
        }
        else
        {
            flock = 0;
        }
        int RoleNo = 0;
        int ret = 0;

        if (ViewState["action"] != null)
        {
            if (ViewState["action"].Equals("edit"))
            {
                RoleNo = Convert.ToInt32(ViewState["RoleNo"].ToString());
                ret = Convert.ToInt32(objUAC.InsertUserRole(RoleNo, Convert.ToInt32(ddlUserType.SelectedValue), txtUserRole.Text.Trim(), txtroledescript.Text.Trim(), flock, Convert.ToInt32(Session["userno"].ToString()), ViewState["IPADDRESS"].ToString()));
                if (ret > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                    Clear();
                    GetRolelist();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Transaction Fail.", this.Page);
                    return;
                }
            }
        }
        else
        {
            ret = Convert.ToInt32(objUAC.InsertUserRole(RoleNo, Convert.ToInt32(ddlUserType.SelectedValue), txtUserRole.Text.Trim(), txtroledescript.Text.Trim(), flock, Convert.ToInt32(Session["userno"].ToString()), ViewState["IPADDRESS"].ToString()));
            if (ret > 0)
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                GetRolelist();
                //if (flock == 0)
                //{
                //    DeactivateAssigLinks();
                //}
                Clear();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Transaction Fail.", this.Page);
                return;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            string[] commandArgs = btnEdit.CommandArgument.ToString().Split(new char[] { ',' });
            string roleNo = commandArgs[0];
            string roleName = commandArgs[1];

            // Prevent to Save|Update Role start with 'Default_'
            if (roleName.Trim().ToString().ToLower().StartsWith("default"))
            {
                objCommon.DisplayMessage(this.Page, "You are not allow to generate or modify default roles.", this.Page);
                roleNo = string.Empty; roleName = string.Empty;
                return;
            }



            int Roleno = int.Parse(roleNo);
            //int Roleno = int.Parse(btnEdit.CommandArgument);            
            ViewState["RoleNo"] = Roleno;
            DataSet ds = objUAC.GetUserRoleDetails(Roleno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUserType.SelectedValue = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                txtUserRole.Text = ds.Tables[0].Rows[0]["ROLE_NAME"].ToString();
                txtroledescript.Text = ds.Tables[0].Rows[0]["ROLE_DES"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlUserType.SelectedIndex = 0;
        txtUserRole.Text = string.Empty;
        txtroledescript.Text = string.Empty;
        //chkFlock.Checked = false;
        ViewState["action"] = null;
        ViewState["RoleNo"] = 0;
        ddlMusertype.SelectedIndex = 0;
        ddlMuserrole.SelectedIndex = 0;
        BindListView();
        Fill_TreeLinks(tvLinks, string.Empty);

        Fill_OriginalTreeLinks();
    }

    private void DeactivateAssigLinks()
    {
        try
        {
            string RoleLink = string.Empty;
            RoleLink = objCommon.LookUp("ACD_USER_ROLE_MASTER", "ROLE_UA_ACC", "ROLE_NO=" + Convert.ToInt32(ViewState["RoleNo"].ToString()));
        }
        catch
        {
        }
    }

    #endregion

    #region UserRoleMenue

    private void GetRolelist()
    {
        DataSet ds = objUAC.GetUserRoleDetails(Convert.ToInt32(ViewState["RoleNo"].ToString()));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvrolelist.DataSource = ds;
            lvrolelist.DataBind();
        }
        else
        {
            lvrolelist.DataSource = null;
            lvrolelist.DataBind();
        }
    }

    protected void btnMSubmit_Click(object sender, EventArgs e)
    {
        User_AccController objUAC = new User_AccController();
        string links = string.Empty;
        if (ViewState["action"] == null)
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_USER_ROLE_MASTER", "COUNT(*)", "UA_TYPE=" + Convert.ToInt32(ddlMusertype.SelectedValue) + "AND ROLE_NO=" + Convert.ToInt32(ddlMuserrole.SelectedValue) + " AND ROLE_UA_ACC IS NOT NULL"));
            if (count > 0)
            {
                objCommon.DisplayMessage(this, "Record Already Available.", this.Page);
                ddlMusertype.SelectedIndex = 0;
                ddlMuserrole.SelectedIndex = 0;
                return;
            }
        }

        links = objCommon.GetLinks(tvLinks);
        if (links == "0,500")
        {
            objCommon.DisplayMessage(this.updrolemenu, "Please select at least one link.", this.Page);
            return;
        }
        links = links.Replace("0,500,", "");

        if (ViewState["action"] != null)
        {
            if (ViewState["action"].Equals("edit"))
            {
                int ret = objUAC.UpdateRoleLinkUaTypewise(Convert.ToInt32(ddlMusertype.SelectedValue), Convert.ToInt32(ddlMuserrole.SelectedValue), links);

                if (ret != -99)
                    objCommon.DisplayMessage(this, "Record Updated Successfully.", this.Page);
                else
                    objCommon.DisplayMessage(this, "Transaction Failed.", this.Page);
            }
        }
        else
        {
            int ret = objUAC.UpdateRoleLinkUaTypewise(Convert.ToInt32(ddlMusertype.SelectedValue), Convert.ToInt32(ddlMuserrole.SelectedValue), links);
            if (ret != -99)
                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
            else
                objCommon.DisplayMessage(this, "Transaction Failed.", this.Page);
        }
        Clear();
        clearMuserRole();

        BindListView();
        Fill_TreeLinks(tvLinks, string.Empty);
    }

    protected void btnMcancel_Click(object sender, EventArgs e)
    {
        Clear();
        clearMuserRole();
    }

    protected void ddlMusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_OriginalTreeLinks();

        if (ddlMusertype.SelectedIndex > 0)
        {
            ////objCommon.FillDropDownList(ddlMuserrole, "ACD_USER_ROLE_MASTER", "ROLE_NO", "ROLE_NAME", "UA_TYPE=" + Convert.ToInt32(ddlMusertype.SelectedValue) + "AND ISNULL(STATUS,0)=1", "ROLE_NO");
            objCommon.FillDropDownList(ddlMuserrole, "ACD_USER_ROLE_MASTER", "ROLE_NO", "ROLE_NAME", "UA_TYPE=" + Convert.ToInt32(ddlMusertype.SelectedValue) + "AND ISNULL(STATUS,0)=1 AND ROLE_UA_ACC IS NULL", "ROLE_NO");

            ddlMuserrole.Focus();
            BindListView();
        }
        else
        {
            ddlMusertype.SelectedIndex = 0;
            BindListView();
            clearMuserRole();
        }
    }


    private void clearMuserRole()
    {
        ddlMuserrole.Items.Clear();
        ddlMuserrole.Items.Add(new ListItem("Please Select", "0"));

    }

    protected void btnRmenuEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Fill_OriginalTreeLinks();


            ImageButton btnEdit = sender as ImageButton;
            int Roleno = int.Parse(btnEdit.CommandArgument);
            int ua_type = int.Parse(btnEdit.CommandName);
            ViewState["RoleNo"] = Roleno;
            DataSet ds = objUAC.GetUserRoleDetails(Roleno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMusertype.SelectedValue = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                objCommon.FillDropDownList(ddlMuserrole, "ACD_USER_ROLE_MASTER", "ROLE_NO", "ROLE_NAME", "UA_TYPE=" + Convert.ToInt32(ddlMusertype.SelectedValue) + "AND ISNULL(STATUS,0)=1", "ROLE_NO");
                ddlMuserrole.SelectedValue = ds.Tables[0].Rows[0]["ROLE_NO"].ToString();
            }
            ViewState["action"] = "edit";
            DataSet dsAccDomain = objUAC.GetUserRoleLinkDomain(Roleno, ua_type);


            //ClearFileds2();
            tvLinks.Visible = false;
            tvLinks.Nodes.Clear();

            if (dsAccDomain.Tables[0].Rows.Count > 0 && dsAccDomain != null && dsAccDomain.Tables.Count > 0)
            {
                Fill_TreeLinks(tvLinks, string.Empty);
                domainCount = 1;
                for (int i = 0; i < dsAccDomain.Tables[0].Rows.Count; i++)
                {
                    string val = dsAccDomain.Tables[0].Rows[i]["AL_ASNO"].ToString();

                    if (activity.Page_links.Length > 0)
                        activity.Page_links += ",";
                    activity.Page_links += val;
                }
            }

            ShowDetails(Convert.ToInt32(ddlMuserrole.SelectedValue));

            tvLinks.Visible = true;


            if (domainCount > 0)
            {
                //fill link tree view
                // Fill_TreeLinks(tvLinks, ds.Tables[0].Rows[0]["ROLE_UA_ACC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ROLE_UA_ACC"].ToString());
            }
            //lnkID_Click(ua_no.ToString());

            btnSubmit.Enabled = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        DataSet ds = objUAC.GetUserRoleLinkDetails(Convert.ToInt32(ddlMusertype.SelectedValue), 2);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvrolemenulist.DataSource = ds;
            lvrolemenulist.DataBind();
        }
        else
        {
            lvrolemenulist.DataSource = null;
            lvrolemenulist.DataBind();
        }
    }

    private void ShowDetails(int Roleno)
    {
        DataSet ds = objUAC.GetUserRoleDetails(Roleno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            //Bind the TreeView By default according to the rights of selected user
            string lnks = ds.Tables[0].Rows[0]["ROLE_UA_ACC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ROLE_UA_ACC"].ToString();
            Fill_Default_TreeLinks(tvLinks, lnks);

            Fill_Default_OriginalTreeLinks(tvOriginalLinks, lnks);
        }

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

    public void Fill_Default_TreeLinks(TreeView tvLinks, string links)
    {
        SqlDataReader drLinks = null;

        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        TreeNode xx = null;
        TreeNode yy = null;
        TreeNode zz = null;

        tvLinks.Nodes.Clear();

        try
        {
            SQLHelper objSH = new SQLHelper(nitprm_constr);
            //SqlParameter[] objParams = new SqlParameter[2];

            //Added by Manish Chawade on 23/04/2016 parameter to show only assigned links
            //objParams[0] = new SqlParameter("@P_UA_NO", Session["userno"].ToString());
            //objParams[1] = new SqlParameter("@P_AL_ASNO", activity.Page_links);//added by satish -30102017
            //Get all user links
            //drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS_NEW", objParams);
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

                        // adding node to root
                        tvLinks.Nodes.Add(xx);

                        if (drLinks["al_url"].ToString().Trim() == "")
                        {
                            zz = new TreeNode();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.Value = drLinks["al_no"].ToString();
                            zz.ShowCheckBox = true;
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
                            zz = new TreeNode();   // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.ShowCheckBox = true;
                            zz.Value = drLinks["al_no"].ToString();

                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                zz.Checked = true;

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


    public void Fill_Default_OriginalTreeLinks(TreeView tvOriginalLinks, string links)
    {
        div_tvOriginalLinks.Visible = true;

        SqlDataReader drLinks = null;

        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        TreeNode xx = null;
        TreeNode yy = null;
        TreeNode zz = null;

        tvOriginalLinks.Nodes.Clear();

        try
        {
            SQLHelper objSH = new SQLHelper(nitprm_constr);
            //SqlParameter[] objParams = new SqlParameter[2];

            //Added by Manish Chawade on 23/04/2016 parameter to show only assigned links
            //objParams[0] = new SqlParameter("@P_UA_NO", Session["userno"].ToString());
            //objParams[1] = new SqlParameter("@P_AL_ASNO", activity.Page_links);//added by satish -30102017
            //Get all user links
            //drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS_NEW", objParams);
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

                        // adding node to root
                        tvOriginalLinks.Nodes.Add(xx);

                        if (drLinks["al_url"].ToString().Trim() == "")
                        {
                            zz = new TreeNode();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.Value = drLinks["al_no"].ToString();
                            zz.ShowCheckBox = true;
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
                            zz = new TreeNode();   // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.ShowCheckBox = true;
                            zz.Value = drLinks["al_no"].ToString();

                            if (!links.Equals(string.Empty) && SearchLink(links, drLinks["al_no"].ToString()))
                                zz.Checked = true;

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
            tvOriginalLinks.Nodes.Add(xx);

            //Log Out for All
            xx = new TreeNode();
            //xx.ID = "lout"
            xx.Text = "Logout";
            xx.Target = "_parent";
            xx.NavigateUrl = "logout.aspx";
            tvOriginalLinks.Nodes.Add(xx);

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

        tvOriginalLinks.ExpandAll();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>DisableCheckBoxes();</script>", false);
    }
    public void Fill_OriginalTreeLinks()
    {
        div_tvOriginalLinks.Visible = false;
        tvOriginalLinks.Nodes.Clear();
    }

    #endregion

    #region BulklinkAssign

    protected void btnBShow_Click(object sender, EventArgs e)
    {
        BindUserListView();
        int pagesize = Convert.ToInt32(DataPager1.PageSize);
        NumberDropDown.SelectedValue = pagesize.ToString();
        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ResetCheckBoxValue();</script>", false);
    }

    protected void btnBSubmit_Click(object sender, EventArgs e)
    {
        string ua_nos = string.Empty;
        string links = string.Empty;
        string Roleno = string.Empty;
        int count = 0;
        int Usercount = 0;
        foreach (ListViewDataItem item in lvBulkDetail.Items)
        {
            CheckBox chkuano = item.FindControl("chkuano") as CheckBox;
            if (chkuano.Checked == true)
            {
                Usercount++;
                ua_nos += chkuano.ToolTip + ",";
            }
        }
        foreach (ListItem item in chkListRole.Items)
        {
            if (item.Selected)
            {
                count = count + 1;
                if (count == 1)
                {
                    Roleno = item.Value;
                }
                else
                {
                    Roleno = Roleno + "," + item.Value;
                }
            }
        }
        if (Usercount == 0)
        {
            objCommon.DisplayMessage(this, "Please Select Atleast One User from list to assign.", this.Page);
            return;
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(this, "Please Select Atleast One Role from list to assign.", this.Page);
            return;
        }
        DataSet ds = objUAC.GetRolewiseAssignlinks(Roleno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            links = ds.Tables[0].Rows[0]["UA_ACC"].ToString();
        }
        else
        {
        }
        if (links == "0,500")
        {
            objCommon.DisplayMessage(this, "Please select at least one link.", this.Page);
            return;
        }
        links = links.Replace("0,500,", "");
        int ret = objUAC.UpdateLinkRolewise(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), ua_nos, links, Roleno);

        if (ret != -99)
            objCommon.DisplayMessage(this, "User Link Assign Successfully.", this.Page);

        else
            objCommon.DisplayMessage(this, "Transaction Failed.", this.Page);

        //chkListRole.Items.Clear();
        chkListRole.SelectedIndex = -1;
        ddlBusertype.SelectedIndex = 0;
        ddlBDept.SelectedIndex = 0;
        ddlBdegree.SelectedIndex = 0;
        ddlBbranch.SelectedIndex = 0;
        ddlBSemester.SelectedIndex = 0;
        trBDept.Visible = true;
        pnlBStudent.Visible = false;

        ////BindUserListView(); //commented by arjun 12012023
        clearListView();// added by arjun 12012023

        //Fill_TreeLinks(tvLinks, string.Empty);
    }

    protected void btnBcancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        ddlBusertype.SelectedIndex = 0;
        ddlBDept.SelectedIndex = 0;
        ddlBdegree.SelectedIndex = 0;
        ddlBbranch.SelectedIndex = 0;
        ddlBSemester.SelectedIndex = 0;
        lvBulkDetail.DataSource = null;
        lvBulkDetail.DataBind();
        pnlBulkDetail.Visible = false;
        //btnBSubmit.Enabled = true;
        lblBtotal.Visible = false;
        chkListRole.SelectedIndex = -1;
        btnBSubmit.Enabled = false;
        DataPager1.Visible = false;


        clearListView();//new by arjun 13-01-2023
    }

    private void BindCheckList_ForUserRole()
    {
        try
        {
            DataSet dsrole = null;
            //dsrole = objCommon.FillDropDown("ACD_USER_ROLE_MASTER", "ROLE_NO", "ROLE_NAME,ROLE_UA_ACC", "UA_TYPE=" + Convert.ToInt32(ddlBusertype.SelectedValue) + "AND (ROLE_UA_ACC IS NOT NULL OR ROLE_UA_ACC='') AND ISNULL(STATUS,0)=1", "ROLE_NO");
            dsrole = objCommon.FillDropDown("ACD_USER_ROLE_MASTER", "ROLE_NO", "ROLE_NAME,ROLE_UA_ACC", "ISNULL(STATUS,0)=1 AND (ROLE_UA_ACC IS NOT NULL OR ROLE_UA_ACC='')", "ROLE_NO");

            if (dsrole.Tables[0].Rows.Count > 0)
            {
                chkListRole.Items.Clear();
                chkListRole.DataTextField = "ROLE_NAME";
                chkListRole.DataValueField = "ROLE_NO";
                chkListRole.DataSource = dsrole.Tables[0];
                chkListRole.DataBind();
                chkListRole.Visible = true;
            }
            else
            {
                chkListRole.DataSource = null;
                chkListRole.DataBind();
                chkListRole.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DuplicateDataCorrectionApplication.BindCheckList_ForSemester()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkListRole_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlBusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBDept.Items.Clear();
        ddlBDept.Items.Add(new ListItem("Please Select", "0"));
        lvBulkDetail.DataSource = null;
        lvBulkDetail.DataBind();
        pnlBulkDetail.Visible = false;
        DataSet ds = null;
        if (Session["usertype"].ToString() == "8")
            ds = objCommon.FillDropDownDepartmentUserWise(Convert.ToInt32(ddlBusertype.SelectedValue), Convert.ToString(Session["userdeptno"]));
        //objCommon.FillDropDownList(ddlBDept, "ACD_DEPARTMENT D INNER JOIN USER_ACC U ON (D.DEPTNO=U.UA_DEPTNO)", "DISTINCT DEPTNO", "DEPTNAME", "UA_TYPE=" + Convert.ToInt32(ddlBusertype.SelectedValue) + " AND UA_DEPTNO=" + Convert.ToInt32(Session["userdeptno"]) + " AND DEPTNO>0", "DEPTNO");  //BY SNEHA(STATUS=ACTIVE)
        else
            ds = objCommon.FillDropDownDepartmentUserWise(Convert.ToInt32(ddlBusertype.SelectedValue), "0");
        //objCommon.FillDropDownList(ddlBDept, "ACD_DEPARTMENT D INNER JOIN USER_ACC U ON (D.DEPTNO=U.UA_DEPTNO)", "DISTINCT DEPTNO", "DEPTNAME", "UA_TYPE=" + Convert.ToInt32(ddlBusertype.SelectedValue) + " AND DEPTNO>0", "DEPTNO");   //BY SNEHA(STATUS=ACTIVE)     
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBDept.DataSource = ds;
            ddlBDept.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlBDept.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlBDept.DataBind();
            ddlBDept.SelectedIndex = 0;
        }
        else
        {
            ddlBDept.Items.Clear();
            ddlBDept.Items.Add(new ListItem("Please Select", "0"));
        }

        if (ddlBusertype.SelectedValue == "2")
        {
            pnlBStudent.Visible = true;
            trBDept.Visible = false;

            objCommon.FillDropDownList(ddlBdegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            ddlBbranch.Items.Clear(); ddlBbranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBSemester.Items.Clear(); ddlBSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            pnlBStudent.Visible = false;
            trBDept.Visible = true;
        }
        BindCheckList_ForUserRole();
    }

    protected void ddlBdegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlBdegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBbranch, " ACD_COLLEGE_DEGREE_BRANCH A  inner join  ACD_BRANCH B on (a.BRANCHNO=b.BRANCHNO)", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO=" + Convert.ToInt32(ddlBdegree.SelectedValue), "BRANCHNO");
            objCommon.FillDropDownList(ddlBSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            ddlBbranch.Focus();
            clearListView();
        }

        else
        {
            ddlBSemester.Items.Clear(); ddlBSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlBbranch.Items.Clear(); ddlBbranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBdegree.SelectedIndex = 0;
            clearListView();

        }
    }

    private void BindUserListView()
    {
        DataSet ds = null;
        if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8")
        {
            if (ddlBusertype.SelectedValue != "2")
            {
                //if (ddlBDept.SelectedIndex != 0) //Added by Nikhil S For Department Wise Faculty Display dt-07/09/2022 as per Yograj Sir
                //{
                ds = objUAC.GetDepartmentWiseFaculty(Convert.ToInt32(ddlBusertype.SelectedValue), Convert.ToInt32(ddlBDept.SelectedValue));
                //}
                //else
                //{
                //    ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_NAME,UA_FULLNAME", "ISNULL(UA_STATUS,0)=0 AND UA_TYPE=" + Convert.ToInt32(ddlBusertype.SelectedValue) + " AND (UA_DEPTNO ='" + Convert.ToString(ddlBDept.SelectedValue) + "' OR '" + Convert.ToInt32(ddlBDept.SelectedValue) + "'='0')", "UA_FULLNAME");
                //}
                //ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_NAME,UA_FULLNAME", "ISNULL(UA_STATUS,0)=0 AND UA_TYPE=" +Convert.ToInt32(ddlBusertype.SelectedValue) + " AND (UA_DEPTNO ='" + Convert.ToString(ddlBDept.SelectedValue) +  "' OR '" + Convert.ToInt32(ddlBDept.SelectedValue) + "'='0')", "UA_FULLNAME");

            }
            else
            {
                // ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT  S ON (U.UA_IDNO=S.IDNO)", "UA_NO", "UA_NAME,UA_FULLNAME", "ISNULL(UA_STATUS,0)=0  AND UA_TYPE=" + Convert.ToInt32(ddlBusertype.SelectedValue) + " AND S.ENROLLNO IS NOT NULL AND ISNULL(ADMCAN,0)=0 AND (DEGREENO=" + Convert.ToInt32(ddlBdegree.SelectedValue) + " OR " + Convert.ToInt32(ddlBdegree.SelectedValue) + "=0) AND (BRANCHNO=" + Convert.ToInt32(ddlBbranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBbranch.SelectedValue) + "=0) AND (SEMESTERNO=" + Convert.ToInt32(ddlBSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlBSemester.SelectedValue) + "=0)", "UA_FULLNAME");

                ds = objUAC.GetUserRole(Convert.ToInt32(ddlBusertype.SelectedValue), Convert.ToInt32(ddlBdegree.SelectedValue), Convert.ToInt32(ddlBbranch.SelectedValue), Convert.ToInt32(ddlBSemester.SelectedValue));//added by kajal jaiswal on 22-12-2022

                // ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT  S ON (U.UA_IDNO=S.IDNO)", "UA_NO", "UA_NAME,UA_FULLNAME", "ISNULL(UA_STATUS,0)=0  AND UA_TYPE=" + Convert.ToInt32(ddlBusertype.SelectedValue)+ "  AND (DEGREENO=" + Convert.ToInt32(ddlBdegree.SelectedValue) + " OR " + Convert.ToInt32(ddlBdegree.SelectedValue) + "=0) AND (BRANCHNO=" + Convert.ToInt32(ddlBbranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBbranch.SelectedValue) + "=0) AND (SEMESTERNO=" + Convert.ToInt32(ddlBSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlBSemester.SelectedValue) + "=0)", "UA_FULLNAME");                 //Changes by Ruchika Dhakate on 14.09.2022

                // ISNULL(USER_TYPE,0)=0 AND commented by rishabh as per umesh sir
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBulkDetail.DataSource = ds;
                lvBulkDetail.DataBind();
                pnlBulkDetail.Visible = true;
                btnBSubmit.Enabled = true;
                lblBtotal.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvBulkDetail.DataSource = null;
                lvBulkDetail.DataBind();
                pnlBulkDetail.Visible = false;
                btnBSubmit.Enabled = false;
                lblBtotal.Text = "Total Records : 0";
                objCommon.DisplayMessage(this, "Record Not Found!!", this.Page);
            }
        }
    }
    #endregion


    #region New Code by arjun on 11-01-2023 for Single User Selection in Listview
    protected void chkuano_CheckedChanged(object sender, EventArgs e)
    {

        //checking singele user selection selected or not
        if (!cbAllowSingleSelection.Checked)
            return;

        ////chkListRole.ClearSelection();

        CheckBox chkbox = sender as CheckBox;

        if (chkbox.Checked == true)
        {
            foreach (ListItem item in chkListRole.Items)
            {
                item.Selected = false;
            }

            string userNo = chkbox.ToolTip.ToString();


            DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_ROLENO", "UA_NO =" + userNo + " AND UA_ROLENO is not null", "UA_NO");

            if (ds != null && ds.Tables[0].Rows.Count > 0 && chkListRole.Items.Count > 0)
            {
                string roles = ds.Tables[0].Rows[0]["UA_ROLENO"].ToString();
                string[] arrRoles = roles.Split(new char[] { ',' });

                foreach (ListItem item in chkListRole.Items)
                {
                    for (int i = 0; i < arrRoles.Length; i++)
                    {
                        if (item.Value.ToString().Equals(arrRoles[i]))
                        {
                            item.Selected = true;
                        }
                    }

                    /*if (item.Selected)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            Roleno = item.Value;
                        }
                        else
                        {
                            Roleno = Roleno + "," + item.Value;
                        }
                    }*/

                }

            }

        }
        ////ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> setCheckSingleCB(cbAllowSingleSelection);restrictListViewCheckboxSelection('chkuano');</script>", false);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setListViewCheckBoxHeader();</script>", false);
    }

    private void clearListView()
    {
        cbAllowSingleSelection.Checked = false;

        lvBulkDetail.DataSource = null;
        lvBulkDetail.DataBind();
        pnlBulkDetail.Visible = false;
        btnBSubmit.Enabled = false;
        lblBtotal.Text = "Total Records : 0";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ResetCheckBoxValue();</script>", false);
    }

    protected void btnAssignLinks_Click(object sender, EventArgs e)
    {
        //do whatever you want
    }

    [WebMethod]
    public static string SetUserAccessLinks(string UserNo)
    {
        ////setCheckedBox(UserNo);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;

        Common objCommons = new Common();
        ds = objCommons.FillDropDown("USER_ACC", "UA_NO", "UA_ROLENO", "UA_NO =" + UserNo + " AND UA_ROLENO is not null", "UA_NO");

        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }
        string json = serializer.Serialize(root);
        json = json.Replace("[[", "").Replace("]]", "");
        return json;
    }


    private void setCheckedBox(string userNo)
    {
        //if (chkbox.Checked == true)
        //{
        foreach (ListItem item in chkListRole.Items)
        {
            item.Selected = false;
        }

        //string userNo = chkbox.ToolTip.ToString();


        DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_ROLENO", "UA_NO =" + userNo + " AND UA_ROLENO is not null", "UA_NO");

        if (ds != null && ds.Tables[0].Rows.Count > 0 && chkListRole.Items.Count > 0)
        {
            string roles = ds.Tables[0].Rows[0]["UA_ROLENO"].ToString();
            string[] arrRoles = roles.Split(new char[] { ',' });

            foreach (ListItem item in chkListRole.Items)
            {
                for (int i = 0; i < arrRoles.Length; i++)
                {
                    if (item.Value.ToString().Equals(arrRoles[i]))
                    {
                        item.Selected = true;
                    }
                }

                /*if (item.Selected)
                {
                    count = count + 1;
                    if (count == 1)
                    {
                        Roleno = item.Value;
                    }
                    else
                    {
                        Roleno = Roleno + "," + item.Value;
                    }
                }*/

            }

        }

        //}
    }
    #endregion

    protected void lvBulkDetail_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindUserListView();
    }

    protected void NumberDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
        DataPager1.PageSize = Convert.ToInt32(NumberDropDown.SelectedValue);
    }
}