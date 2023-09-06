using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;

public partial class HOSTEL_Hostel_Module_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelConfigurationController objhostelconfig = new HostelConfigurationController();

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
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                   
                }
                this.BindData();
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Hostel_Module_Configuration.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Hostel_Module_Configuration.aspx");
        }
    }
    #endregion


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int UANO = 0;
            int HostelConfigid = 0;
            UANO = Convert.ToInt32(Session["userno"].ToString());
            string IP_ADDRESS = string.Empty;
            string MAC_ID = string.Empty;
            IP_ADDRESS = Session["ipAddress"].ToString();
            MAC_ID = Session["macAddress"].ToString();

            int AdminAllowRoomAllot = 0;
            int StudAllowRoomAllot = 0;
            int FeeCollectionType = 0;
            int PaymentGateway = 0;
            int HostelWiseAtten = 0;
            int BlockWiseAtten = 0;
            int hostelDayWiseBooking = 0;
            int hostelDisciplinaryAction = 0;

            int CreateDemandOnRoomAllotment = 0;
            int AllowDirectPayFromOnlinePaymentForm = 0;

            if (chkAdminAllowRoomAllot.Checked == true)
            {
                AdminAllowRoomAllot = 1;
            }
            if (chkStudAllowRoomAllot.Checked == true)
            {
                StudAllowRoomAllot = 1;
            }
            if (chkPaymentGateway.Checked == true)
            {
                PaymentGateway = 1;
            }

            if (ddlFeeCollectionType.SelectedValue != "0")
            {
                FeeCollectionType = Convert.ToInt32(ddlFeeCollectionType.SelectedValue);
            }
            if (chkAtteHoswise.Checked == true)
            {
                HostelWiseAtten = 1;
            }
            if (chkAttenBlockWise.Checked)
            {
                BlockWiseAtten = 1;
            }

            if (chkHosBookDayWise.Checked)
            {
                hostelDayWiseBooking = 1;
            }

            if (chkHosDisciplinary.Checked)
            {
                hostelDisciplinaryAction = 1;
            }

            if (chkCreateDemandOnRoomAllotment.Checked)
            {
                CreateDemandOnRoomAllotment = 1;
            }

            if (chkAllowDirectPayFromOnlinePaymentForm.Checked)
            {
                AllowDirectPayFromOnlinePaymentForm = 1;
            }

            //if (hdfAdminAllowRoomAllot.Value == "true")
            //{
            //    AdminAllowRoomAllot = 1;
            //}
            //if (hdfStudAllowRoomAllot.Value == "true")
            //{
            //    StudAllowRoomAllot = 1;
              
            //}
            //if (hdfPaymentGateway.Value == "true")
            //{
            //    PaymentGateway = 1;

            //}




            CustomStatus cs = (CustomStatus)objhostelconfig.Add_HostelConfiguration(HostelConfigid, AdminAllowRoomAllot, StudAllowRoomAllot, FeeCollectionType, PaymentGateway, UANO, IP_ADDRESS, MAC_ID, HostelWiseAtten, BlockWiseAtten, hostelDayWiseBooking, hostelDisciplinaryAction, CreateDemandOnRoomAllotment, AllowDirectPayFromOnlinePaymentForm);
            if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
            {
                objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                this.BindData();
              //  this.Clear();
            }
            else
            {
                objCommon.DisplayMessage("Something went wrong.Please try again.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Clear();
    }

    public void Clear()
    {
        hdfAdminAllowRoomAllot.Value = "false";
        hdfFeeCollectionType.Value = "false";
        hdfPaymentGateway.Value = "false";
        hdfStudAllowRoomAllot.Value = "false";
        // ddlFeeCollectionType.SelectedValue = "0"; // BUG-ID 163745
        //chkPaymentGateway.Checked = false;  // BUG-ID 163745 
        //chkStudAllowRoomAllot.Checked = false;
        //chkAdminAllowRoomAllot.Checked = false;
    }


    private void BindData()
    {
        try
        {
            DataSet ds = objhostelconfig.GetModuleConfigData();
             if (ds.Tables != null)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {

                     if (ds.Tables[0].Rows[0]["Allow_Admin_Room_Allocation"].ToString() != null && (ds.Tables[0].Rows[0]["Allow_Admin_Room_Allocation"].ToString() == "1" || ds.Tables[0].Rows[0]["Allow_Admin_Room_Allocation"].ToString() == "True"))
                     {
                         chkAdminAllowRoomAllot.Checked = true;
                     }
                     else
                     {
                         chkAdminAllowRoomAllot.Checked = false;
                     }
                     if (ds.Tables[0].Rows[0]["allow_Stud_Room_Allocation"].ToString() != null && (ds.Tables[0].Rows[0]["allow_Stud_Room_Allocation"].ToString() == "1" || ds.Tables[0].Rows[0]["allow_Stud_Room_Allocation"].ToString() == "True"))
                     {
                         chkStudAllowRoomAllot.Checked = true;
                     }
                     else
                     {
                         chkStudAllowRoomAllot.Checked = false;
                     }
                     if (ds.Tables[0].Rows[0]["Fees_Collection_Through"].ToString() != null && ds.Tables[0].Rows[0]["Fees_Collection_Through"].ToString() != "0")
                     {
                         ddlFeeCollectionType.SelectedValue = ds.Tables[0].Rows[0]["Fees_Collection_Through"].ToString();
                     }
                     else
                     {
                         ddlFeeCollectionType.SelectedValue = "0";
                     }
                     if (ds.Tables[0].Rows[0]["Payment_Gateway"].ToString() != null && (ds.Tables[0].Rows[0]["Payment_Gateway"].ToString() == "1" || ds.Tables[0].Rows[0]["Payment_Gateway"].ToString() == "True"))
                     {
                         chkPaymentGateway.Checked = true;
                     }
                     else
                     {
                         chkPaymentGateway.Checked = false;
                     }

                     // ADDED ON 27/12/2022 BY SONALI FOR CRESCENT REQUIREMENT
                     if (ds.Tables[0].Rows[0]["HostelWise_Attendence"].ToString() != null && (ds.Tables[0].Rows[0]["HostelWise_Attendence"].ToString() == "1" || ds.Tables[0].Rows[0]["HostelWise_Attendence"].ToString() == "True"))
                     {
                         chkAtteHoswise.Checked = true;
                   
                     }
                     else
                     {
                         chkAtteHoswise.Checked = false;
                     }

                     if (ds.Tables[0].Rows[0]["BlockWise_Attendence"].ToString() != null && (ds.Tables[0].Rows[0]["BlockWise_Attendence"].ToString() == "1" || ds.Tables[0].Rows[0]["BlockWise_Attendence"].ToString() == "True"))
                     {
                         chkAttenBlockWise.Checked = true;
                         
                     }
                     else
                     {
                         chkAttenBlockWise.Checked = false;
                     }
                     // ADDED ON 19/01/2023 BY SONALI FOR CPUKOTA||CPUH REQUIREMENT
                     if (ds.Tables[0].Rows[0]["HostelDayWise_Booking"].ToString() != null && (ds.Tables[0].Rows[0]["HostelDayWise_Booking"].ToString() == "1" || ds.Tables[0].Rows[0]["HostelDayWise_Booking"].ToString() == "True"))
                     {
                       
                         chkHosBookDayWise.Checked = true;
                     }
                     else
                     {                         
                         chkHosBookDayWise.Checked = false;
                     }
                     
                     // ADDED ON 21/02/2023 BY SONALI FOR CRESCENT REQUIREMENT
                     if (ds.Tables[0].Rows[0]["Allow_HostelDisciplinaryAction"].ToString() != null && (ds.Tables[0].Rows[0]["Allow_HostelDisciplinaryAction"].ToString() == "1" || ds.Tables[0].Rows[0]["Allow_HostelDisciplinaryAction"].ToString() == "True"))
                     {

                         chkHosDisciplinary.Checked = true;
                     }
                     else
                     {
                         chkHosDisciplinary.Checked = false;
                     }

                     // Added by Saurabh L on 5th June 2023 For DAIICT Allow Create Demand on Room Allotment or NOT flag
                     if (ds.Tables[0].Rows[0]["Allow_Create_Demand_On_RoomAllotment"].ToString() != null && (ds.Tables[0].Rows[0]["Allow_Create_Demand_On_RoomAllotment"].ToString() == "1"))
                     {

                         chkCreateDemandOnRoomAllotment.Checked = true;
                     }
                     else
                     {
                         chkCreateDemandOnRoomAllotment.Checked = false;
                     }

                     // Added by Saurabh L on 5th June 2023 for restrict student from direct pay from 'OnlinePayment' form 
                     if (ds.Tables[0].Rows[0]["Allow_Stud_OnlinePay_Direct_From_OnlinePayment"].ToString() != null && (ds.Tables[0].Rows[0]["Allow_Stud_OnlinePay_Direct_From_OnlinePayment"].ToString() == "1"))
                     {

                         chkAllowDirectPayFromOnlinePaymentForm.Checked = true;
                     }
                     else
                     {
                         chkAllowDirectPayFromOnlinePaymentForm.Checked = false;
                     }

                 }
             }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "RFC_CONFIG_Masters_AffilationType.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}