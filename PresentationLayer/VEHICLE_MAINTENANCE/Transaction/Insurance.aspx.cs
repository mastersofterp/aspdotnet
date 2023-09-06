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


public partial class VEHICLE_MAINTENANCE_Transaction_Insurance : System.Web.UI.Page
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
               // txttravellingDate.Text = System.DateTime.Now.Date.ToString();
                hdnDate.Value = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
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
    private void BindExpiryList()
    {
        try
        {
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                DataSet dsCollegeVehicleExpiry = objCommon.FillDropDown("VEHICLE_INSURANCE I INNER JOIN VEHICLE_MASTER M ON (M.VIDNO=I.VIDNO)", " I.POLICYNO,I.INSCOMPANY,I.INSENDDT,I.INSSIDNO", "M.VIDNO,REGNO +':'+NAME AS MODEL", "convert(date,INSENDDT) between GETDATE() and DATEADD(day,15,GETDATE()) AND I.VEHICLE_CATEGORY='C'", "");
                if (dsCollegeVehicleExpiry.Tables[0].Rows.Count > 0)
                {
                    lvIsuranceExpire.Visible = true;
                    lvIsuranceExpire.DataSource = dsCollegeVehicleExpiry;
                    lvIsuranceExpire.DataBind();
                }
                else
                {
                    lvIsuranceExpire.Visible = false;
                }
            }
            else
            {
                DataSet dsHireVehicleExpiry = objCommon.FillDropDown("VEHICLE_INSURANCE I INNER JOIN VEHICLE_HIRE_MASTER M ON (M.VEHICLE_ID=I.VIDNO)", " I.POLICYNO,I.INSCOMPANY,I.INSENDDT,I.INSSIDNO", "M.VEHICLE_ID,M.VEHICLE_NAME AS MODEL", "convert(date,INSENDDT) between GETDATE() and DATEADD(day,15,GETDATE()) AND I.VEHICLE_CATEGORY='H'", "");

                if (dsHireVehicleExpiry != null)
                {
                    if (dsHireVehicleExpiry.Tables[0].Rows.Count > 0)
                    {
                        lvIsuranceExpire.Visible = true;
                        lvIsuranceExpire.DataSource = dsHireVehicleExpiry;
                        lvIsuranceExpire.DataBind();
                    }
                    else
                    {
                        lvIsuranceExpire.Visible = false;
                    }
                }
                else
                {
                    lvIsuranceExpire.Visible = false;
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
            DataSet ds = objVMC.GetInsuranceAll(vehicle_cat);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvInsurance.DataSource = ds;
                lvInsurance.DataBind();
            }
            else
            {
                lvInsurance.DataSource = null;
                lvInsurance.DataBind();
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
        objVM.INSIDNO = Convert.ToInt32(no);
        CustomStatus CS =(CustomStatus)objVMC.DeleteInsuranceByInsIdNo(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
        }
    }
    private void clear()
    {
        ddl.SelectedIndex = 0;
        txtAgntName.Text = string.Empty;
        txtAgntNo.Text = string.Empty;
        txtAgntPh.Text = string.Empty;
        txtClaim.Text = string.Empty;
        txtClmDt.Text = string.Empty;
        txtInsComp.Text = string.Empty;
        txtInsDt.Text = string.Empty;
        txtInsEndDt.Text = string.Empty;
        txtNCB.Text = string.Empty;
        txtPolicyNo.Text = string.Empty;
        txtPrem.Text = string.Empty;
        txtSecPh.Text = string.Empty;     
        ViewState["action"] = "add";
        ViewState["IDNO"] = null;
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
            objVM.INSIDNO = Convert.ToInt32(NO);
            DataSet ds = objVMC.GetInsuranceByINSIDNO(objVM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAgntName.Text = ds.Tables[0].Rows[0]["AGENTNAME"].ToString();
                txtAgntNo.Text = ds.Tables[0].Rows[0]["AGENTNO"].ToString();
                txtAgntPh.Text = ds.Tables[0].Rows[0]["AGENTPHONE"].ToString();
                txtClaim.Text = ds.Tables[0].Rows[0]["CLAIMAMT"].ToString();
                txtClmDt.Text = ds.Tables[0].Rows[0]["CLAIMDT"].ToString();
                txtInsComp.Text = ds.Tables[0].Rows[0]["INSCOMPANY"].ToString();
                txtInsDt.Text = ds.Tables[0].Rows[0]["INSDT"].ToString();
                txtInsEndDt.Text = ds.Tables[0].Rows[0]["INSENDDT"].ToString();
                txtNCB.Text = ds.Tables[0].Rows[0]["NCB"].ToString();
                txtPolicyNo.Text = ds.Tables[0].Rows[0]["POLICYNO"].ToString();
                txtPrem.Text = ds.Tables[0].Rows[0]["PREMIUM"].ToString();
                ddl.SelectedValue = ds.Tables[0].Rows[0]["VIDNO"].ToString();
                txtSecPh.Text = ds.Tables[0].Rows[0]["AGENT_SEC_PHONE"].ToString(); //AGENT_SEC_PHONE
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
            if (!txtInsDt.Text.Equals(string.Empty))
            {
                if (!txtInsEndDt.Text.Equals(string.Empty))
                {
                    if (DateTime.Compare(Convert.ToDateTime(txtInsDt.Text), Convert.ToDateTime(txtInsEndDt.Text)) == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('To Date Should Be Greater Than From Date.');", true);
                        txtInsDt.Focus();
                        return;
                    }
                }
            }
            //if (!txtClmDt.Text.Equals(string.Empty))
            //{
            //    if (DateTime.Compare(Convert.ToDateTime(txtClmDt.Text), Convert.ToDateTime(txtInsEndDt.Text)) == 1)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Claim Date Should Be Greater Than Insurance Last Date.');", true);
            //        txtClmDt.Focus();
            //        return;
            //    }
            //}

            if (ddl.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Vehicle.", this.Page);
                return;
            }
            objVM.INSCOMPANY = txtInsComp.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtInsComp.Text.Trim());
            objVM.POLICYNO= txtPolicyNo.Text.Trim().Equals(string.Empty)?string.Empty:Convert.ToString(txtPolicyNo.Text.Trim());
            objVM.VIDNO=Convert.ToInt32(ddl.SelectedValue);
            objVM.INSDT=Convert.ToDateTime(txtInsDt.Text);
            objVM.INSENDDT=Convert.ToDateTime(txtInsEndDt.Text);
            objVM.PREMIUM=Convert.ToDecimal(txtPrem.Text);
            objVM.AGENTNAME=txtAgntName.Text.Trim().Equals(string.Empty)?string.Empty:Convert.ToString(txtAgntName.Text.Trim());
            objVM.AGENTNO=Convert.ToInt32(txtAgntNo.Text);
            objVM.AGENTPHONE=txtAgntPh.Text.Trim().Equals(string.Empty)?string.Empty:Convert.ToString(txtAgntPh.Text.Trim());
            objVM.AGENTSECPHONE = txtSecPh.Text.Trim().Equals(string.Empty)? string.Empty : Convert.ToString(txtSecPh.Text.Trim());
           
            if (txtNCB.Text == "")
            {
                objVM.NCB = 0;
            }
            else
            {
                objVM.NCB = Convert.ToDecimal(txtNCB.Text);
            }
            if (txtClaim.Text == "")
            {
                objVM.CLAIMAMT = 0;
            }
            else
            {
                objVM.CLAIMAMT = Convert.ToDecimal(txtClaim.Text);
            }

            if (txtClmDt.Text == "")
            {
                objVM.CLAIMDT = DateTime.MinValue;
            }
            else
            {
                objVM.CLAIMDT = Convert.ToDateTime(txtClmDt.Text);
            }
            if (rdblistVehicleType.SelectedItem.Text.Equals("College Vehicles"))
            {
                objVM.VEHICLECAT = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLECAT = Convert.ToString('H');
            }


          

            if (ViewState["action"]!=null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //--======start===Shaikh Juned 30-08-2022


                    DataSet ds = objCommon.FillDropDown("VEHICLE_INSURANCE", "POLICYNO", "AGENTNO", "POLICYNO='" + Convert.ToString(txtPolicyNo.Text) + "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string policyno = dr["POLICYNO"].ToString();
                            if (policyno == txtPolicyNo.Text)
                            {
                                objCommon.DisplayMessage(this.Page, "Policy No Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }
                    //---========end=====
                    CustomStatus cs = (CustomStatus)objVMC.AddInsurance(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        clear();
                        objCommon.DisplayMessage("Record Already Exist.", this.Page);
                        return;
                    }                 
                    else if  (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Save Successfully.",this.Page);
                        clear();
                        ViewState["action"]="add";
                        BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                        BindExpiryList();
                    }
                }
                else
                {
                    if (ViewState["IDNO"]!=null)
                    {
                        objVM.INSIDNO=Convert.ToInt32(ViewState["IDNO"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.UpdInsurance(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                            ViewState["action"]="add";
                            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue.ToString()));
                            BindExpiryList();
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
    protected void btnInsuExpiry_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "InsuranceExpiry.rpt");
    }
    private void ShowReport(string exporttype, string rptFileName)
    {

        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=InsuranceExpiry" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            //url += "&param=@P_EXPIRY_FOR=INSURANCE,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value;
            url += "&param=@P_EXPIRY_FOR=INSURANCE,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value + ",@P_VEHICLE_TYPE=" + rdblistVehicleType.SelectedValue;


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
