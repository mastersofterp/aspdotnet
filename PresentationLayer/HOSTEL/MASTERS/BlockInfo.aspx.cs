//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : BLOCK MASTER                                                         
// CREATION DATE : 24-DEC-2010                                                          
// CREATED BY    : GAURAV S SONI
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

public partial class Hostel_Masters_BlockInfo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();
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
            BindListView();
            ViewState["action"] = "add";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
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

    #region Actions
    protected void btnCancel_Click(object sender, EventArgs e)
             {
                 Clear();
                 ViewState["action"] = "add";
             }

             protected void btnSave_Click(object sender, EventArgs e)
             {
                 try
                 {
                     BlockController objBC = new BlockController();
                     Block objBlock = new Block();
                     objBlock.BlockName = ddlBlockName.SelectedValue;
                     objBlock.HostelNo = Convert.ToInt32(ddlHostelName.SelectedValue);
                     objBlock.Floor_No = Convert.ToInt32(ddlFloor.SelectedValue);
                     objBlock.RoomCapacity = Convert.ToInt32(txtBlockCapacity.Text.Trim());
                     objBlock.CollegeCode = Session["colcode"].ToString();

                     //Check whether to add or update
                     if (ViewState["action"] != null)
                     {
                         if (ViewState["action"].ToString().Equals("add"))
                         {
                             if (CheckDuplicateEntry() == true)
                             {
                                 objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                                 return;
                             }
                             //Add Block
                             CustomStatus cs = (CustomStatus)objBC.AddBlockInfo(objBlock);
                             if (cs.Equals(CustomStatus.RecordSaved))
                             {
                                 objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                                 ViewState["action"] = "add";
                                 Clear();
                             }
                         }
                         else
                         {
                             //Edit
                             if (ViewState["block_no"] != null)
                             {
                                 objBlock.BlockNo = Convert.ToInt32(ViewState["block_no"].ToString());
                                 if (CheckDuplicateEntryUpdate(objBlock.BlockNo) == true)
                                 {
                                     objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                                     return;
                                 }
                                 CustomStatus cs = (CustomStatus)objBC.UpdateBlockInfo(objBlock);
                                 if (cs.Equals(CustomStatus.RecordUpdated))
                                 {
                                     objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                                     ViewState["action"] = "add";
                                     Clear();
                                 }
                             }
                         }
                         BindListView();
                     }

                 }
                 catch (Exception ex)
                 {
                     if (Convert.ToBoolean(Session["error"]) == true)
                         objUCommon.ShowError(Page, "BlockInfo.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
                     else
                         objUCommon.ShowError(Page, "Server UnAvailable");
                 }
             }


             protected void btnEdit_Click(object sender, ImageClickEventArgs e)
         {
             try
             {
                 ImageButton btnEdit = sender as ImageButton;
                 int block_no = int.Parse(btnEdit.CommandArgument);

                 ShowDetail(block_no);
                 ViewState["action"] = "edit";
             }
             catch (Exception ex)
             {
                 if (Convert.ToBoolean(Session["error"]) == true)
                     objUCommon.ShowError(Page, "BlockInfo.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
                 else
                     objUCommon.ShowError(Page, "Server UnAvailable");
             }
         }
        #endregion

    #region Private Methods
    private void Clear()
        {
            ddlHostelName.SelectedIndex = 0;
            ddlBlockName.SelectedIndex = 0;
            txtBlockCapacity.Text = string.Empty;
            ddlFloor.SelectedIndex = 0;
        }

    private void PopulateDropDownList()
        {
            try
            {
                // FILL DROPDOWN HOSTEL NAME
                objCommon.FillDropDownList(ddlHostelName, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "HOSTEL_NAME");
                objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_FLOOR", "FLOOR_NO", "FLOOR_NAME", "FLOOR_NO > 0 and ACTIVESTATUS =1", "FLOOR_NAME");
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "BlockInformation.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    private void BindListView()
        {
            try
            {
                BlockController objBC = new BlockController();
                DataSet ds = objBC.GetAllBlock();
                lvBlock.DataSource = ds;
                lvBlock.DataBind();
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "BlockInfo.BindListView-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    private bool CheckDuplicateEntry()
        {
            bool flag = false;
            try
            {
                string blockno = objCommon.LookUp("ACD_HOSTEL_BLOCK", "BLOCK_NO", "HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND NO_OF_FLOORS=" + ddlFloor.SelectedValue + " AND BLK_NO='" + ddlBlockName.SelectedValue + "'");
                if (blockno != null && blockno != string.Empty)
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

    private bool CheckDuplicateEntryUpdate(int blno)
    {
        bool flag = false;
        try
        {
            string blockno = objCommon.LookUp("ACD_HOSTEL_BLOCK", "BLOCK_NO", "BLOCK_NO !=" + blno + "  and HOSTEL_NO=" + ddlHostelName.SelectedValue + " AND NO_OF_FLOORS=" + ddlFloor.SelectedValue + " AND BLK_NO='" + ddlBlockName.SelectedValue + "'");
            if (blockno != null && blockno != string.Empty)
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

    private void ShowDetail(int block_no)
        {
            BlockController objBC = new BlockController();
            SqlDataReader dr = objBC.GetBlockType(block_no);

            //Show Detail
            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["block_no"] = block_no.ToString();
                    ddlHostelName.SelectedValue = dr["HOSTEL_NO"] == null ? string.Empty : dr["HOSTEL_NO"].ToString();
                    objCommon.FillDropDownList(ddlBlockName, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + dr["HOSTEL_NO"].ToString(), "BLOCK_NAME");
                    ddlBlockName.Focus();
                    ddlBlockName.SelectedValue = dr["BLK_NO"] == null ? string.Empty : dr["BLK_NO"].ToString();
                    ddlFloor.Text = dr["NO_OF_FLOORS"] == null ? string.Empty : dr["NO_OF_FLOORS"].ToString();
                    txtBlockCapacity.Text = dr["ROOM_CAPACITY"] == null ? string.Empty : dr["ROOM_CAPACITY"].ToString();
                }
            }
            if (dr != null) dr.Close();
        }

    protected void dpBlock_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void ddlHostelName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBlockName, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + ddlHostelName.SelectedValue + " AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "BLOCK_NAME");

        ddlBlockName.Focus();
    }
    #endregion
}
