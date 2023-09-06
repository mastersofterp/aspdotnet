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

public partial class VEHICLE_MAINTENANCE_Transaction_TransportManagement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController objVMcont = new VMController();
    int ROUTEID = 0;
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
                this.FillDropdown();
                this.BindListView();
                //this.BindYear();
               // this.BindRouteName();
                //this.BindBoardingPoint();
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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TransportManagement.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseAllotment_Bulk.aspx");
        }
    }
    protected void FillDropdown()
    {
        DataSet ds = objCommon.FillDropDown("ACD_BRANCH", "DISTINCT BRANCHNO", "SHORTNAME", "BRANCHNO > 0", "SHORTNAME");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlBranchMultiCheck.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
        DataSet dsyear = objCommon.FillDropDown("ACD_YEAR", "DISTINCT YEAR", "YEARNAME", "YEAR > 0", "YEARNAME");
        for (int i = 0; i < dsyear.Tables[0].Rows.Count; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString(dsyear.Tables[0].Rows[i][1]), Convert.ToString(dsyear.Tables[0].Rows[i][0])));
        }
        DataSet dsroute = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "DISTINCT ROUTEID", "ROUTENAME", "ROUTEID > 0", "ROUTENAME");

        for (int i = 0; i < dsroute.Tables[0].Rows.Count; i++)
        {
            ddlrouteName.Items.Add(new ListItem(Convert.ToString(dsroute.Tables[0].Rows[i][1]), Convert.ToString(dsroute.Tables[0].Rows[i][0])));
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string Validate = hdnBranch.ToString().TrimEnd();
        string ValidateYear = hdnYear.ToString().TrimEnd();
        string ValidateRoute = hdnRoute.ToString().TrimEnd();
        string Validateboarding = hdnBroading.ToString().TrimEnd();
        if (string.IsNullOrEmpty(Validate))
        {
            
            //objCommon.DisplayMessage(this.updPanel, "Please select at least one Branch !", this.Page);
            return;
        }
        else if (string.IsNullOrEmpty(ValidateYear))
        {
            //objCommon.DisplayMessage(this.updPanel, "Please select at least one Year !", this.Page);
            return;
        }
        else if (string.IsNullOrEmpty(ValidateRoute))
        {
            //objCommon.DisplayMessage(this.updPanel, "Please select at least one Route Name !", this.Page);
            return;
        }
        else if (string.IsNullOrEmpty(Validateboarding))
        {
            //objCommon.DisplayMessage(this.updPanel, "Please select at least one Boarding Point !", this.Page);
            return;
        }
        else
        {

            this.ShowReport("TransportReport", "rptTransportManagement.rpt");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlBranchMultiCheck.Items.Clear();
        ddlYear.Items.Clear();
        ddlrouteName.Items.Clear();
        ddlboardingPoint.Items.Clear();
        this.FillDropdown();
    }
    //private void BindBranch()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_BRANCH", "DISTINCT BRANCHNO", "SHORTNAME", "BRANCHNO > 0", "BRANCHNO");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlBranchMultiCheck.DataSource = ds;
    //            ddlBranchMultiCheck.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlBranchMultiCheck.DataTextField = ds.Tables[0].Columns[1].ToString();
    //            ddlBranchMultiCheck.DataBind();
    //        }
    //        else
    //        {
    //            ddlBranchMultiCheck.Items.Clear();
    //            ddlBranchMultiCheck.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //        ddlBranchMultiCheck.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //private void BindYear()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_YEAR", "DISTINCT YEAR", "YEARNAME", "YEAR > 0", "YEAR");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlYear.DataSource = ds;
    //            ddlYear.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlYear.DataTextField = ds.Tables[0].Columns[1].ToString();
    //            ddlYear.DataBind();
    //        }
    //        else
    //        {
    //            ddlYear.Items.Clear();
    //            ddlYear.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //        ddlYear.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    //private void BindRouteName()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "DISTINCT ROUTEID", "ROUTENAME", "ROUTEID > 0", "ROUTEID");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlrouteName.DataSource = ds;
    //            ddlrouteName.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlrouteName.DataTextField = ds.Tables[0].Columns[1].ToString();
    //            ddlrouteName.DataBind();

    //        }
    //        else
    //        {
    //            ddlrouteName.Items.Clear();
    //            ddlrouteName.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //        ddlrouteName.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    //private void BindBoardingPoint()
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("VEHICLE_STOPMASTER ", "DISTINCT STOPID", "STOPNAME", "STOPID > 0", "STOPID");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlboardingPoint.DataSource = ds;
    //            ddlboardingPoint.DataValueField = ds.Tables[0].Columns[0].ToString();
    //            ddlboardingPoint.DataTextField = ds.Tables[0].Columns[1].ToString();
    //            ddlboardingPoint.DataBind();
    //        }
    //        else
    //        {
    //            ddlboardingPoint.Items.Clear();
    //            ddlboardingPoint.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //        ddlboardingPoint.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_BRANCHNO=" + hdnBranch.Value.ToString().TrimEnd('$') + ",@P_YEAR=" + hdnYear.Value.ToString().TrimEnd('$') + ",@P_ROUTEID=" + hdnRoute.Value.ToString().TrimEnd('$') + ",@P_STOPNO=" + hdnBroading.Value.ToString().TrimEnd('$') + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    protected void ddlrouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoardingPoint = GetBoardingPoint();
        BoardingPoint = BoardingPoint.Replace('$', ',');
        ViewState["BoardingPoint"] = BoardingPoint;
        string[] DegreeNo = BoardingPoint.Split(',');
        DataSet ds = objVMcont.FillBoarding(BoardingPoint);
        ddlboardingPoint.Items.Clear();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlboardingPoint.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }

       // DataSet ds = objVMcont.FillBoarding(Convert.ToInt32(ddlrouteName.SelectedValue));

        //if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        //{
        //    ddlboardingPoint.DataSource = ds.Tables[0];
        //    ddlboardingPoint.DataValueField = ds.Tables[1].Columns[0].ToString();
        //    ddlboardingPoint.DataTextField = ds.Tables[0].Columns[0].ToString();
        //    ddlboardingPoint.DataBind();
        //}
        //else
        //{
        //    ddlboardingPoint.Items.Clear();
        //    ddlboardingPoint.Items.Add(new ListItem("Please Select", "0"));
        //}
        //ddlboardingPoint.Focus();

        //DataSet ds = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "B.BRANCHNO", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "B.DEGREENO in(" + deg + ")", "B.DEGREENO,B.BRANCHNO");
        //ddlboardingPoint.Items.Clear();
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    ddlboardingPoint.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[1].Rows[i][0])));
        //}
        //objCommon.FillDropDownList(ddlboardingPoint, "VEHICLE_ROUTEMASTER S INNER JOIN VEHICLE_STOPMASTER A ON S.ROUTEID = A.STOPID", "DISTINCT A.STOPID", "A.STOPNAME", "S.ROUTEID=" + ddlrouteName.SelectedValue, "A.STOPID");
    }

    private string GetBoardingPoint()
    {
        string BoardingPoint = "";
        string boardingPoint = string.Empty;
        //  degreeNo = hdndegreeno.Value;
        //pnlFeeTable.Update();
        foreach (ListItem item in ddlrouteName.Items)
        {
            if (item.Selected == true)
            {
                BoardingPoint += item.Value + '$';
            }
        }

        boardingPoint = BoardingPoint.Substring(0, BoardingPoint.Length - 1);
        if (boardingPoint != "")
        {
            string[] degValue = boardingPoint.Split('$');
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return boardingPoint;

    }

    private string GetBRANCH()
    {
        string retBRANCH = string.Empty;
        foreach (ListItem item in ddlBranchMultiCheck.Items)
        {
            if (item.Selected)
            {
                retBRANCH += "$" + ddlBranchMultiCheck.SelectedValue;
            }
        }
        if (retBRANCH.Equals("")) return "0";
        else return retBRANCH;
        //return retIDNO;
    }
    protected void btnTR_Report_Click(object sender, EventArgs e)
    {
        ShowTransportReport("TransportReport", "rptTransportPassengerDetails.rpt");
    }


    private void ShowTransportReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    private void BindListView()
    {
        DataSet ds = objVMcont.Transport_Passenger_List();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTransport.DataSource = ds;
            lvTransport.DataBind();

        }
    }

    protected void lnkTransportList_Command(object sender, CommandEventArgs e)
    {

    }
    protected void lnk_No_of_Students_Command(object sender, CommandEventArgs e)
    {
        ROUTEID = 0;
        LinkButton btn = (LinkButton)(sender);
        ROUTEID = Convert.ToInt32(btn.CommandArgument);
        ShowTransportListReport("StudentList", "rptTransportStudentList.rpt");

    }

    protected void lnk_No_of_Employee_Command(object sender, CommandEventArgs e)
    {
        ROUTEID = 0;
        LinkButton btn = (LinkButton)(sender);
        ROUTEID = Convert.ToInt32(btn.CommandArgument);
        ShowTransportListReport("EmployeeList", "rptTransportEmployeeList.rpt");

    }

    protected void btnTr_show_Click(object sender, EventArgs e)
    {
        lvTransport.Visible = true;
    }


    protected void ShowTransportListReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ROUTEID=" + ROUTEID;
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
   
}