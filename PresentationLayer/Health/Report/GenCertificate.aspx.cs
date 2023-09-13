///======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 07-NOV-2016
// CREATED BY    : SAKET SINGH                                      
// MODIFIED DATE : 10-NOV-2016
// MODIFIED DESC :
//====================================================================================== 
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class Health_Report_GenCertificate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HealthTransactionController objHelTranTransaction = new HealthTransactionController();
    HealthTransactions objHelTran = new HealthTransactions();
    Health objHel = new Health();
    HelMasterController objHelController = new HelMasterController();
   
    #region Page Events
    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["userno"],Session["username"],   
    /// </summary>
    /// 
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
                      //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    ViewState["action"] = "add";
                    BindAllListViewData();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }






    /// <summary>
    /// Page_PreInit event calls SetMasterPage() method.   @P_PATIENT_CODE
    /// </summary>
    /// 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {

    }

    #endregion

    #region Private Methods
    /// <summary>
    /// CheckPageAuthorization() method checks whether the user is authorised to access this Page    
    /// </summary>
    /// 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }

    private void BindAllListViewData()
    {
        DataSet dsRec = objHelController.GetDataHealthCertificate(Convert.ToInt32(ddlSelectCertificate.SelectedValue));
        if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
        {
            lvGenRateCer.DataSource = dsRec.Tables[0];
            lvGenRateCer.DataBind();
            lvGenRateCer.Visible = true;
        }
        else
        {
            lvGenRateCer.DataSource = null;
            lvGenRateCer.DataBind();
            lvGenRateCer.Visible = false;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_CERID=" + Convert.ToInt32(ViewState["CER_ID"]) + ",@P_CERNO=" + Convert.ToInt32(ViewState["CER_NO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_MedicineExpiry.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        ddlSelectCertificate.SelectedIndex = 0;
        txtPatientName.Text = string.Empty;
        txtDrName.Text = string.Empty;
        txtSufferingFrom.Text = string.Empty;
        txtDepartment.Text = string.Empty;
        txtIssuedDate.Text = string.Empty;
        txtPostOf.Text = string.Empty;
        txtAMAttentant.Text = string.Empty;
        txtSignature.Text = string.Empty;
        txtAbsenceDays.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtAgeAppearance.Text = string.Empty;
        txtReferTO.Text = string.Empty;
        txtExpenditure.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        ViewState["CER_ID"] = null;
        ViewState["action"] = "add";
        ddlSelectCertificate.Enabled = true;

        if (ddlSelectCertificate.SelectedValue == "1")
        {
            trSignature.Visible = true;
            trDrName.Visible = true;
            trSufferingFrom.Visible = true;
            trAbsenceday.Visible = true;
            trFromDate.Visible = true;

            trDepartment.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
            trReferTo.Visible = false;

        }
        else if (ddlSelectCertificate.SelectedValue == "2")
        {
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trPostOf.Visible = true;
            trAgeAccor.Visible = true;
            trAgeAppearence.Visible = true;

            trSignature.Visible = false;
            trDrName.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trReferTo.Visible = false;
            trExpenditure.Visible = false;

        }
        else if (ddlSelectCertificate.SelectedValue == "3")
        {
            trSignature.Visible = true;
            trDrName.Visible = true;
            trDepartment.Visible = true;

            trSufferingFrom.Visible = false;
            trPostOf.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trReferTo.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "4")
        {
            trDrName.Visible = true;
            trDepartment.Visible = true;

            trSignature.Visible = false;
            trSufferingFrom.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trReferTo.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "5")
        {
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trReferTo.Visible = true;

            trSignature.Visible = false;
            trDrName.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "6")
        {
            trDrName.Visible = true;
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trReferTo.Visible = true;

            trSignature.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "7")
        {
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trReferTo.Visible = true;
            trExpenditure.Visible = true;

            trSignature.Visible = false;
            trDrName.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
        }
        else
        {
            trSignature.Visible = false;
            trDrName.Visible = false;
            trSufferingFrom.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;

            trDepartment.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
            trReferTo.Visible = false;
        }
        BindAllListViewData();
    }

    private void BindDataById()
    {
        //DataSet ds = objHelController.GetDataHealthCertificateById(Convert.ToInt32(ViewState["CER_ID"]));
        DataSet ds = objHelController.GetDataHealthCertificateById(Convert.ToInt32(ViewState["CER_ID"]));
        if (Convert.ToInt32(ds.Tables[0].Rows.Count) > 0)
        {
            ddlSelectCertificate.SelectedValue = ds.Tables[0].Rows[0]["CER_ID"].ToString();
            txtPatientName.Text = ds.Tables[0].Rows[0]["PatientName"].ToString();
            txtDrName.Text = ds.Tables[0].Rows[0]["DrName"].ToString();
            txtSufferingFrom.Text = ds.Tables[0].Rows[0]["SufferingFrom"].ToString();
            txtAbsenceDays.Text = ds.Tables[0].Rows[0]["AbsenceDays"].ToString();
            txtFromDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
            txtIssuedDate.Text = ds.Tables[0].Rows[0]["IssuedDate"].ToString();
            txtAMAttentant.Text = ds.Tables[0].Rows[0]["AuthorizedMed"].ToString();
            txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
            txtPostOf.Text = ds.Tables[0].Rows[0]["PostOf"].ToString();
            txtAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
            txtAgeAppearance.Text = ds.Tables[0].Rows[0]["AgeAppreance"].ToString();
            txtReferTO.Text = ds.Tables[0].Rows[0]["ReferTo"].ToString();
            txtExpenditure.Text = ds.Tables[0].Rows[0]["Expenditure"].ToString();

        }
        else
        {

        }

    }

    private void ShowDetails(int CER_ID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_GENERATE_CERTIFICATE", "*", "", "CER_ID=" + CER_ID, "CER_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSelectCertificate.SelectedValue = ds.Tables[0].Rows[0]["CER_NO"].ToString();
                txtPatientName.Text = ds.Tables[0].Rows[0]["PatientName"].ToString();
                txtDrName.Text = ds.Tables[0].Rows[0]["DrName"].ToString();
                txtSufferingFrom.Text = ds.Tables[0].Rows[0]["SufferingFrom"].ToString();
                txtAbsenceDays.Text = ds.Tables[0].Rows[0]["AbsenceDays"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                txtIssuedDate.Text = ds.Tables[0].Rows[0]["IssuedDate"].ToString();
                txtAMAttentant.Text = ds.Tables[0].Rows[0]["AuthorizedMed"].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                txtSignature.Text = ds.Tables[0].Rows[0]["SignGovt"].ToString();
                txtPostOf.Text = ds.Tables[0].Rows[0]["PostOf"].ToString();
                txtAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                txtAgeAppearance.Text = ds.Tables[0].Rows[0]["AgeAppreance"].ToString();
                txtReferTO.Text = ds.Tables[0].Rows[0]["ReferTo"].ToString();
                txtExpenditure.Text = ds.Tables[0].Rows[0]["Expenditure"].ToString();

                if (ddlSelectCertificate.SelectedValue == "1")
                {
                    trSignature.Visible = true;
                    trDrName.Visible = true;
                    trSufferingFrom.Visible = true;
                    trAbsenceday.Visible = true;
                    trFromDate.Visible = true;

                    trDepartment.Visible = false;
                    trPostOf.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                    trExpenditure.Visible = false;
                    trReferTo.Visible = false;


                }
                else if (ddlSelectCertificate.SelectedValue == "2")
                {
                    trDepartment.Visible = true;
                    trSufferingFrom.Visible = true;
                    trPostOf.Visible = true;
                    trAgeAccor.Visible = true;
                    trAgeAppearence.Visible = true;

                    trSignature.Visible = false;
                    trDrName.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;
                    trReferTo.Visible = false;
                    trExpenditure.Visible = false;

                }
                else if (ddlSelectCertificate.SelectedValue == "3")
                {
                    trSignature.Visible = true;
                    trDrName.Visible = true;
                    trDepartment.Visible = true;

                    trSufferingFrom.Visible = false;
                    trPostOf.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                    trReferTo.Visible = false;
                    trExpenditure.Visible = false;
                }
                else if (ddlSelectCertificate.SelectedValue == "4")
                {
                    trDrName.Visible = true;
                    trDepartment.Visible = true;

                    trSignature.Visible = false;
                    trSufferingFrom.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;
                    trPostOf.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                    trReferTo.Visible = false;
                    trExpenditure.Visible = false;
                }
                else if (ddlSelectCertificate.SelectedValue == "5")
                {
                    trDepartment.Visible = true;
                    trSufferingFrom.Visible = true;
                    trReferTo.Visible = true;

                    trSignature.Visible = false;
                    trDrName.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;
                    trPostOf.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                    trExpenditure.Visible = false;
                }
                else if (ddlSelectCertificate.SelectedValue == "6")
                {
                    trDrName.Visible = true;
                    trDepartment.Visible = true;
                    trSufferingFrom.Visible = true;
                    trReferTo.Visible = true;

                    trSignature.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;
                    trPostOf.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                    trExpenditure.Visible = false;
                }
                else if (ddlSelectCertificate.SelectedValue == "7")
                {
                    trDepartment.Visible = true;
                    trSufferingFrom.Visible = true;
                    trReferTo.Visible = true;
                    trExpenditure.Visible = true;

                    trSignature.Visible = false;
                    trDrName.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;
                    trPostOf.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                }
                else
                {
                    trSignature.Visible = false;
                    trDrName.Visible = false;
                    trSufferingFrom.Visible = false;
                    trAbsenceday.Visible = false;
                    trFromDate.Visible = false;

                    trDepartment.Visible = false;
                    trPostOf.Visible = false;
                    trAgeAccor.Visible = false;
                    trAgeAppearence.Visible = false;
                    trExpenditure.Visible = false;
                    trReferTo.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Actions

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objHel.CER_NO = Convert.ToInt32(ddlSelectCertificate.SelectedValue);
            objHel.SignGovt = txtSignature.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSignature.Text.Trim());
            objHel.PatientName = txtPatientName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPatientName.Text.Trim());
            objHel.DrName = txtDrName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrName.Text.Trim());
            objHel.SufferingFrom = txtSufferingFrom.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSufferingFrom.Text.Trim());
            objHel.AbsenceDays = txtAbsenceDays.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtAbsenceDays.Text.Trim());

            if (trFromDate.Visible == true)
            {
                objHel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            }
            else
            {
                objHel.FromDate = DateTime.MinValue;
            }
            if (txtIssuedDate.Text != string.Empty)
            {
                objHel.IssuedDate = Convert.ToDateTime(txtIssuedDate.Text);
            }
            else
            {
                objHel.IssuedDate = DateTime.MinValue;
            }
           
            objHel.AuthorizedMed = txtAMAttentant.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtAMAttentant.Text.Trim());
            objHel.Department = txtDepartment.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDepartment.Text.Trim());
            objHel.PostOf = txtPostOf.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPostOf.Text.Trim());
            objHel.Age = txtAge.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtAge.Text.Trim());
            objHel.AgeAppreance = txtAgeAppearance.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtAgeAppearance.Text.Trim());
            objHel.ReferTo = txtReferTO.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtReferTO.Text.Trim());
            objHel.Expenditure = txtExpenditure.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtExpenditure.Text.Trim());

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objHelController.AddGenerateCertificate(objHel, objHelTran);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindAllListViewData();
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updOpdTransaction, "Record Save Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["CER_ID"] != null)
                    {
                        objHel.CER_ID = Convert.ToInt32(ViewState["CER_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objHelController.AddGenerateCertificate(objHel, objHelTran);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindAllListViewData();
                            ViewState["action"] = "add";
                             objCommon.DisplayMessage(this.updOpdTransaction, "Record Updated Successfully.", this.Page);
                            Clear();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlSelectCertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectCertificate.SelectedValue == "1")
        {
            trSignature.Visible = true;
            trDrName.Visible = true;
            trSufferingFrom.Visible = true;
            trAbsenceday.Visible = true;
            trFromDate.Visible = true;

            trDepartment.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
            trReferTo.Visible = false;

        }
        else if (ddlSelectCertificate.SelectedValue == "2")
        {
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trPostOf.Visible = true;
            trAgeAccor.Visible = true;
            trAgeAppearence.Visible = true;

            trSignature.Visible = false;
            trDrName.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trReferTo.Visible = false;
            trExpenditure.Visible = false;

        }
        else if (ddlSelectCertificate.SelectedValue == "3")
        {
            trSignature.Visible = true;
            trDrName.Visible = true;
            trDepartment.Visible = true;

            trSufferingFrom.Visible = false;
            trPostOf.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trReferTo.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "4")
        {
            trDrName.Visible = true;
            trDepartment.Visible = true;

            trSignature.Visible = false;
            trSufferingFrom.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trReferTo.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "5")
        {
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trReferTo.Visible = true;

            trSignature.Visible = false;
            trDrName.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "6")
        {
            trDrName.Visible = true;
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trReferTo.Visible = true;

            trSignature.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "7")
        {
            trDepartment.Visible = true;
            trSufferingFrom.Visible = true;
            trReferTo.Visible = true;
            trExpenditure.Visible = true;

            trSignature.Visible = false;
            trDrName.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
        }
        else
        {
            trSignature.Visible = false;
            trDrName.Visible = false;
            trSufferingFrom.Visible = false;
            trAbsenceday.Visible = false;
            trFromDate.Visible = false;

            trDepartment.Visible = false;
            trPostOf.Visible = false;
            trAgeAccor.Visible = false;
            trAgeAppearence.Visible = false;
            trExpenditure.Visible = false;
            trReferTo.Visible = false;
        }

        BindAllListViewData();
    }

    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        string IP = Request.ServerVariables["REMOTE_HOST"];

        Button btnPrint = sender as Button;
        int CER_ID = int.Parse(btnPrint.CommandArgument);
        ViewState["CER_ID"] = int.Parse(btnPrint.CommandArgument);
        ViewState["CER_NO"] = int.Parse(btnPrint.CommandName);

        if (Convert.ToInt32(ViewState["CER_NO"]) == 1)
        {
            ShowReport("GenrateCertificate", "RecommendedLeaveReport.rpt");
        }
        else if (Convert.ToInt32(ViewState["CER_NO"]) == 2)
        {
            ShowReport("GenrateCertificate", "AppointmentGazettedReport.rpt");
        }
        else if (Convert.ToInt32(ViewState["CER_NO"]) == 3)
        {
            ShowReport("GenrateCertificate", "FitnessToReturnDutyReport.rpt");
        }
        else if (Convert.ToInt32(ViewState["CER_NO"]) == 4)
        {
            ShowReport("GenrateCertificate", "FitnessCertificateReport.rpt");
        }
        else if (Convert.ToInt32(ViewState["CER_NO"]) == 5)
        {
            ShowReport("GenrateCertificate", "Reference1CertificateReport.rpt");
        }
        else if (Convert.ToInt32(ViewState["CER_NO"]) == 6)
        {
            ShowReport("GenrateCertificate", "Reference2CertificateReport.rpt");
        }
        else if (Convert.ToInt32(ViewState["CER_NO"]) == 7)
        {
            ShowReport("GenrateCertificate", "Reference3CertificateReport.rpt");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CER_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["CER_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ddlSelectCertificate.Enabled = false;
            ShowDetails(CER_ID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Report_Certificates.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

}