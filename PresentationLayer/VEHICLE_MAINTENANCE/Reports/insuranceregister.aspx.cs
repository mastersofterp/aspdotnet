//=============================================
//MODIFIED BY    : MRUNAL SINGH
//MODIFIED DATE  : 14-01-2015
//DESCRIPTION    : NOT ALLOW TO TAKE EPORT  IN 
//                 CRYATAL VIEWER. 
//=============================================
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


public partial class VEHICLE_MAINTENANCE_Reports_insuranceregister : System.Web.UI.Page
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

                    Fill_DropDown();
                    //BindlistView();
                }

            }
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);


                    txtFDate.Focus();
                    txtFDate.Text = string.Empty;
                    txtTDate.Text = string.Empty;
                    return;
                }
            }

            ShowInsuranceRegister(rdoReportType.SelectedValue, "cryInsurence.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
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
            int TTID = int.Parse(btnEdit.CommandArgument);
            ViewState["TTID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            //ShowDetails(TTID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_TripType.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlVehicle.SelectedValue = "0";
        ddlInsurance.SelectedValue = "0";

        ddlAgentName.SelectedValue = "0";

        txtFDate.Text = string.Empty;
        txtTDate.Text = string.Empty;
        //chkActive.Checked = true;
    }

    public void Fill_DropDown()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0", "VIDNO");
        objCommon.FillDropDownList(ddlInsurance, "VEHICLE_INSURANCE", "DISTINCT INSCOMPANY", "INSCOMPANY AS COMPANY", "", "INSCOMPANY");
        objCommon.FillDropDownList(ddlAgentName, "VEHICLE_INSURANCE", "DISTINCT AGENTNO", "AGENTNAME", "", "AGENTNAME");

    }





    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }



    private void ShowInsuranceRegister(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("LegalMatters")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Insurance_Register" + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_VIDNO=" + ddlVehicle.SelectedValue + ",@P_INSURANCE=" + ddlInsurance.SelectedValue + ",@P_AGENT=" + ddlAgentName.SelectedValue; //+ ",@P_TTID=" + ddlTripType.SelectedValue + ",@P_PASSENGER=" + txtPassenger.Text.Trim();



            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
            }



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







}
