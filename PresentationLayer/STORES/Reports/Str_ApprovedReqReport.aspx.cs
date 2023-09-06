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
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class STORES_Reports_Str_ApprovedReqReport : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    Str_DSR_Entry_Controller objDSR = new Str_DSR_Entry_Controller();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();


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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Page Authority
                CheckPageAuthorization();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


            }
            fillDepartment();
            fillRequisition();
            Session["ReqNO"] = 0;
            Session["DeptNO"] = 0;

        }
        //divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Not Authorized");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Not Authorized");
        }
    }

    protected void fillDepartment()
    {
        objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "MDNO>0", "MDNAME");
    }

    protected void fillRequisition()
    {
        objCommon.FillDropDownList(ddlRequisitionNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' And STAPPROVAL='A' And MDNO=" + ddlDept.SelectedValue + "", "REQTRNO DESC");
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillRequisition();
        Session["DeptNO"] = int.Parse(ddlDept.SelectedValue);
    }

    protected void ddlReqNo_OnSelectedChanged(object sender, EventArgs e)
    {
        Session["ReqNO"] = int.Parse(ddlRequisitionNo.SelectedValue);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                objCommon.DisplayMessage(this.Page, "To Date Should Be Greater Than Or Equal To From Date.", this.Page);
                return;
            }
            FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            ToDate = Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
        }
        else
        {
            FromDate = null;
            ToDate = null;
        }
        ShowReport("RequisitionApproval", "ApprovalRequisitionWithSignReport.rpt",FromDate,ToDate);
    }

    private void ShowReport(string reportTitle, string rptFileName, string FromDate, string ToDate)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Stores," + rptFileName;
            url += "&param=@P_MDNO=" + ddlDept.SelectedValue + ",@P_REQTRNO=" + ddlRequisitionNo.SelectedValue + ",@P_FROM_DATE=" + FromDate + ",@P_TO_DATE=" + ToDate + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RequisitionApproval.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    void Clear()
    {
        ddlDept.SelectedIndex = 0;
        fillRequisition();
    }
}