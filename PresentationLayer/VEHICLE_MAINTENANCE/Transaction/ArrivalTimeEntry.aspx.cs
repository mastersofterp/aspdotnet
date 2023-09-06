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

public partial class VEHICLE_MAINTENANCE_Transaction_ArrivalTimeEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController ObjCon = new VMController();
    VM ObjEnt = new VM();
    DataSet ds = null;
    DataTable dt;
    DataTable dtBindListview;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ds = (DataSet)ViewState["Tables"];
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

                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindData();

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

    private void BindData()
    {
        try
        {
            ViewState["Tables"] = ds = ObjCon.GetTablesforArrival();
            FillDropDownList(ddlRouteNo, "ROUTEID", "ROUTENAME", "", 0);
            FillDropDownList(ddlReason, "REASON_ID", "REASON", "", 1);
            BindListView();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Transport_ArrivalTime.btnAddC_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    public void FillDropDownList(DropDownList ddllist, string ValueField, string TextField, string Condition, int Table)
    {
        try
        {
            DataTable table = null;
            table = ds.Tables[Table];
            DataRow[] foundRows;
            foundRows = table.Select(Condition);
            DataTable newTable = new DataTable();
            newTable = table.Clone();
            if (table.Rows.Count > 0)
            {
                ddllist.Items.Clear();
                ddllist.Items.Add("Please Select");
                ddllist.SelectedItem.Value = "0";
                foreach (DataRow dRow in foundRows)
                    newTable.ImportRow(dRow);
                ddllist.DataSource = newTable;
                ddllist.DataTextField = TextField;
                ddllist.DataValueField = ValueField;
                ddllist.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Transport_ArrivalTime.btnAddC_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void clear()
    {
        try
        {
            Session["ArrivalTable"] = null;
            ViewState["actionCo"] = null;
            ViewState["Sno"] = null;

            lvArrival.DataSource = null;
            lvArrival.DataBind();
            lvArrival.Visible = false;

            txtSvcebus.Text = string.Empty;
            txtDedicatedbus.Text = string.Empty;
            txtAcbus38.Text = string.Empty;
            txtAcbus55.Text = string.Empty;
            ClearControl();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }

    private DataTable CreateArrivalTimeEntry()
    {

        DataTable dtATE = new DataTable();
        dtATE.Columns.Add(new DataColumn("ATID", typeof(int)));
        dtATE.Columns.Add(new DataColumn("ROUTEID", typeof(int)));
        dtATE.Columns.Add(new DataColumn("ARRIVAL_DATE_TIME", typeof(DateTime)));
        dtATE.Columns.Add(new DataColumn("REASON_ID", typeof(int)));
        dtATE.Columns.Add(new DataColumn("ROUTENAME", typeof(string)));
        dtATE.Columns.Add(new DataColumn("REASON", typeof(string)));

        return dtATE;

    }

    private void BindListView()
    {
        try
        {
            Session["Date"] = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");

            DataSet Ds1 = objCommon.FillDropDown(" VEHICLE_ARRIVALTIME AT left JOIN VEHICLE_ROUTEMASTER RM  ON(AT.ROUTEID=RM.ROUTEID) left JOIN VEHICLE_ARRIVAL_TIME_REMARK RE ON(AT.REASON_ID=RE.REASON_ID)", "AT.ATID,RM.ROUTEID,AT.ARRIVAL_DATE_TIME,ISNULL(RE.REASON_ID,0)AS REASON_ID,RM.ROUTENAME,RE.REASON", "", "CAST(AT.ARRIVAL_DATE_TIME AS DATE)='" + Session["Date"].ToString() + "'", "");
            Session["ArrivalTable"] = Ds1.Tables[0];
            lvArrival.DataSource = Session["ArrivalTable"];
            lvArrival.DataBind();
            lvArrival.Visible = true;

            DataRow[] Found = ds.Tables[3].Select("ARRIVAL_DATETIME<='" + txtDate.Text + "'AND ARRIVAL_DATETIME>='" + txtDate.Text + "'");
            if (Found.Length > 0)
            {
                txtAcbus38.Text = Found[0]["AC_BUS_38_SEAT"].ToString();
                txtAcbus55.Text = Found[0]["AC_BUS_55_SEAT"].ToString();
                txtDedicatedbus.Text = Found[0]["DEDICATED_BUSES"].ToString();
                txtSvcebus.Text = Found[0]["SVCE_BUSES"].ToString();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DataTable dt = null;
        try
        {
            ObjEnt.ArrivalDate = Convert.ToDateTime(txtDate.Text);
            ObjEnt.AC_BUS_38_SEAT = Convert.ToInt32(txtAcbus38.Text);
            ObjEnt.AC_BUS_55_SEAT = Convert.ToInt32(txtAcbus55.Text);
            ObjEnt.DEDICATED_BUSES = Convert.ToInt32(txtDedicatedbus.Text);
            ObjEnt.SVCE_BUSES = Convert.ToInt32(txtSvcebus.Text);
            dt = (DataTable)Session["ArrivalTable"];
            ObjEnt.ArrivalTimeEntryDataTable = dt;
            CustomStatus cs = (CustomStatus)ObjCon.InsUpdateArrivalTimeEntry(ObjEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record saved successfully.");
            }
            else
            {
                MessageBox("Record saved successfully.");
            }
            clear();
            BindData();
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControl();
            DateTime date = DateTime.Now;
            txtDate.Text = Convert.ToString(date);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void ClearControl()
    {
        try
        {
            ddlReason.SelectedValue = "0";
            ddlRouteNo.SelectedValue = "0";
            txtArrivalTime.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            clear();
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        

    }

    private void ShowTransportReport(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ARRIVAL_DATE=" + Session["PDate"].ToString();//+ ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Session["DateTime"] = null;
            var date = Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy");
            var Time = Convert.ToDateTime(txtArrivalTime.Text).ToString(" hh:mm:ss");
            Session["DateTime"] = date + Time;

            CheckRowExist();
            lvArrival.DataSource = dt;
            lvArrival.DataBind();
            lvArrival.Visible = true;
            ClearControl();



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void CheckRowExist()
    {
        try
        {
            dt = (DataTable)Session["ArrivalTable"];
            if (dt != null)
            {
                var Chk = from dtchk in dt.AsEnumerable()
                          where dtchk.Field<DateTime>("ARRIVAL_DATE_TIME") == Convert.ToDateTime(Session["DateTime"]) && dtchk.Field<int>("ROUTEID") == Convert.ToInt32(ddlRouteNo.SelectedValue)
                          select dtchk.Field<int>("ATID");
                if (Chk.Any())
                {
                    MessageBox("Record Already exist.");
                    ClearControl();

                }
                else
                {
                    AddtoTable();
                }
            }
            else
            {
                AddtoTable();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void AddtoTable()
    {
        try
        {
            dt = (DataTable)Session["ArrivalTable"];
            if (ViewState["actionCo"] == null)
            {

                if (Session["ArrivalTable"] != null && (DataTable)Session["ArrivalTable"] != null)
                {
                    int maxVal = 0;
                    DataRow dr = dt.NewRow();
                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATID"]));
                    }
                    dr["ATID"] = maxVal + 1;
                    dr["ROUTEID"] = Convert.ToInt32(ddlRouteNo.SelectedValue);
                    dr["ARRIVAL_DATE_TIME"] = Convert.ToDateTime(Session["DateTime"]);
                    if (ddlReason.SelectedValue != string.Empty)
                    {
                        dr["REASON_ID"] = Convert.ToDateTime(ddlReason.SelectedValue);
                    }
                    else
                    {
                        dr["REASON_ID"] = DBNull.Value;
                    }
                   // dr["REASON_ID"] = Convert.ToInt32(ddlReason.SelectedValue);
                    dr["ROUTENAME"] = ddlRouteNo.SelectedItem;
                    dr["REASON"] = ddlReason.SelectedValue == "0" ? null : ddlReason.SelectedItem;



                    dt.Rows.Add(dr);
                    Session["ArrivalTable"] = dt;
                    ViewState["ATID"] = Convert.ToInt32(ViewState["ATID"]) + 1;
                }
                else
                {

                    dt = this.CreateArrivalTimeEntry();
                    DataRow dr = dt.NewRow();
                    dr["ATID"] = Convert.ToInt32(ViewState["ATID"]) + 1;
                    dr["ROUTEID"] = Convert.ToInt32(ddlRouteNo.SelectedValue);
                    dr["ARRIVAL_DATE_TIME"] = Convert.ToDateTime(Session["DateTime"]);
                    dr["REASON_ID"] = Convert.ToInt32(ddlReason.SelectedValue);
                    dr["ROUTENAME"] = ddlRouteNo.SelectedItem;
                    dr["REASON"] = ddlReason.SelectedValue == "0" ? null : ddlReason.SelectedItem;


                    ViewState["ATID"] = Convert.ToInt32(ViewState["ATID"]) + 1;
                    dt.Rows.Add(dr);

                    Session["ArrivalTable"] = dt;
                }
            }

            else
            {
                if (Session["ArrivalTable"] != null && ((DataTable)Session["ArrivalTable"]) != null)
                {
                    dt = (DataTable)Session["ArrivalTable"];
                    DataRow dr = dt.NewRow();
                    dr["ROUTEID"] = Convert.ToInt32(ddlRouteNo.SelectedValue);
                    dr["ARRIVAL_DATE_TIME"] = Convert.ToDateTime(Session["DateTime"]); 
                    dr["REASON_ID"] = Convert.ToInt32(ddlReason.SelectedValue);
                    dr["ROUTENAME"] = ddlRouteNo.SelectedItem;
                    dr["REASON"] = ddlReason.SelectedValue == "0" ? null : ddlReason.SelectedItem;

                    dt.Rows.Add(dr);

                    Session["ArrivalTable"] = dt;

                    ViewState["ATID"] = Convert.ToInt32(ViewState["ATID"]) + 1;
                }
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton Img = sender as ImageButton;
            string Delete = Convert.ToString(Img.CommandArgument);

            dt = (DataTable)Session["ArrivalTable"];
            DataRow[] Found = dt.Select("ATID=" + Delete);
            foreach (DataRow row in Found)
                row.Delete();
            dt.AcceptChanges();
            lvArrival.DataSource = Session["ArrivalTable"] = dt;
            lvArrival.DataBind();
            lvArrival.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_TransportManagement.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }



    }
    protected void btnReport_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != string.Empty)
            {
                int count = 0;
                Session["PDate"] = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd 00:00:00");
                count = Convert.ToInt32(objCommon.LookUp("VEHICLE_ARRIVALTIME", "COUNT(*)", "CAST(ARRIVAL_DATE_TIME AS DATE)='" + Session["PDate"].ToString() + "'"));
                if (count > 0)
                {
                    ShowTransportReport("ArrivalTimeReport", "cryTransportArrivalReport.rpt");
                }
                else
                {
                    MessageBox("Entry is not found on this Date.");
                    return;
                }
            }
            else
            {
                MessageBox("Please Select Arrival Date.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}
