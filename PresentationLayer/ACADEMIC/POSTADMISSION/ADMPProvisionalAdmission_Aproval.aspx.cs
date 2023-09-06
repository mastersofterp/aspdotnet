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

public partial class ACADEMIC_POSTADMISSION_ADMPProvisionalAdmission_Aproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPProvisionalAdmissionAprovalController objADMAPPR = new ADMPProvisionalAdmissionAprovalController();

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
    
            this.FillDropdown();        ;
         
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

            btnShow.Visible = true;
            btnSubmit.Visible = false;
            //btnGenerateAdmissionNote.Visible = false;
            //btnSendEMail.Visible = false;
            //btnPrintAdmissionNote.Visible = false;

            pnlGV1.Visible = false;
            pnlCount.Visible = false;
            pnlPaymentCat.Visible = false;

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
        string SpeckStatus = objCommon.LookUp("ACD_DEGREE_SPECIALIZATION_MAPPING", "SPECIALIZATION", "DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue));

        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);

        int UGPGOT = Convert.ToInt16(ddlProgramType.SelectedValue);
        MultipleCollegeBind(Degree, UGPGOT);

        btnShow.Visible = true;
        btnSubmit.Visible = false;
        //btnGenerateAdmissionNote.Visible = false;
        //btnSendEMail.Visible = false;
        //btnPrintAdmissionNote.Visible = false;

        pnlGV1.Visible = false;
        pnlCount.Visible = false;
        pnlPaymentCat.Visible = false;
        if (SpeckStatus == "Not Applicable")
        {
            //lblSubProgBranch.Visible= false;
            pnlProg.Visible = false;
            lstProgram.Visible = false;
        }
        else
        {
           // lblSubProgBranch.Visible = true;
            pnlProg.Visible = true;
            lstProgram.Visible = true;
        }

       
    }

    private void MultipleCollegeBind(int Degree, int UGPGOT)
    {
        try
        {
            DataSet ds = null;
            ds = objADMAPPR.GetBranch(Degree, UGPGOT);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                txtPaymentCatgory.Text = ds.Tables[1].Rows[0]["Payment_Category"].ToString();
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
              string SpeckStatus = objCommon.LookUp("ACD_DEGREE_SPECIALIZATION_MAPPING", "SPECIALIZATION", "DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue));

            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upProvisionalADM, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upProvisionalADM, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "" && SpeckStatus != "Not Applicable")
            {
                objCommon.DisplayMessage(upProvisionalADM, "Please Select Branch/Program.", this.Page);
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
            ds = objADMAPPR.GetProvisionalStudent(ADMBATCH, ProgramType, DegreeNo, branchno,0);

            //lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlGV1.Visible = true;
                pnlPaymentCat.Visible = true;
                btnShow.Visible = false;
                btnSubmit.Visible = true;
                pnlCount.Visible = true;
                //lvSchedule.Visible = true;
                //lvSchedule.DataSource = ds;
                //lvSchedule.DataBind();
                gvProvADM.DataSource = ds;
                gvProvADM.DataBind();
                txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
                txtPaidStudent.Text = ds.Tables[0].AsEnumerable().Where(c => Convert.ToInt32(c["Paystatus"]) == 1).ToList().Count.ToString();
                txtUnPaidStudent.Text = ds.Tables[0].AsEnumerable().Where(c => Convert.ToInt32(c["Paystatus"]) == 0).ToList().Count.ToString();
              
                //btnGenerateAdmissionNote.Visible=true;
                //btnSendEMail.Visible=true;
                //btnPrintAdmissionNote.Visible = true;
            }
            else 
            {

                objCommon.DisplayMessage(upProvisionalADM, "No Record Found.", this.Page);
                gvProvADM.DataSource = null;
                gvProvADM.DataBind();
                pnlCount.Visible = false;

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
    //protected void lvSchedule_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);

    //        //var List2QuoteSubProject = e.Item.FindControl("ddlBranch") as DropDownList;
    //        DropDownList ddl = e.Item.FindControl("ddlBranch") as DropDownList;
    //        DropDownList ddlPayment = e.Item.FindControl("ddlPayment") as DropDownList;
            

    //        var UserNO = e.Item.FindControl("hdfUserNo") as HiddenField;
    //        BindBranch(ADMBATCH, Convert.ToInt32(UserNO.Value),ddl,ddlPayment);


    //    }
    //}

    protected void gvProvADM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
                int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
               

                DropDownList ddlBranch = (e.Row.FindControl("ddlBranch") as DropDownList);
                DropDownList ddlPayment = (e.Row.FindControl("ddlPayment") as DropDownList);
                CheckBox chkRecon = (e.Row.FindControl("chkRecon") as CheckBox);
                if (chkRecon.Checked == true)
                {
                    chkRecon.Enabled = false;
                }
                else
                {
                    chkRecon.Enabled = true;
                }
                var UserNO = (e.Row.FindControl("hdfUserNo") as HiddenField);
                var BRANCHNO = (e.Row.FindControl("hdfBranchNo") as HiddenField);
                var PaymentTypeNo = (e.Row.FindControl("hdfPaymentTypeNo") as HiddenField);  
            
              
                //DropDownList ddl = e.Item.FindControl("ddlBranch") as DropDownList;
                //DropDownList ddlPayment = e.Item.FindControl("ddlPayment") as DropDownList;


                //var UserNO = e.Item.FindControl("hdfUserNo") as HiddenField;
                BindBranch(ADMBATCH, Convert.ToInt32(UserNO.Value), ddlBranch, ddlPayment, Convert.ToInt32(BRANCHNO.Value), Convert.ToInt32(PaymentTypeNo.Value));
          
        }  
    }

    private void BindBranch(int ADMBATCH, int UserNO, DropDownList ddlBranch, DropDownList ddlPayment, int BranchNO, int PaymentTypeNo )
    {
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
        ds = objADMAPPR.GetProvisionalStudent(ADMBATCH, 0, DegreeNo, branchno, UserNO);
        string ChkSpec = objCommon.LookUp("ACD_DEGREE_SPECIALIZATION_MAPPING", "SPECIALIZATION", "DEGREE=" + Convert.ToInt32(ddlDegree.SelectedIndex));
        if (DegreeNo == 7)
        {
            ddlBranch.DataSource = ds.Tables[1];
            ddlBranch.DataTextField = "LongName";
            ddlBranch.DataValueField = "BRANCHNO";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
            ddlBranch.Items.FindByValue(BranchNO.ToString()).Selected = true;
        }
        else
        {
            if (ChkSpec == "Not Applicable")
            {
                ddlBranch.DataSource = ds.Tables[1];
                ddlBranch.DataTextField = "LongName";
                ddlBranch.DataValueField = "BRANCHNO";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
                ddlBranch.Items.FindByValue(BranchNO.ToString()).Selected = true;
            }
            else
            {
                ddlBranch.DataSource = ds.Tables[1];
                ddlBranch.DataTextField = "LongName";
                ddlBranch.DataValueField = "BRANCHNO";
                ddlBranch.DataBind();
            }

            //ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
            //ddlBranch.Items.FindByValue(BranchNO.ToString()).Selected = true;
        }

        if (DegreeNo != 7)
        {

            if (ds.Tables[2].Rows.Count > 0)
            {
                ddlPayment.DataSource = ds.Tables[2];
                ddlPayment.DataTextField = "PAYTYPENAME";
                ddlPayment.DataValueField = "PAYTYPENO";
                ddlPayment.DataBind();
                ddlPayment.Items.Insert(0, new ListItem("Select Payment Type", "0"));

                ddlPayment.Items.FindByValue(PaymentTypeNo.ToString()).Selected = true;
            
            }
            else
            {
               

                objCommon.DisplayMessage(this.Page, "Standard fee is not created for this branch.", this.Page);
                clearfileds();

            }
          //  BindPaymentCatForOther(ddlBranch, PaymentTypeNo);

        }
        else
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                ddlPayment.DataSource = ds.Tables[2];
                ddlPayment.DataTextField = "PAYTYPENAME";
                ddlPayment.DataValueField = "PAYTYPENO";
                ddlPayment.DataBind();
                ddlPayment.Items.Insert(0, new ListItem("Select Payment Type", "0"));

                ddlPayment.Items.FindByValue(PaymentTypeNo.ToString()).Selected = true;
            }
            else
            {
                

                objCommon.DisplayMessage(this.Page, "Standard fee is not created for this branch.", this.Page);
                clearfileds();
            }

        }

       

      
    }

    private void BindPaymentCatForOther(DropDownList ddlBranch, int PaymentTypeNo)
    {
        try
        {
            DataSet ds = new DataSet();

            DropDownList ddlBranch_ = (DropDownList)ddlBranch;
            GridViewRow row = (GridViewRow)ddlBranch_.NamingContainer;

            DropDownList ddlPayment = (DropDownList)row.FindControl("ddlPayment");
            ddlPayment.Enabled = true;
            ddlPayment.SelectedIndex = 0;

            HiddenField hdfBatchNo = (HiddenField)row.FindControl("hdfBatchNo");
            HiddenField hdfDegreeNo = (HiddenField)row.FindControl("hdfDegreeNo");
            HiddenField hdfUserNo = (HiddenField)row.FindControl("hdfUserNo");
            //DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");

            int ADMBATCH = Convert.ToInt32(hdfBatchNo.Value);
            int DegreeNo = Convert.ToInt32(hdfDegreeNo.Value);
            string BranchNo = ddlBranch.SelectedValue;
            int UserNO = Convert.ToInt32(hdfUserNo.Value);

            ds = objADMAPPR.GetProvisionalStudent(ADMBATCH, 0, DegreeNo, BranchNo, UserNO);

           
            if (ds.Tables[2].Rows.Count > 0)
            {
                ddlPayment.DataSource = ds.Tables[2];
                ddlPayment.DataTextField = "PAYTYPENAME";
                ddlPayment.DataValueField = "PAYTYPENO";
                ddlPayment.DataBind();
                ddlPayment.Items.Insert(0, new ListItem("Select Payment Type", "0"));
                ddlPayment.SelectedIndex = 0;
                if (PaymentTypeNo > 0)
                {
                    //ddlPayment.Items.FindByValue(PaymentTypeNo.ToString()).Selected = true;
                }
                
            }

            //ddlPayment.Items.FindByValue(PaymentTypeNo.ToString()).Selected = true;
           
        }
        catch(Exception ex)
        {
        
        }
    }




    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TextBox box1 = (TextBox)GridView1.Rows[e.currentrow].Cells[2].FindControl("lblPayment");
        //DropDownList dl = (DropDownList)GridView1.Rows[e.currentrow].Cells[2].FindControl("DropDownProducts");

        //box1.Text = dl.SelectedValue.ToString();


        //var payment = (Label)lvSchedule.FindControl("lblPayment");

        //payment.Text = "10000";

    }

    protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
    {

        //int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
        //int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);  

        DropDownList ddlPayment = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlPayment.NamingContainer;
        int PAYTYPENO = Convert.ToInt32(ddlPayment.SelectedValue);

        HiddenField hdfBatchNo = (HiddenField)row.FindControl("hdfBatchNo");
        HiddenField hdfDegreeNo = (HiddenField)row.FindControl("hdfDegreeNo");
        DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");

        int ADMBATCH = Convert.ToInt32(hdfBatchNo.Value);
        int DegreeNo = Convert.ToInt32(hdfDegreeNo.Value);
        int BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);

        DataSet ds = null;
        ds = objADMAPPR.GetPayableAmount(ADMBATCH, DegreeNo, BranchNo, PAYTYPENO);


        if (ds.Tables.Count != 0)
        {
            Label lblAmount = (Label)row.FindControl("lblAmount");
            HiddenField hdnSatndardFee = (HiddenField)row.FindControl("hdnStandardFee");

            lblAmount.Text = ds.Tables[0].Rows[0]["PayableAmount"].ToString();
            hdnSatndardFee.Value = ds.Tables[1].Rows[0]["StandardFee"].ToString();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Fee Payment Is Not Configured For this Degree.", this.Page);
            return;
        }

    }
    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        DropDownList ddlBranch_ = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlBranch_.NamingContainer;

        DropDownList ddlPayment = (DropDownList)row.FindControl("ddlPayment");
        ddlPayment.Enabled = true;
        ddlPayment.SelectedIndex = 0;

        HiddenField hdfBatchNo = (HiddenField)row.FindControl("hdfBatchNo");
        HiddenField hdfDegreeNo = (HiddenField)row.FindControl("hdfDegreeNo");
        HiddenField hdfUserNo = (HiddenField)row.FindControl("hdfUserNo");
        DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
        Label lblAmount = (Label)row.FindControl("lblAmount");

        int ADMBATCH = Convert.ToInt32(hdfBatchNo.Value);
        int DegreeNo = Convert.ToInt32(hdfDegreeNo.Value);
        string BranchNo = ddlBranch.SelectedValue;
        int UserNO = Convert.ToInt32(hdfUserNo.Value);

        ds = objADMAPPR.GetProvisionalStudent(ADMBATCH, 0, DegreeNo, BranchNo, UserNO);

        if (ds.Tables[2].Rows.Count > 0)
        {
            ddlPayment.DataSource = ds.Tables[2];
            ddlPayment.DataTextField = "PAYTYPENAME";
            ddlPayment.DataValueField = "PAYTYPENO";
            ddlPayment.DataBind();
            ddlPayment.Items.Insert(0, new ListItem("Select Payment Type", "0"));
            ddlPayment.SelectedIndex = 0;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Standard fee is not created for this branch.", this.Page);
            ddlPayment.Enabled = false;
            ddlPayment.SelectedIndex = 0;
            lblAmount.Text = "";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearfileds();
    }

    private void clearfileds()
    {
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        lstProgram.Items.Clear();
        pnlGV1.Visible = false;
        pnlCount.Visible = false;
        pnlPaymentCat.Visible = false;

        btnShow.Visible = true;
        btnSubmit.Visible = false;
        pnlProg.Visible = false;
        //btnGenerateAdmissionNote.Visible = false;
        //btnSendEMail.Visible = false;
        //btnPrintAdmissionNote.Visible = false;

    }

    //protected void btnGenerateAdmissionNote_Click(object sender, EventArgs e)
    //{
        
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int UpdCnt = 0;
            int ret = 0;
            int USERNO = 0;
            int ADMBATCH = 0;
            string APPLICATIONID;
            string STUDNAME;
            int DEGREENO = 0;
            int BRANCHNO = 0;
            int PAYMENTTYPE = 0;
            double AMOUNT;
            double STANDARDFEE = 0;
            int CREATEDBY = 0;
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            foreach (GridViewRow dataRow in gvProvADM.Rows)
            {
                CheckBox chkRec = dataRow.FindControl("chkRecon") as CheckBox;

                if (chkRec.Checked == true && chkRec.Enabled !=false)
                {
                    
                    HiddenField UserNo = dataRow.FindControl("hdfUserNo") as HiddenField;
                    HiddenField IDNO = dataRow.FindControl("hdfIDNO") as HiddenField;
                    HiddenField BatchNo = dataRow.FindControl("hdfBatchNo") as HiddenField;
                    Label ApplicationId = dataRow.FindControl("lblApplicationId") as Label;
                    HiddenField DegreeNo = dataRow.FindControl("hdfDegreeNo") as HiddenField;
                    DropDownList ddlBranch = (DropDownList)dataRow.FindControl("ddlBranch");
                    DropDownList ddlPayment = (DropDownList)dataRow.FindControl("ddlPayment");
                    Label lblAmount = dataRow.FindControl("lblAmount") as Label;
                    Label lblStudentName = dataRow.FindControl("lblStudentName") as Label;
                    HiddenField StandardFee = dataRow.FindControl("hdnStandardFee") as HiddenField;

                    if (ddlBranch.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Branch.", this.Page);
                        return;
                    }
                    if(ddlPayment.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Payment Type.", this.Page);
                        return;
                    }
                    if (lblAmount.Text == "" || lblAmount.Text==string.Empty)
                    {
                        objCommon.DisplayMessage(this.Page, "Payment amount should not be blank,\n please check standard fee create or not.", this.Page);
                        return;
                    }
                    USERNO = Convert.ToInt32(UserNo.Value);
                    ADMBATCH = Convert.ToInt32(BatchNo.Value);
                    APPLICATIONID = ApplicationId.Text;
                    DEGREENO = Convert.ToInt32(DegreeNo.Value);
                    BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
                    PAYMENTTYPE = Convert.ToInt32(ddlPayment.SelectedValue);
                    AMOUNT = Convert.ToDouble(lblAmount.Text.ToString());
                    STUDNAME = lblStudentName.Text.ToString();
                    CREATEDBY = int.Parse(Session["userno"].ToString());
                    if (StandardFee.Value == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Standard fees is not created for this branch, Please create standard fee.", this.Page);
                        return;
                    }
                    else
                    {
                        STANDARDFEE = Convert.ToDouble(StandardFee.Value);
                    }

                    ret = objADMAPPR.Create_ProAdmission_Demand(USERNO, ADMBATCH, APPLICATIONID, DEGREENO, BRANCHNO, PAYMENTTYPE, AMOUNT, STANDARDFEE, CREATEDBY, ipAddress, STUDNAME);

                    UpdCnt++;
                    //ret = objADMAPPR.Create_ProAdmission_Online_Demand(USERNO, ADMBATCH, APPLICATIONID, DEGREENO, BRANCHNO, PAYMENTTYPE, AMOUNT, STANDARDFEE, CREATEDBY, ipAddress);
                }
            }
            if (ret > 0 && UpdCnt > 0)
            {
                if (ret == 1)
                {

                    objCommon.DisplayMessage(this.Page, "Provisional Admission Approval Successfully With Demand.", this.Page);
                    btnShow_Click(sender, e);
                    return;
                }
                else if (ret == 2)
                {
                    objCommon.DisplayMessage(this.Page, "Provisional Admission Approval Successfully With Demand.", this.Page);
                    btnShow_Click(sender, e);
                    return;
                }

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Plase Select Al least One Student.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EntranceExamMarkEntry.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void chkRecon_CheckedChanged(object sender, EventArgs e)
    {
        string SpeckStatus=objCommon.LookUp("ACD_DEGREE_SPECIALIZATION_MAPPING","SPECIALIZATION","DEGREE="+ Convert.ToInt32(ddlDegree.SelectedValue));
        if (ddlDegree.SelectedValue == "7")
        {
            DataSet ds = new DataSet();

            CheckBox chkRecon = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkRecon.NamingContainer;

            if (chkRecon.Checked == true)
            {
                DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                ddlBranch.Enabled = true;
                ddlBranch.SelectedIndex = 0;
            }
            else
            {
                DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                DropDownList ddlPayment = (DropDownList)row.FindControl("ddlPayment");
                Label lblAmount = (Label)row.FindControl("lblAmount");
                ddlBranch.Enabled = false;
                ddlBranch.SelectedIndex = 0;

                ddlPayment.Enabled = false;
                ddlPayment.SelectedIndex = 0;
                lblAmount.Text = string.Empty;

            }
        }
        else if (SpeckStatus == "Not Applicable")
        {
            DataSet ds = new DataSet();

            CheckBox chkRecon = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkRecon.NamingContainer;

            if (chkRecon.Checked == true)
            {
                DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                ddlBranch.Enabled = true;
                ddlBranch.SelectedIndex = 0;
            }
            else
            {
                DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                DropDownList ddlPayment = (DropDownList)row.FindControl("ddlPayment");
                Label lblAmount = (Label)row.FindControl("lblAmount");
                ddlBranch.Enabled = false;
                ddlBranch.SelectedIndex = 0;

                ddlPayment.Enabled = false;
                ddlPayment.SelectedIndex = 0;
                lblAmount.Text = string.Empty;

            }
        }
        else
        {
            DataSet ds = new DataSet();

            CheckBox chkRecon = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkRecon.NamingContainer;

            if (chkRecon.Checked == true)
            {
                //DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                //ddlBranch.Enabled = true;
                //ddlBranch.SelectedIndex = 0;
                DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                DropDownList ddlPayment = (DropDownList)row.FindControl("ddlPayment");
                Label lblAmount = (Label)row.FindControl("lblAmount");
                ddlBranch.Enabled = true;
                ddlBranch.SelectedIndex = 0;

                ddlPayment.Enabled = true;
                ddlPayment.SelectedIndex = 0;
                lblAmount.Text = string.Empty;

            }
            else
            {
                DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBranch");
                DropDownList ddlPayment = (DropDownList)row.FindControl("ddlPayment");
                Label lblAmount = (Label)row.FindControl("lblAmount");
                ddlBranch.Enabled = false;
                ddlBranch.SelectedIndex = 0;

                ddlPayment.Enabled = false;
                ddlPayment.SelectedIndex = 0;
                lblAmount.Text = string.Empty;

            }
        }
    }

    protected void ddlAdmissionBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearfileds();
    }
}


///PKG_FEECOLLECT_CREATE_ONLINE_DEMAND_BULK_FOR_SELECTED_STUDENTS
///PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS