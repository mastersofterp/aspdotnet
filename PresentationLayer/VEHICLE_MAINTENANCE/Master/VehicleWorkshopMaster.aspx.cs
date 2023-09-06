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

public partial class VEHICLE_MAINTENANCE_Master_VehicleWorkshopMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();

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
                
                    BindlistView();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objVM.WORKSHOP = txtWorkshopName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtWorkshopName.Text.Trim());
            objVM.WADD1 = txtWorkshopAddress.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtWorkshopAddress.Text);
            objVM.WADD2 = string.Empty;
            objVM.WPHONE = txtWorkshopContactNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtWorkshopContactNo.Text.Trim());
            objVM.WPERSONNAME = txtWorkshopContactPerson.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtWorkshopContactPerson.Text.Trim());
            objVM.COLLEGE_CODE = Convert.ToInt32(Session["colcode"]);

           

            if (ViewState["WORKSHOP_ID"] == null)
            {
                //--======start===Shaikh Juned 22-08-2022
                //DataSet ds = objCommon.FillDropDown("VEHICLE_WORKSHOP", "PHONE", "PERSONNAME", "PHONE=" + txtWorkshopContactNo.Text  , "");//txtWorkshopContactNo.Text

                DataSet ds = objCommon.FillDropDown("VEHICLE_WORKSHOP", "PHONE", "PERSONNAME", "PHONE='" + txtWorkshopContactNo.Text + "'", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string phone = dr["PHONE"].ToString();
                        if (phone == txtWorkshopContactNo.Text)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Contact No Is Already Exist.", this.Page);
                            return;
                        }

                    }
                }


                //---========end=====
                CustomStatus cs = (CustomStatus)objVMC.AddUpdateWorkshopMaster(objVM);
                if (cs.Equals(CustomStatus.RecordExist))
                {

                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                Clear();
                BindlistView();
                //  ViewState["SUPPILER_ID"] = null;

   
            }
            else
            {
                //--======start===Shaikh Juned 22-08-2022
                //DataSet ds = objCommon.FillDropDown("VEHICLE_WORKSHOP", "PHONE", "PERSONNAME", "PHONE=" + txtWorkshopContactNo.Text  , "");//txtWorkshopContactNo.Text

                DataSet ds = objCommon.FillDropDown("VEHICLE_WORKSHOP", "PHONE", "PERSONNAME", "PHONE='" + txtWorkshopContactNo.Text + "' and WSNO!='" + Convert.ToInt32(ViewState["WORKSHOP_ID"].ToString()) + "'", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string phone = dr["PHONE"].ToString();
                        if (phone == txtWorkshopContactNo.Text)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Contact No Is Already Exist.", this.Page);
                            return;
                        }

                    }
                }


                //---========end=====

                objVM.WSNO = Convert.ToInt32(ViewState["WORKSHOP_ID"].ToString());

                objVMC.AddUpdateWorkshopMaster(objVM);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                Clear();
                BindlistView();
                ViewState["WORKSHOP_ID"] = null;
            


            }



        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int workshop_id = int.Parse(btnEdit.CommandArgument);
            ViewState["WORKSHOP_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(workshop_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_WORKSHOP", "*", "", "", "WSNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvWorkshop.DataSource = ds;
                lvWorkshop.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int workshop_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_WORKSHOP", "*", "", "WSNO=" + workshop_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtWorkshopName.Text = ds.Tables[0].Rows[0]["WORKSHOP_NAME"].ToString();
                txtWorkshopAddress.Text = ds.Tables[0].Rows[0]["ADD1"].ToString();
                txtWorkshopContactNo.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                txtWorkshopContactPerson.Text = ds.Tables[0].Rows[0]["PERSONNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtWorkshopName.Text = string.Empty;
        txtWorkshopAddress.Text = string.Empty;
        txtWorkshopContactNo.Text = string.Empty;
        txtWorkshopContactPerson.Text = string.Empty;
        ViewState["WORKSHOP_ID"] = null;
    }
}
