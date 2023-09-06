using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS.UAIMS;
using BusinessLogicLayer.BusinessLogic.PostAdmission;
using System.Data;

public partial class IndexScoreGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    IndexScoreGenerateController objISG = new IndexScoreGenerateController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ScoreGen_ID"] = 0;
            ViewState["action"] = "add";

            FillDropDown();
            BindListView();
        }
    }

    public void FillDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "DISTINCT ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID > 0 AND ACTIVE_STATUS=1", "ACADMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlAppType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0 AND ACTIVESTATUS=1 AND UA_SECTIONNAME IN('UG','PG')", "UA_SECTION DESC");
            //objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0 AND ACTIVESTATUS=1", "UA_SECTION DESC");
            ////objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO >0 AND ACTIVESTATUS=1", "DEGREENO");

            ddlDegree.Items.Clear();
            ddlDegree.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlDegree.SelectedIndex = 0;

            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlBranch.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.FillDropDown() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }


    protected void BindListView()
    {
        try
        {
            DataSet ds = objISG.GetAllIndexScoreApplicantList("", "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlIndexScoreGenerateList.Visible = true;
                lvIndexScoreGenerateList.DataSource = ds.Tables[0];
                lvIndexScoreGenerateList.DataBind();
            }
            else
            {
                pnlIndexScoreGenerateList.Visible = false;
                lvIndexScoreGenerateList.DataSource = null;
                lvIndexScoreGenerateList.DataBind();
            }


            /*DataSet ds = objAFPCEC.GetRetADMPFeePayConfigListData(0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlFeePayConfig.Visible = true;
                lvFeePayConfig.DataSource = ds.Tables[0];
                lvFeePayConfig.DataBind();
            }
            else
            {
                pnlFeePayConfig.Visible = false;
                lvFeePayConfig.DataSource = null;
                lvFeePayConfig.DataBind();
            }


            foreach (ListViewDataItem dataitem in lvFeePayConfig.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss.Equals("Started"))
                {
                    Status.CssClass = "badge badge-success";
                }
                else
                {
                    Status.CssClass = "badge badge-danger";
                }

            }*/


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.BindListView() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.Items.Clear();
            ddlBranch.Items.Clear();

            if (ddlAppType.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlAppType.SelectedValue, "D.DEGREENO");
            }
            ddlDegree.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();

            ddlBranch.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlBranch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.ddlAppType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlBranch.Items.Clear();

            if (ddlAppType.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH BH LEFT OUTER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = BH.BRANCHNO)", "BH.BRANCHNO", "BH.LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DB.ACTIVESTATUS = 1", "BH.LONGNAME");
            }
            ddlBranch.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlBranch.SelectedIndex = 0;
            ddlBranch.Focus();

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreGeneration.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void btnGenerate_Click(object sender, System.EventArgs e)
    {
        try
        {
            int AdmBatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            int BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);

            if (AdmBatchNo < 0)
            {
                objCommon.DisplayMessage("Please Select Admission Batch.", this.Page);
                return;
            }
            if (DegreeNo < 0)
            {
                objCommon.DisplayMessage("Please Select Degree.", this.Page);
                return;
            }
            if (BranchNo < 0)
            {
                objCommon.DisplayMessage("Please Select Program/Branch.", this.Page);
                return;
            }

            int ret = 0;
            string displaymsg = "";
            ret = Convert.ToInt32(objISG.GenerateIndexScore(DegreeNo, BranchNo, AdmBatchNo));
            if (ret == 1)
            {
                displaymsg = "Successfully generated score.";
            }
            /*else if (ret > 0)
            {
                ClearAllControls();
            }*/
            else
            {
                displaymsg = "Error!Please try again to generate score.";
            }
            objCommon.DisplayMessage(displaymsg, this.Page);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }

    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        try
        {
            ClearAllControls();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }
    private void ClearAllControls()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlAppType.SelectedIndex = 0;

        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "Please Select"));
        ddlDegree.SelectedIndex = 0;

        ddlBranch.Items.Clear();
        ddlBranch.Items.Insert(0, new ListItem("Please Select", "Please Select"));
        ddlBranch.SelectedIndex = 0;
    }

    protected void btnIndexMarks_Click(object sender, System.EventArgs e)
    {
        try
        {


            if (ddlDegree.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage("Please Select Degree.", this.Page);
                return;
            }

            if (ddlBranch.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage("Please Select Branch.", this.Page);
                return;
            }

            Button btnIndexMarks = (Button)sender;
            string[] commandArgs = btnIndexMarks.CommandArgument.ToString().Split(new char[] { ',' });
            string Id = commandArgs[0];
            string Userno = commandArgs[1];
            string ADMBATCH = commandArgs[2];
            string BRPREF = commandArgs[3];
            string APPLICANTNAME = commandArgs[4];
            string APPLICATION_ID = commandArgs[5];


            DataSet dsScoreList = objISG.GetAllIndexScoreMarksList(ddlDegree.SelectedValue, ddlBranch.SelectedValue, ADMBATCH, Userno);
            ////DataSet dsScoreList = objISG.GetAllIndexScoreMarksList("0", "0", "8", "2");
            if (dsScoreList != null && dsScoreList.Tables[0].Rows.Count > 0)
            {
                lvIndexMark.DataSource = dsScoreList.Tables[0];
                lvIndexMark.DataBind();
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "OpenIndexListModal();", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.btnIndexMarks_Click() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

    protected void btnMarksDetails_Click(object sender, System.EventArgs e)
    {
        try
        {

            Button btnIndexMarks = (Button)sender;
            string[] commandArgs = btnIndexMarks.CommandArgument.ToString().Split(new char[] { ',' });
            string Id = commandArgs[0];
            string Userno = commandArgs[1];
            string ADMBATCH = commandArgs[2];
            string BRPREF = commandArgs[3];
            string APPLICANTNAME = commandArgs[4];
            string APPLICATION_ID = commandArgs[5];

            lblNameOfStd.Text = APPLICANTNAME;
            lblAppNo.Text = APPLICATION_ID;

            DataSet dsList = objISG.GetAllIndexScoreQualificationMarksList(Userno, "2");
            if (dsList != null && dsList.Tables[0].Rows.Count > 0)
            {
                lvMarksDetailsList.DataSource = dsList.Tables[0];
                lvMarksDetailsList.DataBind();
            }

            if (dsList != null && dsList.Tables[2].Rows.Count > 0)
            {
                lvProgramsList.DataSource = dsList.Tables[2];
                lvProgramsList.DataBind();
            }

            ////Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "OpenMarksListModal()", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "OpenMarksListModal();", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("IndexScoreGeneration.btnIndexMarks_Click() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

}