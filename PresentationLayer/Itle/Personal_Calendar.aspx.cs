//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ITLE                                                  
// PAGE NAME     : PERSONAL_CALENDAR
// CREATION DATE : 1-FEB-2014
// CREATED BY    : TARUN DAS
// MODIFIED DATE :                                                            
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

public partial class Itle_Personal_Calendar : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    //AssetAllotmentController objAssetAllotmentController = new AssetAllotmentController();
    IPersonal_Calendar objIPC = new IPersonal_Calendar();
    IPersonal_CalendarController objIPCC = new IPersonal_CalendarController();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    //if (Request.QueryString["pageno"] != null)
                    //{
                    //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //}

                    Calendar1.EventStartDateColumnName = "EventStartDate";
                    Calendar1.EventEndDateColumnName = "EventEndDate";
                    Calendar1.EventDescriptionColumnName = "EventDescription";
                    Calendar1.EventHeaderColumnName = "EventHeader";
                    Calendar1.EventBackColorName = "EventBackColor";
                    Calendar1.EventForeColorName = "EventForeColor";
                    ViewState["action"] = "add";

                    Calendar1.EventSource = GetEvents();
                    listRow.Visible = false;
                    DtEntry.Visible = false;

                }
                if (Page.IsPostBack)
                {
                    //  divData.Visible = true;
                    listRow.Visible = true;
                    DtEntry.Visible = true;
                    Calendar1.EventSource = GetEvents();
                    BindListview();
                }
                //this.ShowAllAssestAllotment();
            }
            //Calendar1.EventSource = GetEvents();
            //BindListview();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Personal_Calendar.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }    

    #endregion

    #region Actions

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();
                if (string.IsNullOrEmpty(txtHeader.Text) & string.IsNullOrEmpty(txtDesc.Text))
                {
                    this.ShowMessage("Please Enter the Header and Description");
                    return;
                }
                else
                {

                    /// Add Personal Calendar
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        objIPC.Operation = "INSERT";
                        objIPC.HEADER = txtHeader.Text.Trim();
                        objIPC.DESCRIPTION = txtDesc.Text.Trim();
                        objIPC.MAINDATE = Convert.ToDateTime(ViewState["selecteddate"]);
                        objIPC.USERID = Session["username"].ToString();
                        cs = (CustomStatus)objIPCC.PersonalCalendar_SaveUpdateDelete(objIPC);
                        if (cs.Equals(CustomStatus.RecordSaved))
                            objCommon.DisplayMessage("Record Saved successfully!!", this.Page);
                    }

                    /// Update Personal Calendar
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objIPC.Operation = "UPDATE";
                        objIPC.HEADER = txtHeader.Text.Trim();
                        objIPC.DESCRIPTION = txtDesc.Text.Trim();
                        objIPC.MAINDATE = Convert.ToDateTime(ViewState["selecteddate"]);
                        objIPC.ID = Convert.ToInt32(ViewState["ID"]);
                        cs = (CustomStatus)objIPCC.PersonalCalendar_SaveUpdateDelete(objIPC);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                            objCommon.DisplayMessage("Record Updated successfully!!", this.Page);
                        ViewState["action"] = "add";

                    }

                    //gvbind();
                    BindListview();
                    Calendar1.EventSource = GetEvents();
                    Clear();
                    //divData.Visible = true;
                    listRow.Visible = true;
                    DtEntry.Visible = true;

                    if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                        this.ShowMessage("Unable to complete the operation.");
                    //else
                    //this.ShowAllAssestAllotment();
                }
                this.Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Personal_Calendar.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDeleteQualDetail_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = new CustomStatus();

            ImageButton btnDelete = sender as ImageButton;
            int ID = Int32.Parse(btnDelete.CommandArgument);
            objIPC.Operation = "DELETE";
            objIPC.ID = ID;
            cs = (CustomStatus)objIPCC.PersonalCalendar_SaveUpdateDelete(objIPC);

            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayMessage("Record Delete successfully!!", this.Page);

            BindListview();
            Calendar1.EventSource = GetEvents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PersonalCalendar.btnDeleteQualDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int ID = Int32.Parse(editButton.CommandArgument);
            objIPC.Operation = "LoadById";
            objIPC.ID = ID;
            DataSet ds = objIPCC.GetAllPersonalCalendar(objIPC);
            txtHeader.Text = ds.Tables[0].Rows[0]["EventHeader"].ToString();
            txtDesc.Text = ds.Tables[0].Rows[0]["EventDescription"].ToString();
            //txtd
            ViewState["action"] = "edit";
            ViewState["ID"] = ID;
            //DataSet ds = objAssetAllotmentController.GetAssetAllotmentByNo(assetAllotmentNo);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    BindDataToControls(ds.Tables[0].Rows[0]);

            //    ViewState["action"] = "edit";
            //    ViewState["AssetAllotmentNo"] = ds.Tables[0].Rows[0]["ASSET_ALLOTMENT_NO"].ToString();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Personal_Calendar.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void dpAessetAllotment_PreRender(object sender, EventArgs e)
    {
        //ShowAllAssestAllotment();
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            SelectedDatesCollection theDates = Calendar1.SelectedDates;
            DataTable dtSelectedDateEvents = Calendar1.EventSource;//  Calendar1.EventSource.Clone();
            DataRow dr;
            DateTime Dtt = theDates[0].Date;
            string Dtts = (Dtt).ToString("dd/MM/yyyy");
            ViewState["selecteddate"] = Dtts;

            //SQLHelper SqlHlp = new SQLHelper(objCommon._client_constr);

            //SqlParameter[] objParams = null;

            //objParams = new SqlParameter[3];
            //objParams[0] = new SqlParameter("@P_IDNO", Session["STUDID"]);
            //objParams[1] = new SqlParameter("@P_Operation", "LoadDateUserId");
            //objParams[2] = new SqlParameter("@P_DateFilter", Dtts);
            //DataSet ds = SqlHlp.ExecuteDataSetSP("PKG_ITLE_SP_GET_PERSONAL_CALENDER", objParams);
            //
            objIPC.Operation = "LoadDateUserId";
            objIPC.USERID = Session["username"].ToString(); ;
            objIPC.MAINDATE = Convert.ToDateTime(ViewState["selecteddate"]);

            DataSet ds = objIPCC.GetAllPersonalCalendar(objIPC);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAssetAllotment.DataSource = ds;
                lvAssetAllotment.DataBind();
                //divData.Visible = true;
                listRow.Visible = true;
                DtEntry.Visible = true;
            }
            else
            {
                lvAssetAllotment.DataSource = null;
                lvAssetAllotment.DataBind();
                //divData.Visible = true;
                listRow.Visible = true;
                DtEntry.Visible = true;
                Clear();

                //RecurseControls(gvSelectedDateEvents.Controls[0].Controls);
            }
            Clear();
            //divData.Visible = true;
            listRow.Visible = true;
            DtEntry.Visible = true;

        }
        catch (Exception ex)
        {

        }

    }   

    //    private AssetAllotment BindDataFromControls()
    //{
    //    AssetAllotment objAssetAllotment = new AssetAllotment();
    //    try
    //    {

    //        if (ddlRoom.SelectedValue != null && ddlRoom.SelectedIndex > 0)
    //            objAssetAllotment.RoomNo = (ddlRoom.SelectedValue != string.Empty ? int.Parse(ddlRoom.SelectedValue) : 0);

    //        if (ddlAsset.SelectedValue != null && ddlAsset.SelectedIndex > 0)
    //            objAssetAllotment.AssetNo = (ddlAsset.SelectedValue != string.Empty ? int.Parse(ddlAsset.SelectedValue) : 0);

    //        objAssetAllotment.Quantity =  Convert.ToInt32(txtAssetQty.Text.Trim());
    //        objAssetAllotment.AllotmentDate = Convert.ToDateTime(txtAllotmentDate.Text.Trim());
    //        objAssetAllotment.AllotmentCode = txtAllotmentCode.Text.Trim();
    //        objAssetAllotment.CollegeCode = Session["colcode"].ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.BindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable");
    //    }
    //    return objAssetAllotment;
    //}

    #endregion

    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Personal_Calendar.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Personal_Calendar.aspx");
        }
    }

    private void BindListview()
    {
        objIPC.Operation = "LoadDateUserId";
        objIPC.USERID = Session["username"].ToString(); ;
        objIPC.MAINDATE = Convert.ToDateTime(ViewState["selecteddate"]);
        DataSet ds = objIPCC.GetAllPersonalCalendar(objIPC);
        lvAssetAllotment.DataSource = ds;
        lvAssetAllotment.DataBind();
    }

    private void ShowAllAssestAllotment()
    {
        try
        {
            //DataSet ds = objAssetAllotmentController.GetAllAssetAllotment();
            //lvAssetAllotment.DataSource = ds;
            //lvAssetAllotment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.ShowAllAssestAllotment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private DataTable GetEvents()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Id", Type.GetType("System.Int32"));
        dt.Columns.Add("EventStartDate", Type.GetType("System.DateTime"));
        dt.Columns.Add("EventEndDate", Type.GetType("System.DateTime"));
        dt.Columns.Add("EventHeader", Type.GetType("System.String"));
        dt.Columns.Add("EventDescription", Type.GetType("System.String"));
        dt.Columns.Add("EventForeColor", Type.GetType("System.String"));
        dt.Columns.Add("EventBackColor", Type.GetType("System.String"));



        DataRow dr;
        objIPC.Operation = "LoadDefault";
        objIPC.USERID = Session["username"].ToString();
        DataSet ds = objIPCC.GetAllPersonalCalendar(objIPC);  //SqlHlp.ExecuteDataSetSP("PKG_ITLE_SP_GET_PERSONAL_CALENDER", objParams);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dtr in ds.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["Id"] = dtr["id"];// ds.Tables[0].Rows[i][0].ToString();
                dr["EventStartDate"] = dtr["EventStartDate"];// ds.Tables[0].Rows[i][6].ToString();
                dr["EventEndDate"] = dtr["EventEndDate"];//ds.Tables[0].Rows[i][6].ToString();
                dr["EventHeader"] = dtr["EventHeader"];//ds.Tables[0].Rows[i][1].ToString();
                dr["EventDescription"] = dtr["EventDescription"];//ds.Tables[0].Rows[i][2].ToString();
                dr["EventForeColor"] = "White";
                dr["EventBackColor"] = "#a389d4";
                dt.Rows.Add(dr);
            }
        }
        //----------------$$$$$------------------
        // Yesterday's Events
        //dr = dt.NewRow();
        //dr["Id"] = idCount++;
        //dr["EventStartDate"] = DateTime.Now.AddDays(-1);
        //dr["EventEndDate"] = DateTime.Now.AddDays(-1);
        //dr["EventHeader"] = "My Yesterday's Single Day Event";
        //dr["EventDescription"] = "My Yesterday's Single Day Event Details";
        //dr["EventForeColor"] = "White";
        //dr["EventBackColor"] = "Navy";
        //dt.Rows.Add(dr);

        //// Three Day's Event Starting Tomorrow
        //dr = dt.NewRow();
        //dr["Id"] = idCount++;
        //dr["EventStartDate"] = DateTime.Now.AddDays(1);
        //dr["EventEndDate"] = DateTime.Now.AddDays(+3);
        //dr["EventHeader"] = "My Three Days Event";
        //dr["EventDescription"] = "My Three Days Event Details, which starts tomorrow";
        //dr["EventForeColor"] = "White";
        //dr["EventBackColor"] = "Green";
        //dt.Rows.Add(dr);


        return dt;
    }

    private void BindDataToControls(DataRow dr)
    {
        try
        {
            //if (dr["HOSTEL_NO"].ToString() != null &&
            //    ddlHostel.Items.FindByValue(dr["HOSTEL_NO"].ToString()) != null)
            //    ddlHostel.SelectedValue = dr["HOSTEL_NO"].ToString();
            //this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "HB.BLOCK_NAME");

            //if (dr["BL_NO"].ToString() != null &&
            //    ddlBlock.Items.FindByValue(dr["BL_NO"].ToString()) != null)
            //    ddlBlock.SelectedValue = dr["BL_NO"].ToString();
            //this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLK_NO=" + ddlBlock.SelectedValue, "FLOOR_NO");

            //if (dr["FLOOR_NO"].ToString() != null &&
            //    ddlFloor.Items.FindByValue(dr["FLOOR_NO"].ToString()) != null)
            //    ddlFloor.SelectedValue = dr["FLOOR_NO"].ToString();

            //this.objCommon.FillDropDownList(ddlRoom, "ACD_HOSTEL_ROOM", "ROOM_NO", "ROOM_NAME", "BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue) + " and FLOOR_NO=" + Convert.ToInt32(ddlFloor.SelectedValue), "ROOM_NO");
            //if (dr["ROOM_NO"].ToString() != null &&
            //    ddlRoom.Items.FindByValue(dr["ROOM_NO"].ToString()) != null)
            //    ddlRoom.SelectedValue = dr["ROOM_NO"].ToString();

            //if (dr["ASSET_NO"].ToString() != null &&
            //    ddlAsset.Items.FindByValue(dr["ASSET_NO"].ToString()) != null)
            //    ddlAsset.SelectedValue = dr["ASSET_NO"].ToString();

            //if (dr["QUANTITY"].ToString() != null)
            //    txtAssetQty.Text = dr["QUANTITY"].ToString(); 

            //if (dr["ALLOTMENT_DATE"].ToString() != null)
            //    txtAllotmentDate.Text = dr["ALLOTMENT_DATE"].ToString();

            //if (dr["ALLOTMENT_CODE"].ToString() != null)
            //    txtAllotmentCode.Text = dr["ALLOTMENT_CODE"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.BindDataToControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void Clear()
    {
        txtHeader.Text = string.Empty;
        txtDesc.Text = string.Empty;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            //div.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";

        }
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    #endregion
}