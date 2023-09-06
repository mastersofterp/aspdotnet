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

public partial class ACADEMIC_POSTADMISSION_ExamSchedule : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPExamScheduleController objexam = new ADMPExamScheduleController();

    #region MasterPage
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #endregion End MasterPage set

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Sessionbtnsubmi
           //Session["OrgId"] = "7";
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
            if (Session["OrgId"].ToString() == "2")
            {
                pnlDegree.Visible = true;
                pnlCenter.Visible = false;
                divEndTime.Visible = false;
            }
            else if (Session["OrgId"].ToString() == "7")
            {
                pnlDegree.Visible = false;
                pnlCenter.Visible = true;
                divEndTime.Visible = true;
            }
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //Populate the Drop Down Lists
            //PopulateDropDownList();
            this.FillDropdown();
            this.BindData();
            ViewState["action"] = "add";
            // BinGridAssesment();
            //SetInitialRow();
        }
    }
    #endregion Page Load

    private void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");

        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlAdmissionBatch.SelectedIndex = 0;

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0", "D.DEGREENO");
        //ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
        //ddlDegree.SelectedIndex = 0;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objexam.GetBranch(Degree);

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

    protected void BindData()
    {

        //try
        //{

        //    DataSet ds = objexam.BindExamSchedule();
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        lvSchedule.DataSource = ds;
        //        lvSchedule.DataBind();
        //        lvSchedule.Visible = true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}

        try
        {
            int ORGID = Convert.ToInt32(Session["OrgId"]);
            DataSet ds = objexam.BindExamSchedule(ORGID);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (Session["OrgId"].ToString() == "2")
                {
                    pnlGV1.Visible = true;
                    pnlGV2.Visible = false;
                    lvSchedule.DataSource = ds;
                    lvSchedule.DataBind();
                    lvSchedule.Visible = true;
                }
                else if (Session["OrgId"].ToString() == "7")
                {
                    pnlGV1.Visible = false;
                    pnlGV2.Visible = true;
                    lsSchedule2.DataSource = ds;
                    lsSchedule2.DataBind();
                    lsSchedule2.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }

        //try
        //{
        //    //DataSet ds = objCommon.FillDropDown("ACD_ENTRANCE_TEST_SCHEDULE", "SCHEDULE_NO,DBO.FN_DESC('ADMBATCH',ADMBATCH) ADMBATCH,CONVERT(NVARCHAR,SCHD_DATE,103) SCHD_DATE,SCHD_TIME,DEGREENAME,BRANCHNAME,ISNULL(ACTIVE_STATUS,0) ACTIVE_STATUS", "", "", "");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvSchedule.DataSource = ds;
        //        lvSchedule.DataBind();
        //        lvSchedule.Visible = true;
        //    }
        //    else
        //    {
        //        //objCommon.DisplayMessage(updSchedule, "No Record Found", this.Page);
        //       // clearListView();
        //        return;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //if (Convert.ToBoolean(Session["error"]) == true)
        //    //    //objUaimsCommon.ShowError(Page, "ACADEMIC_EntranceExamSchedule.BindData() --> " + ex.Message + " " + ex.StackTrace);
        //    //else
        //    //   //objUaimsCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    protected void chkTime_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTime.Checked)
            {
                chkTime.Text = "AM";
            }
            else
            {
                chkTime.Text = "PM";
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUaimsCommon.ShowError(Page, "ACADEMIC_EntranceExamSchedule.chkTime_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ScheduleNo = 0;
            string endTime = string.Empty;
            string reportingTime = string.Empty;
            if (txtScheduleDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Scheduled Date.", this.Page);
                return;
            }
            if (txtLoginTime.Text == string.Empty)
            {
                objCommon.DisplayMessage(updSchedule, "Please Enter Start Time.", this.Page);
                return;
            }            
            if (Convert.ToInt32(Session["OrgId"]) == 7)
            {
                if (txtEndTime.Text == string.Empty)
                {
                    objCommon.DisplayMessage(updSchedule, "Please Enter End Time.", this.Page);
                    return;
                }
            }
            if (txtReportingTime.Text == string.Empty)
            {
                objCommon.DisplayMessage(updSchedule, "Please Enter Reporting Time.", this.Page);
                return;
            }

            if (pnlDegree.Visible != false)
            {

                if (ddlDegree.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(updSchedule, "Please Select Degree.", this.Page);
                    return;
                }
                else if (lstProgram.SelectedValue == "")
                {
                    objCommon.DisplayMessage(updSchedule, "Please Select Programme/Branch.", this.Page);
                    return;
                }
            }
            if (Session["OrgId"].ToString() == "7" && txtPlace.Text == string.Empty)
            {
                objCommon.DisplayMessage(updSchedule, "Please Enter Place.", this.Page);
                return;
            }

            DateTime date = new DateTime();
            string centFinal = string.Empty;
            date = Convert.ToDateTime(txtScheduleDate.Text.Trim());
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            string time = string.Empty;
            time = txtLoginTime.Text.TrimEnd();
            int flag = 0;
            string centres = string.Empty;
            string codes = string.Empty;
            int activeStatus = 0;
            //int ScheduleNo = 0;

            string activitynos = string.Empty;
            string activitynames = string.Empty;
            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    activitynos += items.Value + ',';
                    activitynames += items.Text + ',';
                }
            }
            //department = department.Length - 1;
            if (activitynos.Length > 1)
            {
                activitynos = activitynos.Remove(activitynos.Length - 1);
            }
            if (activitynames.Length > 1)
            {
                activitynames = activitynames.Remove(activitynames.Length - 1);
            }

            if (chkTime.Checked)
            {
                //if (chkTime.Text == "PM")
                //{
                //    chkTime.Text = "PM";
                //}
                //else
                //{
                    chkTime.Text = "AM";
                //}
            }
            else
            {
                chkTime.Text = "PM";
            }

            time = time + chkTime.Text.ToString().Trim();
            if (ViewState["action"] != null && ViewState["action"].Equals("edit"))
            {
                flag = 2;
            }
            else
            {
                flag = 1;
            }
            if (hfdShowStatus.Value == "true")
            {
                activeStatus = 1;
            }
            else
            {
                activeStatus = 0;
            }

            // activeStatus = rdActive.Checked == true ? 1 : 0;

        
            string Place = txtPlace.Text.Trim();
            string CenterAddress = txtAddress.Text.Trim();
            int ExamMode = Convert.ToInt16(ddlMode.SelectedValue);
            int ORGID = Convert.ToInt32(Session["OrgId"]);


            if (chkEndTime.Checked)
            {
                chkEndTime.Text = "AM";
            }
            else
            {
                chkEndTime.Text = "PM";
            }
            if (chkReportingTime.Checked)
            {
                chkReportingTime.Text = "AM";
            }
            else
            {
                chkReportingTime.Text = "PM";
            }
            if (Convert.ToInt32(Session["OrgId"]) == 7)
            {
                endTime = txtEndTime.Text.TrimEnd() + chkEndTime.Text.ToString().Trim();
            }
            else
            {
                endTime= string.Empty;
            }
            reportingTime = txtReportingTime.Text.TrimEnd() + chkReportingTime.Text.ToString().Trim();

            if (ViewState["action"].ToString() == "add")
            {
                ScheduleNo = 0;
            }
            else
            {
               ScheduleNo = Convert.ToInt16(hdnScheduleNo.Value);
            }

            CustomStatus cs = (CustomStatus)objexam.Add_Update_EntranceSchedule(ScheduleNo, Convert.ToInt32(ddlAdmissionBatch.SelectedValue), date, time, Convert.ToInt32(ddlDegree.SelectedValue), ddlDegree.SelectedItem.ToString(), activitynos, activeStatus, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), Place, CenterAddress, ExamMode, ORGID, endTime, reportingTime);
            if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updSchedule, "Record Already Exists.", this.Page);
                BindData();
                clearFields();
                return;
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updSchedule, "Record Saved Successfully.", this.Page);
                //BindData();
                //clearFields();
                //ViewState["action"] = null;
                BindData();
                clearFields();
                return;
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updSchedule, "Record Updated Successfully.", this.Page);
                BindData();
                //clearFields();
                ViewState["action"] = null;
                BindData();
                clearFields();
                return;
            }
            if (cs.Equals(CustomStatus.RecordFound))
            {
                objCommon.DisplayMessage(updSchedule, "Record Can Not be Modify because, For this schedule Admit card already generated.", this.Page);
                BindData();
                clearFields();
                return;

            }


        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUaimsCommon.ShowError(Page, "ACADEMIC_EntranceExamSchedule.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;
        int scheduleNo = Convert.ToInt32(imgButton.CommandArgument);
        DataSet ds = objCommon.FillDropDown("ACD_ENTRANCE_TEST_SCHEDULE", "SCHEDULE_NO", "*", "SCHEDULE_NO=" + scheduleNo, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string time = string.Empty;
            string endTime = string.Empty;
            string reportingTime = string.Empty;
            // objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH");

            //objCommon.FillDropDownList(ddlCentre, "ACD_ENTRANCE_TEST_CENTRES TC INNER JOIN ACD_ENTRANCE_TEST_CENTERS_DETAILS TCD ON (TC.ETCNO=TCD.CENTRES)", "CENTRECODE", "(ETCNAME+' - '+CAST(CENTRECODE AS NVARCHAR(10))) CENTRE", "CENTRECODE > 0", "CENTRECODE");

           // objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");

            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0", "D.DEGREENO");


            ddlAdmissionBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]) > 0)
            {
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            }
            txtScheduleDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SCHD_DATE"].ToString()).ToString("dd-MM-yyyy");
            //txtScheduleDate.Text = "12-10-2022";
            //txtLoginTime.Text = ds.Tables[0].Rows[0]["SCHD_TIME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SCHD_TIME"].ToString();
            time = ds.Tables[0].Rows[0]["SCHD_TIME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SCHD_TIME"].ToString();
            if (time.Contains("PM"))
            {
                chkTime.Checked = false;
                //chkTime.Text = "PM";
                txtLoginTime.Text = time;
            }
            else
            {
                chkTime.Checked = true;
                //chkTime.Text = "AM";
                txtLoginTime.Text = time;
            }
            endTime = ds.Tables[0].Rows[0]["END_TIME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["END_TIME"].ToString();
            if (endTime.Contains("PM"))
            {
                chkEndTime.Checked = false;
            }
            else
            {
                chkEndTime.Checked = true;            
            }
            txtEndTime.Text = endTime;
            reportingTime = ds.Tables[0].Rows[0]["REPORTING_TIME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["REPORTING_TIME"].ToString();
            if (reportingTime.Contains("PM"))
            {
                chkReportingTime.Checked = false;              
            }
            else
            {
                chkReportingTime.Checked = true;
            }
            txtReportingTime.Text = reportingTime;
            if (time != string.Empty)
            {
                int len = txtLoginTime.Text.Length;
                string str = txtLoginTime.Text.Substring((len - 2), 2);
                chkTime.Text = str.ToString().Trim();
                txtLoginTime.Text = txtLoginTime.Text.Remove((len - 2), 2);
                txtLoginTime.Text = txtLoginTime.Text.ToString().Trim();
            }
            else
            {
                chkTime.Text = "AM";
                txtLoginTime.Text = string.Empty;
            }
            if (endTime != string.Empty)
            {
                int lenEndTime = txtEndTime.Text.Length;
                string strEndTime = txtEndTime.Text.Substring((lenEndTime - 2), 2);
                chkEndTime.Text = strEndTime.ToString().Trim();
                txtEndTime.Text = txtEndTime.Text.Remove((lenEndTime - 2), 2);
                txtEndTime.Text = txtEndTime.Text.ToString().Trim();
            }
            else
            {
                chkEndTime.Text = "AM";
                txtEndTime.Text = string.Empty;
            }
            if (reportingTime != string.Empty)
            {
                int lenReportingTime = txtReportingTime.Text.Length;
                string strReportingTime = txtReportingTime.Text.Substring((lenReportingTime - 2), 2);
                chkReportingTime.Text = strReportingTime.ToString().Trim();
                txtReportingTime.Text = txtReportingTime.Text.Remove((lenReportingTime - 2), 2);
                txtReportingTime.Text = txtReportingTime.Text.ToString().Trim();
            }
            else
            {
                chkReportingTime.Text = "AM";
                txtReportingTime.Text = string.Empty;
            }
            hdnScheduleNo.Value = ds.Tables[0].Rows[0]["SCHEDULE_NO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SCHEDULE_NO"].ToString();
            //string branch= ds.Tables[0].Rows[0]["BRANCHNAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNAME"].ToString().Split(',');

            // For Schedule 2
            txtPlace.Text = ds.Tables[0].Rows[0]["CENTRE_NAME"].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0]["CENTRE_ADDRESS"].ToString();
            if (ds.Tables[0].Rows[0]["EXAM_MODE"].ToString() == "1")
            {
                ddlMode.SelectedIndex = 2;
            }
            else if (ds.Tables[0].Rows[0]["EXAM_MODE"].ToString() == "0")
            {
                ddlMode.SelectedIndex = 1;
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]) > 0)
            {
                int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
                MultipleCollegeBind(Degree);
            }
            if (ds.Tables[0].Rows[0]["BRANCHNO"].ToString() != null || ds.Tables[0].Rows[0]["BRANCHNO"].ToString() != string.Empty)
            {
                string[] Tempsemester = ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Split(',');
                foreach (ListItem items in lstProgram.Items)
                {
                    foreach (string Semester in Tempsemester)
                    {
                        if (items.Value == Semester)
                        {
                            items.Selected = true;
                        }
                    }
                }
            }
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
            }

            ViewState["action"] = "edit";
        }
        else
        {
            objCommon.DisplayMessage(updSchedule, "Something Went Wrong.", this.Page);
            return;
        }
    }
    private void clearFields()
    {
        txtScheduleDate.Text = string.Empty;
        txtLoginTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtReportingTime.Text = string.Empty;
        ddlDegree.SelectedIndex = 0;
        chkTime.Checked = true;
        chkTime.Text = "AM";
        chkEndTime.Checked = true;
        chkEndTime.Text = "AM";
        chkReportingTime.Checked = true;
        chkReportingTime.Text = "AM";
        txtPlace.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlMode.SelectedIndex = -1;
        lstProgram.Items.Clear();
        ViewState["action"] = "add";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }
    protected void chkEndTime_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkEndTime.Checked)
            {
                chkEndTime.Text = "AM";
            }
            else
            {
                chkEndTime.Text = "PM";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPExamScheduled.chkEndTime_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void chkReportingTime_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkReportingTime.Checked)
            {
                chkReportingTime.Text = "AM";
            }
            else
            {
                chkReportingTime.Text = "PM";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPExamScheduled.chkReportingTime_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void txtScheduleDate_TextChanged(object sender, EventArgs e)
    {
        DateTime SchdDate;
        DateTime CurrentDt = DateTime.Now.Date;
        SchdDate = Convert.ToDateTime(txtScheduleDate.Text);
        if (CurrentDt > SchdDate)
        {
            objCommon.DisplayMessage(updSchedule, "Schedule date must be greater or equal the current date", this.Page);   
            txtScheduleDate.Text = string.Empty;
        }
    }
    protected void txtLoginTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string text = txtLoginTime.Text;

            int tm = Convert.ToInt32(text.Split(':').LastOrDefault());


            //string text1 = txtTimeTo.Text;
            //string[] separatingStrings = { 
            //                                     ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
            //                                   ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
            //                                   ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
            //                                    ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
            //                                   ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
            //                                  ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
            //                                  ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
            //                                  ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
            //                                   ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
            //                                    ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
            //                                     ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
            //                                     ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
            //                                     ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
            //                                     ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
            //                                     ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
            //                                     ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
            //                                     ":97", ":97", ":98", ":98", ":99", ":99"
            //                                 };



            string[] separatingStrings = { 
                                             ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
                                           ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
                                           ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
                                            ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
                                           ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
                                          ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
                                          ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
                                          ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
                                           ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
                                            ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
                                             ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
                                             ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
                                             ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
                                             ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
                                             ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
                                             ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
                                             ":97", ":97", ":98", ":98", ":99", ":99"
                                         };



            //**********added on 01052022*******


            //string[] TempData = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //string[] data = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //*******************END****************
            string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            //string[] word = text1.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);



            int FromTime = Convert.ToInt32(words[0]);
            //int ToTime = Convert.ToInt32(words[0]);

            if (FromTime > 12)
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtLoginTime.Text = string.Empty;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtLoginTime.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtLoginTime.Text = string.Empty;
                    return;
                }
            }
        }
        catch(Exception ex)
        {
        }

    }
    protected void txtReportingTime_TextChanged(object sender, EventArgs e)
    {
        //DateTime d1 = DateTime.Parse(txtScheduleDate.Text);
        //DateTime d2 = DateTime.Parse(txtScheduleDate.Text);
        ////TimeSpan timeFrom = TimeSpan.Parse(d1.ToString("11:00 AM"));
        ////TimeSpan timeTo = TimeSpan.Parse(d2.ToString("10:00"));
        //TimeSpan timeFrom = TimeSpan.Parse(txtLoginTime.Text);
        //TimeSpan timeTo = TimeSpan.Parse(txtReportingTime.Text);
        //TimeSpan timeDiff;
        //if (timeFrom.TotalSeconds > timeTo.TotalSeconds)
        //{
        //    d2 = d2.AddDays(1);
        //    timeDiff = d2.Subtract(d1);
        //}
        //else
        //{
        //    timeDiff = d2.Subtract(d1);
        //}
        //Response.Write(timeDiff);

        try
        {
            string text = txtReportingTime.Text;

            int tm = Convert.ToInt32(text.Split(':').LastOrDefault());


            //string text1 = txtTimeTo.Text;
            //string[] separatingStrings = { 
            //                                     ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
            //                                   ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
            //                                   ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
            //                                    ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
            //                                   ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
            //                                  ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
            //                                  ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
            //                                  ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
            //                                   ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
            //                                    ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
            //                                     ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
            //                                     ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
            //                                     ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
            //                                     ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
            //                                     ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
            //                                     ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
            //                                     ":97", ":97", ":98", ":98", ":99", ":99"
            //                                 };



            string[] separatingStrings = { 
                                             ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
                                           ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
                                           ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
                                            ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
                                           ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
                                          ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
                                          ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
                                          ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
                                           ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
                                            ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
                                             ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
                                             ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
                                             ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
                                             ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
                                             ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
                                             ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
                                             ":97", ":97", ":98", ":98", ":99", ":99"
                                         };



            //**********added on 01052022*******


            //string[] TempData = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //string[] data = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //*******************END****************
            string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            //string[] word = text1.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);



            int FromTime = Convert.ToInt32(words[0]);
            //int ToTime = Convert.ToInt32(words[0]);

            if (FromTime > 12)
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtReportingTime.Text = string.Empty;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtReportingTime.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtReportingTime.Text = string.Empty;
                    return;
                }
            }
        }
        catch(Exception  ex)
        {
        
        }

    }
    protected void txtEndTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string text = txtEndTime.Text;

            int tm = Convert.ToInt32(text.Split(':').LastOrDefault());


            string[] separatingStrings = { 
                                             ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
                                           ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
                                           ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
                                            ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
                                           ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
                                          ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
                                          ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
                                          ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
                                           ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
                                            ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
                                             ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
                                             ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
                                             ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
                                             ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
                                             ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
                                             ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
                                             ":97", ":97", ":98", ":98", ":99", ":99"
                                         };



            //**********added on 01052022*******


            //string[] TempData = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //string[] data = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //*******************END****************
            string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            //string[] word = text1.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);



            int FromTime = Convert.ToInt32(words[0]);
    
            if (FromTime > 12)
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtEndTime.Text = string.Empty;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtEndTime.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtEndTime.Text = string.Empty;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
        }

    }
}
