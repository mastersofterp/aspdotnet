using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;
using System.Net;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.NetworkInformation;
using BusinessLogicLayer.BusinessEntities;

public partial class MessChange_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                FillDropDown();
                 txtMonthYear.Text = DateTime.Now.ToString("MMM/yyyy");
            }
        }
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void ClearControl()
    {
        ddlyear.SelectedIndex = 0;
        ddldegree.SelectedIndex = 0;
    }
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO >0", "HOSTEL_SESSION_NO desc");
        ddlSession.SelectedIndex = 1;
        objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HBNO", "HOSTEL_NAME", "HBNO > 0", "HOSTEL_NAME");
        objCommon.FillDropDownList(ddldegree, "ACD_DEGREE D", "D.DEGREENO", "DEGREENAME", "DEGREENO >0", "DEGREENO");
        objCommon.FillDropDownList(ddlyear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
        objCommon.FillDropDownList(ddlmess, "ACD_HOSTEL_MESS", "MESS_NO", "MESS_NAME", "", "MESS_NO");
    }
    protected void btnprintreport_Click(object sender, EventArgs e)
    {
        ShowReport("Mess Change Detail", "rptMessChangeDetail.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_NO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_BLOCK_NO=" + Convert.ToInt32(ddlBlockNo.SelectedValue) + ",@P_ROOM_NO=" + Convert.ToInt32(ddlRoomNo.SelectedValue) + ",username=" + Session["username"].ToString(); 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_IDNO=0,@P_DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + ",@P_YEARNO=" + Convert.ToInt32(ddlyear.SelectedValue) + ",@P_SEMESTER=0,@P_BRANCHNO=0,@P_HBNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_BASE_MESS_NO=0,@P_CHANGE_MESS_NO=" + Convert.ToInt32(ddlmess.SelectedValue) + ",@P_UPDATE_MONTH=" + "1/" + txtMonthYear.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
