//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : FIXED DEPOSITE REPORT                                                    
// CREATION DATE : 20-FEB-2019                                               
// CREATED BY    : NOKHLAL KUMAR                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using IITMS.NITPRM;

public partial class ACCOUNT_FD_Report : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountingVouchersController objAVC = new AccountingVouchersController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    
    protected void Page_Load(object sender, EventArgs e)
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
                //Page Authorization
                CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                txtInvestmentFrmDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
                txtInvestmentToDate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtInvestmentFrmDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
        txtInvestmentToDate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        try
        {
            if (txtInvestmentFrmDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Investment From Date", this);
                txtInvestmentFrmDate.Focus();
                return;
            }
            if (txtInvestmentToDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Investment Upto Date", this);
                txtInvestmentToDate.Focus();
                return;
            }

            ds = objAVC.GetFdExcelReport(Convert.ToDateTime(txtInvestmentFrmDate.Text).ToString("dd-MMM-yyyy"), Convert.ToDateTime(txtInvestmentToDate.Text).ToString("dd-MMM-yyyy"));

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridExcel.DataSource = ds.Tables[0];
                    GridExcel.DataBind();

                    AddHeader();

                    //string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";

                    HttpContext.Current.Response.Clear();
                    string attachment = "attachment; filename=FixedDepositReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GridExcel.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.UPDLedger, "No Data Found.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Error Occured!", this.Page);
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
        }
    }

    //Add Header to GridView
    private void AddHeader()
    {
        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = Session["coll_name"].ToString();
        HeaderCell.ColumnSpan = 14;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        GridExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "Fixed Deposit Report";
        HeaderCell1.ColumnSpan = 14;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        GridExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);


        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = "From " + Convert.ToDateTime(txtInvestmentFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtInvestmentToDate.Text).ToString("dd-MMM-yyyy");
        HeaderCell2.ColumnSpan = 14;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "";
        HeaderCell3.ColumnSpan = 14;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        GridExcel.Controls[0].Controls.AddAt(3, HeaderGridRow3);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }

}