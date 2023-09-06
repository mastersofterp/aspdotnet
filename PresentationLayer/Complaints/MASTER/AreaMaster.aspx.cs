//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : AREA MASTER                                                       
// CREATION DATE : 11-OCT-2017                                                        
// CREATED BY    : MRUNAL SINGH                                                   
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Complaints_MASTER_AreaMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Complaint objCEnt = new Complaint();
    ComplaintController objCCon = new ComplaintController();

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindListView();
                ViewState["action"] = "add";

            }
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_AREA", "AREAID", "AREANAME", "", "AREAID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvArea.DataSource = ds;
                lvArea.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_AreaMaster.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int AreaId = int.Parse(btnEdit.CommandArgument);
            ViewState["AREAID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetail(AreaId);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_AreaMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int AreaId)
    {
        DataSet ds = objCommon.FillDropDown("COMPLAINT_AREA", "AREAID", "AREANAME", "AREAID = " + AreaId, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["AREAID"] = ds.Tables[0].Rows[0]["AREAID"].ToString();
            txtAreaName.Text = ds.Tables[0].Rows[0]["AREANAME"].ToString();
        }
    }

    protected Boolean AreaDuplicate()
    {
        DataSet ds = null;

        ds = objCommon.FillDropDown("COMPLAINT_AREA", "*", " ", "AREANAME='" + txtAreaName.Text + "'", "");
        
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCEnt.AREAID = 0;
            objCEnt.AREANAME = txtAreaName.Text.Trim();

            if (AreaDuplicate() == true)
            {
                objCommon.DisplayMessage(UpdatePanel1, "This Area Name Already Exist.", this.Page);
                return;
            }
            else
            {

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        CustomStatus cs = (CustomStatus)objCCon.AddUpdateAreaName(objCEnt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            Clear();
                            BindListView();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully.", this.Page);
                        }
                    }
                    else
                    {
                        if (ViewState["AREAID"] != null)
                        {
                            objCEnt.AREAID = Convert.ToInt32(ViewState["AREAID"].ToString());

                            CustomStatus cs = (CustomStatus)objCCon.AddUpdateAreaName(objCEnt);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                Clear();
                                BindListView();
                                objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully.", this.Page);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_AreaMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtAreaName.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["AREAID"] = null;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }
}