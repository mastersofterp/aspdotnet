//=================================================
//Modified By: ZUBAIR AHMAD
//Modified Date: 15-FEB-2015
//Reason:   To display Sub-Payhead report

//=================================================

using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Linq;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class PAYROLL_REPORTS_Pay_SubPayHead_Reort : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    string itemselectcnt = string.Empty;

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
                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                if (ua_type != 1)
                {
                   
                    string staffno = objCommon.LookUp("payroll_empmas", "staffno", "idno=" + IDNO);

                    //ddlEmployeeNo.SelectedItem.Text = empname;
                    PopulateDropDownListForFaculty();
                    //ddlStaffNo.SelectedIndex = 1;
                    FillPayhead();
                    FillCollege();
                    //ddlEmployeeNo.SelectedIndex = 1;

                }
                else
                {
                    PopulateDropDownList();
                    FillPayhead();
                    FillCollege();
                }

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

            //FILL STAFF
            //objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO=" + staffno, "STAFFNO");

            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD H INNER JOIN PAYROLL_PAY_SUBPAYHEAD SH ON (H.PAYHEAD = SH.PAYHEAD)", "DISTINCT H.PAYHEAD", "H.PAYSHORT", "", "PAYSHORT");

            //FILL EMPLOYEE
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS ", "IDNO", "'['+ convert(nvarchar(150),IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "IDNO=" + IDNO, "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SubPayHead_Reort.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
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

            //FILL STAFF
            //objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            //objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE = 'E' AND (CAL_ON IS NULL or CAL_ON='') AND PAYSHORT IS NOT NULL AND SRNO > 15 AND PAYSHORT<>'' and PAYSHORT <> '-'", "PAYSHORT");
            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD H INNER JOIN PAYROLL_PAY_SUBPAYHEAD SH ON (H.PAYHEAD = SH.PAYHEAD)", "DISTINCT H.PAYHEAD", "H.PAYSHORT", "", "PAYSHORT");

            //FILL EMPLOYEE
            //objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS e left outer join " + ddlMonthYear.SelectedItem.Text + " t on (e.idno = t.idno)", "t.IDNO", "'['+ convert(nvarchar(150),t.IDNO) + ']' +' '+ FNAME + ' ' + MNAME + ' ' + LNAME", "t.IDNO>0 and t.STAFFNO =" + ddlStaffNo.SelectedValue, "t.IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SubPayHead_Reort.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void FillPayhead()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO<>0", "STAFFNO");
            lstStaffName.Items.Clear();
            //lstStaffName.Items.Add(new ListItem("Please Select", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lstStaffName.DataSource = ds;
                lstStaffName.DataTextField = "STAFF";
                lstStaffName.DataValueField = "STAFFNO";
                lstStaffName.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SubPayHead_Reort.FillPayhead() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_SubPayHead_Reort.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SubPayHead_Reort.aspx");
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
                objUCommon.ShowError(Page, "Pay_SubPayHead_Reort.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

  
    protected void btnShowReport_Click(object sender, EventArgs e)
    {

       
        if (lstStaffName.Items.Count > 0)
        {
            for (int i = 0; i < lstStaffName.Items.Count; i++)        
            {
                if (lstStaffName.Items[i].Selected)
                {
                    itemselectcnt += lstStaffName.Items[i].Value +",";
                }
            }
            
        }

        itemselectcnt = itemselectcnt.TrimEnd(',');
        itemselectcnt = itemselectcnt.Replace(',', '_');


        ShowReportEmployeePayslip("Subpayhead_Report", "PAY_SUB_PAYHEAD_REPORT.rpt");

    }

    private void ShowReportEmployeePayslip(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;           
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;            
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + itemselectcnt.ToString() + ",@P_COLLEGNO="+Convert.ToInt32(ddlCollege.SelectedValue)+",@P_PAYHEAD=" + ddlPayhead.SelectedValue.ToString() + ",@P_SUBPAYHEAD=" + ddlSubpayhead.SelectedValue + ",@P_CHEQUE_NO=" + txtAccNo.Text.Trim() + ",Submit_Date=" + Convert.ToDateTime(txtFromDate.Text.ToString()).ToString("dd/MM/yyyy") + ",@P_COLLEGE_CODE=24";

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
          
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.ShowReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        this.FillSubPayHead();

    }

    private void FillSubPayHead()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubpayhead, "PAYROLL_PAY_SUBPAYHEAD", "SUBHEADNO", "SHORTNAME", "SHORTNAME IS NOT NULL AND PAYHEAD='" + ddlPayhead.SelectedValue + "'", "SUBHEADNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillSubPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillCollege()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}
