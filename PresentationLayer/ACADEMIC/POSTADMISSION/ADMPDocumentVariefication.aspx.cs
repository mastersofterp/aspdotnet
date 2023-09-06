using BusinessLogicLayer.BusinessEntities.Academic.PoastAdmission;
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

public partial class ACADEMIC_POSTADMISSION_ADMPDocumentVariefication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPDocumentVerificationController objCnrl = new ADMPDocumentVerificationController();
    DocumentVerification EntDocVerification = new DocumentVerification();

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
            }

            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

            this.FillDropdown();
        }
    }

    private void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlAdmissionBatch.SelectedIndex = 0;

    }
    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlGV1.Visible = false;
        ddlDegree.Items.Clear();
        lstProgram.Items.Clear();
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0", "D.DEGREENO");
        ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
        ddlDegree.SelectedIndex = 0;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlGV1.Visible = false;
        lstProgram.Items.Clear();
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }
    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objCnrl.GetBranch(Degree);

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
                objCommon.DisplayMessage(updDocumentVerification, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updDocumentVerification, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(updDocumentVerification, "Please Select Branch/Program.", this.Page);
                return;
            }
            EntDocVerification.ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            EntDocVerification.ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            EntDocVerification.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            branchno = branchno.TrimEnd(',').Trim();
            EntDocVerification.Branchno = branchno;

            DataSet ds = null;
            ds = objCnrl.StudentListFocVerification(EntDocVerification);


            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlGV1.Visible = true;
                gvDocVerification.Visible = true;
                gvDocVerification.DataSource = ds;
                gvDocVerification.DataBind();

            }
            else
            {
                objCommon.DisplayMessage(updDocumentVerification, "No Recored Found", this.Page);
                pnlGV1.Visible = false;
                gvDocVerification.Visible = false;
                gvDocVerification.DataSource = null;
                gvDocVerification.DataBind();
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
            string ipaddress = string.Empty;

            string rollno = string.Empty;
            int chkCount = 0;
            int updCount = 0;
            string time = "";
            ipaddress = Request.ServerVariables["REMOTE_HOST"];
           
              foreach (GridViewRow dataRow in gvDocVerification.Rows)
              {

                HiddenField HdnBatchNo = dataRow.FindControl("hdnAdmBatch") as HiddenField;
                HiddenField HdnDegreeNo = dataRow.FindControl("hdnDegreeNo") as HiddenField;
                HiddenField HdnBranchNo = dataRow.FindControl("hdnBranchNo") as HiddenField;
                HiddenField hdnUserNo = dataRow.FindControl("hdnUserNo") as HiddenField;
                DropDownList ddlStatus = dataRow.FindControl("ddlStatus") as DropDownList;
                CheckBox ChkCall = dataRow.FindControl("chkCallLrt") as CheckBox;

                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    EntDocVerification.USerNo = Convert.ToInt32(hdnUserNo.Value);
                }

                if (ChkCall.Checked == true)
                {
                    if (ddlStatus.SelectedValue == "2")
                    {
                        objCommon.DisplayMessage(updDocumentVerification, "Please Select Document Status Yes/NO", this.Page);
                        return;
                    }
                    else
                    {
                        EntDocVerification.ADMBATCH = Convert.ToInt32(HdnBatchNo.Value);
                        EntDocVerification.DegreeNo = Convert.ToInt32(HdnDegreeNo.Value);
                        EntDocVerification.Branchno = HdnBranchNo.Value.ToString();
                        EntDocVerification.USerNo = Convert.ToInt32(hdnUserNo.Value);
                        EntDocVerification.DocStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                        EntDocVerification.IpAdreess = ipaddress;
                        EntDocVerification.CreatedBy = Convert.ToInt32(Session["UserNo"]);

                        chkCount++;
                        CustomStatus cs = (CustomStatus)objCnrl.INSERT_UPDATE_DOCUMENTVERIFICATION(EntDocVerification);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            updCount++;
                        }
                    }
                }
            }
            if (chkCount == 0)
            {
                objCommon.DisplayMessage(updDocumentVerification, "Please Select At Least One Student.", this.Page);
                return;
            }
            if (chkCount > 0 && chkCount == updCount)
            {

                objCommon.DisplayMessage(updDocumentVerification, "Document Verified Successfully.", this.Page);
                btnShow_Click(sender, e);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_POSTADMISSION_ADMPDocumentVariefication.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void gvDocVerification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            DropDownList ddlStatus = (e.Row.FindControl("ddlStatus") as DropDownList);

            var DocStatus = (e.Row.FindControl("hdnStatus") as HiddenField);

            ddlStatus.Items.FindByValue(DocStatus.Value.ToString()).Selected = true;

            //BindDocStatus(ADMBATCH, Convert.ToInt32(UserNO.Value), ddlBranch, ddlPayment, Convert.ToInt32(BRANCHNO.Value), Convert.ToInt32(PaymentTypeNo.Value));

        }  
    }
    //private void BindDocStatus(int ADMBATCH, int UserNO, DropDownList ddlBranch, DropDownList ddlPayment, int BranchNO, int PaymentTypeNo)
    //{
        

    //    //ddlBranch.Items.FindByValue(BranchNO.ToString()).Selected = true;

    //    ddlPayment.DataSource = ds.Tables[2];
    //    ddlPayment.DataTextField = "PAYTYPENAME";
    //    ddlPayment.DataValueField = "PAYTYPENO";
    //    ddlPayment.DataBind();
    //    ddlPayment.Items.Insert(0, new ListItem("Select Payment Type", "0"));

    //    ddlPayment.Items.FindByValue(PaymentTypeNo.ToString()).Selected = true;


    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        lstProgram.Items.Clear();
        pnlGV1.Visible = false;
        gvDocVerification.Visible = false;
        gvDocVerification.DataSource = null;
        gvDocVerification.DataBind();
    }
}