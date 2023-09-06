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

public partial class VEHICLE_MAINTENANCE_Transaction_RoadTax : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM objVM = new VM();
    VMController objVMC = new VMController();
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
                    FillDropDown();
                    BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                    BindExpiryList();
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


    private void BindExpiryList()
    {
        try
        {
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                DataSet dsCollegeVehicleExpiry = objCommon.FillDropDown("VEHICLE_ROADTAX R INNER JOIN VEHICLE_MASTER M ON (M.VIDNO=R.VIDNO)", " R.RTAXID,R.TDAT,RECNO,R.AMT", "M.VIDNO,REGNO +':'+NAME AS MODEL", "convert(date,TDAT) between GETDATE() and DATEADD(day,15,GETDATE()) AND R.VEHICLE_CATEGORY='C'", "");
                if (dsCollegeVehicleExpiry.Tables[0].Rows.Count > 0)
                {
                    lvRoadTaxExpire.Visible = true;
                    lvRoadTaxExpire.DataSource = dsCollegeVehicleExpiry;
                    lvRoadTaxExpire.DataBind();
                }
                else
                {
                    lvRoadTaxExpire.Visible = false;
                }
            }
            else
            {
                DataSet dsHireVehicleExpiry = objCommon.FillDropDown("VEHICLE_ROADTAX R INNER JOIN VEHICLE_HIRE_MASTER M ON (M.VEHICLE_ID=R.VIDNO)", " R.RTAXID,R.TDAT,RECNO,R.AMT", "M.VEHICLE_ID,M.VEHICLE_NAME AS MODEL", "convert(date,TDAT) between GETDATE() and DATEADD(day,15,GETDATE()) AND R.VEHICLE_CATEGORY='H'", "");

                if (dsHireVehicleExpiry != null)
                {
                    if (dsHireVehicleExpiry.Tables[0].Rows.Count > 0)
                    {
                        lvRoadTaxExpire.Visible = true;
                        lvRoadTaxExpire.DataSource = dsHireVehicleExpiry;
                        lvRoadTaxExpire.DataBind();
                    }
                    else
                    {
                        lvRoadTaxExpire.Visible = false;
                    }
                }
                else
                {
                    lvRoadTaxExpire.Visible = false;
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
            DataSet ds = objVMC.GetRoadTaxAll(vehicle_cat);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRoadTax.DataSource = ds;
                lvRoadTax.DataBind();
            }
            else
            {
                lvRoadTax.DataSource = null;
                lvRoadTax.DataBind();
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
        objVM.RTAXID= Convert.ToInt32(no);
        CustomStatus CS = (CustomStatus)objVMC.DeleteRoadTaxByRTaxID(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Sucessfully", this.Page);
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
        }
    }
    private void clear()
    {
        ddl.SelectedIndex = 0;
        txtAmt.Text = string.Empty;
        txtFrmDt.Text = string.Empty;
        txtPdDt.Text = string.Empty;
        txtReceiptNo.Text = string.Empty;
        txtToDt.Text = string.Empty;
        txtValid.Text = string.Empty;
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
            objVM.RTAXID= Convert.ToInt32(NO);
            DataSet ds = objVMC.GetRoadTaxByRTaxID(objVM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAmt.Text = ds.Tables[0].Rows[0]["AMT"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["FDATE"].ToString();
                txtPdDt.Text = ds.Tables[0].Rows[0]["PAIDDT"].ToString();
                txtReceiptNo.Text = ds.Tables[0].Rows[0]["RECNO"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TDAT"].ToString();
                txtValid.Text = ds.Tables[0].Rows[0]["YR"].ToString();
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
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) > 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('To Date Should Be Greater Than From Date.');", true);
                    txtFrmDt.Focus();
                    return;
                }
            }
            if (ddl.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Vehicle.", this.Page);
                return;
            }

            objVM.RYR= txtValid.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtValid.Text.Trim());
            objVM.RFDATE = Convert.ToDateTime(txtFrmDt.Text);
            objVM.RTDATE= Convert.ToDateTime(txtToDt.Text);
            objVM.RAMT= Convert.ToDecimal(txtAmt.Text);
            objVM.RECNO = txtReceiptNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtReceiptNo.Text.Trim());
            objVM.RPAIDDT= Convert.ToDateTime(txtPdDt.Text);
            objVM.VIDNO= Convert.ToInt32(ddl.SelectedValue);
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                objVM.VEHICLECAT = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLECAT = Convert.ToString('H');
            }

            //--======start===Shaikh Juned 30-08-2022
            if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtToDt.Text))
            {
                objCommon.DisplayMessage(this.Page, "To Date Should Be Greater Than From Date.", this.Page);
                txtFrmDt.Focus();
                return;
            }

           
            //---========end=====

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataSet ds = objCommon.FillDropDown("VEHICLE_ROADTAX", "RECNO", "RTAXID", "RECNO='" + Convert.ToString(txtReceiptNo.Text) + "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string recno = dr["RECNO"].ToString();
                            if (recno == txtReceiptNo.Text)
                            {
                                objCommon.DisplayMessage(this.Page, "Receipt No Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }

                    CustomStatus cs = (CustomStatus)objVMC.AddRoadTax(objVM);
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
                        objVM.RTAXID= Convert.ToInt32(ViewState["IDNO"].ToString());
                        DataSet ds = objCommon.FillDropDown("VEHICLE_ROADTAX", "RECNO", "RTAXID", "RECNO='" + Convert.ToString(txtReceiptNo.Text) + "' and RTAXID!='"+Convert.ToInt32(ViewState["IDNO"].ToString())+"'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string recno = dr["RECNO"].ToString();
                                if (recno == txtReceiptNo.Text)
                                {
                                    objCommon.DisplayMessage(this.Page, "Receipt No Is Already Exist.", this.Page);
                                    return;
                                }

                            }
                        }

                        CustomStatus cs = (CustomStatus)objVMC.UpdRoadTax(objVM);
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
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_Insurance.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnRoadTax_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "RoadTax.rpt");
    }
    private void ShowReport(string exporttype, string rptFileName)
    {

        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=RoadTax" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

            url += "&param=@P_EXPIRY_FOR=ROAD_TAX,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value + ",@P_VEHICLE_TYPE=" + rdblistVehicleType.SelectedValue;

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
