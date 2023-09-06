using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Web;
using System.IO;
using System.Data;

public partial class VEHICLE_MAINTENANCE_Transaction_BusTokenIssue : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController ObjCon = new VMController();
    VM ObjEnt = new VM();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                }

                txtcomdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindListview();
                ViewState["Action"] = "Add";

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListview()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_BUS_TOKEN_ISSUE", "*", "", "", "");

            lvBustokenissue.DataSource = ds;
            lvBustokenissue.DataBind();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void Clear()
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Total", "<script type='text/javascript'>Clear();</script>", false);
            ViewState["Action"] = "Add";
            txtcomdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           

            ObjEnt.BUS_TOKEN_ISSUE_DATE = Convert.ToDateTime(txtcomdate.Text);
            ObjEnt.TOKEN_30 = Convert.ToInt32(txtToken30.Text);
            ObjEnt.TOKEN_40 = Convert.ToInt32(txtToken40.Text);
            ObjEnt.TOKEN_30_AMOUNT = Convert.ToDouble(txtAmount30.Text);
            ObjEnt.TOKEN_40_AMOUNT = Convert.ToDouble(txtAmount40.Text);
            ObjEnt.GRAND_TOTAL = Convert.ToDouble(txtGrandTotal.Text);
            if (ViewState["Action"].ToString().Equals("edit"))
            {
                ObjEnt.BTNO = Convert.ToInt32(ViewState["BTNO"].ToString());
            }

            //--======start===Shaikh Juned 5-09-2022
            String tokenIssueDate = Convert.ToDateTime(txtcomdate.Text).ToString("yyyy-MM-dd HH:mm:ss");
            DataSet ds = objCommon.FillDropDown("VEHICLE_BUS_TOKEN_ISSUE", "BUS_TOKEN_ISSUE_DATE", "TOKEN_40,TOKEN_30", "BUS_TOKEN_ISSUE_DATE='" + tokenIssueDate + "'AND TOKEN_40 ='" + Convert.ToInt32(txtToken40.Text) + "' AND TOKEN_30 = '" + Convert.ToInt32(txtToken30.Text) + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string bus_token_issue_date = Convert.ToDateTime(dr["BUS_TOKEN_ISSUE_DATE"]).ToString("dd/MM/yyyy");
                    string token_40 = dr["TOKEN_40"].ToString();
                    string token_30 = dr["TOKEN_30"].ToString();
                    if (bus_token_issue_date == txtcomdate.Text & token_40 == txtToken40.Text & token_30 == txtToken30.Text)
                    {
                        objCommon.DisplayMessage(this.Page, "Bus Token Is Already Exist.", this.Page);
                        return;
                    }

                }
            }
            //---========end=====

            CustomStatus cs = (CustomStatus)ObjCon.InsUpdBusTokenIssue(ObjEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record saved successfully.");
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                MessageBox("Record Updated successfully.");
            }
            else
            {
                MessageBox("Record Already Exist.");
            }
            Clear();
            BindListview();

        }
         catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DivControls.Visible = false;
        DivReport.Visible = true;
    }
    protected void btnEditBtn_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton IMG = sender as ImageButton;
            ViewState["BTNO"] = IMG.CommandArgument;
            ViewState["Action"] = "edit";
            DataSet ds = objCommon.FillDropDown("VEHICLE_BUS_TOKEN_ISSUE", "*", "", "BTNO=" + ViewState["BTNO"].ToString(), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcomdate.Text = ds.Tables[0].Rows[0]["BUS_TOKEN_ISSUE_DATE"].ToString();
                txtAmount30.Text = ds.Tables[0].Rows[0]["TOKEN_30_AMOUNT"].ToString();
                txtAmount40.Text = ds.Tables[0].Rows[0]["TOKEN_40_AMOUNT"].ToString();
                txtToken30.Text = ds.Tables[0].Rows[0]["TOKEN_30"].ToString();
                txtToken40.Text = ds.Tables[0].Rows[0]["TOKEN_40"].ToString();
                txtGrandTotal.Text = ds.Tables[0].Rows[0]["GRAND_TOTAL"].ToString();
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_LeaveAndHolidayEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }
    protected void btnShowRpt_Click(object sender, EventArgs e)
    {
        ShowTransportReport("BUS_TOKEN_ISSUE_REPORT", "cryBusTokenIssueReport.rpt");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = txtToDate.Text = string.Empty;
        DivControls.Visible = true;
        DivReport.Visible = false;
       
    }
    private void ShowTransportReport(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd 00:00:00") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd 00:00:00") + ",@Fromdate=" +"From Date : "+ txtFromDate.Text + ",@Todate="+" - To Date : "+txtToDate.Text;
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
}