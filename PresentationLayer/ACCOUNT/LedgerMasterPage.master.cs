using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using System.Data.SqlClient;

using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class LedgerMasterPage : System.Web.UI.MasterPage
{
    ////Common objCommon = new Common();
    //UAIMS_Common objUCommon = new UAIMS_Common();
    //private string nitprm_constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //Very Important
    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetColor", "<Script language='javascript' type='text/javascript'> function SetTextBackColor() { var cl='" + Application["TextBackColor"] + "';  return cl;  } </Script> ");
    //    Response.Cache.SetAllowResponseInBrowserHistory(false);
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.Cache.SetNoStore();
    //    Response.Expires = 0;
    //    Response.CacheControl = "no-cache";
    //    if (!Page.IsPostBack)
    //    {
    //        //Check Session
    //        if (Session["userno"] == null || Session["username"] == null ||
    //            Session["usertype"] == null || Session["userfullname"] == null)
    //        {
    //            Response.Redirect("~/default.aspx");
    //        }
    //        //Left Link height
    //        int ht = Convert.ToInt32(Session["screenheight"]);
    //        master_left1.Height = Unit.Pixel(ht - (ht * 22) / 100);
    //        lblLink.Text = "Welcome " + Session["username"].ToString().ToUpper();
    //        //Page title
    //        Page.Title = Session["coll_name"].ToString();
    //        lblColName.Text = Session["coll_name"].ToString();
    //        try
    //        {
    //            //Fill the Treeview with Links and expand it to last state
    //                Fill_TreeLinks(tvLinks);
    //            // Disable ExpandDepth if the TreeView's expand/collapse
    //            // state is stored in session.
    //            if (Session["TreeViewState"] != null)
    //                tvLinks.ExpandDepth = -1;
//            if (Session["TreeViewState"] == null)
    //            {
    //                // Record the TreeView's current expand/collapse state.
    //                //List<string> list = new List<string>;
    //                List<string> list = new List<string>(tvLinks.Nodes.Count);
    //                SaveTreeViewState(tvLinks.Nodes, list);
    //                Session["TreeViewState"] = list;
    //            }
    //            else
    //            {
    //                // Apply the recorded expand/collapse state to the TreeView.
    //                List<string> list = (List<string>)Session["TreeViewState"];
    //                RestoreTreeViewState(tvLinks.Nodes, list);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}

    ////Fill the Treeview with links
    //public void Fill_TreeLinks(TreeView tvLinks)
    //{
    //    SQLHelper objSH = new SQLHelper(nitprm_constr);
    //    SqlDataReader drLinks = null;

    //    int linkno = 0;
    //    int mastno = 0;
    //    int url_idno = 0;
    //    TreeNode xx = null;
    //    TreeNode yy = null;
    //    TreeNode zz = null;

        
    //    try
    //    {
    //        int userno = int.Parse(Session["userno"].ToString());

    //        //Get all user links
    //        SqlParameter[] objParams = new SqlParameter[1];
    //        objParams[0] = new SqlParameter("@P_UA_NO", userno);

    //        //Get all user links
    //        drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams);

    //        //loop thru links
    //        while (drLinks.Read())
    //        {
    //            if (drLinks["al_link"].ToString().Trim() != string.Empty)
    //            {
    //                if (drLinks["url_idno"] != null & drLinks["url_idno"].ToString() != string.Empty)
    //                    url_idno = int.Parse(drLinks["url_idno"].ToString());

    //                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty & int.Parse(drLinks["al_asno"].ToString()) != linkno)
    //                {
    //                    xx = new TreeNode();  // this is defination of the node.
    //                    xx.Text = drLinks["as_title"].ToString();
    //                    xx.NavigateUrl = "";
    //                    xx.SelectAction = TreeNodeSelectAction.Expand;

    //                    // adding node to root
    //                    tvLinks.Nodes.Add(xx);

    //                    if (drLinks["al_url"].ToString().Trim() == string.Empty)
    //                    {
    //                        zz = new TreeNode();    // this is defination of the node.
    //                        zz.Text = drLinks["al_link"].ToString();
    //                        zz.NavigateUrl = "";
    //                        zz.SelectAction = TreeNodeSelectAction.Expand;

    //                        mastno = int.Parse(drLinks["mastno"].ToString());
    //                        // adding node as child of node xx.
    //                        xx.ChildNodes.Add(zz);
    //                        xx.Expanded = false;
    //                    }
    //                    else
    //                    {
    //                        yy = new TreeNode();
    //                        yy.Text = drLinks["al_link"].ToString();
    //                        //yy.ID =  dr.GetInt32(6).ToString();

    //                        if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
    //                        {
    //                            if (int.Parse(drLinks["summer"].ToString()) == 1)
    //                                yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
    //                            else
    //                            {
    //                                string qry = string.Empty;
    //                                if (drLinks["al_url"].ToString().Contains("?"))
    //                                    qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
    //                                else
    //                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();

    //                                if (url_idno > 0)
    //                                    qry += "&idno=" + url_idno.ToString();
    //                                else
    //                                    qry += "&idno=-1";

    //                                //yy.NavigateUrl = qry;
    //                                yy.ToolTip = qry;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            string qry = string.Empty;
    //                            if (drLinks["al_url"].ToString().Contains("?"))
    //                                qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
    //                            else
    //                                qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();

    //                            if (url_idno > 0)
    //                                qry += "&idno=" + url_idno.ToString();
    //                            else
    //                                qry += "&idno=-1";

    //                            //yy.NavigateUrl = qry;
    //                            yy.ToolTip = qry;
    //                        }
    //                        if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
    //                        {
    //                            if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
    //                            {
    //                                // adding node as child of node xx.
    //                                //zz = new TreeNode();
    //                                zz.ChildNodes.Add(yy);
    //                                zz.Expanded = false;
    //                            }
    //                            else
    //                            {
    //                                // adding node as child of node xx.
    //                                xx.ChildNodes.Add(yy);
    //                                xx.Expanded = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            // adding node as child of node xx.
    //                            xx.ChildNodes.Add(yy);
    //                            xx.Expanded = false;
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    if (drLinks["al_url"].ToString() == string.Empty)
    //                    {
    //                        zz = new TreeNode();   // this is defination of the node.
    //                        zz.Text = drLinks["al_link"].ToString();
    //                        if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
    //                            mastno = int.Parse(drLinks["mastno"].ToString());
    //                        zz.NavigateUrl = "";
    //                        zz.SelectAction = TreeNodeSelectAction.Expand;

    //                        // adding node as child of node xx.
    //                        xx.ChildNodes.Add(zz);
    //                        xx.Expanded = false;
    //                    }
    //                    else
    //                    {
    //                        yy = new TreeNode();
    //                        yy.Text = drLinks["al_link"].ToString();
    //                        //yy.ID =  Val(rs1.Fields("al_no").Value & string.Empty)

    //                        if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
    //                        {
    //                            if (int.Parse(drLinks["summer"].ToString()) == 1)
    //                            {
    //                                yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
    //                            }
    //                            else
    //                            {
    //                                string qry = string.Empty;
    //                                if (drLinks["al_url"].ToString().Contains("?"))
    //                                    qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
    //                                else
    //                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();

    //                                if (url_idno > 0)
    //                                    qry += "&idno=" + url_idno.ToString();
    //                                else
    //                                    qry += "&idno=-1";
    //                                //yy.NavigateUrl = qry;
    //                                yy.ToolTip = qry;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            string qry = string.Empty;
    //                            if (drLinks["al_url"].ToString().Contains("?"))
    //                                qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
    //                            else
    //                                qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();

    //                            yy.ToolTip = qry;
    //                        }

    //                        if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
    //                        {
    //                            if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
    //                            {
    //                                // adding node as child of node xx.
    //                                //zz = new TreeNode();
    //                                zz.ChildNodes.Add(yy);
    //                                zz.Expanded = false;
    //                            }
    //                            else
    //                            {
    //                                // adding node as child of node xx.
    //                                xx.ChildNodes.Add(yy);
    //                                xx.Expanded = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            // adding node as child of node xx.
    //                            xx.ChildNodes.Add(yy);
    //                            xx.Expanded = false;
    //                        }
    //                    }
    //                }
    //            }

    //            if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty)
    //                linkno = int.Parse(drLinks["al_asno"].ToString());
    //            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
    //                mastno = int.Parse(drLinks["mastno"].ToString());
    //        }

    //        //Change Password for All
    //        xx = new TreeNode();
    //        //xx.ID = "ChPw";
    //        xx.Text = "Change Password";
    //        //xx.Target = "main";
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
    //            objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    finally
    //    {
    //        //close all objects
    //        if (drLinks != null) drLinks.Close();
    //    }
    //}

    //protected void lnkTheme1_Click(object sender, EventArgs e)
    //{
    //    Session["masterpage"] = "SiteMasterPage.master";
    //    Response.Redirect(Request.Url.ToString());
    //}

    //protected void lnkTheme2_Click(object sender, EventArgs e)
    //{
    //    Session["masterpage"] = "SiteMasterPage2.master";
    //    Response.Redirect(Request.Url.ToString());
    //}

    //protected void tvLinks_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    //If the selected node is changed and we are working with the top node (If you want the ExpandCollapse func further dow, duplicate this part..)
    //    if (tvLinks.SelectedNode.Depth == 0)
    //    {
    //        tvLinks.CollapseAll();         //Collapse all nodes
    //        tvLinks.SelectedNode.Expand(); //Expand the selected node
    //    }

    //    //Save the state of the treeview
    //    if (IsPostBack)
    //    {
    //        List<string> list = new List<string>(tvLinks.Nodes.Count);
    //        SaveTreeViewState(tvLinks.Nodes, list);
    //        Session["TreeViewState"] = list;
    //    }

    //    //All done, lets redirect to the new page
    //    if (IsPostBack)
    //    {
    //        //get the page link from tooltip of the selected node, so that we can redirect to it.
    //        string link = string.Empty;
    //        link = tvLinks.SelectedNode.ToolTip;
    //        Response.Redirect("~/" + link.ToString());
    //    }
    //}

    ////The save state func...
    //private void SaveTreeViewState(TreeNodeCollection nodes, List<string> list)
    //{
    //    // Recursivley record all expanded nodes in the List.
    //    foreach (TreeNode node in nodes)
    //    {
    //        if (node.ChildNodes != null && node.ChildNodes.Count != 0)
    //        {
    //            if (node.Expanded.HasValue && node.Expanded == true && !String.IsNullOrEmpty(node.Text))
    //                list.Add(node.Text);
    //            SaveTreeViewState(node.ChildNodes, list);
    //        }
    //    }
    //}

    ////The restore state func...
    //private void RestoreTreeViewState(TreeNodeCollection nodes, List<string> list)
    //{
    //    foreach (TreeNode node in nodes)
    //    {
    //        // Restore the state of one node.
    //        if (list.Contains(node.Text))
    //        {
    //            if (node.ChildNodes != null && node.ChildNodes.Count != 0 && node.Expanded.HasValue && node.Expanded == false)
    //                node.Expand();
    //        }
    //        else
    //        {
    //            if (node.ChildNodes != null && node.ChildNodes.Count != 0 && node.Expanded.HasValue && node.Expanded == true)
    //                node.Collapse();
    //        }
    //        // If the node has child nodes, restore their state, too.
    //        if (node.ChildNodes != null && node.ChildNodes.Count != 0)
    //            RestoreTreeViewState(node.ChildNodes, list);
    //    }
    //}

    //protected void lnkHome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/home.aspx");
    //}
    ////protected void TextBox1_TextChanged(object sender, EventArgs e)
    ////{

        
    ////    foreach ( TreeNode nd in tvLinks.Nodes)
    ////    {
    ////        string var=selectNode(nd.ChildNodes);
    ////        if (var == "Selected")
    ////        {
    ////            return;
    ////        }
            
    ////    }

      

    ////}

    ////private string selectNode(TreeNodeCollection n)
    ////{
    ////    string IsSel=string.Empty; 

    ////    foreach (TreeNode nc in n)
    ////    {
    ////        string FirstString=string.Empty;
    ////        string LastString = string.Empty;
    ////        if (TextBox1.Text.Length >= nc.Text.Length)
    ////        {
    ////            FirstString = nc.Text;
    ////            LastString = nc.Text;
    ////        }
    ////        else
    ////        {
    ////            FirstString = nc.Text.Substring(0, TextBox1.Text.Length);
    ////            LastString = nc.Text.Substring(TextBox1.Text.Length + 1);
            
    ////        }

    ////            if (FirstString == TextBox1.Text)
    ////            {
                    
    ////                nc.Select();
    ////                nc.Selected = true; 
    ////                nc.Expanded = true;
    ////                int d = nc.Depth;
    ////                ExpandParentNode(nc,d);
    ////                IsSel = "Y";
    ////                return "Selected";

    ////            }

    ////            if (LastString == TextBox1.Text)
    ////            {
    ////                nc.Select();
    ////                nc.Selected = true;
    ////                nc.Expanded = true;
    ////                int d = nc.Depth;
    ////                ExpandParentNode(nc, d);
    ////                IsSel = "Y";
    ////                return "Selected";

    ////            }
           
    ////        if (IsSel != "Y")
    ////        {
    ////            selectNode(nc.ChildNodes);
            
    ////        }
            


    ////    }
    ////    return "NotSelected";
      

    ////}

    ////private void ExpandParentNode(TreeNode np,int depth)
    ////{
    ////    for (int i = 1; i <= depth+1; i++)
    ////    {
    ////        np.ExpandAll();
    ////        np=np.Parent;
        
    ////    }
        
    ////}
        

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 