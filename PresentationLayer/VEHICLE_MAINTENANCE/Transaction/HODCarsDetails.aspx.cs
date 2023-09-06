//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 21-MAR-2020
// DESCRIPTION   : USE TO ADD HOD CAR DETAILS
//=========================================================================
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

public partial class VEHICLE_MAINTENANCE_Transaction_HODCarsDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();

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
                    ViewState["RecTbl"] = null;
                    lvInTime.Visible = false;
                    divAddDetails.Visible = false;
                    DateTime date = DateTime.Now;
                    txtDate.Text = Convert.ToString(date);
                
                    objCommon.FillDropDownList(ddlTravels, "VEHICLE_TRAVELS_MASTER", "TRAVELS_ID", "TRAVELS_NAME", "", "TRAVELS_ID");
                    BindListView(Convert.ToDateTime(txtDate.Text));

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private void Clear()
    {
        ddlTravels.SelectedIndex = 0;
        txtINTime.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        lvInTime.DataSource = null;
        lvInTime.DataBind();
        lvInTime.Visible = false;
        btnAdd.Visible = true;
        ViewState["HODCARS_ID"] = null;
        ViewState["RecTbl"] = null;

    }


    private void BindListView(DateTime date)
    {
        try
        {
            DataSet ds = null;
            ds = objVMC.GetHODCarsDetails(Convert.ToDateTime(date.ToString("yyyy-MM-dd")));
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvMainList.DataSource = ds;
                //lvMainList.DataBind();
                //divMainList.Visible = true;
               // ViewState["RecTbl"] = ds.Tables[1];  //18-07-2022

                lvMainList.DataSource = ds.Tables[0];
                lvMainList.DataBind();
                divAddDetails.Visible = false ;
                lvInTime.Visible = false ;
                divMainList.Visible = true;
                lvMainList.Visible = true;

            }
            else
            {
                lvInTime.DataSource = null;
                lvInTime.DataBind();
                divAddDetails.Visible = false;
                lvInTime.Visible = false;
                //lvMainList.DataSource = null;
                //lvMainList.DataBind();
                //divMainList.Visible = false;
            }
            //divAddDetails.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #region Add HOD Cars Details
    private DataTable CreateTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ARRIVAL_DATE", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("TRAVELS_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("TRAVELS_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("REGNO", typeof(string)));
        dt.Columns.Add(new DataColumn("IN_TIME", typeof(string)));
        return dt;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            divAddDetails.Visible = true;

            if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)ViewState["RecTbl"];
                DataRow dr = dt.NewRow();

                if (CheckRowExist())
                {
                    MessageBox("Record Already exist.");
                    return;
                }

                int maxVal = 0;
                if (dr != null)
                {
                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                }
                dr["SRNO"] = maxVal + 1;
                dr["ARRIVAL_DATE"] = txtDate.Text == null ? string.Empty : txtDate.Text;
                dr["TRAVELS_ID"] = Convert.ToInt32(ddlTravels.SelectedValue);
                dr["TRAVELS_NAME"] = ddlTravels.SelectedItem.Text;
                dr["REGNO"] = txtRegNo.Text == string.Empty ? "" : txtRegNo.Text.Trim();
              //  dr["IN_TIME"] = Convert.ToDateTime(txtINTime.Text);
                dr["IN_TIME"] = txtINTime.Text;

                dt.Rows.Add(dr);
                // dt = dt.DefaultView.ToTable();
                ViewState["RecTbl"] = dt;
                lvInTime.DataSource = dt;
                lvInTime.DataBind();
                divAddDetails.Visible = true;
                lvInTime.Visible = true;
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();

                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["ARRIVAL_DATE"] = txtDate.Text == null ? string.Empty : txtDate.Text;
                dr["TRAVELS_ID"] = Convert.ToInt32(ddlTravels.SelectedValue);
                dr["TRAVELS_NAME"] = ddlTravels.SelectedItem.Text;
                dr["REGNO"] = txtRegNo.Text == string.Empty ? "" : txtRegNo.Text.Trim();
                //dr["IN_TIME"] = Convert.ToDateTime(txtINTime.Text);
                dr["IN_TIME"] = txtINTime.Text;

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                // dt = dt.DefaultView.ToTable();
                ViewState["RecTbl"] = dt;
                lvInTime.DataSource = dt;
                lvInTime.DataBind();
                ClearRec();
                divAddDetails.Visible = true;
                lvInTime.Visible = true;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ClearRec()
    {
        // txtDate.Text = string.Empty;
        ddlTravels.SelectedIndex = 0;
        txtRegNo.Text = string.Empty;
        txtINTime.Text = string.Empty;

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
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

        return datRow;
    }

  //  public void CheckRowExist()
    private bool CheckRowExist()
    {
        bool retVal = false;
        try
        {           
           DataTable dt = (DataTable)ViewState["RecTbl"];
           if (dt != null)
           {
               var Chk = from dtchk in dt.AsEnumerable()
                         where dtchk.Field<DateTime>("ARRIVAL_DATE") == Convert.ToDateTime(txtDate.Text) && dtchk.Field<int>("TRAVELS_ID") == Convert.ToInt32(ddlTravels.SelectedValue) && dtchk.Field<string>("REGNO") == txtRegNo.Text && dtchk.Field<string>("IN_TIME") == txtINTime.Text
                       select dtchk.Field<int>("SRNO");
               if (Chk.Any())
               {
                   retVal = true;                 
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
        return retVal;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int HODCARS_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["HODCARS_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(HODCARS_ID);
            btnAdd.Visible = true;   //shaikh juned 12-09-2022
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetails(int HODCARS_ID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_HOD_CARS_DETAILS", "*", "", "HODCARS_ID=" + HODCARS_ID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDate.Text = ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString();
                ddlTravels.SelectedValue = ds.Tables[0].Rows[0]["TRAVELS_ID"].ToString();
                txtRegNo.Text = ds.Tables[0].Rows[0]["REGISTRATION_NO"].ToString();
                txtINTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["IN_TIME"]).ToString("hh:mm tt");
                hiddenSrNo.Value = ds.Tables[0].Rows[0]["SRNO"].ToString();

                btnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }



    #endregion

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objVM.HODCARS_ID = 0;
            DataTable dt = null;
            dt = (DataTable)ViewState["RecTbl"];
            txtRegNo.Text = dt.Rows[0]["REGNO"].ToString();
            txtDate.Text = dt.Rows[0]["ARRIVAL_DATE"].ToString();
            //string ArivalDate = txtDate.Text.ToString("yyyy-MM-dd HH:mm:ss");
            txtINTime.Text = dt.Rows[0]["IN_TIME"].ToString();
            string ArivalDate = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm:ss");
            objVM.HODCarsTable = dt;
            objVM.CREATED_BY = Convert.ToInt32(Session["userno"]);
            if (ViewState["HODCARS_ID"] == null)
            {
                //--======start===Shaikh Juned 2-09-2022

                DataSet ds = objCommon.FillDropDown("VEHICLE_HOD_CARS_DETAILS", "REGISTRATION_NO", "ARRIVAL_DATE,IN_TIME", "REGISTRATION_NO='" + txtRegNo.Text + "' and ARRIVAL_DATE='" + ArivalDate + "'", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //string REGISTRATIONNO = dr["REGISTRATION_NO"].ToString();
                        //string ARIVAL_DATE = dr["ARRIVAL_DATE"].ToString();

                        //if (REGISTRATIONNO == txtRegNo.Text & ARIVAL_DATE == txtDate.Text)
                        //{
                        //objCommon.DisplayMessage(this.updProg, "Hod Car's Arrival Is Already Exist.", this.Page);
                            MessageBox("Hod Car's Arrival Is Already Exist.");
                            return;
                        //}

                    }
                }
                //---========end=====

                CustomStatus cs = (CustomStatus)objVMC.HODCarsDetailsIU(objVM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    MessageBox("Record saved successfully.");
                    BindListView(Convert.ToDateTime(txtDate.Text));
                }
            }
            else
            {
                objVM.HODCARS_ID = Convert.ToInt32(ViewState["HODCARS_ID"].ToString());
                objVM.ARRIVAL_DATE = Convert.ToDateTime(txtDate.Text);
                objVM.TRAVELS_ID = Convert.ToInt32(ddlTravels.SelectedValue);
                objVM.REGNO = txtRegNo.Text;
                objVM.IN_TIME = Convert.ToDateTime(txtINTime.Text);
                objVM.CREATED_BY = Convert.ToInt32(Session["userno"]);

                //--======start===Shaikh Juned 2-09-2022

                DataSet ds = objCommon.FillDropDown("VEHICLE_HOD_CARS_DETAILS", "HODCARS_ID", "ARRIVAL_DATE,IN_TIME", "REGISTRATION_NO='" + txtRegNo.Text + "' and ARRIVAL_DATE='" + ArivalDate + "'", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int HODCARSID =Convert.ToInt32( dr["HODCARS_ID"]);
                        //string ARIVAL_DATE = dr["ARRIVAL_DATE"].ToString();

                        if (HODCARSID != Convert.ToInt32(objVM.HODCARS_ID))
                        {
                            objCommon.DisplayMessage(this.updProg, "Hod Car's Arrival Is Already Exist.", this.Page);
                            MessageBox("Hod Car's Arrival Is Already Exist.");
                            return;
                        }

                    }
                }
                //---========end=====

                CustomStatus cs = (CustomStatus)objVMC.UpdateHODCarsDetails(objVM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListView(Convert.ToDateTime(txtDate.Text));
                    MessageBox("Record updated successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

    }


    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["RecTbl"] != null && ((DataTable)ViewState["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)ViewState["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                ViewState["RecTbl"] = dt;
                lvInTime.DataSource = dt;
                lvInTime.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["RecTbl"] = null;
            BindListView(Convert.ToDateTime(txtDate.Text));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_HODCarsDetails.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    

}