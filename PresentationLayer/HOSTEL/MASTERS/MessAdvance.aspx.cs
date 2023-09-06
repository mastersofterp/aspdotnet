//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : BLOCK MASTER
// CREATION DATE : 08-march-2013
// CREATED BY    : Oves khan
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

public partial class Mess_Advance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                PopulateDropDownList();
                BindListView();
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
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            MessAdvanceController objMA = new MessAdvanceController();
            MessAdvance objmd = new MessAdvance();
            objmd.MESS_NO = Convert.ToInt32(ddlMess.SelectedValue);
            objmd.ADV_AMOUNT = Convert.ToDecimal(txtadvamt.Text);
            objmd.ADV_DATE = Convert.ToDateTime(txtadvdate.Text);
            objmd.ADV_REMARK = txtadvremark.Text.ToString().Trim();
            objmd.Comm_Member = Convert.ToInt32(ddlCommMem.SelectedValue);
            objmd.Session_No = Convert.ToInt32(ddlSession.SelectedValue);
            objmd.CollegeCode = Session["colcode"].ToString();
            int ret = -99;
            if (ViewState["action"].ToString() == "add")
                ret = objMA.AddMessInfo(objmd);
            else
            {
                objmd.MESSRNO = Convert.ToInt32(ViewState["bl_no"].ToString());
                ret = objMA.UpdateMessInfo(objmd);
                ViewState["action"] = "add";
            }
            if (ret != -99)
            {
                objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                Clear();
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed!!!", this.Page);
            }
            BindListView();
        }
        catch (Exception ex)
        {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUaimsCommon.ShowError(Page, "MESS_ADVANCE.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
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
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int mess_no = int.Parse(btnDelete.CommandArgument);
            MessAdvanceController objMA = new MessAdvanceController();
            MessAdvance objmd = new MessAdvance();
            objmd.MESS_NO = Convert.ToInt32(mess_no);
            CustomStatus cs = (CustomStatus)objMA.DeleteMess(objmd);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Record Deleted Successfully!!!", this.Page);               
                Clear();
            }
            BindListView();
            
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
        ddlMess.SelectedIndex = 0;
        txtadvamt.Text = string.Empty;
        txtadvdate.Text = string.Empty;
        txtadvremark.Text = string.Empty;
        ddlCommMem.SelectedIndex = 0;
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlMess, "ACD_HOSTEL_MESS", "Mess_No", "MESS_NAME", "Mess_No>0", "MESS_NAME");
            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "MessInformation.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            MessAdvanceController objMA = new MessAdvanceController();
            DataSet ds = new DataSet();
            ds = objMA.GetMessInfo();           
            lvMess.DataSource = ds;
            lvMess.DataBind();
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "GetMessInfo.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int bl_no)
    {
        MessAdvanceController objMD = new MessAdvanceController();
        MessAdvance objmc = new MessAdvance();      
        SqlDataReader dr = objMD.GetMess(bl_no);
        //Show Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["bl_no"] = bl_no.ToString();
                ddlMess.SelectedValue = dr["MESS_NO"] == null ? string.Empty : dr["MESS_NO"].ToString();
                txtadvamt.Text = dr["ADV_AMOUNT"] == null ? string.Empty : dr["ADV_AMOUNT"].ToString();
                txtadvdate.Text = dr["ADV_DATE"] == null ? string.Empty : dr["ADV_DATE"].ToString();
                txtadvremark.Text = dr["ADV_REMARK"] == null ? string.Empty : dr["ADV_REMARK"].ToString();
                objCommon.FillDropDownList(ddlCommMem, "ACD_STUDENT", "IDNO", "STUDNAME", "IDNO IN (SELECT IDNO FROM ACD_HOSTEL_COMM_MEMBER UNPIVOT(IDNO FOR A IN (MEMBER_ID1,MEMBER_ID2,MEMBER_ID3,MEMBER_ID4,MEMBER_ID5)) U WHERE MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue) + ")", "IDNO");
                if (dr["COMM_MEMBER"].ToString() != null && dr["COMM_MEMBER"].ToString() != "")
                ddlCommMem.SelectedValue = dr["COMM_MEMBER"] == null ? string.Empty : dr["COMM_MEMBER"].ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0", "HOSTEL_SESSION_NO");
                if (dr["SESSION_NO"].ToString() != null && dr["SESSION_NO"].ToString() != "")
                    ddlSession.SelectedValue = dr["SESSION_NO"] == null ? string.Empty : dr["SESSION_NO"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    //protected void dpBlock_PreRender(object sender, EventArgs e)
    //{
    //    BindListView();
    //}

    protected void ddlMess_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommMem, "ACD_STUDENT", "IDNO", "STUDNAME", "IDNO IN (SELECT IDNO FROM ACD_HOSTEL_COMM_MEMBER UNPIVOT(IDNO FOR A IN (MEMBER_ID1,MEMBER_ID2,MEMBER_ID3,MEMBER_ID4,MEMBER_ID5)) U WHERE MESS_NO="+Convert.ToInt32(ddlMess.SelectedValue)+")", "IDNO");
    }
    
    #endregion Action
}
