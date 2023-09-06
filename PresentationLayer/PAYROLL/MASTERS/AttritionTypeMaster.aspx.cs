
//======================================================================================
// PROJECT NAME  : RFC                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ ATTRITION TYPE                                                
// CREATION DATE : 13-01-2023
// CREATED BY    : Purva  Raut                                          
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class PAYROLL_MASTERS_AttritionTypeMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objPay = new PayController();
    EmpMaster empmas = new EmpMaster();
    string UsrStatus = string.Empty;
    int OrganizationId;
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
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                OrganizationId = Convert.ToInt32(Session["OrgId"]);
            }

            BindListView();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttritionTypeMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttritionTypeMaster.aspx");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        empmas.AttritionName = txtAttritionName.Text.ToString();
        empmas.OrganizationId = Convert.ToInt32(Session["OrgId"]);
        if (chkisActive.Checked == true)
        {
            empmas.IsActive = true;
        }
        else
        {
            empmas.IsActive = false;
        }
        empmas.COLLEGE_CODE = Session["colcode"].ToString();
        if (ViewState["action"] != null)
        {
            bool result = CheckPurpose();
            if (ViewState["action"].ToString().Equals("add"))
            {
                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    txtAttritionName.Text = "";
                    return;
                }
                else
                {
                    empmas.AttritionTypeNo = 0;
                    CustomStatus cs = (CustomStatus)objPay.AddAttritionTypeMaster(empmas);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                        ViewState["AttritionTypeNo"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
            else
            {
                 if (result == true)
                 {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        return;
                 }
                 else
                 {
                ViewState["action"] = "add";
                empmas.AttritionTypeNo = Convert.ToInt32(ViewState["AttritionTypeNo"].ToString().Trim());
                CustomStatus cs = (CustomStatus)objPay.AddAttritionTypeMaster(empmas);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                    ViewState["AttritionTypeNo"] = null;
                    Clear();
                }
                else
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
        }
    }
    private void Clear()
    {
        txtAttritionName.Text = "";
        ViewState["action"] = "add";
        ViewState["AttritionTypeNo"] = null;


    }
    private void BindListView()
    {
        try
        {
            empmas.AttritionTypeNo = 0;
            DataSet ds = objPay.GetAttritionTypeMaster(empmas);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAttritionType.DataSource = ds;
                lvAttritionType.DataBind();
            }
            else
            {
                lvAttritionType.DataSource = ds;
                lvAttritionType.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_AttritionTypeMaster.aspx.btnsubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet ds = new DataSet();

        // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
        ds = objCommon.FillDropDown("AttritionTypeMaster", "AttritionName", "", "AttritionName='" + txtAttritionName.Text.ToString() + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["AttritionTypeNo"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label LBLATTRITIONTYPE = lst.FindControl("lblAttritionName") as Label;
            Label LBLISACTIVE = lst.FindControl("lblisactive") as Label;
            txtAttritionName.Text = LBLATTRITIONTYPE.Text.Trim();
            if (LBLISACTIVE.Text == "true")
            {
                chkisActive.Checked = true;
            }
            else
            {
                chkisActive.Checked = false;
            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_AttritionTypeMaster.aspx. btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void DataPager1_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
}