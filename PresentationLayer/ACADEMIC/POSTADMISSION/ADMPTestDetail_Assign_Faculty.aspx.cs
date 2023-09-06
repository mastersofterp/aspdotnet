using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_POSTADMISSION_ADMPTestDetail_Assign_Faculty : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPTestDetails_Score_Assign_VerifyController objFaculty = new ADMPTestDetails_Score_Assign_VerifyController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int isverify = Convert.ToInt32(Request.QueryString["isverify"]);

            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMPTestDetail_Assign_Faculty.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO > 0 AND UA_TYPE IN (1, 3, 5)", "UA_FULLNAME");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPTestDetail_Assign_Faculty.PopulateDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND ACTIVESTATUS=1", "UA_SECTION");
        ddlProgramType.SelectedIndex = 0;
        ddlTestName.SelectedIndex = 0;
    }

    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlTestName, "ACD_ADMP_TESTSCORE_MASTER", "SCOREID", "TESTNAME", "ACTIVE_STATUS=1 AND SCOREID>0 AND DEGREE_TYPE LIKE '%" + ddlProgramType.SelectedValue + "%'", "SCOREID");
            ddlTestName.SelectedIndex = 0;  
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPTestDetail_Assign_Faculty.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlTestScore.Visible = false;
        lvTestScore.Visible = false;
        btnAssign.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgramType.SelectedIndex = 0;
        ddlTestName.SelectedIndex = 0;
        ddlFaculty.SelectedIndex = 0;
        ddlPaymentStatus.SelectedIndex = 0;
        lvTestScore.Visible = false;
        btnAssign.Visible = false;
        DataPager1.Visible = false;
        pnlTestScore.Visible = false;

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
        int pagesize = Convert.ToInt32(DataPager1.PageSize);
        NumberDropDown.SelectedValue = pagesize.ToString();
        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
    }

    public void BindListView()
    {
        int ScoreId = Convert.ToInt32(ddlTestName.SelectedValue);
        int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
        int Ua_No = Convert.ToInt32(ddlFaculty.SelectedValue);
        int PaymentStatus = Convert.ToInt32(ddlPaymentStatus.SelectedValue);

        DataSet ds = objFaculty.GetTestScoreDataListByFaculty(ScoreId, BatchNo, ProgramType, Ua_No, PaymentStatus);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvTestScore.Visible = true;
            pnlTestScore.Visible = true;
            btnAssign.Visible = true;
            lvTestScore.DataSource = ds;
            lvTestScore.DataBind();
        }
        else
        {
            pnlTestScore.Visible = false;
            lvTestScore.DataSource = null;
            btnAssign.Visible = false;
            lvTestScore.DataBind();
            objCommon.DisplayMessage(this.Page, "Record not found.", Page);
            return;
        }
    }

    public DataTable CreateTable_TestScore()
    {
        DataTable dtTestScore = new DataTable();
        dtTestScore.Columns.Add("USERNO", typeof(int));
        dtTestScore.Columns.Add("SCOREID", typeof(int));
        dtTestScore.Columns.Add("UA_NO", typeof(int));
        return dtTestScore;
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = CreateTable_TestScore();
        int rowIndex = 0;
        int count = 0;

        foreach (var lvItems in lvTestScore.Items)
        {
            DataRow dRow = dt.NewRow();
            HiddenField hfdScoreId = lvItems.FindControl("hfdScoreId") as HiddenField;
            HiddenField hfdUserNo = lvItems.FindControl("hfdUserNo") as HiddenField;
            CheckBox chkIsVerify = lvItems.FindControl("chkIsVerify") as CheckBox;

            if (chkIsVerify.Checked == true)
            {
                dRow["USERNO"] = hfdUserNo.Value;
                dRow["SCOREID"] = ddlTestName.SelectedValue;
                dRow["UA_NO"] = ddlFaculty.SelectedValue;
                dt.Rows.Add(dRow);
                rowIndex = rowIndex + 1;
            }
            else if (chkIsVerify.Checked == false)
            {
                count++;
            }
        }

        if (count == lvTestScore.Items.Count)
        {
            objCommon.DisplayMessage(this.Page, "Please Select atleast One Candidate!", Page);
            BindListView();
            return;
        }


        ds.Tables.Add(dt);
        string TestScore = ds.GetXml();
        int Ua_No = Convert.ToInt32(ddlFaculty.SelectedValue);

        int status = Convert.ToInt32(objFaculty.AssignFacultyTestScore(Ua_No, TestScore));

        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Faculty Assigned Successfully!", Page);
            BindListView();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
            return;
        }
    }

    protected void lvTestScore_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindListView();
    }

    protected void NumberDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
        DataPager1.PageSize = Convert.ToInt32(NumberDropDown.SelectedValue);
    }

   
}