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

public partial class PAYROLL_TRANSACTIONS_Pay_WithheldWithdrawn : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    WithheldWithdrawnController objWWC = new WithheldWithdrawnController();
    WithheldWithDrawn objWWE = new WithheldWithDrawn();


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

                PopulateDropDownList();
                pnlWithheldWithdrawn.Visible = false;
                gdrpaid.Visible = false;
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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_WithheldWithdrawn.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_WithheldWithdrawn.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL MONTH YEAR 
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL COLLEGE
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");


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
    protected void btnShow_Click(object sender, EventArgs e)
    {

        pnlWithheldWithdrawn.Visible = false;
        btnSave.Enabled = false;
        int salcount = Convert.ToInt32(objCommon.LookUp("PAYROLL_SALFILE", "COUNT(1)", "MONYEAR='" + ddlMonthYear.SelectedItem.Text + "' AND COLLEGE_NO = " + ddlCollege.SelectedValue + " AND STAFFNO = " + ddlStaffNo.SelectedValue + " OR " + ddlStaffNo.SelectedValue+"=0"));

        if (salcount == 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Salary for " + ddlMonthYear.SelectedItem.Text + " is not process for " + ddlCollege.SelectedItem.Text + " , " + ddlStaffNo.SelectedItem.Text + " staff ", this);
        }
        else
        {
            bool sallock = Convert.ToBoolean(objCommon.LookUp("PAYROLL_SALFILE", "SALLOCK", "MONYEAR='" + ddlMonthYear.SelectedItem.Text + "' AND COLLEGE_NO = " + ddlCollege.SelectedValue + " AND STAFFNO = " + ddlStaffNo.SelectedValue + " OR " + ddlStaffNo.SelectedValue + "=0"));

            if (sallock == true)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Salary for " + ddlMonthYear.SelectedItem.Text + " is locked permanently for " + ddlCollege.SelectedItem.Text + " , " + ddlStaffNo.SelectedItem.Text + " staff ", this);
            }
            else
            {
                BindListViewList();
            }
        }
    }



    private void ShowReportEmployeeAbstractSalary(string reportTitle, string rptFileName)
    {
        try
        {
            int a = 0;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Payroll")));
            //url += "Reports/CommonReport.aspx?";
            if (a == 1)
            {
                url += "pagetitleForEmployeeCummulativeAbstractSalary=" + reportTitle;
                url += "&pathForEmployeeCummulativeAbstractSalary=~,Reports,Payroll," + rptFileName + "&@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "&@P_IDNO=0";
                url += "&paramForEmployeeCummulativeAbstractSalary=username=" + Session["username"].ToString();
            }
            else
            {
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Payroll," + rptFileName;
                // url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue)+",username=Admin";
                url += "&param=@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_IDNO=0" + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPTYPENO=" + 0 + ",username=Admin";
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListViewList()
    {

        try
        {
            gdrpaid.Visible = false;
            pnlWithheldWithdrawn.Visible = true;
            objWWE.MONTHYEAR = ddlMonthYear.SelectedItem.Text;
            objWWE.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);
            objWWE.STAFFNO = Convert.ToInt32(ddlStaffNo.SelectedValue);
            objWWE.ORDERBY = ddlOrderBy.SelectedItem.Text;

            DataSet ds = objWWC.GetWithHeldWithDrawnDetails(objWWE);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }

            lvWithheldWithdrawn.DataSource = ds;
            lvWithheldWithdrawn.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_WithheldWithdrawn.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BtnAbst_Click(object sender, EventArgs e)
    {
        ShowReportEmployeeAbstractSalary("Employee_Abstract_Salary", "rptAbstract_Salary_Count.rpt");
    }

    protected void btnsummary_Click(object sender, EventArgs e)
    {
        try
        {
            pnlWithheldWithdrawn.Visible = false;
            gdrpaid.Visible = true;
            objWWE.MONTHYEAR = ddlMonthYear.SelectedItem.Text;
            objWWE.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);
            objWWE.STAFFNO = Convert.ToInt32(ddlStaffNo.SelectedValue);
            objWWE.ORDERBY = ddlOrderBy.SelectedItem.Text;

            DataSet ds = objWWC.GetWithHeldWithDrawnDetails_PaidUpiad(objWWE);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }

            gdrpaid.DataSource = ds;
            gdrpaid.DataBind();

        }
        catch (Exception)
        {

            throw;
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int checkcount = 0;
            string colvalues;
            colvalues = string.Empty;

            foreach (ListViewDataItem lvitem in lvWithheldWithdrawn.Items)
            {
                objWWE.MONTHYEAR = ddlMonthYear.SelectedItem.Text;
                objWWE.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);
                objWWE.STAFFNO = Convert.ToInt32(ddlStaffNo.SelectedValue);

                CheckBox chk = lvitem.FindControl("ChkWithheldStatus") as CheckBox;
                TextBox txtWithheldRemark = lvitem.FindControl("txtWithHeldRemark") as TextBox;
                if (chk.Checked)
                {
                    checkcount = 1;
                    objWWE.IDNO = Convert.ToInt32(chk.ToolTip);
                    objWWE.WITHHELDSTATUS = true;
                    objWWE.WITHHELDREMARK = txtWithheldRemark.Text;
                    CustomStatus cs = (CustomStatus)objWWC.InsertUpdateWithHeldWithDrawn(objWWE);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                        objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
                    }
                }
                else
                {
                    colvalues = colvalues + chk.ToolTip + ",";

                }
            }

            objWWE.MONTHYEAR = ddlMonthYear.SelectedItem.Text;
            objWWE.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);
            objWWE.STAFFNO = Convert.ToInt32(ddlStaffNo.SelectedValue);
            objWWE.WITHHELDSTATUS = false;
            objWWE.WITHHELDREMARK = string.Empty;
            colvalues = colvalues.Substring(0, colvalues.Length - 1);
            objWWE.IDNOS = colvalues;
            CustomStatus cs1 = (CustomStatus)objWWC.UpdateWithHeldWithDrawn(objWWE);

            if (checkcount == 0)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please check withheld status", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_WithheldWithdrawn.btnSave-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlWithheldWithdrawn.Visible = false;
        btnSave.Enabled = false;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlWithheldWithdrawn.Visible = false;
        btnSave.Enabled = false;
    }
    protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlWithheldWithdrawn.Visible = false;
        btnSave.Enabled = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Added on 14-4-2017 By Saket Singh to clear the fields
        ddlMonthYear.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlStaffNo.SelectedIndex = 0;
        ddlOrderBy.SelectedIndex = 0;
        pnlWithheldWithdrawn.Visible = false;

    }
}