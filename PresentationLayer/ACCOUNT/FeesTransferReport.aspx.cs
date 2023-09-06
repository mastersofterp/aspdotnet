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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text;
using System.IO;
using IITMS.NITPRM;

public partial class ACCOUNT_FeesTransferReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();

    FeesTransferStudentwiseController objFTS = new FeesTransferStudentwiseController();
    AccountTransaction objAccountTrans = new AccountTransaction();

    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        objCommon = new Common();
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
                    //objCommon.DisplayUserMessage(updBank, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    txtFromDate.Focus();
                    PopulateDegreeDropdown();
                    PopulateVoucherNo();
                    SetFinancialYear();
                }
            }
        }
    }

    #region Private Event

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
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtTodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }

    private void PopulateDegreeDropdown()
    {
        try
        {
            objCommon = new Common();
            DataSet ds = objFTS.PopulateDegreeFromRF(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDegree.Items.Insert(0, "Please Select");
                    ddlDegree.SelectedValue = "0";
                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataSource = ds.Tables[0]; ;
                    ddlDegree.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FeeTransferStudentwise.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateVoucherNo()
    {
        try
        {
            objCommon = new Common();
            string Database = connectionString.Split(';')[3];
            string DbName = Database.Split('=')[1];
            DataSet ds = objFTS.PopulateDegreeFromRF(_CCMS, Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString())), Session["comp_code"].ToString(), DbName);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlVoucher.Items.Clear();
                    ddlVoucher.Items.Insert(0, "Please Select");
                    //ddlVoucher.SelectedValue = "0";
                    ddlVoucher.DataTextField = "VoucherNo";
                    ddlVoucher.DataValueField = "VchNo";
                    ddlVoucher.DataSource = ds.Tables[0];
                    ddlVoucher.DataBind();
                }
                else
                {
                    ddlVoucher.DataSource = null;
                    ddlVoucher.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FeeTransferStudentwise.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtFromDate.Text = Session["fin_date_from"].ToString();
        txtTodate.Text = Session["fin_date_to"].ToString();

        txtFromDate.Focus();
        ViewState["uanos"] = null;
        ViewState["ReceiptNo"] = null;
        Session["dtFees"] = null;
    }

    #endregion Private Event

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        PopulateVoucherNo();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string DCRNO = string.Empty;
        double GrandAmount = 0;

        FeesTransferStudentwiseController objFTS = new FeesTransferStudentwiseController();
        DataSet ds = null;
        DataTable dtFees = new DataTable();

        ds = objFTS.populateTableModify(_CCMS, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlDegree.SelectedValue.ToString() == "Please Select" ? 0 : Convert.ToInt32(ddlDegree.SelectedValue.ToString())), Convert.ToInt32(ddlVoucher.SelectedValue.Split('*')[0]));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                GrandAmount = GrandAmount + Convert.ToDouble(ds.Tables[0].Rows[i]["TOTAL_AMT"]);
            }

            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Voucher No " + ddlVoucher.SelectedItem.Text.Split('-')[0];
            HeaderCell1.ColumnSpan = 8;
            HeaderCell1.BackColor = System.Drawing.Color.White;
            HeaderCell1.ForeColor = System.Drawing.Color.Black;
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GridView1.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Voucher Date " + ddlVoucher.SelectedItem.Text.Split('-')[1];
            HeaderCell2.ColumnSpan = 8;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            GridViewRow HeaderGridRow5 = new GridViewRow(5, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell5 = new TableCell();
            HeaderCell5.Text = "";
            HeaderCell5.ColumnSpan = 8;
            HeaderCell5.BackColor = System.Drawing.Color.White;
            HeaderCell5.ForeColor = System.Drawing.Color.Black;
            HeaderCell5.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow5.Cells.Add(HeaderCell5);
            GridView1.Controls[0].Controls.AddAt(5, HeaderGridRow5);

            string attachment = "attachment; filename=Fees_" + ddlVoucher.SelectedItem.Text.Replace(" ", "_") + ".xls";
            //"attachment; filename=FeesTransfer.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlDegree.SelectedValue = "Please Select";
        ddlVoucher.SelectedValue = "Please Select";
        Clear();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
}
