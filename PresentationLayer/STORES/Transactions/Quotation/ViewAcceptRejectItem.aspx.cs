//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : ViewAcceptRejectItem.aspx                                                  
// CREATION DATE : 14-Mar-2019                                                        
// CREATED BY    : Mrunal Singh                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;

public partial class STORES_Transactions_Quotation_ViewAcceptRejectItem : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objApp = new StoreMasterController();
    DataSet dsdetails = new DataSet();
    DataTable dtgrid = new DataTable();
    Str_Purchase_Order_Controller objstrPO = new Str_Purchase_Order_Controller();
    STR_DEPT_REQ_CONTROLLER ObjReq = new STR_DEPT_REQ_CONTROLLER();
    Str_DeptRequest objdeptRequest = new Str_DeptRequest();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                } 
                objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "", "MDNAME");      
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }


    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.ddlDept_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindGrid()
    {
        try
        {
            DataSet ds = null;
            ds = objApp.GetApprovedRequisitionList(Convert.ToInt32(ddlDept.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRequisition.DataSource = ds;
                lvRequisition.DataBind();
                pnllist.Visible = true;
            }
            else
            {
                lvRequisition.DataSource = null;
                lvRequisition.DataBind();
                pnllist.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.BindGrid ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
        // DataSet ds = objApp.GetAllSTAGE();        
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        int ReqTrno = Convert.ToInt32(btn.CommandArgument);
        ViewState["TRNO"] = ReqTrno;
        pnllist.Visible = false;
        pnlDept.Visible = false;        
        GetReqDetails(ReqTrno);
    }


    public void GetReqDetails(int REQTRNO)
    {
        if (REQTRNO > 0)
        {
           
            Decimal AproxCost = 0;
            DataSet ds = ObjReq.GetItemDetailsByReqId(REQTRNO);           
            lvitemReq.DataSource = ds;
            lvitemReq.DataBind();

            ViewState["REQ_SLIP_NO"] = ds.Tables[0].Rows[0]["REQ_NO"].ToString();
            lblReqSlipNo.Text = ds.Tables[0].Rows[0]["REQ_NO"].ToString();
            lblReqDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["REQ_DATE"]).ToString("dd/MM/yyyy");
            lblDeptName.Text = ds.Tables[0].Rows[0]["SDNAME"].ToString();
            lblAuthorityName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

            pnlItemDetail.Visible = true;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AproxCost = AproxCost + Convert.ToDecimal(ds.Tables[0].Rows[i]["Tot_Cost"].ToString());
            }
            lblTotAppCost.Text = AproxCost.ToString();
        }
        else
        {
            lvitemReq.DataSource = null;
            lvitemReq.DataBind();
        }
    }

    protected void lvitemReq_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
        HiddenField hdnARstaus = e.Item.FindControl("hdbAcceptReject") as HiddenField;
        if (hdnARstaus.Value == "A")
        {
            chkItem.Checked = true;
            chkItem.Enabled = false;
        }
        else if (hdnARstaus.Value == "R")
        {
            chkItem.Checked = false;
            chkItem.Enabled = false;
        }
        else
        {
            chkItem.Enabled = false;
        }
        
    }



  

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["TRNO"] = null;
        ddlDept.SelectedIndex = 0;
        pnlDept.Visible = true;
        pnllist.Visible = false;
        pnlItemDetail.Visible = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {      
        pnllist.Visible = true; 
        pnlItemDetail.Visible = false;
        pnlDept.Visible = true;
    }  



   
}