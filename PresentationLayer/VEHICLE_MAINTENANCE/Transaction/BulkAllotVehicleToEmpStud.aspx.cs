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

public partial class VEHICLE_MAINTENANCE_Transaction_BulkAllotVehicleToEmpStud : System.Web.UI.Page
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
                    BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);

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
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO > 0", "SEMESTERNO");
    }



    protected void rdbUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //for (int i = 0; i < lvAllotment.Items.Count; i++)
            //{
            //    Label lbl = this.lvAllotment.Controls[0].FindControl("lblUserName") as Label;
            //    if (rdbUserType.SelectedValue == "1")
            //    {
            //        lbl.Text = "EMPLOYEE NAME"; 
            //    }
            //    else
            //    {
            //        lbl.Text = "STUDENT NAME";
            //    }

            //}
            
            if (rdbUserType.SelectedValue == "1")
            {
                divDegree.Visible = false;
                divBranch.Visible = false;
                divSem.Visible = false;
                divStud.Visible = false;
                ddlDegree.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                rdbAllotted.SelectedValue = "1";
                BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
               // lbl.Text = "EMPLOYEE NAME";              
                lvAllotment.Visible = true;
            }
            else
            {
                divDegree.Visible = true;
                divSem.Visible = true;
                ddlDegree.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                rdbAllotted.SelectedValue = "1";
                BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
               // lbl.Text = "STUDENT NAME";              
                lvAllotment.Visible = false;               
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
            if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex>0 && ddlSem.SelectedIndex>0)
            {
                divStud.Visible = true;
            }
            else
            {
                DisplayMessage("Please Select Degree, Branch And Semester.");
                return;
            }
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
    private void BindlistView(string USER_TYPE, string ALLOT_TYPE)
    {
        try
        {
            DataSet ds = null;
            if (USER_TYPE == "1") //Employee
            {
                if (ALLOT_TYPE == "1") // Employee Not-Allotted
                {
                    ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ ISNULL(FNAME,' ')+' '+ ISNULL(MNAME,' ')+' '+ ISNULL(LNAME,' ') as NAME", "IsBusFas=1 AND IDNO NOT IN (SELECT IDNO FROM VEHICLE_USER_ROUTEALLOT WHERE USER_TYPE='E')", "FNAME");
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
                else // Employee  Allotted
                {
                    ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT UR INNER JOIN PAYROLL_EMPMAS E ON (UR.IDNO = E.IDNO AND UR.USER_TYPE='E')", "UR.IDNO, UR.STOPNO, UR.ROUTEID", "ISNULL(TITLE,'')+' '+ ISNULL(FNAME,' ')+' '+ ISNULL(MNAME,' ')+' '+ ISNULL(LNAME,' ') as NAME", "ISNULL(UR.CANCEL_STATUS,0) =0", "FNAME");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvAllotment.Visible = true;
                        lvAllotment.DataSource = ds;
                        lvAllotment.DataBind();

                        ViewState["action"] = "edit";
                        for (int i = 0; i < lvAllotment.Items.Count; i++)
                        {
                            DropDownList ddlRoute = lvAllotment.Items[i].FindControl("ddlRoute") as DropDownList;
                            HiddenField hdnID = lvAllotment.Items[i].FindControl("hdnIDNO") as HiddenField;
                            DropDownList ddlBP = lvAllotment.Items[i].FindControl("ddlBoardingPoint") as DropDownList;
                            CheckBox chkRow = lvAllotment.Items[i].FindControl("chkRow") as CheckBox;

                            if (hdnID.Value == ds.Tables[0].Rows[i]["IDNO"].ToString())
                            {
                                chkRow.Checked = true;
                                ddlRoute.SelectedValue = ds.Tables[0].Rows[i]["ROUTEID"].ToString();

                                DataSet dsBP = objVMC.GetBoardingPointsByRouteID(Convert.ToInt32(ddlRoute.SelectedValue));
                                if (dsBP.Tables[0].Rows.Count > 0)
                                {
                                    ddlBP.DataSource = dsBP.Tables[0];
                                    ddlBP.DataTextField = "STOPNAME";
                                    ddlBP.DataValueField = "STOPNO";
                                    ddlBP.DataBind();                                    
                                }
                                ddlBP.SelectedValue = ds.Tables[0].Rows[i]["STOPNO"].ToString();
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
            }
            else //Student
            {
                if (ALLOT_TYPE == "1") // Student Not-Allotted
                {
                    if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0 && ddlSem.SelectedIndex > 0)
                    {
                        //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN VEHICLE_TRANSPORT_REQUISITION_APPLICATION A ON (S.IDNO = A.STUD_IDNO)", "S.IDNO", "S.STUDNAME AS NAME", "S.TRANSPORT=1 AND A.SECOND_APPROVE_STATUS='A' AND S.IDNO NOT IN (SELECT IDNO FROM VEHICLE_USER_ROUTEALLOT WHERE USER_TYPE='S') AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue), "S.IDNO");
                        ds = objCommon.FillDropDown("ACD_STUDENT S", "S.IDNO", "S.STUDNAME AS NAME", "S.TRANSPORT=1 AND S.IDNO NOT IN (SELECT IDNO FROM VEHICLE_USER_ROUTEALLOT WHERE USER_TYPE='S') AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue), "S.IDNO");
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
                            DisplayMessage("Record Not Found.");
                        }
                    }
                    else
                    {
                        DisplayMessage("Please Select Degree, Branch And Semester.");
                        return;
                    }
                }
                else  // Student Allotted
                {
                    if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0 && ddlSem.SelectedIndex > 0)
                    {
                        ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT UR INNER JOIN ACD_STUDENT S ON (UR.IDNO = S.IDNO AND UR.USER_TYPE='S')", "UR.IDNO, UR.STOPNO, UR.ROUTEID", "S.STUDNAME as NAME", "S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) +"AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) +" AND ISNULL(UR.CANCEL_STATUS,0) =0", "S.STUDNAME");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lvAllotment.Visible = true;
                            lvAllotment.DataSource = ds;
                            lvAllotment.DataBind();

                            ViewState["action"] = "edit";
                            for (int i = 0; i < lvAllotment.Items.Count; i++)
                            {
                                DropDownList ddlRoute = lvAllotment.Items[i].FindControl("ddlRoute") as DropDownList;
                                HiddenField hdnID = lvAllotment.Items[i].FindControl("hdnIDNO") as HiddenField;
                                DropDownList ddlBP = lvAllotment.Items[i].FindControl("ddlBoardingPoint") as DropDownList;
                                CheckBox chkRow = lvAllotment.Items[i].FindControl("chkRow") as CheckBox;

                                if (hdnID.Value == ds.Tables[0].Rows[i]["IDNO"].ToString())
                                {
                                    chkRow.Checked = true;
                                    ddlRoute.SelectedValue = ds.Tables[0].Rows[i]["ROUTEID"].ToString();

                                    DataSet dsBP = objVMC.GetBoardingPointsByRouteID(Convert.ToInt32(ddlRoute.SelectedValue));
                                    if (dsBP.Tables[0].Rows.Count > 0)
                                    {
                                        ddlBP.DataSource = dsBP.Tables[0];
                                        ddlBP.DataTextField = "STOPNAME";
                                        ddlBP.DataValueField = "STOPNO";
                                        ddlBP.DataBind();
                                    }
                                    ddlBP.SelectedValue = ds.Tables[0].Rows[i]["STOPNO"].ToString();
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
                    else
                    {
                        DisplayMessage("Please Select Degree, Branch And Semester.");
                        return;
                    }
                }

                for (int i = 0; i < lvAllotment.Items.Count; i++)
                {
                    Label lbl = this.lvAllotment.Controls[0].FindControl("lblUserName") as Label;
                    if (rdbUserType.SelectedValue == "1")
                    {
                        lbl.Text = "EMPLOYEE NAME";
                    }
                    else
                    {
                        lbl.Text = "STUDENT NAME";
                    }

                }
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
           
            DataTable AllotmentTbl = new DataTable("allotTbl");
            AllotmentTbl.Columns.Add("IDNO", typeof(int));
            AllotmentTbl.Columns.Add("ROUTEID", typeof(int));
            AllotmentTbl.Columns.Add("STOPNO", typeof(int));
            AllotmentTbl.Columns.Add("SEMESTERNO", typeof(int));


            DataRow dr = null;
            foreach (ListViewItem i in lvAllotment.Items)
            {
                CheckBox chkRow = i.FindControl("chkRow") as CheckBox;
                DropDownList ddlVeh = i.FindControl("ddlRoute") as DropDownList;
                DropDownList ddlBP = i.FindControl("ddlBoardingPoint") as DropDownList;
                HiddenField hdnID = i.FindControl("hdnIDNO") as HiddenField;

                if (chkRow.Checked)
                {
                    if (ddlVeh.SelectedIndex == 0 || ddlBP.SelectedIndex == 0)
                    {
                        DisplayMessage("Please select route and boarding point.");
                        return;
                    }
                    else
                    {
                        dr = AllotmentTbl.NewRow();
                        dr["IDNO"] = hdnID.Value;
                        dr["ROUTEID"] = ddlVeh.SelectedValue;
                        dr["STOPNO"] = ddlBP.SelectedValue;
                        //dr["SEMESTERNO"] = Convert.ToInt32(null);
                        dr["SEMESTERNO"] = Convert.ToInt32(ddlSem.SelectedValue);

                        AllotmentTbl.Rows.Add(dr);
                    }
                }

            }
            objVM.ALLOTMENT_TABLE = AllotmentTbl;

            if (objVM.ALLOTMENT_TABLE.Rows.Count <= 0)
            {
                DisplayMessage("Please Select Employee.");
                return;
            }


            if (rdbUserType.SelectedValue == "1")
            {
                objVM.USER_TYPE = 'E';
            }
            else
            {
                objVM.USER_TYPE = 'S';
            }



            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateBulkUserAllotment(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        DisplayMessage("Record Already Exist.");
                        Clear();
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        DisplayMessage("Record Save Successfully.");
                        BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
                        ViewState["action"] = "add";
                        Clear();

                    }
                }
                else
                {

                    objVM.URID = 1;
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateBulkUserAllotment(objVM);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
                        ViewState["action"] = "edit";
                        DisplayMessage("Record Update Successfully.");
                        Clear();
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                        DisplayMessage("Record Already Exist.");
                        return;
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

   

   


   
    // This method is used to clear all the controls.
    private void Clear()
    {
        ViewState["URNO"] = null;
        ViewState["action"] = "add";
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        lvAllotment.DataSource = null;
        lvAllotment.DataBind();
        rdbUserType.SelectedValue = "1";
        rdbAllotted.SelectedValue = "1";
    }


    
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
            BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);

        }
        else
        {
            Clear();
            ViewState["action"] = "add";
            BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);

        }
    }   


    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
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
            DataSet ds = null;
            DropDownList ddlRoute = sender as DropDownList;
            string assa = ddlRoute.ClientID;      
            string ddlboardingpointclientid = ddlRoute.ClientID.Replace("ddlRoute", "ddlBoardingPoint");
            ds = objVMC.GetBoardingPointsByRouteID(Convert.ToInt32(ddlRoute.SelectedValue));
            for (int i = 0; i <= lvAllotment.Items.Count - 1; i++)
            {
                DropDownList ddlBoardingPoint = lvAllotment.Items[i].FindControl("ddlBoardingPoint") as DropDownList;
                
                if (ddlBoardingPoint.ClientID == ddlboardingpointclientid)
                {
                    ddlBoardingPoint.Items.Clear();    
                    ddlBoardingPoint.DataSource = ds.Tables[0];
                    ddlBoardingPoint.DataTextField = "STOPNAME";
                    ddlBoardingPoint.DataValueField = "STOPNO";
                    ddlBoardingPoint.DataBind();
                    ddlBoardingPoint.Items.Insert(0, new ListItem("Please Select", "0"));        
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.ddlRoute_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void rdbAllotted_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbAllotted.SelectedValue == "1")
            {
               
                BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
                ViewState["action"] = "add";
            }
            else
            {               
                BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
                ViewState["action"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.rdbAllotted_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void lvAllotment_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlRoute = (DropDownList)e.Item.FindControl("ddlRoute");
                objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME", "ROUTEID > 0", "ROUTEID");
               
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.lvAllotment_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }




    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdbAllotted.SelectedValue == "1")
            {
                if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0 && ddlSem.SelectedIndex > 0)
                {
                    BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
                    ViewState["action"] = "add";
                }
                else
                {
                    DisplayMessage("Please Select Degree, Branch And Semester.");
                    return;
                }
            }
            else
            { 
                BindlistView(rdbUserType.SelectedValue, rdbAllotted.SelectedValue);
                ViewState["action"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}