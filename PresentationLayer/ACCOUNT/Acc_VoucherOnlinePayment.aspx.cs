//=================================================================================
// PROJECT NAME  : RF CAMPUS                                                           
// MODULE NAME   : FINANCE
// CREATION DATE : 01-10-2021                                               
// CREATED BY    : GOPAL ANTHATI                                               
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Data;
using System.Data.Linq;

using System.Web;
using System.Configuration;

public partial class ACCOUNT_Acc_VoucherOnlinePayment : System.Web.UI.Page
{

    Common objCommon = new Common();
    AccountTransactionController objPC1 = new AccountTransactionController();
    public string Docpath = ConfigurationManager.AppSettings["AccountDirPath"];
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");

            }
            else
            {

                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {

            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //This procedure is called RPDATA to add in treeview for receipt entry or for payment entry
        //called proc GETRECPAYMENTDATA,ALLOTSEQNOTORPGROUP to allot sequence no. to group

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        //Session["WithoutCashBank"] = "N";

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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    objCommon.DisplayUserMessage(UPDLedger, "Select company/cash book.", this);

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    SetFinancialYear();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                }
            }
            CheckPageAuthorization();


            //SetFinancialYear();
        }
    }

    private void FillVouchers()
    {
        DataSet ds = null;
        int Party_no = 0;
        if (txtBankLedger.Text != string.Empty)
            Party_no = Convert.ToInt32(txtBankLedger.Text.ToString().Trim().Split('*')[0].ToString());
        else
            Party_no = 0;
        if (txtFrmDate.Text != "" && txtToDate.Text != "")
        {
            ds = objPC1.GetVouchers(Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd"), Party_no, Session["comp_code"].ToString());
        }
        else
        {
            ds = objPC1.GetVouchers(Convert.ToDateTime(Session["fin_date_from"]).ToString("yyyy-MM-dd"), Convert.ToDateTime(Session["fin_date_to"]).ToString("yyyy-MM-dd"), Party_no, Session["comp_code"].ToString());
        }
        if (ds!=null && ds.Tables[0].Rows.Count > 0)
        {
            ddlVoucherNo.DataSource = ds.Tables[0];
            ddlVoucherNo.DataTextField = "VOUCHER_NO";
            ddlVoucherNo.DataValueField = "VOUCHER_SQN";
            ddlVoucherNo.DataBind();
            divVoucher.Visible = true;
        }
        else
        {
            MessageBox("No Records Found.");
            divVoucher.Visible = false;
            return;
        }
    }

    private void SetFinancialYear()
    {
        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtToDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();


    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }
    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAcc(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAgainstAccLedger(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    public void BindVoucherListView()
    {
        int Party_no = 0;
        if (txtBankLedger.Text != string.Empty)
        {
            Party_no = Convert.ToInt32(txtBankLedger.Text.ToString().Trim().Split('*')[1].ToString());
            //string ty_no = txtBankLedger.Text.ToString().Trim().Split('*').ToString();
        }
        else
        {
            Party_no = 0;
        }

        DataSet ds = new DataSet();
        if (ddlVoucherType.SelectedValue == "BK")
        {
            ds = objPC1.GetBulkVoucherPaymentDetails(ddlVoucherNo.SelectedValue, Session["comp_code"].ToString());
            divVoucher.Visible = true;
        }
        else
        {
            if (txtFrmDate.Text != "" && txtToDate.Text != "")
            {
                ds = objPC1.GetVoucherPaymentDetails(ddlVoucherType.SelectedValue, Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd"), Session["comp_code"].ToString(), Party_no);
            }
            else
            {
                ds = objPC1.GetVoucherPaymentDetails(ddlVoucherType.SelectedValue, Convert.ToDateTime(Session["fin_date_from"]).ToString("yyyy-MM-dd"), Convert.ToDateTime(Session["fin_date_to"]).ToString("yyyy-MM-dd"), Session["comp_code"].ToString(), Party_no);
            }

            divVoucher.Visible = false;
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvVoucher.DataSource = ds;
            lvVoucher.DataBind();
            btnMakePayment.Visible = true;
           // btnUpdateTranStatus.Visible = true;

            divOther.Visible = true; ;
           
        }
        else
        {
            lvVoucher.DataSource = null;
            lvVoucher.DataBind();           
        }
    }    
   
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
      //  ddlVoucherType.SelectedIndex = 0;
        if (txtToDate.Text != "" && txtFrmDate.Text != "")
        {
            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
                txtToDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
                txtToDate.Focus();
                return;
            }
            if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
                txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
                txtFrmDate.Focus();
                return;
            }
            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
                txtToDate.Focus();
                return;
            }
        }
       
        if (ddlVoucherType.SelectedValue == "BK")
        {           
            FillVouchers();          
        }
        else
        {            
            BindVoucherListView();
        }

    }
  

    public void Clear()
    {       
        lvVoucher.DataSource = null;
        lvVoucher.DataBind();
        btnMakePayment.Visible = false;
        btnUpdateTranStatus.Visible = false;
        pnlVch.Visible = true; 
        ddlVoucherNo.SelectedIndex = 0;
        ddlVoucherNo.SelectedValue = null;

        txtFrmDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtBankLedger.Text = string.Empty;
        ddlVoucherType.SelectedIndex = 0;
        SetFinancialYear();
    }

   

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }  
   
    public void ExportDataTabletoFile(DataTable datatable, string delimited, bool exportcolumnsheader, string file)
    {

        StreamWriter str = new StreamWriter(file, false, System.Text.Encoding.Default);

        //if (exportcolumnsheader)
        //{
        //string Columns = string.Empty;

        //foreach (DataColumn column in datatable.Columns)
        //{
        //    Columns += column.ColumnName + delimited;
        //}
        //str.WriteLine(Columns.Remove(Columns.Length - 1, 1));

        //}

        foreach (DataRow datarow in datatable.Rows)
        {
            string row = string.Empty;

            foreach (object items in datarow.ItemArray)
            {
                if (items.ToString() != "")
                    row += items.ToString() + delimited;
            }
            if (row != "")
                str.WriteLine(row.Remove(row.Length - 1, 1));
        }
        str.Flush();
        str.Close();
    }


    protected void btnMakePayment_Click(object sender, EventArgs e)
    {
        try
        {
            string voucher_No = string.Empty;
            string voucher_Sqn = string.Empty;
            string EmpDataId = string.Empty;
            int count = 0;
            foreach (ListViewItem lv in lvVoucher.Items)
            {
                HiddenField hdnVchNo = lv.FindControl("hdnVchNo") as HiddenField;
                HiddenField hdnEmpDataId = lv.FindControl("hdnEmpDataId") as HiddenField;
                CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
                if (chkSelect.Checked)
                {
                    voucher_No += hdnVchNo.Value + ",";
                    voucher_Sqn += chkSelect.ToolTip + ",";
                    EmpDataId += hdnEmpDataId.Value + ",";
                    count++;
                }

            }
            if (count > 0)
            {
                voucher_No = voucher_No.Substring(0, voucher_No.Length - 1);
                voucher_Sqn = voucher_Sqn.Substring(0, voucher_Sqn.Length - 1);
                EmpDataId = EmpDataId.Substring(0, EmpDataId.Length - 1);
            }
            //Random rnd = new Random();
            //int ir = rnd.Next(100000000, 999999999);

            //string CustomerRefNumber = "7R" + ir.ToString() + "SA";


            DataSet ds = objPC1.GetPaymentTrnDetails(voucher_No, voucher_Sqn, ddlVoucherType.SelectedValue, EmpDataId, Session["comp_code"].ToString());
            string monyear = DateTime.Now.ToString();

            string filename = "MAKAUT_KAU14RBI_KAU14RBI" + Convert.ToDateTime(monyear).ToString("ddMM");
            //filename = filename + ".txt";
            filename = filename + ds.Tables[1].Rows[0]["SRNO"].ToString();

            // Exporting Data to text file


            if (ds.HasErrors == false)
            {
                //string file = Server.MapPath("~/OutTran/");

                //    if (!System.IO.Directory.Exists(file))
                //    {
                //        System.IO.Directory.CreateDirectory(file);
                //    }
                //if (!ds.Tables[0].Rows.Contains("Transaction Details"))
                //{                    
                //    ds.Tables[0].Rows.RemoveAt(1);
                //    ds.AcceptChanges();
                //}

                String FilePath = Docpath; //+ "PaymentFiles\\OutTranFiles\\";
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
                //ExportDataTabletoFile(ds.Tables[0], "    ", true, Server.MapPath("~/ACCOUNT/OutTranFiles/" + filename));
              
                //FilePath = FilePath + "\\" + filename;
                FilePath = Docpath + "\\" + filename;
                ExportDataTabletoFile(ds.Tables[0], "    ", true, FilePath);
                
                lnkDownload.NavigateUrl = GetFileNamePath(filename);
                //lnkDownload.Visible = true;
                BindVoucherListView();
                MessageBox("Transaction Done Successfully");
                //Clear();
            }
            else
            {
                MessageBox("No data found for current selection");
                return;
            }

            //#region download notepad or text file.

            //Response.ContentType = "application/octet-stream";

            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);

            //string aaa = Server.MapPath("~/images/" + filename);

            //Response.TransmitFile(Server.MapPath("~/images/" + filename));

            //HttpContext.Current.ApplicationInstance.CompleteRequest();

            ////Response.End();
            ////MessageBox("Transaction Done Successfully.");

            //#endregion

            //DownloadFile(filename);
        }
        catch (Exception ex)
        {
            MessageBox("No data found for current selection");
            return;
        }
    }

    public string GetFileNamePath(string fileName)
    {
        String Path = Docpath + fileName;
        //string Path = Server.MapPath("~/ACCOUNT/OutTranFiles/" + fileName);

        string[] extension = fileName.ToString().Split('.');
        if (fileName != null && fileName.ToString() != string.Empty)
            return (Path.ToString().Trim());
        else
            return "";
    }

    public void DownloadFile(string fileName)
    {
        try
        {


            string Path = string.Empty;
            if (fileName != "")
            {
                Path = Server.MapPath("~/images/");

                FileStream sourceFile = new FileStream((Path + "\\" + fileName), FileMode.Open);

                long fileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)fileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                sourceFile.Dispose();

                Response.ClearContent();
                Response.Clear();
                Response.BinaryWrite(getContent);
                Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                MessageBox("File Not Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            //throw;
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    private static DataTable DataTableFromTextFile(string location, char delimiter = ',')
    {
        DataTable result;

        string[] LineArray = File.ReadAllLines(location);

        result = FormDataTable(LineArray, delimiter);

        return result;
    }

    private static DataTable FormDataTable(string[] LineArray, char delimiter)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("TRANSACTION_TYPE", typeof(string));
        dt.Columns.Add("BENEFICIARY_CODE", typeof(string));
        dt.Columns.Add("BENEFICIARY_ACC_NO", typeof(string));
        dt.Columns.Add("INSTRUMENT_AMOUNT", typeof(string));
        dt.Columns.Add("BENEFICIARY_NAME", typeof(string));
        dt.Columns.Add("DRAWEE_LOCATION", typeof(string));
        dt.Columns.Add("PRINT_LOCATION", typeof(string));
        dt.Columns.Add("BENE_ADDRESS1", typeof(string));
        dt.Columns.Add("BENE_ADDRESS2", typeof(string));
        dt.Columns.Add("BENE_ADDRESS3", typeof(string));
        dt.Columns.Add("BENE_ADDRESS4", typeof(string));
        dt.Columns.Add("BENE_ADDRESS5", typeof(string));
        dt.Columns.Add("INSTRUCTION_REF_NUMBER", typeof(string));
        dt.Columns.Add("CUSTOMER_REF_NUMBER", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS1", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS2", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS3", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS4", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS5", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS6", typeof(string));
        dt.Columns.Add("PAYMENTDETAILS7", typeof(string));
        dt.Columns.Add("CHQ_NO", typeof(string));
        dt.Columns.Add("TRANSACTION_DATE", typeof(string));
        dt.Columns.Add("MICR_NUMBER", typeof(string));
        dt.Columns.Add("IFSC_CODE", typeof(string));
        dt.Columns.Add("BENE_BANKNAME", typeof(string));
        dt.Columns.Add("BENE_BRANCH", typeof(string));
        dt.Columns.Add("BENEFICIARY_EMAIL_ID", typeof(string));

        AddRowToTable(LineArray, delimiter, ref dt);

        return dt;


    }

    private static void AddRowToTable(string[] valueCollection, char delimiter, ref DataTable dt)
    {

        for (int i = 0; i < valueCollection.Length; i++)
        {
            string[] values = valueCollection[i].Split(delimiter);
            DataRow dr = dt.NewRow();
            for (int j = 0; j < values.Length; j++)
            {
                dr[j] = values[j];
            }
            dt.Rows.Add(dr);
        }
    }

    private static void AddColumnToTable(string[] columnCollection, char delimiter, ref DataTable dt)
    {
        string[] columns = columnCollection[0].Split(delimiter);
        foreach (string columnName in columns)
        {
            DataColumn dc = new DataColumn(columnName, typeof(string));
            dt.Columns.Add(dc);
        }
    }
    protected void btnUpdateTranStatus_Click(object sender, EventArgs e)
    {

        string monyear = DateTime.Now.ToString();

        string filename = ("MAKAUT" + Convert.ToDateTime(monyear).ToString("yyyy-MM-dd"));
        filename = filename + ".txt";
        //grd123.DataSource = DataTableFromTextFile("D:/MAKAUT2021-12-10.txt");
        DataTable dttt = DataTableFromTextFile(Server.MapPath("~/Images/" + filename));

        for (int i = 0; i < dttt.Rows.Count; i++)
        {
            if (i > 0)
            {
                string BenficiaryCode = dttt.Rows[i]["BENEFICIARY_CODE"].ToString();
                string BenficiaryAccNo = dttt.Rows[i]["BENEFICIARY_ACC_NO"].ToString();

                CustomStatus cs = (CustomStatus)objPC1.UpdateTranStatus(BenficiaryCode, BenficiaryAccNo);

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    MessageBox("Transaction Status Updated Successfully.");
                    return;
                }

            }

        }

    }
    protected void ddlVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {      
        BindVoucherListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvVoucher.DataSource = null;
        lvVoucher.DataBind();
        divOther.Visible=false;

        btnMakePayment.Visible=false;
        btnUpdateTranStatus.Visible=false;
        pnlVch.Visible=true;
        ddlVoucherNo.SelectedIndex = 0;
        divVoucher.Visible=false;
    }
}