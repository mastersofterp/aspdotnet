using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMP_RemainingPayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPRemainingPaymentController objRemainPayment = new ADMPRemainingPaymentController();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Sessionbtnsubmi
            // Session["OrgId"] = "7";
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }
            //if (Session["OrgId"].ToString() == "2")
            //{
            //    pnlDegree.Visible = true;
            //    pnlCenter.Visible = false;
            //}
            //else if (Session["OrgId"].ToString() == "7")
            //{
            //    pnlDegree.Visible = false;
            //    pnlCenter.Visible = true;
            //}
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

            this.FillDropdown(); ;

        }
    }
    #endregion Page Load



    private void FillDropdown()
    {
        // objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlAdmissionBatch.SelectedIndex = 0;
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
            ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();

            //btnShow.Visible = true;
            //btnSubmit.Visible = false;
            //btnGenerateAdmissionNote.Visible = false;
            //btnSendEMail.Visible = false;
            //btnPrintAdmissionNote.Visible = false;

            pnlGV1.Visible = false;
       

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMPProvisionalAdmission_Aproval.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        int UGPGOT = Convert.ToInt16(ddlProgramType.SelectedValue);
        MultipleCollegeBind(Degree, UGPGOT);

        //btnShow.Visible = true;
        //btnSubmit.Visible = false;
        //btnGenerateAdmissionNote.Visible = false;
        //btnSendEMail.Visible = false;
        //btnPrintAdmissionNote.Visible = false;

        pnlGV1.Visible = false;

    }

    private void MultipleCollegeBind(int Degree, int UGPGOT)
    {
        try
        {
            DataSet ds = null;
            ds = objRemainPayment.GetBranch(Degree, UGPGOT);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }

           
        }
        catch
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upRemainingPayment, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upRemainingPayment, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upRemainingPayment, "Please Select Branch/Program.", this.Page);
                return;
            }
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd(); ACD_ADMP_PAYMENT_CONFIGURATION
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = objRemainPayment.GetStudentList(ADMBATCH, ProgramType, DegreeNo, branchno);

            //lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlGV1.Visible = true;               

                lvStudent.DataSource = ds;
                lvStudent.DataBind();

               

                btnShow.Visible = false;
                btnSubmit.Visible = true;
                //btnGenerateAdmissionNote.Visible = true;
                //btnSendEMail.Visible = true;
                //btnPrintAdmissionNote.Visible = true;
            }
            else
            {

                objCommon.DisplayMessage(upRemainingPayment, "No Recored Found.", this.Page);     
               
                lvStudent.DataSource =null;
                lvStudent.DataBind();

                btnShow.Visible = true;
                btnSubmit.Visible = false;
                //btnGenerateAdmissionNote.Visible = false;
                //btnSendEMail.Visible = false;
                //btnPrintAdmissionNote.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upRemainingPayment, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upRemainingPayment, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upRemainingPayment, "Please Select Branch/Program.", this.Page);
                return;
            }

        
            string ipaddress = string.Empty;

            string rollno = string.Empty;
            int chkCount = 0;
            int updCount = 0;

            ipaddress = Request.ServerVariables["REMOTE_HOST"];

            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int userNo = 0;
          
            string rollNo = string.Empty;
            bool IsReschedule = false;

            foreach (ListViewDataItem lvItem in lvStudent.Items)
            {

                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
                HiddenField hdnDegreeNo = lvItem.FindControl("hdnDegreeNo") as HiddenField;
                HiddenField hdnBranchNo = lvItem.FindControl("hdnBranchNo") as HiddenField;
                //Label lblPaidAmt = lvItem.FindControl("lblPaidAmt") as Label;
                Label lblDemand = lvItem.FindControl("lblDemand") as Label;
                Label lblOutStandingAmt = lvItem.FindControl("lblOutStandingAmt") as Label;
                Label lblApplicationId = lvItem.FindControl("lblApplicationId") as Label;

                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    userNo = Convert.ToInt32(hdnUserNo.Value);
                }

                if (chkBox.Checked && chkBox.Enabled == true)
                {
                    //rollno = hdfRollno.Value;
                    chkCount++;
                    CustomStatus cs = (CustomStatus)objRemainPayment.InsertRemaingPayment(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(hdnDegreeNo.Value), Convert.ToInt32(hdnBranchNo.Value), userNo,  Convert.ToDecimal(lblDemand.Text), Convert.ToDecimal(lblOutStandingAmt.Text), ipaddress, ua_no, Convert.ToInt32(lblApplicationId.Text), Convert.ToInt32(ua_no));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        updCount++;
                        //SendEmail(hdnEmailId.Value, lblStudName.Text);
                    }
                }
            }
            if (chkCount == 0)
            {
                //objCommon.DisplayMessage(this.Page, "Please Select At Least One Student.", this.Page);
                objCommon.DisplayMessage(upRemainingPayment, "Please Select At Least One Student.", this.Page);
                btnShow_Click(sender, e);
                return;
            }
            if (chkCount > 0 && chkCount == updCount)
            {
                //objCommon.DisplayMessage("Admit Card Generated Successfully for Selected Students.", this.Page);
                objCommon.DisplayMessage(upRemainingPayment, "Recored Saved Successfully.", this.Page);
                btnShow_Click(sender, e);
                //this.BindData();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    private void clearFields()
    {
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        lstProgram.Items.Clear();
        pnlGV1.Visible = false;
        btnShow.Visible = true;
        btnSubmit.Visible = false;
    }
}