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
using System.Data.SqlClient;

public partial class PAYROLL_REPORTS_OthReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //ConnectionStrings
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


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
                    //if (Request.QueryString["pageno"] != null)
                    //{
                    //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //}
                }

                //Populate DropdownList
                PopulateDropDownList();

                //Fill Listbox
                FillPayhead();

                //Select list particular Head
                lstParticularColumn.SelectedIndex = 0;

                //Get focus on Textbox


                //Staff No. DropdownList Enabled False
                //ddlStaffNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?OtherReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OtherReport.aspx");
        }
    }

    private void FillPayhead()
    {
        try
        {
            //This method is for filling the Payhead in Pay_SelectedField Report page on To_Date textbox Click
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_COLLEGE_CODE", Session["colcode"].ToString());
            int clgcode;
            clgcode = Convert.ToInt32(Session["colcode"]);
            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_DROPDOWN_PAYHEADS", objParams);
            lstParticularColumn.Items.Clear();
            lstParticularColumn.Items.Add(new ListItem("Please Select", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lstParticularColumn.DataSource = ds;
                lstParticularColumn.DataTextField = "PAYSHORT";
                lstParticularColumn.DataValueField = "PAYHEAD";
                lstParticularColumn.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.FillPayhead() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void PopulateDropDownList()
    {
        try
        {
            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL MONTH YEAR 
            objCommon.FillDropDownList(ddlMonth, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");

            //FILL COLLEGE
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportColumnPayHead(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitleColumnPayhead=" + reportTitle;
            //url += "&pathColumnPayhead=~,Reports,Payroll," + rptFileName + "&@P_FROM_DATE=" + txtFromDate.Text.Trim() + "&@P_TO_DATE=" + txtToDate.Text.Trim() + "&@P_IDNO=" + Convert.ToInt32(ddlEmployeeNo.SelectedValue) + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_PAYHEAD=" + (lstParticularColumn.SelectedValue) + "&@P_MONTH=" + (lstMonth.SelectedItem.ToString());
            //url += "&paramColumnPayhead=username=" + Session["username"].ToString() + ",From_Date=" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",To_Date=" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MMM/yyyy") + ",Month1=" + "JAN" + ",Month2=" + "FEB" + ",Month3=" + "MAR" + ",Month4=" + "APR" + ",Month5=" + "MAY" + ",Month6=" + "JUN" + ",Month7=" + "JUL" + ",Month8=" + "AUG" + ",Month9=" + "SEP" + ",Month10=" + "OCT" + ",Month11=" + "NOV" + ",Month12=" + "DEC";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Annual_Salary_Detail.ShowReportColumnPayHead() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string selectedItem = string.Empty;
            string selectedText = string.Empty;
            int d = 0;
            string b = "0";
            string c = "0";
            string shoreport = string.Empty;
            if (lstParticularColumn.Items.Count > 0)
            {
                for (int i = 0; i < lstParticularColumn.Items.Count; i++)
                {
                    if (lstParticularColumn.Items[i].Selected)
                    {
                        //selectedItem =selectedItem + lstParticularColumn.Items[i].Text.Trim() + ",";
                        selectedItem = selectedItem + lstParticularColumn.Items[i].Value.ToString().Trim() + "$";
                        selectedText = selectedText + lstParticularColumn.Items[i].Text.Trim() + ",";
                        //insert command
                        d = d + 1;
                    }
                }


                selectedItem = selectedItem.Remove(selectedItem.Length - 1);
                selectedText = selectedText.Remove(selectedText.Length - 1);
                string[] a = selectedText.Split(',');
                if (d == 1)
                {
                    b = a[0];
                }
                else if (d == 2)
                {
                    b = a[0];
                    c = a[1];
                }

                if (ddlShowInReport.SelectedValue == "1")
                {
                    shoreport = "BANKACC_NO";
                }
                if (ddlShowInReport.SelectedValue == "2")
                {
                    shoreport = "GPF_NO";
                }
                if (ddlShowInReport.SelectedValue == "3")
                {
                    shoreport = "PAN_NO";
                }
                if (ddlShowInReport.SelectedValue == "0")
                {
                    shoreport = "";
                }


            }


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_PAYHEADS=" + selectedItem + ",@P_MONYEAR=" + ddlMonth.SelectedItem.Text + ",@P_STAFFNO=" + ddlStaffNo.SelectedValue + ",@P_SHOWINREPORT=" + shoreport + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HEAD1=" + b + ",@P_HEAD2=" + c + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportPayhead(string reportTitle, string rptFileName)
    {
        try
        {

            string selectedItem = string.Empty;
            string selectedText = string.Empty;
            int d = 0;
            string b = "0";
            string c = "0";
            string shoreport = string.Empty;
            if (lstParticularColumn.Items.Count > 0)
            {
                for (int i = 0; i < lstParticularColumn.Items.Count; i++)
                {
                    if (lstParticularColumn.Items[i].Selected)
                    {
                        //selectedItem =selectedItem + lstParticularColumn.Items[i].Text.Trim() + ",";
                        selectedItem = selectedItem + lstParticularColumn.Items[i].Value.ToString().Trim() + "$";
                        selectedText = selectedText + lstParticularColumn.Items[i].Text.Trim() + ",";
                        //insert command
                        d = d + 1;
                    }
                }


                selectedItem = selectedItem.Remove(selectedItem.Length - 1);
                selectedText = selectedText.Remove(selectedText.Length - 1);
                string[] a = selectedText.Split(',');
                if (d == 1)
                {
                    b = a[0];
                }
                else if (d == 2)
                {
                    b = a[0];
                    c = a[1];
                }

                if (ddlShowInReport.SelectedValue == "1")
                {
                    shoreport = "BANKACC_NO";
                }
                if (ddlShowInReport.SelectedValue == "2")
                {
                    shoreport = "GPF_NO";
                }
                if (ddlShowInReport.SelectedValue == "3")
                {
                    shoreport = "PAN_NO";
                }
                if (ddlShowInReport.SelectedValue == "0")
                {
                    shoreport = "";
                }
            }


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_PAYHEADS=" + selectedItem + ",@P_MONYEAR=" + ddlMonth.SelectedItem.Text + ",@P_STAFFNO=" + ddlStaffNo.SelectedValue + ",@P_SHOWINREPORT=" + shoreport + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HEAD1=" + b + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            int itemselectcnt = 0;
            if (lstParticularColumn.Items.Count > 0)
            {
                for (int i = 0; i < lstParticularColumn.Items.Count; i++)
                {
                    if (lstParticularColumn.Items[i].Selected)
                    {
                        itemselectcnt = itemselectcnt + 1;
                    }
                }

                if (itemselectcnt == 1)
                {
                    ShowReportPayhead("Report_For_Particular_Column_Payhead", "Other_Selected_Fields_Single_Payhead.rpt");
                }
                else if (itemselectcnt == 2)
                {
                    ShowReport("Report_For_Particular_Column_Payhead", "Other_Selected_Fields.rpt");
                }
                else
                    objCommon.DisplayMessage("Please Select Two Heads", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.btnShowReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lstMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_SelectedField.lstMonth_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
}
