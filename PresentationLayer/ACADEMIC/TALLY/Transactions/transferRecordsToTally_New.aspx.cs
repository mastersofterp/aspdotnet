using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using MSXML2;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;



public partial class Tally_Transactions_transferRecordsToTally_New : System.Web.UI.Page
{
    Ent_Acd_TransferRecordsToTally objTRM = new Ent_Acd_TransferRecordsToTally();
    Con_Acd_TransferRecordsToTally objTRC = new Con_Acd_TransferRecordsToTally();

    Ent_Acd_GetRecordsForTallyTransfer ObjTTM = new Ent_Acd_GetRecordsForTallyTransfer();
    Con_Acd_GetRecordsForTallyTransfer objTTC = new Con_Acd_GetRecordsForTallyTransfer();

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


    Con_Acd_CompanyConfig ObjCC = new Con_Acd_CompanyConfig();
    Ent_Acd_CompanyConfig ObjCCM = new Ent_Acd_CompanyConfig();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Message = string.Empty;
    string UsrStatus = string.Empty;



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
        try
        {

            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                      Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    // this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlCashbook, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
                   
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
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

            if (fromdate <= todate)
            {

                //objTRM.CollegeId = Convert.ToInt32(Session["userno"]);
                //objTRM.CashBookId = Convert.ToInt32(ddlCashbook.SelectedValue);
                ////objTRM.FromDate = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ////objTRM.ToDate = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //string FROMDATE = txtDateFrom.Text.ToString();
                //string TODATE = txtDateTo.Text.ToString();

                //DataSet ds = objTRC.GetRecords(objTRM, FROMDATE, TODATE);

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
                            DivReceipt.Visible = false;
                            objCommon.DisplayMessage(upDetails, "Record Not Found", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        DivReceipt.Visible = false;
                        objCommon.DisplayMessage(upDetails, "Record Not Found", this.Page);
                        return;
                    }
                }
                else
                {
                    DivReceipt.Visible = false;
                    objCommon.DisplayMessage(upDetails, "Record Not Found", this.Page);
                    return;
                }


                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Rep_Receipt.DataSource = ds.Tables[0];
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
                           // lblChequeAmount.Text = chequeAmount.ToString("0.00");
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
                objCommon.DisplayMessage(upDetails, "To Date Should Greater Than Equal to Form Date", this.Page);
                return;
            }
            btnSubmit.Enabled = false;
            btnGetRecTally.Enabled = true;
          
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnGetRecTally_Click(object sender, EventArgs e)
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

            long res = objTTC.AddUpdateDCRTallyRecords(ObjTTM, ref Message, fromdate, todate);

            if (res == -99)
            {
                objCommon.DisplayMessage(upDetails, "Exception Occure", this.Page);
                btnSubmit.Enabled = false;
                btnGetRecTally.Enabled = true;
                return;

            }
            else if (res == 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Already Exists", this.Page);
                btnSubmit.Enabled = false;
                btnGetRecTally.Enabled = true;
                return;

            }
            else if (res <= 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Not Save", this.Page);
                btnSubmit.Enabled = false;
                btnGetRecTally.Enabled = true;
                return;
            }

            else if (res == 2)
            {
                objCommon.DisplayMessage(upDetails, "Record Already Save", this.Page);
                btnSubmit.Enabled = true;
                btnGetRecTally.Enabled = false;
                return;
            }

            else if (res > 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Get Successfully for Tally Transfer.", this.Page);
                btnSubmit.Enabled = true;
                btnGetRecTally.Enabled = false;
                return;
            }

          

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
     {
        try
        {

            string dcrid = string.Empty;

            objTRM.CollegeId = Convert.ToInt32(Session["colcode"]);
            objTRM.CashBookId = Convert.ToInt32(ddlCashbook.SelectedValue);
            //objTRM.FromDate = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //objTRM.ToDate = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string FROMDATE = txtDateFrom.Text.ToString();
            string TODATE = txtDateTo.Text.ToString();
            DataSet ds = objTRC.GetRecords(objTRM, FROMDATE, TODATE);


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        objCommon.DisplayMessage(upDetails,"Record Not Found", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(upDetails,"Record Not Found", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(upDetails,"Record Not Found", this.Page);
                return;
            }



            DataTable DtTran = ds.Tables[0];
            DataTable dtHeads = ds.Tables[1];
            DataTable dtVoucher = ds.Tables[2];
            DataTable dtServerName = ds.Tables[3];

            DataView view = new DataView(DtTran);
            DataTable DistinctDCRId = view.ToTable(false, "DCRId");






            DataTable DtResult = new DataTable();
            DtResult.Columns.Add("DCRTranId", typeof(int));
            DtResult.Columns.Add("TallyResponse", typeof(string));
            DtResult.Columns.Add("IsTransfer", typeof(bool));


            foreach (DataRow dr in DistinctDCRId.Rows)
            {
                foreach (DataRow drTran in DtTran.Select("DCRId=" + dr["DCRId"]))
                {
                    DataTable dt = new DataTable();
                  

                    DataColumn dc;
                    dc = new DataColumn("LedgerName", typeof(string));
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Amount", typeof(double));
                    dt.Columns.Add(dc);

                    dc = new DataColumn("BankName", typeof(string));
                    dt.Columns.Add(dc);

                    Double Amount = 0.00;

                    if (Convert.ToString(drTran["PaymentType"]).Trim() == "C")
                    {
                        Amount = Convert.ToDouble(drTran["Cash"]);
                    }
                    else if (Convert.ToString(drTran["PaymentType"]).Trim() == "Q")
                    {
                        Amount = Convert.ToDouble(drTran["Cheque"]);
                    }
                    else if (Convert.ToString(drTran["PaymentType"]).Trim() == "D")
                    {
                        Amount = Convert.ToDouble(drTran["DDAmount"]);
                    }
                    else
                    {
                        Amount = 0.00;
                    }

                    
                    string vchType = "";

                    if (Convert.ToString(drTran["ReceiptType"]) == "Bank")
                    {
                        vchType = "Bank";

                    }
                    else if (Convert.ToString(drTran["ReceiptType"]) == "Cash")
                    {
                        if (Convert.ToString(drTran["PaymentType"]) == "C")
                        {
                            vchType = "Cash";
                        }
                        else
                        {
                            vchType = "Bank";
                        }
                    }





                    foreach (DataRow drVouch in dtVoucher.Select("TallyVoucherType='" + vchType + "'"))
                    {
                        objTRM.VoucherTypeName = Convert.ToString(drVouch["TallyVoucherName"]);
                    }

                    foreach (DataRow drHead in dtHeads.Select("DCRId=" + dr["DCRId"]))
                    {

                        if (Amount <= 0)
                        {
                            break;
                        }
                        DataRow r = dt.NewRow();
                        if (Convert.ToString(drTran["ReceiptType"]) == "Bank")
                        {
                            r["LedgerName"] = drHead["CashLedgerName"];
                            r["BankName"] = drHead["BankLedgerName"];
                        //    objTRM.ReceiptLedgerName = Convert.ToString(drHead["BankLedgerName"]);

                        }
                        else if (Convert.ToString(drTran["ReceiptType"]) == "Cash")
                        {
                            if (Convert.ToString(drTran["PaymentType"]) == "C")
                            {
                                r["LedgerName"] = drHead["CashLedgerName"];
                                r["BankName"] = "Cash";                //drHead["BankLedgerName"];
                                objTRM.ReceiptLedgerName = "Cash";
                            }
                            else
                            {
                                r["LedgerName"] = drHead["CashLedgerName"];
                                r["BankName"] = drHead["BankLedgerName"];
                            //    objTRM.ReceiptLedgerName = Convert.ToString(drHead["BankLedgerName"]);
                            }
                        }
                        if (Amount > Convert.ToDouble(drHead["Amount"]))
                        {
                            r["Amount"] = drHead["Amount"];
                            Amount = Amount - Convert.ToDouble(drHead["Amount"]);
                            drHead["Amount"] = 0;

                        }
                        else if (Amount <= Convert.ToDouble(drHead["Amount"]))
                        {
                            r["Amount"] = Amount;
                            drHead["Amount"] = Convert.ToDouble(drHead["Amount"]) - Amount;
                            Amount = 0;
                        }
                        else
                        {
                            r["Amount"] = Amount;
                            Amount = 0;
                            drHead["Amount"] = 0.0;
                        }

                        dt.Rows.Add(r);
                    }




                    string Narration = "";



                    // objTRM.VoucherTypeName = drTran["000000"];
                    objTRM.VoucherDate = Convert.ToDateTime(drTran["ReceiptDate"]);
                    objTRM.ReceiptNumber = Convert.ToString(drTran["ReceiptNumber"]);
                    objTRM.NameOnReceipt = Convert.ToString("StudentName");

                    //
                    Narration = Convert.ToString(drTran["StudentName"]) + "Receipt Number :" + Convert.ToString(drTran["ReceiptNumber"]);

                    if (Convert.ToString(drTran["PaymentType"]) != "C")
                    {
                        objTRM.InstrumentNumber = Convert.ToString(drTran["DDNumber"]);
                 //       objTRM.InstrumentDate = Convert.ToDateTime(drTran["DDDate"]);
                        objTRM.BankName = Convert.ToString(drTran["BankName"]);
                        objTRM.BankLocation = Convert.ToString(drTran["Location"]);

                        Narration = Narration + " Instrument Number : " + Convert.ToString(drTran["DDNumber"]);

                        objTRM.PaymentFavoring = Convert.ToString(drTran["StudentName"]);
                    }
                    objTRM.Narration = Narration;
                    objTRM.HeadsTable = dt;



                    ServerXMLHTTP30 serverHTTP = new ServerXMLHTTP30();


                    string xml = objTRC.GenerateXML(objTRM);


                    string Server = "";
                    foreach (DataRow drSer in dtServerName.Rows)
                    {
                        Server = Convert.ToString(drSer["ServerName"]);
                    }

                    string Address = "http://" + Server;

                    serverHTTP.open("POST", Address, false, null, null);

                    serverHTTP.send(xml);

                    string responseStr = serverHTTP.responseText;


                    DataSet dsResult = new DataSet();

                    dsResult.ReadXml(new StringReader(responseStr));

                    bool TransferError = false; 
                    if (dsResult.Tables["IMPORTRESULT"] != null)
                    {
                        foreach (DataRow drRow in dsResult.Tables["IMPORTRESULT"].Rows)
                        {
                            if (Convert.ToInt32(drRow["CREATED"]) != 1)
                            {
                                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Error);
                                TransferError = true;
                            }
                            else
                            {
                                DataRow drResultRow = DtResult.NewRow();

                                //drResultRow["DCRTranId"] = drTran["DCRTransactionId"];
                                drResultRow["DCRTranId"] = drTran["DCRId"];
                                drResultRow["TallyResponse"] = drRow["LASTVCHID"];
                                drResultRow["IsTransfer"] = true;

                                DtResult.Rows.Add(drResultRow);
                            }
                        }
                   //     dcrid += drTran["DCRId"].ToString() + ',';
       
                                                            
                    }

                    else
                    {
                        //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Error);
                        TransferError = true;
                    }

                    if (TransferError == true)
                    {
                   //     objTRM.DcrId = dcrid.ToString(); 
                        objTRM.HeadsTable = DtResult;
                        objTRM.CollegeId = Convert.ToInt32(Session["colcode"]);
                        objTRM.ModifiedBy = Convert.ToInt32(Session["userno"]);
                        objTRM.IPAddress = Convert.ToString(Session["ipAddress"]);
                        objTRM.MACAddress = Convert.ToString("0");
                   

                        long res = objTRC.UpdateTallyResponce(objTRM, ref Message);

                        if (res == -99)
                        {

                            objCommon.DisplayMessage(upDetails,"Exception Occure", this.Page);
                            return;
                        }
                        else if (res <= 0)
                        {

                            objCommon.DisplayMessage(upDetails,"Record Not Save", this.Page);
                            return;
                        }
                        else if (res > 0)
                        {

                            objCommon.DisplayMessage(upDetails, "Data Transfer to Tally Successfully.", this.Page);
                            return;
                        }
                    }



                    dt.Clear();
                    dt.Dispose();



                }
            }




            objTRM.DcrId = dcrid.ToString(); 
            objTRM.HeadsTable = DtResult;
            objTRM.CollegeId = Convert.ToInt32(Session["colcode"]);
            objTRM.ModifiedBy = Convert.ToInt32(Session["userno"]);
            objTRM.IPAddress = Convert.ToString(Session["ipAddress"]);
            objTRM.MACAddress = Convert.ToString("0");
            long result = objTRC.UpdateTallyResponce(objTRM, ref Message);

            if (result == -99)
            {

                objCommon.DisplayMessage(upDetails,"Exception Occure", this.Page);
                return;
            }
            else if (result <= 0)
            {

                objCommon.DisplayMessage(upDetails,"Record Not Save", this.Page);
                return;
            }
            else if (result > 0)
            {
                objCommon.DisplayMessage(upDetails, "Data Transfer to Tally Successfully.", this.Page);
                return;
            }

            btnSubmit.Enabled = false;
            btnGetRecTally.Enabled = true;


            // DataSet ds = objTTC
            //ObjTTM.CashBookId = Convert.ToInt32(ddlCashbook.SelectedValue);
            //ObjTTM.CollegeId = Convert.ToInt32(Session["COLL_ID"]);
            //ObjTTM.FromDate = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //ObjTTM.ToDate = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //ObjTTM.CreatedBy = Convert.ToInt32(Session["USERID"]);
            //ObjTTM.IPAddress = Convert.ToString(Session["IPADDR"]);
            //ObjTTM.MACAddress = Convert.ToString(Session["MACADDR"]);

            //long res = objTTC.AddUpdateDCRTallyRecords(ObjTTM, ref Message);

            //if (res == -99)
            //{
            //    objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);

            //    return;

            //}
            //else if (res == 0)
            //{
            //    objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.DuplicateEntry, CLOUD_COMMON.MessageType.Alert);

            //    return;

            //}
            //else if (res <= 0)
            //{
            //    objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.NotSaved, CLOUD_COMMON.MessageType.Alert);
            //    return;
            //}
            //else if (res > 0)
            //{
            //    objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, CLOUD_COMMON.Message.Saved, CLOUD_COMMON.MessageType.Alert);
            //    return;
            //}

           

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnCancel_Click(object sender, System.EventArgs e)
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