//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   :                                                                 
// PAGE NAME     : TO CREATE/MODIFY EXISTING USER                                  
// CREATION DATE : 01-Nov-2017                                                   
// CREATED BY    :                                                 
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
using System.Net.Mail;

public partial class Link_user : System.Web.UI.Page
{
    public int domainCount = 0;
    User_AccController objUC = new User_AccController();
    UserAcc objUA = new UserAcc();

    Activity activity = new Activity();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        divMsg.InnerHtml = string.Empty;

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();

                //Populate the user dropdownlist with username and userid
                PopulateDropDownList();
                PopulateAccessLink();

                //ShowPanel();
                ViewState["action"] = null;
                ShowUserDetails(Convert.ToInt32(Session["userno"].ToString()));
                //if (Session["usertype"].ToString() == "1")
                //{
                //    ddlSupervisorstatus.Enabled = true;

                //}
                if (Session["usertype"].ToString() == "4")
                {
                    chkDEC.Checked = true;
                    chkDEC.Enabled = false;
                }
                else
                    if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")
                    {
                        chkDEC.Checked = false;
                        chkDEC.Enabled = false;
                    }

                tvLinks.Attributes.Add("onclick", "OnTreeClick(event)");
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
        }

        //Added code by Arjun on Date :27012023 for Disabled Default Role Links solve page postback issue
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>DisableCheckBoxes();</script>", false);

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    #endregion

    #region Click Events


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ViewState["CheckBtn"] = true;
        string pwd = string.Empty;
        string OldMobileno = string.Empty;
        string OldEmailid = string.Empty;

        try
        {
            objUA.UA_Acc = objCommon.GetLinks(tvLinks);
            objUA.UA_Dec = chkDEC.Checked ? 1 : 0;
            objUA.UA_DeptNo = Convert.ToString((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1" && ddlDept.SelectedIndex > 0 ? ddlDept.SelectedValue.ToString() : "0");
            objUA.UA_Desig = txtDesignation.Text.Replace("'", "`").Trim();
            objUA.UA_Email = txtEmail.Text.Replace("'", "`").Trim();
            objUA.UA_EmpDeptNo = Convert.ToInt32((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "11" || Session["usertype"].ToString() == "1" && ddlSubDept.SelectedIndex > 0 ? ddlSubDept.SelectedValue.ToString() : "0");
            objUA.Parent_UserType = Convert.ToInt16(Session["usertype"].ToString());
            objUA.UA_EmpSt = "0";
            objUA.UA_FullName = txtFName.Text.Replace("'", "`").Trim();

            objUA.UA_No = Convert.ToInt16(txtUsername.ToolTip.ToString() == "" ? "0" : txtUsername.ToolTip.ToString());
            objUA.UA_Name = txtUsername.Text.Replace("'", "`").Trim();

            objUA.UA_Status = chkActive.Checked ? 0 : 1;
            objUA.UA_Type = Convert.ToInt32(ddlUserType.SelectedValue.ToString());

            if (Convert.ToBoolean(ViewState["ExistUser"]) == true)
            {
                CustomStatus cs = (CustomStatus)objUC.UpdateUserAccLink(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), objUA);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(Page, "Link(s) Updated successfully !!", this);

                    ClearTxt();
                }
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "create_user.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id=")));
        else
            Response.Redirect(Request.Url.ToString());
    }

    public void test2()
    {
        try
        {
            //added by satish
            PopulateAccessLink();
            chkListAccLink.Enabled = true;


            ViewState["action"] = "edit";
            int ua_no = Convert.ToInt32(ViewState["ua_no"]);
            int UA_TYPE = Convert.ToInt32(ddlUserType.SelectedValue);

            //check access domain already assign to user 

            // string ua_acc = objCommon.LookUp("USER_ACC", "UA_ACC", "UA_NO=" + ua_no + "AND UA_TYPE=" + ddlUserType.SelectedValue);

            ////get user access domain
            //  DataSet dsAL_ASNO = objCommon.FillDropDown("ACCESS_LINK", "DISTINCT AL_ASNO", string.Empty, "AL_No IN (" + ua_acc + ")", "AL_ASNO");


            //objUA.UA_Acc = ua_acc;
            //objUA.UA_No = ua_no;
            DataSet dsAccDomain = objUC.GetUserLinkDomain(ua_no, UA_TYPE);


            //ClearFileds2();
            tvLinks.Visible = false;
            tvLinks.Nodes.Clear();

            if (dsAccDomain.Tables[0].Rows.Count > 0 && dsAccDomain != null && dsAccDomain.Tables.Count > 0)
            {
                domainCount = 1;

                for (int i = 0; i < dsAccDomain.Tables[0].Rows.Count; i++)
                {
                    string val = dsAccDomain.Tables[0].Rows[i]["AL_ASNO"].ToString();
                    foreach (ListItem item in chkListAccLink.Items)
                    {
                        if (item.Value == val)
                        {
                            item.Selected = true;
                            test1();
                            //chkListAccLink_SelectedIndexChanged(sender, e);
                            item.Enabled = false;
                        }
                    }
                }

            }


            //show checked domain in tree list


            foreach (ListItem item in chkListAccLink.Items)
            {
                if (item.Selected)
                {
                    if (activity.Page_links.Length > 0)
                        activity.Page_links += ",";

                    activity.Page_links += item.Value;
                }
            }


            //lnkID_Click(ua_no.ToString());

            ShowUserDetails(ua_no);

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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //added by satish
            PopulateAccessLink();
            chkListAccLink.Enabled = true;

            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int ua_no = int.Parse(btnEdit.CommandArgument);
            int UA_TYPE = Convert.ToInt32(ddlUserType.SelectedValue);
            ViewState["ua_no"] = ua_no;
            //check access domain already assign to user 

            // string ua_acc = objCommon.LookUp("USER_ACC", "UA_ACC", "UA_NO=" + ua_no + "AND UA_TYPE=" + ddlUserType.SelectedValue);

            ////get user access domain
            //  DataSet dsAL_ASNO = objCommon.FillDropDown("ACCESS_LINK", "DISTINCT AL_ASNO", string.Empty, "AL_No IN (" + ua_acc + ")", "AL_ASNO");


            //objUA.UA_Acc = ua_acc;
            //objUA.UA_No = ua_no;
            DataSet dsAccDomain = objUC.GetUserLinkDomain(ua_no, UA_TYPE);


            //ClearFileds2();
            tvLinks.Visible = false;
            tvLinks.Nodes.Clear();

            if (dsAccDomain.Tables[0].Rows.Count > 0 && dsAccDomain != null && dsAccDomain.Tables.Count > 0)
            {
                domainCount = 1;

                for (int i = 0; i < dsAccDomain.Tables[0].Rows.Count; i++)
                {
                    string val = dsAccDomain.Tables[0].Rows[i]["AL_ASNO"].ToString();
                    foreach (ListItem item in chkListAccLink.Items)
                    {
                        if (item.Value == val)
                        {
                            item.Selected = true;
                            test1();
                            //chkListAccLink_SelectedIndexChanged(sender, e);
                            item.Enabled = false;
                        }
                    }
                }

            }


            //show checked domain in tree list


            foreach (ListItem item in chkListAccLink.Items)
            {
                if (item.Selected)
                {
                    if (activity.Page_links.Length > 0)
                        activity.Page_links += ",";

                    activity.Page_links += item.Value;
                }
            }


            //lnkID_Click(ua_no.ToString());

            ShowUserDetails(ua_no);

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



    #endregion

    #region Other Events
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearFileds();

        ShowPanel();

        if (Convert.ToInt32(ddlUserType.SelectedValue) == 2)
        {
            //ddlSubDept.Enabled = false;
            txtDesignation.Enabled = false;

            //added by satish-31102017
            pnlStudent.Visible = true;
            trSubDept.Visible = false;
            trDept.Visible = false;
            objCommon.FillDropDownList(ddlCollege, "ACD_college_master", "college_id", "college_name", "college_id>0", "college_id");
            ddlBranch.Items.Clear(); ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear(); ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            trDept.Visible = true;
            trSubDept.Visible = true;
            //ddlSubDept.Enabled = true;
            txtDesignation.Enabled = true;
            //added by satish-31102017
            pnlStudent.Visible = false;
        }
        //ClearCheck();
        lblStatus.Text = string.Empty;
        lblSubmitStatus.Text = string.Empty;



    }
    private void ClearFileds2()
    {

        ViewState["ExistUser"] = null;

        lvlinks.DataSource = null;
        lvlinks.DataBind();

        chkListAccLink.DataSource = null;
        chkListAccLink.DataBind();

    }

    private void ClearFileds()
    {
        txtDesignation.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFName.Text = string.Empty;
        //  txtPassword.Text = string.Empty;
        // txtQtrRoomNo.Text = string.Empty;
        txtUserID.Text = string.Empty;
        txtUsername.Text = string.Empty;
        ddlDept.SelectedIndex = 0;
        ddlSubDept.SelectedIndex = 0;

        ViewState["ExistUser"] = null;

        lvlinks.DataSource = null;
        lvlinks.DataBind();
        chkListAccLink.SelectedIndex = -1;
        chkListAccLink.DataSource = null;
        chkListAccLink.DataBind();
        tvLinks.Visible = false;

    }
    /// <summary>
    /// Added By MR. MANISH WALDE on 09-JAN-2015
    /// By default Load tree view links with the selected user type
    /// </summary>
    /// <param name="usertypeid"></param>
    private void ShowDetails(int usertypeid)
    {
        UserRightsController objURC = new UserRightsController();
        SqlDataReader dr = objURC.GetSingleRecord(usertypeid);

        if (dr != null)
        {
            if (dr.Read())
            {
                //Bind the TreeView By default according to the rights of selected user
                string lnks = dr["USERRIGHTS"] == null ? "" : dr["USERRIGHTS"].ToString();
                Fill_Default_TreeLinks(tvLinks, lnks);
            }
        }

        if (dr != null) dr.Close();
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
            SQLHelper objSH = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = new SqlParameter[2];

            //Added by Manish Chawade on 23/04/2016 parameter to show only assigned links
            objParams[0] = new SqlParameter("@P_UA_NO", Session["userno"].ToString());
            objParams[1] = new SqlParameter("@P_AL_ASNO", activity.Page_links);//added by satish -30102017
            //Get all user links
            drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS_NEW", objParams);

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

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        SetStatus();
    }
    #endregion

    #region User Methods

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "11" || Session["userdeptno"] != null || Session["usertype"].ToString() == "1")
            {
                trDept.Visible = true;
                trSubDept.Visible = true;
                ddlDept.Items.Clear();
                if (Session["userdeptno"] != null && Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")
                {
                    ddlDept.Items.Add(new ListItem("Please Select", "0"));
                    DataSet dsAcd = objCommon.FillDropDown("ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO=" + Session["userdeptno"].ToString(), string.Empty);
                    ddlDept.DataSource = dsAcd;
                    ddlDept.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
                    ddlDept.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
                    ddlDept.DataBind();
                }
                else
                    if (Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1")
                    {
                        ddlDept.Items.Add(new ListItem("Please Select", "0"));
                        DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLACDDEPT");
                        ddlDept.DataSource = dsAcd;
                        ddlDept.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
                        ddlDept.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
                        ddlDept.DataBind();

                        //Payroll Department 
                        DataSet dsPayroll = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLPAYROLLDEPT");
                        ddlSubDept.DataSource = dsPayroll;
                        ddlSubDept.DataTextField = dsPayroll.Tables[0].Columns[1].ToString();
                        ddlSubDept.DataValueField = dsPayroll.Tables[0].Columns[0].ToString();
                        ddlSubDept.DataBind();
                    }
                    else
                    {
                        trDept.Visible = false;
                        trSubDept.Visible = false;
                        ddlDept.Items.Clear();
                        ddlSubDept.Items.Clear();
                    }
            }
            else
            {
                trDept.Visible = false;
                trSubDept.Visible = false;
                ddlDept.Items.Clear();
            }
            DataSet dsUser = null;
            if (Session["usertype"].ToString() == "1")
                dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID<>0", "USERTYPEID");
            else
                dsUser = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") ? "USERTYPEID = 3" : "PARENT_USERTYPE = " + Session["usertype"].ToString(), "USERTYPEID");

            ddlUserType.DataSource = dsUser;
            ddlUserType.DataTextField = dsUser.Tables[0].Columns[1].ToString();
            ddlUserType.DataValueField = dsUser.Tables[0].Columns[0].ToString();
            ddlUserType.DataBind();




            btnSubmit.Enabled = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "create_user.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void Fill_TreeLinks(TreeView tvLinks, string links, string rolelinks)
    {
        SqlDataReader drLinks = null;

        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        TreeNode xx = null;
        TreeNode yy = null;
        TreeNode zz = null;

        tvLinks.Nodes.Clear();


        string[] roleArrayLinks = rolelinks.Split(new char[] { ',' });//Added code by Arjun on Date :27012023 for Disabled Default Role Links

        try
        {
            //SQLHelper objSH = new SQLHelper(uaims_constr);
            //SqlParameter[] objParams = new SqlParameter[1];

            //objParams[0] = new SqlParameter("@P_UA_NO",Convert.ToInt32(Session["userno"].ToString()));
            SQLHelper objSH = new SQLHelper(uaims_constr);

            SqlParameter[] objParams = new SqlParameter[2];

            //Added by Manish Chawade on 23/04/2016 parameter to show only assigned links
            objParams[0] = new SqlParameter("@P_UA_NO", Session["userno"].ToString());
            objParams[1] = new SqlParameter("@P_AL_ASNO", activity.Page_links);//added by satish -30102017
            //Get all user links
            drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_ALL_USERLINKS_NEW", objParams);
            string ss = string.Empty;
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
                                {
                                    zz.Checked = true;

                                    //Added code by Arjun on Date :27012023 for Disabled Default Role Links
                                    #region
                                    foreach (string role in roleArrayLinks)
                                    {
                                        if (role.Equals(drLinks["al_no"].ToString()))
                                        {
                                            zz.Text = "<span class=\"disable_default\">" + zz.Text + "</span>";
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    zz.Checked = false;
                                }
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
                                {
                                    yy.Checked = true;

                                    //Added code by Arjun on Date :27012023 for Disabled Default Role Links
                                    #region
                                    foreach (string role in roleArrayLinks)
                                    {
                                        if (role.Equals(drLinks["al_no"].ToString()))
                                        {
                                            yy.Text = "<span class=\"disable_default\">" + yy.Text + "</span>";
                                        }
                                    }
                                    #endregion

                                }
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
                                {
                                    zz.Checked = true;

                                    //Added code by Arjun on Date :27012023 for Disabled Default Role Links
                                    #region
                                    foreach (string role in roleArrayLinks)
                                    {
                                        if (role.Equals(drLinks["al_no"].ToString()))
                                        {
                                            zz.Text = "<span class=\"disable_default\">" + zz.Text + "</span>";
                                        }
                                    }
                                    #endregion

                                }
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
                                {
                                    yy.Checked = true;

                                    //Added code by Arjun on Date :27012023 for Disabled Default Role Links
                                    #region
                                    foreach (string role in roleArrayLinks)
                                    {
                                        if (role.Equals(drLinks["al_no"].ToString()))
                                        {
                                            yy.Text = "<span class=\"disable_default\">" + yy.Text + "</span>";
                                        }
                                    }
                                    #endregion

                                }
                                else
                                    yy.Checked = false;
                            else
                                if (Session["usertype"].ToString() != "1" && !SearchLink(Session["userlinks"].ToString(), drLinks["al_no"].ToString()))
                                    yy.ShowCheckBox = false;

                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != ""))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    //Added code by Arjun on Date :27012023 for Disabled Default Role Links
                                    #region                                    
                                    foreach (string role in roleArrayLinks)
                                    {
                                        if (role.Equals(drLinks["al_no"].ToString()))
                                        {
                                            //        //add css class
                                            ////ss += drLinks["al_no"].ToString();
                                            //tvLinks.LevelStyles.Add(  continue your work
                                            //zz.CssClass = "BoldNode";

                                            //Original
                                            yy.Text = "<span class=\"disable_default\">" + yy.Text + "</span>";
                                        }
                                    }
                                    #endregion

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

            //Added below code by Arjun on Date :27012023 for Disabled Default Role Links
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>DisableCheckBoxes();</script>", false);
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

    private void ClearTxt()
    {
        txtDesignation.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFName.Text = string.Empty;
        // txtPassword.Text = string.Empty;
        //txtQtrRoomNo.Text = string.Empty;
        txtUserID.Text = string.Empty;
        txtUsername.Text = string.Empty;
        ddlDept.SelectedIndex = 0;
        ddlSubDept.SelectedIndex = 0;
        //hdfPassword.Value = string.Empty;
        chkActive.Checked = false;
        chkDEC.Checked = false;
        //lblPassStatus.Text = string.Empty;
        ddlUserType.SelectedIndex = 0;
        // lvUsers.DataSource = null;
        //lvUsers.DataBind();
        ViewState["ExistUser"] = null;
        //rfvPassword.Visible = true;
        //txtMobile.Text = string.Empty;

        tvLinks.Visible = false;
        chkListAccLink.SelectedIndex = -1;
        lvlinks.DataSource = null;
        lvlinks.DataBind();

        //lvUsers.DataSource = null;
        // lvUsers.DataBind();
        btnSubmit.Enabled = false;
        chkListAccLink.Enabled = false;
    }

    private void lnkID_Click(string id)
    {
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Response.Redirect(url + "&id=" + id);
    }

    private void bindlist(string category, string searchtext)
    {
        User_AccController objUAC = new User_AccController();
        DataSet ds = objUAC.FindUser(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            // lvUsers.DataSource = ds;
            // lvUsers.DataBind();
            //  lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        // else
        // lblNoRecords.Text = "Total Records : 0";
    }

    private void ShowUserDetails(int idno)
    {
        try
        {
            User_AccController objACC = new User_AccController();
            DataTableReader dtr;

            dtr = objACC.GetUserByUANo(idno);
            if (ViewState["action"].ToString() == "edit")
            {
                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        ddlUserType.SelectedValue = dtr["UA_TYPE"] == DBNull.Value ? "0" : dtr["UA_TYPE"].ToString();
                        txtFName.Text = dtr["UA_FULLNAME"] == DBNull.Value ? string.Empty : dtr["UA_FULLNAME"].ToString();
                        txtDesignation.Text = dtr["UA_DESIG"] == DBNull.Value ? string.Empty : dtr["UA_DESIG"].ToString();

                        //ddlDept.SelectedValue = dtr["UA_DEPTNO"] == DBNull.Value ? "0" : dtr["UA_DEPTNO"].ToString();

                        //txtQtrRoomNo.Text = dtr["UA_QTRNO"] == null ? string.Empty : dtr["UA_QTRNO"].ToString();
                        txtUsername.Text = dtr["UA_NAME"] == DBNull.Value ? string.Empty : dtr["UA_NAME"].ToString();
                        txtUsername.ToolTip = idno.ToString();
                        //hdfPassword.Value = dtr["UA_PWD"] == DBNull.Value ? string.Empty : dtr["UA_PWD"].ToString();

                        chkActive.Checked = dtr["UA_STATUS"].ToString() == "0" ? true : false;
                        chkDEC.Checked = dtr["UA_DEC"].ToString() == "1" ? true : false;

                        txtEmail.Text = dtr["UA_EMAIL"] == DBNull.Value ? string.Empty : dtr["UA_EMAIL"].ToString();
                        //lblPassStatus.Text = "To use existing password, keep blank.";
                        //txtMobile.Text = dtr["UA_MOBILE"] == DBNull.Value ? string.Empty : dtr["UA_MOBILE"].ToString();

                        if (domainCount > 0)
                        {
                            //fill link tree view
                            //Added role_links code by Arjun on Date :27012023 for Disabled Default Role Links
                            Fill_TreeLinks(tvLinks, dtr["UA_ACC"] == DBNull.Value ? string.Empty : dtr["UA_ACC"].ToString(), dtr["role_links"] == DBNull.Value ? string.Empty : dtr["role_links"].ToString());
                        }


                        ViewState["ExistUser"] = true;
                        //rfvPassword.Visible = false;
                    }
                }
            }
            else
                if (dtr.Read())
                {
                    //if (Convert.ToInt16(Session["userno"].ToString()) == idno) 
                    Session["userlinks"] = dtr["UA_ACC"].ToString();

                    //Added role_links code by Arjun on Date :27012023 for Disabled Default Role Links
                    Fill_TreeLinks(tvLinks, dtr["UA_ACC"] == null ? string.Empty : dtr["UA_ACC"].ToString(), dtr["role_links"] == DBNull.Value ? string.Empty : dtr["role_links"].ToString());
                }
            dtr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "user_rights.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void SetStatus()
    {
        if (chkActive.Checked == true)
        {
            chkActive.ForeColor = System.Drawing.Color.Green;
            chkActive.Text = "Active";
        }
        else
        {
            chkActive.ForeColor = System.Drawing.Color.Red;
            chkActive.Text = "InActive";
        }
    }

    private void ShowPanel()
    {
        DataSet ds = null;
        if (Session["usertype"].ToString() == "1")
        {
            if (ddlUserType.SelectedValue == "2")
            {
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0", "UA_TYPE,UA_NO");

                //comment by satish
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "A.UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "A.UA_NO IS NOT NULL AND  UA_STATUS = 0 AND ISNULL(CAN,0) = 0  AND UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }
            else
            {
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND UA_DEPTNO=" + ddlDept.SelectedValue + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)" + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }
            if (Convert.ToInt32(ddlSubDept.SelectedValue) > 0)
            {
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND UA_EMPDEPTNO=" + ddlSubDept.SelectedValue + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND (UA_EMPDEPTNO=" + Convert.ToInt32(ddlSubDept.SelectedValue) + " OR " + Convert.ToInt32(ddlSubDept.SelectedValue) + "=0)" + " AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }

        }
        else
            ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "' ' AS UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0 " + (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1" ? " AND UA_TYPE= 3 AND (UA_DEC IS NULL OR UA_DEC = 0) AND UA_DEPTNO= " + Session["userdeptno"].ToString() : Session["usertype"].ToString() == "4" ? " AND UA_TYPE= 3 AND UA_DEC = 1" : " AND R.PARENT_USERTYPE =" + Session["usertype"].ToString()), "UA_TYPE,UA_NO,UA_NAME");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                // pnlUser.Visible = true;
                lblEmpty.Visible = false;
                foreach (RepeaterItem item in lvlinks.Items)
                {
                    //Label lblUserpass = item.FindControl("lblUserpass") as Label;
                    //lblUserpass.Text = Common.DecryptPassword(lblUserpass.Text.ToString());

                    ///STATUS COLOR CHANGE
                    Label Status = item.FindControl("lblUStatus") as Label;
                    if (Status.Text.Trim().ToUpper() == "0")
                    {
                        Status.Text = "Active";
                        Status.Style.Add("color", "Green");
                    }
                    if (Status.Text.Trim().ToUpper() == "1")
                    {
                        Status.Text = "Blocked";
                        Status.Style.Add("color", "Red");
                    }
                }
                lvlinks.Visible = true;
            }
            else
            {
                // objCommon.DisplayMessage(this.updEdit, "No Record Found!", this.Page);
                lblEmpty.Visible = true;
                lvlinks.Visible = false;
                // pnlUser.Visible = false;
            }
        }

    }

    private void ShowPanelStudent()
    {
        DataSet ds = null;
        if (Session["usertype"].ToString() == "1")
        {
            if (ddlUserType.SelectedValue == "2")
            {
                //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0", "UA_TYPE,UA_NO");

                //comment by satish
                // ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "A.UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "A.UA_NO IS NOT NULL AND  UA_STATUS = 0 AND ISNULL(CAN,0) = 0  AND UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");

                //added by satish
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_STUDENT S ON A.UA_IDNO= S.IDNO", "A.UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_TYPE=" + Convert.ToInt32(ddlUserType.SelectedValue) + " AND UA_STATUS = 0 " + " AND CAN=0 AND ADMCAN=0 AND (DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0)", "UA_FULLNAME");

            }
            else
            {
                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            }

        }
        else
            ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO,UA_FULLNAME,UA_NAME", "' ' AS UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND UA_NO <> 0 " + (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1" ? " AND UA_TYPE= 3 AND (UA_DEC IS NULL OR UA_DEC = 0) AND UA_DEPTNO= " + Session["userdeptno"].ToString() : Session["usertype"].ToString() == "4" ? " AND UA_TYPE= 3 AND UA_DEC = 1" : " AND R.PARENT_USERTYPE =" + Session["usertype"].ToString()), "UA_TYPE,UA_NO,UA_NAME");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                // pnlUser.Visible = true;
                lblEmpty.Visible = false;
                foreach (RepeaterItem item in lvlinks.Items)
                {
                    //Label lblUserpass = item.FindControl("lblUserpass") as Label;
                    //lblUserpass.Text = Common.DecryptPassword(lblUserpass.Text.ToString());

                    ///STATUS COLOR CHANGE
                    Label Status = item.FindControl("lblUStatus") as Label;
                    if (Status.Text.Trim().ToUpper() == "0")
                    {
                        Status.Text = "Active";
                        Status.Style.Add("color", "Green");
                    }
                    if (Status.Text.Trim().ToUpper() == "1")
                    {
                        Status.Text = "Blocked";
                        Status.Style.Add("color", "Red");
                    }
                }
                lvlinks.Visible = true;
            }
            else
            {
                // objCommon.DisplayMessage(this.updEdit, "No Record Found!", this.Page);
                lblEmpty.Visible = true;
                lvlinks.Visible = false;
                // pnlUser.Visible = false;
            }
        }
        else
        {
            // objCommon.DisplayMessage(this.updEdit, "Error!", this.Page);
            // pnlUser.Visible = false;
            lblEmpty.Visible = true;
        }
    }

    #endregion


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserType.SelectedIndex > 0)
        {
            chkListAccLink.Enabled = true;
        }

        ShowPanelStudent();
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserType.SelectedIndex > 0)
        {
            chkListAccLink.Enabled = true;
        }
        ShowPanel();



    }

    private void PopulateAccessLink()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Acc_Section", "AS_No", "AS_Title", "AS_No>0 AND IS_ACTIVE=1", "AS_No");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkListAccLink.DataTextField = "AS_Title";
                    chkListAccLink.DataValueField = "AS_No";
                    chkListAccLink.DataSource = ds.Tables[0];
                    chkListAccLink.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_ActivityMaster.PopulateUserTypes --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void test1()
    {
        foreach (ListItem item in chkListAccLink.Items)
        {


            if (item.Selected)
            {
                if (activity.Page_links.Length > 0)
                    activity.Page_links += ",";
                activity.Page_links += item.Value;
            }
        }

        ShowDetails(Convert.ToInt32(ddlUserType.SelectedValue));
        tvLinks.Visible = true;

        btnSubmit.Enabled = false;
    }
    protected void chkListAccLink_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in chkListAccLink.Items)
        {
            if (item.Selected)
            {
                if (activity.Page_links.Length > 0)
                    activity.Page_links += ",";
                activity.Page_links += item.Value;
            }
        }
        if (activity.Page_links.Length > 1)
        {
            test1();
            test2();
        }
        else
        {
            test2();
            test1();
            btnSubmit.Enabled = true;
        }

        int i;
        foreach (string value in activity.Page_links.Split(','))
        {
            if (value != "")
            {
                int val = Convert.ToInt32(value);
                for (i = 0; i < chkListAccLink.Items.Count; i++)
                {
                    if (chkListAccLink.Items[i].Value == val.ToString())
                    {
                        chkListAccLink.Items[i].Selected = true;

                        break;
                    }
                }
            }
        }

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "a.DEGREENO");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
        }
    }
    protected void ddlSubDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPanel();
    }
}