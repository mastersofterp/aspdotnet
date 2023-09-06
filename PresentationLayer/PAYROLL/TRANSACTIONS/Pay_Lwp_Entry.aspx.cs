//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Lwp_Entry.ASPX                                                    
// CREATION DATE : 23-NOV-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
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

public partial class PAYROLL_TRANSACTIONS_Pay_Lwp_Entry : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objpay = new PayController();
    
    public string monYear;

    public string mondate;
    
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
                this.FillCollege();              
                ViewState["action"] = "add";
            }
        }

        if (!(txtMonthYear.Text == null || txtMonthYear.Text == string.Empty))
        {
             monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy").ToUpper();
             mondate = "1" + "/" + txtMonthYear.Text;
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Lwp_Entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Lwp_Entry.aspx");
        }
    }

    private void BindListViewLwpEntry(int idno,string lwpMonYear)
    {
        try
        {
            DataSet ds = objpay.GetAllLwpEntry(idno,lwpMonYear);
            lvLwpDetails.DataSource = ds;
            lvLwpDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Lwp_Entry.BindListViewLwpEntry()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int lwpNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(lwpNo);
            ViewState["action"] = "edit";           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Lwp_Entry.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void ShowDetails(Int32 lwpNo)
    {

        DataSet ds = null;
        try
        {
            ds = objpay.GetSingleLwpEntry(lwpNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["lwpNo"] = lwpNo.ToString();
                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["LWPDATE"].ToString();
                txtToDate.Text= ds.Tables[0].Rows[0]["LWPDATE"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Lwp_Entry.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {  
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    int instCount=0;
                    DateTime lwpdate = Convert.ToDateTime(txtFromDate.Text);

                    while (lwpdate <= Convert.ToDateTime(txtToDate.Text))
                    {
                       CustomStatus cs = (CustomStatus)objpay.AddLwpEntry(Convert.ToInt32(ddlEmployee.SelectedValue), monYear,lwpdate, Session["colcode"].ToString());
                       if (cs.Equals(CustomStatus.RecordSaved))
                       {
                          instCount = 1;
                          ViewState["action"] = "add";
                          lwpdate = lwpdate.AddDays(1);    
                        }
                    }

                    if (instCount == 1)
                       objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                    
                }
                else
                {
                    //Edit
                    if (ViewState["lwpNo"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objpay.UpdateLwpEntry(Convert.ToInt32(ViewState["lwpNo"].ToString()), Convert.ToInt32(ddlEmployee.SelectedValue), monYear,Convert.ToDateTime(txtFromDate.Text) , Session["colcode"].ToString());  
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
                        }
                    }
                }
            }

            this.BindListViewLwpEntry(Convert.ToInt32(ddlEmployee.SelectedValue), monYear);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Lwp_Entry.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int lwpNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objpay.DeleteLwpEntry(lwpNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                objCommon.DisplayMessage(UpdatePanel1, "Record Deleted Successfully", this);
                this.BindListViewLwpEntry(Convert.ToInt32(ddlEmployee.SelectedValue), monYear);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_Lwp_Entry.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(txtMonthYear.Text == null || txtMonthYear.Text == string.Empty))
            this.BindListViewLwpEntry(Convert.ToInt32(ddlEmployee.SelectedValue), monYear);
        else
            objCommon.DisplayMessage(UpdatePanel1, "Please Select Month for Lwp entry", this);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.ToString());
    }

    private void FillCollege()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_Master", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", " EM.COLLEGE_NO="+ Convert.ToInt32(ddlCollege.SelectedValue)+" AND EM.COLLEGE_NO=PM.COLLEGE_NO AND EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");

        }
        catch (Exception ex)
        { 

        }
    }
}
