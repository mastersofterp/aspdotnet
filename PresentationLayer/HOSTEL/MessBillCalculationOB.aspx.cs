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
using System.IO;

public partial class HOSTEL_MessBillCalculationOB : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    MessExpenditureController objMBC = new MessExpenditureController();
    MessExpenditureMaster objME = new MessExpenditureMaster();

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
                    // Check User Authority 
                    //this.CheckPageAuthorization();
                    FillDropDown();
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "MessBillCalculation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MessBillCalculation.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MessBillCalculation.aspx");
        }
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
        objCommon.FillDropDownList(ddlMess, "ACD_HOSTEL_MESS", "MESS_NO", "MESS_NAME", "MESS_NO>0", "MESS_NO");
        
        for (int i=1; i <= 12; i++)
        { 
            DateTime date=new DateTime(1900,i,1);
            ddlMonth.Items.Add(new ListItem(date.ToString("MMMM"), i.ToString()));
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        btnSubmit.Enabled = false;
        DataSet ds = null;
        ds = objMBC.GetMessBillBalance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlMess.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue),Convert.ToInt32(ddlHostel.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            
            lvStudents.Visible = true;
            lvStudents.DataSource = ds;
            lvStudents.DataBind();
            btnSubmit.Enabled = true;
            //if (ds.Tables[0].Rows[0]["TOTAL_EXPENDITURE"].ToString() == "")
            //    objCommon.DisplayMessage(pnlFeeTable, "Mess Expenditure not defined for Month " + ddlMonth.SelectedItem.Text,this.Page);
        }
        else
        {
            lvStudents.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;

        }

        string totStud = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_DCR B ON (B.IDNO=A.RESIDENT_NO AND B.SESSIONNO=A.HOSTEL_SESSION_NO)", "COUNT(distinct idno)", "HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + " AND RECIEPT_CODE='MF' and recon=1 AND A.CAN=0 AND B.CAN=0");
        string totExpend = objCommon.LookUp("ACD_HOSTEL_MESS_EXPENDITURE", "ISNULL(SUM(f1),0)TOTAL_EXPENDITURE", "SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " and (MONTH([MONTH])=" + Convert.ToInt32(ddlMonth.SelectedValue) + " or " + Convert.ToInt32(ddlMonth.SelectedValue) + "=0)");

        // no. of days
       // int days=DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(ddlMonth.SelectedValue));

        
        //From date to date
        DateTime fdate = Convert.ToDateTime(txtFromDate.Text.Trim());
        DateTime tdate = Convert.ToDateTime(txtToDate.Text.Trim());
        int dayno = (tdate.Date - fdate.Date).Days;

        hdfDay.Value = dayno.ToString();

        //txtPerDay.Text = Convert.ToString(Math.Round((Convert.ToDecimal(totExpend.ToString()) / Convert.ToDecimal(totStud.ToString())) / dayno, 2));
       

        txtTotalStud.Text = totStud;

        //BindExpendListView();
        //Calculate();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int output = 0;
            objME.Session_no = Convert.ToInt32(ddlSession.SelectedValue);
            objME.Hostel_no = Convert.ToInt32(ddlHostel.SelectedValue);
            objME.Mess_no = Convert.ToInt32(ddlMess.SelectedValue);
            objME.Month_no = Convert.ToInt32(ddlMonth.SelectedValue);
            objME.Ua_no = Convert.ToInt32(Session["userno"].ToString());
            objME.COLLEGE_CODE = Session["colcode"].ToString();
            objME.Mdate = DateTime.Now;
            objME.Bill_date = Convert.ToDateTime(txtBillDate.Text.Trim());
            objME.AUDITDATE = DateTime.Now;
            objME.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                Label lblName = item.FindControl("lblName") as Label;
                TextBox txtDays = item.FindControl("txtDays") as TextBox;
                TextBox txtExpenditure = item.FindControl("txtExpenditure") as TextBox;
                TextBox txtBalance = item.FindControl("txtBalance") as TextBox;
                TextBox txtF1 = item.FindControl("txtF1") as TextBox;
                TextBox txtF2 = item.FindControl("txtF2") as TextBox;
                TextBox txtF3 = item.FindControl("txtF3") as TextBox;
                TextBox txtF4 = item.FindControl("txtF4") as TextBox;
                TextBox txtF5 = item.FindControl("txtF5") as TextBox;
                TextBox txtF6 = item.FindControl("txtF6") as TextBox;
                TextBox txtF7 = item.FindControl("txtF7") as TextBox;
                TextBox txtF8 = item.FindControl("txtF8") as TextBox;
                TextBox txtF9 = item.FindControl("txtF9") as TextBox;
                TextBox txtF10 = item.FindControl("txtF10") as TextBox;
                objME.Idno = Convert.ToInt32(lblName.ToolTip);
                if (txtExpenditure.Text.Trim() != "") objME.Total_expence = Convert.ToDecimal(txtExpenditure.Text); else objME.Total_expence = 0.0m;
                if (txtDays.Text.Trim()!="")   objME.Attend_days = Convert.ToInt32(txtDays.Text);
                if (txtBalance.Text.Trim() != "") objME.Total_balance = Convert.ToDecimal(txtBalance.Text); else objME.Total_balance = 0.0m;
                if (txtF1.Text.Trim() != "") objME.F1 = Convert.ToDouble(txtF1.Text.Trim()); else objME.F1 = 0.0;
                if (txtF2.Text.Trim() != "") objME.F2 = Convert.ToDouble(txtF2.Text.Trim()); else objME.F2 = 0.0;
                if (txtF3.Text.Trim() != "") objME.F3 = Convert.ToDouble(txtF3.Text.Trim()); else objME.F3 = 0.0;
                if (txtF4.Text.Trim() != "") objME.F4 = Convert.ToDouble(txtF4.Text.Trim()); else objME.F4 = 0.0;
                if (txtF5.Text.Trim() != "") objME.F5 = Convert.ToDouble(txtF5.Text.Trim()); else objME.F5 = 0.0;
                if (txtF6.Text.Trim() != "") objME.F6 = Convert.ToDouble(txtF6.Text.Trim()); else objME.F6 = 0.0;
                if (txtF7.Text.Trim() != "") objME.F7 = Convert.ToDouble(txtF7.Text.Trim()); else objME.F7 = 0.0;
                if (txtF8.Text.Trim() != "") objME.F8 = Convert.ToDouble(txtF8.Text.Trim()); else objME.F8 = 0.0;
                if (txtF9.Text.Trim() != "") objME.F9 = Convert.ToDouble(txtF9.Text.Trim()); else objME.F9 = 0.0;
                if (txtF10.Text.Trim() != "") objME.F10 = Convert.ToDouble(txtF10.Text.Trim()); else objME.F10 = 0.0;
                //decimal guestAmt = 0.00m;
                //if (txtGuestAmt.Text != "0.00" && txtGuestAmt.Text != "") guestAmt = Convert.ToDecimal(txtGuestAmt.Text);
                output = objMBC.AddMessBillStudent(objME);
            }
            if (output == 1)
            {
                int op = objMBC.AddMessBillTrans(objME, Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
                objCommon.DisplayMessage(pnlFeeTable, "Record Saved Successfully!!", this.Page);
                btnSubmit.Enabled = false;

            }
            else
                objCommon.DisplayMessage(pnlFeeTable, "Transcation Failed", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MessBillCalculation.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void lvStudents_DataBound(object sender, EventArgs e)
    //{
        // // Other Heads
        //DataSet ds1 = null;
        //int i = 0;
        //ds1 = objCommon.FillDropDown("ACD_HOSTEL_MESS_BILL_HEAD", "MESS_BILL_NO", "MESS_BILL_HEAD,MESS_BILL_SHORTNAME,MESS_BILL_LONGNAME,MESS_HEAD_NO", "MESS_BILL_LONGNAME<>''", "MESS_BILL_NO");
        //if (ds1.Tables[0].Rows.Count > 0)
        //{
        //    if (lvStudents.Visible == false)
        //        return;
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF1 = (sender as ListView).FindControl("lblF1") as Label;
        //        lblF1.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF1.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF1 = item.FindControl("txtF1") as TextBox;
        //        //    txtF1.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF2 = (sender as ListView).FindControl("lblF2") as Label;
        //        lblF2.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF2.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF2 = item.FindControl("txtF2") as TextBox;
        //        //    txtF2.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF3 = (sender as ListView).FindControl("lblF3") as Label;
        //        lblF3.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF3.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF3 = item.FindControl("txtF3") as TextBox;
        //        //    txtF3.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF4 = (sender as ListView).FindControl("lblF4") as Label;
        //        lblF4.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF4.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF1 = item.FindControl("txtF4") as TextBox;
        //        //    txtF1.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF5 = (sender as ListView).FindControl("lblF5") as Label;
        //        lblF5.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF5.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF5 = item.FindControl("txtF5") as TextBox;
        //        //    txtF5.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF6 = (sender as ListView).FindControl("lblF6") as Label;
        //        lblF6.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF6.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF6 = item.FindControl("txtF6") as TextBox;
        //        //    txtF6.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF7 = (sender as ListView).FindControl("lblF7") as Label;
        //        lblF7.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF7.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF7 = item.FindControl("txtF7") as TextBox;
        //        //    txtF7.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF8 = (sender as ListView).FindControl("lblF8") as Label;
        //        lblF8.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF8.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF8 = item.FindControl("txtF8") as TextBox;
        //        //    txtF8.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF9 = (sender as ListView).FindControl("lblF9") as Label;
        //        lblF9.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF9.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF9 = item.FindControl("txtF9") as TextBox;
        //        //    txtF9.Visible = true;
        //        //}
        //    }
        //    if (ds1.Tables[0].Rows.Count > i)
        //    {
        //        Label lblF10 = (sender as ListView).FindControl("lblF10") as Label;
        //        lblF10.Text = ds1.Tables[0].Rows[i]["MESS_BILL_LONGNAME"].ToString();
        //        i++;
        //        //lblF10.Visible = true;
        //        //foreach (ListViewDataItem item in lvStudents.Items)
        //        //{
        //        //    TextBox txtF10 = item.FindControl("txtF10") as TextBox;
        //        //    txtF10.Visible = true;
        //        //}
        //    }
            
        //}
    //}

    //Expenditure 
    public void BindExpendListView()
    {
        DataSet ds = new DataSet();
        string no = "0";
        ds = objMBC.GetMessHeads(no);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvExpend.DataSource = ds.Tables[0];
            lvExpend.DataBind();
            if (ddlMess.SelectedIndex != 0)
            {
                DataSet dsstd = objMBC.GetMessHeadsbyMessno_Month(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddlMess.SelectedValue.ToString().Trim()), Convert.ToInt32(ddlMonth.SelectedValue));
                if (dsstd != null && dsstd.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < lvExpend.Items.Count; i++)
                    {
                        Label lblsheadA = (Label)lvExpend.Items[i].FindControl("lblsheadA");
                        lblsheadA.Text = "";
                        Label lbl = (Label)lvExpend.Items[i].FindControl("lblshead");


                        if (lblsheadA != null && lbl != null)
                        {
                            lblsheadA.Text = dsstd.Tables[0].Rows[0][lbl.Text.ToString().Trim()].ToString().Trim();
                        }
                    }
                }
            }
        }
        else
        {
            lvExpend.DataSource = null;
            lvExpend.DataBind();
            return;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=MessBillMonthwise";
            url += "&path=~,Reports,Hostel,rptHostelMessBillMonth.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + ",@P_SESSIONNO_NO=" + ddlSession.SelectedValue + ",@P_MONTH_NO=" + ddlMonth.SelectedValue + ",@P_HOSTEL_NO=" + ddlHostel.SelectedValue;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + ",@P_SESSIONNO_NO=" + ddlSession.SelectedValue + ",@P_MONTH=" + ddlMonth.SelectedValue + ",@P_HOSTEL_NO=" + ddlHostel.SelectedValue + ",@P_HOSTEL="+ddlHostel.SelectedItem.Text+",@P_MESS_TYPE="+ddlMess.SelectedItem.Text+",@P_BILL_DATE="+txtBillDate.Text+",@P_SESSION_NAME="+ddlSession.SelectedItem.Text+",@P_MONTH_NAME="+ddlMonth.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','MessBillMonthwise','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MessBillCalculation.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        Calculate();
        btnSubmit.Enabled = true;
    }

    private void Calculate()
    {
        try
        {
            int totdays = 0;
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                TextBox txtDays = item.FindControl("txtDays") as TextBox;
                if (txtDays.Text!="")
                    totdays += Convert.ToInt32(txtDays.Text);
            }
            
            string totStud = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_DCR B ON (B.IDNO=A.RESIDENT_NO AND B.SESSIONNO=A.HOSTEL_SESSION_NO)", "COUNT(distinct idno)", "HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + " AND RECIEPT_CODE='MF' and recon=1 AND A.CAN=0 AND B.CAN=0");
            string totExpend = objCommon.LookUp("ACD_HOSTEL_MESS_EXPENDITURE", "ISNULL(SUM(f1),0)TOTAL_EXPENDITURE", "SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " and (MONTH([MONTH])=" + Convert.ToInt32(ddlMonth.SelectedValue) + " or " + Convert.ToInt32(ddlMonth.SelectedValue) + "=0)");

            //per day
            decimal perDay = Convert.ToDecimal(totExpend.ToString()) / totdays;
            if(chkInt.Checked==true)
                txtPerDay.Text = Math.Round(perDay, 2).ToString();
            else            
                txtPerDay.Text = Math.Ceiling(perDay).ToString();

            decimal monthlyExp = 0.0m;
            decimal otherCharges = 0.0m;
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                TextBox txtDays = item.FindControl("txtDays") as TextBox;
                TextBox txtExpenditure = item.FindControl("txtExpenditure") as TextBox;
                TextBox txtTotPaid = item.FindControl("txtTotPaid") as TextBox;
                TextBox txtBalance = item.FindControl("txtBalance") as TextBox;

                TextBox txtF1 = item.FindControl("txtF1") as TextBox;
                TextBox txtF2 = item.FindControl("txtF2") as TextBox;
                TextBox txtF3 = item.FindControl("txtF3") as TextBox;
                TextBox txtF4 = item.FindControl("txtF4") as TextBox;
                TextBox txtF5 = item.FindControl("txtF5") as TextBox;
                TextBox txtF6 = item.FindControl("txtF6") as TextBox;
                TextBox txtF7 = item.FindControl("txtF7") as TextBox;
                TextBox txtF8 = item.FindControl("txtF8") as TextBox;
                TextBox txtF9 = item.FindControl("txtF9") as TextBox;
                TextBox txtF10 = item.FindControl("txtF10") as TextBox;

                if (txtF1.Text == "") txtF1.Text = "0";
                if (txtF2.Text == "") txtF2.Text = "0";
                if (txtF3.Text == "") txtF3.Text = "0";
                if (txtF4.Text == "") txtF4.Text = "0";
                if (txtF5.Text == "") txtF5.Text = "0";
                if (txtF6.Text == "") txtF6.Text = "0";
                if (txtF7.Text == "") txtF7.Text = "0";
                if (txtF8.Text == "") txtF8.Text = "0";
                if (txtF9.Text == "") txtF9.Text = "0";
                if (txtF10.Text == "") txtF10.Text = "0";
                if (txtDays.Text == "") txtDays.Text = "0";
                // txtF2.Text not consider becouse it is already include
                otherCharges = Convert.ToDecimal(txtF1.Text) + Convert.ToDecimal(txtF2.Text) + Convert.ToDecimal(txtF3.Text) + Convert.ToDecimal(txtF4.Text) + Convert.ToDecimal(txtF5.Text) + Convert.ToDecimal(txtF6.Text) + Convert.ToDecimal(txtF7.Text) + Convert.ToDecimal(txtF8.Text) + Convert.ToDecimal(txtF9.Text) + Convert.ToDecimal(txtF10.Text);
                monthlyExp = Convert.ToDecimal(Convert.ToDecimal(txtDays.Text) * Convert.ToDecimal(txtPerDay.Text));
                txtExpenditure.Text = (monthlyExp + otherCharges).ToString();

                //Balance
                txtBalance.Text = (Convert.ToDecimal(txtTotPaid.Text) - Convert.ToDecimal(txtExpenditure.Text)).ToString();

                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MessBillCalculation.Calculate-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnDays_Click(object sender, EventArgs e)
    {
        SetDaysEmpty();
    }

    private void SetDaysEmpty()
    {
        //// Set Day no null for new month
        //string check = objCommon.LookUp("ACD_HOSTEL_MESS_BILL", "COUNT(DISTINCT MONTH_NO)", "SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + " AND MONTH_NO=" + Convert.ToInt32(ddlMonth.SelectedValue));
        //if (check == "0")
        //{
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                TextBox txtDays = item.FindControl("txtDays") as TextBox;
                txtDays.Text = "";
            }
        //}
    }
}
