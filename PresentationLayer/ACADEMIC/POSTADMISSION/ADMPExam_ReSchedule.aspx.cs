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

public partial class Exam_ReSchedule : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPExamRescheduleController objReschedule = new ADMPExamRescheduleController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Check User Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["colcode"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH DESC");
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH ASC");
                ddlAdmissionBatch.SelectedIndex = 1;
            }
        }
    }


    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlCount.Visible = false;
            pnllistView.Visible = false;
            lstProgram.Items.Clear();
            ddlExamSlot.Items.Clear();
            ddlNewExamSchedule.Items.Clear();
            //ddlExamSlot.DataSource = null;
            //ddlExamSlot.DataBind();

            if (ddlProgramType.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Exam_Hall_Ticket.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamSlot.Items.Clear();
        ddlNewExamSchedule.Items.Clear();
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }
    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objReschedule.GetBranch(Degree);

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

    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlNewExamSchedule.Items.Clear();
            DataSet ds = null;
            int DegreeId = Convert.ToInt32(ddlDegree.SelectedValue);
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();
            string Branch = ddlProgramType.SelectedValue;
            ds = objReschedule.GetSChedule(DegreeId, branchno, ADMBATCH);

            ddlExamSlot.Items.Clear();
            ddlNewExamSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlExamSlot.DataSource = ds;
                ddlExamSlot.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlExamSlot.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlExamSlot.DataBind();
                ddlExamSlot.Items.Insert(0, new ListItem("Select Schedule", "0"));
                ddlExamSlot.SelectedIndex = 0;

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
                objCommon.DisplayMessage(updReSchedule, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updReSchedule, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(updReSchedule, "Please Select Branch/Program.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" || ddlExamSlot.SelectedValue == "")
            {
                objCommon.DisplayMessage(updReSchedule, "Please Select Exam Slot.", this.Page);
                return;
            }           
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            //string Schedule = ddlExamSlot.SelectedItem.Text;
            string Schedule = ddlExamSlot.SelectedValue;
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = objReschedule.GetAdmitCardStudent(ADMBATCH, ProgramType, DegreeNo, branchno, Schedule);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlCount.Visible = true;
                pnllistView.Visible = true;
                pnlReschedule.Visible = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                lvSchedule.DataBind();
                txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(updReSchedule, "No Recored Found.", this.Page);
                pnlCount.Visible = false;
                pnllistView.Visible = false;
                pnlReschedule.Visible = false;
                lvSchedule.Visible = false;
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
                txtTotalCount.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnRescheduleExam_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlNewExamSchedule.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updReSchedule, "Please Select New Schedule.", this.Page);
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
            bool IsReschedule = true;
            int PrevScheduleNo = Convert.ToInt32(ddlExamSlot.SelectedValue);
            string PrevSchedule = ddlExamSlot.SelectedItem.Text;

            foreach (ListViewDataItem lvItem in lvSchedule.Items)
            {

                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    userNo = Convert.ToInt32(hdnUserNo.Value);
                }

                if (chkBox.Checked && chkBox.Enabled == true)
                {
                    //rollno = hdfRollno.Value;
                    chkCount++;
                    CustomStatus cs = (CustomStatus)objReschedule.Insert_Update_Reschedule(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), userNo, ipaddress, ua_no, ddlNewExamSchedule.SelectedItem.Text, Convert.ToInt32(ddlNewExamSchedule.SelectedValue), IsReschedule, PrevSchedule, PrevScheduleNo);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        updCount++;
                    }
                }
            }
            if (chkCount == 0)
            {
                //objCommon.DisplayMessage(this.Page, "Please Select At Least One Student.", this.Page);
                objCommon.DisplayMessage(updReSchedule, "Please Select At Least One Student.", this.Page);
                return;
            }
            if (chkCount > 0 && chkCount == updCount)
            {
                //objCommon.DisplayMessage("Admit Card Generated Successfully for Selected Students.", this.Page);
                objCommon.DisplayMessage(updReSchedule, "Reschedule Exam Successfully", this.Page);
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
        pnlCount.Visible = false;
        pnllistView.Visible = false;
        pnlReschedule.Visible = false;
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlExamSlot.Items.Clear();
        ddlNewExamSchedule.Items.Clear();
        lstProgram.Items.Clear();
    }
    protected void ddlExamSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            int DegreeId = Convert.ToInt32(ddlDegree.SelectedValue);
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();
            string Branch = ddlProgramType.SelectedValue;
            ds = objReschedule.GetReschedule(DegreeId, branchno, ADMBATCH, ddlExamSlot.SelectedItem.Value);

            ddlNewExamSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlNewExamSchedule.DataSource = ds;
                ddlNewExamSchedule.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlNewExamSchedule.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlNewExamSchedule.DataBind();
                ddlNewExamSchedule.Items.Insert(0, new ListItem("Select Schedule", "0"));
                ddlNewExamSchedule.SelectedIndex = 0;

            }
        }
        catch
        {
            throw;
        }

    }
}