
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Vendor_Quotation_Entry.aspx                                      
// CREATION DATE : 16-march-2010                                                    
// CREATED BY    : chaitanya Bhure                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;

using System.Drawing.Printing;

using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using IITMS.SQLServer.SQLDAL;

using System.Collections;
using System.Globalization;


public partial class Stores_Transactions_Quotation_Str_Vendor_Quotation_Entry : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Vendor_Quotation_Entry_Controller objVQtEntry = new Str_Vendor_Quotation_Entry_Controller();
    STR_PARTY_ITEM_ENTRY objPIEntry = new STR_PARTY_ITEM_ENTRY();
    STR_PARTY_FIELD_ENTRY objPFEntry = new STR_PARTY_FIELD_ENTRY();
    DataTable tmpCalc;
    DataTable tmpPer;
    DataTable tmpInfo;
    GridView gvBudgetReport = new GridView();
    ArrayList arrlist = new ArrayList();
    ArrayList alPno = new ArrayList();
    ArrayList TotalPrice = new ArrayList();
    ArrayList TotalVale = new ArrayList();
    ArrayList GrandTotal = new ArrayList();
    ArrayList SubTotalPrice = new ArrayList();
    ArrayList SubTotalVale = new ArrayList();
    ArrayList GrandTotalPrice = new ArrayList();
    ArrayList GrandTotalVale = new ArrayList();

    ArrayList Tax = new ArrayList();
    ArrayList Discount = new ArrayList();

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //lbluser.Text = Session["userfullname"].ToString();
                //lblDept.Text = Session["strdeptname"].ToString();
                this.BindQuotation();
                Session["dsItem"] = null;
                Session["tmpInfo"] = null;
                Session["tmpCalc"] = null;
                Session["tmpPer"] = null;
                ViewState["TotAmount"] = null;
                Session["dsItem"] = null;
                ViewState["TaxTable"] = null;
                ViewState["Action"] = "add";

                //objCommon.ReportPopUp(btncmpitem, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Single_Item_Cmp_Report.rpt&param=@UserName=" + Session["userfullname"].ToString() + "," + "@P_QUOTNO=" + lstQtNo.SelectedValue + "," + "@P_ITEM_NO=" + lstItem.SelectedValue, "UAIMS");
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    private void BindQuotation()
    {
        lstQtNo.Items.Clear();
        lstQuot1.Items.Clear();
        lstQtNo.Items.Insert(0, new ListItem("Please Select", "0"));
        lstQuot1.Items.Insert(0, new ListItem("Please Select", "0"));

        DataSet ds = objVQtEntry.GetQuotationByDepartment(Convert.ToInt32(Session["strdeptcode"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstQtNo.DataSource = ds.Tables[0];
            lstQtNo.DataTextField = "REFNO";
            lstQtNo.DataValueField = "QUOTNO";
            lstQtNo.DataBind();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            lstQuot1.DataSource = ds.Tables[1];
            lstQuot1.DataTextField = "REFNO";
            lstQuot1.DataValueField = "QUOTNO";
            lstQuot1.DataBind();
        }
    }

    private void BindQuotforComp()
    {
        lstQuot1.Items.Clear();
        lstQuot1.Items.Insert(0, new ListItem("Please Select", "0"));

        DataSet ds = objVQtEntry.GetQuotationByDepartment(Convert.ToInt32(Session["strdeptcode"].ToString()));
        if (ds.Tables[1].Rows.Count > 0)
        {
            lstQuot1.DataSource = ds.Tables[1];
            lstQuot1.DataTextField = "REFNO";
            lstQuot1.DataValueField = "QUOTNO";
            lstQuot1.DataBind();
        }
    }

    protected void lstQtNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Count = Convert.ToInt32(objCommon.LookUp("STORE_PARTYENTRY", "COUNT(*)", "QUOTNO = '" + lstQtNo.SelectedValue + "' AND POSTATUS=1"));
        if (Count > 0)
        {
            lstVendor.Enabled = false;
            return;
        }
        this.BindVendorByQuotno(lstQtNo.SelectedValue);
        this.BindLastSubmitDateTime(lstQtNo.SelectedValue);
        divItemEntryList.Visible = false;
        pnlCmpst.Visible = false;
        pnlitems.Visible = false;
    }

    private void BindVendorByQuotno(string quotno)
    {
        lstVendor.Items.Clear();
        lstVendor.Items.Insert(0, new ListItem("Please Select", "0"));
        DataSet dsvendor = objVQtEntry.GetVendorByQuotation(quotno);
        if (dsvendor.Tables[0].Rows.Count > 0)
        {
            lstVendor.DataSource = dsvendor.Tables[0];
            lstVendor.DataTextField = "PNAME";
            lstVendor.DataValueField = "PNO";
            lstVendor.DataBind();
        }
        for (int i = 0; i < dsvendor.Tables[0].Rows.Count; i++)
        {
            Boolean postatus = Convert.ToBoolean(dsvendor.Tables[0].Rows[i]["POSTATUS"].ToString());
            int flag = Convert.ToInt32(dsvendor.Tables[0].Rows[i]["flag"]);
            if (postatus == true || flag == 1)
            {
                lstVendor.Enabled = true;
                break;
            }
            else
                lstVendor.Enabled = true;
        }
    }

    private void BindLastSubmitDateTime(string quotno)
    {
        DataSet dsSingleQuotation = objVQtEntry.GetSingleQuotation(quotno);
        txtLastDate.Text = Convert.ToDateTime(dsSingleQuotation.Tables[0].Rows[0]["LDATE"]).ToString("dd/MM/yyyy");
        // txtLastTime.Text = Convert.ToDateTime(dsSingleQuotation.Tables[0].Rows[0]["LTIME"]).ToString("hh:mm:ss tt");
    }
    protected void lstVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.InitializeTmpDatatable();
        this.BindItemList(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
        //btnSubmit.Visible = true;       
    }

    private void BindItemList(string quotno, int Pno)
    {
        DataSet dsItems = objVQtEntry.GetItemsByQuotNo(quotno, Pno);
        Session["dsItem"] = dsItems.Tables[0];
        ViewState["PINO"] = dsItems.Tables[0].Rows[0]["PINO"].ToString();
        if (dsItems.Tables[0].Rows.Count > 0)
        {
            grdItemList.DataSource = dsItems.Tables[0];
            string[] DataKeyNames = { "ITEM_NO" };
            grdItemList.DataKeyNames = DataKeyNames;
            string[] DataKey = { "PINO" };
            grdItemList.DataKeyNames = DataKeyNames;
            grdItemList.DataBind();
            pnlitems.Visible = true;

            hdnRowCount.Value = dsItems.Tables[0].Rows.Count.ToString();

        }
        else
        {
            grdItemList.DataSource = null;
            // string[] DataKeyNames = { "ITEM_NO" };
            //grdItemList.DataKeyNames = DataKeyNames;
            grdItemList.DataBind();
            pnlitems.Visible = false;
        }
        if (dsItems.Tables[1].Rows.Count > 0)
        {
            ViewState["TaxTable"] = dsItems.Tables[1];
            hdnListCount.Value = dsItems.Tables[1].Rows.Count.ToString();
            ViewState["Action"] = "edit";
            hdnOthEdit.Value = "1";
        }
        else
        {
            ViewState["TaxTable"] = null;
        }

    }

    //Save Party Fields 

    protected void ClearItem()
    {
        // divItemEntryList.Visible = true;
        ViewState["TotAmount"] = null;
        ViewState["PINO"] = null;
    }

    protected void BindItemForCmpStmtByQuot(string Quotno, int Pno)
    {
        DataSet dsItems = objVQtEntry.GetItemsByQuotNo(Quotno, Pno);
        lstItem.DataSource = dsItems.Tables[0];
        lstItem.DataTextField = "ITEM_NAME";
        lstItem.DataValueField = "ITEM_NO";
        lstItem.DataBind();
    }

    void InitializeTmpDatatable()
    {
        StoreMasterController objStoreMaster = new StoreMasterController();
        //Temporary Table For Informative DataFields
        tmpInfo = objStoreMaster.GetSingleRecordField(0).Tables[0];
        DataColumn[] PriFortmpInfo = { tmpInfo.Columns["FNO"] };
        tmpInfo.PrimaryKey = PriFortmpInfo;
        //Temporary Table For Calculative DataFields
        tmpCalc = objStoreMaster.GetSingleRecordField(0).Tables[0];
        DataColumn[] PriFortmpCalc = { tmpCalc.Columns["FNO"] };
        tmpCalc.PrimaryKey = PriFortmpCalc;
        //Temporary Table For Percentage DataFields
        tmpPer = objStoreMaster.GetSingleRecordField(0).Tables[0];
        DataColumn[] PriFortmpPer = { tmpPer.Columns["FNO"] };
        tmpPer.PrimaryKey = PriFortmpPer;
        //add to session
        Session["tmpInfo"] = tmpInfo;
        Session["tmpCalc"] = tmpCalc;
        Session["tmpPer"] = tmpPer;
    }
    protected void lstQuot1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemForCmpStmtByQuot(lstQuot1.SelectedValue, Convert.ToInt32(0));

        string CompStatApproval = objCommon.LookUp("STORE_REFERENCE", "ISNULL(IS_COMPARATIVE_STAT_APPROVAL,0) as COMP_STAT_APPROVAL", "");
        ViewState["IS_COMPARATIVE_STAT_APPROVAL"] = CompStatApproval;

        if (ViewState["IS_COMPARATIVE_STAT_APPROVAL"].ToString() == "1")
        {
            btnApproval.Visible = true;
        }
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstQuot1.SelectedValue != "" || lstQuot1.SelectedValue != "0")
            {
                CustomStatus cs = (CustomStatus)objVQtEntry.SendStatementForApproval(lstQuot1.SelectedValue);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                    {
                        SendEmailToAuthority(lstQuot1.SelectedValue);
                    }
                    Showmessage("Record Send Successfully.");
                    return;
                }

            }
            else
            {
                Showmessage("Please Select Quotation From List.");
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, ex.Message);
        }
    }

    #region Mail Sending
    private void SendEmailToAuthority(string QUOTNO)
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = objVQtEntry.GetFromDataForEmail(QUOTNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = ds.Tables[0].Rows[0]["UA_EMAIL"].ToString();

                sendmail(fromEmailId, fromEmailPwd, receiver, "Comparative Statement Approval", "Dear Sir");

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;
            string QuotNo = string.Empty;
            string QuotRefNo = string.Empty;

            if (lstQuot1.SelectedValue != "")
            {
                QuotNo = lstQuot1.SelectedValue;
                QuotRefNo = lstQuot1.SelectedItem.Text;
            }

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />Quotation Reference No. : " + QuotRefNo);
            MailBody.AppendLine(@"<br />is sending you for approval. It is available at your login.");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            MailBody.AppendLine(@"<br />" + Session["userfullname"].ToString());


            mailMessage.Body = MailBody.ToString();

            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mailMessage);

            Showmessage("Record Send Successfully.");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    #region Comparative Report
    protected void btncmpitem_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstItem.SelectedValue == string.Empty || lstItem.SelectedValue == "" || lstItem.SelectedValue == "0")
            {
                Showmessage("Please Select Atleast One Item");
            }
            else
            {
                ShowReport("Comparative_Statements", "Single_Item_Cmp_Report.rpt");
            }
        }
        catch (Exception eX)
        {
            objCommon.ShowError(this, eX.Message);
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Stores," + rptFileName;
            url += "&param=@UserName=" + Session["userfullname"].ToString() + "," + "@P_QUOTNO=" + Convert.ToString(lstQuot1.SelectedValue) + "," + "@P_ITEM_NO=" + Convert.ToInt32(lstItem.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btncmpall_Click(object sender, EventArgs e)
    {
        if (lstQuot1.SelectedIndex >= 0)
        {
            DataTable VendorDt = objVQtEntry.GetVendorForCmpRpt(lstQuot1.SelectedValue).Tables[0];
            if (VendorDt.Rows.Count == 0)
            {
                Showmessage("Please Save All The Items For At Least One Vendor");
                return;
            }
            //Added by vijay andoju For getting Proper Structure for Showing Report
            DataTable dtBind = Datateble(VendorDt);

            gvBudgetReport.RowDataBound += new GridViewRowEventHandler(gvBudgetReport_RowDataBound);

            gvBudgetReport.DataSource = dtBind;
            gvBudgetReport.DataBind();

            int ColumnCount = dtBind.Columns.Count;
            int rowCount = dtBind.Rows.Count;

            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=ComparativeStatement.xls";

            AddHeader(ColumnCount, rowCount);



            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", attachment);
            Response.AppendHeader("Refresh", ".5; BudgetReportNew.aspx");
            Response.Charset = "";
            Response.ContentType = "application/" + ContentType;
            StringWriter sw1 =

                new StringWriter();
            HtmlTextWriter htw1 = new HtmlTextWriter(sw1);
            gvBudgetReport.HeaderRow.Visible = false;
            gvBudgetReport.RenderControl(htw1);
            Response.Output.Write(sw1.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            Showmessage("Please Select Quotation From The List.");
            //Tabs.ActiveTabIndex = 3;
        }
    }
    //added by vijay andoju on 21102020 for Store the values by vendor wise
    protected void gvBudgetReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvBudgetReport.HeaderRow.Cells.Count; i++)
                {
                    string Header = gvBudgetReport.HeaderRow.Cells[i].Text;
                    if (Header.ToString().Contains("/") || Header.ToString().Contains("Price/Unit") || Header.ToString().Contains("Value"))
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;")
                        {
                            //e.Row.Cells[i].Text = String.Format("{0:N2}", e.Row.Cells[i].Text == "&nbsp;" ? 0.00 : Convert.ToDouble(e.Row.Cells[i].Text));
                            e.Row.Cells[i].Text = IndianCurrency(e.Row.Cells[i].Text);
                        }
                        else
                        {
                            e.Row.Cells[i].Text = IndianCurrency("0");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void AddHeader(int colspan, int Row)
    {
        int rows = Row + 9;


        string[] Col = { "Price/Unit", "Value" };

        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell();

        string CollegeName = objCommon.LookUp("reff", "CollegeName", "");

        //HeaderCell.Text = "Maulana Abul Kalam Azad University of Technology, West Bengal"; //08/04/2022 shabina
        HeaderCell.Text = CollegeName; //08/04/2022 shabina
        HeaderCell.ColumnSpan = colspan;
        HeaderCell.Font.Size = 14;
        HeaderCell.Font.Bold = true;
        HeaderCell.BackColor = System.Drawing.Color.White;
        HeaderCell.ForeColor = System.Drawing.Color.Black;
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow.Cells.Add(HeaderCell);
        gvBudgetReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

        GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell1 = new TableCell();
        string CollegeAddress = objCommon.LookUp("reff", "College_address", "");

        HeaderCell1.Text = CollegeAddress;  //08/04/2022 shabina
        //HeaderCell1.Text = "SIMHAT, HARINGHATA, NADIA, WEST  BENGAL, INDIA - 741249."; //08/04/2022 shabina
        HeaderCell1.ColumnSpan = colspan;
        HeaderCell1.Font.Size = 9;
        HeaderCell1.Font.Bold = true;
        HeaderCell1.BackColor = System.Drawing.Color.White;
        HeaderCell1.ForeColor = System.Drawing.Color.Black;
        HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow1.Cells.Add(HeaderCell1);
        gvBudgetReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

        GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell2 = new TableCell();
        HeaderCell2 = new TableCell();
        HeaderCell2.Text = "For FT/GN/20/08/01.04.19";
        HeaderCell2.ColumnSpan = colspan;
        HeaderCell2.Font.Size = 10;
        HeaderCell2.Font.Bold = true;
        HeaderCell2.BackColor = System.Drawing.Color.White;
        HeaderCell2.ForeColor = System.Drawing.Color.Black;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow2.Cells.Add(HeaderCell2);
        gvBudgetReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

        GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell3 = new TableCell();
        HeaderCell3 = new TableCell();
        HeaderCell3.Text = "COMPARATIVE STATEMENT";
        HeaderCell3.ColumnSpan = colspan;
        HeaderCell3.Font.Size = 14;
        HeaderCell3.Font.Bold = true;
        HeaderCell3.BackColor = System.Drawing.Color.White;
        HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow3.Cells.Add(HeaderCell3);
        gvBudgetReport.Controls[0].Controls.AddAt(3, HeaderGridRow3);

        GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell4 = new TableCell();
        HeaderCell4 = new TableCell();
        HeaderCell4.Text = "Guidelines for preparation of Comparative Statement";
        HeaderCell4.ColumnSpan = colspan;
        HeaderCell4.Font.Size = 9;
        HeaderCell4.Font.Bold = true;
        HeaderCell4.BackColor = System.Drawing.Color.White;
        HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow4.Cells.Add(HeaderCell4);
        gvBudgetReport.Controls[0].Controls.AddAt(4, HeaderGridRow4);

        GridViewRow HeaderGridRow5 = new GridViewRow(5, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell5 = new TableCell();
        HeaderCell5 = new TableCell();
        HeaderCell5.Text = "* The final approval form should be prepared supplier wise";
        HeaderCell5.ColumnSpan = colspan;
        HeaderCell5.Font.Size = 10;
        HeaderCell5.Font.Bold = true;
        HeaderCell5.BackColor = System.Drawing.Color.White;
        HeaderCell5.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow5.Cells.Add(HeaderCell5);
        gvBudgetReport.Controls[0].Controls.AddAt(5, HeaderGridRow5);

        #region Headder

        GridViewRow HeaderGridRow6 = new GridViewRow(6, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell Header6Cell = new TableCell();
        Header6Cell.Text = "Sr.No.";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        Header6Cell = new TableCell();
        Header6Cell.Text = "Description";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        //----Added by vijay andoju 21-07-2020 for showing opening balance

        Header6Cell = new TableCell();
        Header6Cell.Text = "Quantity";
        Header6Cell.ColumnSpan = 1;
        Header6Cell.RowSpan = 2;
        Header6Cell.Font.Size = 10;
        Header6Cell.Font.Bold = true;
        Header6Cell.HorizontalAlign = HorizontalAlign.Center;
        HeaderGridRow6.Cells.Add(Header6Cell);
        gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

        TableCell HeaderN = new TableCell();

        for (int i = 0; i < arrlist.Count; i++)
        {
            for (int j = i; j == i; j++)
            {
                HeaderN = new TableCell();

                HeaderN.Text = arrlist[j].ToString();


                HeaderN.ColumnSpan = 2;
                HeaderN.RowSpan = 1;
                HeaderN.Font.Size = 10;
                HeaderN.Font.Bold = true;
                HeaderN.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow6.Cells.Add(HeaderN);
                gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

            }
        }

        GridViewRow HeaderGridRow7 = new GridViewRow(7, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell Header7Cell = new TableCell();

        for (int i = 1; i <= arrlist.Count; i++)
        {
            for (int j = 0; j < Col.Length; j++)
            {

                TableCell HeaderN1 = new TableCell();
                HeaderN1.Text = Col[j].ToString();
                HeaderN1.ColumnSpan = 1;
                HeaderN1.RowSpan = 1;
                HeaderN1.Font.Size = 10;
                HeaderN1.Font.Bold = true;
                HeaderN1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow7.Cells.Add(HeaderN1);
                gvBudgetReport.Controls[0].Controls.AddAt(7, HeaderGridRow7);
            }
        }

        #endregion

        //----Added by Gopal Anthati 10-12-2020 to get Calculative Taxes Dynamically ***
        #region Dynamic Total
        string NetAmount = string.Empty;
        DataTable CalculativeDt = objVQtEntry.GetCalculativHeadsForCmpRpt(lstQuot1.SelectedValue).Tables[0];

        List<string> CalculativeHead = new List<string>();
        List<string> CalculativeHeadNo = new List<string>();
        CalculativeHead.Add("Net Amount");
        CalculativeHeadNo.Add("0");
        for (int i = 0; i < CalculativeDt.Rows.Count; i++)
        {
            CalculativeHead.Add(CalculativeDt.Rows[i]["TAX_NAME"].ToString());
            CalculativeHeadNo.Add(CalculativeDt.Rows[i]["TAXID"].ToString());
        }

        for (int i = 1; i <= CalculativeHead.Count; i++)
        {
            GridViewRow HeaderGridRow10 = new GridViewRow(11, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell10 = new TableCell();
            HeaderCell10 = new TableCell();

            HeaderCell10.Text = CalculativeHead[i - 1];
            HeaderCell10.ColumnSpan = 3;
            HeaderCell10.RowSpan = 1;
            HeaderCell10.Font.Size = 11;

            HeaderCell10.Font.Bold = true;

            HeaderCell10.HorizontalAlign = HorizontalAlign.Left;
            HeaderGridRow10.Cells.Add(HeaderCell10);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow10);


            //for (int j = 1; j <= arrlist.Count * 2; j++)
            for (int j = 1; j <= arrlist.Count; j++)
            {
                for (int k = 1; k <= 2; k++)
                {
                    TableCell HeaderN1 = new TableCell();
                    DataSet CalculativeChargDt = objVQtEntry.GetCalculativeCharges(lstQuot1.SelectedValue, Convert.ToInt32(alPno[j - 1].ToString()), Convert.ToInt32(CalculativeHeadNo[i - 1]));
                    if (CalculativeHeadNo[i - 1] == "0")
                    {
                        if (k == 1)
                        {
                            HeaderN1.Text = "0";
                        }
                        else
                        {
                            String GrandTotalAmount = "0";
                            string CalAmt = objCommon.LookUp("STORE_PARTYENTRY A INNER JOIN  STORE_PARTYFIELDENTRY B ON A.PNO=B.PNO AND A.QUOTNO=B.QUOTNO INNER JOIN  STORE_TAX_MASTER C ON (B.TAXID=C.TAXID)", "SUM(isnull(TAX_AMOUNT,0))TAX_AMOUNT", "A.QUOTNO='" + lstQuot1.SelectedValue + "' AND B.PNO =" + Convert.ToInt32(alPno[j - 1].ToString()));
                            if (CalAmt == "") CalAmt = "0";
                            //for (int l = 1; l <= GrandTotal.Count; l++)
                            //{
                            GrandTotalAmount = GrandTotal[j - 1].ToString();
                            //}
                            NetAmount = IndianCurrency((Convert.ToDouble(CalAmt) + Convert.ToDouble(GrandTotalAmount)).ToString());

                            string[] array = NetAmount.Split('.');
                            if (array[1] == "50")
                            {
                                NetAmount = (Convert.ToDouble(NetAmount) + 0.01).ToString();
                            }
                            //HeaderN1.Text = IndianCurrency((Math.Round(Convert.ToDouble(CalAmt) + Convert.ToDouble(GrandTotalAmount))).ToString());
                            HeaderN1.Text = IndianCurrency(Math.Round(Convert.ToDouble(NetAmount)).ToString());
                        }
                    }
                    else
                    {
                        if (k == 1)
                        {
                            HeaderN1.Text = "0";
                        }
                        else
                        {
                            if (CalculativeChargDt.Tables[0].Rows.Count > 0)
                                HeaderN1.Text = IndianCurrency(CalculativeChargDt.Tables[0].Rows[0]["AMT"].ToString());
                            else
                                HeaderN1.Text = "0";
                        }
                    }


                    HeaderN1.ColumnSpan = 1;
                    HeaderN1.RowSpan = 1;
                    HeaderN1.Font.Size = 11;
                    if (i % 2 == 0)
                    {
                        HeaderN1.Font.Bold = false;
                    }
                    else
                    {
                        HeaderN1.Font.Bold = true;
                    }

                    HeaderN1.HorizontalAlign = HorizontalAlign.Right;
                    HeaderGridRow10.Cells.Add(HeaderN1);
                    gvBudgetReport.Controls[0].Controls.AddAt(rows + 2, HeaderGridRow10);
                }
            }
        }
        int count = CalculativeDt.Rows.Count;
        #endregion

        #region Total

        //string[] Head = { "Grand Total Amount", "Add - GST", "Total Amount", "Less - Discount" };
        string[] Head = { "Total Amount", "Less - Discount" };
        for (int i = 1; i <= Head.Length; i++)
        {
            GridViewRow HeaderGridRow9 = new GridViewRow(10, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell9 = new TableCell();
            HeaderCell9 = new TableCell();

            HeaderCell9.Text = Head[i - 1];
            HeaderCell9.ColumnSpan = 3;
            HeaderCell9.RowSpan = 1;
            HeaderCell9.Font.Size = 11;

            HeaderCell9.Font.Bold = true;

            HeaderCell9.HorizontalAlign = HorizontalAlign.Left;
            HeaderGridRow9.Cells.Add(HeaderCell9);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + 0, HeaderGridRow9);
            for (int j = 1; j <= arrlist.Count * 2; j++)
            {

                TableCell HeaderN1 = new TableCell();
                if (i == 1)
                {
                    HeaderN1.Text = IndianCurrency(TotalPrice[j - 1].ToString());
                }
                if (i == 2)
                {
                    HeaderN1.Text = IndianCurrency(Discount[j - 1].ToString());
                }
                if (i == 3)
                {
                    HeaderN1.Text = IndianCurrency(Tax[j - 1].ToString());
                }
                if (i == 4)
                {
                    HeaderN1.Text = IndianCurrency(GrandTotalVale[j - 1].ToString());
                }

                HeaderN1.ColumnSpan = 1;
                HeaderN1.RowSpan = 1;
                HeaderN1.Font.Size = 11;
                if (i % 2 == 0)
                {
                    HeaderN1.Font.Bold = false;
                }
                else
                {
                    HeaderN1.Font.Bold = true;
                }

                HeaderN1.HorizontalAlign = HorizontalAlign.Right;
                HeaderGridRow9.Cells.Add(HeaderN1);
                gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow9);
            }

        }
        #endregion

        #region SignatureHead
       //   string[] FooterHead = { "Prepared by", "Head of the Department", "Accounts Section", "Principal", "Secretary", "Treasurer" };    // 24/03/2023 by shabina for crescent requirement.
        string[] FooterHead = { "Prepared by", "Checked by", "Approved by" };    /// Modified on 24/03/2023 
        GridViewRow HeaderGridRow12 = new GridViewRow(12, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //  for (int i = 0; i < 6; i++)                  //-----------// 24/03/2023 by shabi------
        for (int i = 0; i < 3; i++)             //----modified       24/03/2023
        {
            TableCell HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
          //  HeaderCell11.ColumnSpan = 1;  //modified  27-03-2023
            HeaderCell11.ColumnSpan = 2;
            HeaderCell11.RowSpan = 1;
            HeaderCell11.Font.Size = 10;
            HeaderCell11.Font.Bold = true;
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow12.Cells.Add(HeaderCell11);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + (4 + count), HeaderGridRow12);
        }
        GridViewRow HeaderGridRow11 = new GridViewRow(13, 0, DataControlRowType.Header, DataControlRowState.Insert);
        // for (int i = 0; i < 5; i++)   //=--// 24/03/2023 by shabina
        for (int i = 0; i < 3; i++)    //----Modified 
        {
            TableCell HeaderCell11 = new TableCell();
            HeaderCell11.Text = FooterHead[i].ToString();
            //  HeaderCell11.ColumnSpan = 1;  //modified  27-03-2023
            HeaderCell11.ColumnSpan = 2;
            HeaderCell11.RowSpan = 2;
            HeaderCell11.Font.Size = 10;
            HeaderCell11.Font.Bold = true;
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow11.Cells.Add(HeaderCell11);
            gvBudgetReport.Controls[0].Controls.AddAt(rows + (5 + count), HeaderGridRow11);
        }
        #endregion

        gvBudgetReport.FooterStyle.Font.Bold = true;
        gvBudgetReport.FooterStyle.Font.Size = 19;
    }
    private DataTable Datateble(DataTable dtvendor)
    {


        //Added by vijay andoju for Creating Table 
        DataTable dt = new DataTable();
        dt.Columns.Add("Sl.No", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        //---------------------------------------------


        //Creating Column names for Vendor wise
        for (int i = 0; i < dtvendor.Rows.Count; i++)
        {
            dt.Columns.Add("Price/Unit" + i, typeof(string));
            dt.Columns.Add("Value" + i, typeof(string));
            arrlist.Add(dtvendor.Rows[i]["PNAME"].ToString());
            alPno.Add(dtvendor.Rows[i]["PNO"].ToString());
        }
        //---------------------------------------------------


        //Inserting the Itemname Qty in table

        DataTable QuotItemDt = objVQtEntry.GetItemsByQuotNo(lstQuot1.SelectedValue, Convert.ToInt32(0)).Tables[0];
        for (int j = 0; j < QuotItemDt.Rows.Count; j++)
        {
            DataRow Row1;
            Row1 = dt.NewRow();
            Row1["Sl.No"] = j + 1;
            Row1["Description"] = QuotItemDt.Rows[j]["Item_name"].ToString();
            Row1["Quantity"] = QuotItemDt.Rows[j]["Qty"].ToString();
            dt.Rows.Add(Row1);


            for (int k = 0; k < dtvendor.Rows.Count; k++)
            {
                DataTable RateByVendorandItemDt = objVQtEntry.GetItemsForVendor(lstQuot1.SelectedValue, Convert.ToInt32(dtvendor.Rows[k]["PNO"].ToString()), Convert.ToInt32(QuotItemDt.Rows[j]["ITEM_NO"].ToString())).Tables[0];
                if (RateByVendorandItemDt.Rows.Count > 0)
                {
                    dt.Rows[j]["Price/Unit" + k] = RateByVendorandItemDt.Rows[0]["PRICE"].ToString();
                    dt.Rows[j]["Value" + k] = Convert.ToDouble(QuotItemDt.Rows[j]["Qty"]) * Convert.ToDouble(RateByVendorandItemDt.Rows[0]["PRICE"]);
                    // dt.Rows[j]["Value" + k] = RateByVendorandItemDt.Rows[0]["Grand"].ToString();
                    dt.Rows[j].AcceptChanges();
                }
                else
                {
                    dt.Rows[j]["Price/Unit" + k] = "0";
                    dt.Rows[j]["Value" + k] = "0";
                    dt.Rows[j].AcceptChanges();
                }
            }
        }
        //--------------------------------------------------------------------------------------------------
        int RoCount1 = dt.Rows.Count - 1;


        //added by vijay andoju 21102020 for calculation perpose like TotalAmount,SubTotalAmount,TaxAmount,DiscountAmount,GrandTotal
        for (int n = 0; n < arrlist.Count; n++)
        {

            Discount.Add("0");
            Discount.Add(Convert.ToDecimal(objCommon.LookUp("STORE_PARTYITEMENTRY", "ISNULL(SUM(DISCOUNT_AMOUNT),0)DISAMT", " PNO=" + Convert.ToInt32(alPno[n].ToString()) + " AND QUOTNO='" + lstQuot1.SelectedValue + "'")).ToString());

            TotalPrice.Add(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price/Unit" + n] == DBNull.Value ? 0 : x["Price/Unit" + n])));
            TotalPrice.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n])))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()));


            DataTable NetExtraCharge = null;//objVQtEntry.GetExtraCharge(lstQuot1.SelectedValue, Convert.ToInt32(alPno[n].ToString())).Tables[0];
            if (NetExtraCharge != null && NetExtraCharge.Rows.Count > 0)
            {
                Tax.Add("0");
                //Tax.Add(Convert.ToDecimal(Convert.ToDecimal(TotalPrice[TotalPrice.Count - 1]) * Convert.ToDecimal(NetExtraCharge.Rows[0]["TOTVAT"])) / 100);
                Tax.Add(NetExtraCharge.Rows[0]["TOTVAT"].ToString());
            }
            else
            {
                Tax.Add("0");
                Tax.Add("0");
            }
            GrandTotalVale.Add(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price/Unit" + n] == DBNull.Value ? 0 : x["Price/Unit" + n]))));
            GrandTotalVale.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n]))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()) + Convert.ToDecimal(Tax[Tax.Count - 1])).ToString());

            GrandTotal.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n]))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()) + Convert.ToDecimal(Tax[Tax.Count - 1])).ToString());

        }

        //--------------------------------------------------------------------------------------------------------

        return dt;

    }
    private string IndianCurrency(string AMOUNT)
    {

        decimal Amount = decimal.Parse(AMOUNT, CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("en-IN");
        string text = Amount.ToString("N2", hindi);
        return text;
    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    //added by vijay andoju forcomparative statement report
    //private void AddHeader(int colspan, int Row)
    //{
    //    int rows = Row + 9;


    //    string[] Col = { "Price/Unit", "Value" };

    //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell = new TableCell();

    //    //DataSet ds= Common.lo
    //    HeaderCell.Text = "Sri Venkateswara College of Engineering";
    //    HeaderCell.ColumnSpan = colspan;
    //    HeaderCell.Font.Size = 14;
    //    HeaderCell.Font.Bold = true;
    //    HeaderCell.BackColor = System.Drawing.Color.White;
    //    HeaderCell.ForeColor = System.Drawing.Color.Black;
    //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow.Cells.Add(HeaderCell);
    //    gvBudgetReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

    //    GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell1 = new TableCell();
    //    HeaderCell1.Text = "Pennalur, Sriperumbudur Tk - 602 117.";
    //    HeaderCell1.ColumnSpan = colspan;
    //    HeaderCell1.Font.Size = 9;
    //    HeaderCell1.Font.Bold = true;
    //    HeaderCell1.BackColor = System.Drawing.Color.White;
    //    HeaderCell1.ForeColor = System.Drawing.Color.Black;
    //    HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow1.Cells.Add(HeaderCell1);
    //    gvBudgetReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

    //    GridViewRow HeaderGridRow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell2 = new TableCell();
    //    HeaderCell2 = new TableCell();
    //    HeaderCell2.Text = "For FT/GN/20/08/01.04.19";
    //    HeaderCell2.ColumnSpan = colspan;
    //    HeaderCell2.Font.Size = 10;
    //    HeaderCell2.Font.Bold = true;
    //    HeaderCell2.BackColor = System.Drawing.Color.White;
    //    HeaderCell2.ForeColor = System.Drawing.Color.Black;
    //    HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow2.Cells.Add(HeaderCell2);
    //    gvBudgetReport.Controls[0].Controls.AddAt(2, HeaderGridRow2);

    //    GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell3 = new TableCell();
    //    HeaderCell3 = new TableCell();
    //    HeaderCell3.Text = "COMPARATIVE STATEMENT";
    //    HeaderCell3.ColumnSpan = colspan;
    //    HeaderCell3.Font.Size = 14;
    //    HeaderCell3.Font.Bold = true;
    //    HeaderCell3.BackColor = System.Drawing.Color.White;
    //    HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow3.Cells.Add(HeaderCell3);
    //    gvBudgetReport.Controls[0].Controls.AddAt(3, HeaderGridRow3);

    //    GridViewRow HeaderGridRow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell4 = new TableCell();
    //    HeaderCell4 = new TableCell();
    //    HeaderCell4.Text = "Guidelines for preparation of Comparative Statement";
    //    HeaderCell4.ColumnSpan = colspan;
    //    HeaderCell4.Font.Size = 9;
    //    HeaderCell4.Font.Bold = true;
    //    HeaderCell4.BackColor = System.Drawing.Color.White;
    //    HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow4.Cells.Add(HeaderCell4);
    //    gvBudgetReport.Controls[0].Controls.AddAt(4, HeaderGridRow4);

    //    GridViewRow HeaderGridRow5 = new GridViewRow(5, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell HeaderCell5 = new TableCell();
    //    HeaderCell5 = new TableCell();
    //    HeaderCell5.Text = "* The final approval form should be prepared supplier wise";
    //    HeaderCell5.ColumnSpan = colspan;
    //    HeaderCell5.Font.Size = 10;
    //    HeaderCell5.Font.Bold = true;
    //    HeaderCell5.BackColor = System.Drawing.Color.White;
    //    HeaderCell5.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow5.Cells.Add(HeaderCell5);
    //    gvBudgetReport.Controls[0].Controls.AddAt(5, HeaderGridRow5);

    //    #region Headder

    //    GridViewRow HeaderGridRow6 = new GridViewRow(6, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell Header6Cell = new TableCell();
    //    Header6Cell.Text = "Sr.No.";
    //    Header6Cell.ColumnSpan = 1;
    //    Header6Cell.RowSpan = 2;
    //    Header6Cell.Font.Size = 10;
    //    Header6Cell.Font.Bold = true;
    //    Header6Cell.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow6.Cells.Add(Header6Cell);
    //    gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

    //    Header6Cell = new TableCell();
    //    Header6Cell.Text = "Description";
    //    Header6Cell.ColumnSpan = 1;
    //    Header6Cell.RowSpan = 2;
    //    Header6Cell.Font.Size = 10;
    //    Header6Cell.Font.Bold = true;
    //    Header6Cell.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow6.Cells.Add(Header6Cell);
    //    gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

    //    //----Added by vijay andoju 21-07-2020 for showing opening balance

    //    Header6Cell = new TableCell();
    //    Header6Cell.Text = "Quantity";
    //    Header6Cell.ColumnSpan = 1;
    //    Header6Cell.RowSpan = 2;
    //    Header6Cell.Font.Size = 10;
    //    Header6Cell.Font.Bold = true;
    //    Header6Cell.HorizontalAlign = HorizontalAlign.Center;
    //    HeaderGridRow6.Cells.Add(Header6Cell);
    //    gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

    //    TableCell HeaderN = new TableCell();

    //    for (int i = 0; i < arrlist.Count; i++)
    //    {
    //        for (int j = i; j == i; j++)
    //        {
    //            HeaderN = new TableCell();

    //            HeaderN.Text = arrlist[j].ToString();


    //            HeaderN.ColumnSpan = 2;
    //            HeaderN.RowSpan = 1;
    //            HeaderN.Font.Size = 10;
    //            HeaderN.Font.Bold = true;
    //            HeaderN.HorizontalAlign = HorizontalAlign.Center;
    //            HeaderGridRow6.Cells.Add(HeaderN);
    //            gvBudgetReport.Controls[0].Controls.AddAt(6, HeaderGridRow6);

    //        }
    //    }

    //    GridViewRow HeaderGridRow7 = new GridViewRow(7, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    TableCell Header7Cell = new TableCell();

    //    for (int i = 1; i <= arrlist.Count; i++)
    //    {
    //        for (int j = 0; j < Col.Length; j++)
    //        {

    //            TableCell HeaderN1 = new TableCell();
    //            HeaderN1.Text = Col[j].ToString();
    //            HeaderN1.ColumnSpan = 1;
    //            HeaderN1.RowSpan = 1;
    //            HeaderN1.Font.Size = 10;
    //            HeaderN1.Font.Bold = true;
    //            HeaderN1.HorizontalAlign = HorizontalAlign.Center;
    //            HeaderGridRow7.Cells.Add(HeaderN1);
    //            gvBudgetReport.Controls[0].Controls.AddAt(7, HeaderGridRow7);
    //        }
    //    }

    //    #endregion

    //    //----Added by Gopal Anthati 10-12-2020 to get Calculative Taxes Dynamically
    //    #region Dynamic Total
    //    string NetAmount = string.Empty;
    //    DataTable CalculativeDt = objVQtEntry.GetCalculativHeadsForCmpRpt(lstQuot1.SelectedValue).Tables[0];

    //    List<string> CalculativeHead = new List<string>();
    //    List<string> CalculativeHeadNo = new List<string>();
    //    CalculativeHead.Add("Net Amount");
    //    CalculativeHeadNo.Add("0");
    //    for (int i = 0; i < CalculativeDt.Rows.Count; i++)
    //    {
    //        CalculativeHead.Add(CalculativeDt.Rows[i]["FNAME"].ToString());
    //        CalculativeHeadNo.Add(CalculativeDt.Rows[i]["FNO"].ToString());
    //    }

    //    for (int i = 1; i <= CalculativeHead.Count; i++)
    //    {
    //        GridViewRow HeaderGridRow10 = new GridViewRow(11, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell10 = new TableCell();
    //        HeaderCell10 = new TableCell();

    //        HeaderCell10.Text = CalculativeHead[i - 1];
    //        HeaderCell10.ColumnSpan = 3;
    //        HeaderCell10.RowSpan = 1;
    //        HeaderCell10.Font.Size = 11;

    //        HeaderCell10.Font.Bold = true;

    //        HeaderCell10.HorizontalAlign = HorizontalAlign.Left;
    //        HeaderGridRow10.Cells.Add(HeaderCell10);
    //        gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow10);


    //        //for (int j = 1; j <= arrlist.Count * 2; j++)
    //        for (int j = 1; j <= arrlist.Count; j++)
    //        {
    //            for (int k = 1; k <= 2; k++)
    //            {
    //                TableCell HeaderN1 = new TableCell();
    //                DataSet CalculativeChargDt = objVQtEntry.GetCalculativeCharges(lstQuot1.SelectedValue, Convert.ToInt32(alPno[j - 1].ToString()), Convert.ToInt32(CalculativeHeadNo[i - 1]));
    //                if (CalculativeHeadNo[i - 1] == "0")
    //                {
    //                    if (k == 1)
    //                    {
    //                        HeaderN1.Text = "0";
    //                    }
    //                    else
    //                    {
    //                        String GrandTotalAmount = "0";
    //                       // string CalAmt = objCommon.LookUp("STORE_PARTYENTRY A	INNER JOIN  STORE_PARTYFIELDENTRY B ON A.PNO=B.PNO AND A.QUOTNO=B.QUOTNO INNER JOIN  STORE_FIELDMASTER C ON (B.FNO=C.FNO)", "isnull(SUM((CASE TAX_DEDUCTED WHEN 1 THEN -AMT ELSE AMT END)),0) AS AMT", "ADDED_IN_BASIC=0 AND B.FTYPE='C' AND A.QUOTNO='" + lstQuot1.SelectedValue + "' AND B.PNO =" + Convert.ToInt32(alPno[j - 1].ToString())); //08/04/2022

    //                        string CalAmt = objCommon.LookUp("STORE_PARTYENTRY A INNER JOIN  STORE_PARTYFIELDENTRY B ON A.PNO=B.PNO AND A.QUOTNO=B.QUOTNO INNER JOIN  STORE_TAX_MASTER C ON (B.TAXID=C.TAXID)", "TAX_AMOUNT", "A.QUOTNO='" + lstQuot1.SelectedValue + "' AND B.PNO =" + Convert.ToInt32(alPno[j - 1].ToString())); //08/04/2022
    //                        //for (int l = 1; l <= GrandTotal.Count; l++)
    //                        //{
    //                        GrandTotalAmount = GrandTotal[j - 1].ToString();
    //                        //}
    //                        NetAmount = IndianCurrency((Convert.ToDouble(CalAmt) + Convert.ToDouble(GrandTotalAmount)).ToString());

    //                        string[] array = NetAmount.Split('.');
    //                        if (array[1] == "50")
    //                        {
    //                            NetAmount = (Convert.ToDouble(NetAmount) + 0.01).ToString();
    //                        }
    //                        //HeaderN1.Text = IndianCurrency((Math.Round(Convert.ToDouble(CalAmt) + Convert.ToDouble(GrandTotalAmount))).ToString());
    //                        HeaderN1.Text = IndianCurrency(Math.Round(Convert.ToDouble(NetAmount)).ToString());
    //                    }
    //                }
    //                else
    //                {
    //                    if (k == 1)
    //                    {
    //                        HeaderN1.Text = "0";
    //                    }
    //                    else
    //                    {
    //                        if (CalculativeChargDt.Tables[0].Rows.Count > 0)
    //                            HeaderN1.Text = IndianCurrency(CalculativeChargDt.Tables[0].Rows[0]["AMT"].ToString());
    //                        else
    //                            HeaderN1.Text = "0";
    //                    }
    //                }


    //                HeaderN1.ColumnSpan = 1;
    //                HeaderN1.RowSpan = 1;
    //                HeaderN1.Font.Size = 11;
    //                if (i % 2 == 0)
    //                {
    //                    HeaderN1.Font.Bold = false;
    //                }
    //                else
    //                {
    //                    HeaderN1.Font.Bold = true;
    //                }

    //                HeaderN1.HorizontalAlign = HorizontalAlign.Right;
    //                HeaderGridRow10.Cells.Add(HeaderN1);
    //                gvBudgetReport.Controls[0].Controls.AddAt(rows + 2, HeaderGridRow10);
    //            }
    //        }
    //    }
    //    int count = CalculativeDt.Rows.Count;
    //    #endregion

    //    #region Total

    //    string[] Head = { "Grand Total Amount", "Add - GST", "Total Amount", "Less - Discount" };
    //    for (int i = 1; i <= Head.Length; i++)
    //    {
    //        GridViewRow HeaderGridRow9 = new GridViewRow(10, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell9 = new TableCell();
    //        HeaderCell9 = new TableCell();

    //        HeaderCell9.Text = Head[i - 1];
    //        HeaderCell9.ColumnSpan = 3;
    //        HeaderCell9.RowSpan = 1;
    //        HeaderCell9.Font.Size = 11;

    //        HeaderCell9.Font.Bold = true;

    //        HeaderCell9.HorizontalAlign = HorizontalAlign.Left;
    //        HeaderGridRow9.Cells.Add(HeaderCell9);
    //        gvBudgetReport.Controls[0].Controls.AddAt(rows + 0, HeaderGridRow9);
    //        for (int j = 1; j <= arrlist.Count * 2; j++)
    //        {

    //            TableCell HeaderN1 = new TableCell();
    //            if (i == 1)
    //            {
    //                HeaderN1.Text = IndianCurrency(GrandTotalVale[j - 1].ToString());
    //            }
    //            if (i == 2)
    //            {
    //                HeaderN1.Text = IndianCurrency(Tax[j - 1].ToString());
    //            }
    //            if (i == 3)
    //            {
    //                HeaderN1.Text = IndianCurrency(TotalPrice[j - 1].ToString());
    //            }
    //            if (i == 4)
    //            {
    //                HeaderN1.Text = IndianCurrency(Discount[j - 1].ToString());
    //            }

    //            HeaderN1.ColumnSpan = 1;
    //            HeaderN1.RowSpan = 1;
    //            HeaderN1.Font.Size = 11;
    //            if (i % 2 == 0)
    //            {
    //                HeaderN1.Font.Bold = false;
    //            }
    //            else
    //            {
    //                HeaderN1.Font.Bold = true;
    //            }

    //            HeaderN1.HorizontalAlign = HorizontalAlign.Right;
    //            HeaderGridRow9.Cells.Add(HeaderN1);
    //            gvBudgetReport.Controls[0].Controls.AddAt(rows + 1, HeaderGridRow9);
    //        }

    //    }
    //    #endregion

    //    #region SignatureHead
    //    string[] FooterHead = { "Prepared by", "Head of the Department", "Accounts Section", "Principal", "Secretary", "Treasurer" };
    //    GridViewRow HeaderGridRow12 = new GridViewRow(12, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        TableCell HeaderCell11 = new TableCell();
    //        HeaderCell11.Text = "";
    //        HeaderCell11.ColumnSpan = 1;
    //        HeaderCell11.RowSpan = 1;
    //        HeaderCell11.Font.Size = 10;
    //        HeaderCell11.Font.Bold = true;
    //        HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
    //        HeaderGridRow12.Cells.Add(HeaderCell11);
    //        gvBudgetReport.Controls[0].Controls.AddAt(rows + (6 + count), HeaderGridRow12);
    //    }
    //    GridViewRow HeaderGridRow11 = new GridViewRow(13, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //    for (int i = 0; i < 5; i++)
    //    {
    //        TableCell HeaderCell11 = new TableCell();
    //        HeaderCell11.Text = FooterHead[i].ToString();
    //        HeaderCell11.ColumnSpan = 1;
    //        HeaderCell11.RowSpan = 2;
    //        HeaderCell11.Font.Size = 10;
    //        HeaderCell11.Font.Bold = true;
    //        HeaderCell11.HorizontalAlign = HorizontalAlign.Center;
    //        HeaderGridRow11.Cells.Add(HeaderCell11);
    //        gvBudgetReport.Controls[0].Controls.AddAt(rows + (7 + count), HeaderGridRow11);
    //    }
    //    #endregion

    //    gvBudgetReport.FooterStyle.Font.Bold = true;
    //    gvBudgetReport.FooterStyle.Font.Size = 19;
    //}
    //private DataTable Datateble(DataTable dtvendor)
    //{


    //    //Added by vijay andoju for Creating Table 
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Sl.No", typeof(string));
    //    dt.Columns.Add("Description", typeof(string));
    //    dt.Columns.Add("Quantity", typeof(string));
    //    //---------------------------------------------


    //    //Creating Column names for Vendor wise
    //    for (int i = 0; i < dtvendor.Rows.Count; i++)
    //    {
    //        dt.Columns.Add("Price/Unit" + i, typeof(string));
    //        dt.Columns.Add("Value" + i, typeof(string));
    //        arrlist.Add(dtvendor.Rows[i]["PNAME"].ToString());
    //        alPno.Add(dtvendor.Rows[i]["PNO"].ToString());
    //    }
    //    //---------------------------------------------------


    //    //Inserting the Itemname Qty in table

    //    DataTable QuotItemDt = objVQtEntry.GetItemsByQuotNo(lstQuot1.SelectedValue, Convert.ToInt32(0)).Tables[0];
    //    for (int j = 0; j < QuotItemDt.Rows.Count; j++)
    //    {
    //        DataRow Row1;
    //        Row1 = dt.NewRow();
    //        Row1["Sl.No"] = j + 1;
    //        Row1["Description"] = QuotItemDt.Rows[j]["Item_name"].ToString();
    //        Row1["Quantity"] = QuotItemDt.Rows[j]["Qty"].ToString();
    //        dt.Rows.Add(Row1);


    //        for (int k = 0; k < dtvendor.Rows.Count; k++)
    //        {
    //            DataTable RateByVendorandItemDt = objVQtEntry.GetItemsForVendor(lstQuot1.SelectedValue, Convert.ToInt32(dtvendor.Rows[k]["PNO"].ToString()), Convert.ToInt32(QuotItemDt.Rows[j]["ITEM_NO"].ToString())).Tables[0];
    //            if (RateByVendorandItemDt.Rows.Count > 0)
    //            {
    //                dt.Rows[j]["Price/Unit" + k] = RateByVendorandItemDt.Rows[0]["PRICE"].ToString();
    //                dt.Rows[j]["Value" + k] = Convert.ToDouble(QuotItemDt.Rows[j]["Qty"]) * Convert.ToDouble(RateByVendorandItemDt.Rows[0]["PRICE"]);
    //                // dt.Rows[j]["Value" + k] = RateByVendorandItemDt.Rows[0]["Grand"].ToString();
    //                dt.Rows[j].AcceptChanges();
    //            }
    //            else
    //            {
    //                dt.Rows[j]["Price/Unit" + k] = "0";
    //                dt.Rows[j]["Value" + k] = "0";
    //                dt.Rows[j].AcceptChanges();
    //            }
    //        }
    //    }
    //    //--------------------------------------------------------------------------------------------------
    //    int RoCount1 = dt.Rows.Count - 1;


    //    //added by vijay andoju 21102020 for calculation perpose like TotalAmount,SubTotalAmount,TaxAmount,DiscountAmount,GrandTotal
    //    for (int n = 0; n < arrlist.Count; n++)
    //    {

    //        Discount.Add("0");
    //        Discount.Add(Convert.ToDecimal(objCommon.LookUp("STORE_PARTYITEMENTRY", "ISNULL(SUM(DISCOUNT_AMOUNT),0)DISAMT", " PNO=" + Convert.ToInt32(alPno[n].ToString()) + " AND QUOTNO='" + lstQuot1.SelectedValue + "'")).ToString());

    //        TotalPrice.Add(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price/Unit" + n] == DBNull.Value ? 0 : x["Price/Unit" + n])));
    //        TotalPrice.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n])))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()));


    //        DataTable NetExtraCharge = null;//objVQtEntry.GetExtraCharge(lstQuot1.SelectedValue, Convert.ToInt32(alPno[n].ToString())).Tables[0];
    //        if (NetExtraCharge.Rows.Count > 0)
    //        {
    //            Tax.Add("0");
    //            //Tax.Add(Convert.ToDecimal(Convert.ToDecimal(TotalPrice[TotalPrice.Count - 1]) * Convert.ToDecimal(NetExtraCharge.Rows[0]["TOTVAT"])) / 100);
    //            Tax.Add(NetExtraCharge.Rows[0]["TOTVAT"].ToString());
    //        }
    //        else
    //        {
    //            Tax.Add("0");
    //            Tax.Add("0");
    //        }
    //        GrandTotalVale.Add(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Price/Unit" + n] == DBNull.Value ? 0 : x["Price/Unit" + n]))));
    //        GrandTotalVale.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n]))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()) + Convert.ToDecimal(Tax[Tax.Count - 1])).ToString());

    //        GrandTotal.Add(Convert.ToDecimal(Convert.ToDecimal(dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["Value" + n] == DBNull.Value ? 0 : x["Value" + n]))) - Convert.ToDecimal(Discount[Discount.Count - 1].ToString()) + Convert.ToDecimal(Tax[Tax.Count - 1])).ToString());

    //    }

    //    //--------------------------------------------------------------------------------------------------------

    //    return dt;

    //}
    //private string IndianCurrency(string AMOUNT)
    //{

    //    decimal Amount = decimal.Parse(AMOUNT, CultureInfo.InvariantCulture);
    //    CultureInfo hindi = new CultureInfo("en-IN");
    //    string text = Amount.ToString("N2", hindi);
    //    return text;
    //}

    #endregion

    ////Display Jquery Message Window.
    //void DisplayMessage(string Message)
    //{
    //    string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
    //    string message = string.Format(prompt, Message);
    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    //}


    protected void btnShowComp_Click(object sender, EventArgs e)
    {
        divQuotlist.Visible = false;
        divLastDate.Visible = false;
        pnlitems.Visible = false;
        divItemEntryList.Visible = false;
        btnSubmit.Visible = false;
        pnlCmpst.Visible = true;
        btnBack.Visible = true;
        btnShowComp.Visible = false;
        btncmpall.Visible = true;
        divItemList.Visible = false;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divQuotlist.Visible = true;
        divLastDate.Visible = true;
        //pnlitems.Visible = true;      
        pnlCmpst.Visible = false;
        btnBack.Visible = false;
        btnShowComp.Visible = true;
        btncmpall.Visible = false;
        lstQtNo.SelectedIndex = -1;
        lstVendor.SelectedIndex = -1;
        grdItemList.DataSource = null;
        grdItemList.DataBind();
        btnSubmit.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstVendor.SelectedIndex >= 0)
            {
                //double BillAmount = 0.0;
                //for (int i = 0; i < grdItemList.Rows.Count; i++)
                //{
                //    TextBox txtRate = (TextBox)grdItemList.Rows[i].FindControl("txtRate");
                //    TextBox hdnItemTotalAmt = (TextBox)grdItemList.Rows[i].FindControl("hdnItemTotalAmt");   //12/05/2021
                //    if(txtRate.Text == ""|| Convert.ToDouble(txtRate.Text) < 1)
                //    {
                //        Showmessage("Please Enter Valid Rate For Each Item");
                //        return;
                //    }
                //    BillAmount = BillAmount + Convert.ToDouble(hdnItemTotalAmt.Text);    //12/05/2022
                //}

                for (int i = 0; i < grdItemList.Rows.Count; i++)
                {

                    //int PINO = (int)grdItemList.DataKeys[i].Value;
                    //objPIEntry.PINO = Convert.ToInt32(ViewState["PINO"]);

                    objPIEntry.ITEM_NO = (int)grdItemList.DataKeys[i].Value;
                    int itemno = (int)grdItemList.DataKeys[i].Value;
                    //Label lblUnit = (Label)grdItemList.Rows[i].FindControl("lblUnit");                        
                    Label lblqty = (Label)grdItemList.Rows[i].FindControl("lblQty");
                    TextBox txtRate = (TextBox)grdItemList.Rows[i].FindControl("txtRate");
                    HiddenField hdnPINO = (HiddenField)grdItemList.Rows[i].FindControl("hdnPINO");

                    HiddenField hdnOthItemRemark = (HiddenField)grdItemList.Rows[i].FindControl("hdnOthItemRemark");
                    HiddenField hdnTechSpec = (HiddenField)grdItemList.Rows[i].FindControl("hdnTechSpec");
                    HiddenField hdnQualityQtySpec = (HiddenField)grdItemList.Rows[i].FindControl("hdnQualityQtySpec");

                    HiddenField hdnItemDiscPer = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemDiscPer");
                    HiddenField hdnItemDiscAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemDiscAmt");
                    HiddenField hdnItemTaxableAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTaxableAmt");
                    HiddenField hdnItemTaxAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTaxAmt");
                    HiddenField hdnItemTotalAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTotalAmt");

                    objPIEntry.ITEM_REMARK = hdnOthItemRemark.Value;
                    objPIEntry.TECHSPECH = hdnTechSpec.Value;
                    objPIEntry.QUALITY_QTY_SPEC = hdnQualityQtySpec.Value;

                    objPIEntry.PINO = Convert.ToInt32(hdnPINO.Value);
                    objPIEntry.QTY = Convert.ToInt32(lblqty.Text);
                    objPIEntry.TAXABLE_AMT = Convert.ToDecimal(hdnItemTaxableAmt.Value);
                    objPIEntry.TAX_AMT = Convert.ToDecimal(hdnItemTaxAmt.Value);
                    //objPIEntry.UNIT = lblUnit.Text;

                    objPIEntry.PRICE = Convert.ToDecimal(txtRate.Text);

                    objPIEntry.DISCOUNT = hdnItemDiscPer.Value == "" ? 0 : Convert.ToDecimal(hdnItemDiscPer.Value);
                    objPIEntry.DISCOUNTAMOUNT = hdnItemDiscAmt.Value == "" ? 0 : Convert.ToDecimal(hdnItemDiscAmt.Value);
                    objPIEntry.TOTAMOUNT = Convert.ToDecimal(hdnItemTotalAmt.Value);
                    //objPIEntry.TOTAMOUNT = (objPIEntry.QTY * objPIEntry.PRICE) - objPIEntry.DISCOUNTAMOUNT;
                    ViewState["TotAmount"] = objPIEntry.TOTAMOUNT;

                    objPIEntry.FLAG = "S";
                    objPIEntry.EDATE = DateTime.Now.Date;
                    objPIEntry.QUOTNO = lstQtNo.SelectedValue;
                    objPIEntry.MDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
                    objPIEntry.PNO = Convert.ToInt32(lstVendor.SelectedValue);

                    int ret = Convert.ToInt32(objCommon.LookUp("STORE_PARTYITEMENTRY", "count(*)", "item_no =" + objPIEntry.ITEM_NO + "and quotno='" + objPIEntry.QUOTNO + "' and pno=" + objPIEntry.PNO));
                    if (ret == 0)
                    {
                        objVQtEntry.SavePartyItemsEntry(objPIEntry, Session["colcode"].ToString());
                        Showmessage("Vendor Entry Saved Successfully");
                    }
                    else
                    {
                        //if (Convert.ToInt32(hdnPINO.Value) == 0)  //22/04/2022
                        //{
                        //    Showmessage("Please Select Again Quotation No. And Vendor From List.");
                        //    return;
                        //}
                        objVQtEntry.UpdatePartyItemsEntry(objPIEntry, Session["colcode"].ToString());
                        Showmessage("Vendor Entry Updated Successfully");
                    }
                    // this.SavePartyFields(objPIEntry.PNO, Session["colcode"].ToString(), objPIEntry.ITEM_NO);
                }
                objPFEntry.QUOTNO = lstQtNo.SelectedValue;
                objPFEntry.PNO = Convert.ToInt32(lstVendor.SelectedValue);
                //if (ViewState["TaxTable"] != null)
                objPFEntry.VENDOR_TAX_TBL = ViewState["TaxTable"] as DataTable;
                objVQtEntry.SavePartyFieldEntry(objPFEntry, Session["colcode"].ToString());
                //this.BindItemList(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
                BindQuotforComp();
            }
            else
            {
                Showmessage("Please Select Vendor.");
            }
            //BindQuotation();
            ClearItem();
            // lstQtNo.SelectedIndex = -1;
            //lstVendor.SelectedIndex = -1;
            // lstQtNo.ClearSelection();
            //lstVendor.ClearSelection();
            lstVendor.DataSource = null;
            lstVendor.DataBind();
            this.BindQuotation();
            this.BindVendorByQuotno("0");

            grdItemList.DataSource = null;
            grdItemList.DataBind();
            pnlitems.Visible = false;
            ViewState["TaxTable"] = null;
            ViewState["Action"] = "add";
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, ex.Message);
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Img = sender as ImageButton;
        int Itemno = Convert.ToInt32(Img.CommandArgument);
        objVQtEntry.DeletePartyItemEntry(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Itemno);
        this.BindItemList(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
        Showmessage("Record Deleted Successfully");
        BindQuotforComp();
    }



    protected void btnAddTax_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < grdItemList.Rows.Count; i++)
        {
            TextBox txtTaxableAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxableAmt");
            TextBox txtTotalAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTotalAmt");
            TextBox txtDiscAmt = (TextBox)grdItemList.Rows[i].FindControl("txtDiscAmt");
            HiddenField hdnItemTotalAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTotalAmt");
            HiddenField hdnItemTaxableAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTaxableAmt");
            HiddenField hdnItemDiscAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemDiscAmt");
            txtTaxableAmt.Text = hdnItemTaxableAmt.Value;
            txtTotalAmt.Text = hdnItemTotalAmt.Value;
            txtDiscAmt.Text = hdnItemDiscAmt.Value;
        }

        //DataTable dtTaxdup = null;
        ViewState["ItemNo"] = null;
        ImageButton btn = sender as ImageButton;
        ViewState["ItemNo"] = Convert.ToInt32(btn.CommandArgument);

        if (ViewState["TaxTable"] != null)
        {
            //dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                //BindTaxes();
                DataSet ds = null;
                int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO=" + lstVendor.SelectedValue));
                int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
                if (VendorState == CollegeState)
                {
                    ds = objVQtEntry.GetTaxes(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
                }
                else
                {
                    ds = objVQtEntry.GetTaxes(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
                }
                lvTax.DataSource = ds.Tables[0];
                lvTax.DataBind();
                hdnListCount.Value = ds.Tables[0].Rows.Count.ToString();
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                CalTotTax();
                //if (ViewState["Action"].ToString() == "edit")
                //{
                //    DataTable dt = foundRow.CopyToDataTable();
                //    lvTax.DataSource = dt;
                //    lvTax.DataBind();
                //    hdnListCount.Value = dtTaxdup.Rows.Count.ToString();
                //    this.MdlTax.Show();
                //    divOthPopup.Visible = false;
                //    divTaxPopup.Visible = true;
                //    //ViewState["TaxEdit"]="edit";
                //    CalTotTax();
                //}
                //else
                //{
                //    BindTaxes();
                //}

            }
            else
            {
                BindTaxes();
            }

        }
        else
        {
            BindTaxes();
        }

        //------------------------Addedby shabina----16/12/2022----For Tchecking Tax is Percentage wise or not--------------------------//
        DataSet dss = objCommon.FillDropDown("STORE_TAX_MASTER", "TAXID", "TAX_NAME,isnull(IS_PER,0) as IS_PER", "TAXID in (Select TAXID from STORE_TAX_ITEM_MAP where item_no=" + Convert.ToInt32(ViewState["ItemNo"]) + ")", "");
        // int IsTaxInPercent = Convert.ToInt32(objCommon.LookUp("STORE_TAX_MASTER", "isnull(IS_PER,0)", "TAXID in (Select TAXID from STORE_TAX_ITEM_MAP where item_no="+Convert.ToInt32(ViewState["ItemNo"])+")");
        for (int ii = 0; ii < dss.Tables[0].Rows.Count; ii++)
        {

            if (dss.Tables[0].Rows[ii]["IS_PER"].ToString() == "0")
            {
                foreach (ListViewItem i in lvTax.Items)
                {
                    TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                    lblTaxAmount.Enabled = true;
                }
            }
            else
            {
                foreach (ListViewItem i in lvTax.Items)
                {
                    TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                    lblTaxAmount.Enabled = false;
                }
            }
        }
        //------------------------Addedby shabina--16/12/2022--------------------------------//


    }
    private void BindTaxes()
    {
        DataSet ds = null;
        int VendorState = Convert.ToInt32(objCommon.LookUp("STORE_PARTY", "STATENO", "PNO=" + lstVendor.SelectedValue));
        int CollegeState = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "STATENO", ""));
        if (VendorState == CollegeState)
        {
            ds = objVQtEntry.GetTaxes(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 1);
        }
        else
        {
            ds = objVQtEntry.GetTaxes(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToDecimal(hdnTaxableAmt.Value), Convert.ToDecimal(hdnBasicAmt.Value), Convert.ToInt32(ViewState["ItemNo"]), 0);
        }
        if (ViewState["TaxTable"] != null)
        {
            DataTable dtTaxAdd = (DataTable)ViewState["TaxTable"];
            if (ds.Tables[0].Rows.Count > 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    int maxVal = 0;

                    DataRow datarow = null;
                    datarow = dtTaxAdd.NewRow();
                    if (datarow != null)
                    {
                        maxVal = Convert.ToInt32(dtTaxAdd.AsEnumerable().Max(row => row["TAX_SRNO"]));
                    }
                    datarow["TAX_SRNO"] = ds.Tables[0].Rows[i]["TAX_SRNO"].ToString();//maxVal + 1;
                    datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                    datarow["TAXID"] = ds.Tables[0].Rows[i]["TAXID"].ToString();
                    datarow["TAX_NAME"] = ds.Tables[0].Rows[i]["TAX_NAME"].ToString();
                    datarow["TAX_AMOUNT"] = ds.Tables[0].Rows[i]["TAX_AMOUNT"].ToString();
                    ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                    dtTaxAdd.Rows.Add(datarow);
                }
                ViewState["TaxTable"] = dtTaxAdd;
                DataRow[] foundRow = dtTaxAdd.Select("ITEM_NO=" + ViewState["ItemNo"]);
                if (foundRow.Length > 0)
                {
                    DataTable dtTax = foundRow.CopyToDataTable();
                    lvTax.DataSource = dtTax;
                    lvTax.DataBind();
                    hdnListCount.Value = dtTax.Rows.Count.ToString();
                    ViewState["Action"] = "edit";
                    hdnOthEdit.Value = "1";

                    this.MdlTax.Show();
                    divOthPopup.Visible = false;
                    divTaxPopup.Visible = true;
                    CalTotTax();
                }
            }
            else
            {
                lvTax.DataSource = null;
                lvTax.DataBind();
                this.MdlTax.Hide();
                Showmessage("No Taxes Are Applicable For This Item.");
                return;
            }
            // AddTaxTable(dtTaxdup);

        }
        else if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtTax = (DataTable)ds.Tables[0];
            DataRow[] foundRow = dtTax.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                DataTable dtTaxAdd = foundRow.CopyToDataTable();
                lvTax.DataSource = dtTaxAdd;
                lvTax.DataBind();
                hdnListCount.Value = dtTaxAdd.Rows.Count.ToString();
                ViewState["Action"] = "edit";
                hdnOthEdit.Value = "1";
                this.MdlTax.Show();
                divOthPopup.Visible = false;
                divTaxPopup.Visible = true;
                CalTotTax();
            }
        }
        else
        {
            lvTax.DataSource = null;
            lvTax.DataBind();
            this.MdlTax.Hide();
            Showmessage("No Taxes Are Applicable For This Item.");
            return;
        }

    }

    private void CalTotTax()
    {
        decimal TotTaxAmt = 0;
        foreach (ListViewItem i in lvTax.Items)
        {
            TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
            if (lblTaxAmount.Text != "")
                TotTaxAmt += Convert.ToDecimal(lblTaxAmount.Text);
        }
        txtTotTaxAmt.Text = TotTaxAmt.ToString("00.00");
    }

    private void AddTaxTable(DataTable dtTaxds)
    {
        //DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
        //DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
        //if (foundRow.Length > 0)
        //{
        //    foreach (DataRow drow in foundRow)
        //        dtTaxdup.Rows.Remove(drow);
        //}
        //foreach (ListViewItem i in lvTax.Items)

        for (int i = 0; i < dtTaxds.Rows.Count; i++)
        {
            int maxVal = 0;
            DataTable dtTax = (DataTable)ViewState["TaxTable"];
            DataRow datarow = null;
            datarow = dtTax.NewRow();
            if (datarow != null)
            {
                maxVal = Convert.ToInt32(dtTax.AsEnumerable().Max(row => row["TAX_SRNO"]));
            }
            datarow["TAX_SRNO"] = maxVal + 1;
            datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
            datarow["TAXID"] = dtTaxds.Rows[0]["TAXID"].ToString();
            datarow["TAX_NAME"] = dtTaxds.Rows[0]["TAX_NAME"].ToString();
            datarow["TAX_AMOUNT"] = dtTaxds.Rows[0]["TAX_AMOUNT"].ToString();
            ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
            dtTax.Rows.Add(datarow);
            ViewState["TaxTable"] = dtTax;
        }

    }

    protected void btnSaveTax_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < grdItemList.Rows.Count; i++)
        {
            TextBox txtTaxAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxAmt");
            TextBox txtTotalAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTotalAmt");

            HiddenField hdnItemTaxAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTaxAmt");
            HiddenField hdnItemTotalAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTotalAmt");

            txtTaxAmt.Text = hdnItemTaxAmt.Value;
            txtTotalAmt.Text = hdnItemTotalAmt.Value;

        }
        //if (ViewState["TaxEdit"] == null)
        // {
        if (ViewState["TaxTable"] != null && ((DataTable)ViewState["TaxTable"]) != null)
        {
            DataTable dtTaxdup = (DataTable)ViewState["TaxTable"];
            DataRow[] foundRow = dtTaxdup.Select("ITEM_NO=" + ViewState["ItemNo"]);
            if (foundRow.Length > 0)
            {
                foreach (DataRow drow in foundRow)
                    dtTaxdup.Rows.Remove(drow);
            }
            DataTable dtTax = (DataTable)ViewState["TaxTable"];
            DataTable dtCount = (DataTable)ViewState["TaxTable"];
            int SrnoCount = dtCount.Rows.Count;
            int count = 0;
            int maxVal = 0;
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;

                DataRow datarow = null;
                datarow = dtTax.NewRow();

                if (SrnoCount > 0)
                {
                    datarow["TAX_SRNO"] = maxVal + 1; //dtTax.Rows[count]["TAX_SRNO"].ToString();
                    maxVal++;
                }
                else
                {
                    if (datarow != null)
                    {
                        maxVal = Convert.ToInt32(dtTax.AsEnumerable().Max(row => row["TAX_SRNO"]));
                    }
                    datarow["TAX_SRNO"] = maxVal + 1;
                }

                datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                datarow["TAXID"] = hdnTaxId.Value;
                datarow["TAX_NAME"] = lblTaxName.Text;
                datarow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(datarow);
                count++;
            }
            ViewState["TaxTable"] = dtTax;
        }
        else
        {
            DataTable dtTax = this.CreateTaxTable();
            DataRow datarow = null;
            foreach (ListViewItem i in lvTax.Items)
            {
                HiddenField hdnTaxId = i.FindControl("hdnTaxId") as HiddenField;
                TextBox lblTaxAmount = i.FindControl("lblTaxAmount") as TextBox;
                Label lblTaxName = i.FindControl("lblTaxName") as Label;
                datarow = dtTax.NewRow();

                datarow["TAX_SRNO"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                datarow["ITEM_NO"] = ViewState["ItemNo"].ToString();
                datarow["TAXID"] = hdnTaxId.Value;
                datarow["TAX_NAME"] = lblTaxName.Text;
                datarow["TAX_AMOUNT"] = lblTaxAmount.Text == "" ? "0" : lblTaxAmount.Text;
                ViewState["SRNO_TAX"] = Convert.ToInt32(ViewState["SRNO_TAX"]) + 1;
                dtTax.Rows.Add(datarow);
                ViewState["TaxTable"] = dtTax;
            }
        }
        // }
        // else
        // {
        // }
        txtTotTaxAmt.Text = string.Empty;
    }

    private DataTable CreateTaxTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("TAX_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(int)));
        dt.Columns.Add(new DataColumn("TAX_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TAX_AMOUNT", typeof(decimal)));
        return dt;
    }

    protected void btnAddOthInfo_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < grdItemList.Rows.Count; i++)
        {
            TextBox txtTaxableAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxableAmt");
            TextBox txtTotalAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTotalAmt");
            TextBox txtDiscAmt = (TextBox)grdItemList.Rows[i].FindControl("txtDiscAmt");
            HiddenField hdnItemTotalAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTotalAmt");
            HiddenField hdnItemTaxableAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTaxableAmt");
            HiddenField hdnItemDiscAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemDiscAmt");
            txtTaxableAmt.Text = hdnItemTaxableAmt.Value;
            txtTotalAmt.Text = hdnItemTotalAmt.Value;
            txtDiscAmt.Text = hdnItemDiscAmt.Value;
        }

        this.MdlTax.Show();
        divOthPopup.Visible = true;
        divTaxPopup.Visible = false;
        if (ViewState["Action"].ToString() == "edit" && hdnOthEdit.Value == "1")
        {
            ImageButton btn = sender as ImageButton;
            int ItemNo = Convert.ToInt32(btn.CommandArgument);
            DataSet ds = objCommon.FillDropDown("STORE_PARTYITEMENTRY", "ITEM_REMARK,TECH_SPEC", "QUALITY_QTY_SPEC", "QUOTNO='" + lstQtNo.SelectedValue + "' AND PNO=" + Convert.ToInt32(lstVendor.SelectedValue) + " AND ITEM_NO=" + ItemNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemRemarkOth.Text = ds.Tables[0].Rows[0]["ITEM_REMARK"].ToString();
                txtQualityQtySpec.Text = ds.Tables[0].Rows[0]["QUALITY_QTY_SPEC"].ToString();
                txtTechSpec.Text = ds.Tables[0].Rows[0]["TECH_SPEC"].ToString();
            }
        }
    }

    protected void btnSaveOthInfo_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < grdItemList.Rows.Count; i++)
        {
            TextBox txtTaxAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTaxAmt");
            TextBox txtTotalAmt = (TextBox)grdItemList.Rows[i].FindControl("txtTotalAmt");

            HiddenField hdnItemTaxAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTaxAmt");
            HiddenField hdnItemTotalAmt = (HiddenField)grdItemList.Rows[i].FindControl("hdnItemTotalAmt");

            txtTaxAmt.Text = hdnItemTaxAmt.Value;
            txtTotalAmt.Text = hdnItemTotalAmt.Value;

        }

        this.MdlTax.Hide();
        txtItemRemarkOth.Text = string.Empty;
        txtQualityQtySpec.Text = string.Empty;
        txtTechSpec.Text = string.Empty;

    }
}



