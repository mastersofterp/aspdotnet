using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Dispatch_Transactions_Search_Detail : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();

    protected void Page_Load(object sender, EventArgs e)
    {
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = null;
                    ViewState["action"] = null;
                    FillDropdown();
                    // BindLetter();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Search_Detail.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void FillDropdown()
    {
        if (radlSelect.SelectedValue == "I")
        {
            objCommon.FillDropDownList(ddlFrmTo, "USER_ACC", "UA_IDNO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlFrmTo, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void lvLetterDetails_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        Label IoNO = dataitem.FindControl("lbIoNo") as Label;
        int IoNo = Convert.ToInt32(IoNO.Text);


        int type = 0;

        if (radlSelect.SelectedValue == "I")
        {
            type = 0;
        }
        else
        {
            type = 1;
        }

        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {
            DataSet ds = objIOtranc.GetMovementStatus(IoNo, type);
            lv.DataSource = ds;
            lv.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Movement.lvLetterDetails_Bound --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void BindLetter()
    {
        try
        {
            int type = 0;
            if (radlSelect.SelectedValue == "I")
            {
                type = 0;
            }
            else
            {
                type = 1;
            }
            DateTime DiF = DateTime.MinValue;
            DateTime DiT = DateTime.MinValue;
            DateTime UF = DateTime.MinValue;
            DateTime UTo = DateTime.MinValue;

            if (!txtFrmDate.Text.Equals(string.Empty)) DiF = Convert.ToDateTime(txtFrmDate.Text);
            if (!txtToDate.Text.Equals(string.Empty)) DiT = Convert.ToDateTime(txtToDate.Text);
            if (!txtUFrmDt.Text.Equals(string.Empty)) UF = Convert.ToDateTime(txtUFrmDt.Text);
            if (!txtUToDt.Text.Equals(string.Empty)) UTo = Convert.ToDateTime(txtUToDt.Text);


            DataSet ds = objIOtranc.Disp_Letter(DiF, DiT, UF, UTo, Convert.ToString(txtRefNo.Text.Trim()), Convert.ToInt32(ddlFrmTo.SelectedValue), type);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLetterDetails.DataSource = ds;
                lvLetterDetails.DataBind();
            }
            else
            {
                lvLetterDetails.DataSource = null;
                lvLetterDetails.DataBind();
                objCommon.DisplayMessage(this.updActivity, "Record Not Found.", this.Page);
                return;
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("LetterDetails", "LetterSearchDetails.rpt");
    }

    protected void dpUserDet_PreRender(object sender, EventArgs e)
    {

    }

    protected void dpLett_PreRender(object sender, EventArgs e)
    {
        BindLetter();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFrmDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text)) == 1)
                {
                    objCommon.DisplayMessage(this.updActivity, "From Date Can Not Be Greater Than to Date.", this.Page);
                    txtFrmDate.Focus();
                    return;
                }
            }
            if (!txtUFrmDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtUFrmDt.Text), Convert.ToDateTime(txtUToDt.Text)) == 1)
                {
                    objCommon.DisplayMessage(this.updActivity, "From Date Can Not Be Greater Than to Date.", this.Page);

                    txtUFrmDt.Focus();
                    return;
                }
            }
            if (txtFrmDate.Text == "" && txtRefNo.Text == "" && txtToDate.Text == "" && txtUFrmDt.Text == "" && txtUToDt.Text == ""
                && ddlFrmTo.SelectedValue == "0")
            {
                //lblReq.Text = "Please Select Atleast One Option";
                objCommon.DisplayMessage(this.updActivity, "Please Enter Atleast One Field.", this.Page);
                return;
            }
            else
            {
                BindLetter();
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Search_Detail.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void radlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLetter();
        txtFrmDate.Text = string.Empty;
        txtRefNo.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtUFrmDt.Text = string.Empty;
        txtUToDt.Text = string.Empty;
        ddlFrmTo.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFrmDate.Text = string.Empty;
        txtRefNo.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtUFrmDt.Text = string.Empty;
        txtUToDt.Text = string.Empty;
        ddlFrmTo.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;
        //Modified by Saahil Trivedi 17-02-2022
        //BindLetter();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("DISPATCH")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,DISPATCH," + rptFileName;

            int type = 0;
            if (radlSelect.SelectedValue == "I")
            {
                type = 0;
            }
            else
            {
                type = 1;
            }
            DateTime DiF = DateTime.MinValue;
            DateTime DiT = DateTime.MinValue;
            DateTime UF = DateTime.MinValue;
            DateTime UTo = DateTime.MinValue;

            if (!txtFrmDate.Text.Equals(string.Empty)) DiF = Convert.ToDateTime(txtFrmDate.Text);
            if (!txtToDate.Text.Equals(string.Empty)) DiT = Convert.ToDateTime(txtToDate.Text);
            if (!txtUFrmDt.Text.Equals(string.Empty)) UF = Convert.ToDateTime(txtUFrmDt.Text);
            if (!txtUToDt.Text.Equals(string.Empty)) UTo = Convert.ToDateTime(txtUToDt.Text);



            if (txtFrmDate.Text.Trim() != string.Empty && txtToDate.Text.Trim() != string.Empty)
            {
                url += "&param=@P_FROMDT=" + Convert.ToDateTime(txtFrmDate.Text).ToString("dd/MM/yyyy") + ",@P_TODT=" + Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
            }
            else
            {
                url += "&param=@P_FROMDT=null,@P_TODT=null";
            }
            if (txtUFrmDt.Text.Trim() != string.Empty && txtUToDt.Text.Trim() != string.Empty)
            {
                url += ",@P_UFROMDT=" + Convert.ToDateTime(txtUFrmDt.Text).ToString("dd/MM/yyyy") + ",@P_UTODT=" + Convert.ToDateTime(txtUToDt.Text).ToString("dd/MM/yyyy") + ",@P_DISPREREFNO=" + Convert.ToString(txtRefNo.Text.Trim()) + ",@P_USER=" + Convert.ToInt32(ddlFrmTo.SelectedValue) + ",@P_TYPE=" + type + ",@P_USER_TYPE=" + ddlUserType.SelectedValue;
            }
            else
            {
                if (txtRefNo.Text.Trim() != string.Empty)
                {
                    url += ",@P_UFROMDT=null,@P_UTODT=null,@P_DISPREREFNO=" + Convert.ToString(txtRefNo.Text.Trim()) + ",@P_USER=" + Convert.ToInt32(ddlFrmTo.SelectedValue) + ",@P_TYPE=" + type + ",@P_USER_TYPE=" + ddlUserType.SelectedValue;

                }
                else
                {
                    url += ",@P_UFROMDT=null,@P_UTODT=null,@P_DISPREREFNO=null,@P_USER=" + Convert.ToInt32(ddlFrmTo.SelectedValue) + ",@P_TYPE=" + type + ",@P_USER_TYPE=" + ddlUserType.SelectedValue;
                }
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_Dispatch.btnShow_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}