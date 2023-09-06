//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : ESATE
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 11-SEP-2016
// DESCRIPTION   : ENERGY BILL SUBMISSION FORM
//=========================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Transaction_ElectricityBillPayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BillPayment objBPay = new BillPayment();
    BillPaymentController objBPCon = new BillPaymentController();

    string bDate = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }                  

                    ViewState["PID"] = null;
                    objCommon.FillDropDownList(ddlBlock, "EST_BLOCK_MASTER", "BLOCKID", "BLOCKNAME", "BLOCKID>0", "BLOCKID");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    } 

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string bDate = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
            ds = objCommon.FillDropDown("EST_BILL_PROCESSING", "LAST_DATE", "DUE_DATE", "YEAR='" + Convert.ToDateTime(txtDate.Text).Year + "' and MONTH='" + Convert.ToDateTime(txtDate.Text).Month + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {               
                    bDate = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
                    objCommon.FillDropDownList(ddlEmployee, "EST_BILL_PROCESSING", "PID", " CASE WHEN EMP_CODE = '0' THEN '' + EMP_NAME  ELSE ISNULL(EMP_CODE,'')  + '  ' + EMP_NAME  end as NAME", "YEAR='" + Convert.ToDateTime(txtDate.Text).Year + "' and MONTH='" + Convert.ToDateTime(txtDate.Text).Month + "'", "EMP_CODE");
                    trEmp.Visible = true;
                    trBlock.Visible = true;
                    fBillPanel.Visible = true;
                    if (Convert.ToDateTime(ds.Tables[0].Rows[0]["LAST_DATE"]) < System.DateTime.Now)
                    {
                       // btnSave.Enabled = false;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Last Date Of Bill Payment Is Over');", true);
                        //return;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.txtDate_TextChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    // This method is used to check the authorization of page.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }   

    // This event is used to Submit the details of Hired vehicle.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmployee.SelectedIndex > 0 && txtDate.Text != string.Empty)
            {

                if (Convert.ToDouble(txtPAmount.Text) != null)
                {
                    if (Convert.ToDateTime(txtReceiptDt.Text) <= Convert.ToDateTime(lblDueDate.Text))
                    {
                        if (Convert.ToDouble(txtPAmount.Text) > Convert.ToDouble(lblW.Text))
                        {
                            txtPAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Paid amount is greater than bill amount');", true);
                            return;
                        }                        
                    }
                    else if (Convert.ToDateTime(txtReceiptDt.Text) > Convert.ToDateTime(lblDueDate.Text))
                    {
                        if (Convert.ToDouble(txtPAmount.Text) > Convert.ToDouble(lblAfter.Text))
                        {
                            txtPAmount.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Paid amount is greater than bill amount');", true);
                            return;
                        }
                    }
                }                

               

                objBPay.PID = Convert.ToInt32(ddlEmployee.SelectedValue);
                objBPay.DATE = Convert.ToDateTime(txtDate.Text); 
                objBPay.RECEIPT_NO = txtReceiptNo.Text == string.Empty ? "" : txtReceiptNo.Text;
                
               
                    objBPay.RECEIPT_DATE = Convert.ToDateTime(txtReceiptDt.Text);
               
                objBPay.PAID_AMOUNT = Convert.ToDouble(txtPAmount.Text);
                objBPay.TOTAL_AMOUNT = 0.0; // Convert.ToDouble(lblTAmount.Text);
                objBPay.BALANCE_AMOUNT = 0.0;// Convert.ToDouble(lblTAmount.Text) - Convert.ToDouble(txtPAmount.Text);



                if (ViewState["PID"] == null)
                {
                    CustomStatus cs = (CustomStatus)objBPCon.AddUpdateBillPayment(objBPay);                   
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                        ClearRec();
                    }
                }
                else
                {
                    objBPay.PID = Convert.ToInt32(ViewState["PID"].ToString());
                    CustomStatus cs = (CustomStatus)objBPCon.AddUpdateBillPayment(objBPay);                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                    ClearRec();
                }
                BindlistView(bDate);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Month And Employee.');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //This event is used to cancel the last selection.
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //This event is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int pid = int.Parse(btnEdit.CommandArgument);
            ViewState["PID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(pid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to bind the list of application of user.
    private void BindlistView(string date)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_BILL_PROCESSING", "PID, EMP_NAME, isnull(RECEIPT_NO,'') as RECEIPT_NO , (CASE WHEN RECEIPT_DATE='1900-01-01' THEN '' ELSE RECEIPT_DATE END)  as RECEIPT_DATE", "(CASE TOTAL_AMOUNT WHEN 0 THEN NULL ELSE ROUND(TOTAL_AMOUNT,0) END) as TOTAL_AMOUNT, (CASE PAID_AMOUNT WHEN 0 THEN NULL ELSE ROUND(PAID_AMOUNT,0) END) as PAID_AMOUNT, (CASE BALANCE_AMOUNT WHEN 0 THEN NULL ELSE ROUND(BALANCE_AMOUNT,0) END) as BALANCE_AMOUNT", "PID='" + Convert.ToInt32(ddlEmployee.SelectedValue) + "' and YEAR='" + Convert.ToDateTime(txtDate.Text).Year + "' and MONTH='" + Convert.ToDateTime(txtDate.Text).Month + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBillPay.DataSource = ds;
                lvBillPay.DataBind();
                lvBillPay.Visible = true;
            }
            else
            {
                lvBillPay.DataSource = null;
                lvBillPay.DataBind();
                lvBillPay.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to show the details of the selected record.
    private void ShowDetails(int pid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("EST_BILL_PROCESSING", "*", "", "PID=" + pid, "");          

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployee.SelectedValue = pid.ToString(); // ds.Tables[0].Rows[0]["UNIT_SOLD"].ToString();
                    //lblUnitSold.Text = ds.Tables[0].Rows[0]["UNIT_SOLD"].ToString();
                    //lblPreDemand.Text = ds.Tables[0].Rows[0]["PRESENT_DEMAND"].ToString();
                    //lblMRent.Text = ds.Tables[0].Rows[0]["METER_RENT"].ToString();
                    //lblFCharge.Text = ds.Tables[0].Rows[0]["FIXED_CHARGE"].ToString();
                    //lblArrear.Text = ds.Tables[0].Rows[0]["ARREAR"].ToString();
                    //lblAInterest.Text = ds.Tables[0].Rows[0]["ARREAR_INTEREST"].ToString();
                    ////lblTAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    //lblW.Text = ds.Tables[0].Rows[0]["WITHIN_DUE_DATE_AMT"].ToString();
                    //lblAfter.Text = ds.Tables[0].Rows[0]["AFTER_DUE_DATE_AMT"].ToString();

                    fPanel.Visible = true;
                    txtReceiptNo.Text = ds.Tables[0].Rows[0]["RECEIPT_NO"].ToString();
                    txtReceiptDt.Text = ds.Tables[0].Rows[0]["RECEIPT_DATE"].ToString();
                    txtPAmount.Text = ds.Tables[0].Rows[0]["PAID_AMOUNT"].ToString();

                    ddlEmployee.Enabled = false;
                    txtDate.Enabled = false;
                }
                else
                {
                    fPanel.Visible = false;
                    ddlEmployee.Enabled = true;
                    txtDate.Enabled = true;
                }            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to clear the controles.
    private void Clear()
    {           
           txtReceiptDt.Text = string.Empty;
           txtReceiptNo.Text = string.Empty;
           txtPAmount.Text = string.Empty;
           fPanel.Visible = false;

           lblUnitSold.Text = string.Empty;
           lblPreDemand.Text = string.Empty;
           lblMRent.Text = string.Empty;
           lblFCharge.Text = string.Empty;
          
           lblArrear.Text = string.Empty;
           lblAInterest.Text = string.Empty;
           //lblTAmount.Text = string.Empty;

           ViewState["PID"] = null;
           txtDate.Text = string.Empty;
           ddlEmployee.SelectedIndex = 0;
           lvBillPay.Visible = false;
           lvBillPay.DataSource = null;
           lvBillPay.DataBind();
           trEmp.Visible = false;
           trBlock.Visible = false;
           ddlEmployee.Enabled = true;
           txtDate.Enabled = true;
           btnSave.Enabled = true;
    }

    private void ClearRec()
    {
         txtReceiptDt.Text = string.Empty;
         txtReceiptNo.Text = string.Empty;
         txtPAmount.Text = string.Empty;
         txtDate.Enabled = true;
         ddlEmployee.Enabled = true;
         ViewState["PID"] = null;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("Hire Vehicle Details", "rptHireDetails.rpt");
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        //try
        //{
        //    string Script = string.Empty;

        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "exporttype=pdf";
        //    url += "&filename=HireVehicleDetailReport.pdf";
        //    url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
        //    url += "&param=@P_HIRE_TYPE=" + ddlHireType.SelectedValue;

        //    // To open new window from Updatepanel
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //    sb.Append(@"window.open('" + url + "','','" + features + "');");
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.ShowHireDetails() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //} 
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != string.Empty)
            {
               
                DataSet ds = null;
                string bDate = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
                ds = objBPCon.GetBillDetails(Convert.ToInt32(ddlEmployee.SelectedValue), bDate);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblUnitSold.Text = ds.Tables[0].Rows[0]["UNIT_SOLD"].ToString();
                    lblPreDemand.Text = ds.Tables[0].Rows[0]["PRESENT_DEMAND"].ToString();
                    lblMRent.Text = ds.Tables[0].Rows[0]["METER_RENT"].ToString();
                    lblFCharge.Text = ds.Tables[0].Rows[0]["FIXED_CHARGE"].ToString();

                    lblArrear.Text = ds.Tables[0].Rows[0]["ARREAR"].ToString();
                    lblAInterest.Text = ds.Tables[0].Rows[0]["ARREAR_INTEREST"].ToString();
                    lblDueDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DUE_DATE"]).ToString("dd-MMM-yyyy");
                    lblW.Text = ds.Tables[0].Rows[0]["WITHIN_DUE_DATE_AMT"].ToString();
                    lblAfter.Text = ds.Tables[0].Rows[0]["AFTER_DUE_DATE_AMT"].ToString();
                    lblLastDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LAST_DATE"]).ToString("dd-MMM-yyyy");

                   // lblTAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    fPanel.Visible = true;

                    if (ds.Tables[0].Rows[0]["RECEIPT_NO"].ToString() == string.Empty)
                    {
                        lvBillPay.Visible = false;                       
                    }
                    else
                    {
                        BindlistView(bDate);
                        lvBillPay.Visible = true;
                    }                   
                }
                else
                {
                    fPanel.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Information is not Found.');", true);
                    Clear();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Month.');", true);
            }
            //BindlistView(bDate);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
       // BindlistView(bDate);
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
         objCommon.FillDropDownList(ddlEmployee, "EST_BILL_PROCESSING", "PID", " CASE WHEN EMP_CODE = '0' THEN '' + EMP_NAME  ELSE ISNULL(EMP_CODE,'')  + '  ' + EMP_NAME  end as NAME", "YEAR='" + Convert.ToDateTime(txtDate.Text).Year + "' and MONTH='" + Convert.ToDateTime(txtDate.Text).Month + "' AND (BLOCKID =" + Convert.ToInt32(ddlBlock.SelectedValue) + " OR " + Convert.ToInt32(ddlBlock.SelectedValue) + " =0)" , "EMP_CODE");
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_ElectricityBillPayment.ddlBlock_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}