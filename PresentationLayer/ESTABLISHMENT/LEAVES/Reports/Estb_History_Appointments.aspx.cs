//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:15-01-2016
//PURPOSE:History for Appointments of Head & Deans
//==============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Drawing;
public partial class Estb_History_Appointments : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLC = new LeavesController();
    Leaves objLM = new Leaves();
    DataTable dtAttendance = new DataTable();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        
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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
              
               
             }  
        }
    }   
   
    private void Clear()
    {
        txtStartDt.Text = System.DateTime.Now.ToShortDateString();
        txtEndDt.Text = string.Empty;
        ddlType.SelectedIndex = 0;

        txtEndDt.Enabled = true;
        imgCalEndDt.Visible = true;
        chkAsOnDate.Checked = false;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
          Clear();
    }

 

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }


    protected void btnReportHistory_Click(object sender, EventArgs e)
    {
        // DateTime dt = System.DateTime.MinValue;
        if (txtStartDt.Text == string.Empty && txtEndDt.Text == string.Empty)
        {
            MessageBox("Please select date");
            return;
        }
        else
        {
            ShowReport_History("History", "Estb_AppointmentHead_DeanReport_History.rpt");
        }
    }
    private void ShowReport_History(string reportTitle, string rptFileName)
    {
        try
        {
            EmpMaster objEM = new EmpMaster();
            // string start_date = "", end_date = "" , Script= string.Empty;
            string Script = string.Empty;
            string type = string.Empty;


            type = ddlType.SelectedValue;

            // DateTime start_date = System.DateTime.MinValue, end_date = System.DateTime.MinValue;
            string start_date = "", end_date = "";


            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            string frmdate = txtStartDt.Text.ToString().Trim();
            string todate = txtEndDt.Text.ToString().Trim();
            if (txtStartDt.Text != string.Empty)
            {

                start_date = Convert.ToDateTime(txtStartDt.Text).ToString("yyyy-MM-dd");
            }
           
            if (chkAsOnDate.Checked == true)
            {
                txtEndDt.Text = txtStartDt.Text;
            }

            if (txtEndDt.Text != string.Empty)
            {

                end_date = Convert.ToDateTime(txtEndDt.Text).ToString("yyyy-MM-dd");
            }

            url += "&param=@P_FROMDATE=" + start_date + ",@P_TODATE=" + end_date + ",@P_TYPE=" + type + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_STARTDATE="+ txtStartDt.Text +",@P_ENDDATE="+txtEndDt.Text;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void chkAsOnDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAsOnDate.Checked == true)
        {
            if (txtStartDt.Text != string.Empty)
            {
                txtEndDt.Text = txtStartDt.Text;
                txtEndDt.Enabled = false;
                imgCalEndDt.Visible = false;
            }
            else
            {
                txtEndDt.Enabled = false;
                imgCalEndDt.Visible = false;
            }
        }
        else
        {
            txtEndDt.Enabled = true;
            imgCalEndDt.Visible = true;
        }
    }
    protected void txtStartDt_TextChanged(object sender, EventArgs e)
    {
        if (chkAsOnDate.Checked == true)
        {
            txtEndDt.Text = txtStartDt.Text;
        }
    }
}