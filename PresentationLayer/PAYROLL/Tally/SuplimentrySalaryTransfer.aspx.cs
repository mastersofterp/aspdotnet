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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using MSXML2;
using System.IO;
using System.Text;

public partial class PAYROLL_Tally_SuplimentrySalaryTransfer : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    Ent_TransferRecordsToTally objTRM = new Ent_TransferRecordsToTally();
    Con_TransferRecordsToTally objTRC = new Con_TransferRecordsToTally();
    Ent_GetRecordsForTallyTransfer ObjTTM = new Ent_GetRecordsForTallyTransfer();
    Con_GetRecordsForTallyTransfer objTTC = new Con_GetRecordsForTallyTransfer();
    Con_PayrollTallyConfig objPTC = new Con_PayrollTallyConfig();

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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
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

    private void Clear()
    {
        GridData.DataSource = null;
        GridData.DataBind();
       ViewState["dtPay"] = null;
        ddlPayMonth.SelectedIndex = 0;
        ddlstafftype.SelectedIndex = 0;
        txtPayDate.Text = string.Empty;
        DivReceipt.Visible = false;
        lblCashAmount.Text = string.Empty;
        lblDDAmount.Text = string.Empty;
        lblTotalAmount.Text = string.Empty;
        btnSubmit.Enabled = true;
    }

    protected void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO <> 0", "STAFFNO");
            objCommon.FillDropDownList(ddlPayMonth, "PAYROLL_SALFILE", "DISTINCT MONYEAR", "MONYEAR AS MONYEAR1", "", "MONYEAR DESC");
            objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "", "COLLEGE_NAME");
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
            ObjTTM.CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
            int StaffTypeId = Convert.ToInt32(ddlstafftype.SelectedValue);
            string PayMonth = ddlPayMonth.SelectedValue;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;

            int Count = Convert.ToInt32(objCommon.LookUp("PayHeadTallyTransfer", "Count(PayHead)", "PayMonthYear = '" + PayMonth + "' AND TallyResponce = 1 AND IsTransfered = 1 and IsSupliSal = 'Y' AND CollegeCode = '" + ddlCollegeName.SelectedValue + "'"));
            if (Count <= 0)
            {
                Con_TallyFeeHeads ObjTFC = new Con_TallyFeeHeads();
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                DataSet dsPay = objPTC.GetTallyPayHeadDataStaffTypeWise(Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlstafftype.SelectedValue));
                DataTable dt = dsPay.Tables[0];
                DivReceipt.Visible = true;
                GridData.DataSource = dt;
                GridData.DataBind();
                DataSet ds = objTTC.GetAllSupplementrySalaryDetails(ObjTTM, StaffTypeId, PayMonth);
                DataTable dtSum = ds.Tables[0];

                if (dsPay != null)
                {
                    if (dsPay.Tables.Count > 0)
                    {
                        if (dsPay.Tables[0].Rows.Count <= 0)
                        {
                            objCommon.DisplayMessage("Records Not Found.", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Records Not Found.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Records Not Found.", this.Page);
                    return;
                }


                if (dsPay.Tables[0].Rows.Count > 0)
                {
                    double cashAmt = 0;
                    //GridData.Rows[i].Cells[0].Text.ToString().Trim()
                    if (GridData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // string temp=  dt.Rows[i]["PAY_HEAD_No"].ToString();

                            for (int j = 0; j < dtSum.Columns.Count; j++)
                            {
                                //checking 
                                string tycOL = dtSum.Columns[j].ToString();
                                string QW = dt.Rows[i]["PAYHEAD"].ToString();

                                if (dt.Rows[i]["PAYHEAD"].ToString() == dtSum.Columns[j].ToString())
                                {
                                    Label lblCash = GridData.Rows[i].FindControl("lblAmt") as Label;
                                    if (lblCash != null)
                                    {
                                        //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtSum.Rows[0][j].ToString().Trim())));         // "123.00"    txtTranAmt.Text.ToString().Trim();
                                        lblCash.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtSum.Rows[0][j].ToString().Trim())));
                                        if (lblCash.Text == "")
                                        {
                                            lblCash.Text = "0.0";

                                        }
                                        cashAmt = cashAmt + Convert.ToDouble(lblCash.Text);
                                        //cashAmt = String.Format("{0:0.00}", Math.Abs(cashAmt));
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    GridData.DataSource = null;
                    GridData.DataBind();
                    DivReceipt.Visible = false;

                    lblCashAmount.Text = "--";
                    lblChequeAmount.Text = "--";
                    lblDDAmount.Text = "--";
                    lblTotalAmount.Text = "--";
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Salary for the month already transfered", this.Page);
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
            DataTable dtPay = new DataTable();

            if (!(dtPay.Columns.Contains("PayHead")))
                dtPay.Columns.Add("PayHead");
            if (!(dtPay.Columns.Contains("CashLedgerName")))
                dtPay.Columns.Add("CashLedgerName");
            if (!(dtPay.Columns.Contains("Amount")))
                dtPay.Columns.Add("Amount");

            foreach (GridViewRow row in GridData.Rows)
            {
                Label lblPayHeadNo = row.FindControl("lblPayHeadNo") as Label;
                Label lblLEDGER = row.FindControl("lblLEDGER") as Label;
                Label lblAmt = row.FindControl("lblAmt") as Label;

                if (ViewState["dtPay"] != null)
                    dtPay = (DataTable)ViewState["dtPay"];

                DataRow drPay = dtPay.NewRow();
                drPay["PayHead"] = lblPayHeadNo.Text == string.Empty ? "PAY" : lblPayHeadNo.Text;
                drPay["CashLedgerName"] = lblLEDGER.Text;
                drPay["Amount"] = lblAmt.Text;

                dtPay.Rows.Add(drPay);

               ViewState["dtPay"] = dtPay;
            }

            string StaffType = ddlstafftype.SelectedValue;
            string PayMonth = ddlPayMonth.SelectedValue;
            ObjTTM.CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
            string Paydate = txtPayDate.Text.ToString();
            //string todate = txtDateTo.Text.ToString();
            ObjTTM.CreatedBy = Convert.ToInt32(Session["userno"]);
            ObjTTM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTTM.MACAddress = Convert.ToString("1");

            long res = objTTC.AddUpdateSupplePayTallyRecords(ObjTTM, ref Message, Paydate, StaffType, PayMonth, (DataTable)ViewState["dtPay"]);

            if (res == -99)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Exception Occure", this.Page);
                return;

            }
            else if (res == 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Already Exists", this.Page);
                //Clear();
                return;
            }
            else if (res <= 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Saved", this.Page);
                //Clear();
                return;
            }
            else if (res == 2)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Already Saved", this.Page);
                //Clear();
                return;
            }

            else if (res > 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully, Please click on transfer Button to Transfer Salary to Tally", this.Page);
                btnSubmit.Enabled = false;
                return;
            }



        }
        catch (Exception ex)
        {

            throw;
        }
    }


    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            string StaffType = ddlstafftype.SelectedValue;
            string PayMonth = ddlPayMonth.SelectedValue;
            string Paydate = txtPayDate.Text.ToString();
            int CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
            //string Padate = txtDateTo.Text.ToString();
            ObjTTM.CreatedBy = Convert.ToInt32(Session["userno"]);
            ObjTTM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTTM.MACAddress = Convert.ToString("1");

            DataSet ds = objTRC.GetSuplementrySalRecordsPayrollTransfer(objTRM, StaffType, PayMonth, CollegeId);
            DataSet dsCompany = objPTC.GetTallyCompanyName(objTRM, CollegeId);
            string CompanyName = dsCompany.Tables[0].Rows[0]["TallyCompanyName"].ToString();

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Record Not Found", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record Not Found", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Found", this.Page);
                return;
            }

            DataTable dtHeads = ds.Tables[0];
            DataTable dtServerName = ds.Tables[1];

            DataView view = new DataView(dtHeads);
            DataTable DistinctDCRId = view.ToTable(false, "PAYHEAD");

            DataTable DtResult = new DataTable();
            DtResult.Columns.Add("PAYHEAD", typeof(string));
            DtResult.Columns.Add("TallyResponse", typeof(string));
            DtResult.Columns.Add("IsTransfer", typeof(bool));

            string ret = string.Empty;
            StringBuilder sb = new StringBuilder();

            ///Code for XML String formation
            ///text for Lendger header
            sb.AppendLine("<ENVELOPE>");
            sb.AppendLine(" <HEADER>");
            sb.AppendLine("  <TALLYREQUEST>Import Data</TALLYREQUEST>");
            sb.AppendLine(" </HEADER>");
            sb.AppendLine(" <BODY>");
            sb.AppendLine("  <IMPORTDATA>");
            sb.AppendLine("   <REQUESTDESC>");
            sb.AppendLine("    <REPORTNAME>Vouchers</REPORTNAME>");
            sb.AppendLine("    <STATICVARIABLES>");
            sb.AppendLine("     <SVCURRENTCOMPANY>" + CompanyName + "</SVCURRENTCOMPANY>");
            sb.AppendLine("    </STATICVARIABLES>");
            sb.AppendLine("   </REQUESTDESC>");
            sb.AppendLine("   <REQUESTDATA>");
            sb.AppendLine("    <TALLYMESSAGE xmlns:UDF=\"TallyUDF\">");
            sb.AppendLine("     <VOUCHER REMOTEID=\"aef7b457-766a-4446-a7ab-e0a803d0a5cc-00000077\" VCHTYPE=\"Journal\" ACTION=\"Create\">");
            sb.AppendLine("      <DATE>" + DateTime.ParseExact(txtPayDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + "</DATE>");
            sb.AppendLine("     <NARRATION>SUPPLEMENTRY SALARY TRANSFER FOR THE MONTH OF " + ddlPayMonth.SelectedValue + "</NARRATION>");
            sb.AppendLine("     <VOUCHERTYPENAME>Journal</VOUCHERTYPENAME>");
            sb.AppendLine("      <CSTFORMISSUETYPE/>");
            sb.AppendLine("      <CSTFORMRECVTYPE/>");
            sb.AppendLine("      <FBTPAYMENTTYPE>Default</FBTPAYMENTTYPE>");
            sb.AppendLine("      <VCHGSTCLASS/>");
            sb.AppendLine("      <DIFFACTUALQTY>No</DIFFACTUALQTY>");
            sb.AppendLine("      <AUDITED>No</AUDITED>");
            sb.AppendLine("      <FORJOBCOSTING>No</FORJOBCOSTING>");
            sb.AppendLine("      <ISOPTIONAL>No</ISOPTIONAL>");
            sb.AppendLine("      <EFFECTIVEDATE>" + DateTime.ParseExact(txtPayDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + "</EFFECTIVEDATE>");
            sb.AppendLine("      <USEFORINTEREST>No</USEFORINTEREST>");
            sb.AppendLine("      <USEFORGAINLOSS>No</USEFORGAINLOSS>");
            sb.AppendLine("      <USEFORGODOWNTRANSFER>No</USEFORGODOWNTRANSFER>");
            sb.AppendLine("      <USEFORCOMPOUND>No</USEFORCOMPOUND>");
            sb.AppendLine("      <EXCISEOPENING>No</EXCISEOPENING>");
            sb.AppendLine("      <USEFORFINALPRODUCTION>No</USEFORFINALPRODUCTION>");
            sb.AppendLine("      <ISCANCELLED>No</ISCANCELLED>");
            sb.AppendLine("      <HASCASHFLOW>No</HASCASHFLOW>");
            sb.AppendLine("      <ISPOSTDATED>No</ISPOSTDATED>");
            sb.AppendLine("      <USETRACKINGNUMBER>No</USETRACKINGNUMBER>");
            sb.AppendLine("      <ISINVOICE>No</ISINVOICE>");
            sb.AppendLine("      <MFGJOURNAL>No</MFGJOURNAL>");
            sb.AppendLine("      <HASDISCOUNTS>No</HASDISCOUNTS>");
            sb.AppendLine("      <ASPAYSLIP>No</ASPAYSLIP>");
            sb.AppendLine("      <ISCOSTCENTRE>No</ISCOSTCENTRE>");
            sb.AppendLine("      <ISDELETED>No</ISDELETED>");
            sb.AppendLine("      <ASORIGINAL>No</ASORIGINAL>");

            foreach (DataRow dr in DistinctDCRId.Rows)
            {
                foreach (DataRow drTran in dtHeads.Select("PAYHEAD='" + dr["PAYHEAD"] + "'"))
                {
                    DataTable dt = new DataTable();
                    DataColumn dc;
                    dc = new DataColumn("LedgerName", typeof(string));
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Amount", typeof(double));
                    dt.Columns.Add(dc);
                    Double Amount = 0.00;
                    Amount = Convert.ToDouble(drTran["Amount"]);
                    string vchType = string.Empty;
                    vchType = "Cash";
                    objTRM.VoucherTypeName = "Journal";

                    foreach (DataRow drHead in dtHeads.Select("PAYHEAD='" + dr["PAYHEAD"] + "'"))
                    {
                        if (Amount <= 0)
                        {
                            break;
                        }
                        DataRow r = dt.NewRow();

                        if (Convert.ToString(drTran["PaymentType"]) == "C")
                        {
                            r["LedgerName"] = drHead["CashLedgerName"];
                            r["Amount"] = drHead["Amount"];
                        }
                        else
                        {
                            r["LedgerName"] = drHead["CashLedgerName"];
                            r["Amount"] = Convert.ToInt32(drHead["Amount"]) * (-1);
                        }
                        dt.Rows.Add(r);
                    }

                    ///Text for VOUCHER Entries
                    foreach (DataRow drHead in dt.Rows)
                    {

                        sb.AppendLine("      <ALLLEDGERENTRIES.LIST>");
                        sb.AppendLine("<LEDGERNAME>" + Convert.ToString(drHead["LedgerName"]) + "</LEDGERNAME>");
                        sb.AppendLine(" <GSTCLASS/>");

                        if (Convert.ToString(drTran["PaymentType"]) == "C")
                            sb.AppendLine(" <ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>");
                        else
                            sb.AppendLine(" <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>");

                        sb.AppendLine(" <LEDGERFROMITEM>No</LEDGERFROMITEM>");
                        sb.AppendLine(" <REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>");
                        sb.AppendLine(" <ISPARTYLEDGER>No</ISPARTYLEDGER>");
                        sb.AppendLine(" <AMOUNT>" + drHead["Amount"] + "</AMOUNT>");
                        sb.AppendLine("</ALLLEDGERENTRIES.LIST>");
                    }
                }
            }

            ///Code for Lendger footer tag
            ///
            sb.AppendLine("     </VOUCHER>");
            sb.AppendLine("    </TALLYMESSAGE>");
            sb.AppendLine("   </REQUESTDATA>");
            sb.AppendLine("  </IMPORTDATA>");
            sb.AppendLine(" </BODY>");
            sb.AppendLine("</ENVELOPE>");

            ServerXMLHTTP30 serverHTTP = new ServerXMLHTTP30();

            string xml = sb.ToString();

            string Server = "";
            foreach (DataRow drSer in dtServerName.Rows)
            {
                Server = Convert.ToString(drSer["ServerName"]);
            }

            string Address = "http://" + Server;
            serverHTTP.open("POST", Address, false, null, null);
            StringBuilder testXML = new StringBuilder();
            serverHTTP.send(xml);
            string responseStr = serverHTTP.responseText;

            DataSet dsResult = new DataSet();

            dsResult.ReadXml(new StringReader(responseStr));

            bool TransferError = false;
            if (Convert.ToInt32(dsResult.Tables[0].Rows[0]["CREATED"]) == 1)
                TransferError = true;

            if (TransferError == true)
            {
                objTRM.CollegeId = Convert.ToInt32(Session["colcode"]);
                objTRM.ModifiedBy = Convert.ToInt32(Session["userno"]);
                objTRM.IPAddress = Convert.ToString(Session["ipAddress"]);
                objTRM.MACAddress = Convert.ToString("0");
                long res = objTRC.UpdatePayrollTallyResponce(objTRM, PayMonth, ref Message);

                if (res == -99)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Exception Occure", this.Page);
                    return;
                }
                else if (res <= 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record Not Save", this.Page);
                    return;
                }
                else if (res > 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Data Transfer to Tally Successfully.", this.Page);
                    Clear();
                    return;
                }
            }
            else if (Convert.ToInt32(dsResult.Tables[0].Rows[0]["ALTERED"]) == 1)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Data Transfer to Tally Successfully. Voucher Altered.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Data Transfer to Tally Failed.", this.Page);
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
        ddlPayMonth.SelectedIndex = 0;
        ddlstafftype.SelectedIndex = 0;
        txtPayDate.Text = string.Empty;
        DivReceipt.Visible = false;
        lblCashAmount.Text = string.Empty;
        lblDDAmount.Text = string.Empty;
        lblTotalAmount.Text = string.Empty;
    }
}