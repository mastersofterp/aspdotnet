//================================
//CREATED BY : GOPAL ANTHATI
//CREATED DATE : 22-03-2021
//================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Transaction_AmbulanceMaintanance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM objVM = new VM();
    VMController objVMC = new VMController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";                    
                    BindListView();
                }


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_AmbulanceMaintanance.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_AMBULANCE_MAINTENANCE", "*", "", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAmbulance.DataSource = ds;
                lvAmbulance.DataBind();
            }
            else
            {
                lvAmbulance.DataSource = null;
                lvAmbulance.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.BindList -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
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
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {        
        ViewState["action"] = "add";
        txtDate.Text = string.Empty;
        txtBillNo.Text = string.Empty;
        txtSupplierName.Text = string.Empty;
        txtDiscription.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtCGST.Text = string.Empty;
        txtSGST.Text = string.Empty;
        txtTotalAmount.Text = string.Empty;
        txtIncharge.Text = string.Empty;
        ViewState["AM_ID"] = null;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int AM_ID = int.Parse(imgBtn.CommandArgument);
        ViewState["AM_ID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(AM_ID);
    }

    private void ShowDetails(int AM_ID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_AMBULANCE_MAINTENANCE", "*", "", "AM_ID =" + Convert.ToInt32(AM_ID), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                txtBillNo.Text = ds.Tables[0].Rows[0]["BILLNO"].ToString();
                txtSupplierName.Text = ds.Tables[0].Rows[0]["SUPPLIERS_NAME"].ToString();
                txtDiscription.Text = ds.Tables[0].Rows[0]["DISCRIPTION_NAME"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["RATE"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                txtCGST.Text = ds.Tables[0].Rows[0]["CGST"].ToString();
                txtSGST.Text = ds.Tables[0].Rows[0]["SGST"].ToString();
                txtTotalAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                txtIncharge.Text = ds.Tables[0].Rows[0]["INCHARGE"].ToString();
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_AmbulanceMaintanance.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.DATE = Convert.ToDateTime(txtDate.Text);
            objVM.BILLNO = txtBillNo.Text;
            objVM.SUPPLIERS_NAME = txtSupplierName.Text;
            objVM.DISCRIPTION_NAME = txtDiscription.Text;
            objVM.RATE = Convert.ToDecimal(txtRate.Text);
            objVM.AMOUNT = Convert.ToDouble(txtAmount.Text);
            objVM.TOTAL_AMOUNT = Convert.ToDouble(txtTotalAmount.Text);
            objVM.INCHARGE = txtIncharge.Text;
            objVM.CGST = Convert.ToDouble(txtCGST.Text);
            objVM.SGST = Convert.ToDouble(txtSGST.Text);


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objVM.AM_ID = 0;
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdAmbulanceMaintanance(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        clear();
                        objCommon.DisplayMessage("Bill No. Already Exist.", this.Page);
                        return;
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["AM_ID"] != null)
                    {
                        objVM.AM_ID = Convert.ToInt32(ViewState["AM_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdAmbulanceMaintanance(objVM);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            clear();
                            objCommon.DisplayMessage("Bill No. Already Exist.", this.Page);
                            return;
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                        }
                    }
                }
                BindListView();
                clear();
                ViewState["action"] = "add";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_AmbulanceMaintanance.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }  
}