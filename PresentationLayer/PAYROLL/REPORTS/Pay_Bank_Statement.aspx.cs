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
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Data.SqlClient;

public partial class PAYROLL_REPORTS_Pay_Bank_Statement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        try
        {
            ddlMappingAccNo.Enabled=false;
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //if (ua_type != 1)
                //{
                //    trCertificate.Visible = false;
                //    //trrbl.Visible = false;
                //    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

                //    //ddlEmployeeNo.SelectedItem.Text = empname;
                //    PopulateDropDownListForFaculty();
                //   // ddlStaffNo.SelectedIndex = 1;
                //    //ddlEmployeeNo.SelectedIndex = 1;

                //}
                //else
                //{
                    PopulateDropDownList();
               // }

                //Populate DropdownList
                //PopulateDropDownList();

                //check user is admin or other user
                //if (Session["userno"].ToString() != "0")
                //{
                //    ddlEmployeeNo.SelectedValue = Session["userno"].ToString();
                //    ddlEmployeeNo.Visible = true;

                //}
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void PopulateDropDownListForFaculty()
    {
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

        try
        {
            //FILL MONTH YEAR 
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103)");

            objCommon.FillListBox(lstStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
           

            //FILL STAFF
           // objCommon.FillListBox(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            //objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");

            //FILL COLLEGE
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

            //FILL EMPLOYEE
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void PopulateDropDownList()
    {
        try
        {
            //FILL MONTH YEAR 
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");

            //FILL Scheme
            objCommon.FillListBox(lstStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL STAFF
            //  objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "", "EMPTYPENO");

            //FILL COLLEGE
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

            //FILL BANK //comment
            //objCommon.FillDropDownList(ddlBank, "PAYROLL_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
            objCommon.FillListBox(lbBank, "PAYROLL_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");

            //FILL EMPLOYEE
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS e left outer join " + ddlMonthYear.SelectedItem.Text + " t on (e.idno = t.idno)", "t.IDNO", "'['+ convert(nvarchar(150),t.IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "t.IDNO>0 and t.STAFFNO =" + ddlStaffNo.SelectedValue, "t.IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_Bank_Statement.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Bank_Statement.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
        //{
        //    ShowMessage("Please Select Employee.");
        //    return;
        //}
        ShowReportEmployeeBankStatementReport("Bank_Advice", "PAY_BANK_STATEMENT_REPORT.rpt");
    }
    protected void btnBankSuppli_Click(object sender, EventArgs e)
    {
        //if (rdoSelectEmployee.Checked && ddlEmployeeNo.SelectedIndex == 0)
        //{
        //    ShowMessage("Please Select Employee.");
        //    return;
        //}
        ShowReportEmployeeBankStatementReport("Bank_Advice", "PAY_BANK_STATEMENT_REPORT_SPPLI.rpt");
    }
    private void ShowReportEmployeePayslip(string reportTitle, string rptFileName)
    {
        try
        {
            int count = 0;
            int count1 = 0;
            string employeelist = string.Empty;
            string stafflist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                    count++;
                }
                else
                {

                }
            }
            if (count == 0)
            {
                employeelist = "";
            }
            else
            {
                employeelist = employeelist.Substring(0, employeelist.Length - 1);
            }
            for (int i = 0; i < lstStaffNo.Items.Count; i++)
            {
                if (lstStaffNo.Items[i].Selected)
                {
                    stafflist += lstStaffNo.Items[i].Value + "$";
                    count1++;
                }
                else
                {

                }
            }
            if (count1 == 0)
            {
                stafflist = "";
            }
            else
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;


            url += "&path=~,Reports,Payroll," + rptFileName;

            //if(ViewState["action"].Equals("salaryCertificate"))
           // url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_CODE=29,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue;
            //url += "&param=@P_COLLEGE_CODE=33,@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue);
           // url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) +",@P_ORDERBY=" + ddlOrderBy.SelectedValue + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue);
     
            //+ "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            //else
            //    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReportEmployeePayslipNeft(string reportTitle, string rptFileName)
    {
        try
        {
            int count = 0;
            int count1 = 0;
            string employeelist = string.Empty;
            string stafflist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                    count++;
                }
                else
                {

                }
            }
            if (count == 0)
            {
                employeelist = "";
            }
            else
            {
                employeelist = employeelist.Substring(0, employeelist.Length - 1);
            }
            for (int i = 0; i < lstStaffNo.Items.Count; i++)
            {
                if (lstStaffNo.Items[i].Selected)
                {
                    stafflist += lstStaffNo.Items[i].Value + "$";
                    count1++;
                }
                else
                {

                }
            }
            if (count1 == 0)
            {
                stafflist = "";
            }
            else
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
            }
          if(count > 0)
          {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;
            url += "&path=~,Reports,Payroll," + rptFileName;

            //if(ViewState["action"].Equals("salaryCertificate"))
            url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_CODE=29,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_BANKNO=" + employeelist + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue);
            //+ "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            //else
            //    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
          }
          else
          {
              objCommon.DisplayMessage("Please Select Bank", this.Page);
          }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReportEmployeeBankStatementReport(string reportTitle, string rptFileName)
    {
        try
        {
        int count = 0;
        int count1 = 0;
        string employeelist = string.Empty;
        string stafflist = string.Empty;

        for (int i = 0; i < lbBank.Items.Count; i++)
        {
            if (lbBank.Items[i].Selected)
            {
                employeelist += lbBank.Items[i].Value + "$";
                count++;
            }
            else
            {

            }
        }
        if (count == 0)
        {
            employeelist = "";
        }
        else
        {
            employeelist = employeelist.Substring(0, employeelist.Length - 1);
        }

            // Multiple  STAFF
        for (int i = 0; i < lstStaffNo.Items.Count; i++)
        {
            if (lstStaffNo.Items[i].Selected)
            {
                stafflist += lstStaffNo.Items[i].Value + "$";
                count1++;
            }
            else
            {

            }
        }
        if (count1 == 0)
        {
            stafflist = "";
        }
        else
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }

        if (count > 0)
       {
           
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;


            url += "&path=~,Reports,Payroll," + rptFileName;

            //if(ViewState["action"].Equals("salaryCertificate"))
            //url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_CODE=29,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_BANKNO=" + Convert.ToInt32(ddlBank.SelectedValue) +"";
            url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_CODE="+Session["colcode"].ToString()+",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_BANKNO=" + employeelist + ",@P_ORDERBY=" + ddlOrderBy.SelectedValue + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue);
            //+ "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue);
            //else
            //    url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
       }

       else
       {
           objCommon.DisplayMessage("Please Select Bank", this.Page);
       }
            //string colvalues;
            //int checkcount = 0;
            //colvalues = string.Empty;
            //foreach (ListViewDataItem lvitem in lvBankStatement.Items)
            //{
            //    CheckBox chk = lvitem.FindControl("ChkAppointment") as CheckBox;
            //    if (chk.Checked)
            //    {
            //        checkcount =checkcount+1;
            //        colvalues = colvalues + chk.ToolTip + ".";
            //    }
            //    else
            //    {

            //        //objCommon.DisplayMessage("Please select employees from the list!", this.Page);
            //        //  return;
            //    }
            //}

            //if (checkcount == 0)
            //{
            //    objCommon.DisplayMessage("Please Select Employees from List!", this.Page);
            //    return;
            //}

            //string colval = colvalues.Substring(0, colvalues.Length - 1);




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    

    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    //protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ddlStaffNo.SelectedIndex != 0)

    //    //    //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "STAFFNO="+ddlStaffNo.SelectedValue, "IDNO");
    //    //    objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS e left outer join " + ddlMonthYear.SelectedItem.Text + " t on (e.idno = t.idno)", "t.IDNO", "'['+ convert(nvarchar(150),t.IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "t.IDNO>0 and t.STAFFNO =" + ddlStaffNo.SelectedValue, "t.IDNO");
    //    int idno = Convert.ToInt32(Session["idno"]);
    //    int ua_type = Convert.ToInt32(Session["usertype"]);
    //    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + idno);
    //    if (ua_type == 3)
    //    {
    //        int IDNO = Convert.ToInt32(Session["idno"]);
    //        ddlStaffNo.SelectedValue = staffno;
    //        //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS e left outer join " + ddlMonthYear.SelectedItem.Text + " t on (e.idno = t.idno)", "t.IDNO", "'['+ convert(nvarchar(150),t.IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "t.IDNO=" + IDNO + "and t.STAFFNO =" + ddlStaffNo.SelectedValue, "t.IDNO");
    //    }
    //    else
    //    {

    //        //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS e left outer join " + ddlMonthYear.SelectedItem.Text + " t on (e.idno = t.idno)", "t.IDNO", "'['+ convert(nvarchar(150),t.IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "t.IDNO>0 and t.STAFFNO =" + ddlStaffNo.SelectedValue, "t.IDNO");
    //    }
    //   // pnlBankStatement.Visible = false;
    //    ddlBank.SelectedIndex = 0;
    //}

    
    protected void btnreportexcel_Click(object sender, EventArgs e)
    {
        int count=0;
        int count1 = 0;
        string employeelist = string.Empty;
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.Text;
        string stafflist = string.Empty;
       // int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int EmployeeType=Convert.ToInt32(ddlEmployeeType.SelectedValue);
        string OrderBy =Convert.ToString(ddlOrderBy.SelectedValue);
        //int bankno = Convert.ToInt32(ddlBank.SelectedValue);

            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                    count++;
                }
                else
                {

                }
            }
            if(count == 0)
            {
               employeelist = "";
            }
            else
            {
                employeelist = employeelist.Substring(0, employeelist.Length - 1);
            }
            for (int i = 0; i < lstStaffNo.Items.Count; i++)
              {
                if (lstStaffNo.Items[i].Selected)
                {
                  stafflist += lstStaffNo.Items[i].Value + "$";
                  count1++;
                }
                else
                {
                     
                 }
              }
              if (count1 == 0)
              {
                  stafflist = "";
              }
              else
              {
                  stafflist = stafflist.Substring(0, stafflist.Length - 1);
              }
      if (count > 0)
      {
          DataSet ds = BankStatementExcelforListBox(monyear, collegeNo, stafflist, employeelist,OrderBy,EmployeeType);
          if (ds.Tables[0].Rows.Count > 0)
          {
              string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
              string Collegeaddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);
              string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
              string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");

              //ds.Tables[0].Columns.RemoveAt(3);
              GVDayWiseAtt.DataSource = ds;
              GVDayWiseAtt.DataBind();

              GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
              TableCell HeaderCell = new TableCell();
              HeaderCell = new TableCell();
              HeaderCell.Text = collename;
              HeaderCell.ColumnSpan = 9;
              HeaderCell.Font.Bold = true;
              HeaderCell.Font.Size = 16;
              HeaderCell.Attributes.Add("style", "text-align:center !important;");
              HeaderGridRow.Cells.Add(HeaderCell);
              GVDayWiseAtt.Controls[0].Controls.AddAt(0, HeaderGridRow);

              GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
              TableCell HeaderCell2 = new TableCell();
              HeaderCell2 = new TableCell();
              HeaderCell2.Text = Collegeaddress;
              HeaderCell2.ColumnSpan = 9;
              HeaderCell2.Font.Bold = true;
              HeaderCell2.Font.Size = 16;
              HeaderCell2.Attributes.Add("style", "text-align:center !important;");
              HeaderGridRow2.Cells.Add(HeaderCell2);
              GVDayWiseAtt.Controls[0].Controls.AddAt(1, HeaderGridRow2);



              GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
              TableCell HeaderCell1 = new TableCell();
              HeaderCell1.Text = "  BANK STATEMEMENT  " + Month + "    " + Year + "    ";
              HeaderCell1.ColumnSpan = 9;
              HeaderCell1.Font.Bold = true;
              HeaderCell1.Font.Size = 14;
              HeaderCell1.Attributes.Add("style", "text-align:center !important;");
              HeaderGridRow1.Cells.Add(HeaderCell1);
              GVDayWiseAtt.Controls[0].Controls.AddAt(2, HeaderGridRow1);

              string attachment = "attachment; filename=BankStatement.xls";
              Response.ClearContent();
              Response.AddHeader("content-disposition", attachment);
              Response.ContentType = "application/vnd.MS-excel";
              StringWriter sw = new StringWriter();
              HtmlTextWriter htw = new HtmlTextWriter(sw);
              GVDayWiseAtt.RenderControl(htw);
              Response.Write(sw.ToString());
              Response.End();
          }
          else
          {
              objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
          }

      }
      else
      {
          objCommon.DisplayMessage("Please Select Bank", this.Page);
      }
    }
   // string OrderBy,int EmployeeType)

    //method for listbox 26/05/21
    public DataSet BankStatementExcelforListBox(string MonthYear, int collegeNo, string staffNo, string bankNo, string OrderBy, int EmployeeType)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
            objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
            objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
            objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
            objParams[4] = new SqlParameter("@P_ORDERBY", OrderBy);
            objParams[5] = new SqlParameter("@P_EMPTYPENO", EmployeeType);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_REPORT_EXCEL", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

    protected void btnPFStatement_Click(object sender, EventArgs e)
    {
        ShowReportEmployeePayslip("Bank_Advice", "PAY_PF_STATEMENT_REPORT_FORMAT2.rpt");
    }
    protected void btnreportNEFT_Click(object sender, EventArgs e)
    {
        ShowReportEmployeePayslipNeft("Bank_Advice", "PAY_NEFT_STATEMENT_REPORT.rpt");
    }

    protected void btnNEFTexcel_Click(object sender, EventArgs e)
    {
        int count = 0;
        int count1 = 0;
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.Text;
       // int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
       
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        //int bankno = Convert.ToInt32(ddlBank.SelectedValue);
        int EmployeeType = Convert.ToInt32(ddlEmployeeType.SelectedValue);
        string OrderBy = Convert.ToString(ddlOrderBy.SelectedValue);

        string employeelist = string.Empty;
        string stafflist = string.Empty;
        for (int i = 0; i < lbBank.Items.Count; i++)
        {
            if (lbBank.Items[i].Selected)
            {
                employeelist += lbBank.Items[i].Value + "$";
                count++;
            }
            else
            {

            }
        }
        if (count == 0)
        {
            employeelist = "";
        }
        else
        {
            employeelist = employeelist.Substring(0, employeelist.Length - 1);
        }
        for (int i = 0; i < lstStaffNo.Items.Count; i++)
        {
            if (lstStaffNo.Items[i].Selected)
            {
                stafflist += lstStaffNo.Items[i].Value + "$";
                count1++;
            }
            else
            {

            }
        }
        if (count1 == 0)
        {
            stafflist = "";
        }
        else
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }
        if(count > 0)
        {
            DataSet ds = NEFTStatementExcel(monyear, collegeNo, stafflist, employeelist,OrderBy,EmployeeType);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment; filename=NEFTStatement.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
        }
       }
       else
       {
           objCommon.DisplayMessage("Please Select Bank", this.Page);
       }
        
    }


    public DataSet NEFTStatementExcel(string MonthYear, int collegeNo, string staffNo, string bankNo, string OrderBy, int EmployeeType)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
            objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
            objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
            objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
            objParams[4] = new SqlParameter("@P_ORDERBY", OrderBy);
            objParams[5] = new SqlParameter("@P_EMPTYPENO", EmployeeType);
            //objParams[4] = new SqlParameter("@P_IDNOS", idnos);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_NEFT_STATEMENT_REPORT_EXCEL", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }
    
    protected void btnPFexcel_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.Text;
       // int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int count = 0;
        int count1 = 0;
        string employeelist = string.Empty;
        string stafflist = string.Empty;
        string OrderBy =Convert.ToString(ddlOrderBy.SelectedValue);
        int EmployeeType=Convert.ToInt32(ddlEmployeeType.SelectedValue);

        for (int i = 0; i < lbBank.Items.Count; i++)
        {
            if (lbBank.Items[i].Selected)
            {
                employeelist += lbBank.Items[i].Value + "$";
                count++;
            }
            else
            {

            }
        }
        if (count == 0)
        {
            employeelist = "";
        }
        else
        {
            employeelist = employeelist.Substring(0, employeelist.Length - 1);
        }
        for (int i = 0; i < lstStaffNo.Items.Count; i++)
        {
            if (lstStaffNo.Items[i].Selected)
            {
                stafflist += lstStaffNo.Items[i].Value + "$";
                count1++;
            }
            else
            {

            }
        }
        if (count1 == 0)
        {
            stafflist = "";
        }
        else
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }
       
        //DataSet ds = objpay.PFStatementExcel(monyear, collegeNo, stafflist);
        DataSet ds = PFStatementExcel(monyear, collegeNo, stafflist, OrderBy, EmployeeType);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string Collegeaddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");

            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            // Added on 12-12-2022
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = collename;
            HeaderCell.ColumnSpan = 3;
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 16;
            HeaderCell.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow.Cells.Add(HeaderCell);
            GVDayWiseAtt.Controls[0].Controls.AddAt(0, HeaderGridRow);

            GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = Collegeaddress;
            HeaderCell2.ColumnSpan = 3;
            HeaderCell2.Font.Bold = true;
            HeaderCell2.Font.Size = 16;
            HeaderCell2.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GVDayWiseAtt.Controls[0].Controls.AddAt(1, HeaderGridRow2);



            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "  BANK STATEMEMENT  " + Month + "    " + Year + "    ";
            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.Font.Bold = true;
            HeaderCell1.Font.Size = 14;
            HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GVDayWiseAtt.Controls[0].Controls.AddAt(2, HeaderGridRow1);


            string attachment = "attachment; filename=PFStatement.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
        }
    }

    public DataSet PFStatementExcel(string MonthYear, int collegeNo, string staffNo,string OrderBy,int EmployeeType)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
            objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
            objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
            objParams[3] = new SqlParameter("@P_ORDERBY", OrderBy);
            objParams[4] = new SqlParameter("@P_EMPTYPENO", EmployeeType);
            //objParams[2] = new SqlParameter("@P_IDNO", idno);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_PF_STATEMENT_REPORT_EXCEL", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }
    //Updated on 13-12-2022
    protected void btnITStatement_Click(object sender, EventArgs e)
    {
        ShowReportEmployeePayslip("Bank_Advice", "PAY_IT_STATEMENT_REPORT.rpt");
    }



    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string employeelist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                }
            }
            employeelist = employeelist.Substring(0, employeelist.Length - 1);


            // pnlBankStatement.Visible = false;
            //ddlBank.SelectedIndex = 0;
           // ddlStaffNo.SelectedIndex = 0;
            string stafflist = string.Empty;
            foreach (ListItem lstitem in lstStaffNo.Items)
            {
                if (lstitem.Selected == true)
                {
                    for (int i = 0; i < lstStaffNo.Items.Count; i++)
                    {
                        if (lstStaffNo.Items[i].Selected)
                        {
                            stafflist += lstStaffNo.Items[i].Value + "$";
                        }
                    }
                    stafflist = stafflist.Substring(0, stafflist.Length - 1);
                }
                else
                {
                    stafflist = "0";
                }
            }
          
            ddlEmployeeType.SelectedIndex = 0;
            if (ddlEmployeeType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and STAFFNO=" + ddlEmployeeType.SelectedValue + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string employeelist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                }
            }
            employeelist = employeelist.Substring(0, employeelist.Length - 1);

            // pnlBankStatement.Visible = false;
            //ddlBank.SelectedIndex = 0;


            string stafflist = string.Empty;
            foreach (ListItem lstitem in lstStaffNo.Items)
            {
                if (lstitem.Selected == true)
                {
                    for (int i = 0; i < lstStaffNo.Items.Count; i++)
                    {
                        if (lstStaffNo.Items[i].Selected)
                        {
                            stafflist += lstStaffNo.Items[i].Value + "$";
                        }
                    }
                    stafflist = stafflist.Substring(0, stafflist.Length - 1);
                }
                else
                {
                    stafflist = "0";
                }
            }
          
            ddlEmployeeType.SelectedIndex = 0;

            if (ddlEmployeeType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and STAFFNO=" + ddlEmployeeType.SelectedValue + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string employeelist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                }
            }
            employeelist = employeelist.Substring(0, employeelist.Length - 1);

            // pnlBankStatement.Visible = false;
            //ddlBank.SelectedIndex = 0;

            string stafflist = string.Empty;
            foreach (ListItem lstitem in lstStaffNo.Items)
            {
                if (lstitem.Selected == true)
                {
                    for (int i = 0; i < lstStaffNo.Items.Count; i++)
                    {
                        if (lstStaffNo.Items[i].Selected)
                        {
                            stafflist += lstStaffNo.Items[i].Value + "$";
                        }
                    }
                    stafflist = stafflist.Substring(0, stafflist.Length - 1);
                }
                else
                {
                    stafflist = "0";
                }
            }
           
            if (ddlEmployeeType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and STAFFNO=" + ddlEmployeeType.SelectedValue + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    protected void btnBankStatment_Format2_Click(object sender, EventArgs e)
    {
        ShowReportEmployeeBankStatementReport("Bank_Advice", "Pay_BankStatement_Format2_Report.rpt");
    }

    protected void btnBankText_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int count1 = 0;
            string monyear = ddlMonthYear.SelectedItem.Text;
            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
           // int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
            int staffno;
            string employeelist = string.Empty;
            int Bankno;
            string stafflist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                    count++;
                }
                else
                {

                }
            }
            if (count == 0)
            {
                employeelist = "0";
                ShowMessage("Select Only One Bank Name");
                return;
            }
            else if (count == 1)
            {
                 employeelist = employeelist.Substring(0, employeelist.Length - 1);

                Bankno = Convert.ToInt32(employeelist);
            }
            else
            {
                ShowMessage("Select Only One Bank Name");
                return;

            }
            for (int i = 0; i < lstStaffNo.Items.Count; i++)
            {
                if (lstStaffNo.Items[i].Selected)
                {
                    stafflist += lstStaffNo.Items[i].Value + "$";
                    count1++;
                }
                else
                {

                }
            }
            if (count1 == 0)
            {
                //stafflist = "";
                employeelist = "0";
                ShowMessage("Select Only One Staff Name");
                return;
            }
            else if(count1  == 1)
            {
                stafflist = stafflist.Substring(0, stafflist.Length - 1);
                staffno = Convert.ToInt32(stafflist);
            }
            else
            {
                ShowMessage("Select Only One Staff Name");
                return;

            }
            if (count > 0)
            {
                //DataSet ds = objpay.BankStatementTextListbox(monyear, collegeNo, stafflist, employeelist, Convert.ToInt32(ddlEmployeeType.SelectedValue));
                DataSet ds = BankStatementTextListbox(monyear, collegeNo, staffno, Bankno, Convert.ToInt32(ddlEmployeeType.SelectedValue), Convert.ToString(ddlOrderBy.SelectedValue));
                // string stafftext = ddlStaffNo.SelectedItem.Text;
                string stafftext = stafflist;
                //string bankname = ddlBankName.SelectedItem.Text;

                //string filename = "MyObCustomerCard2.txt";
                string filename = (monyear + '_' + stafftext);
                filename = filename + ".txt";

                // Exporting Data to text file

                if (ds.HasErrors == false)
                {
                    ExportDataTabletoFile(ds.Tables[0], "    ", true, Server.MapPath("~/Images/" + filename));
                }
                else
                {
                    ShowMessage("No data found for current selection");
                    return;
                }

                #region download notepad or text file.

                Response.ContentType = "application/octet-stream";

                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);

                string aaa = Server.MapPath("~/images/" + filename);

                Response.TransmitFile(Server.MapPath("~/images/" + filename));

                HttpContext.Current.ApplicationInstance.CompleteRequest();

                Response.End();
                #endregion
            }
            else
            {
                objCommon.DisplayMessage("Please Select Bank", this.Page);
            }
          
        }
        catch (Exception ex)
        {
            ShowMessage("No data found for current selection");
            return;

        }
    }

    //method for  bankstatement for listbox 26/05/21
    public DataSet BankStatementTextListbox(string MonthYear, int collegeNo, int staffNo, int bankNo, int EmployeeType,string OrderBy)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_MONYEAR", MonthYear);
            objParams[1] = new SqlParameter("@P_COLLEGENO", collegeNo);
            objParams[2] = new SqlParameter("@P_STAFFNO", staffNo);
            objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
            objParams[4] = new SqlParameter("@P_EMPLOYEETYPE", EmployeeType);
            objParams[5] = new SqlParameter("@P_ORDERBY", OrderBy);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_TEXT", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.BankStatementText-> " + ex.ToString());
        }
        return ds;
    }
    public void ExportDataTabletoFile(DataTable datatable, string delimited, bool exportcolumnsheader, string file)
    {

        StreamWriter str = new StreamWriter(file, false, System.Text.Encoding.Default);

        if (exportcolumnsheader)
        {
            string Columns = string.Empty;

            foreach (DataColumn column in datatable.Columns)
            {
                Columns += column.ColumnName + delimited;
            }
            str.WriteLine(Columns.Remove(Columns.Length - 1, 1));

        }

        foreach (DataRow datarow in datatable.Rows)
        {
            string row = string.Empty;

            foreach (object items in datarow.ItemArray)
            {
                row += items.ToString() + delimited;
            }
            str.WriteLine(row.Remove(row.Length - 1, 1));
        }
        str.Flush();
        str.Close();
    }

    private void ShowReportEmployeeBankStatement_Format2Report(string reportTitle, string rptFileName)
    {
        try
        {
            //string colvalues;
            //int checkcount = 0;
            //colvalues = string.Empty;
            //foreach (ListViewDataItem lvitem in lvBankStatement.Items)
            //{
            //    CheckBox chk = lvitem.FindControl("ChkAppointment") as CheckBox;
            //    if (chk.Checked)
            //    {
            //        checkcount = checkcount + 1;
            //        colvalues = colvalues + chk.ToolTip + ".";
            //    }
            //    else
            //    {

            //    }
            //}

            //if (checkcount == 0)
            //{
            //    objCommon.DisplayMessage("Please Select Employees from List!", this.Page);
            //    return;
            //}

            //string colval = colvalues.Substring(0, colvalues.Length - 1);

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;



            //url += "&path=~,Reports,Payroll," + rptFileName;

            //url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLLEGE_CODE=29,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_BANKNO=" + Convert.ToInt32(ddlBank.SelectedValue) + ",@P_IDNOS=" + colval + "";


            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            string employeelist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                }
            }
            employeelist = employeelist.Substring(0, employeelist.Length - 1);

            string stafflist = string.Empty;
            foreach (ListItem lstitem in lstStaffNo.Items)
            {
                if (lstitem.Selected == true)
                {
                    for (int i = 0; i < lstStaffNo.Items.Count; i++)
                    {
                        if (lstStaffNo.Items[i].Selected)
                        {
                            stafflist += lstStaffNo.Items[i].Value + "$";
                        }
                    }
                    stafflist = stafflist.Substring(0, stafflist.Length - 1);
                }
                else
                {
                    stafflist = "0";
                }
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;

            url += "&path=~,Reports,Payroll," + rptFileName;

            url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafflist + ",@P_COLLEGE_CODE=29,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_BANKNO=" + employeelist + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // 25 Apr 2018


    //protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try

    //    {

    //        string employeelist = string.Empty;
    //        for (int i = 0; i < lbBank.Items.Count; i++)
    //        {
    //            if (lbBank.Items[i].Selected)
    //            {
    //                employeelist += lbBank.Items[i].Value + ",";
    //            }
    //        }
    //        employeelist = employeelist.Substring(0, employeelist.Length - 1);

    //        if (ddlEmployeeType.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + ddlStaffNo.SelectedValue + " and STAFFNO=" + ddlEmployeeType.SelectedValue + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + ddlStaffNo.SelectedValue + " and BANKNO=" + employeelist, "BANK_ACCNO");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    protected void lbBank_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            string employeelist = string.Empty;
            for (int i = 0; i < lbBank.Items.Count; i++)
            {
                if (lbBank.Items[i].Selected)
                {
                    employeelist += lbBank.Items[i].Value + "$";
                }
            }
            employeelist = employeelist.Substring(0, employeelist.Length - 1);

            string stafflist = string.Empty;
            foreach (ListItem lstitem in lstStaffNo.Items)
            {
                if (lstitem.Selected == true)
                {
                    for (int i = 0; i < lstStaffNo.Items.Count; i++)
                    {
                        if (lstStaffNo.Items[i].Selected)
                        {
                            stafflist += lstStaffNo.Items[i].Value + "$";
                        }
                    }
                    stafflist = stafflist.Substring(0, stafflist.Length - 1);
                }
                else
                {
                    stafflist = "0";
                }
            }

            if (ddlEmployeeType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and STAFFNO=" + ddlEmployeeType.SelectedValue + " and 	BANKNO=" + employeelist, "BANK_ACCNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlMappingAccNo, "Payroll_BankAccMapping ", "distinct BANK_ACCNO As ID", "BANK_ACCNO", "COLLEGE_ID=" + ddlCollege.SelectedValue + " and SCHEMENO=" + stafflist + " and BANKNO=" + employeelist, "BANK_ACCNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnexporttoexcelacc_Click(object sender, EventArgs e)
    {
        int count = 0;
        int count1 = 0;
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.Text;
       // int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        //int bankno = Convert.ToInt32(ddlBank.SelectedValue);

        // Added on 12-12-2022
        int EmployeeType = Convert.ToInt32(ddlEmployeeType.SelectedValue);
        string OrderBy = Convert.ToString(ddlOrderBy.SelectedValue);

        string employeelist = string.Empty;
        string stafflist = string.Empty;
        for (int i = 0; i < lbBank.Items.Count; i++)
        {
            if (lbBank.Items[i].Selected)
            {
                employeelist += lbBank.Items[i].Value + "$";
                count++;
            }
            else
            {

            }
        }
        if (count == 0)
        {
            employeelist = "";
        }
        else
        {
            employeelist = employeelist.Substring(0, employeelist.Length - 1);
        }
        for (int i = 0; i < lstStaffNo.Items.Count; i++)
        {
            if (lstStaffNo.Items[i].Selected)
            {
                stafflist += lstStaffNo.Items[i].Value + "$";
                count1++;
            }
            else
            {

            }
        }
        if (count1 == 0)
        {
            stafflist = "";
        }
        else
        {
            stafflist = stafflist.Substring(0, stafflist.Length - 1);
        }

        DataSet ds = BankStatementExcelforListBoxWithAcc(monyear, collegeNo, stafflist, employeelist,OrderBy,EmployeeType);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string Collegeaddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");
               

            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = collename;
            HeaderCell.ColumnSpan = 5;           
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 16;
            HeaderCell.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow.Cells.Add(HeaderCell);
            GVDayWiseAtt.Controls[0].Controls.AddAt(0, HeaderGridRow);


            GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = Collegeaddress;
            HeaderCell2.ColumnSpan = 5;
            HeaderCell2.Font.Bold = true;
            HeaderCell2.Font.Size = 16;
            HeaderCell2.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow2.Cells.Add(HeaderCell2);
            GVDayWiseAtt.Controls[0].Controls.AddAt(1, HeaderGridRow2);


            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "  BANK STATEMEMENT  " + Month + "    " + Year + "    ";
            HeaderCell1.ColumnSpan = 5;
            HeaderCell1.Font.Bold = true;
            HeaderCell1.Font.Size = 14;
            HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GVDayWiseAtt.Controls[0].Controls.AddAt(2, HeaderGridRow1);


            string attachment = "attachment; filename=BankStatementWithoutAcc.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
        }

     }
    public DataSet BankStatementExcelforListBoxWithAcc(string MonthYear, int collegeNo, string staffNo, string bankNo,string OrderBy,int EmployeeType)
     { 
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
            objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
            objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
            objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
            objParams[4] = new SqlParameter("@P_ORDERBY", OrderBy);
            objParams[5] = new SqlParameter("@P_EMPTYPENO", EmployeeType);          
            //objParams[4] = new SqlParameter("@P_IDNOS", idnos);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_REPORT_EXCEL_WITHOUT_ACC", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
     }

    protected void lstStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

//--Session["colcode"].ToString()