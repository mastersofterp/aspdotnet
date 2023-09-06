//=====================================
//Created By : Gopal Anthati
//Created Date : 17/02/2021
//=====================================

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

public partial class VEHICLE_MAINTENANCE_Transaction_VehReqTransportIncharge : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Config objConfig = new Config();
    VMController ObjCon = new VMController();
    VM ObjEnt = new VM();

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

                }
                ViewState["Action"] = "Add";
                BindListview();
                objCommon.FillDropDownList(ddlContractor,"VEHICLE_SUPPILER_MASTER", "SUPPILER_ID", "SUPPILER_NAME", "IS_ACTIVE=1", "SUPPILER_NAME");
                objCommon.FillDropDownList(ddlRInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");
              //  divApprReqList.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRequisition.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListview()
    {
        //DataSet ds = objCommon.FillDropDown("VEHICLE_REQUISITION A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) INNER JOIN VEHICLE_REQUISITION_PASS_ENTRY C ON (A.VEH_REQ_ID=C.VEH_REQ_ID)", "A.VEH_REQ_ID,DATE_OF_JOURNEY", "(CASE WHEN ONE_WAY=1 THEN 'One-Way' ELSE 'Two-Way' END) AS ONE_WAY,COLLEGE_NAME", "SRNO=2 AND [STATUS]='A' AND A.VEH_REQ_ID NOT IN (SELECT DISTINCT VEH_REQ_ID FROM VEHICLE_ALLOT_TRANSPORT_INCHRG)", "VEH_REQ_ID DESC");
        DataSet ds = objCommon.FillDropDown("VEHICLE_REQUISITION A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) INNER JOIN VEHICLE_REQUISITION_PASS_ENTRY C ON (A.VEH_REQ_ID=C.VEH_REQ_ID)", "A.VEH_REQ_ID,DATE_OF_JOURNEY", "(CASE WHEN ONE_WAY=1 THEN 'One-Way' ELSE 'Two-Way' END) AS ONE_WAY,COLLEGE_NAME", "SRNO=2 AND [STATUS]='A' AND A.VEH_REQ_ID NOT IN (SELECT DISTINCT VEH_REQ_ID FROM VEHICLE_ALLOT_TRANSPORT_INCHRG)", "VEH_REQ_ID DESC");

        lvVehicleReq.DataSource = ds;
        lvVehicleReq.DataBind();
        lvVehicleReq.Visible = true;
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        int VehReqId = Convert.ToInt32(btn.CommandArgument);
        ViewState["VEH_REQ_ID"] = VehReqId;
        divVehArrange.Visible = true;
        divApprReqList.Visible = false;
        BindVehicleList(VehReqId);
    }

    private void BindVehicleList(int VehReqId)
    {
        DataSet ds = objCommon.FillDropDown("VEHICLE_REQUISITION A INNER JOIN VEHICLE_REQUISITION_VEHICLE B ON (A.VEH_REQ_ID = B.VEH_REQ_ID) INNER JOIN VEHICLE_HIRE_MASTER C ON (B.VEHICLE_ID = C.VEHICLE_ID)", "B.VEHICLE_AC_NONAC", "REGNO+' ' +':'+' ' + C.VEHICLE_NAME  AS VEHICLE_NAME", "A.VEH_REQ_ID =" + VehReqId, "");
        lvVehicle.DataSource = ds;
        lvVehicle.DataBind();
        lvVehicle.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divVehArrange.Visible = false;
        divApprReqList.Visible = true;
        Clear();
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ObjEnt.SUPPILER_ID = Convert.ToInt32(ddlContractor.SelectedValue);
        ObjEnt.VEHICLE_NO = txtVehicleNo.Text;
        ObjEnt.VEH_REQ_ID = Convert.ToInt32(ViewState["VEH_REQ_ID"]);
        ObjEnt.ARRIVAL_TIME = Convert.ToDateTime(txtArrivalTime.Text);
        ObjEnt.DEPARTURE_TIME = Convert.ToDateTime(txtDepartureTime.Text);
        ObjEnt.TOT_KM_TRAVEL = Convert.ToInt32(txtTotKm.Text);
        ObjEnt.TOT_HRS_TRAVEL = Convert.ToInt32(txtTotHrs.Text);
        ObjEnt.AMOUNT_PAY = Convert.ToDouble(txtAmountPay.Text);
        ObjEnt.USERNO = Convert.ToInt32(Session["userno"]);

        if (ViewState["Action"].ToString() == "Add")
        {
            ObjEnt.VATC_ID = 0;
            CustomStatus cs = (CustomStatus)ObjCon.AddUpdVehicleAllotmentByTC(ObjEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListview();
               // MessageBox("Record Saved Successfully");
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                Clear();
                return;
            }
        }
        else
        {
            ObjEnt.VATC_ID = Convert.ToInt32(ViewState["VATC_ID"]);
        }       
           
    }

    private void Clear()
    {
        ViewState["VEH_REQ_ID"] = null;
        ViewState["Action"] = "Add";
        ddlContractor.SelectedIndex = 0;
        txtVehicleNo.Text = string.Empty;
        txtArrivalTime.Text = string.Empty;
        txtDepartureTime.Text = string.Empty;
        txtTotHrs.Text = string.Empty;
        txtTotKm.Text = string.Empty;
        txtAmountPay.Text = string.Empty;
        divVehArrange.Visible = false;
        divApprReqList.Visible = true;
        
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        divApprReqList.Visible = false;
        divReport.Visible = true;
    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "rptVehicleReqTransportInc.rpt"); 
    }
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Date_Of_Journey = Convert.ToDateTime(txtDOJ.Text).ToString("yyyy-MM-dd");
            string Veh_Req_Id = objCommon.LookUp("VEHICLE_REQUISITION", "isnull(VEH_REQ_ID,0)", "COLLEGE_ID =" + ddlRInstitute.SelectedValue + " AND DATE_OF_JOURNEY='" + Date_Of_Journey + "'");
            if (Veh_Req_Id == "0" || Veh_Req_Id == "")
            {
                MessageBox("No Records Found");
                return;
            }
            string Count = objCommon.LookUp("VEHICLE_ALLOT_TRANSPORT_INCHRG", "COUNT(*)", "VEH_REQ_ID =" + Convert.ToInt32(Veh_Req_Id));

            if (Count == "0")
            {
                MessageBox("No Records Found");
                return;
            }

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=DrivingLicenceExpiry" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

            url += "&param=@P_VEH_REQ_ID=" +Convert.ToInt32(Veh_Req_Id);
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
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnRBack_Click(object sender, EventArgs e)
    {
        divReport.Visible = false;
        divApprReqList.Visible = true;
    }
}
