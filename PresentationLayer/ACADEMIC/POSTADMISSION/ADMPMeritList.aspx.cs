
using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_DAIICTPostAdmission_ADMPMeritList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();  
    ADMPMeritListController objMeritLst = new ADMPMeritListController();

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
               
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
                ddlAdmissionBatch.SelectedIndex = 0;
                btnSubmit.Visible = false;
                btnLock.Visible = false;

                objCommon.FillDropDownList(ddlTestDetails, "ACD_ADMP_TESTSCORE_MASTER", "SCOREID", "TESTNAME", "ISNULL(ACTIVE_STATUS,0)=1", "SCOREID ASC");
                ddlTestDetails.Items.Insert(0,new ListItem("Please Select","0"));
                ddlTestDetails.SelectedIndex = 0;

                pnlBranch.Visible = false;
                pnlCategory.Visible = false;
                BindSortBy();
            }
        }
    }

    private void BindSortBy()
    {
        objCommon.FillDropDownList(ddlSortBy, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "IsNull(activestatus,0)=1 AND isnull(allow_jee, 0) = 1", "CATEGORYNO ASC");
        ddlSortBy.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlSortBy.SelectedIndex = 0;
    }
    #endregion Page Load


    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
            ddlBranch.Items.Clear();
            lvMeritList.DataSource = null;
            lvMeritList.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_DAIICTPostAdmission_ADMPMeritList.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        pnlCount.Visible = false;
        ddlCategory.Items.Clear();
        pnllvSh.Visible = false;
        lvMeritList.DataSource = null;
        lvMeritList.DataBind();
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }


    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objMeritLst.GetBranch(Degree);

            ddlBranch.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = ds;
                ddlBranch.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlBranch.DataTextField = ds.Tables[0].Columns[1].ToString();

                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Please Select","0"));
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
            if (ddlTestDetails.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Test Details.", this.Page);
                return;
            }
            else if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (ddlGeneration.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Generation Type.", this.Page);
                return;
            }
            else if (ddlApplicationType.SelectedValue == "-1")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Application Type.", this.Page);
                return;
            }
          
            DateTime ? date = new DateTime();
            if (txtRecDate.Text != string.Empty || txtRecDate.Text != "")
            {
                date = Convert.ToDateTime(txtRecDate.Text.Trim());
            }
            else
            {
                date = null;
            }

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ScoreId = Convert.ToInt32(ddlTestDetails.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            int SortBy = Convert.ToInt32(ddlSortBy.SelectedValue);
            int Applicationtype = Convert.ToInt32(ddlApplicationType.SelectedValue);
            int Category = 0;
            int branchno = 0; 

            btnSubmit.Visible = true;
            btnLock.Visible = true;

            if (ddlCategory.SelectedValue == "")
            {
                Category = 0;
            }
            else
            {
                Category = Convert.ToInt32(ddlCategory.SelectedValue);
            }

            if (ddlBranch.SelectedValue == "")
            {
                branchno = 0;
            }
            else
            {
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
          
            DataSet ds = null;
            ds = objMeritLst.GetMeritList(ADMBATCH, ScoreId, DegreeNo, branchno, Category, SortBy, Applicationtype, date);

            lvMeritList.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllvSh.Visible = true;
                lvMeritList.Visible = true;
                lvMeritList.DataSource = ds;
                lvMeritList.DataBind();
                pnlCount.Visible = true;
                txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString("0000");
                int listviewcount = lvMeritList.Items.Count;
            }
            else
            {                
                lvMeritList.DataSource = null;
                lvMeritList.DataBind();
                pnlCount.Visible = false;
                objCommon.DisplayMessage(upAttendance, "Record Not Found", this.Page);
                btnSubmit.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }


    private DataTable CreateTable_TestScore()
    {
        DataTable dtTestScore = new DataTable();
        dtTestScore.Columns.Add("USERNO", typeof(int));
        dtTestScore.Columns.Add("ADMBATCH", typeof(int));
        dtTestScore.Columns.Add("CATEGORY", typeof(int));
        dtTestScore.Columns.Add("GENERALMERITNO", typeof(int));
        dtTestScore.Columns.Add("MERITMARKS", typeof(float));      
        dtTestScore.Columns.Add("CATEGORYMERITNO", typeof(int));
        dtTestScore.Columns.Add("BRANCHNO", typeof(int));
        dtTestScore.Columns.Add("ISNRI", typeof(int));
        dtTestScore.Columns.Add("APPLICATIONID", typeof(string));

        return dtTestScore;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlTestDetails.SelectedValue == "0")
        {
            objCommon.DisplayMessage(upAttendance, "Please Select Test Details.", this.Page);
            return;
        }
        else if (ddlProgramType.SelectedValue == "0")
        {
            objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
            return;
        }
        else if (ddlDegree.SelectedValue == "0")
        {
            objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
            return;
        }
        else if (ddlGeneration.SelectedValue == "0")
        {
            objCommon.DisplayMessage(upAttendance, "Please Select Generation Type.", this.Page);
            return;
        }
        else if (ddlApplicationType.SelectedValue == "-1")
        {
            objCommon.DisplayMessage(upAttendance, "Please Select Application Type.", this.Page);
            return;
        }

        DataSet ds = new DataSet();
        DataTable dt = CreateTable_TestScore();
        int rowIndex = 0;
        int count = 0;
        int UANO =  Convert.ToInt32(Session["UserNo"]);
        int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        int ProgramCode = Convert.ToInt32(ddlProgramType.SelectedValue);
        int EntranceExam = Convert.ToInt32(ddlTestDetails.SelectedValue);
        string IpAdrress = Request.ServerVariables["REMOTE_HOST"];

        foreach (var lvItems in lvMeritList.Items)
        {
            DataRow dRow = dt.NewRow();
            HiddenField hdnUserNo = lvItems.FindControl("hdnUserNo") as HiddenField;
            HiddenField hdnAdmBatch = lvItems.FindControl("hdnAdmBatch") as HiddenField;
            HiddenField hdnCategoryNo = lvItems.FindControl("hdnCategoryNo") as HiddenField;
            Label lblCatMeritNo = lvItems.FindControl("lblCatMeritNo") as Label;
            Label lblScoreObtained = lvItems.FindControl("lblScoreObtained") as Label;
            Label lblGeneralMeritNo = lvItems.FindControl("lblGeneralMeritNo") as Label;
            HiddenField hdnBranchNo = lvItems.FindControl("hdnBranchNo") as HiddenField;
            Label lblApplicationId = lvItems.FindControl("lblApplicationId") as Label;
            HiddenField hdnIsNRI = lvItems.FindControl("hdnIsNRI") as HiddenField;

            dRow["USERNO"] = hdnUserNo.Value;
            dRow["ADMBATCH"] = hdnAdmBatch.Value;
            dRow["CATEGORY"] = hdnCategoryNo.Value;
            dRow["GENERALMERITNO"] = lblGeneralMeritNo.Text;
            dRow["MERITMARKS"] = lblScoreObtained.Text;
            dRow["CATEGORYMERITNO"] = lblCatMeritNo.Text;
            dRow["BRANCHNO"] = hdnBranchNo.Value;
            dRow["APPLICATIONID"] = lblApplicationId.Text;
            dRow["ISNRI"] = hdnIsNRI.Value;
            dt.Rows.Add(dRow);
            rowIndex = rowIndex + 1;         
        }

        //if (count == lvMeritList.Items.Count)
        //{
        //    objCommon.DisplayMessage(this.Page, "Please Select atleast One Candidate!", Page);           
        //    return;
        //}

        ds.Tables.Add(dt);
        string TestScore = ds.GetXml();

        int status = Convert.ToInt32(objMeritLst.INSERT_UPDATE_MERITLIST(TestScore, UANO, ProgramCode, EntranceExam, IpAdrress, DegreeNo));

        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Information Saved Successfully!", Page);            
            return;
        }
        if (status == 2)
        {
            objCommon.DisplayMessage(this.Page, "Information Updated Successfully!", Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
            return;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlTestDetails.SelectedIndex = 0;
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlGeneration.SelectedIndex = 0;
        ddlApplicationType.SelectedIndex = 0;
        ddlBranch.Items.Clear();        
        ddlSortBy.SelectedIndex = 0;
        txtRecDate.Text = string.Empty;
        pnlCount.Visible = false;
        ddlCategory.Items.Clear();
        pnllvSh.Visible = false;
        lvMeritList.DataSource = null;
        lvMeritList.DataBind();
        btnSubmit.Visible = false;
        btnLock.Visible = false;
        pnlSortBy.Visible = false;
        pnlBranch.Visible = false;
        pnlCategory.Visible = false;

    }


    protected void btnSubmitSheet_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTestDetails.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (ddlBranch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Branch/Program.", this.Page);
                return;
            }
            else if (ddlCategory.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 2)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Exam Slot.", this.Page);
                return;
            }
            else if (ddlCategory.SelectedValue == "0" && Convert.ToInt32(Session["OrgId"]) == 7)
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Schedule Slot.", this.Page);
                return;
            }

            GridView GV = new GridView();
            string ContentType = string.Empty;

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlTestDetails.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string Schedule = ddlCategory.SelectedValue;
            string branchno = string.Empty;

            foreach (ListItem items in ddlBranch.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                }
            }

            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;            
            ds = objMeritLst.ExcelAttendanceSheet(ADMBATCH, ProgramType, DegreeNo, branchno, Schedule);
         
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
                objCommon.DisplayMessage(upAttendance, "No Record Found.", this.Page);
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


    protected void ddlGeneration_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            if (ddlCategory.SelectedValue != "")
            {
                ddlCategory.SelectedIndex = -1;
            }
            if (ddlBranch.SelectedValue != "")
            {
                ddlBranch.SelectedIndex = -1;
            }

            if (ddlGeneration.SelectedValue == "0")
            {
                pnlBranch.Visible = false;
                pnlCategory.Visible = false;
                pnlSortBy.Visible = false;
            }
            else if (ddlGeneration.SelectedValue == "1")
            {
                int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                pnlBranch.Visible = true;
                pnlCategory.Visible = false;
                BindBranch(DegreeNo);
                pnlSortBy.Visible = true;
            }
            else if (ddlGeneration.SelectedValue == "2")
            {
                pnlBranch.Visible = false;
                pnlCategory.Visible = true;

                objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "ISNULL(activestatus,0)=1 AND isnull(allow_jee, 0) = 1", "CATEGORYNO ASC");
                ddlCategory.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlCategory.SelectedIndex = 0;
                pnlSortBy.Visible = false;
            }

            if (ddlGeneration.SelectedValue == "3")
            {       
                pnlBranch.Visible = false;
                pnlCategory.Visible = false;
                pnlSortBy.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMPMeritList.ddlGeneration_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void BindBranch(int DegreeNo)
    {
        try
        {
            DataSet ds = null;            
            ds = objMeritLst.GetBranch(DegreeNo);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = ds;
                ddlBranch.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlBranch.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }


    protected void btnLock_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        DataTable dt = CreateTable_TestScore();
        int rowIndex = 0;
        int count = 0;
        int UANO = Convert.ToInt32(Session["UserNo"]);
        int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        int ProgramCode = Convert.ToInt32(ddlProgramType.SelectedValue);
        int EntranceExam = Convert.ToInt32(ddlTestDetails.SelectedValue);
        string IpAdrress = Request.ServerVariables["REMOTE_HOST"];

        foreach (var lvItems in lvMeritList.Items)
        {
            DataRow dRow = dt.NewRow();
            HiddenField hdnUserNo = lvItems.FindControl("hdnUserNo") as HiddenField;
            HiddenField hdnAdmBatch = lvItems.FindControl("hdnAdmBatch") as HiddenField;
            HiddenField hdnCategoryNo = lvItems.FindControl("hdnCategoryNo") as HiddenField;
            Label lblCatMeritNo = lvItems.FindControl("lblCatMeritNo") as Label;
            Label lblScoreObtained = lvItems.FindControl("lblScoreObtained") as Label;
            Label lblGeneralMeritNo = lvItems.FindControl("lblGeneralMeritNo") as Label;
            HiddenField hdnBranchNo = lvItems.FindControl("hdnBranchNo") as HiddenField;
            Label lblApplicationId = lvItems.FindControl("lblApplicationId") as Label;

            dRow["USERNO"] = hdnUserNo.Value;
            dRow["ADMBATCH"] = hdnAdmBatch.Value;
            dRow["CATEGORY"] = hdnCategoryNo.Value;
            dRow["GENERALMERITNO"] = lblGeneralMeritNo.Text;
            dRow["MERITMARKS"] = lblScoreObtained.Text;
            dRow["CATEGORYMERITNO"] = lblCatMeritNo.Text;
            dRow["BRANCHNO"] = hdnBranchNo.Value;
            dRow["APPLICATIONID"] = lblApplicationId.Text;
            dt.Rows.Add(dRow);
            rowIndex = rowIndex + 1;         
        }

        if (count == lvMeritList.Items.Count)
        {
            objCommon.DisplayMessage(this.Page, "Please Select atleast One Candidate!", Page);
            return;
        }

        ds.Tables.Add(dt);
        string TestScore = ds.GetXml();

        int status = Convert.ToInt32(objMeritLst.LOCK_MERITLIST(TestScore, UANO, ProgramCode, EntranceExam, IpAdrress, DegreeNo));

        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Information Locked Successfully!", Page);
            return;
        }

        if (status == 2627)
        {
            objCommon.DisplayMessage(this.Page, "Information Is Already Locked!", Page);
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
            return;
        }
    }

   
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            
            DateTime? date = new DateTime();
            if (txtRecDate.Text != string.Empty || txtRecDate.Text != "")
            {
                date = Convert.ToDateTime(txtRecDate.Text.Trim());
            }
            else
            {
                date = null;
            }

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ScoreId = Convert.ToInt32(ddlTestDetails.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            int SortBy = Convert.ToInt32(ddlSortBy.SelectedValue);
            int ApplicationType = Convert.ToInt32(ddlApplicationType.SelectedValue);
            int Category = 0;
            int branchno = 0; 

            btnSubmit.Visible = true;
            btnLock.Visible = true;

            if (ddlCategory.SelectedValue == "")
            {
                Category = 0;
            }
            else
            {
                Category = Convert.ToInt32(ddlCategory.SelectedValue);
            }

            if (ddlBranch.SelectedValue == "")
            {
                branchno = 0;
            }
            else
            {
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
      
            DataSet ds = null;
            ds = objMeritLst.ExcelReport(ADMBATCH, ScoreId, DegreeNo, branchno, Category, SortBy, ApplicationType, date);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
           
            dt.Columns.Remove("CATEGORYNO");
            dt.Columns.Remove("ADMBATCH");
            dt.Columns.Remove("USERNO");
            dt.Columns.Remove("BRANCHNO");
            dt.Columns.Remove("REC_DT");
            dt.Columns.Remove("IS_NRI");
            dt.Columns.Remove("LOCK_STATUS");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dt;
                GV.DataBind();
                string attachment = "attachment; filename=MeritList.xls";
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
                objCommon.DisplayMessage(upAttendance, "No Record Found.", this.Page);
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