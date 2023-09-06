using BusinessLogicLayer.BusinessLogic.Academic.Admission;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.SQLServer.SQLDAL;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Collections;

public partial class InterviewTestConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPInterviewTestConfigController objITC = new ADMPInterviewTestConfigController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlAdmissionBatchList();
            ddlDegreeList();

            //BindListView(0, 0);

        }

        ddlAdmissionBatchList();
        ddlDegreeList();
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
            ddlAdmissionBatch.Items.Clear();
            ddlAdmissionBatch.Items.Insert(0, "Please Select");
            DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNO DESC");
            BindALLDDL(ref ddlAdmissionBatch, ds, "BATCHNAME", "BATCHNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPInterviewTestConfig.ddlAdmissionBatchList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void ddlDegreeList()
    {
        try
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, "Please Select");
            DataSet ds = objCommon.FillDropDown("ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ACTIVESTATUS = 1", "DEGREENAME");
            BindALLDDL(ref ddlDegree, ds, "DEGREENAME", "DEGREENO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPInterviewTestConfig.ddlDegreeList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlAdmissionBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlAdmissionBatch.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0)
            {
                //BindListView(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));

            }
            else
            {
                //BindListView(0, 0);

            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPInterviewTestConfig.ddlAdmissionBatch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmissionBatch.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0)
            {
                //BindListView(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));

            }
            else
            {
                //BindListView(0, 0);

            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ClubStudentMapping.ddlCollege_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    //private void BindListView(int BatchNo, int DegreeNo)
    //{
    //    try
    //    {
    //        ViewState["action"] = "add";
    //        pnlInterview.Visible = false;
    //        lvInterview.Visible = false;
    //        lvInterview.DataSource = null;
    //        lvInterview.DataBind();

    //        DataSet ds = objITC.GetAllInterviewTestList(BatchNo, DegreeNo);

    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {

    //            ViewState["action"] = "edit";
    //            pnlInterview.Visible = true;
    //            lvInterview.Visible = true;
    //            lvInterview.DataSource = ds;
    //            lvInterview.DataBind();

    //        }
    //        else
    //        {
    //            ViewState["action"] = "add";
    //            pnlInterview.Visible = false;
    //            lvInterview.Visible = false;
    //            lvInterview.DataSource = null;
    //            lvInterview.DataBind();

    //        }
    //        foreach (ListViewDataItem lv in lvInterview.Items)
    //        {
    //            HiddenField hfdHideInter = lv.FindControl("hfdHideInter") as HiddenField;

    //            if (Convert.ToInt32(hfdHideInter.Value).Equals(0))
    //            {
    //                ((Panel)lv.FindControl("Interview1")).Visible = false;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    //#endregion

    //#region Submit, Cancel Buttons
    //protected void btnSubmit_Click(object sender, System.EventArgs e)
    //{
    //    try
    //    {
    //        int BatchNo = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
    //        int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);

    //        string displaymsg = "";
            
    //        // validation methods
    //        foreach (ListViewDataItem lv in lvInterview.Items)
    //        {
    //            HiddenField hfdBranchNo = lv.FindControl("hfdBranchNo") as HiddenField;

    //            HiddenField hdfPanelForInter = lv.FindControl("hdfPanelForInter") as HiddenField;
    //            CheckBox ChkInterMark = lv.FindControl("ChkInterMark") as CheckBox;
    //            TextBox txtInterMarks = lv.FindControl("txtInterMarks") as TextBox;

    //            if (ChkInterMark.Checked && Convert.ToInt32(txtInterMarks.Text) < 1)
    //            {
    //                objCommon.DisplayMessage(this, "Please Enter Interview Marks Greater than Zero", this.Page);
    //                return;
    //            }
    //            HiddenField hdfPanelForGD = lv.FindControl("hdfPanelForGD") as HiddenField;
    //            CheckBox ChkGDMark = lv.FindControl("ChkGDMark") as CheckBox;
    //            TextBox txtGDMarks = lv.FindControl("txtGDMarks") as TextBox;
    //            if (ChkGDMark.Checked && Convert.ToInt32(txtGDMarks.Text) < 1)
    //            {
    //                objCommon.DisplayMessage(this, "Please Enter GD Marks Greater than Zero", this.Page);
    //                return;
    //            }

    //            HiddenField hdfPanelForTest = lv.FindControl("hdfPanelForTest") as HiddenField;
    //            CheckBox ChkTestMark = lv.FindControl("ChkTestMark") as CheckBox;
    //            TextBox txtTest = lv.FindControl("txtTest") as TextBox;
    //            if (ChkTestMark.Checked && Convert.ToInt32(txtTest.Text) < 1)
    //            {
    //                objCommon.DisplayMessage(this, "Please Enter Test Marks Greater than Zero", this.Page);
    //                return;
    //            }
                
    //        }
    //        // save operation methods
    //        foreach (ListViewDataItem lv in lvInterview.Items)
    //        {
    //            HiddenField hfdBranchNo = lv.FindControl("hfdBranchNo") as HiddenField;

    //            HiddenField hdfPanelForInter = lv.FindControl("hdfPanelForInter") as HiddenField;
    //            CheckBox ChkInterMark = lv.FindControl("ChkInterMark") as CheckBox;
    //            TextBox txtInterMarks = lv.FindControl("txtInterMarks") as TextBox;

    //            if (ChkInterMark.Checked && Convert.ToInt32(txtInterMarks.Text) > 0)
    //            {
    //                SaveData(Convert.ToInt32(hdfPanelForInter.Value), BatchNo, DegreeNo, Convert.ToInt32(hfdBranchNo.Value), Convert.ToInt32(txtInterMarks.Text.Trim()),0);
                   
    //            }
    //            else
    //            {
    //                SaveData(Convert.ToInt32(hdfPanelForInter.Value), BatchNo, DegreeNo, Convert.ToInt32(hfdBranchNo.Value), Convert.ToInt32(txtInterMarks.Text.Trim()), 1);
    //            }

    //            HiddenField hdfPanelForGD = lv.FindControl("hdfPanelForGD") as HiddenField;
    //            CheckBox ChkGDMark = lv.FindControl("ChkGDMark") as CheckBox;
    //            TextBox txtGDMarks = lv.FindControl("txtGDMarks") as TextBox;

    //            if (ChkGDMark.Checked && Convert.ToInt32(txtGDMarks.Text) > 0)
    //            {
    //                SaveData(Convert.ToInt32(hdfPanelForGD.Value), BatchNo, DegreeNo, Convert.ToInt32(hfdBranchNo.Value), Convert.ToInt32(txtGDMarks.Text.Trim()), 0);
    //            }
    //            else
    //            {
    //                SaveData(Convert.ToInt32(hdfPanelForGD.Value), BatchNo, DegreeNo, Convert.ToInt32(hfdBranchNo.Value), Convert.ToInt32(txtGDMarks.Text.Trim()), 1);
    //            }

    //            HiddenField hdfPanelForTest = lv.FindControl("hdfPanelForTest") as HiddenField;
    //            CheckBox ChkTestMark = lv.FindControl("ChkTestMark") as CheckBox;
    //            TextBox txtTest = lv.FindControl("txtTest") as TextBox;

    //            if (ChkTestMark.Checked && Convert.ToInt32(txtTest.Text) > 0)
    //            {
    //                SaveData(Convert.ToInt32(hdfPanelForTest.Value), BatchNo, DegreeNo, Convert.ToInt32(hfdBranchNo.Value), Convert.ToInt32(txtTest.Text.Trim()), 0);
    //            }
    //            else 
    //            {
    //                SaveData(Convert.ToInt32(hdfPanelForTest.Value), BatchNo, DegreeNo, Convert.ToInt32(hfdBranchNo.Value), Convert.ToInt32(txtTest.Text.Trim()), 1);
    //            }

    //        }

    //        displaymsg = "Record added successfully.";
    //        objCommon.DisplayMessage(this, displaymsg, this.Page);

    //        ClearData();

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "InterviewTestConfig.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //private void SaveData(int PanelForId, int BatchNo, int DegreeNo, int BranchNo, int MaxMarks, int OprCode)
    //{
    //    try
    //    {
    //        int ret = 0;
    //        //displaymsg = "Record added successfully.";
    //        ret = objITC.INSUPDInterviewTestData(PanelForId, BatchNo, DegreeNo, BranchNo, MaxMarks, OprCode);

    //        //objCommon.DisplayMessage(displaymsg, this.Page);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "InterviewTestConfig.SaveData --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }

    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    //#endregion

    ////protected void TextBoxVisibility(bool isEnabled)
    ////{
    ////    foreach (ListViewDataItem lv in lvInterview.Items)
    ////    {
    ////        CheckBox ChkInterMark = lv.FindControl("ChkInterMark") as CheckBox;
    ////        TextBox txtInterMarks = lv.FindControl("txtInterMarks") as TextBox;

    ////        CheckBox ChkGDMark = lv.FindControl("ChkGDMark") as CheckBox;
    ////        TextBox txtGDMarks = lv.FindControl("txtGDMarks") as TextBox;

    ////        CheckBox ChkTestMark = lv.FindControl("ChkTestMark") as CheckBox;
    ////        TextBox txtTest = lv.FindControl("txtTest") as TextBox;


    ////        txtInterMarks.Enabled = ChkInterMark.Checked;
    ////        txtGDMarks.Enabled = ChkGDMark.Checked;
    ////        txtTest.Enabled = ChkTestMark.Checked;
    ////    }

    ////}

    public void ClearData()
    {

        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;

        //BindListView(0, 0);

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetInterviewData(int BatchNo, int DegreeNo)
        {
        string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);


        SqlParameter[] objParams = new SqlParameter[2];
        objParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
        objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);

        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_ALL_INTERVIEW_TEST_CONFIG", objParams);

        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }

        this.Context.Response.ContentType = "application/json; charset=utf-8";
        this.Context.Response.Write(serializer.Serialize(root));
    }


}