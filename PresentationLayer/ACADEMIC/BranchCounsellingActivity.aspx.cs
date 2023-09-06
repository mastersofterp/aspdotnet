using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Data;

public partial class ACADEMIC_BranchCounsellingActivity : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ActivityController objAc = new ActivityController();

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
               // Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = GetUserIPAddress();

                //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMISSION_CONFIG AG INNER JOIN ACD_ADMBATCH AB ON AG.ADMBATCH=AB.BATCHNO", "DISTINCT ADMBATCH", "BATCHNAME", "ADMBATCH>0 and AB.ACTIVESTATUS=1", "ADMBATCH DESC");
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH AB", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO>0 and AB.ACTIVESTATUS=1", "BATCHNO DESC");
                ddlAdmBatch.SelectedIndex = 1;
                
                LoadDefinedForeignStudentRegActivities();
            }
        }
    }
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
        }
        return User_IPAddress;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchCounsellingActivity.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchCounsellingActivity.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            //bool IsStarted = false;

            StudentController objstucontroller = new StudentController();
          // int activityno = Convert.ToInt32(ViewState["Acitivityno"]);
            DateTime startdate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
            DateTime endate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
            int IsStarted = 0;
            decimal regFees = 0;
            regFees = txtFees.Text.ToString().Equals(string.Empty) ? 0 : Convert.ToDecimal(txtFees.Text.ToString());
            if (hfdActive.Value == "true")
            {
                IsStarted = 1;
            }
            else
            {
                IsStarted = 0;
            }
                     
     
           // else if (rdoSelection.SelectedValue == "4")


            CustomStatus cs = (CustomStatus)objAc.InsertPhD_Students_RegistrationForm_Activity(Convert.ToInt32(ViewState["Acitivityno"]), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), IsStarted,regFees);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updPhdactivity, "PhD Students Registration Form Activity Define Successfully.", this.Page);
                    LoadDefinedForeignStudentRegActivities();
                    ClearControl();
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updPhdactivity, "PhD Students Registration Form Activity Update Successfully.", this.Page);
                    LoadDefinedForeignStudentRegActivities();
                    ClearControl();
                }
                //else
                //{
                //    objCommon.DisplayMessage(this.updPhdactivity, "PhD Students Registration Form Activity Already Exist.", this.Page);
                //    LoadDefinedForeignStudentRegActivities();
                //    ClearControl();
                //}
            
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_BranchCounsellingActivity.btnSubmit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearControl()
    {
        ddlAdmBatch.SelectedIndex = 0;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtFees.Text = "";
    }


    private void LoadDefinedForeignStudentRegActivities()
    {
        try
        {
            DataSet ds = null;
            StudentController objstucontroller = new StudentController();



            ds = objAc.GetPhDStudents_Registration_Form_Activity_Details();
            
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvForeign.DataSource = ds;
                lvForeign.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_SessionActivityDefinition.LoadDefinedForeignStudentRegActivities() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnForeignEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnForeignEdit = sender as ImageButton;
            DataSet ds = null;
            int Activityno = int.Parse(btnForeignEdit.CommandArgument);
            Convert.ToInt32(ViewState["Acitivityno"]);
          
            ds = objAc.GetDefinedPhDStudentActivities(Activityno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ddlAdmBatch.SelectedValue = dr["ADMBATCH"].ToString();

                txtStartDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                txtFees.Text = ds.Tables[0].Rows[0]["REGISTRATION_FEES"].ToString();

                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript10", "SetStatActive(false);", true);
                }
                ViewState["Acitivityno"] = dr["ACTIVITYNO"].ToString();
            
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_SessionActivityDefinition.btnForeignEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}