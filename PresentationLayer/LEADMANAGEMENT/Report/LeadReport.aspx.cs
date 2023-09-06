using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.IO;

public partial class LEADMANAGEMENT_Report_LeadReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeadReportController objLRC = new LeadReportController();

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
                    DropDown();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Report_LeadReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            string ReportName = string.Empty;
            DataSet ds = null;
            if (rdoConsolitedLeadReport.Checked == true)
            {
                ds = objLRC.GetAllLeadExcelReport(Convert.ToInt32(ddlAdmissionBatch.SelectedValue),1);
                ReportName = "Consolited_Lead_" + ddlAdmissionBatch.SelectedItem.Text.Replace(' ', '_') + "_Report";
            }
            else if (rdoAllotedNotAllotedEnquiryList.Checked == true)
            {
                ds = objLRC.GetAllLeadExcelReport(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), 2);
                ReportName = "Enquiry_Lead_Alloted_and_Not_Alloted_" + ddlAdmissionBatch.SelectedItem.Text.Replace(' ', '_') + "_Report";
            }
            else 
            {
                objCommon.DisplayMessage(this.upLeadReport, "Please Select Report Type.!!", this.Page);
                return;
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=" + ReportName + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.upLeadReport, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnCancel_Click(object sender,EventArgs e)
    {
        rdoConsolitedLeadReport.Checked = false;
        ddlAdmissionBatch.SelectedIndex = 0;
    }

    #region User Define Function

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=LeadReport.aspx");
                }
            }
            else
            {
                Response.Redirect("~/notauthorized.aspx?page=LeadReport.aspx");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Report_LeadReport.CheckPageAuthorization-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void DropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Report_LeadReport.DropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    #endregion User Define Function
}