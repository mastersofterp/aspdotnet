using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Masters_str_stage : System.Web.UI.Page
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
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.BindTables();
                this.BindLstVewSTAGE();
                ViewState["action"] = "add";
                //Set Report Parameters
                //objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Department_User_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    private void BindTables()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllTable();
            ddlTable.DataSource = ds.Tables[0];
            ddlTable.DataTextField = "NAME";
            ddlTable.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindLstVewSTAGE()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllSTAGE();
            ds = objCommon.FillDropDown("STORE_STAGE", "SNAME,TABLE_INV", "STNO", "", "");
            if (ds.Tables[0] != null)
            {
                lvStage.DataSource = ds.Tables[0];
                lvStage.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string tableInv;
            if (ddlTable.SelectedValue != "0")
                tableInv = ddlTable.SelectedItem.ToString();
            else
                tableInv = " ";

            int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("store_stage", " count(*)", "sname='" + txtStname.Text + "' OR TABLE_INV='" + tableInv + "'"));

            if (duplicateCkeck == 0)
            {
                if (ViewState["action"] != null)
                {

                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        CustomStatus cs = (CustomStatus)objStrMaster.AddStage(txtStname.Text, Session["colcode"].ToString(), tableInv);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {


                            objCommon.DisplayMessage(UpdatePanel1, "Record Save Succesfully", this);
                            this.BindLstVewSTAGE();
                            this.Clear();
                        }

                    }
                    else
                    {

                        CustomStatus cs = (CustomStatus)objStrMaster.UpdateStage(Convert.ToInt32(ViewState["STNO"].ToString()), txtStname.Text, Session["colcode"].ToString(), tableInv);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {

                            objCommon.DisplayMessage(UpdatePanel1, "Record Updated Succesfully", this);
                            this.BindLstVewSTAGE();
                            this.Clear(); ;
                        }
                    }


                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Already Exists", this);
                return;
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

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lvStage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["STNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsSTAGE(Convert.ToInt32(ViewState["STNO"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindLstVewSTAGE();
    }
    public void DisplayMessage(Control UpdatePanelId, string Message, Page pg)
    {
        ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", " alert('" + Message + "');", true);

    }
    protected void Clear()
    {

        ddlTable.SelectedValue = "0";
        txtStname.Text = string.Empty;
        ViewState["action"] = "add";
    }

    private void ShowEditDetailsSTAGE(int stno)
    {
        DataSet ds = null;

        try
        {
            // ds = objStrMaster.GetSingleRecordStage(stno );
            ds = objCommon.FillDropDown("STORE_STAGE", "SNAME,TABLE_INV", "STNO", "STNO=" + stno, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTable.SelectedIndex = ddlTable.Items.IndexOf(ddlTable.Items.FindByText(ds.Tables[0].Rows[0]["TABLE_INV"].ToString()));
                txtStname.Text = ds.Tables[0].Rows[0]["SNAME"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.ShowEditDetailsDeptRegister-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }


}
