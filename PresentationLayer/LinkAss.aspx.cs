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
public partial class ADMINISTRATION_LinkAss : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ConnectionStrings
    string nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //CheckPageAuthorization();

                //Populate the user dropdownlist with username and userid
                PopulateDropDownList();
                Fill_TreeLinks(tvLinks, string.Empty);
                
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() == "8")
                objCommon.FillDropDownList(ddlUser, "USER_RIGHTS R INNER JOIN USER_ACC U ON (U.UA_TYPE = R.USERTYPEID)", "DISTINCT USERTYPEID", "USERDESC", "USERTYPEID<>0  AND U.UA_DEPTNO IN(" + Convert.ToInt32(Session["userdeptno"]) + ") AND U.UA_TYPE=3", "USERTYPEID");
            else
                objCommon.FillDropDownList(ddlUser, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID>0", "USERTYPEID");
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

            //Added by abdul samad on 20/0/2017 parameter to show only assigned links
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        User_AccController objUAC = new User_AccController();
        string ua_nos = string.Empty;
        string links = string.Empty;
        foreach (ListViewDataItem item in lvDetail.Items)
        {
            CheckBox chk = item.FindControl("cbRow") as CheckBox;
            if (chk.Checked)
               // ua_no =ua_no + chk.ToolTip+",";
            ua_nos += chk.ToolTip + ",";
        }

        links = objCommon.GetLinks(tvLinks);
        if (links == "0,500")
        {
            objCommon.DisplayMessage("Please select at least one link.", this.Page);
            return;
        }
        links = links.Replace("0,500,","");
        int ret = objUAC.UpdateLinkDeptwise(ua_nos, links);

        if (ret !=-99)
            objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
        else
            objCommon.DisplayMessage("Transaction Failed.", this.Page);
        
        BindListView();
        Fill_TreeLinks(tvLinks, string.Empty);
                
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    private void BindListView()
    {
        DataSet ds = null;
        if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8")
        {
            if(ddlUser.SelectedValue!="2")
              //  ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue)+" OR "+Convert.ToInt32(ddlDept.SelectedValue)+"=0)", "UA_FULLNAME");
                ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND (UA_DEPTNO LIKE '%" + Convert.ToInt32(ddlDept.SelectedValue) + "%' OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");            
            else
                ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT  S ON (U.UA_IDNO=S.IDNO)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND CAN=0 AND ADMCAN=0 AND (DEGREENO="+Convert.ToInt32(ddlDegree.SelectedValue)+" OR "+Convert.ToInt32(ddlDegree.SelectedValue)+"=0) AND (BRANCHNO="+Convert.ToInt32(ddlBranch.SelectedValue)+" OR "+Convert.ToInt32(ddlBranch.SelectedValue)+"=0) AND (SEMESTERNO="+Convert.ToInt32(ddlSemester.SelectedValue)+" OR "+Convert.ToInt32(ddlSemester.SelectedValue)+"=0)", "UA_FULLNAME");
                
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDetail.DataSource = ds;
                lvDetail.DataBind();
                pnlDetail.Visible = true;
                btnSubmit.Enabled = true;
                pnlDetail.Visible = true;
            }
            else
            {
                lvDetail.DataSource = null;
                lvDetail.DataBind();
                pnlDetail.Visible = false;
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
            }
        }
    }


    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvDetail.DataSource = null;
        lvDetail.DataBind();
        pnlDetail.Visible = false;
        string  deptnos = string.Empty;
        if (Session["usertype"].ToString() == "8")
            deptnos = Convert.ToString(Session["userdeptno"]);
        //objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN USER_ACC U ON (D.DEPTNO=U.UA_DEPTNO)", "DISTINCT DEPTNO", "DEPTNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND UA_DEPTNO IN(" + Convert.ToInt32(Session["userdeptno"]) + ") AND DEPTNO>0", "DEPTNO");
        else
            deptnos = "0";
           //objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN USER_ACC U ON (D.DEPTNO=U.UA_DEPTNO)", "DISTINCT DEPTNO", "DEPTNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND DEPTNO>0", "DEPTNO");
        DataSet ds = objCommon.FillDropDownDepartmentUserWise(Convert.ToInt32(ddlUser.SelectedValue), deptnos);
       if (ds.Tables[0].Rows.Count > 0)
       {
           ddlDept.DataSource = ds;
           ddlDept.DataValueField = ds.Tables[0].Columns[0].ToString();
           ddlDept.DataTextField = ds.Tables[0].Columns[1].ToString();
           ddlDept.DataBind();
           ddlDept.SelectedIndex = 0;
       }

        if (ddlUser.SelectedValue == "2")
        {
            pnlStudent.Visible = true;
            trDept.Visible = false;
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            ddlBranch.Items.Clear(); ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear(); ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            pnlStudent.Visible = false;
            trDept.Visible = true;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, " ACD_COLLEGE_DEGREE_BRANCH A  inner join  ACD_BRANCH B on (a.BRANCHNO=b.BRANCHNO)", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }
}
