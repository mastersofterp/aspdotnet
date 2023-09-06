//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_DaySal.ASPX                                                    
// CREATION DATE : 05-Nov-2009                                                        
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

public partial class PayRoll_Transactions_Pay_DaySal : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objpay = new PayController();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillStaff();
                this.FillPayHead();
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                this.BindListViewDaySal();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_DaySal.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_DaySal.aspx");
        }
    }

    private void BindListViewDaySal()
    {
        try
        {
            DataSet ds = objpay.GetAllDaysal();

            if (ds.Tables[0].Rows.Count <= 0)
            {   
                dpPager.Visible = false;
            }
            else
            {   
                dpPager.Visible = true;
            }
            lvDalSal.DataSource = ds;
            lvDalSal.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.BindListViewDaySal()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";
    }

    private void Clear()
    {
        //ddlPayhead.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        txtMonthYear.Text = string.Empty;
        txtdays.Text = string.Empty;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int dsNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(dsNo);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void ShowDetails(Int32 dsNo)
    {

        DataSet ds = null;
        try
        {
            ds = objpay.GetSingleDaySal(dsNo);
             
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsNo"] = dsNo.ToString();
                ddlPayhead.SelectedValue = ds.Tables[0].Rows[0]["PAYHEAD"].ToString();
                ddlStaff.SelectedValue   = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                txtdays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["DAYS"]).ToString(); // ADDED BY: M. REHBAR SHEIKH ON 23/08/2019
                ceMonthYear.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["MONDATE"].ToString());                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
      

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Check whether to add or update
            int cnt;
            if (ViewState["action"] != null)
            {
               string monYear=Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy").ToUpper();
               //string mondate="1"+"/"+txtMonthYear.Text;
               string mondate = txtMonthYear.Text;

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help

                    cnt = Convert.ToInt32(objCommon.LookUp("PAYROLL_DAYSAL", "COUNT(1)", "MONYEAR='" + monYear + "' AND COLLEGE_NO=" + ddlCollege.SelectedValue + " AND STAFFNO=" + ddlStaff.SelectedValue + " AND PAYHEAD='"+ddlPayhead.SelectedValue+"'"));

                    if (cnt > 0)
                    {
                        objCommon.DisplayMessage("Record already exists!", this.Page);
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objpay.AddDaysal(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(mondate), monYear, Convert.ToDecimal(txtdays.Text), ddlPayhead.SelectedValue, Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue));

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            Clear();
                        }
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["dsNo"] != null)
                    {
                        cnt = Convert.ToInt32(objCommon.LookUp("PAYROLL_DAYSAL", "COUNT(1)", "MONYEAR='" + monYear + "' AND COLLEGE_NO=" + ddlCollege.SelectedValue + " AND STAFFNO=" + ddlStaff.SelectedValue + " AND PAYHEAD='" + ddlPayhead.SelectedValue + "' AND DSNO<>" + ViewState["dsNo"]));

                         if (cnt > 0)
                         {
                             objCommon.DisplayMessage("Record already exists!", this.Page);
                             return;
                         }
                         else
                         {
                             CustomStatus cs = (CustomStatus)objpay.UpdateDaysal(Convert.ToInt32(ViewState["dsNo"].ToString()), Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(mondate), monYear, Convert.ToDecimal(txtdays.Text), ddlPayhead.SelectedValue, Session["colcode"].ToString(), Convert.ToInt32(ddlCollege.SelectedValue));
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {
                                 objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                                 pnlAdd.Visible = false;
                                 pnlList.Visible = true;
                                 Clear();
                             }
                         }
                    }
                }
            }

            this.BindListViewDaySal();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int dsNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objpay.DeleteDaySal(dsNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
      //  Response.Redirect("~/PayRoll/Transactions/Pay_DaySal.aspx");
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        this.BindListViewDaySal();
    }

    private void FillStaff()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff,"PAYROLL_STAFF","STAFFNO","STAFF","", "STAFFNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.FillStaff-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillPayHead()
    {
        try
        {
            string deField = objCommon.LookUp("payroll_pay_ref", "DE_FIELD", "");
            //objCommon.FillDropDownList(ddlPayhead,"PAYROLL_PAYHEAD","PAYHEAD","PAYFULL","SRNO > 15 AND PAYSHORT IS NOT NULL AND PAYSHORT <> ''","SRNO");
            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYFULL", "PAYHEAD='" + deField + "'", "SRNO");
            ddlPayhead.SelectedValue = deField.ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_DaySal.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
}
