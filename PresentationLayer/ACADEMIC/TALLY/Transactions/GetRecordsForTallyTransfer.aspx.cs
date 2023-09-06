//=================================================================================
// PROJECT NAME  :                                                      
// MODULE NAME   :                                   
// CREATION DATE :                                                  
// CREATED BY    :                                              
// MODIFIED BY   :                                                                 
// MODIFIED DESC :                                                                 
//===================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls.Adapters;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
public partial class Tally_Transactions_GetRecordsForTallyTransfer : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();


    Ent_Acd_GetRecordsForTallyTransfer ObjTTM = new Ent_Acd_GetRecordsForTallyTransfer();
    Con_Acd_GetRecordsForTallyTransfer objTTC = new Con_Acd_GetRecordsForTallyTransfer();
   
    string Message = string.Empty;
    string UsrStatus = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    FillDropDown();

                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title

                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                }
            }
            divMsg.InnerHtml = "";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_TallyVoucherType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                //Response.Redirect("~/notauthorized.aspx?page=GetRecordsForTallyTransfer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            //Response.Redirect("~/notauthorized.aspx?page=GetRecordsForTallyTransfer.aspx");
        }
    }
    protected void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCashbook, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");



        }
        catch (Exception ex)
        {
             if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_getRecordsForTallyTransfer.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            var fromdate = DateTime.Parse(txtDateFrom.Text);
            var todate = DateTime.Parse(txtDateTo.Text);
            //var fromdate = DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //var todate = DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
           

            if (fromdate <= todate)
            {
                ObjTTM.CollegeId = Convert.ToInt32(Session["colcode"].ToString());
                ObjTTM.CashBookId = Convert.ToInt32(ddlCashbook.SelectedValue);
                ObjTTM.FromDate = (DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                ObjTTM.ToDate = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string FROMDATE = txtDateFrom.Text.ToString();
                string ToDate = txtDateTo.Text.ToString();
                DataSet ds = objTTC.GetAllDetails(ObjTTM, FROMDATE, ToDate);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count <= 0)
                        {
                            objCommon.DisplayMessage(UpdatePanel1,"Records Not Found.", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1,"Records Not Found.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1,"Records Not Found.", this.Page);
                    return;
                }

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Rep_Receipt.DataSource = ds;
                            Rep_Receipt.DataBind();
                            DivReceipt.Visible = true;




                            lblCashAmount.Text = "--";
                            lblChequeAmount.Text = "--";
                            lblDDAmount.Text = "--";
                            lblTotalAmount.Text = "--";

                            Double cashAmount =
                                ds.Tables[0]
                                    .AsEnumerable()
                                //.Where(r => (String)r["CHDD"] == "C")
                                    .Sum(r => (Double)Convert.ToDouble(r["Cash"]));

                            //Double chequeAmount =
                            //    ds.Tables[0]
                            //        .AsEnumerable()
                            //    // .Where(r => (String)r["CHDD"] == "Q")
                            //        .Sum(r => (Double)Convert.ToDouble(r["Cheque"]));

                            Double ddAmount =
                                ds.Tables[0]
                                    .AsEnumerable()
                                //.Where(r => (String)r["CHDD"] == "D")
                                    .Sum(r => (Double)Convert.ToDouble(r["DemandDraft"]));

                            Double totalAmount = cashAmount + ddAmount;
                            //    ds.Tables[0]
                            //        .AsEnumerable()
                            //        .Sum(r => (Double)Convert.ToDouble(r["TOTAL"]));


                            lblCashAmount.Text = cashAmount.ToString("0.00");
                            //  lblChequeAmount.Text = chequeAmount.ToString("0.00");
                            lblDDAmount.Text = ddAmount.ToString("0.00");
                            lblTotalAmount.Text = totalAmount.ToString("0.00");


                        }
                        else
                        {
                            Rep_Receipt.DataSource = null;
                            Rep_Receipt.DataBind();
                            DivReceipt.Visible = false;

                            lblCashAmount.Text = "--";
                            lblChequeAmount.Text = "--";
                            lblDDAmount.Text = "--";
                            lblTotalAmount.Text = "--";
                        }
                    }
                    else
                    {
                        Rep_Receipt.DataSource = null;
                        Rep_Receipt.DataBind();
                        DivReceipt.Visible = false;

                        lblCashAmount.Text = "--";
                        lblChequeAmount.Text = "--";
                        lblDDAmount.Text = "--";
                        lblTotalAmount.Text = "--";
                    }
                }
                else
                {
                    Rep_Receipt.DataSource = null;
                    Rep_Receipt.DataBind();
                    DivReceipt.Visible = false;

                    lblCashAmount.Text = "--";
                    lblChequeAmount.Text = "--";
                    lblDDAmount.Text = "--";
                    lblTotalAmount.Text = "--";
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "To Date Should Greater Than Equal to Form Date", this.Page);
                return;
            }

       

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Tally_GetrecordsForTallyTransfer.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ObjTTM.CashBookId = Convert.ToInt32(ddlCashbook.SelectedValue);
            ObjTTM.CollegeId = Convert.ToInt32(Session["colcode"]);
            //ObjTTM.FromDate = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //ObjTTM.ToDate = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string fromdate = txtDateFrom.Text.ToString();
            string todate = txtDateTo.Text.ToString();
            ObjTTM.CreatedBy = Convert.ToInt32(Session["userno"]);
            ObjTTM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTTM.MACAddress = Convert.ToString("1");

            long res = objTTC.AddUpdateDCRTallyRecords(ObjTTM, ref Message,fromdate,todate);

            if (res == -99)
            {
                objCommon.DisplayMessage(UpdatePanel1,"Exception Occure", this.Page);
                return;

            }
            else if (res == 0)
            {
                objCommon.DisplayMessage(UpdatePanel1,"Record Already Exists", this.Page);
                return;

            }
            else if (res <= 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Save", this.Page);
                return;
            }
           
            else if (res == 2)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Already Save", this.Page);
                return;
            }

            else if (res > 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Save Successfully", this.Page);
                return;
            }



        }
        catch (Exception ex)    
        {

            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCashbook.SelectedIndex = 0;
        txtDateFrom.Text = string.Empty;
        txtDateTo.Text = string.Empty;
        DivReceipt.Visible = false;
        lblCashAmount.Text = string.Empty;
        lblDDAmount.Text = string.Empty;
        lblTotalAmount.Text = string.Empty;
    }
}