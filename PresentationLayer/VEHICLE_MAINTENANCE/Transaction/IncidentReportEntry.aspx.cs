﻿//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Vehicle Management
// PAGE NAME     : IncidentReportEntry.aspx                                                
// CREATION DATE : 22-Feb-2020                                                        
// CREATED BY    : ANDPOJ VIJAY                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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

public partial class VEHICLE_MAINTENANCE_Transaction_IncidentReportEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController ObjCon = new VMController();
    VM ObjEnt = new VM();
    DataSet ds = null;

    #region PageLoad

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
        try
        {
            ds = (DataSet)ViewState["Table"];
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
                //ViewState["action"] = "add";
                hfviewsate.Value = "add";
                FillTables();
            }


        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    #endregion

    #region Methods

    private void FillTables()
    {
        try
        {
            ViewState["Table"] = ds = ObjCon.GetTables();
            lvComplaint.DataSource = ds.Tables[2];
            lvComplaint.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    public void Clear()
    {
        txtrouteno.Text =
        txtNatureinc.Text =
        txtincidentplace.Text =
        txtincidenttime.Text =
        txtinsdate.Text =
        txtfollwup.Text = string.Empty;
    }
    private void ShowDetails(string no)
    {
        DataTable table = ds.Tables[2];
        //  DataTable table = (DataTable)ViewState["Table"];
        // Presuming the DataTable has a column named id.
        string expression;
        expression = no;
        DataRow[] foundRows;

        // Use the Select method to find all rows matching the filter.
        foundRows = table.Select(expression);

        // Print column 0 of each returned row.
        for (int i = 0; i < foundRows.Length; i++)
        {
            txtrouteno.Text = foundRows[i]["ROUTE_NO"].ToString();
            txtNatureinc.Text = foundRows[i]["NATURE_OF_INCIDENT"].ToString();
            txtincidentplace.Text = foundRows[i]["INCIDENT_PLACE"].ToString();
            txtincidenttime.Text = Convert.ToString(Convert.ToDateTime(foundRows[i]["INCIDENT_TIME"].ToString()).ToString("hh:mm tt"));
            txtinsdate.Text = foundRows[i]["INCIDENT_DATE"].ToString();
            txtfollwup.Text = foundRows[i]["FOLLOW_UP"].ToString();
        }
    }
    public void ClearDate()
    {
        txttodate.Text = txtformdate.Text = string.Empty;
    }
    private void ShowTransportReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtformdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txttodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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

    #endregion

    #region Events

    protected void btnReport_Click(object sender, EventArgs e)
    {
        complaint.Visible = false;
        button.Visible = false;
        datecontrols.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus cs = new CustomStatus();
        ViewState["action"] = hfviewsate.Value;
        DateTime Date, Time;
        try
        {

            ObjEnt.Route_no = txtrouteno.Text;
            ObjEnt.NATUREOFINCIDENT = txtNatureinc.Text;
            ObjEnt.INCIDENTPLACE = txtincidentplace.Text;
            ObjEnt.FOLLOWUP = txtfollwup.Text;
            Date = Convert.ToDateTime(txtinsdate.Text);
            Time = Convert.ToDateTime(txtincidenttime.Text);
            Session["DateTime"] = Date.ToString("yyyy-MM-dd") + Time.ToString(" HH:mm:ss");
            ObjEnt.ComplaintDate = Convert.ToDateTime(Session["DateTime"]);
            ObjEnt.ActionTakenDate = Convert.ToDateTime(txtincidenttime.Text);

            if (ViewState["action"].ToString().Equals("edit"))
            {
                ObjEnt.Sno = Convert.ToInt32(ViewState["IDNO"].ToString());
                //--======start===Shaikh Juned 13-09-2022
               
                
                    DataSet ds = objCommon.FillDropDown("VEHICLE_INCIDENT_REPORT", "*", "INCIDENT_DATE", "ROUTE_NO='" + Convert.ToString(txtrouteno.Text) + "' and CAST(INCIDENT_DATE AS DATE)='" + Convert.ToDateTime(txtinsdate.Text).ToString("yyyy-MM-dd") + "' ", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            int sno =Convert.ToInt32( dr["SNO"]);
                            //string COMPLAINTDATE = dr["COMPLAINT_DATE"].ToString();
                            if (sno !=Convert.ToInt32( ObjEnt.Sno))
                            {
                        objCommon.DisplayMessage(this.Page, "Incident Is Already Exist On Same Date.", this.Page);
                        return;
                            }

                        }
                    }
                
                //---========end=====

            }

            //--======start===Shaikh Juned 13-09-2022
            if (ViewState["action"] != "edit")
            {
            DataSet ds = objCommon.FillDropDown("VEHICLE_INCIDENT_REPORT", "ROUTE_NO", "INCIDENT_DATE", "ROUTE_NO='" + Convert.ToString(txtrouteno.Text) + "' and CAST(INCIDENT_DATE AS DATE)='" + Convert.ToDateTime(txtinsdate.Text).ToString("yyyy-MM-dd") + "' ", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    string ROUTENO = dr["ROUTE_NO"].ToString();
                //    //string COMPLAINTDATE = dr["COMPLAINT_DATE"].ToString();
                //    if (ROUTENO == txtrouteno.Text)
                //    {
                        objCommon.DisplayMessage(this.Page, "Incident Is Already Exist On Same Date.", this.Page);
                        return;
                    //}

               // }
            }
        }
            //---========end=====

            cs = (CustomStatus)ObjCon.IncidentReportEntry(ObjEnt);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record saved successfully.");

            }
            else
            {
                MessageBox("Record updated successfully.");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        hfviewsate.Value = "add";
        Clear();
        FillTables();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        // Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int no = int.Parse(imgBtn.CommandArgument);
        ViewState["IDNO"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        hfviewsate.Value = "edit";
        ShowDetails("SNO=" + no);
    }
    protected void btnDateClear_Click(object sender, EventArgs e)
    {
        //  ClearDate();
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowTransportReport("IncidentReportEntry", "rptTransportIncidentReport.rpt");
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        complaint.Visible = true;
        datecontrols.Visible = false;
        button.Visible = true;
        ClearDate();

    }
    protected void txtrouteno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtrouteno.Text == null || txtrouteno.Text == string.Empty)
            {
                return;
            }
            else
            {

                DataTable table = ds.Tables[1];
                //  DataTable table = (DataTable)ViewState["Table"];
                string expression;
                expression = "ROUTE_NUMBER='" + txtrouteno.Text + "'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = table.Select(expression);
                // Print column 0 of each returned row.
                if (foundRows.Length == 0)
                {
                    MessageBox("This Route No. Does Not Exist");
                    Clear();
                }

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
    #endregion
}
