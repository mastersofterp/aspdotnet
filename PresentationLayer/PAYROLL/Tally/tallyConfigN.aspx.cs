//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : QUESTION BANK MODULE (YCCE)                      
// CREATION DATE : 18-FEB-2017                                                          
// CREATED BY    : NIKHIL DHONGE                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
#region NAMESPACES
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using System.Net;
using IITMS.UAIMS;
#endregion

public partial class PAYROLL_Tally_tallyConfigN : System.Web.UI.Page
{
    Con_TallyConfig ObjTC = new Con_TallyConfig();
    Ent_TallyConfig ObjTCM = new Ent_TallyConfig();
    //new added
    Con_CompanyConfig ObjCC = new Con_CompanyConfig();
    Ent_CompanyConfig ObjCCM = new Ent_CompanyConfig();

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    string Message = string.Empty;
    string UsrStatus = string.Empty;

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region PAGE LOAD EVENTS

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    // BindTallyConfiguration();
                    BindTallyConfigurationNew();
                this.objCommon.FillDropDownList(ddlCollege1, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "", "COLLEGE_NAME");
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    #endregion

    protected void btnEdit_click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            btnSubmit.ToolTip = "Click To Update";
            ImageButton btnEdit = sender as ImageButton;
            int BankId = int.Parse(btnEdit.CommandArgument);
            ViewState["TallyConfigId"] = BankId;
            ObjTCM.TallyConfigId = BankId;
            ObjTCM.CommandType = "BindTallyConfigId";
            ObjTCM.CollegeId = Convert.ToInt32(ddlCollege1.SelectedValue);

            DataSet ds = ObjTC.GetAllPayRollDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtServerIp.Text = ds.Tables[0].Rows[0]["ServerName"].ToString();
                    txtPortNumber.Text = ds.Tables[0].Rows[0]["PortNumber"].ToString();
                    chkIsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    ddlCollege1.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                    //int collegeno = Convert.ToInt32(ds.Tables[0].Rows[0]["CollegeId"].ToString());
                    //string college_name = objCommon.LookUp("ACD_COLLEGE_NAME", "COLLEGE_NO,COLLEGE_NAME", "COLLEGE_NO=" + Convert.ToInt32(collegeno));
                    //ddlCollege.SelectedValue = college_name;
                }
                else
                {

                    objCommon.DisplayMessage(upDetails, "Record Not Found", this.Page);
                    return;
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {

        txtServerIp.Text = string.Empty;
        //txtBankAddress.Text = string.Empty;
        txtPortNumber.Text = string.Empty;
        chkIsActive.Checked = true;
        ViewState["TallyConfigId"] = null;
        btnSubmit.Text = "Submit";
        btnSubmit.ToolTip = "Click To Submit";
        // objCommon.FillDropDownList(ddlCollege1, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "", "COLLEGE_NAME");


    }
    public void BindTallyConfigurationNew()
    {
        try
        {

            ObjTCM.CommandType = "BindTallyConfig";
            ObjTCM.CollegeId = Convert.ToInt32(ddlCollege1.SelectedValue);

            DataSet ds = ObjTC.GetAllPayRollDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DivCompany.Visible = true;
                    Rep_Company.DataSource = ds;
                    Rep_Company.DataBind();
                }
                else
                {
                    DivCompany.Visible = false;
                    Rep_Company.DataSource = null;
                    Rep_Company.DataBind();
                }
            }
            else
            {
                DivCompany.Visible = false;
                Rep_Company.DataSource = null;
                Rep_Company.DataBind();
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            long res = 0;
            ObjTCM.ServerName = txtServerIp.Text.Trim();
            ObjTCM.PortNumber = Convert.ToInt32(txtPortNumber.Text);


            ObjTCM.IsActive = chkIsActive.Checked;


            ObjTCM.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjTCM.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjTCM.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
            ObjTCM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTCM.MACAddress = Convert.ToString("0");
            ObjTCM.CollegeId = Convert.ToInt32(ddlCollege1.SelectedValue.ToString());

            if (ViewState["TallyConfigId"] == null)
            {
                res = ObjTC.AddUpdatePayRollTallyConfig(ObjTCM, ref Message);
            }
            else
            {
                ObjTCM.TallyConfigId = Convert.ToInt32(ViewState["TallyConfigId"].ToString());
                res = ObjTC.AddUpdatePayRollTallyConfig(ObjTCM, ref Message);
            }
            if (res == -99)
            {

                objCommon.DisplayMessage(upDetails, "Exception Occure", this.Page);

                return;

            }
            if (res == 0)
            {

                objCommon.DisplayMessage(upDetails, "Record Already Exists", this.Page);

                return;

            }
            if (res <= 0)
            {

                objCommon.DisplayMessage(upDetails, "Record is Not Save", this.Page);

            }
            else
            {
                if (ViewState["TallyConfigId"] == null)
                {
                    objCommon.DisplayMessage(upDetails, "Record Save Successfully", this.Page);
                    // BindTallyConfiguration();
                    BindTallyConfigurationNew();
                    Clear();
                }
                else
                {

                    objCommon.DisplayMessage(upDetails, "Record Updated Successfully", this.Page);
                    //  BindTallyConfiguration();
                    BindTallyConfigurationNew();
                    Clear();

                }
            }
        }
        catch (System.Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    public void BindTallyConfiguration()
    {
        try
        {
            ObjTCM.CollegeId = Convert.ToInt32(ddlCollege1.SelectedValue);
            ObjTCM.CollegeId = 6;
            ObjTCM.CommandType = "BindTallyConfig";
            DataSet ds = ObjTC.GetAllPayRollDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtServerIp.Text = Convert.ToString(dr["ServerName"]);
                        txtPortNumber.Text = Convert.ToString(dr["PortNumber"]);
                        chkIsActive.Checked = Convert.ToBoolean(dr["IsActive"]);
                        ViewState["TallyConfigId"] = Convert.ToInt32(dr["TallyConfigId"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_questionBankPaperSetReport.btnreportExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}