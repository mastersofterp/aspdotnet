using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Masters_ToolkitMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
            }
            GetItemSubGrp();
            GetToolkit();
        }
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

    #region Private Methods

    private void GetItemSubGrp()
    {
        objCommon.FillDropDownList(ddlItemSubGrp, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "ISTOOLKIT=1", "MISGNAME");
    }

    //private void GetItems()
    //{
    //    objCommon.FillDropDownList(ddlItems, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + ddlItemSubGrp.SelectedValue + "", "ITEM_NAME");
    //}

    private void GetToolkit()
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("STORE_TOOLKIT ", "TK_NO", "TK_NAME", "", "TK_NAME");
        if (ds.Tables[0] != null)
        {
            lvStage.DataSource = ds.Tables[0];
            lvStage.DataBind();
        }
    }

    #endregion Private Methods

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ToolKitName = txttoolkitname.Text;
            string str = string.Empty;
            int count = 0;
            foreach (ListViewItem i in lvitemInvoice.Items)
            {
                CheckBox chk = i.FindControl("chkSelect") as CheckBox;
                if (chk.Checked == true)
                {
                    str += chk.ToolTip.ToString() + ",";
                    count++;
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(UpdatePanel1,"Please Select Atleast One Item", Page);
                return;
            }
            if (ViewState["ToolKitNo"] == null)
            {
                int Count = Convert.ToInt32(objCommon.LookUp("STORE_TOOLKIT", "count(*)", "TK_NAME='" + ToolKitName + "'"));
                if (Count <= 0)
                {
                    CustomStatus cs = (CustomStatus)objStrMaster.InsertToolKitData(ToolKitName, str.Trim(','), Convert.ToInt32(Session["colcode"]));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Record Save Successfull", this);
                        txttoolkitname.Text = string.Empty;
                        ViewState["ToolKitNo"] = null;
                        //ddlItems.Items.Clear();
                        GetItemSubGrp();
                        GetToolkit();
                        clear();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record already exists", this);
                    return;
                }
            }
            else
            {
                //int Count = Convert.ToInt32(objCommon.LookUp("STORE_TOOLKIT", "count(*)", "TK_NAME='" + ToolKitName + "' and ITEM_NO=" + ItemNo + ""));
                //if (Count <= 0)
                //{
                CustomStatus cs = (CustomStatus)objStrMaster.UpdateToolKitData(Convert.ToInt32(ViewState["ToolKitNo"]), ToolKitName, str.Trim(','), Convert.ToInt32(Session["colcode"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfull", this);
                    txttoolkitname.Text = string.Empty;
                    ViewState["ToolKitNo"] = null;
                    //ddlItems.Items.Clear();
                    GetItemSubGrp();
                    GetToolkit();
                    clear();
                }
                //}
                //else
                //{
                //    objCommon.DisplayMessage(UpdatePanel1, "Record already exists", this);
                //    return;
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        txttoolkitname.Text = string.Empty;
        ddlItemSubGrp.SelectedValue = "0";
        //ddlItems.Items.Clear();
        ViewState["ToolKitNo"] = null;
        lvitemInvoice.Visible = false;
    }

    protected void btnshowrpt_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["ToolKitNo"] = btnEdit.CommandArgument;
            ViewState["action"] = "edit";
            int a = Convert.ToInt32(ViewState["ToolKitNo"]);
            DataSet ds = objStrMaster.GetToolKitDetails(a);

            string ITEMNOS = objCommon.LookUp("STORE_TOOLKIT", "ITEM_NO", "TK_NO='" + a + "'");
            txttoolkitname.Text = ds.Tables[0].Rows[0]["TK_NAME"].ToString();
            ddlItemSubGrp.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();
            bindItems();
            foreach (ListViewItem i in lvitemInvoice.Items)
            {
                CheckBox chk = i.FindControl("chkSelect") as CheckBox;
                int itemcount = Convert.ToInt32(objCommon.LookUp("STORE_TOOLKIT", "count(1)", "TK_NO='" + a + "' AND '" + Convert.ToInt32(chk.ToolTip) + "' IN(" + ITEMNOS + ")"));

                Label lbl = i.FindControl("lblReqQty") as Label;
                if (itemcount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        GetToolkit();
    }

    protected void ddlItemSubGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindItems();
    }

    private void bindItems()
    {
        //GetItems();
        int subgroup = Convert.ToInt32(ddlItemSubGrp.SelectedValue);
        DataSet ds = objCommon.FillDropDown("STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + subgroup, "");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvitemInvoice.DataSource = ds;
                lvitemInvoice.DataBind();
                lvitemInvoice.Visible = true;
            }
            else
            {
                lvitemInvoice.DataSource = null;
                lvitemInvoice.DataBind();
                lvitemInvoice.Visible = false;
                objCommon.DisplayUserMessage(UpdatePanel1, "No Items Found", Page);
            }
        }
        else
        {
            lvitemInvoice.DataSource = null;
            lvitemInvoice.DataBind();
            lvitemInvoice.Visible = false;
            objCommon.DisplayUserMessage(UpdatePanel1, "No Items Found", Page);
        }
    }
}