//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : BankAccMapping.ASPX                                                    
// CREATION DATE : 19-Feb-2018                                                        
// CREATED BY    : Sachin Ghagre                                                    
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

public partial class PAYROLL_MASTERS_Pay_BankAccMapping : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    Payroll objcalpay = new Payroll();
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
        //Check Session
        if (Session["userno"] == null || Session["username"] == null ||
        Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {

                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["BankAccMappingId"] = null;
                FillDropDowns();
                //BindListViewBank();
            }

        }

    }


    private void FillDropDowns()
    {
        try
        {
            // College Name
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlScheme, "PAYROLL_STAFF", "STAFFNO", "STAFF", "", "STAFFNO");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "", "EMPTYPENO");
            objCommon.FillDropDownList(ddlBank, "PAYROLL_BANK", "BANKNO", "BANKNAME", "", "BANKNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.FillRule-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    private void BindListViewBank()
    {
        try
        {
            if (ViewState["BankAccMappingId"] == null)
            {
                objcalpay.BankAccMappingId = 0;

            }
            else
            {
                objcalpay.BankAccMappingId = Convert.ToInt32(ViewState["BankAccMappingId"].ToString());
            }
            DataSet ds = objpay.GetAllMappingBank(objcalpay);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lv_Bank.DataSource = ds;
                lv_Bank.DataBind();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.BindListViewPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        txtBankAccNo.Text = string.Empty;
        ViewState["BankAccMappingId"] = null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            objcalpay.COLLEGE_ID = Convert.ToInt32(ddlCollege.SelectedValue);
            objcalpay.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
            objcalpay.STAFFNO = Convert.ToInt32(ddlStaff.SelectedValue);
            objcalpay.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
            objcalpay.BANK_ACCNO = txtBankAccNo.Text;
            objcalpay.COLLEGE_CODE = Session["colcode"].ToString();

            DataSet ds = objpay.GetCheckMappingBankExist(objcalpay);
            if (ds.Tables[0].Rows.Count > 0 && ViewState["BankAccMappingId"] == null)
            {
                Showmessage("This account number is already mapped with this scheme,staff and bank");
                return;
            }
            //Check whether to add or update
            if (ViewState["BankAccMappingId"] == null)
            {
                objcalpay.BankAccMappingId = 0;
                CustomStatus cs = (CustomStatus)objpay.AddUpdateBankAccMapping(objcalpay);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["BankAccMappingId"] = null;
                    //Showmessage("Record saved successfully");
                    MessageBox("Record Saved successfully");
                   // objCommon.DisplayMessage(this.updPanel, "Record saved successfully", this.Page);
                    BindBankAccMapping();
                  //  Clear();
                }
            }
            else
            {
                //Edit
                if (ViewState["BankAccMappingId"] != null)
                {
                    objcalpay.BankAccMappingId = Convert.ToInt32(ViewState["BankAccMappingId"].ToString());
                    CustomStatus cs = (CustomStatus)objpay.AddUpdateBankAccMapping(objcalpay);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //updPanel
                        //Showmessage("Record updated successfully");
                        MessageBox("Record Updated successfully");
                      //  objCommon.DisplayMessage(this.updPanel, "Record updated successfully", this.Page);
                        BindBankAccMapping();
                        //Clear();

                    }
                }
            }
            ViewState["BankAccMappingId"] = null;
           // BindBankAccMapping();
          Clear();
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int BankAccMappingId = int.Parse(btnEdit.CommandArgument);
            ViewState["BankAccMappingId"] = BankAccMappingId;
            objcalpay.BankAccMappingId = Convert.ToInt32(ViewState["BankAccMappingId"].ToString());
            DataSet ds = objpay.GetAllMappingBank(objcalpay);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
                ddlStaff.SelectedValue = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
                ddlBank.SelectedValue = ds.Tables[0].Rows[0]["BANKNO"].ToString();
                txtBankAccNo.Text = ds.Tables[0].Rows[0]["BANK_ACCNO"].ToString();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBankAccMapping();
    }
    public void BindBankAccMapping()
    {
        try
        {
            if (ddlCollege.SelectedIndex == null || ddlCollege.SelectedIndex == 0)
            {
                Showmessage("Please Select College Name ");
                lv_Bank.DataSource = null;
                lv_Bank.DataBind();
            }
            else
            {
                objcalpay.COLLEGE_ID = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            DataSet ds = objpay.GetAll_BYID_MappingBank(objcalpay);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lv_Bank.DataSource = ds;
                lv_Bank.DataBind();
            }
            else
            {
                lv_Bank.DataSource = null;
                lv_Bank.DataBind();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.BindListViewPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}