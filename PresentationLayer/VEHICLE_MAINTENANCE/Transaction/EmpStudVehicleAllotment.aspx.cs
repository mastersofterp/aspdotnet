//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 10-JUN-2019
// DESCRIPTION   : USE TO ALLOT VEHICLES TO EMPLOYEE AND STUDENT
//=========================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment : System.Web.UI.Page
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
                    ViewState["action"] = "add";

                    objVM.IPADDRESS = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (objVM.IPADDRESS == null || objVM.IPADDRESS == "")
                        objVM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    objVM.MACADDRESS = nics[0].GetPhysicalAddress().ToString();

                    FillDropDown();
                    trRoute.Visible = false;
                    trRouteDrop.Visible = true;
                    BindlistView(rdbUserType.SelectedValue);

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to check the page authority.
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

    // This method is used to fill drop down values.
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ ISNULL(FNAME,' ')+' '+ ISNULL(MNAME,' ')+' '+ ISNULL(LNAME,' ') as NAME", "IsBusFas=1", "FNAME");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO > 0", "SEMESTERNO");
        //  objCommon.FillDropDownList(ddlVehicle, "VEHICLE_ROUTEALLOTMENT", "RAID", "ROUTENAME+' :-> '+ROUTEPATH AS ROUTENAME", "", "ROUTEID");
      //  objCommon.FillDropDownList(ddlBoardingPoint, "VEHICLE_STOPMASTER", "STOPID", "STOPNAME", "", "STOPNAME");
        DataSet ds = null;
        ds = objVMC.GetAllottedVehicles();
        if (ds.Tables[0].Rows.Count > 0)
        {           
            ddlVehicle.DataSource = ds.Tables[0];
            ddlVehicle.DataTextField = "VEHICLE_NAME";
            ddlVehicle.DataValueField = "RAID";
            ddlVehicle.DataBind();
        }
    }



    protected void rdbUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Label lbl = this.lvAllotment.Controls[0].FindControl("lblUserName") as Label;
            if (rdbUserType.SelectedItem.Text.Equals("Employee"))
            {
                divDegree.Visible = false;
                divBranch.Visible = false;
                divSem.Visible = false;
                divStud.Visible = false;
                divEmp.Visible = true;
                BindlistView(rdbUserType.SelectedValue);               
                lbl.Text = "EMPLOYEE NAME";

            }
            else
            {
                divDegree.Visible = true;
                divSem.Visible = true;
                divStud.Visible = true;
                divEmp.Visible = false;
                BindlistView(rdbUserType.SelectedValue);
                lbl.Text = "STUDENT NAME";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.rdbUserType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND BRANCHNO IN (SELECT BRANCHNO FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + ")", "BRANCHNO");
            divBranch.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT", "IDNO", "STUDNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND TRANSPORT=1", "STUDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    // This method is used to bind the list of vahicle allotment.
    private void BindlistView(string USER_TYPE)
    {
        try
        {
            DataSet ds = null;
            if (USER_TYPE == "1")
            {
                ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT UR  INNER JOIN VEHICLE_ROUTEALLOTMENT RL ON (UR.RAID = RL.RAID) INNER JOIN VEHICLE_ROUTEMASTER R ON (RL.ROUTEID = R.ROUTEID) INNER JOIN PAYROLL_EMPMAS E ON (UR.IDNO = E.IDNO) LEFT JOIN VEHICLE_MASTER VM ON (RL.VIDNO = VM.VIDNO AND VEHICLE_CATEGORY='C') LEFT JOIN VEHICLE_HIRE_MASTER HM ON (RL.VIDNO = HM.VEHICLE_ID AND VEHICLE_CATEGORY='H')", "UR.URID, E.FNAME AS NAME", "R.ROUTENAME+' -> '+R.ROUTEPATH as ROUTENAME, ISNULL(VM.NAME, HM.VEHICLE_NAME) as VNAME", "UR.USER_TYPE ='E'", "UR.URID DESC");
            }
            else
            {
                ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT UR  INNER JOIN VEHICLE_ROUTEALLOTMENT RL ON (UR.RAID = RL.RAID) INNER JOIN VEHICLE_ROUTEMASTER R ON (RL.ROUTEID = R.ROUTEID) INNER JOIN ACD_STUDENT S ON (UR.IDNO = S.IDNO) LEFT JOIN VEHICLE_MASTER VM ON (RL.VIDNO = VM.VIDNO AND VEHICLE_CATEGORY='C') LEFT JOIN VEHICLE_HIRE_MASTER HM ON (RL.VIDNO = HM.VEHICLE_ID AND VEHICLE_CATEGORY='C')", "UR.URID, S.STUDNAME AS NAME", "R.ROUTENAME+' -> '+R.ROUTEPATH as ROUTENAME, ISNULL(VM.NAME, HM.VEHICLE_NAME) as VNAME", "UR.USER_TYPE ='S'", "UR.URID DESC");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAllotment.Visible = true;
                lvAllotment.DataSource = ds;
                lvAllotment.DataBind();
            }
            else
            {
                lvAllotment.DataSource = null;
                lvAllotment.DataBind();
                lvAllotment.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //This action button save the vehicle-driver-route allotment.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.URID = 0;
            if (rdbUserType.SelectedItem.Text.Equals("Employee"))
            {
                objVM.USER_TYPE = 'E'; // E for Employee and S for Student
                objVM.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                objVM.USER_TYPE = 'S';
                objVM.IDNO = Convert.ToInt32(ddlStudent.SelectedValue);
            }



            objVM.RANO = Convert.ToInt32(ddlVehicle.SelectedValue);

            objVM.ROUTENAME = txtRoute.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRoute.Text.Trim());

            objVM.STOPNO = Convert.ToInt32(ddlBoardingPoint.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateUserRouteAllotment(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        DisplayMessage("Record Already Exist.");
                        //objCommon.DisplayMessage("Record Already Exist.", this.Page);
                        Clear();
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        DisplayMessage("Record Save Successfully.");
                        //objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                        BindlistView(rdbUserType.SelectedValue);
                        ViewState["action"] = "add";
                        Clear();

                    }
                }
                else
                {
                    if (ViewState["URID"] != null)
                    {
                        objVM.URID = Convert.ToInt32(ViewState["URID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateUserRouteAllotment(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView(rdbUserType.SelectedValue);
                            ViewState["action"] = "add";
                            DisplayMessage("Record Update Successfully.");
                            // objCommon.DisplayMessage("Record Update Successfully.", this.Page);
                            Clear();
                        }
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Clear();
                            DisplayMessage("Record Already Exist.");
                            // objCommon.DisplayMessage("Record Already Exist.", this.Page);
                            return;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This action button is use to clear the last selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //This action button is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int URNO = int.Parse(btnEdit.CommandArgument);
            ViewState["URID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(URNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This action button is used to delete the unwanted record.
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int RANO = int.Parse(btnDelete.CommandArgument);
            ViewState["URID"] = int.Parse(btnDelete.CommandArgument);

            objVM.RANO = int.Parse(btnDelete.CommandArgument);
            CustomStatus CS = (CustomStatus)objVMC.DeleteVehicleRouteAllotment(objVM);
            if (CS.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record Deleted Successfully.", this.Page);
                BindlistView(rdbUserType.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This method is used to show the details of the selected record.
    private void ShowDetails(int URNO)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT UR INNER JOIN VEHICLE_ROUTEALLOTMENT RA ON (UR.RAID = RA.RAID)", "UR.URID, UR.USER_TYPE", "UR.IDNO, RA.RAID, UR.STOPNO", "UR.URID=" + URNO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                // string RouteNo = ds.Tables[0].Rows[0]["ROUTEID"].ToString();

                if (ds.Tables[0].Rows[0]["USER_TYPE"].ToString() == "E")
                {
                    rdbUserType.SelectedValue = "1";
                    divDegree.Visible = false;
                    divBranch.Visible = false;
                    divSem.Visible = false;
                    divStud.Visible = false;
                    divEmp.Visible = true;
                    ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
                }
                else
                {
                    rdbUserType.SelectedValue = "2";
                    divDegree.Visible = true;
                    divSem.Visible = true;
                    divStud.Visible = true;
                    divEmp.Visible = false;

                    if (rdbUserType.SelectedValue == "2")
                    {
                        DataSet dsStud = null;
                        dsStud = objCommon.FillDropDown("ACD_STUDENT", "BRANCHNO, SEMESTERNO", "DEGREENO, IDNO", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()), "");
                        ddlDegree.SelectedValue = dsStud.Tables[0].Rows[0]["DEGREENO"].ToString();
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND BRANCHNO IN (SELECT BRANCHNO FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + ")", "BRANCHNO");
                        ddlBranch.SelectedValue = dsStud.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        if (ddlBranch.SelectedIndex > 0)
                        {
                            divBranch.Visible = true;
                        }
                        ddlSem.SelectedValue = dsStud.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                        objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT", "IDNO", "STUDNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND TRANSPORT=1", "STUDNAME");
                        ddlStudent.SelectedValue = dsStud.Tables[0].Rows[0]["IDNO"].ToString();
                    }
                }

                ddlVehicle.SelectedValue = ds.Tables[0].Rows[0]["RAID"].ToString();
                ddlBoardingPoint.SelectedValue = ds.Tables[0].Rows[0]["STOPNO"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to clear all the controls.
    private void Clear()
    {
        ddlVehicle.SelectedIndex = 0;
        ViewState["URNO"] = null;
        ViewState["action"] = "add";
        trRoute.Visible = false;
        trRouteDrop.Visible = true;
        txtRoute.Text = string.Empty;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlStudent.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        ddlBoardingPoint.SelectedIndex = 0;

    }

    // This method is use to generate the list of Vehicle-Driver-Route allotment.
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=" + exporttype + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_URID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // This action button is used to generate the allotment report.
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("VehicleUserAllotment", "rptVehicleUserAllotment.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This action buttons give the specific type of vehicles.
    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbUserType.SelectedItem.Text.Equals("Employee"))
        {
            FillDropDown();
            Clear();
            ViewState["action"] = "add";
            BindlistView(rdbUserType.SelectedValue);

        }
        else
        {
            Clear();
            ViewState["action"] = "add";
            BindlistView(rdbUserType.SelectedValue);

        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT", "IDNO", "STUDNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND TRANSPORT=1", "STUDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindlistView(rdbUserType.SelectedValue);
    }

    protected void btnStudReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("VehicleStudentAllotment", "rptVehicleStudentAllotment.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.btnStudReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    

    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //DataSet ds = objVMC.GetBoardingPointsByVehicleID(Convert.ToInt32(ddlVehicle.SelectedValue));
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    divBP.Visible = true;
            //    ddlBoardingPoint.Items.Clear();

            //    ddlBoardingPoint.Items.Add("Please Select");
            //    ddlBoardingPoint.DataSource = ds.Tables[0];
            //    ddlBoardingPoint.DataTextField = "STOPNAME";
            //    ddlBoardingPoint.DataValueField = "STOPNO";
            //    ddlBoardingPoint.DataBind();
            //}
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlVehicle_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}