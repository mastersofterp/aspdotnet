//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : Advance Voucher Statement                                                  
// CREATION DATE : 17-09-2021                                            
// CREATED BY    : GOPAL ANTHATI                                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using IITMS.NITPRM;

public partial class ACCOUNT_AccAdvanceVoucherStmnt : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();


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
        { }
        else
            Response.Redirect("~/Default.aspx");

        if (hdnBal.Value != "")
        {
            //lblCurBal.Text = hdnBal.Value;
            //lblmode.Text = hdnMode.Value;
        }

        Session["WithoutCashBank"] = "N";
        //btnGo.Attributes.Add("onClick", "return CheckFields();");
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


                    ViewState["action"] = "add";

                }
            }
            ViewState["action"] = "add";
            if (Session["usertype"].ToString() == "1")
            {
                divSelectUser.Visible = true;
                objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "NATURE_NAME");
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "TITLE+' '+ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS NAME", "IDNO > 0", "FNAME");
            }
            else
            {
                divSelectUser.Visible = false;
            }
           

        }
        divMsg.InnerHtml = string.Empty;
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

    protected void ddlPayeeNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");
    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {

        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
        divPayee1.Visible = false;
        divPayee2.Visible = false;
        divPayeeNature1.Visible = false;
        divPayeeNature2.Visible = false;

        ddlEmployee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;
        ddlPayee.SelectedIndex = 0;

        if (ddlEmpType.SelectedValue == "1")
        {
            divEmployee1.Visible = true;
            divEmployee2.Visible = true;

            divPayee1.Visible = false;
            divPayee2.Visible = false;
            divPayeeNature1.Visible = false;
            divPayeeNature2.Visible = false;
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
            divPayee1.Visible = true;
            divPayee2.Visible = true;
            divPayeeNature1.Visible = true;
            divPayeeNature2.Visible = true;
            divEmployee1.Visible = false;
            divEmployee2.Visible = false;
        }

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmpType.SelectedValue == "1")
            {
                if (ddlEmployee.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Employee.", this);
                    return;
                }
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                if (ddlPayeeNature.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Payee Nature.", this);
                    return;
                }
                else if (ddlPayeeNature.SelectedIndex > 0 && ddlPayee.SelectedValue == "0")
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Select Payee Name.", this);
                    return;
                }
            }

            //if (txtFrmDate.Text.ToString().Trim() == "")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Enter From Date", this);
            //    txtFrmDate.Focus();
            //    return;
            //}
            //if (txtUptoDate.Text.ToString().Trim() == "")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Enter Upto Date", this);
            //    txtUptoDate.Focus();
            //    return;
            //}
            if (txtUptoDate.Text != "" && txtFrmDate.Text != "")
            {
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
            this.Export("Excel");
            //ExporttoExcel();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Export(string type)
    {
        try
        {
            AccountTransactionController objPC1 = new AccountTransactionController();
            int Idno = 0;
            if (Session["usertype"].ToString() == "1")
            {
                Idno = ddlEmployee.SelectedValue == "2" ? Convert.ToInt32(ddlPayee.SelectedValue) : Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                Idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "isnull(UA_IDNO,0)", "UA_NO=" + Convert.ToInt32(Session["userno"])));
            }

            DataSet ds = null;
            if (txtUptoDate.Text != "" && txtFrmDate.Text != "")
            {
                ds = objPC1.GetAdvanceVoucherStmntForExcel(Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtUptoDate.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(ddlEmpType.SelectedValue), Idno, Session["comp_code"].ToString());
            }
            else
            {
                ds = objPC1.GetAdvanceVoucherStmntForExcel("", "", Convert.ToInt32(ddlEmpType.SelectedValue), Idno,Session["comp_code"].ToString());
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView gv = new GridView();
                gv.DataSource = ds;
                gv.DataBind();
                string filename = string.Empty;
                string ContentType = string.Empty;
                filename = "AdvanceVoucherStatement.xls";
                ContentType = "ms-excel";
                string attachment = "attachment; filename=" + filename;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + ContentType;
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(UPDLedger, "No Records Found. ", this);
                return;
            }
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.SuppressContent = true;
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {

        }
    }
    private void ExporttoExcel()
    {
        try
        {
            AccountTransactionController objPC1 = new AccountTransactionController();

            if (txtFrmDate.Text != "" && txtUptoDate.Text != "")
            {

                int Idno = ddlEmployee.SelectedValue == "2" ? Convert.ToInt32(ddlPayee.SelectedValue) : Convert.ToInt32(ddlEmployee.SelectedValue);
                DataSet ds = objPC1.GetAdvanceVoucherStmntForExcel(Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtUptoDate.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(ddlEmpType.SelectedValue), Idno,Session["comp_code"].ToString());
                DataSet dsTotal = objCommon.FillDropDown("ACC_DAYBOOK_REPORT", "sum(DEBIT) as totDebit", "sum(CREDIT) totCredit", "party_no<>'0'", "");
                GridExcel.DataSource = ds;
                GridExcel.DataBind();

                GridExcel.Columns[2].Visible = false;
                GridExcel.Columns[8].Visible = false;
                GridExcel.Columns[9].Visible = false;
                #region Add Total
                DataRow rowTotal = ds.Tables[0].NewRow();
                rowTotal["PaymentVoucherNo"] = "Total";

                ds.Tables[0].Rows.Add(rowTotal);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                    GridExcel.DataSource = ds;
                    GridExcel.DataBind();
                    foreach (GridViewRow oItem in GridExcel.Rows)
                    {
                        if (oItem.Cells[5].Text == "Total")
                        {
                            oItem.Cells[5].Attributes.Add("class", "FinalHead");
                            oItem.Cells[6].Attributes.Add("class", "FinalHead");
                            oItem.Cells[7].Attributes.Add("class", "FinalHead");
                        }
                    }

                    AddHeader();

                    HttpContext.Current.Response.Clear();
                    string attachment = "attachment; filename=DayBook.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();

                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Response.Write(FinalHead);

                    GridExcel.RenderControl(htw);
                    Response.Write(sw.ToString());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                    //Response.Flush();
                }
                #endregion Add Total

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchersModifications.btnShowBalanceSheet_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void AddHeader()
    {
        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();
        HeaderCell = new TableCell();
        HeaderCell.Text = Session["comp_name"].ToString().ToUpper();
        HeaderCell.ColumnSpan = 14;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        GridExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.Text = "Advance Voucher Statement";
        HeaderCell1.ColumnSpan = 14;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        GridExcel.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        if (txtFrmDate.Text != "" && txtUptoDate.Text != "")
        {
            GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "From " + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            HeaderCell2.ColumnSpan = 14;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderCell2.ForeColor = System.Drawing.Color.Black;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GridExcel.Controls[0].Controls.AddAt(2, HeaderGridRow2);
        }

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
}