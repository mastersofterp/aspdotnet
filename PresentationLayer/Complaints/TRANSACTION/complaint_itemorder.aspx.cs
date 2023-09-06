//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : COMPLAINT ITEMORDER                                                  
// CREATION DATE : 16-April-2009                                                        
// CREATED BY    : SANJAY RATNAPARKHI                                                   
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


public partial class Estate_complaint_itemorder : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    
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
                if (Request.QueryString["pageno"] != null) //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                //Populate Departments
                PopulateDepartment();

                // show departmentname according username
                ComplaintController objCTC = new ComplaintController();
                ddlDepartmentName.Text = objCTC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())).ToString();

                //Fill Item Names
                FillItemName(objCTC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())));

                //Bind the ListView Item Order
                BindListViewItemOrder(objCTC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())));

                txtOrderDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                ViewState["action"] = "add";
                //pnlAdd.Visible = false;
                //pnlList.Visible = true;
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
       // pnlAdd.Visible = true;
        Clear();
       // pnlList.Visible = false;
        txtQtyOrder.Enabled = true;
        ViewState["action"] = "add";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // pnlAdd.Visible = false;
       // pnlList.Visible = true;
        Clear();
        ViewState["action"] = null;
    }

    //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEdit = sender as ImageButton;
    //        int orderid = int.Parse(btnEdit.CommandArgument);

    //        ShowDetail(orderid);

    //        ViewState["action"] = "edit";
    //        pnlAdd.Visible = true;
    //        pnlList.Visible = false;

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "complaint_itemorder.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //private void ShowDetail(int orderid)
    //{
    //    try
    //    {
    //        ComplaintController objCIC = new ComplaintController();
    //        SqlDataReader dr = objCIC.GetSingleComplaintItemOrder(orderid);

    //        //Show Complaint item master and itemorder 
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {
    //                ViewState["orderid"] = orderid.ToString();
    //                ddlDepartmentName.Text = dr["DEPTID"] == null ? string.Empty : dr["DEPTID"].ToString();
    //                ddlItemName.Text = dr["ITEMID"] == null ? string.Empty : dr["ITEMID"].ToString();
    //                txtQtyOrder.Text = dr["ORDERQTY"] == null ? string.Empty : dr["ORDERQTY"].ToString();
    //                txtQtyOrder.Enabled = false;
    //                txtOrderDate.Text = dr["ORDERDATE"] == null ? string.Empty : Convert.ToDateTime(dr["ORDERDATE"]).ToString("dd-MMM-yyyy");
    //            }
    //        }
    //        if (dr != null) dr.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Estate_complaint_itemorder.ShowDetail-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void BindListViewItemOrder(int deptid)
    {
        try
        {
            ComplaintController objCIC = new ComplaintController();                       
            DataSet dsItemOrder = objCIC.GetAllItemOrder(deptid);

            if (dsItemOrder.Tables[0].Rows.Count > 0)
            {
                lvItemOrder.DataSource = dsItemOrder;
                lvItemOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_itemorder.BindListViewItemOrder-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlItemName.SelectedIndex = -1;
        txtQtyOrder.Text = string.Empty;
        txtOrderDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        ViewState["action"] = "add";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ComplaintController objCIC = new ComplaintController();
            Complaint objComplaintItemorder = new Complaint();

            objComplaintItemorder.ItemId = Convert.ToInt32(ddlItemName.SelectedValue);
            objComplaintItemorder.QtyOrder =Convert.ToInt32(txtQtyOrder.Text.Trim());
            if (!txtOrderDate.Text.Equals(string.Empty)) objComplaintItemorder.OrderDate = Convert.ToDateTime(txtOrderDate.Text.Trim());
            objComplaintItemorder.Deptid = Convert.ToInt32(ddlDepartmentName.Text);

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objCIC.AddItemOrder(objComplaintItemorder);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {                       
                      //  ViewState["action"] = null;
                        Clear();
                    }
                    else
                        lblStatus.Text = "Error";
                }             
            }
            BindListViewItemOrder(Convert.ToInt32(ddlDepartmentName.Text));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ComplaintType.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillItemName(int deptid)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("P_DEPTID", deptid);

            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_DDW_ITEMMASTER", objParams);

            ddlItemName.DataSource = ds;
            ddlItemName.DataValueField = ds.Tables[0].Columns["ITEMID"].ToString();
            ddlItemName.DataTextField = ds.Tables[0].Columns["ITEMNAME"].ToString();
            ddlItemName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "complaint_itemorder.FillItemName-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void PopulateDepartment()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_DEPARTMENT");
            ddlDepartmentName.DataSource = ds;
            ddlDepartmentName.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDepartmentName.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDepartmentName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpComplaintType_PreRender(object sender, EventArgs e)
    {
        ComplaintController objCC = new ComplaintController();
        BindListViewItemOrder(objCC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())));
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