//==================================================================
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 15-01-2015
// DESCRIPTION   : FITNESS TEST DATE SHOULD BE GREATER THAN TO DATE
//==================================================================
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


public partial class VEHICLE_MAINTENANCE_Transaction_Fitness : System.Web.UI.Page
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
                    BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                    BindExpiryList();
                }
                hdnDate.Value = System.DateTime.Now.Date.ToString("dd/MM/yyyy");               
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Fitness.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindExpiryList()
    {
        try
        {
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                DataSet dsCollegeVehicleExpiry = objCommon.FillDropDown("VEHICLE_FITNESS F INNER JOIN VEHICLE_MASTER M ON(M.VIDNO=F.VIDNO)", " F.FITNO,F.TDAT", "M.VIDNO,REGNO +':'+NAME AS MODEL", "convert(date,TDAT) between GETDATE() and DATEADD(day,15,GETDATE()) AND F.VEHICLE_CATEGORY='C' ", "");
                if (dsCollegeVehicleExpiry.Tables[0].Rows.Count > 0)
                {
                    lvFitnessExpire.Visible = true;
                    lvFitnessExpire.DataSource = dsCollegeVehicleExpiry;
                    lvFitnessExpire.DataBind();
                }

                else
                {
                    lvFitnessExpire.Visible = false;
                }
            }
            else
            {
                DataSet dsHireVehicleExpiry = objCommon.FillDropDown("VEHICLE_FITNESS F INNER JOIN VEHICLE_HIRE_MASTER M ON (M.VEHICLE_ID=F.VIDNO)", " F.FITNO,F.TDAT", "M.VEHICLE_ID,M.VEHICLE_NAME AS MODEL", "convert(date,TDAT) between GETDATE() and DATEADD(day,15,GETDATE()) AND F.VEHICLE_CATEGORY='H'", "");

                if (dsHireVehicleExpiry != null)
                {
                    if (dsHireVehicleExpiry.Tables[0].Rows.Count > 0)
                    {
                        lvFitnessExpire.Visible = true;
                        lvFitnessExpire.DataSource = dsHireVehicleExpiry;
                        lvFitnessExpire.DataBind();
                    }

                    else
                    {
                        lvFitnessExpire.Visible = false;

                    }
                }
                else
                {
                    lvFitnessExpire.Visible = false;
                }
                
                

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

    private void BindList(int vehicle_cat)
    {
        try
        {
            DataSet ds = objVMC.GetFitnessAll(vehicle_cat);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFitness.DataSource = ds;
                lvFitness.DataBind();
            }
            else
            {
                lvFitness.DataSource = null;
                lvFitness.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Fitness.BindList -> " + ex.Message + " " + ex.StackTrace);
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
        objCommon.FillDropDownList(ddl, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "VIDNO");
    }
    private void FillDropDownHire()
    {
        objCommon.FillDropDownList(ddl, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0 AND ACTIVE_STATUS=1", "VEHICLE_ID");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int no = int.Parse(btnDelete.CommandArgument);
        objVM.FID= Convert.ToInt32(no);
        CustomStatus CS = (CustomStatus)objVMC.DeleteFitnessByFID(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
        }
    }
    private void clear()
    {
        ddl.SelectedIndex = 0;
        txtFitNo.Text = string.Empty;
        txtFrmDt.Text = string.Empty;
        txtFtDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        ViewState["IDNO"] = null;
        ViewState["action"] = "add";
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int no = int.Parse(imgBtn.CommandArgument);
        ViewState["IDNO"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(no);

    }
    private void ShowDetails(int NO)
    {
        try
        {
            objVM.FID= Convert.ToInt32(NO);
            DataSet ds = objVMC.GetFitnessByFId(objVM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFitNo.Text = ds.Tables[0].Rows[0]["FITNO"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["FDATE"].ToString();
                txtFtDt.Text = ds.Tables[0].Rows[0]["ENTRYDT"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TDAT"].ToString();
                ddl.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                if (ds.Tables[0].Rows[0]["VEHICLE_CATEGORY"].ToString() == Convert.ToString('C'))
                {
                    rdblistVehicleType.SelectedValue = Convert.ToString(1);
                }
                else
                {
                    rdblistVehicleType.SelectedValue = Convert.ToString(2);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFrmDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Should Not Be Greater Than to Date.');", true);
                    txtFrmDt.Focus();
                    return;
                }
            }
            //if (!txtFtDt.Text.Equals(string.Empty))
            //{
            //    if (DateTime.Compare(Convert.ToDateTime(txtToDt.Text), Convert.ToDateTime(txtFtDt.Text)) == 1)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('To Date Can Not Be Greater Than Fitness Test Date.');", true);
            //        txtToDt.Focus();
            //        return;
            //    }
            //}

            if (ddl.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Vehicle", this.Page);
                return;
            }
            objVM.FITNO= txtFitNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtFitNo.Text.Trim());
            objVM.FENTRYDT= Convert.ToDateTime(txtFtDt.Text);
            objVM.FDATE= Convert.ToDateTime(txtFrmDt.Text);
            objVM.TDATE= Convert.ToDateTime(txtToDt.Text);
            objVM.VIDNO = Convert.ToInt32(ddl.SelectedValue);
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                objVM.VEHICLECAT = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLECAT = Convert.ToString('H');
            }
            if (Convert.ToDateTime(txtToDt.Text) > Convert.ToDateTime(txtFtDt.Text))
            {
                objCommon.DisplayMessage(this.Page, "Fitness Test Date Should Be Greater Than To Date.", this.Page);
                return;
            }
          

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //--======start===Shaikh Juned 26-08-2022

                    DataSet ds = objCommon.FillDropDown("vehicle_fitness", "FITNO", "VEHICLE_CATEGORY", "FITNO='" + Convert.ToString(txtFitNo.Text) + "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string FITNO = dr["FITNO"].ToString();
                            // string ORDNO = dr["ORDNO"].ToString();
                            if (FITNO == txtFitNo.Text)
                            {
                                objCommon.DisplayMessage(this.Page, "Fitness No Is Already Exist.", this.Page);
                                return;
                            }


                        }
                    }
                    //---========end=====

                    CustomStatus cs = (CustomStatus)objVMC.AddFitness(objVM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        clear();                      
                        BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                        BindExpiryList();
                        objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["IDNO"] != null)
                    {

                        objVM.FID= Convert.ToInt32(ViewState["IDNO"].ToString());

                        //--======start===Shaikh Juned 26-08-2022

                        DataSet ds = objCommon.FillDropDown("vehicle_fitness", "FITNO", "VEHICLE_CATEGORY", "FITNO='" + Convert.ToString(txtFitNo.Text) + "' AND FID!='" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string FITNO = dr["FITNO"].ToString();
                                // string ORDNO = dr["ORDNO"].ToString();
                                if (FITNO == txtFitNo.Text)
                                {
                                    objCommon.DisplayMessage(this.Page, "Fitness No Is Already Exist.", this.Page);
                                    return;
                                }


                            }
                        }
                        //---========end=====
                        CustomStatus cs = (CustomStatus)objVMC.UpdFitness(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();    
                            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                            BindExpiryList();
                            objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Fitness.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
             ShowReport("pdf", "VehicleFitnessExpiry.rpt");
    }
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=VehicleFitnessExpiry" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

            url += "&param=@P_EXPIRY_FOR=VEHICLE_FITNESS,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value + ",@P_VEHICLE_TYPE=" + rdblistVehicleType.SelectedValue;
            
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
        {
            BindExpiryList();
            FillDropDown();
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            clear();
            ViewState["action"] = "add";
        }
        else
        {
            BindExpiryList();
            FillDropDownHire();
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
            clear();
            ViewState["action"] = "add";
        }
    }

    
}
