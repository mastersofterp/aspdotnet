//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : ROOM MASTER                                                          
// CREATION DATE : 2-DEC-2010                                                          
// CREATED BY    : GAURAV SONI                                    
// MODIFIED DATE :  16-july-2015                                                                    
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Hostel_Masters_Room : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    RoomController roomController = new RoomController();

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
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Fill Dropdown lists
                   // this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "BL_NO > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "BLOCK_NAME"); //OrganizationId filter added by Saurabh L on 24/05/2022
                    //changes on parameters from HBNO to HOSTEL_NO
                    this.objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "HOSTEL_NO"); //OrganizationId filter added by Saurabh L on 24/05/2022

                   //Added new ddl as per Requeriment on 22-07-2022 added by shubham
                    this.objCommon.FillDropDownList(ResidentTypeNo, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "RESIDENT_TYPE_NO > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "RESIDENT_TYPE_NO");
                    //this.objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HBNO", "HOSTEL_NAME", "HBNO > 0 ", "HOSTEL_NAME");
                    
                    //this.objCommon.FillDropDownList(ddlResidentType, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "RESIDENT_TYPE_NO > 0", string.Empty);
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                    //this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                }
                this.ShowAllRooms();
                //ShowAllRooms();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }
    #endregion

  

    protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFloor.SelectedIndex > 0)
            {
                txtRoomName.Focus();
            }
            else
                ddlFloor.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.ddlFloor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

   
   
    #region Actions
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Room room = this.BindDataFromControls();
            
            /// check form action whether add or update
            if (ViewState["action"] != null)        
            {
                
                CustomStatus cs = new CustomStatus();

                /// Add Room
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                        return;
                    }
                    cs = (CustomStatus)roomController.AddRoom(room);
                    objCommon.DisplayMessage("Record Saved successfully!!", this.Page);
                    ClearControlContents(); //called by shubham barke on 22/03/22
                }

                /// Update Room
                if (ViewState["action"].ToString().Equals("edit"))
                {
                   
                    room.RoomNo = (GetViewStateItem("RoomNo") != string.Empty ? int.Parse(GetViewStateItem("RoomNo")) : 0);
                    if (CheckDuplicateEntryUpdate(room.RoomNo) == true)
                    {
                        objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);//Added by Saurabh L on 24/05/2022  CheckDuplicateEntryUpdate() function
                        return;
                    }
                    cs = (CustomStatus)roomController.UpdateRoom(room);
                    objCommon.DisplayMessage("Record Updated successfully!", this.Page);
                    ViewState["action"] = "add";
                    ClearControlContents(); //Added by shubham barke on 22/03/22
                }

                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");
                else
                    this.ShowAllRooms();
            }
            this.ClearControlContents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.btnAllotRoom_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            //string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "BLOCK_NO=" + ddlBlock.SelectedValue + " AND FLOOR_NO=" + ddlFloor.SelectedValue + " AND ROOM_NAME='" + txtRoomName.Text + "'");
            //string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "BLOCK_NO=" + ddlBlock.SelectedValue + "AND ROOM_NAME='" + txtRoomName.Text + "'");
<<<<<<< HEAD
            string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "BLOCK_NO=" + ddlBlock.SelectedValue + "AND ROOM_NAME='" + txtRoomName.Text + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Saurabh L on 24/05/2022
=======
            //string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "BLOCK_NO=" + ddlBlock.SelectedValue + "AND ROOM_NAME='" + txtRoomName.Text + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Saurabh L on 24/05/2022
            //string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "BLOCK_NO=" + ddlBlock.SelectedValue + " AND FLOOR_NO=" + ddlFloor.SelectedValue + "AND ROOM_NAME='" + txtRoomName.Text + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---Floor Cond  added by Himanshu Tamrakar on 28/02/2024
            string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "HBNO="+Convert.ToInt32(ddlHostel.SelectedValue)+" AND BLOCK_NO=" + ddlBlock.SelectedValue + " AND FLOOR_NO=" + ddlFloor.SelectedValue + "AND ROOM_NAME='" + txtRoomName.Text + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // --- Cond  added by Himanshu Tamrakar on 16/03/2024
>>>>>>> 256fde10 ([BUGFIX][56172] HOSTEL ROOM)
            if (roomno != null && roomno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Room.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int roomid)
    {
        bool flag = false;
        try
        {
            //string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "ROOM_NO !="+ roomid + " AND BLOCK_NO=" + ddlBlock.SelectedValue + " AND ROOM_NAME ='" + txtRoomName.Text + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---OrganizationId filter added by Saurabh L on 24/05/2022
            string roomno = objCommon.LookUp("ACD_HOSTEL_ROOM", "ROOM_NO", "ROOM_NO !="+ roomid +" AND HBNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND BLOCK_NO=" + ddlBlock.SelectedValue + " AND FLOOR_NO=" + ddlFloor.SelectedValue + "AND ROOM_NAME='" + txtRoomName.Text + "' AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); // ---cond added by Himanshu Tamrakar on 16/03/2024
            if (roomno != null && roomno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Room.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int roomNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = roomController.GetRoomByNo(roomNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                BindDataToControls(ds.Tables[0].Rows[0]);
               

                ViewState["action"] = "edit";
                ViewState["RoomNo"] = ds.Tables[0].Rows[0]["ROOM_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindDataToControls(DataRow dr)
    {
        try
        {
            this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "DISTINCT BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + dr["HOSTEL_NO"].ToString(), "BLOCK_NAME");
            this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + dr["HOSTEL_NO"].ToString() + " AND BLK_NO=" + dr["BLOCK_NO"].ToString(), "FLOOR_NO");
            if (dr["HBNO"].ToString() != null &&
               ddlHostel.Items.FindByValue(dr["HBNO"].ToString()) != null)
                ddlHostel.SelectedValue = dr["HBNO"].ToString();

            if (dr["ROOM_NAME"].ToString() != null)
                txtRoomName.Text = dr["ROOM_NAME"].ToString();

            if (dr["BLOCK_NO"].ToString() != null )
                ddlBlock.SelectedValue = dr["BLOCK_NO"].ToString();

            if (dr["CAPACITY"].ToString() != null &&
                ddlCapacity.Items.FindByValue(dr["CAPACITY"].ToString()) != null)
                ddlCapacity.SelectedValue = dr["CAPACITY"].ToString();

            if (dr["FLOOR_NO"].ToString() != null &&
                ddlFloor.Items.FindByValue(dr["FLOOR_NO"].ToString()) != null)
                ddlFloor.SelectedValue = dr["FLOOR_NO"].ToString();

            objCommon.FillDropDownList(ddlRoomtypename, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "TYPE_NO");

            if (dr["ROOM_TYPE"].ToString() != null &&
                ddlRoomtypename.Items.FindByValue(dr["ROOM_TYPE"].ToString()) != null)
                ddlRoomtypename.SelectedValue = dr["ROOM_TYPE"].ToString();

            if (dr["RESIDENT_TYPE_NO"].ToString() != null &&
                ResidentTypeNo.Items.FindByValue(dr["RESIDENT_TYPE_NO"].ToString()) != null)
                ResidentTypeNo.SelectedValue = dr["RESIDENT_TYPE_NO"].ToString();

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> '' AND BRANCHNO > 0 ", "SHORTNAME");
            //if (dr["BRANCHNO"].ToString() != null &&
            //    ddlBranch.Items.FindByValue(dr["BRANCHNO"].ToString()) != null)
            //    ddlBranch.SelectedValue = dr["BRANCHNO"].ToString();
            
            //if (dr["SEMESTERNO"].ToString() != null &&
            //    ddlSemester.Items.FindByValue(dr["SEMESTERNO"].ToString()) != null)
            //    ddlSemester.SelectedValue = dr["SEMESTERNO"].ToString();

            //if (dr["CAPACITY"].ToString() != null &&
            //    ddlCapacity.Items.FindByValue(dr["CAPACITY"].ToString()) != null)
            //    ddlCapacity.SelectedValue = dr["CAPACITY"].ToString();

            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 ", "DEGREENO");
            //if (ddlBranch.SelectedValue != "0")
            //    ddlDegree.SelectedValue = objCommon.LookUp("ACD_BRANCH", "DEGREENO", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.BindDataToControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private Room BindDataFromControls()
    {
        Room room = new Room();
        try
        {
            room.RoomName = txtRoomName.Text.Trim();
            room.HostelNo = Convert.ToInt32(ddlHostel.SelectedValue);
            if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
                room.BlockNo = (ddlBlock.SelectedValue != string.Empty ? int.Parse(ddlBlock.SelectedValue) : 0);

            if (ddlCapacity.SelectedValue != null && ddlCapacity.SelectedIndex > 0)
                room.Capacity = (ddlCapacity.SelectedValue != string.Empty ? int.Parse(ddlCapacity.SelectedValue) : 0);

            if (ddlFloor.SelectedValue != null && ddlFloor.SelectedIndex > 0)
                room.FloorNo = (ddlFloor.SelectedValue != string.Empty ? int.Parse(ddlFloor.SelectedValue) : 0);

            //if (ddlResidentType.SelectedValue != null && ddlResidentType.SelectedIndex > 0)
            //    room.ResidentTypeNo = (ddlResidentType.SelectedValue != string.Empty ? int.Parse(ddlResidentType.SelectedValue) : 0);

            //if (ddlBranch.SelectedValue != null && ddlBranch.SelectedIndex > 0)
            //    room.BranchNo = (ddlBranch.SelectedValue != string.Empty ? int.Parse(ddlBranch.SelectedValue) : 0);

            //if (ddlSemester.SelectedValue != null && ddlSemester.SelectedIndex > 0)
            //    room.SemesterNo = (ddlSemester.SelectedValue != string.Empty ? int.Parse(ddlSemester.SelectedValue) : 0);

            //ADDED AS PER REQUERIMENT ON 22-07-2022 ADDED BY SHUBHAM
            if (ddlRoomtypename.SelectedValue != null && ddlRoomtypename.SelectedIndex > 0)
                room.Roomtype = (ddlRoomtypename.SelectedValue != string.Empty ? int.Parse(ddlRoomtypename.SelectedValue) : 0);
            if (ResidentTypeNo.SelectedValue != null && ResidentTypeNo.SelectedIndex > 0)
                room.ResidentTypeNo = (ResidentTypeNo.SelectedValue != string.Empty ? int.Parse(ResidentTypeNo.SelectedValue) : 0);


            //room.organizationid = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            room.CollegeCode = Session["colcode"].ToString();            
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.BindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
        return room;
    }
    
    protected void dpRooms_PreRender(object sender, EventArgs e)
    {
        ShowAllRooms();
    }
    #endregion

    #region Private Methods
    private void ShowAllRooms()
    {
        try
        {
            RoomController objRC = new RoomController();
           // DataSet ds = objRC.GetAllRooms(Convert.ToInt32(ddlHostel.SelectedValue.ToString()), Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
            DataSet ds = objRC.GetAllRooms();
            lvRooms.DataSource = ds;
            lvRooms.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.ShowAllRooms() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearControlContents()
    {
        ddlHostel.SelectedIndex = 0;
        ddlBlock.SelectedIndex = 0;
        ddlFloor.SelectedIndex = 0;
        txtRoomName.Text = string.Empty;
        ddlCapacity.SelectedIndex = 0;
        ddlRoomtypename.SelectedIndex = 0;
        ResidentTypeNo.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
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

    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "BLOCK_NAME");
        ddlBlock.Focus();
        //added by shubham as the requirement form the crescent  
        objCommon.FillDropDownList(ddlRoomtypename, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "TYPE_NO");
       // this.ShowAllRooms();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlock.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLK_NO=" + ddlBlock.SelectedValue, "FLOOR_NO");
                ddlFloor.Focus();
            }
            else
                ddlBlock.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Room.ddlBlock_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
}