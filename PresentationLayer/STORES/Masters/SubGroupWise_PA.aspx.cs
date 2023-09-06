using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STORES_Masters_SubGroupWise_PA : System.Web.UI.Page
{
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
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
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                BindSubGroup();
                //BindEmployees();
                bindGroupName();
            }

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

    private void BindSubGroup()
    {
        try
        {
            int ua_no = Convert.ToInt32(Session["userno"]);
            int dept = Convert.ToInt32(Session["strdeptcode"]);
            objCommon.FillDropDownList(ddlDept, "STORE_SUBDEPARTMENT S INNER JOIN STORE_DEPARTMENT D ON (D.MDNO=S.MDNO) INNER JOIN STORE_DEPARTMENTUSER DU ON (DU.MDNO=D.MDNO)", "MDNAME", "SDSNAME", "D.MDNO=" + dept + " AND Du.UA_NO=" + ua_no, "SDSNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.BindListViewPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void bindGroupName()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "ISAPPROVED IS NULL", "MISGNAME");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvItemMaster.DataSource = ds;
                    lvItemMaster.DataBind();
                    lvItemMaster.Visible = true;
                }
                else
                {
                    lvItemMaster.DataSource = null;
                    lvItemMaster.DataBind();
                    lvItemMaster.Visible = false;
                }
            }
            else
            {
                lvItemMaster.DataSource = null;
                lvItemMaster.DataBind();
                lvItemMaster.Visible = false;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int str = 0;
        int count = 0;
        string remark = string.Empty;
        int ret = 0;
        foreach (ListViewItem i in lvItemMaster.Items)
        {
            CheckBox chk = i.FindControl("chkSelect") as CheckBox;
            TextBox txt = i.FindControl("txtRemark") as TextBox;
            Boolean isapp = false;
            if (chk.Checked == true)
            {
                str = Convert.ToInt32(chk.ToolTip.ToString());
                remark = txt.Text;
                isapp = true;
                ret = objStrMaster.UpdateSubGroupEntry(str, isapp, remark);
                count++;

            }
            if (count == 0)
            {
                objCommon.DisplayMessage("Please Select Atleast One Item", Page);
                return;
            }
        }
        if (ret > 0)
        {
            objCommon.DisplayMessage("Record Saved Successfully", Page);
            bindGroupName();
            return;
        }

    }
}