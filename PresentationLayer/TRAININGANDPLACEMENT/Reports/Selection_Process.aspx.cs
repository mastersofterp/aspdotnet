using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class TRAININGANDPLACEMENT_Reports_Selection_Process : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objStud = new TPController();
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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                
                Fill_Schedule();                
                
                //ddlSceduleAll.Visible = false;

            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    protected void Fill_Schedule()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"]) == 8)
            {
                ds = objStud.FillSchedule_for_Rpt("C", Convert.ToInt32(Session["idno"]));
            }
            else
            {
                //ds = objStud.FillSchedule_for_Rpt("C", 0);
                ds = objStud.FillSchedule_for_Rpt("C,F", 0);
            }

            ddlSchedule.Items.Clear();
            ddlSchedule.Items.Add("Please Select");
            ddlSchedule.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSchedule.DataSource = ds;
                ddlSchedule.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSchedule.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSchedule.DataBind();
                ddlSchedule.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Selection_Process.Fill_Schedule ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ChkViewDetails.Checked == true)
            {
                ShowReport("SelectionList", "TP_SelectionStuList.rpt");
            }
            else
            {
                ShowReport("SelectionList", "TP_SelectionStuList_New.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Selection_Process.btnShow_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            char Rstatus = 'R';
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;

            url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSchedule.SelectedValue) + ",@RptTitle=" + Convert.ToString(ddlRound.SelectedItem.Text.ToUpper()) + ",@P_ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue) + ",@P_REPORT=" + Convert.ToChar(Rstatus) + ",username=" + Convert.ToString(Session["userfullname"]) + ",@schedule_name="+ddlSchedule.SelectedItem.Text.ToUpper();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchedule.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlRound, "ACD_TP_STUDENT_SELECTION_PROCESS", "ROUNDNO", "ROUNDNAME", "SCHEDULENO=" + Convert.ToInt32(ddlSchedule.SelectedValue), "ROUNDNO");
        }
    }
}
