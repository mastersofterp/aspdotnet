using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessLogic.PostAdmission;
using BusinessLogicLayer.BusinessEntities.Academic;
using System.Globalization;
/*
---------------------------------------------------------------------------------------------------------------------------                                                                      
Created By  :                                                                 
Created On  :                                                    
Purpose     :                                      
Version     :                                                             
---------------------------------------------------------------------------------------------------------------------------                                                                        
Version     Modified On      Modified By             Purpose                                                                        
---------------------------------------------------------------------------------------------------------------------------                                                                        
1.0.1     14-03-2024        Isha Kanojiya            Added Branch and Start/End Payment Date and Provision Admission Date  
---------------------------------------------------------------------------------------------------------------------------   
 1.0.2    28-03-2024        Isha Kanojiya            changes for validation for all dates.
---------------------------------------------------------------------------------------------------------------------------         
 */

public partial class ADMPFeePaymentConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPFeePaymentConfigController objAFPCEC = new ADMPFeePaymentConfigController();
    ADMPFeePaymentConfigEntity objAFPCEE = new ADMPFeePaymentConfigEntity();
    ADMPProvisionalAdmissionAprovalController objADMAPPR = new ADMPProvisionalAdmissionAprovalController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["FeePayConfig_ID"] = 0;
            ViewState["action"] = "add";
            FillDropDown_ForActvityFor();
            FillDropDown();
            BindListView();
        }
    }

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS=1", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void FillDropDown_ForActvityFor()
    {
        try
        {
            string whereData = "'phd'";
            if (rdoActivityFor.SelectedValue.ToString().Equals("1"))
            {
                whereData = "'UG','PG'";
            }
            objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0 AND ACTIVESTATUS=1 AND UA_SECTION !=3", "UA_SECTION DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void BindListView()
    {
        try
        {
            DataSet ds = objAFPCEC.GetRetADMPFeePayConfigListData(0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlFeePayConfig.Visible = true;
                lvFeePayConfig.DataSource = ds.Tables[0];
                lvFeePayConfig.DataBind();
            }
            else
            {
                pnlFeePayConfig.Visible = false;
                lvFeePayConfig.DataSource = null;
                lvFeePayConfig.DataBind();
            }


            foreach (ListViewDataItem dataitem in lvFeePayConfig.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss.Equals("Started"))
                {
                    Status.CssClass = "badge badge-success";
                }
                else
                {
                    Status.CssClass = "badge badge-danger";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rdoActivityFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDropDown_ForActvityFor();
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlCountry_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlPaymentCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentCategory.SelectedIndex > 0)
            {
                setAmountLabels();
            }
            else
            {
                divAmount.Visible = false;
                lblAmount.InnerText = "Amount";
                txtAmount.Text = "";
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlCountry_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("ADMPFeePaymentConfiguration.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

    private void setAmountLabels()
    {
        try
        {
            if (ddlPaymentCategory.SelectedValue.ToString().Equals("1"))
            {
                divAmount.Visible = true;
                lblAmount.InnerText = "Amount";
                txtAmount.Attributes["isMaxLength"] = "8";
                txtAmount.Text = "";
            }
            else if (ddlPaymentCategory.SelectedValue.ToString().Equals("2"))
            {
                divAmount.Visible = true;
                lblAmount.InnerText = "Percentage";
                txtAmount.Attributes["isMaxLength"] = "3";
                txtAmount.Text = "";
            }
            else
            {
                divAmount.Visible = false;
                lblAmount.InnerText = "Amount";
                txtAmount.Text = "";
            }

        }
        catch (Exception ex)
        {
            throw;

        }
    }

    protected void btnEditFeePay_Click(object sender, System.EventArgs e)
    {
        try
        {
            LinkButton btnEditCreateEvent = sender as LinkButton;
            int FeePayConfig_ID = Convert.ToInt32(btnEditCreateEvent.CommandArgument);
            ViewState["FeePayConfig_ID"] = Convert.ToInt32(btnEditCreateEvent.CommandArgument);
            ShowDetail(FeePayConfig_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetail(int FeePayConfig_ID)
    {
        try
        {
            DataTable dt;
            DataSet ds = objAFPCEC.GetRetADMPFeePayConfigListData(FeePayConfig_ID);
            dt = objAFPCEC.GetRetADMPFeePayConfigListData(Convert.ToInt32(FeePayConfig_ID)).Tables[0];


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                //ACTIVITYFOR	ADMBATCH	PROGRAMTYPE	DEGREENO	PAYMENTCATEGORY	FEEPAYMENT	STARTDATE	ENDDATE	ACTIVITYSTATUS
                if (ds.Tables[0].Rows[0]["ACTIVITYFOR"].ToString().Equals("1"))
                {
                    rdoActivityFor.SelectedValue = "1";
                }
                else if (ds.Tables[0].Rows[0]["ACTIVITYFOR"].ToString().Equals("2"))
                {
                    rdoActivityFor.SelectedValue = "2";
                }

                if (ddlAdmBatch.Items.Count > 1)
                {
                    ddlAdmBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                }

                if (ddlProgramType.Items.Count > 1)
                {
                    ddlProgramType.SelectedValue = ds.Tables[0].Rows[0]["PROGRAMTYPE"].ToString();
                }

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();

                //<1.0.1>
                int BranchNo = Convert.ToInt32(ddlDegree.SelectedValue);
                MultipleBranchBind(BranchNo, 0);
                char delimiterChars = ',';
                string degreetype = dt.Rows[0]["BRANCHNO"].ToString();
                string[] stu = degreetype.Split(delimiterChars);
                for (int j = 0; j < stu.Length; j++)
                {
                    for (int i = 0; i < lstBranch.Items.Count; i++)
                    {
                        if (stu[j].Trim() == lstBranch.Items[i].Value.Trim())
                        {
                            lstBranch.Items[i].Selected = true;
                        }
                    }
                }




                txtStartDate.Text = ds.Tables[0].Rows[0]["STARTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");
                txtEndDate.Text = ds.Tables[0].Rows[0]["ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");        
                txtOfficeVisitStartDate.Text = ds.Tables[0].Rows[0]["OFFICE_VISIT_START_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["OFFICE_VISIT_START_DATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");
                txtOfficeVisitEndDate.Text = ds.Tables[0].Rows[0]["OFFICE_VISIT_End_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["OFFICE_VISIT_End_DATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");
                txtProvisionalAdmissionValidDate.Text = ds.Tables[0].Rows[0]["PROVISIONAL_ADMISSION_OFFER_VALID_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["PROVISIONAL_ADMISSION_OFFER_VALID_DATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");

                //</1.0.1>   
                if (ddlPaymentCategory.Items.Count > 1)
                {
                    ddlPaymentCategory.SelectedValue = ds.Tables[0].Rows[0]["PAYMENTCATEGORY"].ToString();
                }
                setAmountLabels();
                txtAmount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["FEEPAYMENT"].ToString()).ToString();

                if (ds.Tables[0].Rows[0]["ACTIVITYSTATUS"].ToString().Equals("True"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(false);", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw;

        }
    }

   


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           
            DateTime startDate, endDate, officeStartDate, officeEndDate, provisionalAdmissionDate;

            if (!DateTime.TryParseExact(txtOfficeVisitStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out officeStartDate))
            {
                objCommon.DisplayMessage(updSession, "Enter Valid Office Report Start Date.", this.Page);
                return;
            }

            if (!DateTime.TryParseExact(txtOfficeVisitEndDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out officeEndDate))
            {
                objCommon.DisplayMessage(updSession, "Enter Valid Office Report End Date.", this.Page);
                return;
            }

            if (!DateTime.TryParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                objCommon.DisplayMessage(updSession, "Enter Valid Payment Start Date.", this.Page);
                return;
            }

            if (!DateTime.TryParseExact(txtEndDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                objCommon.DisplayMessage(updSession, "Enter Valid Payment End Date.", this.Page);
                return;
            }

            if (!DateTime.TryParseExact(txtProvisionalAdmissionValidDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out provisionalAdmissionDate))
            {
                objCommon.DisplayMessage(updSession, "Enter valid Provisional Admission Valid Date ", this.Page);
                return;
            }

            
            if (DateTime.Today > officeStartDate)
            {
                objCommon.DisplayMessage(updSession, "Office Report Start Date should be today/future date", this.Page);
                return;
            }
            if (endDate < startDate)
            {
                objCommon.DisplayMessage(updSession, "Payment End Date should be greater than or equal to Payment Start Date", this.Page); 
                return;
            }

            if (provisionalAdmissionDate < endDate)
            {
                objCommon.DisplayMessage(updSession, "Provisional Admission Offer Valid Date should be greater than or equal to Payment End Date", this.Page);
                return;
            }

            if (startDate < officeEndDate)
            {
                objCommon.DisplayMessage(updSession, "Payment Start Date should be greater than or equal to Office Report End Date", this.Page);
                return;
            }

            if (officeEndDate < officeStartDate)
            {
                objCommon.DisplayMessage(updSession, "Office Report End Date should be greater than or equal to Office Report Start Date", this.Page);
                return;
            }

            // Data submission
            objAFPCEE.ConfigID = Convert.ToInt32(ViewState["FeePayConfig_ID"]);
            objAFPCEE.Activityfor = Convert.ToInt32(rdoActivityFor.SelectedValue.Equals("1") ? "1" : "2");
            objAFPCEE.Admbatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objAFPCEE.Programtype = Convert.ToInt32(ddlProgramType.SelectedValue);
            objAFPCEE.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objAFPCEE.Startdate = startDate;
            objAFPCEE.Enddate = endDate;
            objAFPCEE.OfficeVisitStartDate = officeStartDate;
            objAFPCEE.OfficeVisitEndDate = officeEndDate;
            objAFPCEE.ProvisionalAdmissionDate = provisionalAdmissionDate;
            objAFPCEE.Paymentcategory = Convert.ToInt32(ddlPaymentCategory.SelectedValue);

            if (ddlPaymentCategory.SelectedValue.ToString().Equals("3"))
            {
                objAFPCEE.Feepayment = 0.0;
            }
            else
            {
                objAFPCEE.Feepayment = Convert.ToDouble(txtAmount.Text);
            }

            objAFPCEE.Activitystatus = hfdActive.Value == "true";

        
            string SubjectNo = string.Empty;
            foreach (ListItem item in lstBranch.Items)
            {
                if (item.Selected == true)
                {
                    SubjectNo += item.Value + ",";
                }
            }
            if (SubjectNo.Contains(','))
            {
                SubjectNo = SubjectNo.Remove(SubjectNo.Length - 1);
            }
            objAFPCEE.Branchno = SubjectNo;

            int ret = 0;
            string displaymsg = "Record added successfully.";
            if (ViewState["action"].ToString().Equals("add"))
            {
                ret = Convert.ToInt32(objAFPCEC.InsertADMPFeePayConfig(objAFPCEE));
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                displaymsg = "Record updated successfully.";
                ret = Convert.ToInt32(objAFPCEC.UpdateADMPFeePayConfig(objAFPCEE));
            }
            if (ret == 2)
            {
                displaymsg = "Record already exists.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                setDefaultValues();
            }
            else
            {
                objCommon.DisplayMessage("Please fill data again.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPFeePaymentConfiguration.btnsubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        setDefaultValues();
    }

    protected void setDefaultValues()
    {
        try
        {
            rdoActivityFor.SelectedValue = "1";
            ddlAdmBatch.ClearSelection();
            ddlProgramType.ClearSelection();
          //  ddlDegree.ClearSelection(); 
            ddlDegree.Items.Clear();
            //<1.0.1>
            lstBranch.Items.Clear();
            //</1.0.1>
            divAmount.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;

            txtOfficeVisitStartDate.Text = string.Empty;
            txtOfficeVisitEndDate.Text = string.Empty;
            txtProvisionalAdmissionValidDate.Text = string.Empty;
            ddlPaymentCategory.ClearSelection();
            txtAmount.Text = string.Empty;
            ViewState["FeePayConfig_ID"] = 0;
            ViewState["action"] = "add";
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        EndDate();
    }


  
    protected void EndDate()
    {
        if (!string.IsNullOrWhiteSpace(txtStartDate.Text) && !string.IsNullOrWhiteSpace(txtEndDate.Text))
        {
            DateTime startDate;
            DateTime endDate;

            if (!DateTime.TryParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                
            }

            if (!DateTime.TryParseExact(txtEndDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
              
            }
          
            if (endDate < startDate)
            {
                objCommon.DisplayMessage(updSession, "Payment End Date should be greater than or equal to Payment Start Date", this.Page);
               
            }
        }
    }

   


    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        int Per = 0;
        if (lblAmount.InnerText == "Percentage")
        {
            Per = Convert.ToInt32(txtAmount.Text);
            if (Per > 100)
            {
                objCommon.DisplayMessage(updSession, "Percentage Should Not Be Greater Than 100", this.Page);
                txtAmount.Text = "";
            }
        }
    }

    //<1.0.1>
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstBranch.ClearSelection();
            if (ddlDegree.SelectedIndex > 0)
            {
                int BranchNo = Convert.ToInt32(ddlDegree.SelectedValue);
                MultipleBranchBind(BranchNo, 0);
            }
            lstBranch.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("ADMPFeePaymentConfiguration.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }
    //</1.0.1> 

    private void MultipleBranchBind(int Degree, int UGPGOT)
    {
        try
        {
            DataSet ds = null;
            ds = objADMAPPR.GetBranch(Degree, UGPGOT);

            lstBranch.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstBranch.DataSource = ds;
                lstBranch.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstBranch.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstBranch.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPFeePaymentConfiguration.MultipleBranchBind --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void txtOfficeVisitEndDate_TextChanged(object sender, EventArgs e)
    {
        ValidateOfficeVisitEndDate();
    }

    protected void ValidateOfficeVisitEndDate()
    {
        try
        {
            if (txtOfficeVisitStartDate.Text != string.Empty)
            {
                DateTime OfficeStartDate = Convert.ToDateTime(txtOfficeVisitStartDate.Text);
                DateTime OfficeEndDate = Convert.ToDateTime(txtOfficeVisitEndDate.Text);
                if (OfficeEndDate < OfficeStartDate)
                {
                    objCommon.DisplayMessage(updSession, "Office Report End Date should be greater than or equal to Office Report Start Date", this.Page);
                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPFeePaymentConfiguration.OfficeVisitEndDate --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void txtOfficeVisitStartDate_TextChanged(object sender, EventArgs e)
    {
        ValidateOfficeVisitStartDate();
    }

    protected void ValidateOfficeVisitStartDate()
    {
        try
        {
            DateTime selectedDate;
            if (DateTime.TryParse(txtOfficeVisitStartDate.Text, out selectedDate))
            {
                if (selectedDate < DateTime.Today)
                {

                    objCommon.DisplayMessage(updSession, "Office Report Start Date should be today/future date", this.Page);
                    
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPFeePaymentConfiguration.OfficeVisitStartDate --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        StartDate();
    }

    protected void StartDate()
    {
        try
        {
            if (txtOfficeVisitEndDate.Text != string.Empty)
            {
                DateTime OfficeStartDate = Convert.ToDateTime(txtOfficeVisitEndDate.Text);
                DateTime OfficeEndDate = Convert.ToDateTime(txtStartDate.Text);
                if (OfficeEndDate < OfficeStartDate)
                {
                    objCommon.DisplayMessage(updSession, "Payment Start Date should be greater than or equal to Office Report End Date", this.Page);
                  
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPFeePaymentConfiguration.StartDate --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ProvisionalAdmissionValidDate()
    {
        try
        {
            if (txtEndDate.Text != string.Empty)
            {
                DateTime enddate = Convert.ToDateTime(txtEndDate.Text);

                DateTime prodate = Convert.ToDateTime(txtProvisionalAdmissionValidDate.Text);

                if (prodate < enddate)
                {
                    objCommon.DisplayMessage(updSession, "Provisional Admission Offer Valid Date should be greater than or equal to Payment End Date", this.Page);
                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPFeePaymentConfiguration.ProvisionalAdmissionValidDate --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void txtProvisionalAdmissionValidDate_TextChanged1(object sender, EventArgs e)
    {
        ProvisionalAdmissionValidDate();
    }
}




