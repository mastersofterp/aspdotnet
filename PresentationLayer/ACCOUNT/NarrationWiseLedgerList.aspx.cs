//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNTING NARRATION WISE LEDGER REPORT SHOW                                                    
// CREATION DATE : 17/08/2017                                               
// CREATED BY    : MAHESH MALVE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Text;
using IITMS.NITPRM;

public partial class ACCOUNT_NarrationWiseLedgerList : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Grid_Entity objGrid = new Grid_Entity();
    Grid_Controller objGridController = new Grid_Controller();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    public static DataTable dt1 = new DataTable();
    string back = string.Empty;
    string space1 = "     ".ToString();
    string space2 = "          ".ToString();
    string space3 = string.Empty;
    DataTable dt = new DataTable();
    public static int RowIndex = -1;
    string[] para;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (hdnBal.Value != "")
        {
            // lblCurBal.Text = hdnBal.Value;
            // txtmd.Text = hdnMode.Value;
            //lblmode.Text = hdnMode.Value;
        }

        Session["WithoutCashBank"] = "Y";
        //btnGo.Attributes.Add("onClick", "return CheckFields();");
        if (!Page.IsPostBack)
        {
            SetDataColumn();

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
                    objCommon.DisplayMessage("Select company/cash book.", this);
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }


                    ViewState["action"] = "add";
                }
            }
            SetFinancialYear();


        }
        divMsg.InnerHtml = string.Empty;
    }
    private void SetFinancialYear()
    {
        FinCashBookController objCBC = new FinCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFrmDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtUptoDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }
    private void SetDataColumn()
    {
        DataColumn dc = new DataColumn();
        dc.ColumnName = "Date";
        dt.Columns.Add(dc);

        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "Particulars";
        dt.Columns.Add(dc1);

        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "VchType";
        dt.Columns.Add(dc2);

        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "VchNo";
        dt.Columns.Add(dc3);

        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "Debit";
        dt.Columns.Add(dc4);

        DataColumn dc5 = new DataColumn();
        dc5.ColumnName = "Credit";
        dt.Columns.Add(dc5);
        Session["DatatableMod"] = dt;
    }

    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        BindGrid();

    }

    /// <summary>
    /// Showing Grid Value Mah....
    /// </summary>
    private void BindGrid()
    {
        string LedgerName = txtAcc.Text.Split('*')[0].ToString();
        //string PARTY_NO = Request.QueryString["party_no"].ToString();
        DataSet dsVoucher = new DataSet();
        objGrid.CompCode = Session["comp_code"].ToString();
        //lblLedger.Text = txtAcc.Text;
        //lblFrm.Text = txtFrmDate.Text;
        //lblTo.Text = txtUptoDate.Text;
        objGrid.FromDate = Convert.ToDateTime(txtFrmDate.Text);
        objGrid.ToDate = Convert.ToDateTime(txtUptoDate.Text);
        objGrid.Ledger = txtAcc.Text;
        string from = objGrid.FromDate.ToString("dd-MMM-yyyy");
        string to = objGrid.ToDate.ToString("dd-MMM-yyyy");

        dsVoucher = objGridController.NarrationWithLedger(objGrid);

        if (dsVoucher.Tables[0].Rows.Count > 0)
        {
            grid.Visible = true;
            RptData.DataSource = dsVoucher;
            RptData.DataBind();
        }
        else
        {
            grid.Visible = false;
            RptData.DataSource = null;
            RptData.DataBind();
        }
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

    public static bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, "^\\d+$");
    }

    private void ClearRecord()
    {
        Session["VchDatatable"] = null;
        txtAcc.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // lblCurBal.Text = "0.00";
        txtAcc.Text = "";
        SetDataColumn();
        Session["VchDatatable"] = null;
        SetFinancialYear();
        txtAcc.Focus();
    }


    //Check for the Dates
    private void Checkdates()
    {
        if (txtFrmDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter From Date", this);
            txtFrmDate.Focus();
            return;
        }
        if (txtUptoDate.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter Upto Date", this);
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtUptoDate.Text), Convert.ToDateTime(Session["fin_date_to"])) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Upto Date Should Be In The Financial Year Range. ", this);
            txtUptoDate.Text = Convert.ToDateTime(Session["fin_date_to"]).ToString("dd/MM/yyyy");
            txtUptoDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(Session["fin_date_from"]), Convert.ToDateTime(txtFrmDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Should Be In The Financial Year Range. ", this);
            txtFrmDate.Text = Convert.ToDateTime(Session["fin_date_from"]).ToString("dd/MM/yyyy");
            txtFrmDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date Can Not Be Greater Than Upto Date Date. ", this);
            txtUptoDate.Focus();
            return;
        }
    }

    protected void txtUptoDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() != "")
        {
            //btnGo_Click(sender, e);
        }
        txtAcc.Focus();

    }
    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAcc.Text.ToString().Trim() != "")
        {
            //btnGo_Click(sender, e);
        }
        txtUptoDate.Focus();
        txtUptoDate.Text = Convert.ToDateTime(txtFrmDate.Text).AddMonths(1).ToString();
    }


    protected void ExportToExcel()
    {
        try
        {
            DataSet ds = new DataSet();

            objGrid.CompCode = Session["comp_code"].ToString();
            objGrid.FromDate = Convert.ToDateTime(txtFrmDate.Text);
            objGrid.ToDate = Convert.ToDateTime(txtUptoDate.Text);
            objGrid.Ledger = txtAcc.Text;

            ds = objGridController.NarrationWithLedger(objGrid);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridExcel.DataSource = ds;
                GridExcel.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ExportToExcel();

            string filename = string.Empty;
            string ContentType = string.Empty;
            filename = "ACCOUNT_NARRATION_" + txtAcc.Text.Replace("'", "_") + ".xls";

            filename = filename.Replace(" ", "_");
            filename = filename.Replace("/", "-");
            filename = filename.Replace(",", "-");
            GridExcel.Caption = Session["comp_name"].ToString().ToUpper();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= " + filename + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

            // gridstudentdetails.RenderControl(HtmlTextWriter);

            GridExcel.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();


            //ContentType = "ms-excel";
            //string attachment = "attachment; filename=" + filename;
            // Response.Clear();
            // Response.Buffer = true;
            // Response.AddHeader("content-disposition", attachment);
            //  Response.ContentType = "application/" + ContentType;
            //  StringWriter sw = new StringWriter();
            // HtmlTextWriter htm = new HtmlTextWriter(sw);

            // GridExcel.RenderControl(htm);
            // GridExcel.RenderControl(htw);
            // Response.Write(sw.ToString());

            // Response.Flush();
            // HttpContext.Current.ApplicationInstance.CompleteRequest();
            // Response.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_NarrationWiseLedgerList.btnExportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
        return;
    }

    #region Web Method

    //Fill AutoComplete Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItems(string prefixText)
    {
        List<string> Items = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            Grid_Controller objGridController = new Grid_Controller();
            ds = objGridController.fillItem(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Items.Add(ds.Tables[0].Rows[i]["Narration"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Items;
    }

    #endregion Web Method

}