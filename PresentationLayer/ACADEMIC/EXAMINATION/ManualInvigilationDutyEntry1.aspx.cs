//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : INVIGILATOR DUTY ENTRY 
// CREATION DATE : 21-MAR-2012
// CREATED BY    : PRIYANKA KABADE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Web;
public partial class ACADEMIC_ManualInvigilationDutyEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();

    #region Page Events
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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            PopulateDropDownList();
            pnlInvigDuty.Visible = false;

        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
        }
    }

    #endregion

    #region Other Events

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "0")
        {
            string sessionno = ddlSession.SelectedValue;
            string slot = (ddlDepartment.SelectedValue == "2" || ddlDepartment.SelectedValue == "3" || ddlDepartment.SelectedValue == "5" || ddlDepartment.SelectedValue == "6" || ddlDepartment.SelectedValue == "7" || ddlDepartment.SelectedValue == "8" || ddlDepartment.SelectedValue == "14" || ddlDepartment.SelectedValue == "15" || ddlDepartment.SelectedValue == "16" || ddlDepartment.SelectedValue == "17" ? "1,3" : "2,4");
            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME", "SLOTNO IN (" + slot + ")", "SLOTNO");
        }
        else
        {
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.Focus();
        }
    }

    #endregion

    #region Click Events

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Export("Excel");
    }
    #endregion

    #region User Methods
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSION_PNAME", "SESSIONNO>0  ", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0  ", "DEPTNO");
    }

    private void ShowReportDayWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DAYNO=0,@P_SLOTNO=" + ddlSlot.SelectedValue + ",@P_UA_NO=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updInvigAuto, this.updInvigAuto.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportInvigilatorWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DAYNO=0,@P_SLOTNO=" + ddlSlot.SelectedValue + ",@P_UA_NO=" + (string.IsNullOrEmpty("0") == true ? "0" : "0") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_OrderNo=0,@P_Date=0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updInvigAuto, this.updInvigAuto.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlSlot.SelectedIndex = 0;
    }
    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGridview();
    }
    protected void btnDaywiseReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0")
            this.ShowReportDayWise("Invigilation_Duty_Day_Wise", "rptInvigilation.rpt");
        else
            objCommon.DisplayMessage(this.updInvigAuto, "Please select Session!", this.Page);
    }
    protected void btnInvigwiseReport_Click(object sender, EventArgs e)
    {
        this.ShowReportInvigilatorWise("Invigilation_Duty_Invigilator_Wise", "rptInvigilationWise.rpt");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvInvig.Rows)
        {
            Label lblFac = gvRow.FindControl("lblFac") as Label;
            CheckBox chk1 = gvRow.FindControl("CHK1") as CheckBox; CheckBox chk2 = gvRow.FindControl("CHK2") as CheckBox; CheckBox chk3 = gvRow.FindControl("CHK3") as CheckBox; CheckBox chk4 = gvRow.FindControl("CHK4") as CheckBox;
            CheckBox chk5 = gvRow.FindControl("CHK5") as CheckBox; CheckBox chk6 = gvRow.FindControl("CHK6") as CheckBox; CheckBox chk7 = gvRow.FindControl("CHK7") as CheckBox; CheckBox chk8 = gvRow.FindControl("CHK8") as CheckBox;
            CheckBox chk9 = gvRow.FindControl("CHK9") as CheckBox; CheckBox chk10 = gvRow.FindControl("CHK10") as CheckBox;  CheckBox chk11 = gvRow.FindControl("CHK11") as CheckBox; CheckBox chk12 = gvRow.FindControl("CHK12") as CheckBox;
            CheckBox chk13 = gvRow.FindControl("CHK13") as CheckBox; CheckBox chk14 = gvRow.FindControl("CHK14") as CheckBox; CheckBox chk15 = gvRow.FindControl("CHK15") as CheckBox; CheckBox chk16 = gvRow.FindControl("CHK16") as CheckBox;
            CheckBox chk17 = gvRow.FindControl("CHK17") as CheckBox; CheckBox chk18 = gvRow.FindControl("CHK18") as CheckBox; CheckBox chk19 = gvRow.FindControl("CHK19") as CheckBox; CheckBox chk20 = gvRow.FindControl("CHK20") as CheckBox;           
            CheckBox chk21 = gvRow.FindControl("CHK21") as CheckBox; CheckBox chk22 = gvRow.FindControl("CHK22") as CheckBox; CheckBox chk23 = gvRow.FindControl("CHK23") as CheckBox; CheckBox chk24 = gvRow.FindControl("CHK24") as CheckBox; CheckBox chk25 = gvRow.FindControl("CHK25") as CheckBox;
            CheckBox chk26 = gvRow.FindControl("CHK26") as CheckBox; CheckBox chk27 = gvRow.FindControl("CHK27") as CheckBox;CheckBox chk28 = gvRow.FindControl("CHK28") as CheckBox;CheckBox chk29 = gvRow.FindControl("CHK29") as CheckBox;
            CheckBox chk30 = gvRow.FindControl("CHK30") as CheckBox; CheckBox chk31 = gvRow.FindControl("CHK31") as CheckBox;
            string dayno = string.Empty;
            int count = 0;
            if (chk1.Checked == true) { if (count == 0) dayno = "1"; else dayno += "," + "1"; count++; } if (chk2.Checked == true) { if (count == 0) dayno = "2"; else dayno += "," + "2"; count++; }
            if (chk3.Checked == true) { if (count == 0) dayno = "3"; else dayno += "," + "3"; count++; } if (chk4.Checked == true) { if (count == 0) dayno = "4"; else dayno += "," + "4"; count++; }
            if (chk5.Checked == true) { if (count == 0) dayno = "5"; else dayno += "," + "5"; count++; } if (chk6.Checked == true) { if (count == 0) dayno = "6"; else dayno += "," + "6"; count++; }
            if (chk7.Checked == true) { if (count == 0) dayno = "7"; else dayno += "," + "7"; count++; } if (chk8.Checked == true) { if (count == 0) dayno = "8"; else dayno += "," + "8"; count++; }
            if (chk9.Checked == true) { if (count == 0) dayno = "9"; else dayno += "," + "9"; count++; } if (chk10.Checked == true) { if (count == 0) dayno = "10"; else dayno += "," + "10"; count++; }
            if (chk11.Checked == true) { if (count == 0) dayno = "11"; else dayno += "," + "11"; count++; } if (chk12.Checked == true) { if (count == 0) dayno = "12"; else dayno += "," + "1"; count++; }
            if (chk13.Checked == true) { if (count == 0) dayno = "13"; else dayno += "," + "13"; count++; } if (chk14.Checked == true) { if (count == 0) dayno = "14"; else dayno += "," + "14"; count++; }
            if (chk15.Checked == true) { if (count == 0) dayno = "15"; else dayno += "," + "15"; count++; } if (chk16.Checked == true) { if (count == 0) dayno = "16"; else dayno += "," + "16"; count++; }
            if (chk17.Checked == true) { if (count == 0) dayno = "17"; else dayno += "," + "17"; count++; } if (chk18.Checked == true) { if (count == 0) dayno = "18"; else dayno += "," + "18"; count++; }
            if (chk19.Checked == true) { if (count == 0) dayno = "19"; else dayno += "," + "19"; count++; } if (chk20.Checked == true) { if (count == 0) dayno = "20"; else dayno += "," + "20"; count++; }
            if (chk21.Checked == true) { if (count == 0) dayno = "21"; else dayno += "," + "21"; count++; } if (chk22.Checked == true) { if (count == 0) dayno = "22"; else dayno += "," + "22"; count++; }
            if (chk23.Checked == true) { if (count == 0) dayno = "23"; else dayno += "," + "23"; count++; } if (chk24.Checked == true) { if (count == 0) dayno = "24"; else dayno += "," + "24"; count++; }
            if (chk25.Checked == true) { if (count == 0) dayno = "25"; else dayno += "," + "25"; count++; } if (chk26.Checked == true) { if (count == 0) dayno = "26"; else dayno += "," + "26"; count++; }
            if (chk27.Checked == true) { if (count == 0) dayno = "27"; else dayno += "," + "27"; count++; } if (chk28.Checked == true) { if (count == 0) dayno = "28"; else dayno += "," + "28"; count++; }
            if (chk29.Checked == true) { if (count == 0) dayno = "29"; else dayno += "," + "29"; count++; } if (chk30.Checked == true) { if (count == 0) dayno = "30"; else dayno += "," + "30"; count++; }
            if (chk31.Checked == true) { if (count == 0) dayno = "31"; else dayno += "," + "31"; count++; }

            objSC.UpdateInvigilatorAndExamdates(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(lblFac.ToolTip), dayno, Convert.ToInt16(ddlSlot.SelectedValue));
        }
        BindGridview();
    }

    protected void gvInvig_OnRowDataBound(object sender, EventArgs e)
    {
        int tot_col_cnt1 = 0, tot_col_cnt2 = 0, tot_col_cnt3 = 0, tot_col_cnt4 = 0, tot_col_cnt5 = 0, tot_col_cnt6 = 0, tot_col_cnt7 = 0, tot_col_cnt8 = 0, tot_col_cnt9 = 0
           , tot_col_cnt10 = 0, tot_col_cnt11 = 0, tot_col_cnt12 = 0, tot_col_cnt13 = 0, tot_col_cnt14 = 0, tot_col_cnt15 = 0, tot_col_cnt16 = 0, tot_col_cnt17 = 0, tot_col_cnt18 = 0, tot_col_cnt19 = 0, tot_col_cnt20 = 0
           , tot_col_cnt21 = 0, tot_col_cnt22 = 0, tot_col_cnt23 = 0, tot_col_cnt24 = 0, tot_col_cnt25 = 0, tot_col_cnt30 = 0, tot_col_cnt26 = 0, tot_col_cnt27 = 0, tot_col_cnt28 = 0, tot_col_cnt29 = 0, tot_col_cnt31 = 0;
       
        foreach (GridViewRow row in gvInvig.Rows)
        {
            int tot_row_cnt = 0;
            Label lblFac = row.FindControl("lblFac") as Label;
            CheckBox chk1 = row.FindControl("chk1") as CheckBox; CheckBox chk2 = row.FindControl("CHK2") as CheckBox; CheckBox chk3 = row.FindControl("CHK3") as CheckBox; CheckBox chk4 = row.FindControl("CHK4") as CheckBox;
            CheckBox chk5 = row.FindControl("CHK5") as CheckBox; CheckBox chk6 = row.FindControl("CHK6") as CheckBox; CheckBox chk7 = row.FindControl("CHK7") as CheckBox; CheckBox chk8 = row.FindControl("CHK8") as CheckBox;
            CheckBox chk9 = row.FindControl("CHK9") as CheckBox; CheckBox chk10 = row.FindControl("CHK10") as CheckBox; CheckBox chk11 = row.FindControl("CHK11") as CheckBox; CheckBox chk12 = row.FindControl("CHK12") as CheckBox;
            CheckBox chk13 = row.FindControl("CHK13") as CheckBox; CheckBox chk14 = row.FindControl("CHK14") as CheckBox; CheckBox chk15 = row.FindControl("CHK15") as CheckBox; CheckBox chk16 = row.FindControl("CHK16") as CheckBox;
            CheckBox chk17 = row.FindControl("CHK17") as CheckBox; CheckBox chk18 = row.FindControl("CHK18") as CheckBox; CheckBox chk19 = row.FindControl("CHK19") as CheckBox; CheckBox chk20 = row.FindControl("CHK20") as CheckBox;
            CheckBox chk21 = row.FindControl("CHK21") as CheckBox; CheckBox chk22 = row.FindControl("CHK22") as CheckBox; CheckBox chk23 = row.FindControl("CHK23") as CheckBox; CheckBox chk24 = row.FindControl("CHK24") as CheckBox; CheckBox chk25 = row.FindControl("CHK25") as CheckBox;
            CheckBox chk26 = row.FindControl("CHK26") as CheckBox; CheckBox chk27 = row.FindControl("CHK27") as CheckBox; CheckBox chk28 = row.FindControl("CHK28") as CheckBox; CheckBox chk29 = row.FindControl("CHK29") as CheckBox;
            CheckBox chk30 = row.FindControl("CHK30") as CheckBox; CheckBox chk31 = row.FindControl("CHK31") as CheckBox;
            TextBox txtTotalDays = row.FindControl("txtTotalDays") as TextBox;

            string dayno = string.Empty;
            int count = 0;
            if (chk1.Checked == true) { count++; tot_col_cnt1++; } if (chk2.Checked == true) { count++; tot_col_cnt2++; }
            if (chk3.Checked == true) { count++; tot_col_cnt3++; } if (chk4.Checked == true) { count++; tot_col_cnt4++; }
            if (chk5.Checked == true) { count++; tot_col_cnt5++; } if (chk6.Checked == true) { count++; tot_col_cnt6++; }
            if (chk7.Checked == true) { count++; tot_col_cnt7++; } if (chk8.Checked == true) { count++; tot_col_cnt8++; }
            if (chk9.Checked == true) { count++; tot_col_cnt9++; } if (chk10.Checked == true) { count++; tot_col_cnt10++; }
            if (chk11.Checked == true) { count++; tot_col_cnt11++; } if (chk12.Checked == true) { count++; tot_col_cnt12++; }
            if (chk13.Checked == true) { count++; tot_col_cnt13++; } if (chk14.Checked == true) { count++; tot_col_cnt14++; }
            if (chk15.Checked == true) { count++; tot_col_cnt15++; } if (chk16.Checked == true) { count++; tot_col_cnt16++; }
            if (chk17.Checked == true) { count++; tot_col_cnt17++; } if (chk18.Checked == true) { count++; tot_col_cnt18++; }
            if (chk19.Checked == true) { count++; tot_col_cnt19++; } if (chk20.Checked == true) { count++; tot_col_cnt20++; }
            if (chk21.Checked == true) { count++; tot_col_cnt21++; } if (chk22.Checked == true) { count++; tot_col_cnt22++; }
            if (chk23.Checked == true) { count++; tot_col_cnt23++; } if (chk24.Checked == true) { count++; tot_col_cnt24++; }
            if (chk25.Checked == true) { count++; tot_col_cnt25++; } if (chk26.Checked == true) { count++; tot_col_cnt26++; }
            if (chk27.Checked == true) { count++; tot_col_cnt27++; } if (chk28.Checked == true) { count++; tot_col_cnt28++; }
            if (chk29.Checked == true) { count++; tot_col_cnt29++; } if (chk30.Checked == true) { count++; tot_col_cnt30++; }
            if (chk31.Checked == true) { count++; tot_col_cnt31++; }
            tot_row_cnt = count;
            txtTotalDays.Text = tot_row_cnt.ToString();
        }
    }
    protected void BindGridview()
    {
        try
        {
            ////Create a Datatable and all the records
            DataTable dt = new DataTable("Invig_duty");
            //DataColumn column;
            //DataRow row;

            DataSet ds = objSC.GetInvigilatorAndExamdates(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), Convert.ToInt16(ddlSlot.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                gvInvig.DataSource = ds;
                gvInvig.DataBind();
                gvInvig1.DataSource = ds;
                gvInvig1.DataBind();
                pnlInvigDuty.Visible = true;

                //    //Add Columns
                //    //==================
                //    for (int yIndex = 0; yIndex < ds.Tables[0].Columns.Count; yIndex++)
                //    {
                //        column = new DataColumn();
                //        column.DataType = System.Type.GetType("System.Int32");
                //        column.ColumnName = ds.Tables[0].Columns[yIndex].ToString();

                //        //column[yIndex] = yIndex.ToString();

                //        dt.Columns.Add(column);
                //    }
                //    for (int yIndex = 0; yIndex < ds.Tables[0].Rows.Count; yIndex++)
                //    {
                //        row = dt.NewRow();
                //        //row[nIndex] = nIndex.ToString();
                //        dt.Rows.Add(row);
                //    }


                //    int i = 0;
                //    //Iterate through the columns of the datatable to set the data bound field dynamically.
                //    foreach (DataColumn col in dt.Columns)
                //    {
                //        //Declare the bound field and allocate memory for the bound field.
                //        TemplateField bfield = new TemplateField();
                //        //Initalize the DataField value.
                //        bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);
                //        if (i == 0)
                //        {
                //            //Initialize the HeaderText field value.
                //            bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName);
                //        }
                //        else
                //            //Initialize the HeaderText field value.
                //            bfield.ItemTemplate = new GridViewTemplate(ListItemType.EditItem, col.ColumnName);

                //        //Add the newly created bound field to the GridView.
                //        gvInvig.Columns.Add(bfield);
                //        i++;
                //    }
                ////    //Initialize the DataSource
                //    gvInvig.DataSource = ds.Tables[0];
                //    gvInvig.DataBind();


                //    gvInvig.Visible = true;
                //    pnlInvigDuty.Visible = true;
            }
            else
            {
                gvInvig.DataSource = null;
                gvInvig.DataBind();
                gvInvig1.DataSource = null;
                gvInvig1.DataBind();
                pnlInvigDuty.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.BindGridview() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void chkbox_OnCheckedChanged(object sender, EventArgs e)
    {
       
        int tot_col_cnt1 = 0,tot_col_cnt2 = 0,tot_col_cnt3 = 0,tot_col_cnt4 = 0,tot_col_cnt5 = 0,tot_col_cnt6 = 0,tot_col_cnt7 = 0,tot_col_cnt8 = 0,tot_col_cnt9 = 0
            ,tot_col_cnt10= 0,tot_col_cnt11 = 0,tot_col_cnt12 = 0,tot_col_cnt13 = 0,tot_col_cnt14 = 0,tot_col_cnt15 = 0,tot_col_cnt16 = 0,tot_col_cnt17 = 0,tot_col_cnt18 = 0,tot_col_cnt19 = 0,tot_col_cnt20 = 0
            ,tot_col_cnt21 = 0,tot_col_cnt22 = 0,tot_col_cnt23 = 0,tot_col_cnt24 = 0,tot_col_cnt25 = 0,tot_col_cnt30 = 0,tot_col_cnt26 = 0,tot_col_cnt27 = 0,tot_col_cnt28 = 0,tot_col_cnt29 = 0,tot_col_cnt31 = 0;
        foreach (GridViewRow row in gvInvig.Rows)
        {
            int tot_row_cnt = 0;
            Label lblFac = row.FindControl("lblFac") as Label;
            CheckBox chk1 = row.FindControl("chk1") as CheckBox; CheckBox chk2 = row.FindControl("CHK2") as CheckBox; CheckBox chk3 = row.FindControl("CHK3") as CheckBox; CheckBox chk4 = row.FindControl("CHK4") as CheckBox;
            CheckBox chk5 = row.FindControl("CHK5") as CheckBox; CheckBox chk6 = row.FindControl("CHK6") as CheckBox; CheckBox chk7 = row.FindControl("CHK7") as CheckBox; CheckBox chk8 = row.FindControl("CHK8") as CheckBox;
            CheckBox chk9 = row.FindControl("CHK9") as CheckBox; CheckBox chk10 = row.FindControl("CHK10") as CheckBox; CheckBox chk11 = row.FindControl("CHK11") as CheckBox; CheckBox chk12 = row.FindControl("CHK12") as CheckBox;
            CheckBox chk13 = row.FindControl("CHK13") as CheckBox; CheckBox chk14 = row.FindControl("CHK14") as CheckBox; CheckBox chk15 = row.FindControl("CHK15") as CheckBox; CheckBox chk16 = row.FindControl("CHK16") as CheckBox;
            CheckBox chk17 = row.FindControl("CHK17") as CheckBox; CheckBox chk18 = row.FindControl("CHK18") as CheckBox; CheckBox chk19 = row.FindControl("CHK19") as CheckBox; CheckBox chk20 = row.FindControl("CHK20") as CheckBox;
            CheckBox chk21 = row.FindControl("CHK21") as CheckBox; CheckBox chk22 = row.FindControl("CHK22") as CheckBox; CheckBox chk23 = row.FindControl("CHK23") as CheckBox; CheckBox chk24 = row.FindControl("CHK24") as CheckBox; CheckBox chk25 = row.FindControl("CHK25") as CheckBox;
            CheckBox chk26 = row.FindControl("CHK26") as CheckBox; CheckBox chk27 = row.FindControl("CHK27") as CheckBox; CheckBox chk28 = row.FindControl("CHK28") as CheckBox; CheckBox chk29 = row.FindControl("CHK29") as CheckBox;
            CheckBox chk30 = row.FindControl("CHK30") as CheckBox; CheckBox chk31 = row.FindControl("CHK31") as CheckBox;
            TextBox txtTotalDays = row.FindControl("txtTotalDays") as TextBox;
            
            string dayno = string.Empty;
            int count = 0;
            if (chk1.Checked == true) { count++; tot_col_cnt1++; } if (chk2.Checked == true) { count++; tot_col_cnt2++; }
            if (chk3.Checked == true) { count++; tot_col_cnt3++; } if (chk4.Checked == true) { count++; tot_col_cnt4++; }
            if (chk5.Checked == true) { count++; tot_col_cnt5++; } if (chk6.Checked == true) { count++; tot_col_cnt6++; }
            if (chk7.Checked == true) { count++; tot_col_cnt7++; } if (chk8.Checked == true) { count++; tot_col_cnt8++; }
            if (chk9.Checked == true) { count++; tot_col_cnt9++; } if (chk10.Checked == true) { count++; tot_col_cnt10++; }
            if (chk11.Checked == true) { count++; tot_col_cnt11++; } if (chk12.Checked == true) { count++; tot_col_cnt12++; }
            if (chk13.Checked == true) { count++; tot_col_cnt13++; } if (chk14.Checked == true) { count++; tot_col_cnt14++; }
            if (chk15.Checked == true) { count++; tot_col_cnt15++; } if (chk16.Checked == true) { count++; tot_col_cnt16++; }
            if (chk17.Checked == true) { count++; tot_col_cnt17++; } if (chk18.Checked == true) { count++; tot_col_cnt18++; }
            if (chk19.Checked == true) { count++; tot_col_cnt19++; } if (chk20.Checked == true) { count++; tot_col_cnt20++; }
            if (chk21.Checked == true) { count++; tot_col_cnt21++; } if (chk22.Checked == true) { count++; tot_col_cnt22++; }
            if (chk23.Checked == true) { count++; tot_col_cnt23++; } if (chk24.Checked == true) { count++; tot_col_cnt24++; }
            if (chk25.Checked == true) { count++; tot_col_cnt25++; } if (chk26.Checked == true) { count++; tot_col_cnt26++; }
            if (chk27.Checked == true) { count++; tot_col_cnt27++; } if (chk28.Checked == true) { count++; tot_col_cnt28++; }
            if (chk29.Checked == true) { count++; tot_col_cnt29++; } if (chk30.Checked == true) { count++; tot_col_cnt30++; }
            if (chk31.Checked == true) { count++; tot_col_cnt31++; }
            tot_row_cnt = count;
            txtTotalDays.Text = tot_row_cnt.ToString();
        }
    }
    private void Export(string type)
    {
        string filename = string.Empty;
        string ContentType = string.Empty;

        if (type == "Excel")
        {
            filename = "InvigDuty.xlsx";
            ContentType = "ms-excel";
        }
        else if (type == "Word")
        {
            filename = "InvigDuty.doc";
            ContentType = "vnd.word";
        }
        else if (type == "Pdf")
        {
            filename = "InvigDuty.pdf";
            ContentType = "pdf";
        }

        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvInvig.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */

    }
}
