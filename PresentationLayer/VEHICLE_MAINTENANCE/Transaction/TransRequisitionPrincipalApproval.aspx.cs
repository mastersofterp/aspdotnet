//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 20-JUL-2021
// DESCRIPTION   : USE TO APPROVE TRANSPORT REQUISITION BY STUDENT 
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class VEHICLE_MAINTENANCE_Transaction_TransRequisitionPrincipalApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VehicleRequisitionController objVRC = new VehicleRequisitionController();
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
                   
                    BindlistView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to check the page authority.
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

    // This method is used to bind the list of vahicle allotment.
    private void BindlistView()
    {
        try
        {
            DataSet ds = null;
            ds = objVRC.GetPendingRequisitionList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRequisition.Visible = true;
                lvRequisition.DataSource = ds;
                lvRequisition.DataBind();
            }
            else
            {
                lvRequisition.DataSource = null;
                lvRequisition.DataBind();
                lvRequisition.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        LinkButton btnDetail = sender as LinkButton;
        int STUD_IDNO = int.Parse(btnDetail.CommandArgument);
        DataSet ds = objVRC.GetStudentDetails(STUD_IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlDetails.Visible = true;
            txtInstitution.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            txtstudname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDate.Text = ds.Tables[0].Rows[0]["APP_DATE"].ToString();
            txtAdmissionNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            txtDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtBatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
            txtbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtCategory.Text = ds.Tables[0].Rows[0]["CATEGORYNAME"].ToString();
            txtStopName.Text = ds.Tables[0].Rows[0]["STOPNAME"].ToString();
            txtPeriodfrom.Text = ds.Tables[0].Rows[0]["PERIOD_FROM"].ToString();
            txtPeriodTo.Text = ds.Tables[0].Rows[0]["PERIOD_TO"].ToString();
            txtsemester.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtSession.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
            txtSemYear.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString() + " " + ds.Tables[0].Rows[0]["lbl_SEM_YEAR"].ToString();
            txtFeesAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
        }

    }


    //This action button save the vehicle-driver-route allotment.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ApproveTbl = new DataTable("approvTbl");
            ApproveTbl.Columns.Add("STUD_IDNO", typeof(int));
            ApproveTbl.Columns.Add("VTRAID", typeof(int));
            ApproveTbl.Columns.Add("STATUS", typeof(string));
            ApproveTbl.Columns.Add("APPROVE_REMARK", typeof(string));
            ApproveTbl.Columns.Add("SESSIONNO", typeof(int));
            ApproveTbl.Columns.Add("DEGREENO", typeof(int));
            ApproveTbl.Columns.Add("BRANCHNO", typeof(int));
            ApproveTbl.Columns.Add("SEMESTERNO", typeof(int));
            ApproveTbl.Columns.Add("YEAR", typeof(int));
          //  ApproveTbl.Columns.Add("ACTUAL_AMOUNT", typeof(double));
           // ApproveTbl.Columns.Add("APPROVED_AMOUNT", typeof(double));

            int count = 0;
            DataRow dr = null;
            foreach (ListViewDataItem item in lvRequisition.Items)
            {
                string Status = string.Empty;
                CheckBox chkStatus = item.FindControl("chkStatus") as CheckBox;
                HiddenField hdnStudIdNo = item.FindControl("hdnStudIdNo") as HiddenField;
                HiddenField hdnVTRAID = item.FindControl("hdnVTRAID") as HiddenField;
                DropDownList ddlStatus = item.FindControl("ddlStatus") as DropDownList;
                TextBox txtRemarks = item.FindControl("txtRemarks") as TextBox;
                TextBox txtApprovedAmount = item.FindControl("txtFeesAmount") as TextBox;
                HiddenField hdnActualAmount = item.FindControl("hdnActualAmount") as HiddenField;

                HiddenField hdnSessionNo = item.FindControl("hdnSessionNo") as HiddenField;
                HiddenField hdnDegreeNo = item.FindControl("hdnDegreeNo") as HiddenField;
                HiddenField hdnBranchNo = item.FindControl("hdnBranchNo") as HiddenField;
                HiddenField hdnSemesterNo = item.FindControl("hdnSemesterNo") as HiddenField;
                HiddenField hdnYear = item.FindControl("hdnYear") as HiddenField;
                
                if (chkStatus.Checked == true)
                {
                    count = 1;
                    ViewState["action"] = "add";
                    dr = ApproveTbl.NewRow();
                    dr["STUD_IDNO"] = hdnStudIdNo.Value;
                    dr["VTRAID"] = hdnVTRAID.Value;
                    if (ddlStatus.SelectedValue != "0")
                    {
                        dr["STATUS"] = ddlStatus.SelectedValue == "1" ? "A" : "R";
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updApprove, "Please Select Status As Accept/ Reject.", this.Page);
                        return;
                    }
                    dr["APPROVE_REMARK"] = txtRemarks.Text == string.Empty ? "" : txtRemarks.Text;
                    dr["SESSIONNO"] = hdnSessionNo.Value;
                    dr["DEGREENO"] = hdnDegreeNo.Value;
                    dr["BRANCHNO"] = hdnBranchNo.Value;
                    dr["SEMESTERNO"] = hdnSemesterNo.Value;
                    dr["YEAR"] = hdnYear.Value;
                    if (txtApprovedAmount.Text != "" && txtApprovedAmount.Text != "0")
                    {
                        if (Convert.ToDouble(txtApprovedAmount.Text) > Convert.ToDouble(hdnActualAmount.Value))
                        {
                          txtApprovedAmount.Text = hdnActualAmount.Value;
                          objCommon.DisplayMessage(this.updApprove, "Approved Amount Should Not Be Greater Than Fees Amount.", this.Page);
                          return;
                          }
                          else
                          {
                           // dr["ACTUAL_AMOUNT"] = hdnActualAmount.Value;
                           // dr["APPROVED_AMOUNT"] = txtApprovedAmount.Text;
                       
                           }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updApprove, "Fees Amount Is Required.", this.Page);
                        return;
                    }

                    ApproveTbl.Rows.Add(dr);
                }
                    
              
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(this.updApprove, "Please Select At Least One Student.", this.Page);
                return;
            }

            objVM.APPROVAL_TRAN = ApproveTbl;
            if (ViewState["action"] != null)
            {
                CustomStatus cs = (CustomStatus)objVRC.UpdateApprovalDetails(objVM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindlistView();
                    pnlDetails.Visible = false;
                    objCommon.DisplayMessage(this.updApprove, "Record Saved Successfully.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This action button is use to clear the last selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //This action button is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int RANO = int.Parse(btnEdit.CommandArgument);
            ViewState["RAID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(RANO);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This method is used to show the details of the selected record.
    private void ShowDetails(int RANO)
    {
        try
        {
            //   lvDeparture.Visible = false;
            DataSet ds = objCommon.FillDropDown("VEHICLE_ROUTEALLOTMENT", "FDATE,TDAT,DRIVER_ID", "VIDNO,ROUTEID ,DTIME, ROUTENAME", "RAID=" + RANO, "RAID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string RouteNo = ds.Tables[0].Rows[0]["ROUTEID"].ToString();
                if (RouteNo == "0")
                {
                    //trRoute.Visible = true;
                    //trRouteDrop.Visible = false;
                    // trDeparture.Visible = false;
                    //btnAdd.Visible = false;
                    ////MEVDate.Visible = false;
                    ////MaskedEditValidator2.Visible = false;
                    //ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                    ////txtRoute.Text =  ds.Tables[0].Rows[0]["ROUTENAME"].ToString();
                    //ddlDriver.SelectedValue = ds.Tables[0].Rows[0]["DRIVER_ID"].ToString();
                    //txtFrmDt.Text = ds.Tables[0].Rows[0]["FDATE"].ToString();
                    //txtToDt.Text = ds.Tables[0].Rows[0]["TDAT"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear all the controls.
    private void Clear()
    {
        BindlistView();
        pnlDetails.Visible = false;
    }
}