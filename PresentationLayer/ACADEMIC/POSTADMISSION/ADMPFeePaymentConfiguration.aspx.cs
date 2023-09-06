using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessLogic.PostAdmission;
using BusinessLogicLayer.BusinessEntities.Academic;

public partial class ADMPFeePaymentConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPFeePaymentConfigController objAFPCEC = new ADMPFeePaymentConfigController();
    ADMPFeePaymentConfigEntity objAFPCEE = new ADMPFeePaymentConfigEntity();

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
            //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "DISTINCT ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID > 0 AND ACTIVE_STATUS=1", "ACADMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            //objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0 AND ACTIVESTATUS=1", "UA_SECTION DESC");

            ////objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO >0 AND ACTIVESTATUS=1", "DEGREENO");
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


            //objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0 AND ACTIVESTATUS=1 AND UA_SECTIONNAME IN(" + whereData + ")", "UA_SECTION DESC");
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
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            ////ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
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
            DataSet ds = objAFPCEC.GetRetADMPFeePayConfigListData(FeePayConfig_ID);

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

                //if (ddlDegree.Items.Count > 1)
                //{
                //    ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                //}
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");

                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();

                txtStartDate.Text = ds.Tables[0].Rows[0]["STARTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");
                txtEndDate.Text = ds.Tables[0].Rows[0]["ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");

                if (ddlPaymentCategory.Items.Count > 1)
                {
                    ddlPaymentCategory.SelectedValue = ds.Tables[0].Rows[0]["PAYMENTCATEGORY"].ToString();
                }

                setAmountLabels();
                txtAmount.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["FEEPAYMENT"].ToString()).ToString();

                /*if (ds.Tables[0].Rows[0]["ACTIVITYSTATUS"].ToString() == "True")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCreateEvent(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCreateEvent(false);", true);
                }*/

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

    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            objAFPCEE.ConfigID = Convert.ToInt32(ViewState["FeePayConfig_ID"]);
            objAFPCEE.Activityfor = Convert.ToInt32(rdoActivityFor.SelectedValue.Equals("1") ? "1" : "2");
            objAFPCEE.Admbatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objAFPCEE.Programtype = Convert.ToInt32(ddlProgramType.SelectedValue);
            objAFPCEE.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objAFPCEE.Startdate = Convert.ToDateTime(txtStartDate.Text.ToString().Trim());
            objAFPCEE.Enddate = Convert.ToDateTime(txtEndDate.Text.ToString().Trim());
            objAFPCEE.Paymentcategory = Convert.ToInt32(ddlPaymentCategory.SelectedValue);
            if (ddlPaymentCategory.SelectedValue.ToString().Equals("3"))
            {
                objAFPCEE.Feepayment = Convert.ToDouble(0.0);
            }
            else
            {
                objAFPCEE.Feepayment = Convert.ToDouble(txtAmount.Text.ToString().Trim());
            }

            if (hfdActive.Value == "true")
            {
                objAFPCEE.Activitystatus = true;
            }
            else
            {
                objAFPCEE.Activitystatus = false;
            }

            int ret = 0;
            string displaymsg = "Recored added successfully.";
            if (ViewState["action"].ToString().Equals("add"))
            {
                ret = Convert.ToInt32(objAFPCEC.InsertADMPFeePayConfig(objAFPCEE));
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                displaymsg = "Recored updated successfully.";
                ret = Convert.ToInt32(objAFPCEC.UpdateADMPFeePayConfig(objAFPCEE));
            }

            if (ret == 2)
            {
                displaymsg = "Recored already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                setDefaultValues();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
        }


    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
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
            ddlDegree.ClearSelection();
            ////ddlDegree.Items.Clear();
            divAmount.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
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
        if (txtStartDate.Text != string.Empty)
        {
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            if (EndDate < StartDate)
            {
                objCommon.DisplayMessage(updSession, "End Date Should Be Graeter Than Start Date.", this.Page);
                //txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;

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
                txtAmount.Text="";
            }
        }
    }
}