//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:07-09-2015
//PURPOSE: FOR LEAVE CONFIGURATION
//==============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_LeaveConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EMP_Attandance_Controller objAttandance = new EMP_Attandance_Controller();
    Shifts objShift = new Shifts();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillDropDown();

                GetCurrentConfig();
                
                
            }

        }
    }
    protected void FillDropDown()
    {
        //objCommon.FillDropDownList(ddlLeaveType, "PAYROLL_LEAVE", "LNO", "LEAVENAME", "LNO>0", "LEAVE");
        
        objCommon.FillDropDownList(ddlCasual, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlEarned, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlMedical, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlMaternity, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlPaternity, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlLWP, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");

    }
    protected void GetCurrentConfig()
    {
        DataSet ds;
        ds = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "OD_DAYS,OD_DAYS_APP,CL_NO,EL_NO,ML_HPL,LWP_NO", "MATER_NO,PATER_NO", "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtODDays.Text = ds.Tables[0].Rows[0]["OD_DAYS"].ToString();
            txtODAppDays.Text = ds.Tables[0].Rows[0]["OD_DAYS_APP"].ToString();
            ddlLWP.SelectedValue = ds.Tables[0].Rows[0]["LWP_NO"].ToString();
            ddlCasual.SelectedValue = ds.Tables[0].Rows[0]["CL_NO"].ToString();
            ddlEarned.SelectedValue = ds.Tables[0].Rows[0]["EL_NO"].ToString();
            ddlMedical.SelectedValue = ds.Tables[0].Rows[0]["ML_HPL"].ToString();
            ddlLWP.SelectedValue = ds.Tables[0].Rows[0]["LWP_NO"].ToString();
            ddlMaternity.SelectedValue = ds.Tables[0].Rows[0]["MATER_NO"].ToString();
            ddlPaternity.SelectedValue = ds.Tables[0].Rows[0]["PATER_NO"].ToString();
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ODdays = Convert.ToInt32(txtODDays.Text);
            int ODAppdays = Convert.ToInt32(txtODAppDays.Text);
          
            int cl_no = 0; int el_no = 0;
            int medical_no = 0; int lwp_no = 0;
            int mater_no = 0; int pater_no = 0;
            lwp_no = Convert.ToInt32(ddlLWP.SelectedValue);
            cl_no = Convert.ToInt32(ddlCasual.SelectedValue); el_no = Convert.ToInt32(ddlEarned.SelectedValue);
            medical_no = Convert.ToInt32(ddlMedical.SelectedValue); lwp_no = Convert.ToInt32(ddlLWP.SelectedValue);
            mater_no = Convert.ToInt32(ddlMaternity.SelectedValue); pater_no = Convert.ToInt32(ddlPaternity.SelectedValue);
           
           // objAttandance.UpdateLeaveRef_No(ODdays, ODAppdays,cl_no,el_no,medical_no, lwp_no, mater_no, pater_no);            
            objCommon.DisplayMessage("Record Updated Successfully", this);
       

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
       
    }
   
    private void ShowDetails()
    {
        DataSet ds = null;
        try
        {
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

   
    
}
