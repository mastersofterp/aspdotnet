//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STANDARD FEE DEFINITION (USER CONTROL)
// CREATION DATE : 15-MAY-2009
// CREATED BY    : AMIT YADAV
// MODIFIED BY   : GAURAV SONI
// MODIFIED DATE : 4-DEC-2010
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HostelBusinessLogicLayer.BusinessEntities.Hostel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;


public partial class Payments_StandardFeeDefinition : System.Web.UI.Page
{
    /// <summary>
    /// ORGID CONDITION REMOVED FROM ALL OVER THIS PAGE AS PAGE WILL BE SAME FOR ALL CLIENTS (SONALI BHOR ON 12/12/2022)
    /// </summary>

    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common _objCommon = new Common();
    UAIMS_Common _objUaimsCommon = new UAIMS_Common();

    // This object is used to store reciept_code, degree_no and room_capacity for new fees item    
    StandardFee _newFeeItem = new StandardFee();
    HostelStdFee ObjHs = new HostelStdFee();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            _objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            _objCommon.SetMasterPage(Page, "");
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
                        //lblHelp.Text = _objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["action"] = "add";

                    // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)   
                    //** START
                    //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
                    //{
                        Degree.Visible = false;
                        RoomCapacity.Visible = false;
                        HostelType.Visible = false;

                   // } 
                    //** END


                    // Fill Dropdown lists
                    this._objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
                    this._objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
                    this._objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "BELONGS_TO = 'H'", "");
                    this._objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "", "");
                    this._objCommon.FillDropDownList(ddlRoomCapacity, "ACD_HOSTEL_ROOM", "DISTINCT CAPACITY", "CAPACITY", "", "");
                    /// Fill listbox.
                    /// Onfirst time page loading, we need all fee items in listbox hence send no values 
                    /// in parameters not to filter records.
                    this.FillListbox(string.Empty, 0, 0);
                    
                    this.btnSubmit.Enabled = false;
                }
            }
            else
                this.divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StandardFeeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StandardFeeDefinition.aspx");
        }
    }
    #endregion

    #region Load and Reloading Listbox
    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Call_FillListbox();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Call_FillListbox();
    }
    
    protected void ddlRoomCapacity_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Call_FillListbox();
    }   

    private void Call_FillListbox()
    {
        try
        {
            // Call FillListbox() method by passing appropriate values
            this.FillListbox(
                (this.GetDropDownListValue(ddlReceiptType) != null ? ddlReceiptType.SelectedValue : string.Empty),
                (this.GetDropDownListValue(ddlDegree) != null ? Int32.Parse(ddlDegree.SelectedValue) : 0),
                (this.GetDropDownListValue(ddlRoomCapacity) != null ? Int32.Parse(ddlRoomCapacity.SelectedValue) : 0)
            );
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillListbox(string recieptCode, int degreeNo, int roomCapacity)
    {
        try
        {
            StandardFeeController feeController = new StandardFeeController();
            lstFeesItems.DataSource = feeController.GetStdFeesForListbox(recieptCode, degreeNo, roomCapacity);
            lstFeesItems.DataTextField = "FEE_DESCRIPTION";
            lstFeesItems.DataValueField = "FEE_CAT_NO";
            lstFeesItems.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    ////ADDED ANOTHER Load and Reloading Listbox WHEN ROOM TYPE BY SHUBHAM ON 27072022
    //private void FillListboxRoomtype(string recieptCode, int hostelno, int roomtype)
    //{
    //    try
    //    {
    //        StandardFeeController feeController = new StandardFeeController();
    //        lstFeesItems.DataSource = feeController.GetStdFeesForListbox(recieptCode, degreeNo, roomCapacity);
    //        lstFeesItems.DataTextField = "FEE_DESCRIPTION";
    //        lstFeesItems.DataValueField = "FEE_CAT_NO";
    //        lstFeesItems.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            _objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion

    #region Fill Fee Entry Grid

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptType.SelectedIndex > 0 )
            {
                this.DisplayFeeName();
                this.DisplayFeeDefinitionGrid();
                this.btnSubmit.Enabled = true;
                /// Refill listbox.
                /// while reloading, we need all fee items in listbox hence send no values 
                /// in parameters not to filter records.
                this.FillListbox(string.Empty, 0, 0);
                btnReport.Enabled = true;
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('Please select complete criteria.');</script>");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lstFeesItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
            //{
                lblFeeName.Text = lstFeesItems.SelectedItem.Text;
                StandardFeeController feeController = new StandardFeeController();
                DataSet ds = feeController.GetHostelStandardFee(lstFeesItems.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlHostelSessionNo.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_SESSION_NO"].ToString();
                    ddlHostel.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_NO"].ToString();
                   // ddlHosteltype.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_TYPE"].ToString();
                    ddlReceiptType.SelectedValue = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                    this._objCommon.FillDropDownList(ddlRoomType, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "TYPE_NO");
                   // ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                    ddlRoomType.SelectedValue = ds.Tables[0].Rows[0]["ROOM_TYPE"].ToString();
                    //ddlRoomCapacity

                    this.BindListView(ds);
                    this.btnSubmit.Enabled = true;
                    btnReport.Enabled = true;
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('Record Not Exist.');</script>");

                    // Head.InnerText = "Search Standard Fee By Name";
                    lv.Visible = false;
                }
            //}

            // START   // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  

            //else
            //{
            //    lblFeeName.Text = lstFeesItems.SelectedItem.Text;
            //StandardFeeController feeController = new StandardFeeController();
            //DataSet ds = feeController.GetHostelStandardFee(lstFeesItems.SelectedValue);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlHostelSessionNo.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_SESSION_NO"].ToString();
            //    ddlHostel.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_NO"].ToString();
            //    ddlHosteltype.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_TYPE"].ToString();
            //    ddlReceiptType.SelectedValue = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            //    ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //    //ddlRoomCapacity

            //    this.BindListView(ds);
            //    this.btnSubmit.Enabled = true;
            //    btnReport.Enabled = true;
            //}
            //else
            //{
            //    Response.Write("<script type='text/javascript'>alert('Record Not Exist.');</script>");

            //    // Head.InnerText = "Search Standard Fee By Name";
            //    lv.Visible = false;
            //}
            //}

            //END
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void DisplayFeeDefinitionGrid()
    {
        try
        {
            // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  
            //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
            //{
                DataSet ds = this.HostelStandardFeeRoomType(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlRoomType.SelectedValue), Convert.ToInt32(ddlHostelSessionNo.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue));
                this.BindListView(ds);
            //}
            //else
            //{
            //    StandardFeeController feeController = new StandardFeeController();
            //    DataSet ds = feeController.HostelStandardFee(ddlReceiptType.SelectedValue, ddlDegree.SelectedValue, ddlHostelSessionNo.SelectedValue, ddlHosteltype.SelectedValue,ddlHostel.SelectedValue);
            //    this.BindListView(ds);
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView(DataSet ds)
    {
        try
        {
            this.lv.DataSource = ds;
            this.lv.DataBind();
            this.ShowFeeTotal();
            lv.Visible = true;
            btnSubmit.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowFeeTotal()
    {
        try
        {
            bool isFirstFeeItem = true;
            foreach (ListViewDataItem item in lv.Items)
            {
                for (int sem = 1; sem <= 10; sem++)
                {
                    string strFeeItemAmt = (item.FindControl("txtSem" + sem.ToString()) as TextBox).Text.Trim();
                    double feeItemAmt = (strFeeItemAmt != null && strFeeItemAmt != string.Empty) ? double.Parse(strFeeItemAmt) : 0;

                    string strTotalFeeItemAmt = (lv.FindControl("txtSem" + sem.ToString() + "TotalAmt") as TextBox).Text.Trim();
                    double totalFeeItemAmt = (strTotalFeeItemAmt != null && strTotalFeeItemAmt != string.Empty) ? double.Parse(strTotalFeeItemAmt) : 0;

                    if (isFirstFeeItem)
                        totalFeeItemAmt = feeItemAmt;
                    else
                        totalFeeItemAmt += feeItemAmt;

                    (lv.FindControl("txtSem" + sem.ToString() + "TotalAmt") as TextBox).Text = totalFeeItemAmt.ToString();
                }
                isFirstFeeItem = false;
            }
            this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmounts(); </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Save Data

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
            //{
                int feeCatNo = 0;

            foreach (ListViewDataItem dataRow in lv.Items)
            {
                ObjHs.Session_No = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
                ObjHs.Hostel_Name = Convert.ToInt32(ddlHostel.SelectedValue);
                ObjHs.Hostel_No = Convert.ToInt32(ddlHostel.SelectedValue);
                ObjHs.CAPACITY = Convert.ToInt32(ddlRoomCapacity.SelectedValue);
                ObjHs.RoomType = Convert.ToInt32(ddlRoomType.SelectedValue);
                int count = Convert.ToInt32(_objCommon.LookUp("ACD_STANDARD_FEES S INNER JOIN ACD_HOSTEL_STD_FEE H ON S.STD_FEE_NO=H.STD_FEE_NO", "isnull(count(*),0)count", "RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND HOSTEL_SESSION_NO = " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + " AND H.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND H.ROOM_TYPE=" + Convert.ToInt32(ddlRoomType.SelectedValue)));
                if (count != 0)
                {
                    feeCatNo = Convert.ToInt32(_objCommon.LookUp("ACD_STANDARD_FEES S INNER JOIN ACD_HOSTEL_STD_FEE H ON S.STD_FEE_NO=H.STD_FEE_NO", "isnull(FEE_CAT_NO,0)FEE_CAT_NO", "RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND HOSTEL_SESSION_NO = " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + " AND H.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND H.ROOM_TYPE=" + Convert.ToInt32(ddlRoomType.SelectedValue)));
                }
                feeCatNo = this.UpdateStandardFeeItem(dataRow, feeCatNo);
            }
            this.FillListbox(string.Empty, 0, 0);
            this.DisplayFeeName();                       //added for ticket no : 47653
            Common objcommon = new Common();
            objcommon.DisplayMessage(pnlFeeTable, "Standard Fees Saved Successfully.!", this.Page);
            // ClearControls();
            this.lv.Visible = true;
            //btnReport.Enabled = true;
            //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('Record saved successfully.'); </script>";
            btnReport.Enabled = true;
                //ClearControls();

            //}
            // START   // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  

            //else
            //{
            //    /// This variable is used to store and maintain FeeCatNo generated in the stored procedure 
            ///// while inserting data for F1 fee head in case of new standard fee record 
            //int feeCatNo = 0;

            //foreach (ListViewDataItem dataRow in lv.Items)
            //{
            //    _newFeeItem.Session_No = Convert.ToInt32(ddlHostelSessionNo.SelectedValue);
            //    _newFeeItem.Hostel_Name = Convert.ToInt32(ddlHostel.SelectedValue);
            //    _newFeeItem.Hostel_No = Convert.ToInt32(ddlHostel.SelectedValue);
            //    _newFeeItem.CAPACITY = Convert.ToInt32(ddlRoomCapacity.SelectedValue);
            //    int count = Convert.ToInt32(_objCommon.LookUp("ACD_STANDARD_FEES S INNER JOIN ACD_HOSTEL_STD_FEE H ON S.STD_FEE_NO=H.STD_FEE_NO", "isnull(count(*),0)count", "RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND S.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND H.HOSTEL_TYPE=" + Convert.ToInt32(ddlHosteltype.SelectedValue) + " AND HOSTEL_SESSION_NO = " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + " AND H.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ""));
            //    if (count != 0)
            //    {
            //        feeCatNo = Convert.ToInt32(_objCommon.LookUp("ACD_STANDARD_FEES S INNER JOIN ACD_HOSTEL_STD_FEE H ON S.STD_FEE_NO=H.STD_FEE_NO", "isnull(FEE_CAT_NO,0)FEE_CAT_NO", "RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND S.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND H.HOSTEL_TYPE=" + Convert.ToInt32(ddlHosteltype.SelectedValue) + " AND HOSTEL_SESSION_NO = " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + " AND H.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ""));
            //    }
            //    feeCatNo = this.UpdateStandardFeeItem(dataRow, feeCatNo);
            //}

            ///// Refill listbox.
            ///// while reloading, we need all fee items in listbox hence send no values 
            ///// in parameters not to filter records.
            //this.FillListbox(string.Empty, 0, 0);
            //Common objcommon = new Common();
            //objcommon.DisplayMessage(pnlFeeTable, "Standard Fees Saved Successfully.!", this.Page);
            //// ClearControls();
            //this.lv.Visible = true;
            ////btnReport.Enabled = true;
            ////this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('Record saved successfully.'); </script>";
            //btnReport.Enabled = true;
            //    //ClearControls();
            //     }

            //END
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private int UpdateStandardFeeItem(ListViewDataItem dataRow, int feeCatNo)
    {
        StandardFee feeItem = new StandardFee();
        try
        {
            //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
            //{
                string feeCatNum = ((HiddenField)dataRow.FindControl("hidFeeCatNo")).Value;
                feeItem.FeeCatNo = (feeCatNum != null && feeCatNum != string.Empty) ? Convert.ToInt32(feeCatNum) : 0;
                feeItem.FeeCatNo = feeCatNo;

                feeItem.FeeHead = ((HiddenField)dataRow.FindControl("hidFeeHead")).Value;
                feeItem.FeeDesc = ((HiddenField)dataRow.FindControl("hidFeeDesc")).Value;
                feeItem.FeeDesc = lblFeeName.Text;
                feeItem.RecieptCode = ((HiddenField)dataRow.FindControl("hidRecieptCode")).Value;
                feeItem.RecieptCode = ddlReceiptType.SelectedValue;

                string srNo = ((HiddenField)dataRow.FindControl("hidSrNo")).Value;
                feeItem.SerialNo = (srNo != null && srNo != string.Empty) ? Convert.ToInt32(srNo) : 0;

                string degreeNo = ((HiddenField)dataRow.FindControl("hidDegreeNo")).Value;
                feeItem.DegreeNo = (degreeNo != null && degreeNo != string.Empty) ? Convert.ToInt32(degreeNo) : 0;

                //string degreeNo = ((HiddenField)dataRow.FindControl("hidDegreeNo")).Value;
                //// feeItem.DegreeNo = (degreeNo != null && degreeNo != string.Empty) ? Convert.ToInt32(degreeNo) : 0;
                //feeItem.DegreeNo = Degreeno; // Convert.ToInt32(ddlDegree.SelectedValue);

                string batchNo = ((HiddenField)dataRow.FindControl("hidBatchNo")).Value;
                feeItem.BatchNo = (batchNo != null && batchNo != string.Empty) ? Convert.ToInt32(batchNo) : 0;

                string sem1_Fee = ((TextBox)dataRow.FindControl("txtSem1")).Text.Trim();
                feeItem.Sem1_Fee = (sem1_Fee != null && sem1_Fee != string.Empty) ? Convert.ToDouble(sem1_Fee) : 0;

                string sem2_Fee = ((TextBox)dataRow.FindControl("txtSem2")).Text.Trim();
                feeItem.Sem2_Fee = (sem2_Fee != null && sem2_Fee != string.Empty) ? Convert.ToDouble(sem2_Fee) : 0;

                string sem3_Fee = ((TextBox)dataRow.FindControl("txtSem3")).Text.Trim();
                feeItem.Sem3_Fee = (sem3_Fee != null && sem3_Fee != string.Empty) ? Convert.ToDouble(sem3_Fee) : 0;

                string sem4_Fee = ((TextBox)dataRow.FindControl("txtSem4")).Text.Trim();
                feeItem.Sem4_Fee = (sem4_Fee != null && sem4_Fee != string.Empty) ? Convert.ToDouble(sem4_Fee) : 0;

                string sem5_Fee = ((TextBox)dataRow.FindControl("txtSem5")).Text.Trim();
                feeItem.Sem5_Fee = (sem5_Fee != null && sem5_Fee != string.Empty) ? Convert.ToDouble(sem5_Fee) : 0;

                string sem6_Fee = ((TextBox)dataRow.FindControl("txtSem6")).Text.Trim();
                feeItem.Sem6_Fee = (sem6_Fee != null && sem6_Fee != string.Empty) ? Convert.ToDouble(sem6_Fee) : 0;

                string sem7_Fee = ((TextBox)dataRow.FindControl("txtSem7")).Text.Trim();
                feeItem.Sem7_Fee = (sem7_Fee != null && sem7_Fee != string.Empty) ? Convert.ToDouble(sem7_Fee) : 0;

                string sem8_Fee = ((TextBox)dataRow.FindControl("txtSem8")).Text.Trim();
                feeItem.Sem8_Fee = (sem8_Fee != null && sem8_Fee != string.Empty) ? Convert.ToDouble(sem8_Fee) : 0;

                string sem9_Fee = ((TextBox)dataRow.FindControl("txtSem9")).Text.Trim();
                feeItem.Sem9_Fee = (sem9_Fee != null && sem9_Fee != string.Empty) ? Convert.ToDouble(sem9_Fee) : 0;

                string sem10_Fee = ((TextBox)dataRow.FindControl("txtSem10")).Text.Trim();
                feeItem.Sem10_Fee = (sem10_Fee != null && sem10_Fee != string.Empty) ? Convert.ToDouble(sem10_Fee) : 0;

                feeItem.CollegeCode = Session["colcode"].ToString();
                if (sem1_Fee == "" && sem2_Fee == "" && sem3_Fee == "" && sem4_Fee == "" && sem5_Fee == "" && sem6_Fee == "" && sem7_Fee == "" && sem8_Fee == "" && sem9_Fee == "" && sem10_Fee == "")
                //if (feeItem.Sem1_Fee == 0.0 && feeItem.Sem2_Fee == 0.0 && feeItem.Sem3_Fee == 0.0 && feeItem.Sem4_Fee == 0.0 && feeItem.Sem5_Fee == 0.0 && feeItem.Sem6_Fee == 0.0 && feeItem.Sem7_Fee == 0.0 && feeItem.Sem8_Fee == 0.0 && feeItem.Sem9_Fee == 0.0 && feeItem.Sem10_Fee == 0.0)
                {
                    _objCommon.DisplayMessage(pnlFeeTable, "Please do not empty of Semester wise standard fees.!", this.Page);
                    return feeItem.FeeCatNo;
                }
                /// If degree no and Room Capacity is equal to 0 then record is in new mode.
                /// In new mode assign values of their respective drop down list.
                if (feeItem.DegreeNo == 0 && feeItem.BatchNo == 0)
                {
                    /// If degree no, batch no and payment type is not defined for fee head 'F1' 
                    /// then this record is a new standard fee record.
                    if (feeItem.FeeHead == "F1")
                    {
                        feeItem.RecieptCode = ddlReceiptType.SelectedValue;
                        feeItem.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                        feeItem.RoomCapacity = Convert.ToInt32(ddlRoomCapacity.SelectedValue);
                        feeItem.FeeCatNo = feeCatNo;
                        feeItem.FeeDesc = lblFeeName.Text;
                    }
                    else
                    {
                        /// this record can be a new standard fee record or new fee item in predefined std. fees.
                        feeItem.RecieptCode = _newFeeItem.RecieptCode;
                        feeItem.DegreeNo = _newFeeItem.DegreeNo;
                        feeItem.RoomCapacity = _newFeeItem.RoomCapacity;
                        feeItem.FeeDesc = _newFeeItem.FeeDesc;
                        feeItem.FeeCatNo = _newFeeItem.FeeCatNo;
                    }
                }
                //feeItem.CollegeId = Convert.ToInt32(ddlSchClg.SelectedValue);

                // Initialize objSF object to contain basic unique identifying data.
                if (feeItem.FeeHead == "F1")
                    _newFeeItem = feeItem;

                StandardFeeController feeController = new StandardFeeController();
                this.AddUpdateStandardFeesByRoomType(ref feeItem, ObjHs);
          //  }
            // START   // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  

            //else
            //{
            //    string feeCatNum = ((HiddenField)dataRow.FindControl("hidFeeCatNo")).Value;
            //feeItem.FeeCatNo = (feeCatNum != null && feeCatNum != string.Empty) ? Convert.ToInt32(feeCatNum) : 0;
            //feeItem.FeeCatNo = feeCatNo;

            //feeItem.FeeHead = ((HiddenField)dataRow.FindControl("hidFeeHead")).Value;
            //feeItem.FeeDesc = ((HiddenField)dataRow.FindControl("hidFeeDesc")).Value;
            //feeItem.FeeDesc = lblFeeName.Text;
            //feeItem.RecieptCode = ((HiddenField)dataRow.FindControl("hidRecieptCode")).Value;
            //feeItem.RecieptCode = ddlReceiptType.SelectedValue;

            //string srNo = ((HiddenField)dataRow.FindControl("hidSrNo")).Value;
            //feeItem.SerialNo = (srNo != null && srNo != string.Empty) ? Convert.ToInt32(srNo) : 0;

            //string degreeNo = ((HiddenField)dataRow.FindControl("hidDegreeNo")).Value;
            //feeItem.DegreeNo = (degreeNo != null && degreeNo != string.Empty) ? Convert.ToInt32(degreeNo) : 0;

            ////string degreeNo = ((HiddenField)dataRow.FindControl("hidDegreeNo")).Value;
            ////// feeItem.DegreeNo = (degreeNo != null && degreeNo != string.Empty) ? Convert.ToInt32(degreeNo) : 0;
            ////feeItem.DegreeNo = Degreeno; // Convert.ToInt32(ddlDegree.SelectedValue);

            //string batchNo = ((HiddenField)dataRow.FindControl("hidBatchNo")).Value;
            //feeItem.BatchNo = (batchNo != null && batchNo != string.Empty) ? Convert.ToInt32(batchNo) : 0;

            //string sem1_Fee = ((TextBox)dataRow.FindControl("txtSem1")).Text.Trim();
            //feeItem.Sem1_Fee = (sem1_Fee != null && sem1_Fee != string.Empty) ? Convert.ToDouble(sem1_Fee) : 0;

            //string sem2_Fee = ((TextBox)dataRow.FindControl("txtSem2")).Text.Trim();
            //feeItem.Sem2_Fee = (sem2_Fee != null && sem2_Fee != string.Empty) ? Convert.ToDouble(sem2_Fee) : 0;

            //string sem3_Fee = ((TextBox)dataRow.FindControl("txtSem3")).Text.Trim();
            //feeItem.Sem3_Fee = (sem3_Fee != null && sem3_Fee != string.Empty) ? Convert.ToDouble(sem3_Fee) : 0;

            //string sem4_Fee = ((TextBox)dataRow.FindControl("txtSem4")).Text.Trim();
            //feeItem.Sem4_Fee = (sem4_Fee != null && sem4_Fee != string.Empty) ? Convert.ToDouble(sem4_Fee) : 0;

            //string sem5_Fee = ((TextBox)dataRow.FindControl("txtSem5")).Text.Trim();
            //feeItem.Sem5_Fee = (sem5_Fee != null && sem5_Fee != string.Empty) ? Convert.ToDouble(sem5_Fee) : 0;

            //string sem6_Fee = ((TextBox)dataRow.FindControl("txtSem6")).Text.Trim();
            //feeItem.Sem6_Fee = (sem6_Fee != null && sem6_Fee != string.Empty) ? Convert.ToDouble(sem6_Fee) : 0;

            //string sem7_Fee = ((TextBox)dataRow.FindControl("txtSem7")).Text.Trim();
            //feeItem.Sem7_Fee = (sem7_Fee != null && sem7_Fee != string.Empty) ? Convert.ToDouble(sem7_Fee) : 0;

            //string sem8_Fee = ((TextBox)dataRow.FindControl("txtSem8")).Text.Trim();
            //feeItem.Sem8_Fee = (sem8_Fee != null && sem8_Fee != string.Empty) ? Convert.ToDouble(sem8_Fee) : 0;

            //string sem9_Fee = ((TextBox)dataRow.FindControl("txtSem9")).Text.Trim();
            //feeItem.Sem9_Fee = (sem9_Fee != null && sem9_Fee != string.Empty) ? Convert.ToDouble(sem9_Fee) : 0;

            //string sem10_Fee = ((TextBox)dataRow.FindControl("txtSem10")).Text.Trim();
            //feeItem.Sem10_Fee = (sem10_Fee != null && sem10_Fee != string.Empty) ? Convert.ToDouble(sem10_Fee) : 0;

            //feeItem.CollegeCode = Session["colcode"].ToString();
            //if (sem1_Fee == "" && sem2_Fee == "" && sem3_Fee == "" && sem4_Fee == "" && sem5_Fee == "" && sem6_Fee == "" && sem7_Fee == "" && sem8_Fee == "" && sem9_Fee == "" && sem10_Fee == "")
            ////if (feeItem.Sem1_Fee == 0.0 && feeItem.Sem2_Fee == 0.0 && feeItem.Sem3_Fee == 0.0 && feeItem.Sem4_Fee == 0.0 && feeItem.Sem5_Fee == 0.0 && feeItem.Sem6_Fee == 0.0 && feeItem.Sem7_Fee == 0.0 && feeItem.Sem8_Fee == 0.0 && feeItem.Sem9_Fee == 0.0 && feeItem.Sem10_Fee == 0.0)
            //{
            //    _objCommon.DisplayMessage(pnlFeeTable, "Please do not empty of Semester wise standard fees.!", this.Page);
            //    return feeItem.FeeCatNo;
            //}
            ///// If degree no and Room Capacity is equal to 0 then record is in new mode.
            ///// In new mode assign values of their respective drop down list.
            //if (feeItem.DegreeNo == 0 && feeItem.BatchNo == 0)
            //{
            //    /// If degree no, batch no and payment type is not defined for fee head 'F1' 
            //    /// then this record is a new standard fee record.
            //    if (feeItem.FeeHead == "F1")
            //    {
            //        feeItem.RecieptCode = ddlReceiptType.SelectedValue;
            //        feeItem.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            //        feeItem.RoomCapacity = Convert.ToInt32(ddlRoomCapacity.SelectedValue);
            //        feeItem.FeeCatNo = feeCatNo;
            //        feeItem.FeeDesc = lblFeeName.Text;
            //    }
            //    else
            //    {
            //        /// this record can be a new standard fee record or new fee item in predefined std. fees.
            //        feeItem.RecieptCode = _newFeeItem.RecieptCode;
            //        feeItem.DegreeNo = _newFeeItem.DegreeNo;
            //        feeItem.RoomCapacity = _newFeeItem.RoomCapacity;
            //        feeItem.FeeDesc = _newFeeItem.FeeDesc;
            //        feeItem.FeeCatNo = _newFeeItem.FeeCatNo;
            //    }
            //}
            ////feeItem.CollegeId = Convert.ToInt32(ddlSchClg.SelectedValue);

            //// Initialize objSF object to contain basic unique identifying data.
            //if (feeItem.FeeHead == "F1") 
            //    _newFeeItem = feeItem;

            //StandardFeeController feeController = new StandardFeeController();
            //this.AddUpdateStandardFees(ref feeItem, ObjHs);
            //}

            //END
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeItem.FeeCatNo;             
    }

    #endregion

    #region Action
    private void DisplayFeeName()
    {
        //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
        //{
            lblFeeName.Text = ddlHostelSessionNo.SelectedItem.Text + " ";
            lblFeeName.Text += ddlHostel.SelectedItem.Text + " ";
            lblFeeName.Text += ddlRoomType.SelectedItem.Text + " ";
            lblFeeName.Text += ddlReceiptType.SelectedItem.Text + " ";
        //}
        // START   // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  
        //else
        //{
        //    lblFeeName.Text = ddlDegree.SelectedItem.Text + " ";
        //    lblFeeName.Text += ddlReceiptType.SelectedItem.Text + " ";
        //    lblFeeName.Text += ddlRoomCapacity.SelectedValue;
        //}
        //END
    }

    private string GetDropDownListValue(DropDownList ddl)
    {
        return (ddl.SelectedIndex > 0) ? ddl.SelectedValue : null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearControls()
    {
        if (ddlRoomCapacity.Items.Count > 0)
            ddlRoomCapacity.SelectedIndex = 0;

        if (ddlDegree.Items.Count > 0)
            ddlDegree.SelectedIndex = 0;

        if (ddlReceiptType.Items.Count > 0)
            ddlReceiptType.SelectedIndex = 0;

        if (ddlHostelSessionNo.Items.Count > 0)
            ddlHostelSessionNo.SelectedIndex = 0;

        if (ddlHostel.Items.Count > 0)
            ddlHostel.SelectedIndex = 0;

        if (ddlHosteltype.Items.Count > 0)
            ddlHosteltype.SelectedIndex = 0;

        if (ddlRoomType.Items.Count > 0)
            ddlRoomType.SelectedIndex = 0;

        lblFeeName.Text = string.Empty;

        /// while refreshing page we need all fee items in listbox hence send no values 
        /// in parameters to filter records.
        this.FillListbox(string.Empty, 0, 0);

        this.lv.Visible = false;
        this.btnSubmit.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
        //{
            ShowReport("Hostel Standard Fees Information Report", "CrescentHostelStandardFeeReport.rpt");
        //}
        //else
        //{
        //    ShowReport("Hostel Standard Fees Information Report", "HostelStandardFeeReport.rpt");
        //}
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
            //{
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue + ",@P_ROOM_TYPE=" + Convert.ToInt32(ddlRoomType.SelectedValue) + ",@P_SESSION_NO=" + ddlHostelSessionNo.SelectedValue + ",@P_HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            //}
                // START   // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  
            //else
            //{
            //    url += "&param=@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SESSION_NO=" + ddlHostelSessionNo.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTELTYPE=" + ddlHosteltype.SelectedValue + ",@P_HOSTEL_NO=" + ddlHostel.SelectedValue + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            //}
            //END

            //@P_HOSTEL_NO="+ddlHostelName.SelectedValue +",
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objCommon.ShowError(Page, "HOSTEL_REPORT_HostelStandardFee.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                _objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public int AddUpdateStandardFees(ref StandardFee feeItem, HostelStdFee ObjHs)
    {
        
        int status = Convert.ToInt32(CustomStatus.Others);
        try
        {
            int branchno = 0;
            DataSet ds = null;
            SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
                    new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
                    new SqlParameter("@P_SRNO", feeItem.SerialNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
                    new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
                    //new SqlParameter("@P_BATCHNO", feeItem.BatchNo),
                   // new SqlParameter("@P_COLLEGE_ID",feeItem.CollegeId),
                    new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
                    new SqlParameter("@P_SEMESTER2", feeItem.Sem2_Fee),
                    new SqlParameter("@P_SEMESTER3", feeItem.Sem3_Fee),
                    new SqlParameter("@P_SEMESTER4", feeItem.Sem4_Fee),
                    new SqlParameter("@P_SEMESTER5", feeItem.Sem5_Fee),
                    new SqlParameter("@P_SEMESTER6", feeItem.Sem6_Fee),
                    new SqlParameter("@P_SEMESTER7", feeItem.Sem7_Fee),
                    new SqlParameter("@P_SEMESTER8", feeItem.Sem8_Fee),
                    new SqlParameter("@P_SEMESTER9", feeItem.Sem9_Fee),
                    new SqlParameter("@P_SEMESTER10", feeItem.Sem10_Fee),
                    new SqlParameter("@P_SEMESTER11", feeItem.Sem11_Fee),
                    new SqlParameter("@P_SEMESTER12", feeItem.Sem12_Fee),
                    new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
                    new SqlParameter("@P_SESSION_NO", ObjHs.Session_No),
                    new SqlParameter("@P_BRANCH_NO", branchno),
                    new SqlParameter("@P_HOSTEL_NO", ObjHs.Hostel_No),
                    new SqlParameter("@P_HOSTEL_NAME", ObjHs.Hostel_Name),
                     new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    //new SqlParameter("@P_CAPACITY", _newFeeItem.CAPACITY),
                    //new SqlParameter("@P_BATHTYPE", _newFeeItem.BATH),
                    new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
                };
            sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

            
            SQLHelper dataAccess = new SQLHelper(_connectionString);
            object obj = dataAccess.ExecuteNonQuerySP("PKG_HOSTEL_ADD_UPDATE_STD_FEES", sqlParams, true);

           if (obj != null && obj.ToString() != string.Empty)
               feeItem.FeeCatNo = Convert.ToInt32(obj);

            status = Convert.ToInt32(CustomStatus.RecordSaved);
        }
        catch (Exception ex)
        {
            status = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelStdFeesController.AddUpdateStandardFees --> " + ex.Message + " " + ex.StackTrace);
        }
        return status;
    }

    public int AddUpdateStandardFeesByRoomType(ref StandardFee feeItem, HostelStdFee ObjHs)
    {

        int status = Convert.ToInt32(CustomStatus.Others);
        try
        {
            int branchno = 0;
            DataSet ds = null;
            SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
                    new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
                    new SqlParameter("@P_SRNO", feeItem.SerialNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
                    new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
                    //new SqlParameter("@P_BATCHNO", feeItem.BatchNo),
                   // new SqlParameter("@P_COLLEGE_ID",feeItem.CollegeId),
                    new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
                    new SqlParameter("@P_SEMESTER2", feeItem.Sem2_Fee),
                    new SqlParameter("@P_SEMESTER3", feeItem.Sem3_Fee),
                    new SqlParameter("@P_SEMESTER4", feeItem.Sem4_Fee),
                    new SqlParameter("@P_SEMESTER5", feeItem.Sem5_Fee),
                    new SqlParameter("@P_SEMESTER6", feeItem.Sem6_Fee),
                    new SqlParameter("@P_SEMESTER7", feeItem.Sem7_Fee),
                    new SqlParameter("@P_SEMESTER8", feeItem.Sem8_Fee),
                    new SqlParameter("@P_SEMESTER9", feeItem.Sem9_Fee),
                    new SqlParameter("@P_SEMESTER10", feeItem.Sem10_Fee),
                    new SqlParameter("@P_SEMESTER11", feeItem.Sem11_Fee),
                    new SqlParameter("@P_SEMESTER12", feeItem.Sem12_Fee),
                    new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
                    new SqlParameter("@P_SESSION_NO", ObjHs.Session_No),
                    new SqlParameter("@P_BRANCH_NO", branchno),
                    //new SqlParameter("@P_HOSTEL_NO", _newFeeItem.Hostel_No),
                    new SqlParameter("@P_HOSTEL_NAME", ObjHs.Hostel_Name),
                    new SqlParameter("@P_ROOM_TYPE", ObjHs.RoomType),
                     new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    //new SqlParameter("@P_CAPACITY", _newFeeItem.CAPACITY),
                    //new SqlParameter("@P_BATHTYPE", _newFeeItem.BATH),
                    new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
                };
            sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;


            SQLHelper dataAccess = new SQLHelper(_connectionString);
            object obj = dataAccess.ExecuteNonQuerySP("PKG_HOSTEL_ADD_UPDATE_STD_FEES_BY_ROOMTYPE", sqlParams, true);

            if (obj != null && obj.ToString() != string.Empty)
                feeItem.FeeCatNo = Convert.ToInt32(obj);

            status = Convert.ToInt32(CustomStatus.RecordSaved);
        }
        catch (Exception ex)
        {
            status = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelStdFeesController.AddUpdateStandardFees --> " + ex.Message + " " + ex.StackTrace);
        }
        return status;
    }    
    #endregion
    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._objCommon.FillDropDownList(ddlRoomType, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "TYPE_NO");
    }

    public DataSet HostelStandardFeeRoomType(string recieptCode, int roomtype, int Session, int Hostel)
    {
        DataSet ds = null;
        try
        {
            SQLHelper dataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_ROOM_TYPE", roomtype),
                    //new SqlParameter("@P_ROOM_CAPACITY", roomCapacity),
                    new SqlParameter("@P_SESSION_NO",Session),
                    new SqlParameter("@P_HOSTEL_NO",Hostel),
                    new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                    
                };

            ds = dataAccess.ExecuteDataSetSP("PKG_HOSTEL_GET_STANDARD_FEE_CRESCENT", sqlParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
        }
        return ds;
    }
    protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
       // if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)   //commented for ticket no : 47653
     //   {
            btnShow_Click(sender, e);
       // }
    }
}