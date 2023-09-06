//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Budget_Allocation_To_Dept.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
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

public partial class Stores_Masters_Str_Budget_Allocation_To_Dept : System.Web.UI.Page
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
                this.BindListViewBudgetAllocation();
                ViewState["action"] = "add";
                this.FillBudget();
                this.FillDept();
            }
            DateValidation();
            txtStartDate.Text = Convert.ToDateTime(Session["sdate"]).ToShortDateString();
            txtEndDate.Text = Convert.ToDateTime(Session["edate"]).ToShortDateString();
            //Set Report Parameters
            objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Budget_subdept_Allocation_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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

    private void BindListViewBudgetAllocation()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllSubDeptBudget();
            lvBudAllocation.DataSource = ds;
            lvBudAllocation.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Allocation_To_Dept.BindListViewBudgetAllocation-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StoreMaster objStrMst = new StoreMaster();
            objStrMst.SDNO = Convert.ToInt32(ddlDept.SelectedValue);
            objStrMst.BHALNO = Convert.ToInt32(ddlBudget.SelectedValue);
            objStrMst.SD_BUDAMT = Convert.ToDecimal(txtAmount.Text);
            objStrMst.SD_BUDSDATE = Convert.ToDateTime(txtStartDate.Text);
            objStrMst.SD_BUDEDATE = Convert.ToDateTime(txtEndDate.Text);
            objStrMst.COLLEGE_CODE = Session["colcode"].ToString();
            objStrMst.SD_BUDBALAMT= 0;


            if (ViewState["action"] != null)
            {
                
                    //if (objStrMaster.CHECKBUDGETDATE(objStrMst.BHALNO, objStrMst.SD_BUDSDATE, objStrMst.SD_BUDEDATE))//Chech Alloction date Fall between Budget FYear
                    //{
                 if (validateBudgetAMT(Convert.ToDouble(txtAmount.Text), objStrMst.BHALNO))//Check For Sufficient Budget Amount
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_SUBDEPT_BUDGET", " count(*)", "MDNO= " + Convert.ToInt32(ddlDept.SelectedValue) + " and BHALNO =  " + Convert.ToInt32(ddlBudget.SelectedValue)));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus cs = (CustomStatus)objStrMaster.AddSubDeptBudget(objStrMst, Session["userfullname"].ToString());
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {

                                objCommon.DisplayMessage(UpdatePanel1, "Record Saved Succesfully", this);
                                this.BindListViewBudgetAllocation();
                                this.Clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record already exist", this);
                        }
                    }
                    else
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_SUBDEPT_BUDGET", " count(*)", "MDNO= " + Convert.ToInt32(ddlDept.SelectedValue) + " and BHALNO =  " + Convert.ToInt32(ddlBudget.SelectedValue) + "and sd_budno <> " + Convert.ToInt32(ViewState["sdBugNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {


                            if (ViewState["sdBugNo"] != null)
                            {
                                objStrMst.SD_BUDNO = Convert.ToInt32(ViewState["sdBugNo"].ToString());
                                CustomStatus csupd = (CustomStatus)objStrMaster.UpdateSubDeptBudget(objStrMst, Session["userfullname"].ToString());
                                if (csupd.Equals(CustomStatus.RecordUpdated))
                                {
                                    ViewState["action"] = "add";

                                    objCommon.DisplayMessage(UpdatePanel1, "Record Updated Succesfully", this);
                                    this.BindListViewBudgetAllocation();
                                    this.Clear();
                                }
                            }

                        }

                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record already exist", this);
                        }

                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Budget Amount Not Suffiecient",this);
                }
             }
             else
             {
                    objCommon.DisplayMessage(UpdatePanel1, "Date should be fall between dates allocated to Budgets !",this);
             }

        }

       
                
            
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Allocation_To_Dept.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["sdBugNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsBudgetAllocation(Convert.ToInt32(ViewState["sdBugNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Allocation_To_Dept.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsBudgetAllocation(int sdBugNo)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordSubDeptBudget(sdBugNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
                ddlBudget.SelectedValue = ds.Tables[0].Rows[0]["BHALNO"].ToString();
                txtAmount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["SD_BUDAMT"]).ToString();
                cetxtStartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["SD_BUDSDATE"].ToString());
                ceEndDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["SD_BUDEDATE"].ToString());
               
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Allocation_To_Dept.ShowEditDetailsBudHead-> " + ex.Message + " " + ex.StackTrace);
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
        ddlDept.SelectedValue = "0";
        ddlBudget.SelectedIndex = 0;
        txtAmount.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        cetxtStartDate.SelectedDate = null;
        ceEndDate.SelectedDate = null;
        ViewState["action"] = "add";
       txtStartDate.Text= Convert.ToDateTime(Session["sdate"]).ToShortDateString();
       txtEndDate.Text = Convert.ToDateTime(Session["edate"]).ToShortDateString();
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewBudgetAllocation();
    }

    protected void FillDept()
    {
        try
        {
            objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Allocation_To_Dept.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillBudget()
    {
        try 
        {
            objCommon.FillDropDownList(ddlBudget, "STORE_BUDGET_HEAD ", "BHNO", "BHNAME", "", "BHNAME");
            //ddlBudget.DataSource = objStrMaster.GetBudgetNameBYFYEAR(Convert.ToDateTime(Session["sdate"]), Convert.ToDateTime(Session["edate"]));
            //ddlBudget.DataTextField = "BHNAME";
            //ddlBudget.DataValueField = "BHALNO";
            //ddlBudget.DataBind();
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Budget_Allocation_To_Dept.FillBudget-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    bool validateBudgetAMT(double CurAMT,int bhalno)//curAmt -current amount to be allocated
    {
        double TAMT; //// Budget Amount For Given Budget Head
        string TotalAmt = objCommon.LookUp("STORE_BUDGETHEAD_Allocate", "BAMT", " BHALNO=" + bhalno);
        TAMT = Convert.ToDouble(TotalAmt);

        if (ViewState["action"].ToString().Equals("add"))
        {
            double CAMT;//Consume Budget Amount For Given Budget Head
            string ConsumeAmt = objCommon.LookUp("STORE_SUBDEPT_BUDGET ", "SUM(SD_BUDAMT) AS CONSUME", " sd_budsdate>=convert(datetime,'" + Session["sdate"].ToString() + "',103) and sd_budedate<=convert(datetime,'" + Session["edate"].ToString() + "',103) and BHALNO=" + bhalno);
            if (ConsumeAmt == "")
                CAMT = 0;
            else
                CAMT = Convert.ToDouble(ConsumeAmt);

            if (TAMT - (CAMT + CurAMT) >= 0)//Ckeck Sufficie Budget Amount For Allocation
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int sd_budno = Convert.ToInt32(ViewState["sdBugNo"].ToString());
            double CAMT;//Consume Budget Amount For Given Budget Head
            string ConsumeAmt = objCommon.LookUp("STORE_SUBDEPT_BUDGET ", "SUM(SD_BUDAMT) AS CONSUME", " sd_budsdate>=convert(datetime,'" + Session["sdate"].ToString() + "',103) and sd_budedate<=convert(datetime,'" + Session["edate"].ToString() + "',103) and BHALNO=" + bhalno + "and SD_BUDNO!="+sd_budno );
            if (ConsumeAmt == "")
                CAMT = 0;
            else
                CAMT = Convert.ToDouble(ConsumeAmt);

            if (TAMT - (CAMT + CurAMT) >= 0)//Ckeck Sufficie Budget Amount For Allocation
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    void DateValidation()
    {
        rngSdate.MinimumValue =Convert.ToDateTime(Session["sdate"]).ToShortDateString(); ;
        rngSdate.MaximumValue = Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
        rngSdate.ValidationGroup = "store";
        rngSdate.ErrorMessage = "Start Date Should be betw. " + Convert.ToDateTime(Session["sdate"]).ToShortDateString() + ":" + Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
        rngEdate.MinimumValue = Convert.ToDateTime(Session["sdate"]).ToShortDateString(); ;
        rngEdate.MaximumValue = Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
        rngEdate.ValidationGroup = "store";
        rngEdate.ErrorMessage = "End Date Should be betw. " + Convert.ToDateTime(Session["sdate"]).ToShortDateString() + ":" + Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
    }
    void departmentvalidation()
    {
        rngSdate.MinimumValue = Convert.ToDateTime(Session["sdate"]).ToShortDateString(); ;
        rngSdate.MaximumValue = Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
        rngSdate.ValidationGroup = "store";
        rngSdate.ErrorMessage = "Start Date Should be betw. " + Convert.ToDateTime(Session["sdate"]).ToShortDateString() + ":" + Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
        rngEdate.MinimumValue = Convert.ToDateTime(Session["sdate"]).ToShortDateString(); ;
        rngEdate.MaximumValue = Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
        rngEdate.ValidationGroup = "store";
        rngEdate.ErrorMessage = "End Date Should be betw. " + Convert.ToDateTime(Session["sdate"]).ToShortDateString() + ":" + Convert.ToDateTime(Session["edate"]).ToShortDateString(); ;
    }
}
