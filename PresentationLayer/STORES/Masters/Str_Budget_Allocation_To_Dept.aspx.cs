//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Budget_Head_Master.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN     
// MODIFIED BY   : VINOD ANDHALE                                                 
// MODIFIED DATE : 05.05.2014
// MODIFIED DESC : MODIIED START DATE AND END DATE 
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Stores_Masters_Str_Budget_Head_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
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
                BindListViewBudHead();
                FillBudgetHead();
                txtStartDate.Text = Convert.ToDateTime(Session["sdate"]).ToShortDateString();
                txtEndDate.Text = Convert.ToDateTime(Session["edate"]).ToShortDateString();

                txtStartDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                ViewState["action"] = "add";
            }
            //Set Report Parameters
            objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Budget_head_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    private void BindListViewBudHead()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllBudgetHead();
            lvBudHead.DataSource = ds;
            lvBudHead.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Head_Master.BindListViewBudHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillBudgetHead()
    {
        try
        {
            objCommon.FillDropDownList(ddlBudgetHead, "STORE_BUDGET_HEAD", "BHNO", "BHNAME", "", "BHNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Head_Master.BindListViewBudHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StoreMaster objStrMst = new StoreMaster();

            objStrMst.BAMT = Convert.ToDecimal(txtAmount.Text);
            objStrMst.BUDFSDATE = Convert.ToDateTime(txtStartDate.Text);
            objStrMst.BHNO = Convert.ToInt32(ddlBudgetHead.SelectedValue);
            objStrMst.BUDFEDATE = Convert.ToDateTime(txtEndDate.Text);
            objStrMst.BNATURE = txtNature.Text;
            objStrMst.SCHEME = txtScheme.Text;
            objStrMst.COLLEGE_CODE = Session["colcode"].ToString();
            objStrMst.BCOORDINATOR = txtCoordinator.Text;

            if (Convert.ToDateTime(txtStartDate.Text) < Convert.ToDateTime(txtEndDate.Text))
            {
                objCommon.DisplayMessage(UpdatePanel1, "End Date should noy be less than start date", this);
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_BUDGETHEAD_Alloction", " count(*)", "BHNO= " + Convert.ToInt32(ddlBudgetHead.SelectedValue)));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddBudgetHead(objStrMst, Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            this.BindListViewBudHead();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Save Successfully", this);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist", this);
                    }
                }
                else
                {
                    if (ViewState["bhalNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_BUDGETHEAD_Alloction", " count(*)", "BHNO= " + Convert.ToInt32(ddlBudgetHead.SelectedValue) + "and bhalno <> " + Convert.ToInt32(ViewState["bhalNo"].ToString())));
                        if (duplicateCkeck == 0)
                        {

                            objStrMst.BHALNO = Convert.ToInt32(ViewState["bhalNo"].ToString());
                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateBudgetHead(objStrMst, Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                objCommon.DisplayMessage(UpdatePanel1, "Record Update Successfully", this);
                                this.BindListViewBudHead();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist", this);
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Head_Master.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["bhalNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsBudHead(Convert.ToInt32(ViewState["bhalNo"].ToString()));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Head_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowEditDetailsBudHead(int bhNo)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordBudgetHead(bhNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAmount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["BAMT"]).ToString();
                txtCoordinator.Text = ds.Tables[0].Rows[0]["BCOORDINATOR"].ToString();
                txtNature.Text = ds.Tables[0].Rows[0]["BNATURE"].ToString();
                cetxtStartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BUDFSDATE"].ToString());
                ceEndDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BUDFEDATE"].ToString());
                txtScheme.Text = ds.Tables[0].Rows[0]["SCHEME"].ToString();
                ddlBudgetHead.SelectedValue = ds.Tables[0].Rows[0]["BHNO"].ToString();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Head_Master.ShowEditDetailsBudHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void butCancel_Click(object sender, EventArgs e)
    {

        //Clear();
        Response.Redirect(Request.Url.ToString());
    }

    protected void Clear()
    {
        txtAmount.Text = string.Empty;
        txtCoordinator.Text = string.Empty;
        txtNature.Text = string.Empty;
        ddlBudgetHead.SelectedIndex = 0;
        txtScheme.Text = string.Empty;
        //  txtBudgetName.Text = string.Empty;
        // chkActive.Checked = false;
        ViewState["action"] = "add";
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewBudHead();
    }
}
