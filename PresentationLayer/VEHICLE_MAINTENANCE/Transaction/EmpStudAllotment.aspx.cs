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
using System.Collections.Generic;


public partial class VEHICLE_MAINTENANCE_Transaction_EmpStudAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
    int selectedby = 0;
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
                    Session["UserType"] = rdbUserType.SelectedValue;

                    objVM.IPADDRESS = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (objVM.IPADDRESS == null || objVM.IPADDRESS == "")
                        objVM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    objVM.MACADDRESS = nics[0].GetPhysicalAddress().ToString();

                    FillDropDown();
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
        objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME", "ROUTEID > 0", "ROUTEID");

        //DataSet ds = null;
        //ds = objVMC.GetAllottedVehicles();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlVehicle.DataSource = ds.Tables[0];
        //    ddlVehicle.DataTextField = "VEHICLE_NAME";
        //    ddlVehicle.DataValueField = "RAID";
        //    ddlVehicle.DataBind();
        //}
    }



    protected void rdbUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["UserType"] = rdbUserType.SelectedValue;
            txtEmployee.Text = string.Empty;


            if (rdbUserType.SelectedValue == "1")
            {
                divDegree.Visible = false;
                divBranch.Visible = false;
                divSem.Visible = false;
                divStud.Visible = false;
                divEmp.Visible = true;
               
                BindlistView(rdbUserType.SelectedValue);
            }
            else
            {
                divDegree.Visible = true;
                divSem.Visible = true;
                divStud.Visible = true;
                divEmp.Visible = false;
                BindlistView(rdbUserType.SelectedValue);
            }

            ddlEmployee.SelectedIndex = 0;
            ddlRoute.SelectedIndex = 0;
            ddlBoardingPoint.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
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
            objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S INNER JOIN VEHICLE_TRANSPORT_REQUISITION_APPLICATION A ON (S.IDNO = A.STUD_IDNO)", "S.IDNO", "S.STUDNAME", "S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND S.TRANSPORT=1 AND A.SECOND_APPROVE_STATUS='A'", "S.STUDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private void BindlistView(string USER_TYPE)
    {
        try
        {
            DataSet ds = null;
            if (USER_TYPE == "1")
            {
                lblSearch.Text = "Search Employee";
                txtEmployee.ToolTip = "Enter Employee Code To Search";
                lblStdSelectionBy.Text = "Employee Selection By";
                ddlorderby.SelectedValue = "0";
                divsearchby.Visible = true;
                ddlorderby.Items.FindByValue("2").Enabled = false;
                ddlorderby.Items.FindByValue("3").Enabled = false;
                ddlorderby.Items.FindByValue("4").Enabled = false;
                ds = objVMC.GetRouteAllotmentData(USER_TYPE);
            }
            else
            {
                lblSearch.Text = "Search Student";
                txtEmployee.ToolTip = "Enter Enroll No. To Search";
                lblStdSelectionBy.Text = "Student Selection By";
                divsearchby.Visible = true;
                ddlorderby.Items.FindByValue("1").Enabled = true;
                ddlorderby.Items.FindByValue("2").Enabled = true;
                ddlorderby.Items.FindByValue("3").Enabled = true;
                ddlorderby.Items.FindByValue("4").Enabled = true;
                ddlorderby.Items.FindByValue("5").Enabled = false;
                ds = objVMC.GetRouteAllotmentData(USER_TYPE);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAllotment.Visible = true;
                lvAllotment.DataSource = ds;
                lvAllotment.DataBind();
                if (lvAllotment.Items.Count > 0)
                {
                    Label lbl = this.lvAllotment.Controls[0].FindControl("lblUserName") as Label;
                    Label lblNo = this.lvAllotment.Controls[0].FindControl("lblNumber") as Label;
                    if (rdbUserType.SelectedValue == "1")
                    {
                        lbl.Text = "EMPLOYEE NAME";
                        lblNo.Text = "PFILENO";
                    }
                    else
                    {
                        lbl.Text = "STUDENT NAME";
                        lblNo.Text = "ENROLLNO";
                    }
                }
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objVM.URID = 0;
            if (rdbUserType.SelectedValue == "1")
            {
                objVM.USER_TYPE = 'E'; // E for Employee and S for Student
                objVM.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                objVM.USER_TYPE = 'S';
                objVM.IDNO = Convert.ToInt32(ddlStudent.SelectedValue);
            }

            objVM.ROUTEID = Convert.ToInt32(ddlRoute.SelectedValue);
            objVM.STOPNO = Convert.ToInt32(ddlBoardingPoint.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateUserRouteAllotment(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        DisplayMessage("Record Already Exist.");
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        DisplayMessage("Record Saved Successfully.");
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
                            DisplayMessage("Record Updated Successfully.");
                            Clear();
                        }
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            DisplayMessage("Record Already Exist.");
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


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }


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

            DataSet ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT", "URID, USER_TYPE", "IDNO, ROUTEID, STOPNO", "URID=" + URNO, "");
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

                ddlRoute.SelectedValue = ds.Tables[0].Rows[0]["ROUTEID"].ToString();

                DataSet dsBP = objVMC.GetBoardingPointsByRouteID(Convert.ToInt32(ddlRoute.SelectedValue));
                if (dsBP.Tables[0].Rows.Count > 0)
                {
                    ddlBoardingPoint.DataSource = dsBP.Tables[0];
                    ddlBoardingPoint.DataTextField = "STOPNAME";
                    ddlBoardingPoint.DataValueField = "STOPNO";
                    ddlBoardingPoint.DataBind();
                    ddlBoardingPoint.Items.Insert(0, new ListItem("Please Select", "0"));
                    divBP.Visible = true;
                    ddlBoardingPoint.SelectedValue = ds.Tables[0].Rows[0]["STOPNO"].ToString();
                }

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
        ddlRoute.SelectedIndex = 0;
        ViewState["URNO"] = null;
        ViewState["action"] = "add";
        trRouteDrop.Visible = true;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlStudent.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        ddlBoardingPoint.Items.Clear();
        txtEmployee.Text = string.Empty;
        divCanRem.Visible = false;
        txtCanRemark.Text = string.Empty;
        ddlorderby.SelectedValue = "0";
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
        if (rdbUserType.SelectedValue == "1")
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



    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objVMC.GetBoardingPointsByRouteID(Convert.ToInt32(ddlRoute.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                divBP.Visible = true;
                ddlBoardingPoint.Items.Clear();

                ddlBoardingPoint.DataSource = ds.Tables[0];
                ddlBoardingPoint.DataTextField = "STOPNAME";
                ddlBoardingPoint.DataValueField = "STOPNO";
                ddlBoardingPoint.DataBind();
                ddlBoardingPoint.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlVehicle_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtEmployee_TextChanged(object sender, EventArgs e)
    {
        try
        {
            // for Student
            if (rdbUserType.SelectedValue == "2")
            {
                string StudIdNo = hfIdNo.Value;
                DataSet ds = null;
                DataSet dsCheck = null;

                if (StudIdNo != "")
                {
                    string year = objCommon.LookUp("ACD_STUDENT", "[YEAR]", "TRANSPORT=1 AND IDNO=" + hfIdNo.Value);


                    if (Session["OrgId"].ToString() == "2")
                    {
                        dsCheck = objCommon.FillDropDown("ACD_DCR", "IDNO", "NAME", "IDNO=" + hfIdNo.Value + " AND SEMESTERNO IN (SELECT SEMESTERNO FROM ACD_SEMESTER WHERE YEARNO = " + year + ") AND RECIEPT_CODE='BFR' AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0", "");
                    }
                    else
                    {
                        dsCheck = objCommon.FillDropDown("ACD_DCR", "IDNO", "NAME", "IDNO=" + hfIdNo.Value + " AND SEMESTERNO IN (SELECT SEMESTERNO FROM ACD_SEMESTER WHERE YEARNO = " + year + ") AND RECIEPT_CODE='TPF' AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0", "");
                    }
                    if (dsCheck.Tables[0].Rows.Count > 0)
                    {

                        ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO,DEGREENO", "BRANCHNO,SEMESTERNO", "TRANSPORT=1 AND IDNO=" + hfIdNo.Value, "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND BRANCHNO IN (SELECT BRANCHNO FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + ")", "BRANCHNO");
                            ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            if (ddlBranch.SelectedIndex > 0)
                            {
                                divBranch.Visible = true;
                            }

                            ddlSem.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT", "IDNO", "STUDNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND TRANSPORT=1", "STUDNAME");
                            ddlStudent.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
                        }
                    }
                    else
                    {
                        DisplayMessage("Transport fees is not paid by selected student.");
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Please select student from list.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlVehicle_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    // this method is used to find the file name.

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetEmployeeName(string prefixText, string contextKey)
  {
        List<string> EmployeeName = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            VMController objVMCon = new VMController();
           // int studSelectionby = Convert.ToInt32(ddlorderby.SelectedValue);
            ds = objVMCon.FillEmployeeName(prefixText, contextKey);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[0]["Type"].ToString() == "E")
                    EmployeeName.Add(ds.Tables[0].Rows[i]["IDNO"].ToString() + "---------*" + ds.Tables[0].Rows[i]["NAME"].ToString());
                if (ds.Tables[0].Rows[0]["Type"].ToString() == "S")
                   EmployeeName.Add(ds.Tables[0].Rows[i]["IDNO"].ToString() + "---------*" + ds.Tables[0].Rows[i]["NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return EmployeeName;
    }

    protected void btnCancelAllot_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdbUserType.SelectedValue == "1")  // Employee
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    divCanRem.Visible = true;
                }
                else
                {
                    DisplayMessage("Please select employee from list.");
                    return;
                }
            }
            else   // Student
            {
                if (ddlStudent.SelectedIndex > 0)
                {
                    divCanRem.Visible = true;
                }
                else
                {
                    DisplayMessage("Please select student from list.");
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudAllotment.btnCancelAllot_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnSaveCRemark_Click(object sender, EventArgs e)
    {
        try
        {
            if (divCanRem.Visible == true)  // Employee
            {
                if (txtCanRemark.Text != string.Empty)
                {
                    objVM.URID = Convert.ToInt32(ViewState["URID"]);
                    if (rdbUserType.SelectedValue == "1")
                    {
                        objVM.USER_TYPE = 'E'; 
                        objVM.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
                    }
                    else
                    {
                        objVM.USER_TYPE = 'S';
                        objVM.IDNO = Convert.ToInt32(ddlStudent.SelectedValue);
                    }

                    objVM.CANCEL_REMARK = txtCanRemark.Text;

                    CustomStatus cs = (CustomStatus)objVMC.AddCancelAllotment(objVM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();                        
                        DisplayMessage("Record Saved Successfully.");
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Please enter remark to cancel allotment.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudAllotment.btnSaveCRemark_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCAReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("CancelAllotmentReport", "rptCancelAllotment.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudAllotment.btnSaveCRemark_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
   
 
}