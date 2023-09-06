//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 19-AUG-2015
// DESCRIPTION   : USE TO CREATE ROUTES WITH DIFFERENT STOP NAMES.
//=========================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using IITMS;
using IITMS.UAIMS;
using System.Data.Linq;
using System.Collections.Generic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Master_RouteMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
    string routeCode = string.Empty;



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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    Session["RecTbl"] = null;
                    BindlistView();
                    objVM.IPADDRESS = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (objVM.IPADDRESS == null || objVM.IPADDRESS == "")
                        objVM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    objVM.MACADDRESS = nics[0].GetPhysicalAddress().ToString();
                    objCommon.FillDropDownList(ddlStopName, "VEHICLE_STOPMASTER", "STOPID", "STOPNAME", "", "SEQNO");
                    txtRoutePath.Attributes.Add("readonly", "readonly");

                }
            }
            DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "isnull(max(ROUTEID),0)+1 SQNO", "", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSeqNo.Text = ds.Tables[0].Rows[0]["SQNO"].ToString();
            }

            lvRoutePath.Visible = false;

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "ROUTEID, ROUTENAME, SEQNO, ROUTEPATH", "ROUTECODE,	DISTANCE,	STARTING_TIME,	ROUTE_NUMBER,CASE WHEN VEHICLE_TYPE=1 THEN 'A/c' ELSE 'Non A/c' END AS VEHICLE_TYPE", "", "ROUTE_NUMBER");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRoute.DataSource = ds;
                lvRoute.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            string routeCode = string.Empty;
            string Distance = string.Empty;
            string StopName = string.Empty;
            string routefees = string.Empty;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = (DataTable)Session["RecTbl"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (routeCode.Trim().Equals(string.Empty))
                    {
                        routeCode = dr["STOPNO"].ToString();
                    }
                    else
                    {
                        routeCode += " - " + dr["STOPNO"].ToString();
                    }

                    if (Distance.Trim().Equals(string.Empty))
                    {
                        Distance = dr["DISTANCE"].ToString();
                    }
                    else
                    {
                        Distance += " - " + dr["DISTANCE"].ToString();
                    }

                    if (StopName.Trim().Equals(string.Empty))
                    {
                        StopName = dr["STOPNAME"].ToString();
                    }
                    else
                    {
                        StopName += " - " + dr["STOPNAME"].ToString();
                    }
                    if (routefees.Trim().Equals(string.Empty))
                    {
                        routefees = dr["ROUTE_FEES"].ToString();
                    }
                    else
                    {
                        routefees += " - " + dr["ROUTE_FEES"].ToString();
                    }
                }
            }

            objVM.ROUTENAME = txtRouteName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRouteName.Text.Trim());
            objVM.ROUTEPATH = StopName; // txtRoutePath.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRoutePath.Text.Trim());
            objVM.KM = Distance; // txtKM.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtKM.Text.Trim());
            objVM.SEQNO = txtSeqNo.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(txtSeqNo.Text);

            objVM.STARTING_TIME = Convert.ToDateTime(Convert.ToDateTime(txtStartimeRoute.Text).ToString("HH:mm tt"));//added By Nancy Sharma             


            objVM.ROUTECODE = routeCode;
            objVM.ROUTENUMBER = txtRNumber.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRNumber.Text.Trim());
            objVM.VEHICLETYPE = Convert.ToInt32(ddlvehicletype.SelectedValue);
            objVM.ROUTEFEES = routefees;
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateRouteMaster(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {

                        // Clear();
                        lvRoutePath.Visible = true;
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView();
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["ROUTENO"] != null)
                    {
                        objVM.ROUTENO = Convert.ToInt32(ViewState["ROUTENO"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateRouteMaster(objVM);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            lvRoutePath.Visible = true;
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            ViewState["action"] = "add";

                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                            Clear();
                        }

                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void AddRoutMiddle()//Added by vijay on 11-06-2020
    {
        try
        {
            DataTable dt = (DataTable)Session["RecTbl"];
            DataRow dr = dt.NewRow(); //Create New Row
            dr["SRNO"] = 1;
            dr["STOPNAME"] = ddlStopName.SelectedItem.Text == null ? string.Empty : ddlStopName.SelectedItem.Text;
            dr["DISTANCE"] = txtKM.Text.Trim() == null ? string.Empty : Convert.ToString(txtKM.Text.Trim()).Replace(',', ' ');
            dr["STOPNO"] = ddlStopName.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlStopName.SelectedValue);
            if (txtFees.Text != string.Empty)
            {
                dr["ROUTE_FEES"] = txtFees.Text == "0" ? 0 : Convert.ToDecimal(txtFees.Text); //Shaikh Juned 02-02-2023 Added for Fees Amount in Grid
            }
            else
            {
                dr["ROUTE_FEES"] = 0;
            }
            dt.Rows.InsertAt(dr, Convert.ToInt32(ddlAddStop.SelectedValue));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["SRNO"] = i + 1;
            }

            if (txtRoutePath.Text == string.Empty || txtRoutePath.Text == "")
            {
                txtRoutePath.Text = ddlStopName.SelectedItem.Text;
            }
            else
            {
                txtRoutePath.Text = txtRoutePath.Text + " - " + ddlStopName.SelectedItem.Text;
            }
            Session["RecTbl"] = dt;
            lvRoutePath.DataSource = dt;
            lvRoutePath.DataBind();
            BindAddRouteNameDropDown(ddlAddStop, dt);
            ClearRec();
            lvRoutePath.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int ROUTENO = int.Parse(btnEdit.CommandArgument);
            ViewState["ROUTENO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";

            chkSource.Visible = false;
            ShowDetails(ROUTENO);
            //DivAddRoute.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int ROUTENO)
    {
        try
        {
            string routepath = string.Empty;
            string distance = string.Empty;
            string StopName = string.Empty;

            DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEMASTER", "*", "", "ROUTEID=" + ROUTENO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRouteName.Text = ds.Tables[0].Rows[0]["ROUTENAME"].ToString();
                txtRoutePath.Text = ds.Tables[0].Rows[0]["ROUTEPATH"].ToString();
                txtSeqNo.Text = ds.Tables[0].Rows[0]["SEQNO"].ToString();
                txtStartimeRoute.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTING_TIME"]).ToString("hh:mm tt");
                txtRNumber.Text = ds.Tables[0].Rows[0]["ROUTE_NUMBER"].ToString();
                ddlvehicletype.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_TYPE"].ToString();//added by vijay
                //ddlStopName.SelectedValue = ds.Tables[0].Rows[0]["ROUTECODE"].ToString(); //added by juned
            }

            DataSet ds1 = null;
            ds1 = objVMC.GetRouteDataByID(ROUTENO);



            if (ds1.Tables[0].Rows.Count > 0)
            {
                lvRoutePath.Visible = true;
                DataTable dt = ds1.Tables[0];
                Session["RecTbl"] = dt;
                lvRoutePath.DataSource = ds1;
                lvRoutePath.DataBind();
                BindAddRouteNameDropDown(ddlAddStop, dt);


            }
            else
            {
                lvRoutePath.DataSource = null;
                lvRoutePath.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void BindAddRouteNameDropDown(DropDownList ddlList, DataTable dt)//Added by Vijay on 11-06-2020
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";
        ddlList.DataSource = dt;
        ddlList.DataValueField = dt.Columns[0].ToString();
        ddlList.DataTextField = dt.Columns[1].ToString();
        ddlList.DataBind();
        ddlList.SelectedIndex = 0;

    }
    private void Clear()
    {

        txtSeqNo.Text = string.Empty;
        ViewState["ROUTENO"] = null;
        ViewState["action"] = "add";
        lvRoutePath.DataSource = null;
        lvRoutePath.DataBind();
        txtRouteName.Text = string.Empty;
        chkSource.Checked = false;
        chkSource.Visible = true;
        txtKM.Text = string.Empty;
        txtRoutePath.Text = string.Empty;
        ddlStopName.SelectedIndex = 0;
        Session["RecTbl"] = null;
        txtStartimeRoute.Text = null;
        txtRNumber.Text = string.Empty;
        ddlvehicletype.SelectedValue = "0";
        DivAddRoute.Visible = false;
        txtFees.Text = string.Empty; //Shaikh Juned 02-02-2023

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=RouteNameReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ROUTEID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Route Names", "rptRouteNameReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private DataTable CreateTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("STOPNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DISTANCE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("STOPNO", typeof(int)));
        dt.Columns.Add(new DataColumn("SEQNO", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ROUTE_FEES", typeof(decimal))); //Shaikh Juned 02-02-2023 Added for Fees Amount in Grid
        return dt;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFees.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updActivity, "Please Enter Fees.", this.Page);
                return;
            }
            if (ViewState["action"].ToString().Equals("edit"))
            {
                //------26-04-2023---
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();

                if (CheckDuplicateStopName(dt, ddlStopName.SelectedItem.Text))
                {
                    ClearRec();
                   
                    objCommon.DisplayMessage(this.updActivity, "This Stop Name Already Exist.", this.Page);
                    lvRoutePath.DataSource = Session["RecTbl"];
                    lvRoutePath.DataBind();
                    lvRoutePath.Visible = true;
                    return;
                }
                //------------

                AddRoutMiddle();
                return;
            }


            //DataSet dsSeq = null;
            //dsSeq = objCommon.FillDropDown("VEHICLE_STOPMASTER", "", "STOPID", "STOPID=" + Convert.ToInt32(ddlStopName.SelectedValue), "");
            //if (dsSeq.Tables[0].Rows.Count > 0)
            //{
                // lblStopSeq.Text = dsSeq.Tables[0].Rows[0]["SEQNO"].ToString();
            //}

            lvRoutePath.Visible = true;

            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();

                if (CheckDuplicateStopName(dt, ddlStopName.SelectedItem.Text))
                {
                    ClearRec();
                    objCommon.DisplayMessage(this.updActivity, "This Stop Name Already Exist.", this.Page);
                    return;
                }


                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["STOPNAME"] = ddlStopName.SelectedItem.Text == null ? string.Empty : ddlStopName.SelectedItem.Text;
                dr["DISTANCE"] = txtKM.Text.Trim() == null ? string.Empty : Convert.ToString(txtKM.Text.Trim()).Replace(',', ' ');
                dr["STOPNO"] = ddlStopName.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlStopName.SelectedValue);
                //dr["SEQNO"] = lblStopSeq.Text.Trim() == null ? 0 : Convert.ToDecimal(lblStopSeq.Text);
                dr["ROUTE_FEES"] = txtFees.Text == "0" ? 0 : Convert.ToDecimal(txtFees.Text); //Shaikh Juned 02-02-2023 Added for Fees Amount in Grid
                if (txtRoutePath.Text == string.Empty || txtRoutePath.Text == "")
                {
                    txtRoutePath.Text = ddlStopName.SelectedItem.Text;
                }
                else
                {
                    txtRoutePath.Text = txtRoutePath.Text + " - " + ddlStopName.SelectedItem.Text;
                }
                dt.Rows.Add(dr);
                dt.DefaultView.Sort = "SEQNO";
                dt = dt.DefaultView.ToTable();

                Session["RecTbl"] = dt;
                lvRoutePath.DataSource = dt;
                lvRoutePath.DataBind();
                lvRoutePath.Visible = true;
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                if (dr["DISTANCE"].ToString() == "0" && dt.Rows.Count == 1)
                {
                    chkSource.Checked = false;
                    chkSource.Visible = false;
                }
            }
            else
            {
                if (chkSource.Checked == true)
                {
                    chkSource.Checked = false;
                    chkSource.Visible = false;
                }
                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();

                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["STOPNAME"] = ddlStopName.SelectedItem.Text == null ? string.Empty : ddlStopName.SelectedItem.Text;
                if (txtKM.Text != string.Empty)
                {
                    dr["DISTANCE"] = txtKM.Text.Trim() == null ? string.Empty : Convert.ToString(txtKM.Text.Trim()).Replace(',', ' ');
                }
                else
                {
                    dr["DISTANCE"] = "0";
                }
                dr["STOPNO"] = ddlStopName.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlStopName.SelectedValue);
                //  dr["SEQNO"] = lblStopSeq.Text.Trim() == null ? 0 : Convert.ToDecimal(lblStopSeq.Text);
                dr["ROUTE_FEES"] = txtFees.Text == "0" ? 0 : Convert.ToDecimal(txtFees.Text); //Shaikh Juned 02-02-2023 Added for Fees Amount in Grid
                txtRoutePath.Text = ddlStopName.SelectedItem.Text;
                objVM.ROUTECODE = ddlStopName.SelectedValue;
                objVM.KM = dr["DISTANCE"].ToString();

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                dt.DefaultView.Sort = "SEQNO";
                dt = dt.DefaultView.ToTable();

                Session["RecTbl"] = dt;
                lvRoutePath.DataSource = dt;
                lvRoutePath.DataBind();
                ClearRec();
                lvRoutePath.Visible = true;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ClearRec()
    {

        ddlStopName.SelectedIndex = 0;
        txtKM.Text = string.Empty;
        ddlAddStop.SelectedValue = "0";
        txtFees.Text = string.Empty;
        //chkSource.Checked = false;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];

                if (dt.Rows[Convert.ToInt32(btnDelete.CommandArgument) - 1]["DISTANCE"].ToString() == "0" && dt.Rows.Count > 1)
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Delete other stop name before Source station.", this.Page);
                }
                else
                {
                    if (dt.Rows[Convert.ToInt32(btnDelete.CommandArgument) - 1]["DISTANCE"].ToString() == "0" && dt.Rows.Count == 1)
                    {
                        dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                        chkSource.Visible = true;
                    }
                    else
                    {
                        dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));

                    }
                }

                txtRoutePath.Text = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtRoutePath.Text == string.Empty)
                    {
                        txtRoutePath.Text = dr["STOPNAME"].ToString();
                    }
                    else
                    {
                        txtRoutePath.Text = txtRoutePath.Text + " - " + dr["STOPNAME"].ToString();
                    }
                }
                Session["RecTbl"] = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["SRNO"] = i + 1;
                }
                Session["RecTbl"] = dt;
                BindAddRouteNameDropDown(ddlAddStop, dt);
                lvRoutePath.DataSource = dt;
                lvRoutePath.DataBind();
                lvRoutePath.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {

        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {

                    chkSource.Visible = false;
                    datRow = dr;
                    break;



                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

        return datRow;


    }

    private bool CheckDuplicateStopName(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["STOPNAME"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindlistView();
    }


}