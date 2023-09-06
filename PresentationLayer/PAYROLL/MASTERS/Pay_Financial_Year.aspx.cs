//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Finaicial_Year.ASPX                                                    
// CREATION DATE : 10-OCT-2018                                                      
// CREATED BY    : Rohit Maske                                                         
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

public partial class PAYROLL_MASTERS_Pay_Financial_Year : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,Pay_FinancialYearController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //Controller object create
    Pay_FinancialYearController objFinYearCon = new Pay_FinancialYearController();
    //entity object create
    Pay_FinancialYear objFinancialYear = new Pay_FinancialYear();
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
            ViewState["action"] = "add";
            BindListViewFinancialYear();
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                ViewState["action"] = "add";
                BindListViewFinancialYear();

               
            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_SupplimentaryBill.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Financial_Year.aspx");
        }
    }

    private void GetControllValue()
    {

        objFinancialYear.FROMDATE = Convert.ToDateTime(txtFromdate.Text);
        objFinancialYear.TODATE = Convert.ToDateTime(txtToYear.Text);
        objFinancialYear.COLLEGE_CODE = Session["colcode"].ToString();
        objFinancialYear.FINANCIAL_YEAR = txtFinanYear.Text;
        objFinancialYear.SHORT_NAME = txtshortName.Text;
        
    }



  protected void butSubmit_Click(object sender, EventArgs e)
    {
        int A = Convert.ToInt32(objCommon.LookUp("PAYROLL_FINYEAR", "ISNULL(COUNT(*),0)", "FROMDATE=" + txtFromdate.Text.Trim() + "AND TODATE=" + txtToYear.Text.Trim() + "AND COLLEGE_CODE=" + Session["colcode"].ToString() + "AND FINANCIAL_YEAR='" + Convert.ToString(txtFinanYear.Text.Trim()) + "'AND SHORT_NAME=" + txtshortName.Text));// FROMDATE,TODATE,COLLEGE_CODE,FINANCIAL_YEAR,SHORT_NAME,FINANCIAL_YEAR
        if (A == 0)
        {
            MessageBox(" Record Already Exist ");
            Clear();
            return;
        }
        else
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    GetControllValue();
                    objFinancialYear.FINYEARID=Convert.ToInt32(ViewState["FY_ID"]);
                    objFinYearCon.UpdateFinancialYear(objFinancialYear);
                    MessageBox(" Record Updated ");
                    Clear();
                    BindListViewFinancialYear();

                }

                else if (ViewState["action"].ToString().Equals("add"))
                {
                    GetControllValue();                  
                    objFinYearCon.AddFinancialYear(objFinancialYear);
                    MessageBox(" Record Saved ");
                    Clear();
                    BindListViewFinancialYear();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_FINANCIAL_YEAR.butSubmit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //SHOW MESSAGE BOX
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    protected void btnEditFinYearID_Click(object sender, ImageClickEventArgs e)
    {

    }

    //function use for Bind PAYROLL SANCTIONING POST  
    protected void BindListViewFinancialYear()
    {
        try
        {
            objFinancialYear.COLLEGE_CODE = objCommon.LookUp("reff", "college_code", string.Empty);
            DataSet ds = objFinYearCon.GetAllFinancialYearDetails(objFinancialYear.COLLEGE_CODE);
            if (ds.Tables[0].Rows.Count >= 0)
            {
                lvFinancialYear.DataSource = ds.Tables[0];
                lvFinancialYear.DataBind();
                lvFinancialYear.Visible = true;
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Financial_Year.BindListViewFinancialYear ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //Clear Properties
    private void Clear()
    {
        txtFromdate.Text = "";
        txtToYear.Text = "";
        txtshortName.Text = "";
        txtFinanYear.Text = "";
    }

    //Get Details From  Database
    public void Show_Details(int FINYEARID)
    {
        try
        {
            DataSet dsFinancialYear = objFinYearCon.GetFanancialYearID(FINYEARID);
            if (dsFinancialYear != null)
            {
                txtFinanYear.Text = dsFinancialYear.Tables[0].Rows[0]["FINANCIAL_YEAR"].ToString();
                txtFromdate.Text = dsFinancialYear.Tables[0].Rows[0]["FROMDATE"].ToString();
                txtToYear.Text = dsFinancialYear.Tables[0].Rows[0]["TODATE"].ToString();
                txtshortName.Text = dsFinancialYear.Tables[0].Rows[0]["SHORT_NAME"].ToString();
                objFinancialYear.FINYEARID = FINYEARID;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Financial_Year.Show_Details-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Edit Details
    public void btnRPT_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnEdit = sender as Button;
            int FY_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ViewState["FY_ID"] = FY_ID;

            Show_Details(FY_ID);
          
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Financial_Year.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Clear Properties
    protected void Button2_Click(object sender, EventArgs e)
    {
        Clear();
    }
}