//===========================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : RequisitionTrack.aspx                                                  
// CREATION DATE : 26-Sept-2020                                                       
// CREATED BY    : MRUNAL SINGH                                                      
// MODIFIED DATE :
// MODIFIED DESC :
//============================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;

public partial class STORES_Transactions_Quotation_RequisitionTrack : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
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
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                this.FillRequisition();
            }
        }

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

    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStoreUser";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    private void FillRequisition()
    {
        try
        {
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {

                objCommon.FillDropDownList(ddlRequisition, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' AND MDNO='" + Convert.ToInt32(Application["strrefmaindept"]) + "'", "REQTRNO DESC");
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                objCommon.FillDropDownList(ddlRequisition, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' AND MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()), "REQTRNO DESC");
            }
            else
            {
                objCommon.FillDropDownList(ddlRequisition, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' AND MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()) + "AND NAME='" + Session["userfullname"].ToString() + "'", "REQTRNO DESC");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "STORES_Transactions_Quotation_RequisitionTrack.FillRequisition() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/STORES/Transactions/Quotation/Str_user_Requisition.aspx?pageno=1168");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "STORES_Transactions_Quotation_RequisitionTrack.btnBack_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            lblItemIssue.Text  =string.Empty;
            lblItemAccept.Text = string.Empty;
            ds = objDeptReqController.GetRequisitionTrackDetails(Convert.ToInt32(ddlRequisition.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblReqDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["REQ_DATE"]).ToString("dd-MM-yyyy");
                divReqDate.Visible = true;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvItemDetails.DataSource = ds.Tables[0];
                    lvItemDetails.DataBind();
                    pnlItem.Visible = true;
                }
                else
                {
                    lvItemDetails.DataSource = null;
                    lvItemDetails.DataBind();
                    pnlItem.Visible = false;
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lvAuthority.DataSource = ds.Tables[1];
                    lvAuthority.DataBind();
                    pnlAuthhority.Visible = true;
                }
                else
                {
                    lvAuthority.DataSource = null;
                    lvAuthority.DataBind();
                    pnlAuthhority.Visible = false;
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    // Panel1.Visible = true;
                    if (ds.Tables[2].Rows[0]["REQ_FOR"].ToString() == "P") // Purchase
                    {
                        if (ds.Tables[2].Rows[0]["ISTYPE"].ToString() == "Q")  // Quotation
                        {
                            divIndentP.Visible = true;
                            divQuot.Visible = true;
                            divPOPre.Visible = true;
                            divPOApproval.Visible = true;
                            divItemIssue.Visible = false;
                            divItemAccept.Visible = false;
                        }
                        else    // Direct PO
                        {
                            divIndentP.Visible = true;
                            divQuot.Visible = false;
                            divPOPre.Visible = true;
                            divPOApproval.Visible = true;
                            divItemIssue.Visible = false;
                            divItemAccept.Visible = false;
                        }                        
                    }
                    else if (ds.Tables[2].Rows[0]["REQ_FOR"].ToString() == "I")   // Issue
                    {
                        divIndentP.Visible = false;
                        divQuot.Visible = false;
                        divPOPre.Visible = false;
                        divPOApproval.Visible = false;
                        divItemIssue.Visible = true;
                        divItemAccept.Visible = true;
                    }
                    
                    

                    lblIndent.Text = ds.Tables[2].Rows[0]["INDENT"].ToString();
                    lblQuot.Text = ds.Tables[2].Rows[0]["QUOTATION"].ToString();
                    lblPO.Text = ds.Tables[2].Rows[0]["POREF"].ToString();
                    lblPOApprroval.Text = ds.Tables[2].Rows[0]["POAPPROVAL"].ToString();

                    lblInvoice.Text = ds.Tables[2].Rows[0]["INVOICE"].ToString();
                    //lblItemReceived.Text = ds.Tables[3].Rows[0]["ISSUED_QTY"].ToString();
                    lblItemIssue.Text = ds.Tables[3].Rows[0]["ISSUED_QTY"].ToString();
                    lblItemAccept.Text = ds.Tables[3].Rows[0]["ACC_QTY"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "STORES_Transactions_Quotation_RequisitionTrack.btnBack_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}