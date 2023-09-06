//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_SupplimentaryBill.ASPX                                                    
// CREATION DATE : 02-Nov-2009                                                        
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

public partial class PAYROLL_TRANSACTIONS_Pay_UpdatePayHeadEmpWise : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    Pay_UpdatePayHeadEmpWiseController objCon = new Pay_UpdatePayHeadEmpWiseController();
    Pay_SupplimentaryBill_Controller objSupBillCon = new Pay_SupplimentaryBill_Controller();

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


                PopulateDropDownList();
                this.BindListViewEarningHeads();
                this.BindListViewDeductionHeads();
                div_otherinfo.Visible = false;
                tr1.Visible = false;
                tbl_Grid.Visible = false;
                tbl_buttons.Visible = false;
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_UpdatePayHeadEmpWise.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_UpdatePayHeadEmpWise.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmpName, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+ ' ' + '['+ CONVERT(NVARCHAR(20),EM.PFILENO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL ", "EM.IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownListEmployee()
    {
        try
        {
            //FILL EMPLOYEE      AND EM.STAFFNO =" + ddlStaffNo.SelectedValue      
            objCommon.FillDropDownList(ddlEmpName, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+ ' ' + '['+ CONVERT(NVARCHAR(20),EM.PFILENO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.STAFFNO =" + ddlStaffNo.SelectedValue + "", "EM.IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListViewEarningHeads()
    {
        try
        {
            DataSet ds = objCon.GetAllEarningHeads();
            lvEarningHeads.DataSource = ds;
            lvEarningHeads.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.BindListViewEarningHeads()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewDeductionHeads()
    {
        try
        {
            DataSet ds = objCon.GetAllDeductionHeads();
            lvDeductionHeads.DataSource = ds;
            lvDeductionHeads.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.BindListViewDeductionHeads()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowDetails(int IDNO)
    {
        try
        {

            DataSet dsEarHeads = objCon.GetAllEarningHeads();
            DataSet dsDudHeads = objCon.GetAllDeductionHeads();

            DataSet dsPayHeads = objCon.GetPayhead(IDNO);

            lblBasic.Text = dsPayHeads.Tables[0].Rows[0]["BASIC"].ToString();
            lblDept.Text = dsPayHeads.Tables[0].Rows[0]["SUBDEPT"].ToString();
            lblDesignation.Text = dsPayHeads.Tables[0].Rows[0]["SUBSDESIG"].ToString();
            lblGradePay.Text = dsPayHeads.Tables[0].Rows[0]["GRADEPAY"].ToString();
            lblScale.Text = dsPayHeads.Tables[0].Rows[0]["SCALE"].ToString();

            if (dsPayHeads != null)
            {
                if (dsEarHeads.Tables[0].Rows.Count > 0)
                {

                    if (dsEarHeads.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsEarHeads.Tables[0].Rows.Count - 1; i++)
                        {
                            TextBox txtEar = lvEarningHeads.Items[i].FindControl("txEarningsAmt") as TextBox;
                            txtEar.Text = dsPayHeads.Tables[0].Rows[0]["" + dsEarHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                        }
                    }


                    if (dsDudHeads.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsDudHeads.Tables[0].Rows.Count - 1; i++)
                        {
                            TextBox txtDud = lvDeductionHeads.Items[i].FindControl("txDeductionAmt") as TextBox;
                            txtDud.Text = dsPayHeads.Tables[0].Rows[0]["" + dsDudHeads.Tables[0].Rows[i]["PAYHEAD"] + ""].ToString();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void GetDepartmentDesignation(int idNo)
    //{
    //    if (idNo != 0)
    //    {
    //        int appointNo, desigNo;
    //        appointNo = GetCodesFromPaymas("APPOINTNO", idNo);
    //        desigNo = GetCodesFromEmpmas("subdesigno", idNo);
    //        lblAppointMent.Text = objCommon.LookUp("PAYROLL_APPOINT", "APPOINT", "APPOINTNO=" + appointNo);
    //        lblDesignation.Text = objCommon.LookUp("payroll_subdesig", "SUBDESIG", "SUBDESIGNO=" + desigNo);
    //    }
    //    else
    //    {
    //        lblAppointMent.Text = string.Empty;
    //        lblDesignation.Text = string.Empty;
    //    }
    //}



    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStaffNo.SelectedIndex > 0)
        {
            PopulateDropDownListEmployee();
        }
        else
        {
            PopulateDropDownList();
            div_otherinfo.Visible = false;
            tr1.Visible = false;
            tbl_Grid.Visible = false;
            tbl_buttons.Visible = false;
            lvEarningHeads.DataSource = null;
            lvEarningHeads.DataBind();
            lvDeductionHeads.DataSource = null;
            lvDeductionHeads.DataBind();
        }


    }
    protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpName.SelectedIndex > 0)
        {
            ShowDetails(Convert.ToInt32(ddlEmpName.SelectedValue));
            div_otherinfo.Visible = true;
            tbl_Grid.Visible = true;
            tbl_buttons.Visible = true;
            tr1.Visible = true;
        }
        else
        {
            div_otherinfo.Visible = false;
            tbl_Grid.Visible = false;
            tbl_buttons.Visible = false;
            tr1.Visible = false;
            lvEarningHeads.DataSource = null;
            lvEarningHeads.DataBind();
            lvDeductionHeads.DataSource = null;
            lvDeductionHeads.DataBind();
        }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Amount = string.Empty;
            foreach (ListViewDataItem lvitem in lvEarningHeads.Items)
            {
                TextBox txEarningsAmt = lvitem.FindControl("txEarningsAmt") as TextBox;
                if (txEarningsAmt.Text != string.Empty)
                    Amount = txEarningsAmt.Text;
                else
                    Amount = "0.00";
                CustomStatus cs = (CustomStatus)objCon.UpdatePayHeads(Convert.ToInt32(ddlEmpName.SelectedValue), txEarningsAmt.ToolTip, txEarningsAmt.Text);
            }

            foreach (ListViewDataItem lvitem in lvDeductionHeads.Items)
            {
                TextBox txdedAmt = lvitem.FindControl("txDeductionAmt") as TextBox;
                CustomStatus cs = (CustomStatus)objCon.UpdatePayHeads(Convert.ToInt32(ddlEmpName.SelectedValue), txdedAmt.ToolTip, txdedAmt.Text);

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    lblerror.Text = null;
                    objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully", this);
                }
            }

            ShowDetails(Convert.ToInt32(ddlEmpName.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void clear()
    {
        ddlStaffNo.SelectedIndex = 0;
        ddlEmpName.SelectedIndex = 0;
        div_otherinfo.Visible = false;
        tr1.Visible = false;
        tbl_Grid.Visible = false;
        tbl_buttons.Visible = false;
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}