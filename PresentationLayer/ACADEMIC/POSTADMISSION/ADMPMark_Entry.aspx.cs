using BusinessLogicLayer.BusinessLogic.Academic.Admission;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mark_entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    //ADMPInterviewTestConfigController objITC = new ADMPInterviewTestConfigController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlAdmissionBatchList();
            BindSchedule();
        }

        //ddlAdmissionBatchList();
        //BindSchedule();
    }


    #region Common Methods For All DropDown list
    public void BindALLDDL(ref DropDownList ddl, DataSet ds, string textField, string valueField)
    {
        try
        {
            ddl.Items.Clear();
            ddl.DataSource = ds;
            ddl.DataValueField = ds.Tables[0].Columns[valueField].ToString();
            ddl.DataTextField = ds.Tables[0].Columns[textField].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, "Please Select");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "InterviewTestConfig.BindALLDDL() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    //#region All DropDown lists and ListView
    private void ddlAdmissionBatchList()
    {
        try
        {
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Insert(0, "Please Select");
            DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNO DESC");
            BindALLDDL(ref ddlAdmBatch, ds, "BATCHNAME", "BATCHNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPInterviewTestConfig.ddlAdmissionBatchList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }


    private void BindSchedule()
    {
        try
        {
            string UserId = Convert.ToString(Session["usertype"]);
            if (UserId == "1")
            {
                ddlSchedule.Items.Clear();
                ddlSchedule.Items.Insert(0, "Please Select");
                DataSet ds = objCommon.FillDropDown("ACD_ADMITCARD_ENTRY", "ExamSchedule", "SCHEDULE_NO", "CREATEDBY= '" + Convert.ToInt32(Session["userno"]) + "'", "ExamSchedule ASC");
                //BindALLDDL(ref ddlAdmBatch, ds, "ExamSchedule", "SCHEDULE_NO");

                ddlSchedule.Items.Clear();
                ddlSchedule.DataSource = ds;
                ddlSchedule.DataValueField = ds.Tables[0].Columns["SCHEDULE_NO"].ToString();
                ddlSchedule.DataTextField = ds.Tables[0].Columns["ExamSchedule"].ToString();
                ddlSchedule.DataBind();
                ddlSchedule.Items.Insert(0, "Please Select");
            }
            else if (UserId == "3")
            {
                ddlSchedule.Items.Clear();
                ddlSchedule.Items.Insert(0, "Please Select");
                //DataSet ds = objCommon.FillDropDown("ACD_ADMITCARD_ENTRY", "ExamSchedule", "SCHEDULE_NO", "CREATEDBY= '" + Convert.ToInt32(Session["userno"]) + "'", "ExamSchedule ASC");
                DataSet ds = objCommon.FillDropDown("ACD_ADMITCARD_ENTRY B INNER JOIN ACD_ADMP_Panel A  ON  ',' + A.STAFFNO + ',' like '%,' + cast(B.USERNO as nvarchar(20)) + ',%'", "ExamSchedule", "A.SCHEDULE_NO", "CREATEDBY= '" + Convert.ToInt32(Session["userno"]) + "'", "ExamSchedule ASC");
                //BindALLDDL(ref ddlAdmBatch, ds, "ExamSchedule", "SCHEDULE_NO");
                ddlSchedule.Items.Clear();
                ddlSchedule.DataSource = ds;
                ddlSchedule.DataValueField = ds.Tables[0].Columns["SCHEDULE_NO"].ToString();
                ddlSchedule.DataTextField = ds.Tables[0].Columns["ExamSchedule"].ToString();
                ddlSchedule.DataBind();
                ddlSchedule.Items.Insert(0, "Please Select");

            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPInterviewTestConfig.ddlAdmissionBatchList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlProgram.Items.Clear();

            objCommon.FillDropDownList(ddlProgram, "ACD_ADMITCARD_ENTRY D INNER JOIN VW_ACD_COLLEGE_DEGREE_BRANCH B ON (D.BRANCHNO=B.BRANCHNO)", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "", "B.LONGNAME");

            //ddlProgram.Items.Insert(0, "Please Select");
            ddlProgram.SelectedIndex = 0;
            //DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNO DESC");
            //BindALLDDL(ref ddlAdmBatch, ds, "BATCHNAME", "BATCHNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPInterviewTestConfig.ddlAdmissionBatchList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
}