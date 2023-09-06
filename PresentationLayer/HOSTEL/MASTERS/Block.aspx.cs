//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : BLOCK MASTER
// CREATION DATE : 24-NOV-2010
// CREATED BY    : GAURAV SONI
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


public partial class HOSTEL_MASTERS_Block : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    BlockController objBC = new BlockController();
    Block objBlock = new Block();

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
                }              
                BindListView();
                PopulateDropDownList();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_BlockMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Block.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Block.aspx");
        }
    }
    #endregion Page Events

    #region Action
    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {

            string blno = objCommon.LookUp("ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME='" + txtBlockName.Text + "'  and  HOSTEL_NO ='" + Convert.ToInt32(ddlHostel.SelectedValue) + "'");
            if (blno != null && blno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Block.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckDuplicateEntryUpdate(int bl_no)
    {
        bool flag = false;
        try
        {
            string blno = objCommon.LookUp("ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLOCK_NAME='" + txtBlockName.Text + "' and BL_NO != " + bl_no.ToString() + " ");
            if (blno != null && blno != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Block.CheckDuplicateEntryUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {          
                     
            objBlock.BlockCode = txtBlockCode.Text.Trim();
            objBlock.BlockName = txtBlockName.Text.Trim();
            objBlock.CollegeCode = Session["colcode"].ToString();
            objBlock.HostelNo = Convert.ToInt32(ddlHostel.SelectedValue);

            /// check form action whether add or update
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
                    CustomStatus cs = (CustomStatus)objBC.AddBlock(objBlock);
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
                    if (ViewState["bl_no"] != null)
                    {
                        objBlock.BlockNo = Convert.ToInt32(ViewState["bl_no"].ToString());

                        if (CheckDuplicateEntryUpdate(objBlock.BlockNo) == true)
                        {
                            objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                            return;
                        }

                        CustomStatus cs = (CustomStatus)objBC.UpdateBlock(objBlock);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                        }
                    }
                }
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_Asset.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int bl_no = int.Parse(btnEdit.CommandArgument);

            ShowDetail(bl_no);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInfo.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {       
        Clear();        
    }

    private void Clear()
    {     
        txtBlockName.Text = string.Empty;
        txtBlockCode.Text = string.Empty;
        ddlHostel.SelectedIndex = 0;
        ViewState["action"] = "add";
        BindListView();
    }   

    private void BindListView()
    {
        try
        {          
            DataSet ds = null;
           // ds =objCommon.FillDropDown("ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_CODE, BLOCK_NAME", "BL_NO > 0 ", "BLOCK_NAME");

            // ds = objCommon.FillDropDown("ACD_HOSTEL_BLOCK_MASTER HM WITH(NOLOCK) INNER JOIN ACD_HOSTEL H WITH(NOLOCK) ON (HM.HOSTEL_NO=H.HOSTEL_NO)", "HM.BL_NO", "HM.BLOCK_CODE, HM.BLOCK_NAME, H.HOSTEL_NAME", "HM.BL_NO > 0 ", "HM.BLOCK_NAME"); // commented by Saurabh L on 23/05/2022

            ds = objCommon.FillDropDown("ACD_HOSTEL_BLOCK_MASTER HM WITH(NOLOCK) INNER JOIN ACD_HOSTEL H WITH(NOLOCK) ON (HM.HOSTEL_NO=H.HOSTEL_NO)", "HM.BL_NO", "HM.BLOCK_CODE, HM.BLOCK_NAME, H.HOSTEL_NAME", "HM.BL_NO > 0 AND HM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "HM.BLOCK_NAME");

            if (ds != null)
            {
                if((ds.Tables[0].Rows.Count>0))
                {

                   lvBlock.DataSource = ds;
                   lvBlock.DataBind();
                }
                else
                {
                   lvBlock.DataSource = null;
                   lvBlock.DataBind();

                }
            }
            else
            {
                lvBlock.DataSource = null;
                lvBlock.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInfo.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN HOSTEL NAME
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0 ", "HOSTEL_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "BlockInformation.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int bl_no)
    {
       
        SqlDataReader dr = objBC.GetBlock(bl_no);

        //Show Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["bl_no"] = bl_no.ToString();              
                txtBlockCode.Text = dr["BLOCK_CODE"] == null ? string.Empty : dr["BLOCK_CODE"].ToString();
                txtBlockName.Text = dr["BLOCK_NAME"] == null ? string.Empty : dr["BLOCK_NAME"].ToString();

                if (dr["HOSTEL_NO"].ToString() != null)
                    ddlHostel.SelectedValue = dr["HOSTEL_NO"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    protected void dpBlock_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
    #endregion Action
}
