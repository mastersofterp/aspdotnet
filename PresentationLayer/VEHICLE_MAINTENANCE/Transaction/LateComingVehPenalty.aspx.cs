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

public partial class VEHICLE_MAINTENANCE_Transaction_LateComingVehPenalty : System.Web.UI.Page
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
                    FillDropDown();
                    BindListView();
                }


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_LATE_COME_PENALTY A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID = B.COLLEGE_ID)INNER JOIN VEHICLE_HIRE_MASTER C ON (A.VEHICLE_ID = C.VEHICLE_ID)", "A.*", "COLLEGE_NAME,VEHICLE_NAME", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPenaltyList.DataSource = ds;
                lvPenaltyList.DataBind();
            }
            else
            {
                lvPenaltyList.DataSource = null;
                lvPenaltyList.DataBind();
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


    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
      
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0 AND ACTIVE_STATUS=1", "VEHICLE_ID");
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    
    private void clear()
    {
        ddlVehicle.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ViewState["action"] = "add";
        txtAcualArrivalTime.Text = string.Empty;
        txtArrivalDate.Text = string.Empty;
        txtArrivalTime.Text = string.Empty;
        txtFineAmount.Text = string.Empty;
        txtLateHours.Text = string.Empty;
        ViewState["PENALTY_ID"] = null;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int penalty_id = int.Parse(imgBtn.CommandArgument);
        ViewState["PENALTY_ID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(penalty_id);
    }

    private void ShowDetails(int penalty_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_LATE_COME_PENALTY", "*", "", "PENALTY_ID =" + Convert.ToInt32(penalty_id), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["VEHICLE_ID"].ToString();
                txtArrivalDate.Text = ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString();
                txtArrivalTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ARRIVAL_TIME"]).ToString("hh:mm tt");
                txtAcualArrivalTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ACTUAL_ARRIVAL_TIME"]).ToString("hh:mm tt");
                txtLateHours.Text = ds.Tables[0].Rows[0]["LATE_HOURS"].ToString();
                txtFineAmount.Text = ds.Tables[0].Rows[0]["FINE_AMOUNT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objVM.VEHICLE_ID = Convert.ToInt32(ddlVehicle.SelectedValue);
            objVM.COLLEGE_ID = Convert.ToInt32(ddlCollege.SelectedValue);
            objVM.ARRIVAL_DATE = Convert.ToDateTime(txtArrivalDate.Text);
            objVM.ARRIVAL_TIME = Convert.ToDateTime(txtArrivalTime.Text);
            objVM.ACTUAL_ARRIVAL_TIME = Convert.ToDateTime(txtAcualArrivalTime.Text);
            objVM.LATE_HOURS = Convert.ToInt32(txtLateHours.Text);
            objVM.FINE_AMOUNT = Convert.ToDouble(txtFineAmount.Text);
           

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objVM.PENALTY_ID = 0;
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdLateComingVehPenalty(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        clear();
                        objCommon.DisplayMessage("Record Already Exist.", this.Page);
                        return;
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully.", this.Page);                                            
                    }
                }
                else
                {
                    if (ViewState["PENALTY_ID"] != null)
                    {
                        objVM.PENALTY_ID = Convert.ToInt32(ViewState["PENALTY_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdLateComingVehPenalty(objVM);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            clear();
                            objCommon.DisplayMessage("Record Already Exist.", this.Page);
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
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }   
}