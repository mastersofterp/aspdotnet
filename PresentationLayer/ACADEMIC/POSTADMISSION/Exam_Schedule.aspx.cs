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

public partial class Exam_Schedule : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamScheduleController objexam = new ExamScheduleController();

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
            if (Session["OrgId"].ToString() == "2")
            {
                pnlDegree.Visible = true;
                pnlCenter.Visible = false;
            }
            else if (Session["OrgId"].ToString() == "7")
            {
                pnlDegree.Visible = false;
                pnlCenter.Visible = true;
            }
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //Populate the Drop Down Lists
            //PopulateDropDownList();
            this.FillDropdown();
            this.BindData();
            // BinGridAssesment();
            //SetInitialRow();
        }
    }
    #endregion Page Load

    private void FillDropdown()
    {
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");
        ddlAdmissionBatch.SelectedIndex = 1;

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0" , "D.DEGREENO");
        ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
        ddlDegree.SelectedIndex = 0;
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
                if (chkTime.Text == "PM")
                {
                    chkTime.Text = "PM";
                }
                else
                {
                    chkTime.Text = "AM";
                }
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

           int ScheduleNo = Convert.ToInt16(hdnScheduleNo.Value);
           string Place = txtPlace.Text.Trim();
           string CenterAddress = txtAddress.Text.Trim();
           int ExamMode =  Convert.ToInt16(ddlMode.SelectedValue);
           int ORGID = Convert.ToInt32(Session["OrgId"]);
           CustomStatus cs = (CustomStatus)objexam.Add_Update_EntranceSchedule(ScheduleNo, Convert.ToInt32(ddlAdmissionBatch.SelectedValue), date, time, Convert.ToInt32(ddlDegree.SelectedValue), ddlDegree.SelectedItem.ToString(), activitynos, activeStatus, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), Place, CenterAddress, ExamMode, ORGID);
            if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updSchedule, "Record Already Exists.", this.Page);
                BindData();
                return;
            }
             if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updSchedule, "Record Saved Successfully.", this.Page);
                //BindData();
                //clearFields();
                //ViewState["action"] = null;
                BindData();
                return;
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updSchedule, "Record Updated Successfully.", this.Page);
                BindData();
                //clearFields();
                ViewState["action"] = null;
                BindData();
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
            
           // objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH");
            
            //objCommon.FillDropDownList(ddlCentre, "ACD_ENTRANCE_TEST_CENTRES TC INNER JOIN ACD_ENTRANCE_TEST_CENTERS_DETAILS TCD ON (TC.ETCNO=TCD.CENTRES)", "CENTRECODE", "(ETCNAME+' - '+CAST(CENTRECODE AS NVARCHAR(10))) CENTRE", "CENTRECODE > 0", "CENTRECODE");
            
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");
         

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0", "D.DEGREENO");


            ddlAdmissionBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]) > 0)
            {
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            }
            txtScheduleDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SCHD_DATE"].ToString()).ToString("dd/MM/yyyy");
            //txtLoginTime.Text = ds.Tables[0].Rows[0]["SCHD_TIME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SCHD_TIME"].ToString();
            time = ds.Tables[0].Rows[0]["SCHD_TIME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SCHD_TIME"].ToString();
            if (time.Contains("PM"))
            {
                chkTime.Checked = false;
                chkTime.Text = "AM";
                txtLoginTime.Text = time;
            }
            else
            {
                chkTime.Checked = true;
                chkTime.Text = "PM";
                txtLoginTime.Text = time;
            }
            int len = txtLoginTime.Text.Length;
            string str = txtLoginTime.Text.Substring((len - 2), 2);
            chkTime.Text = str.ToString().Trim();
            txtLoginTime.Text = txtLoginTime.Text.Remove((len - 2), 2);
            txtLoginTime.Text = txtLoginTime.Text.ToString().Trim();
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
            if (ds.Tables[0].Rows[0]["BRANCHNO"].ToString() != null || ds.Tables[0].Rows[0]["BRANCHNO"].ToString()!=string.Empty)
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
}
   