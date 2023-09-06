using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exam_Student_Attendance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPExamStudentAttendaceController objAttendance = new ADMPExamStudentAttendaceController();


    #region Page Load
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
                Page.Title = Session["coll_name"].ToString();
                //Page Authorization
                //CheckPageAuthorization();
                //objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                // objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH DESC");
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
                ddlAdmissionBatch.SelectedIndex = 0;
                btnAttendance.Visible = false;
                // this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID");
                //objCommon.FillDropDownList(ddlCode, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "", "BRANCH_CODE");

            }
        }
    }
    #endregion Page Load


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
            lstProgram.Items.Clear();
            //pnlCount.Visible = false;
            pnlCount.Visible = false;
            ddlExamSlot.Items.Clear();
            pnllvSh.Visible = false;
            lvSchedule.DataSource = null;
            lvSchedule.DataBind();
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
        lstProgram.Items.Clear();
        //pnlCount.Visible = false;
        pnlCount.Visible = false;
        ddlExamSlot.Items.Clear();
        pnllvSh.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objAttendance.GetBranch(Degree);

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
            pnlCount.Visible = false;
            pnllvSh.Visible = false;
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
            ds = objAttendance.GetSChedule(DegreeId, branchno, ADMBATCH);

            ddlExamSlot.Items.Clear();
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
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Branch/Program.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 2)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Exam Slot.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 7)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Schedule Slot.", this.Page);
                return;
            }
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string Schedule = ddlExamSlot.SelectedValue;
            string branchno = string.Empty;

            btnAttendance.Visible = true;

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
            ds = objAttendance.GetAdmitCardStudent(ADMBATCH, ProgramType, DegreeNo, branchno, Schedule);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllvSh.Visible = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                lvSchedule.DataBind();
                pnlCount.Visible = true;
                txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString("0000");
                int listviewcount = lvSchedule.Items.Count;
              
              
            }
            else
            {
                //   lvSchedule.Items.Clear();
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
                pnlCount.Visible = false;
                objCommon.DisplayMessage(upAttendance, "Record Not Found", this.Page);
                btnAttendance.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Branch/Program.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 2)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Exam Slot.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 7)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Schedule Slot.", this.Page);
                return;
            }
            string ipaddress = string.Empty;

            string rollno = string.Empty;
            int chkCount = 0;
            int updCount = 0;

            ipaddress = Request.ServerVariables["REMOTE_HOST"];

            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int userNo = 0;
            int ACNO = 0;
            string rollNo = string.Empty;
            bool IsAttend;
            int AttendanceNo = 0;
            int listviewcount = lvSchedule.Items.Count;
            foreach (ListViewDataItem lvItem in lvSchedule.Items)
            {

                Label lblstatus    = lvItem.FindControl("lblStatus") as Label;  
                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
                HiddenField hdnACNo = lvItem.FindControl("hdnACNO") as HiddenField;
                HiddenField hdnAttendaceNo = lvItem.FindControl("hdnAttendanceNO") as HiddenField;
                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    userNo = Convert.ToInt32(hdnUserNo.Value);
                }
                if (!hdnACNo.Value.Equals(string.Empty))
                {
                    ACNO = Convert.ToInt32(hdnACNo.Value);
                }
                if (!hdnACNo.Value.Equals(string.Empty))
                {
                    AttendanceNo = Convert.ToInt32(hdnAttendaceNo.Value);
                }
                      

                if (chkBox.Checked)
                {
                   


                    IsAttend = true;
                }
                else
                {
                    chkCount++;
                    IsAttend = false;
                }


                if (chkCount == listviewcount && lblstatus.Text == "Absent")
                {
                    //objCommon.DisplayMessage(this.Page, "Please Select At Least One Student.", this.Page);
                    objCommon.DisplayMessage(upAttendance, "Please Select At Least One Student.", this.Page);
                    return;
                }

                CustomStatus cs = (CustomStatus)objAttendance.INSERT_Update_STUDENTATTENDANCE(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), ACNO, ipaddress, userNo, IsAttend, AttendanceNo);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    updCount++;
                }


            }
            //if (chkCount == 0)
            //{
            //    //objCommon.DisplayMessage(this.Page, "Please Select At Least One Student.", this.Page);
            //    objCommon.DisplayMessage(upAttendance, "Please Select At Least One Student.", this.Page);
            //    return;
            //}
            //if (chkCount > 0 && chkCount == updCount)
            if (updCount > 0)
            {
                //objCommon.DisplayMessage("Admit Card Generated Successfully for Selected Students.", this.Page);
                objCommon.DisplayMessage(upAttendance, "Save Attendance Successfully.", this.Page);
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
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH DESC");

        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        lstProgram.Items.Clear();
        //pnlCount.Visible = false;
        pnlCount.Visible = false;
        ddlExamSlot.Items.Clear();
        pnllvSh.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
        btnAttendance.Visible = false;
    }
    protected void btnAttendanceSheet_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Branch/Program.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 2)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Exam Slot.", this.Page);
                return;
            }
            else if (ddlExamSlot.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 7)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Schedule Slot.", this.Page);
                return;
            }

            GridView GV = new GridView();
            string ContentType = string.Empty;

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
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
            ds = objAttendance.ExcelAttendanceSheet(ADMBATCH, ProgramType, DegreeNo, branchno, Schedule);


            //DataSet dsfee = objQrC.Get_STUDENT_FOR_EXCEL(Convert.ToInt32(ddlCollege.SelectedValue), sessionNo);


            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=StudentAttendance.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
            }
            else
            {
                objCommon.DisplayMessage(upAttendance, "No Recored Found.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");



        }
        Response.End();
    }
}