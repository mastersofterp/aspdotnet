///======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 07-NOV-2016
// CREATED BY    : SAKET SINGH                                      
// MODIFIED DATE :
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

public partial class Health_Report_GenerateCertificate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HealthTransactions objHelTran = new HealthTransactions();
    Health objHel = new Health();
    HelMasterController objHelController = new HelMasterController();

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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
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


    protected void ddlSelectCertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectCertificate.SelectedValue == "1")
        {
            RECLeave.Visible = true;
        }
        else if (ddlSelectCertificate.SelectedValue == "2")
        {
            appointmentCandidate.Visible = true;
        }
        else if (ddlSelectCertificate.SelectedValue == "3")
        {
            fitnessToReturn.Visible = true;
        }
        else if (ddlSelectCertificate.SelectedValue == "4")
        {
            RECLeave.Visible = false;
            appointmentCandidate.Visible = false;
            fitnessToReturn.Visible = false;
            referTo1.Visible = false;
            referTo3.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "5")
        {
            referTo1.Visible = true;
        }
        else if (ddlSelectCertificate.SelectedValue == "6")
        {
            RECLeave.Visible = false;
            appointmentCandidate.Visible = false;
            fitnessToReturn.Visible = false;
            referTo1.Visible = false;
            referTo3.Visible = false;
        }
        else if (ddlSelectCertificate.SelectedValue == "7")
        {
            referTo3.Visible = true;
        }
        else
        {
            RECLeave.Visible = false;
            appointmentCandidate.Visible = false;
            fitnessToReturn.Visible = false;
            referTo1.Visible = false;
            referTo3.Visible = false;
        }

    }
   
   
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
            objHel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            objHel.IssuedDate = Convert.ToDateTime(txtIssuedDate.Text);
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
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                        objCommon.DisplayMessage(this.upCommonFields, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //BindlistView();
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.upCommonFields, "Record Save Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["CER_ID"] != null)
                    {
                        objHel.CI_ID = Convert.ToInt32(ViewState["CER_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objHelController.AddCertificateIssue(objHel, objHelTran);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Clear();
                            objCommon.DisplayMessage(this.upCommonFields, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //BindlistView();
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.upCommonFields, "Record Updated Successfully.", this.Page);
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
        txtSignatureGov.Text = string.Empty;
        txtReferTO.Text = string.Empty;
        txtExpenditure.Text = string.Empty;

        //ViewState["CONTENTNO"] = null;
        //ViewState["action"] = "add";
        //lvContent.DataSource = null;
        //lvContent.DataBind();
    }
}