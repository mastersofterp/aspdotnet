//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : ASSET ALLOTMENT                                                      
// CREATION DATE : 17-DEC-2010                                                          
// CREATED BY    : GAURAV SONI                                                   
// MODIFIED DATE :                                                                      
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

public partial class Hostel_AssetAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AssetAllotmentController objAssetAllotmentController = new AssetAllotmentController();

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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    
                    //ddlHostel.SelectedIndex = 0;
                    //ddlBlock.Enabled = false;
                    //ddlFloor.Enabled = false;
                    //ddlRoom.Enabled = false;

                    // Fill Dropdown lists                
                    if (Session["usertype"].ToString() == "1")
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
                    else
                        objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
                    this.objCommon.FillDropDownList(ddlRoom, "ACD_HOSTEL_ROOM", "ROOM_NO", "ROOM_NAME", string.Empty, "ROOM_NAME");
                    

                    
                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                }
                this.ShowAllAssestAllotment();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=AssetAllotment.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AssetAllotment.aspx");
        }
    }
    #endregion

    #region Actions

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
          try
          {
            AssetAllotment objAssetAllotment = this.BindDataFromControls();
            
            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();

                /// Add AssetAllotment
                if (ViewState["action"].ToString().Equals("add"))
                {
                    cs = (CustomStatus)objAssetAllotmentController.AddAssetAllotment(objAssetAllotment);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        objCommon.DisplayMessage("Record Saved successfully!!", this.Page);
                }

                /// Update AssetAllotment
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    objAssetAllotment.AssetAllotmentNo = (GetViewStateItem("AssetAllotmentNo") != string.Empty ? int.Parse(GetViewStateItem("AssetAllotmentNo")) : 0);
                    cs = (CustomStatus)objAssetAllotmentController.UpdateAssetAllotment(objAssetAllotment);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                        objCommon.DisplayMessage("Record Updated successfully!!", this.Page);
                    ViewState["action"] = "add";
                }

                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");
                else
                    this.ShowAllAssestAllotment();
            }
            this.ClearControlContents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
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
            int assetAllotmentNo = Int32.Parse(editButton.CommandArgument);

            DataSet ds = objAssetAllotmentController.GetAssetAllotmentByNo(assetAllotmentNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                BindDataToControls(ds.Tables[0].Rows[0]);

                ViewState["action"] = "edit";
                ViewState["AssetAllotmentNo"] = ds.Tables[0].Rows[0]["ASSET_ALLOTMENT_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
        
        private void BindDataToControls(DataRow dr)
        {
        try
        {
            if (dr["HOSTEL_NO"].ToString() != null &&
                ddlHostel.Items.FindByValue(dr["HOSTEL_NO"].ToString()) != null)
                ddlHostel.SelectedValue = dr["HOSTEL_NO"].ToString();
            this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "HB.BLOCK_NAME");

            if (dr["BL_NO"].ToString() != null &&
                ddlBlock.Items.FindByValue(dr["BL_NO"].ToString()) != null)
                ddlBlock.SelectedValue = dr["BL_NO"].ToString();
            this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLK_NO=" + ddlBlock.SelectedValue, "FLOOR_NO");

            if (dr["FLOOR_NO"].ToString() != null &&
                ddlFloor.Items.FindByValue(dr["FLOOR_NO"].ToString()) != null)
                ddlFloor.SelectedValue = dr["FLOOR_NO"].ToString();

            this.objCommon.FillDropDownList(ddlRoom, "ACD_HOSTEL_ROOM", "ROOM_NO", "ROOM_NAME", "BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue) + " and FLOOR_NO=" + Convert.ToInt32(ddlFloor.SelectedValue), "ROOM_NO");
            if (dr["ROOM_NO"].ToString() != null &&
                ddlRoom.Items.FindByValue(dr["ROOM_NO"].ToString()) != null)
                ddlRoom.SelectedValue = dr["ROOM_NO"].ToString();

            if (dr["ASSET_NO"].ToString() != null &&
                ddlAsset.Items.FindByValue(dr["ASSET_NO"].ToString()) != null)
                ddlAsset.SelectedValue = dr["ASSET_NO"].ToString();

            if (dr["QUANTITY"].ToString() != null)
                txtAssetQty.Text = dr["QUANTITY"].ToString(); 
            
            if (dr["ALLOTMENT_DATE"].ToString() != null)
                txtAllotmentDate.Text = dr["ALLOTMENT_DATE"].ToString();

            if (dr["ALLOTMENT_CODE"].ToString() != null)
                txtAllotmentCode.Text = dr["ALLOTMENT_CODE"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.BindDataToControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

        private AssetAllotment BindDataFromControls()
    {
        AssetAllotment objAssetAllotment = new AssetAllotment();
        try
        {

            if (ddlRoom.SelectedValue != null && ddlRoom.SelectedIndex > 0)
                objAssetAllotment.RoomNo = (ddlRoom.SelectedValue != string.Empty ? int.Parse(ddlRoom.SelectedValue) : 0);

            if (ddlAsset.SelectedValue != null && ddlAsset.SelectedIndex > 0)
                objAssetAllotment.AssetNo = (ddlAsset.SelectedValue != string.Empty ? int.Parse(ddlAsset.SelectedValue) : 0);

            objAssetAllotment.Quantity =  Convert.ToInt32(txtAssetQty.Text.Trim());
            objAssetAllotment.AllotmentDate = Convert.ToDateTime(txtAllotmentDate.Text.Trim());
            objAssetAllotment.AllotmentCode = txtAllotmentCode.Text.Trim();
            objAssetAllotment.CollegeCode = Session["colcode"].ToString();
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.BindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
        return objAssetAllotment;
    }

        protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHostel.SelectedIndex > 0)
            {
                //ddlBlock.Enabled = true;
                this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "HB.BLOCK_NAME");
                ddlBlock.Focus();
                txtAllotmentCode.Text = "H"+ddlHostel.SelectedValue+"/";
                ViewState["auto1"] = txtAllotmentCode.Text;
            }
            else
            {
                //ddlBlock.Enabled = false;
                ddlHostel.Focus();
            }
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBlock.SelectedIndex > 0)
            {
                //ddlFloor.Enabled = true;
                this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLK_NO=" + ddlBlock.SelectedValue, "FLOOR_NO");
                ddlFloor.Focus();
                
                //auto code
                txtAllotmentCode.Text = ViewState["auto1"].ToString();
                //string autocode = objCommon.LookUp("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "BLOCK_CODE", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue) + " AND HB.BL_NO="+Convert.ToInt32(ddlBlock.SelectedValue));
                txtAllotmentCode.Text = txtAllotmentCode.Text + "B"+ddlBlock.SelectedValue + "/";
                ViewState["auto2"] = txtAllotmentCode.Text;
            }
            else
            {
                //ddlFloor.Enabled = false;
                ddlBlock.Focus();
            }
        }

        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFloor.SelectedIndex > 0)
                {
                    //ddlRoom.Enabled = true;
                    this.objCommon.FillDropDownList(ddlRoom, "ACD_HOSTEL_ROOM R INNER JOIN ACD_HOSTEL_BLOCK B ON(R.BLOCK_NO=B.BLOCK_NO)", "DISTINCT ROOM_NO", "ROOM_NAME", "HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + " AND R.BLOCK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue) + " AND FLOOR_NO = " + Convert.ToInt32(ddlFloor.SelectedValue), "ROOM_NAME");
                    ddlRoom.Focus();
                    
                    //auto code
                    txtAllotmentCode.Text = ViewState["auto2"].ToString();
                    //string autocode = objCommon.LookUp("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "F.CODE", "B.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue) + " AND B.BLK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue));
                    txtAllotmentCode.Text = txtAllotmentCode.Text + "F" + ddlFloor.SelectedValue + "/";
                    ViewState["auto3"] = txtAllotmentCode.Text;
                }
                else
                {
                    //ddlRoom.Enabled = false;
                    ddlFloor.Focus();
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.ddlFloor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server Unavailable");
            }
        }

        protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.objCommon.FillDropDownList(ddlAsset, "ACD_HOSTEL_ASSET", "ASSET_NO", "ASSET_NAME", "ASSET_NO>0", "ASSET_NAME");
            //auto code
            txtAllotmentCode.Text = ViewState["auto3"].ToString();
            //string autocode = objCommon.LookUp("ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "F.CODE", "B.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue) + " AND B.BLK_NO=" + Convert.ToInt32(ddlBlock.SelectedValue));
            txtAllotmentCode.Text = txtAllotmentCode.Text + "R" + ddlRoom.SelectedValue + "/";
            ViewState["auto4"] = txtAllotmentCode.Text;
        }

        protected void ddlAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAllotmentCode.Text = ViewState["auto4"].ToString();
            string auto = objCommon.LookUp("ACD_HOSTEL_ASSET_ALLOTMENT AA LEFT OUTER JOIN ACD_HOSTEL_ROOM R ON (R.ROOM_NO=AA.ROOM_NO)LEFT OUTER JOIN ACD_HOSTEL_BLOCK_MASTER B ON (B.BL_NO=R.BLOCK_NO)", "ISNULL(MAX(ASSET_ALLOTMENT_NO),0)+1", "AA.ROOM_NO="+Convert.ToInt32(ddlRoom.SelectedValue)+" AND BLOCK_NO="+Convert.ToInt32(ddlBlock.SelectedValue)+" AND FLOOR_NO="+Convert.ToInt32(ddlFloor.SelectedValue)+" AND B.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+" AND ASSET_NO="+Convert.ToInt32(ddlAsset.SelectedValue));
            txtAllotmentCode.Text = txtAllotmentCode.Text +ddlAsset.SelectedValue+auto;
        }
    #endregion

    #region Private Methods
        private void ShowAllAssestAllotment()
    {
        try
        {
            DataSet ds = objAssetAllotmentController.GetAllAssetAllotment();
            lvAssetAllotment.DataSource = ds;
            lvAssetAllotment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_AssetAllotment.ShowAllAssestAllotment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

        protected void dpAessetAllotment_PreRender(object sender, EventArgs e)
    {
        ShowAllAssestAllotment();
    }

        private void ClearControlContents()
    {
        ddlHostel.SelectedIndex = 0;
        ddlBlock.SelectedIndex = 0;
        ddlFloor.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
        ddlAsset.SelectedIndex = 0;
        txtAssetQty.Text = string.Empty;
        txtAllotmentDate.Text = string.Empty;
        txtAllotmentCode.Text = string.Empty;
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
        
        
}