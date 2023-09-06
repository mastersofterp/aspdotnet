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

using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer;

public partial class STORES_Transactions_Quotation_Str_Return_Items : System.Web.UI.Page
{
    Common ObjComman = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
    STR_ISSUE_ITEM_DEPARTMENT_CONTROLLER objIssueItemController = new STR_ISSUE_ITEM_DEPARTMENT_CONTROLLER();
    CustomStatus cs = new CustomStatus();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    //lblHelp.Text = ObjComman.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // lbluser.Text = Session["userfullname"].ToString();
                // lblDept.Text = Session["strdeptname"].ToString();
                ViewState["butAction"] = "add";
                //Session["dtitems"] = null;
                //InitializeItemTable();
            }
            FillDept();
            GenIssueNo();

            txtItemIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

    private void FillDept()
    {

        ObjComman.FillDropDownList(ddlDept, "STORE_SUBDEPARTMENT a inner join STORE_DEPARTMENT b on (a.MDNO=b.MDNO)", "a.SDNO", "a.SDNAME", "b.MDNO=1", "SDNAME");
        ObjComman.FillDropDownList(ddlItemGroup, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "", "");
    }

    void GenIssueNo()
    {
        DataSet ds = new DataSet();
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ds = objDeptReqController.GenrateIssueNo(mdno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtIssueSlipNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ISSUESLIPNO"].ToString());
        }
    }

    protected void ddlItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjComman.FillDropDownList(ddlItems, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlItemGroup.SelectedValue), "");
    }

    protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ObjComman.FillDropDownList(ddlSrno, "STORE_ISSUE_ITEM a inner join STORE_ISSUE_ITEM_TRAN b on (a.ISSUENO=b.ISSUENO)", "b.ISSUE_TNO", "b.serial_no", "issue_from='A' and (b.STATUS is null or b.status<>'R') and b.ITEM_NO=" + ddlItems.SelectedValue + " and isnull(serial_no,'')<>''", "");
            int itemno = Convert.ToInt32(ddlItems.SelectedValue);
            DataSet ds = new DataSet();
            ds = objDeptReqController.GetFixedAssetItems(itemno);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvGrandMaster.DataSource = ds;
                    lvGrandMaster.DataBind();
                    lvGrandMaster.Visible = true;
                }
                else
                {
                    lvGrandMaster.DataSource = null;
                    lvGrandMaster.DataBind();
                    lvGrandMaster.Visible = false;
                }
            }
            else
            {
                lvGrandMaster.DataSource = null;
                lvGrandMaster.DataBind();
                lvGrandMaster.Visible = false;
            }

        }
        catch (Exception ex)
        {

        }

    }


    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        STR_ISSUE_ITEMS objIssueItem = new STR_ISSUE_ITEMS();
        objIssueItem._COLLEGE_CODE = Session["colcode"].ToString();
        objIssueItem._ISSUE_SLIPNO = txtIssueSlipNo.Text;
        objIssueItem._ISSUE_DATE = Convert.ToDateTime(txtItemIssueDate.Text);
        int count = 0;
        string strselectd = string.Empty;
        string strddlcondition = string.Empty;
        foreach (ListViewItem i in lvGrandMaster.Items)
        {
            CheckBox chk = i.FindControl("chkcheck") as CheckBox;
            Label lblSerialNo = i.FindControl("lblSerialNo") as Label;
            DropDownList ddlCondition = i.FindControl("ddlcondition") as DropDownList;
            if (chk.Checked == true)
            {
                strselectd = chk.ToolTip.ToString();
                strddlcondition = ddlCondition.SelectedItem.Text;
                count++;
                cs = (CustomStatus)objIssueItemController.ReturnItem(objIssueItem, Convert.ToInt32(ddlItems.SelectedValue), strselectd, Convert.ToInt32(ddlDept.SelectedValue), txtRemark.Text, strddlcondition.Trim(','));
            }


        }
        if (count == 0)
        {
            ObjComman.DisplayUserMessage(UpdPnlMain, "Please Select Atleast One Item", this.Page);
        }

        //CustomStatus cs = (CustomStatus)objIssueItemController.ReturnItem(objIssueItem, Convert.ToInt32(ddlItems.SelectedValue), strselectd.Trim(','), Convert.ToInt32(ddlDept.SelectedValue), txtRemark.Text, strddlcondition.Trim(','));

        if (cs.Equals(CustomStatus.RecordSaved))
        {

            ObjComman.DisplayUserMessage(UpdPnlMain, "Item Return Successfully", this.Page);
            lvGrandMaster.Visible = false;
            GenIssueNo();
            Clear();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlItemGroup.SelectedValue = "0";
        ddlItems.SelectedValue = "0";
        ddlDept.SelectedValue = "0";
        txtItemIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlItemGroup.SelectedValue = "0";
        ddlItems.Items.Clear();
        ddlItems.Items.Add("Please Select");
        ddlItems.SelectedItem.Value = "0";
        lvGrandMaster.Visible = false;
        //ddlSrno.Items.Clear();
        //ddlSrno.Items.Add("Please Select");
        //ddlSrno.SelectedItem.Value = "0";
        txtRemark.Text = string.Empty;
    }
}