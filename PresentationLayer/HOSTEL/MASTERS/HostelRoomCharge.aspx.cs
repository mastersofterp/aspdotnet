//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : Hostel Room Charge                                                        
// CREATION DATE : 17-Jan-2023                                                         
// CREATED BY    : SONALI BHOR
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using HostelBusinessLogicLayer.BusinessLogic.Hostel;
using HostelBusinessLogicLayer.BusinessEntities.Hostel;

public partial class HOSTEL_MASTERS_HostelRoomCharge : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelRoomCharge objroomcharge = new HostelRoomCharge();
    GuestInfoController objchargecon = new GuestInfoController();


    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
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
                // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Session["colcode"] = "1";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            // hostelname
            PopulateDropDownList();
            this.BindListView();
            ViewState["action"] = "add";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BlockInfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BlockInfo.aspx");
        }
    }
    #endregion

    #region private methods

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NO");
            //objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0", "HOSTEL_SESSION_NO"); // commented for BUG-id 163739 

            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO");

            objCommon.FillDropDownList(ddlResidentType, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "RESIDENT_TYPE_NO > 0", "RESIDENT_TYPE_NO");
            objCommon.FillDropDownList(ddlRoomType, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "TYPE_NO > 0", "TYPE_NO");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

      private bool CheckDuplicateEntry()
        {
            bool flag = false;
            try
            {
                string chargeno = objCommon.LookUp("ACD_HOSTEL_ROOM_CHARGE", "CHARGE_NO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND ROOM_TYPE=" + Convert.ToInt32(ddlRoomType.SelectedValue) + " AND RESIDENT_TYPE="+ Convert.ToInt32(ddlResidentType.SelectedValue) +"");
                if (chargeno != null && chargeno != string.Empty)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
            return flag;
        }

    private bool CheckDuplicateEntryUpdate(int chgno)
    {
        bool flag = false;
        try
        {
            string chargeno = objCommon.LookUp("ACD_HOSTEL_ROOM_CHARGE", "CHARGE_NO", "CHARGE_NO !="+chgno+"  AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO=" +Convert.ToInt32(ddlHostel.SelectedValue) + " AND ROOM_TYPE=" + Convert.ToInt32(ddlRoomType.SelectedValue) + " AND RESIDENT_TYPE="+ Convert.ToInt32(ddlResidentType.SelectedValue) +"");

            if (chargeno != null && chargeno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BlockInfo.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    #endregion
    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHostel.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlRoomType, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "TYPE_NO > 0 AND HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+"", "TYPE_NO");
            ddlRoomType.Focus();
        }
        else
        {
            objCommon.DisplayMessage(this.UpdCharge, "Please Select Hostel Name.", this.Page);
        }
          
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
           objroomcharge.CHARGE_NO=0;
           objroomcharge.SESSIONNO= Convert.ToInt32(ddlSession.SelectedValue);
           objroomcharge.HOSTEL_NO = Convert.ToInt32(ddlHostel.SelectedValue);
           objroomcharge.ROOM_TYPE = Convert.ToInt32(ddlRoomType.SelectedValue);
           objroomcharge.RESIDENT_TYPE = Convert.ToInt32(ddlResidentType.SelectedValue);
           objroomcharge.CHARGES = Convert.ToDecimal(txtCharge.Text.Trim());
           objroomcharge.COLLEGE_CODE = Convert.ToInt32(Session["colcode"].ToString());
           objroomcharge.ORGANIZATIONID = Convert.ToInt32(Session["OrgId"].ToString());
           objroomcharge.USERNO = Convert.ToInt32(Session["userno"].ToString());
           objroomcharge.IPADDRESS =  Session["ipAddress"].ToString();

         if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

             if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage(this.UpdCharge,"Entry for this Selection Already Done!", this.Page);
                        return;
                    }

                    cs = (CustomStatus)objchargecon.AddUpdateRoomCharge(objroomcharge);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this.UpdCharge,"Record Saved Successfully!!!.", this.Page);
                        Clear();
                    }
                }

                /// Update Asset
             if (ViewState["action"].ToString().Equals("edit"))
             {
                 objroomcharge.CHARGE_NO = (GetViewStateItem("CHARGE_NO") != string.Empty ? int.Parse(GetViewStateItem("CHARGE_NO")) : 0);
                 if (CheckDuplicateEntryUpdate(objroomcharge.CHARGE_NO) == true)
                 {
                     objCommon.DisplayMessage(this.UpdCharge, "Entry for this Selection Already Done!", this.Page);
                     return;
                 }
                 cs = (CustomStatus)objchargecon.AddUpdateRoomCharge(objroomcharge);
                 if (cs.Equals(CustomStatus.RecordSaved))
                 {
                     objCommon.DisplayMessage(this.UpdCharge, "Record Updated Successfully!!!.", this.Page);
                     Clear();
                 }
             }

             if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                 this.objCommon.DisplayMessage(this.UpdCharge, "Unable to complete the operation.", this.Page);
             else
                 this.BindListView();
             
           }
        }
       catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "BlockInformation.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Clear();
    }


    private void BindListView()
    {
        try
        {
            int chargeno = 0;
            DataSet ds = objchargecon.GetChargeData(chargeno);
            lvCharge.DataSource = ds;
            lvCharge.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Masters_HostelSession.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlHostel.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlRoomType.SelectedIndex = 0;
        ddlResidentType.SelectedIndex = 0;
        txtCharge.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton editbutton = sender as ImageButton;
            int chargeno = Convert.ToInt32(editbutton.CommandArgument);

            DataSet ds = objchargecon.GetChargeData(chargeno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                ddlSession.SelectedValue = dr["SESSIONNO"] == null ? string.Empty : dr["SESSIONNO"].ToString();
                ddlHostel.SelectedValue = dr["HOSTEL_NO"] == null ? string.Empty : dr["HOSTEL_NO"].ToString();
                ddlHostel_SelectedIndexChanged(sender,e);
                ddlRoomType.SelectedValue = dr["ROOM_TYPE"] == null ? string.Empty : dr["ROOM_TYPE"].ToString();
                ddlResidentType.SelectedValue = dr["RESIDENT_TYPE"] == null ? string.Empty : dr["RESIDENT_TYPE"].ToString();
                txtCharge.Text = dr["CHARGES"] == null ? string.Empty : dr["CHARGES"].ToString();

                ViewState["action"] = "edit";
                ViewState["CHARGE_NO"] = dr["CHARGE_NO"].ToString();
                

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnEdit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}