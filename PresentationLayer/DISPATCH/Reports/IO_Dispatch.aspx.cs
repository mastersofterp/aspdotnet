using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Dispatch_Reports_IO_Dispatch : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,TPController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIO = new IOTranController();
    IOTranController objO = new IOTranController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                this.CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // objCommon.FillDropDownList(ddlDptNm, "ADMN_IO_DEPARTMENT", "DEPTNO", "DEPARTMENTNAME", "", "DEPARTMENTNAME");
                //objCommon.FillDropDownList(ddlDptNm, "ADMN_IO_TRAN A INNER JOIN  ADMN_IO_DEPARTMENT B ON A.TODEPT = B.DEPTNO", "distinct TODEPT", "DEPARTMENTNAME", "iotype='I'", "DEPARTMENTNAME");
                objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "POSTTYPENO > 0", "POSTTYPENAME");
                objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                //objCommon.FillDropDownList(ddlFrmTo, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
                objCommon.FillDropDownList(ddlFrmTo, "USER_ACC", "UA_IDNO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
                objCommon.FillDropDownList(ddlCarrier, "ADMN_IO_CARRIER", "CARRIERNO", "CARRIERNAME", "STATUS=0", "CARRIERNO");
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
                Response.Redirect("~/notauthorized.aspx?page=Weight.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Weight.aspx");
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCarrier.SelectedIndex = 0;
        ddlCheque.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlFrmTo.SelectedIndex = 0;
        ddlLCat.SelectedIndex = 0;
        ddlPostType.SelectedIndex = 0;
        txtFrmDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        lvLetterDetails.DataSource = null;
        lvLetterDetails.DataBind();
        lvLetterDetails.Visible = false;
        lvOutward.DataSource = null;
        lvOutward.DataBind();
        lvOutward.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindLetter();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_Dispatch.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void radlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radlSelect.SelectedValue.ToString().Equals("I"))
        {
            objCommon.FillDropDownList(ddlFrmTo, "USER_ACC", "UA_IDNO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
            lvLetterDetails.DataSource = null;
            lvLetterDetails.DataBind(); 
            

        }
        else if (radlSelect.SelectedValue.ToString().Equals("O"))
        {

            objCommon.FillDropDownList(ddlFrmTo, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
            lvLetterDetails.DataSource = null;
            lvLetterDetails.DataBind(); 
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFrmDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text)) == 1)
                {
                    objCommon.DisplayMessage(this.Page, "From Date Can Not Be Greater Than to Date.", this.Page);
                    txtFrmDate.Focus();
                    return;
                }
            }
            if (radlSelect.SelectedValue == "I")
            {
                DataSet ds = objIO.verifyInwardRecord(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlFrmTo.SelectedValue), Convert.ToInt32(ddlPostType.SelectedValue), Convert.ToInt32(ddlCarrier.SelectedValue), Convert.ToInt32(ddlLCat.SelectedValue), Convert.ToInt32(ddlCheque.SelectedValue), Convert.ToChar(ddlUserType.SelectedValue), 1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("Dispatch", "InwardRegister.rpt");
                }
                else
                {
                    objCommon.DisplayMessage("No Records found", this.Page);
                }
            }
            else if (radlSelect.SelectedValue == "O")
            {

                DataSet ds = objO.VerifyOutwardRecord(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlFrmTo.SelectedValue), Convert.ToInt32(ddlPostType.SelectedValue), Convert.ToInt32(ddlCarrier.SelectedValue), Convert.ToInt32(ddlLCat.SelectedValue), Convert.ToInt32(ddlCheque.SelectedValue), 1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("Dispatch", "OutwardRegister.rpt");
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found", this.Page);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_Dispatch.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("DISPATCH")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,DISPATCH," + rptFileName;

            if (radlSelect.SelectedValue == "I")
            {
                //url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").Trim() + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy").Trim() + "," + "@P_DEPTNO=" + Convert.ToInt32(ddlDptNm.SelectedValue) + "," + "username=" + Session["userfullname"].ToString() + ",@P_TYPE=1";
                url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + ",@P_DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + ",@P_USERNO=" + Convert.ToInt32(ddlFrmTo.SelectedValue) + ",@P_POSTTYPE=" + Convert.ToInt32(ddlPostType.SelectedValue) + ",@P_CHEQUE=" + Convert.ToInt32(ddlCheque.SelectedValue) + ",@P_USERTYPE=" + Session["usertype"].ToString() + ",@P_TYPE=1";
            }
            else if (radlSelect.SelectedValue == "O")
            {
                //url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy").Trim() + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy").Trim() + "," + "@P_DEPTNO=" + Convert.ToInt32(ddlDptNm.SelectedValue) + "," + "username=" + Session["userfullname"].ToString() + ",@P_TYPE=1";
                url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + ",@P_DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + ",@P_USERNO=" + Convert.ToInt32(ddlFrmTo.SelectedValue) + ",@P_POSTTYPE=" + Convert.ToInt32(ddlPostType.SelectedValue) + ",@P_CARRIERNO=" + Convert.ToInt32(ddlCarrier.SelectedValue) + ",@P_LETTERCAT=" + Convert.ToInt32(ddlLCat.SelectedValue) + ",@P_CHEQUE=" + Convert.ToInt32(ddlCheque.SelectedValue) + ",username=" + Session["userfullname"].ToString() + ",@P_TYPE=1";
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_Dispatch.btnShow_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpLett_PreRender(object sender, EventArgs e)
    {
        BindLetter();
    }
    private void BindLetter()
    {
        try
        {
            if (!txtFrmDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text)) == 1)
                {
                    objCommon.DisplayMessage(this.Page, "From Date Can Not Be Greater Than to Date.", this.Page);
                    txtFrmDate.Focus();
                    return;
                }
            }
            if (radlSelect.SelectedValue == "I")
            {
                DataSet ds = objIO.verifyInwardRecord(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlFrmTo.SelectedValue), Convert.ToInt32(ddlPostType.SelectedValue), Convert.ToInt32(ddlCarrier.SelectedValue), Convert.ToInt32(ddlLCat.SelectedValue), Convert.ToInt32(ddlCheque.SelectedValue), Convert.ToChar(ddlUserType.SelectedValue) , 1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvLetterDetails.DataSource = ds;
                    lvLetterDetails.DataBind();
                    lvLetterDetails.Visible = true;
                    lvOutward.Visible = false;
                }
                else
                {
                    lvLetterDetails.Visible = false;
                    lvOutward.Visible = false;
                }
            }
            else if (radlSelect.SelectedValue == "O")
            {
                DataSet ds = objO.VerifyOutwardRecord(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlFrmTo.SelectedValue), Convert.ToInt32(ddlPostType.SelectedValue), Convert.ToInt32(ddlCarrier.SelectedValue), Convert.ToInt32(ddlLCat.SelectedValue), Convert.ToInt32(ddlCheque.SelectedValue), 1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvOutward.DataSource = ds;
                    lvOutward.DataBind();
                    lvOutward.Visible = true;
                    lvLetterDetails.Visible = false;
                }
                else
                {
                    lvOutward.Visible = false;
                    lvLetterDetails.Visible = false;
                    objCommon.DisplayMessage("No Records Found", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Search_Detail.BindLetter --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlDepartment_SelectedIndexChange(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlFrmTo, "USER_ACC U INNER JOIN PAYROLL_SUBDEPT B ON (U.UA_EMPDEPTNO=B.SUBDEPTNO)", "U.UA_IDNO", "U.UA_FULLNAME", "UA_TYPE <> 2 and U.UA_IDNO is not null and U.UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "U.UA_IDNO");
            ddlFrmTo.Focus();
        }
        catch (Exception ex)
        {
            if
                (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.BindUser -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}