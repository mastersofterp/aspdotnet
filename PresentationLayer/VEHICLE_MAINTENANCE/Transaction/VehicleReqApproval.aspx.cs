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

public partial class VEHICLE_MAINTENANCE_Transaction_VehicleReqApproval : System.Web.UI.Page
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
                
                FillDropDown();
                BindListview();
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

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");        
    }

    private void BindListview()
    {
        DataSet ds = objCommon.FillDropDown("VEHICLE_REQUISITION A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) INNER JOIN VEHICLE_REQUISITION_PASS_ENTRY C ON (A.VEH_REQ_ID=C.VEH_REQ_ID)", "A.VEH_REQ_ID,DATE_OF_JOURNEY", "(CASE WHEN ONE_WAY=1 THEN 'One-Way' ELSE 'Two-Way' END) AS ONE_WAY,COLLEGE_NAME", "AUTH_UANO="+Session["userno"]+" AND STATUS='P'", "VEH_REQ_ID DESC");
        lvVehicleReq.DataSource = ds;
        lvVehicleReq.DataBind();
        lvVehicleReq.Visible = true;
        divReqList.Visible = true;
    }

    private void ShowReqDetails(int VehReqId)
    {
        DataSet ds = ObjCon.GetAllReqDetailsForEdit(VehReqId);
        ddlInstitute.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        txtDateOfJourney.Text = ds.Tables[0].Rows[0]["DATE_OF_JOURNEY"].ToString();
        rdlOneWay.SelectedValue = ds.Tables[0].Rows[0]["ONE_WAY"].ToString();

        lvGuestStaff.DataSource = ds.Tables[1];
        lvGuestStaff.DataBind();
        Session["dtGuestStaff"] = ds.Tables[1];
        lvGuestStaff.Visible = true;

        lvVehicle.DataSource = ds.Tables[2];
        lvVehicle.DataBind();
        Session["dtVehicle"] = ds.Tables[2];
        lvVehicle.Visible = true;
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button IMG = sender as Button;
        int VehReqId = Convert.ToInt32(IMG.CommandArgument);
        ViewState["VEHREQID"] = VehReqId;
        ShowReqDetails(VehReqId);
        divControls.Visible = true;
        lvGuestStaff.Visible = true;
        lvVehicle.Visible = true;
        divReqList.Visible = false;
        divApprove.Visible = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();        
    }

    private void Clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedValue = "A";
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus cs = (CustomStatus)ObjCon.ApproveRejectVehReq(Convert.ToInt32(ViewState["VEHREQID"]), Convert.ToChar(ddlSelect.SelectedValue), Convert.ToInt32(Session["userno"]),txtRemarks.Text);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            BindListview();
            MessageBox("Record Saved Successfully");
            Clear();
            //btnBack_Click(sender, e);
        }

        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divControls.Visible = false;
        lvGuestStaff.Visible = false;
        lvVehicle.Visible = false;
        divReqList.Visible = true;
        divApprove.Visible = false;
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedValue = "A";
        divSearch.Visible = true;
    }
   
}