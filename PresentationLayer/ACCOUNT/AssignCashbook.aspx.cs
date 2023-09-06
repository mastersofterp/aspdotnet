//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : CASHBOOK ASSIGN
// CREATION DATE : 24-MAY-2015                                               
// CREATED BY    : NAKUL CHAWRE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.UI.HtmlControls;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;

public partial class ACCOUNT_AssignCashbook : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    string back = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        { back = Request.QueryString["obj"].ToString().Trim(); }

        if (!Page.IsPostBack)
        {
            ViewState["action"] = "update";
            CheckPageAuthorization();

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                //Page Authorization
                //CheckPageAuthorization();


                Page.Title = Session["coll_name"].ToString();
                
                PopulateDropDownList();
                BindCashBook();

            }
        }
    }

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        DataSet ds = objCommon.GetDropDownData("PKG_USER_ACC_SP_RET_USERTYPES");
        ddlUserType.DataSource = ds;
        ddlUserType.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddlUserType.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddlUserType.DataBind();
    }

    private void BindCashBook()
    {
        DataSet ds = objCommon.GetDropDownData("PKG_ACC_GET_CASH_BOOK");
        chkCashbook.DataSource = ds;
        chkCashbook.DataValueField = ds.Tables[0].Columns[0].ToString();
        chkCashbook.DataTextField = ds.Tables[0].Columns[1].ToString();
        chkCashbook.ToolTip = ds.Tables[0].Columns[0].ToString();
        chkCashbook.DataBind();
    }

    #endregion Private Method

    #region Control Events

    protected void btnShow_Click(object sender, EventArgs e)
    {
        User_AccController objUACC = new User_AccController();
        DataSet ds = objUACC.GetUsersByUserType(Convert.ToInt32(ddlUserType.SelectedValue));

        pnlListMain.Visible = true;
        btnAssign.Visible = true;
        lvUsers.DataSource = ds;
        lvUsers.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlUserType.SelectedValue = "0";
        lvUsers.DataSource = null;
        lvUsers.DataBind();
        btnAssign.Visible = false;
        BindCashBook();
        pnlListMain.Visible = false;
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            FinCashBookController objCBC = new FinCashBookController();
            string uanos = string.Empty;
            string links = string.Empty;
            string IPAddress = string.Empty;

            string date = DateTime.Now.ToString("dd-MMM-yyyy");

            //get selected users
            foreach (ListItem ChkCashBook in chkCashbook.Items)
            {
                //CheckBox ChkCashbook = chkCashbook.FindControl("chkCashbook") as CheckBox;
                if (ChkCashBook.Selected == true)
                {
                    if (uanos == string.Empty)
                        uanos += ChkCashBook.Value;
                    else
                        uanos += "," + ChkCashBook.Value;
                }
            }

            if (uanos == string.Empty)
            {
                objCommon.DisplayUserMessage(updCashBookAssign, "Please Select At least one cash book", this.Page);
                return;
            }
            //get selected users

            if (!dt.Columns.Contains("UserNo"))
                dt.Columns.Add("UserNo");
            foreach (ListViewDataItem lvItem in lvUsers.Items)
            {
                CheckBox chkAccept = lvItem.FindControl("chkAccept") as CheckBox;
                if (chkAccept.Checked == true)
                {
                    DataRow dr = dt.NewRow();


                    //uanos += chkAccept.ToolTip + ",";
                    dr["UserNo"] = chkAccept.ToolTip;
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count <= 0)
            {
                objCommon.DisplayUserMessage(updCashBookAssign, "Please Select At least one user", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objCBC.AssignCashBook(dt, uanos, date, Convert.ToInt32(Session["userno"]), IPAddress, Convert.ToInt32(ddlUserType.SelectedValue));

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                Clear();
                objCommon.DisplayUserMessage(updCashBookAssign, "Cash Book Assigned Successfully!!!", this.Page);
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

    #endregion Control Events


    protected void lvUsers_DataBound(object sender, EventArgs e)
    {

    }
    protected void lvUsers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            CheckBox chkAccept = e.Item.FindControl("chkAccept") as CheckBox;
            HtmlGenericControl divCashBook = (HtmlGenericControl)e.Item.FindControl("divCashBook");

            DataSet ds = objCommon.FillDropDown("ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + chkAccept.ToolTip + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "  COMPANY_NAME", "", "");
            string list = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list = list + ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString() +"<br/>";
            }
            divCashBook.InnerHtml = list;
        }
    }
}
