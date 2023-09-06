using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMPAppStagesConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    ADMPApplicationStagesConfigController objAASC = new ADMPApplicationStagesConfigController();
    List<CompaireStageDegreeMappingEntity> lstCSDME = new List<CompaireStageDegreeMappingEntity>();
    CompaireStageDegreeMappingEntity objCSDME = new CompaireStageDegreeMappingEntity();
    CompaireStageDependEntity objCSDEP = new CompaireStageDependEntity();
    List<CompaireStageDependEntity> lstCSDEP = new List<CompaireStageDependEntity>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Table-1 Phase
            BindListView_PhaseList();
            ViewState["action"] = "add";
            ViewState["PHASEID"] = 0;

            //Table-2 Stage
            ViewState["STAGEID"] = 0;
            BindALLDDLPhase();
            BindListView_StageList();
            //   rboN.Checked = true;

            //Table-3 Stage Degree Mapping
            BindALLDDLBatch();
            BindALLDDLDegree();
            BindListView_StageDegreeMap(0, 0);
            ViewState["OldCheckedList"] = null;

            //Table-4 Stage Degree Mapping
            BindStageDepBatch();
            BindStageDepDegree();
            BindDDLCurrentStage();
            BindListView_StageDependancies(0, 0, 0);
            ViewState["OldCheckedListDep"] = null;
        }

    }
    protected void btnTab2_Click(object sender, EventArgs e)
    {
        ViewState["STAGEID"] = 0;
        txtAppliStage.Text = string.Empty;
        txtDescription.Text = string.Empty;
        BindALLDDLPhase();
        BindListView_StageList();
        rboN.Checked = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    protected void btnTab3_Click(object sender, EventArgs e)
    {
        BindALLDDLBatch();
        BindALLDDLDegree();
        BindListView_StageDegreeMap(0, 0);
        ViewState["OldCheckedList"] = null;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
    }
    protected void btnTab4_Click(object sender, EventArgs e)
    {
        BindDDLCurrentStage();
        BindStageDepBatch();
        BindStageDepDegree();
        BindListView_StageDependancies(0, 0, 0);
        ViewState["OldCheckedListDep"] = null;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
    }

    #region Tab-1 Phase

    protected void BindListView_PhaseList()
    {
        try
        {

            DataSet ds = objAASC.GetAllApplicationPhaseList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlPhases.Visible = true;
                lvPhases.DataSource = ds.Tables[0];
                lvPhases.DataBind();


            }
            else
            {
                pnlPhases.Visible = true;
                lvPhases.DataSource = null;
                lvPhases.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvPhases.Items)
            {
                Label Status = dataitem.FindControl("lblStatusPhage") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmitPhase_Click(object sender, EventArgs e)
    {
        int PhaseId = 0;
        string PhaseName = txtPhases.Text.Trim();
        bool ActivePhase = hfdPhase.Value == "true" ? true : false;

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && ActivePhase == false)
        {

            //You can not inactive this record because of this is used in another one form.
            string refStatus = objAASC.CheckReferMasterTable(1, "Application Phase", Convert.ToInt32(ViewState["PHASEID"]));

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            PhaseId = Convert.ToInt32(ViewState["PHASEID"]);

            CustomStatus cs = (CustomStatus)objAASC.UpdateApplicationPhaseData(PhaseId, PhaseName, ActivePhase);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                clearPhaseData();
                BindListView_PhaseList();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            }
        }

        else
        {
            CustomStatus cs = (CustomStatus)objAASC.InsertApplicationPhaseData(PhaseName, ActivePhase);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                clearPhaseData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                clearPhaseData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                clearPhaseData();
            }

            BindListView_PhaseList();

        }
    }

    protected void btnEditPhase_Click(object sender, EventArgs e)
    {

        try
        {
            ImageButton btnEditPhase = sender as ImageButton;
            int PhaseId = Convert.ToInt32(btnEditPhase.CommandArgument);
            ViewState["PHASEID"] = Convert.ToInt32(btnEditPhase.CommandArgument);
            ShowDetails(PhaseId);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int PhaseId)
    {
        DataSet ds = objAASC.GetSingleApplicationPhaseData(PhaseId);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtPhases.Text = ds.Tables[0].Rows[0]["PHASE"].ToString();

            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPhases(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPhases(false);", true);
            }
        }


    }

    protected void btnCancelPhase_Click(object sender, EventArgs e)
    {
        clearPhaseData();
    }

    public void clearPhaseData()
    {
        ViewState["action"] = "add";
        ViewState["PHASEID"] = 0;
        txtPhases.Text = "";

    }

    #endregion

    #region Tab-2 Stages

    public void BindALLDDLPhase()
    {
        try
        {
            ddlAppliPhase.Items.Clear();
            ddlAppliPhase.Items.Insert(0, "Please Select");
            DataSet dsPhase = objCommon.FillDropDown("ACD_ADMP_STAGEPHASES", "PHASEID", "PHASE", "PHASEID > 0 AND ACTIVESTATUS = 1", "PHASE");


            ddlAppliPhase.DataSource = dsPhase;
            ddlAppliPhase.DataValueField = "PHASEID";
            ddlAppliPhase.DataTextField = "PHASE";
            ddlAppliPhase.DataBind();
            ddlAppliPhase.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.BindALLDDLPhase() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void BindListView_StageList()
    {
        try
        {

            DataSet ds = objAASC.GetAllApplicationStagesList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                PnlStage.Visible = true;
                lvStage.DataSource = ds.Tables[0];
                lvStage.DataBind();


            }
            else
            {
                PnlStage.Visible = true;
                lvStage.DataSource = null;
                lvStage.DataBind();

            }
            foreach (ListViewDataItem item in lvStage.Items)
            {
                Label StatusStage = item.FindControl("lblStatusStage") as Label;

                string Statuss = (StatusStage.Text);

                if (Statuss == "InActive")
                {
                    StatusStage.CssClass = "badge badge-danger";
                }
                else
                {
                    StatusStage.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmitStage_Click(object sender, EventArgs e)
    {
        int StageId = 0; string isAllowProcess = string.Empty;
        int PhaseId = Convert.ToInt32(ddlAppliPhase.SelectedValue);
        string StageName = txtAppliStage.Text.Trim();
        string Description = txtDescription.Text.Trim();
        bool ActiveStage = hfdStage.Value == "true" ? true : false;
        if (rboP.Checked == true)
        {
            isAllowProcess = "P";
        }
        else if (rboV.Checked == true)
        {
            isAllowProcess = "V";
        }
        else if (rboS.Checked == true)
        {
            isAllowProcess = "S";
        }
        else if (rboN.Checked == true)
        {
            isAllowProcess = "";
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && ActiveStage == false)
        {

            //You can not inactive this record because of this is used in another one form.
            string refStatus = objAASC.CheckReferMasterTable(2, "Application Stages", Convert.ToInt32(ViewState["STAGEID"]));

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            StageId = Convert.ToInt32(ViewState["STAGEID"]);

            CustomStatus cs = (CustomStatus)objAASC.UpdateApplicationStagesData(StageId, PhaseId, StageName, Description, ActiveStage, isAllowProcess);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                clearStageData();
                BindListView_StageList();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            }
        }

        else
        {
            //Edit 
            CustomStatus cs = (CustomStatus)objAASC.InsertApplicationStagesData(PhaseId, StageName, Description, ActiveStage, isAllowProcess);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                clearStageData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                clearStageData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                clearStageData();
            }

            BindListView_StageList();

        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }

    protected void btnEditStage_Click(object sender, EventArgs e)
    {

        try
        {
            ImageButton btnEditStage = sender as ImageButton;
            int StageId = Convert.ToInt32(btnEditStage.CommandArgument);
            ViewState["STAGEID"] = Convert.ToInt32(btnEditStage.CommandArgument);
            ShowStageDetails(StageId);
            ViewState["action"] = "edit";
            ddlAppliPhase.Enabled = false;

        }
        catch (Exception ex)
        {
            throw;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }

    private void ShowStageDetails(int StageId)
    {
        string IsAllowProcess = string.Empty;
        DataSet ds = objAASC.GetSingleApplicationStagesData(StageId);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlAppliPhase.SelectedValue = ds.Tables[0].Rows[0]["PHASEID"].ToString();
            txtAppliStage.Text = ds.Tables[0].Rows[0]["STAGENAME"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            IsAllowProcess = ds.Tables[0].Rows[0]["ISALLOWPROCESS"].ToString().Trim();

            rboP.Checked = false;
            rboV.Checked = false;
            rboS.Checked = false;
            rboN.Checked = false;
            if (IsAllowProcess == "P")
            {
                rboP.Checked = true;
            }
            else if (IsAllowProcess == "V")
            {
                rboV.Checked = true;
            }
            else if (IsAllowProcess == "S")
            {
                rboS.Checked = true;
            }
            else if (IsAllowProcess == string.Empty)
            {
                rboN.Checked = true;
            }

            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStages(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStages(false);", true);
            }
        }


    }

    protected void btnCancelStage_Click(object sender, EventArgs e)
    {
        clearStageData();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }

    public void clearStageData()
    {
        ViewState["action"] = "add";
        ViewState["STAGEID"] = 0;

        ddlAppliPhase.SelectedIndex = 0;
        txtAppliStage.Text = "";
        txtDescription.Text = "";
        rboN.Checked = true;
        ddlAppliPhase.Enabled = true;
    }

    #endregion

    #region Tab-3 Stages Degree Mapping
    public void BindALLDDLBatch()
    {
        try
        {
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Insert(0, "Please Select");
            DataSet dsBatch = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNO DESC");


            ddlAdmBatch.DataSource = dsBatch;
            ddlAdmBatch.DataValueField = "BATCHNO";
            ddlAdmBatch.DataTextField = "BATCHNAME";
            ddlAdmBatch.DataBind();
            ddlAdmBatch.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.BindALLDDLBatch() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    public void BindALLDDLDegree()
    {
        try
        {
            ddlDegremapping.Items.Clear();
            ddlDegremapping.Items.Insert(0, "Please Select");
            DataSet dsDegree = objCommon.FillDropDown("ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ACTIVESTATUS = 1", "DEGREENAME");


            ddlDegremapping.DataSource = dsDegree;
            ddlDegremapping.DataValueField = "DEGREENO";
            ddlDegremapping.DataTextField = "DEGREENAME";
            ddlDegremapping.DataBind();
            ddlDegremapping.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.BindALLDDLDegree() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlAdmBatch.SelectedIndex > 0 && ddlDegremapping.SelectedIndex > 0)
            {
                BindListView_StageDegreeMap(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegremapping.SelectedValue));
                CheckBoxVisibility(true);
            }
            else
            {
                BindListView_StageDegreeMap(0, 0);
                CheckBoxVisibility(false);
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ClubStudentMapping.ddlCollege_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlDegremapping_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedIndex > 0 && ddlDegremapping.SelectedIndex > 0)
            {
                BindListView_StageDegreeMap(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegremapping.SelectedValue));
                CheckBoxVisibility(true);
            }
            else
            {
                BindListView_StageDegreeMap(0, 0);
                CheckBoxVisibility(false);
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ClubStudentMapping.ddlCollege_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void BindListView_StageDegreeMap(int BatchNo, int DegreeNo)
    {
        try
        {
            ViewState["action"] = "add";
            pnlStageDegree.Visible = false;
            lvStageDegree.Visible = false;
            lvStageDegree.DataSource = null;
            lvStageDegree.DataBind();

            DataSet ds = objAASC.GetAllStagesDegreeMappingList(BatchNo, DegreeNo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ViewState["action"] = "edit";
                pnlStageDegree.Visible = true;
                lvStageDegree.Visible = true;
                lvStageDegree.DataSource = ds;
                lvStageDegree.DataBind();

            }
            else
            {
                ViewState["action"] = "add";
                pnlStageDegree.Visible = false;
                lvStageDegree.Visible = false;
                lvStageDegree.DataSource = null;
                lvStageDegree.DataBind();

            }
            if (lvStageDegree.Items.Count > 0)
            {
                ViewState["OldCheckedList"] = null;

                int i = 0;
                foreach (ListViewDataItem lv in lvStageDegree.Items)
                {
                    CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                    HiddenField hfdStageId = lv.FindControl("hfdStageId") as HiddenField;
                    TextBox txtSequance = lv.FindControl("txtSequance") as TextBox;

                    if (hfdStageId.Value != null && txtSequance.Text.Trim().Length > 0)
                    {
                        if (Convert.ToInt32(hfdStageId.Value) > 0 && Convert.ToInt32(txtSequance.Text.Trim()) > 0)
                        {
                            CompaireStageDegreeMappingEntity objCSDMEE = new CompaireStageDegreeMappingEntity();
                            objCSDMEE.Stageid = Convert.ToInt32(hfdStageId.Value);

                            lstCSDME.Insert(i, objCSDMEE);
                            //lstCCDE.Add(i,objCCDE);

                            chkIsActive.Checked = true;
                            i++;
                        }
                    }
                }
                if (lstCSDME.Count > 0)
                {
                    ViewState["OldCheckedList"] = lstCSDME;
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitStageMapping_Click(object sender, System.EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
            int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegremapping.SelectedValue);

            string displaymsg = "";
            int rowIndex = 0;
            int countForCheked = 0;
            string _validate = string.Empty;
            // validation methods
            foreach (ListViewDataItem lv in lvStageDegree.Items)
            {
                HiddenField hfdStageId = lv.FindControl("hfdStageId") as HiddenField; ;
                CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                TextBox txtSequance = lv.FindControl("txtSequance") as TextBox;
                if (chkIsActive.Checked)
                {
                    countForCheked++;
                }

                int rowcheckDuplicateEntryIndex = 0;
                foreach (var checkGName in lvStageDegree.Items)
                {
                    CheckBox chkIsActive_check = checkGName.FindControl("chkIsActive") as CheckBox;
                    TextBox txtSequance_check = checkGName.FindControl("txtSequance") as TextBox;

                    if (chkIsActive.Checked && chkIsActive_check.Checked)
                    {
                        if (rowcheckDuplicateEntryIndex == rowIndex)
                        {

                            if (Convert.ToInt32(txtSequance.Text) == 0)
                            {
                                objCommon.DisplayMessage(this, "Please Enter Checked Stages Sequance Number Greater than Zero", this.Page);
                                return;
                            }
                            rowcheckDuplicateEntryIndex += 1;
                            continue;
                        }

                        if (Convert.ToInt32(txtSequance.Text) == 0)
                        {
                            objCommon.DisplayMessage(this, "Please Enter Checked Stages Sequance Number Greater than Zero", this.Page);
                            return;
                        }
                        if (Convert.ToInt32(txtSequance.Text) == Convert.ToInt32(txtSequance_check.Text))
                        {
                            objCommon.DisplayMessage(this, "Does not allow duplicate Sequence Number", this.Page);
                            return;
                        }

                    }

                    rowcheckDuplicateEntryIndex += 1;

                }
                //if (chkIsActive.Checked.Equals(count<0))
                
                rowIndex += 1;
            }

            if (countForCheked <= 0)
            {
                objCommon.DisplayMessage(this, "Please Select Stages..!", this.Page);
                return;
            }

            // save operation methods
            foreach (ListViewDataItem lv in lvStageDegree.Items)
            {
                HiddenField hfdStageId = lv.FindControl("hfdStageId") as HiddenField; ;
                CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                TextBox txtSequance = lv.FindControl("txtSequance") as TextBox;

                if (ViewState["OldCheckedList"] != null)
                {
                    lstCSDME = (List<CompaireStageDegreeMappingEntity>)ViewState["OldCheckedList"];
                    if (lstCSDME != null && lstCSDME.Count > 0)
                    {
                        foreach (CompaireStageDegreeMappingEntity objCSDME in lstCSDME)
                        {
                            if (objCSDME.Stageid == Convert.ToInt32(hfdStageId.Value) && !chkIsActive.Checked)
                            {
                                //delete from table unselected(Old) Stage Mapping for Branch and Degree
                                DeleteStgeDegreeMap(BatchNo, DegreeNo, Convert.ToInt32(hfdStageId.Value));
                            }

                        }
                    }

                }


                if (chkIsActive.Checked && Convert.ToInt32(txtSequance.Text.Trim()) > 0)
                {
                    //HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;
                    SaveStageDegreeMap(BatchNo, DegreeNo, Convert.ToInt32(hfdStageId.Value), Convert.ToInt32(txtSequance.Text.Trim()));
                }

            }

            displaymsg = "Record added successfully.";
            objCommon.DisplayMessage(this, displaymsg, this.Page);

            clearStageMapping();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void SaveStageDegreeMap(int BatchNo, int DegreeNo, int StageId, int SequnceNo)
    {
        try
        {
            int ret = 0;
            //displaymsg = "Record added successfully.";
            ret = objAASC.INSUPDStagesDegreeMappingData(BatchNo, DegreeNo, StageId, SequnceNo);

            //objCommon.DisplayMessage(displaymsg, this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StagesDegreeMapping.SaveStageDegreeMap --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void DeleteStgeDegreeMap(int BatchNo, int DegreeNo, int StageId)
    {
        try
        {
            int ret = 0;
            ret = objAASC.DeleteStagesDegreeMapping(BatchNo, DegreeNo, StageId);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StageDegreeMapping.DeleteStgeDegreeMap --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void bntCancelStageMapping_Click(object sender, EventArgs e)
    {
        clearStageMapping();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
    }
    public void clearStageMapping()
    {
        ViewState["action"] = "add";

        ddlAdmBatch.SelectedIndex = 0;
        ddlDegremapping.SelectedIndex = 0;

        ViewState["OldCheckedList"] = null;
        BindListView_StageDegreeMap(0, 0);
        CheckBoxVisibility(false);
    }

    protected void CheckBoxVisibility(bool isEnabled)
    {
        foreach (ListViewDataItem lv in lvStageDegree.Items)
        {
            CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
            TextBox txtSequance = lv.FindControl("txtSequance") as TextBox;

            chkIsActive.Enabled = isEnabled;
            txtSequance.Enabled = isEnabled;
        }

    }

    [Serializable]
    public class CompaireStageDegreeMappingEntity
    {
        private int batchno = 0;
        private int degreeno = 0;
        private int stageid = 0;
        private int sequanceno = 0;

        public int Batchno
        {
            get { return batchno; }
            set { batchno = value; }
        }
        public int Degreeno
        {
            get { return degreeno; }
            set { degreeno = value; }
        }
        public int Stageid
        {
            get { return stageid; }
            set { stageid = value; }
        }
        public int Sequanceno
        {
            get { return sequanceno; }
            set { sequanceno = value; }
        }
    }
    #endregion

    #region Table-4 Stages Dependancies

    public void BindStageDepBatch()
    {
        try
        {
            ddlDepAdmBatch.Items.Clear();
            ddlDepAdmBatch.Items.Insert(0, "Please Select");
            DataSet dsBatch = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNO DESC");
            ddlDepAdmBatch.DataSource = dsBatch;
            ddlDepAdmBatch.DataValueField = "BATCHNO";
            ddlDepAdmBatch.DataTextField = "BATCHNAME";
            ddlDepAdmBatch.DataBind();
            ddlDepAdmBatch.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.BindStageDepBatch() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    public void BindStageDepDegree()
    {
        try
        {
            ddlDepDegree.Items.Clear();
            ddlDepDegree.Items.Insert(0, "Please Select");
            DataSet dsDegree = objCommon.FillDropDown("ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ACTIVESTATUS = 1", "DEGREENAME");
            ddlDepDegree.DataSource = dsDegree;
            ddlDepDegree.DataValueField = "DEGREENO";
            ddlDepDegree.DataTextField = "DEGREENAME";
            ddlDepDegree.DataBind();
            ddlDepDegree.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.BindALLDDLDegree() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    public void BindDDLCurrentStage()
    {
        try
        {
            ddlCurrentStage.Items.Clear();
            ddlCurrentStage.Items.Insert(0, "Please Select");
            DataSet dsCurrentStage = objCommon.FillDropDown("ACD_ADM_APPLICATIONSTAGES ST INNER JOIN ACD_ADMP_STAGEPHASES PH ON (PH.PHASEID = ST.PHASEID)", "CONCAT(PHASE,'-',STAGENAME)AS STAGENAME", "STAGEID", "STAGEID > 0 AND ST.ACTIVESTATUS = 1", "STAGEID");


            ddlCurrentStage.DataSource = dsCurrentStage;
            ddlCurrentStage.DataValueField = "STAGEID";
            ddlCurrentStage.DataTextField = "STAGENAME";
            ddlCurrentStage.DataBind();
            ddlCurrentStage.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.BindDDLCurrentStage() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlDepAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDepDegree.Items.Clear();
            ddlDepDegree.Items.Insert(0, "Please Select");
            ddlCurrentStage.Items.Clear();
            ddlCurrentStage.Items.Insert(0, "Please Select");
            BindListView_StageDependancies(0, 0, 0);
            CheckBoxVisibilityDepend(false);

            if (ddlDepAdmBatch.SelectedIndex > 0)
            {
                BindStageDepDegree();
                BindListView_StageDependancies(0, 0, 0);
                CheckBoxVisibilityDepend(false);
            }
            
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.ddlDepAdmBatch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlDepDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCurrentStage.Items.Clear();
            ddlCurrentStage.Items.Insert(0, "Please Select");
            BindListView_StageDependancies(0, 0, 0);
            CheckBoxVisibilityDepend(false);

            if (ddlDepDegree.SelectedIndex > 0)
            {

                BindDDLCurrentStage();
                BindListView_StageDependancies(0, 0, 0);
                CheckBoxVisibilityDepend(false);
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.ddlDepDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlCurrentStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int batchno = Convert.ToInt32(ddlDepAdmBatch.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDepDegree.SelectedValue);
            if (ddlCurrentStage.SelectedIndex > 0)
            {
                BindListView_StageDependancies(Convert.ToInt32(ddlCurrentStage.SelectedValue), batchno, degreeno);
                CheckBoxVisibilityDepend(true);
            }
            else
            {
                BindListView_StageDependancies(0, 0, 0);
                CheckBoxVisibilityDepend(false);
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPApplicationStagesConfig.ddlCurrentStage_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void BindListView_StageDependancies(int StageId, int BatchNo, int DegreeNo)
    {
        try
        {
            ViewState["action"] = "add";
            pnldependancies.Visible = false;
            lvdependancies.Visible = false;
            lvdependancies.DataSource = null;
            lvdependancies.DataBind();

            DataSet ds = objAASC.GetAllStagesDependanciesList(StageId, BatchNo, DegreeNo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ViewState["action"] = "edit";
                pnldependancies.Visible = true;
                lvdependancies.Visible = true;
                lvdependancies.DataSource = ds;
                lvdependancies.DataBind();

            }
            else
            {
                ViewState["action"] = "add";
                pnldependancies.Visible = false;
                lvdependancies.Visible = false;
                lvdependancies.DataSource = null;
                lvdependancies.DataBind();

            }
            if (lvdependancies.Items.Count > 0)
            {
                ViewState["OldCheckedListDep"] = null;

                int i = 0;
                foreach (ListViewDataItem lv in lvdependancies.Items)
                {
                    CheckBox chkCurrentStage = lv.FindControl("chkCurrentStage") as CheckBox;
                    HiddenField hfdCurrentStage = lv.FindControl("hfdCurrentStage") as HiddenField;
                    HiddenField hfdActiveStatus = lv.FindControl("hfdActiveStatus") as HiddenField;

                    if (hfdCurrentStage.Value != null && hfdActiveStatus.Value != null)
                    {
                        if (Convert.ToInt32(hfdCurrentStage.Value) > 0 && Convert.ToInt32(hfdActiveStatus.Value) > 0)
                        {
                            CompaireStageDependEntity objCSDEPP = new CompaireStageDependEntity();
                            objCSDEPP.Nextstageid = Convert.ToInt32(hfdCurrentStage.Value);

                            lstCSDEP.Insert(i, objCSDEPP);

                            chkCurrentStage.Checked = true;
                            i++;
                        }
                    }
                }
                if (lstCSDEP.Count > 0)
                {
                    ViewState["OldCheckedListDep"] = lstCSDEP;
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnsubDependancy_Click(object sender, System.EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
            int BatchNo = 0; int DegreeNo = 0;
            int CurrentStageId = Convert.ToInt32(ddlCurrentStage.SelectedValue);
            BatchNo = Convert.ToInt32(ddlDepAdmBatch.SelectedValue);
            DegreeNo = Convert.ToInt32(ddlDepDegree.SelectedValue);

            int countForCheked = 0;
            string displaymsg = "";
            foreach (ListViewDataItem lv in lvdependancies.Items)
            {
                HiddenField hfdCurrentStage = lv.FindControl("hfdCurrentStage") as HiddenField; ;
                CheckBox chkCurrentStage = lv.FindControl("chkCurrentStage") as CheckBox;
                
                if (chkCurrentStage.Checked)
                {
                    countForCheked++;
                }

                if (ViewState["OldCheckedListDep"] != null)
                {
                    lstCSDEP = (List<CompaireStageDependEntity>)ViewState["OldCheckedListDep"];
                    if (lstCSDEP != null && lstCSDEP.Count > 0)
                    {
                        foreach (CompaireStageDependEntity objCSDEP in lstCSDEP)
                        {
                            if (objCSDEP.Nextstageid == Convert.ToInt32(hfdCurrentStage.Value) && !chkCurrentStage.Checked)
                            {
                                //delete from table unselected(Old) Stage Mapping for Branch and Degree
                                DeleteStgeDepend(Convert.ToInt32(ddlCurrentStage.SelectedValue), Convert.ToInt32(hfdCurrentStage.Value), BatchNo, DegreeNo);
                            }

                        }
                    }

                }


                if (chkCurrentStage.Checked)
                {
                    //HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;
                    SaveStageDepend(Convert.ToInt32(ddlCurrentStage.SelectedValue), Convert.ToInt32(hfdCurrentStage.Value), BatchNo, DegreeNo);
                }

            }
            if (countForCheked <= 0)
            {
                objCommon.DisplayMessage(this, "Please Select Next Stages..!", this.Page);
                return;
            }

            displaymsg = "Record added successfully.";
            objCommon.DisplayMessage(this, displaymsg, this.Page);

            clearStageDepend();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StagesDependancies.btnsubDependancy_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void SaveStageDepend(int CurrentStageId, int NextStageId, int BatchNo, int DegreeNo)
    {
        try
        {
            int ret = 0;
            //displaymsg = "Record added successfully.";
            ret = objAASC.InsertStagesDependanciesData(CurrentStageId, NextStageId, BatchNo, DegreeNo);
            //objCommon.DisplayMessage(displaymsg, this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StagesDependancies.SaveStageDepend --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void DeleteStgeDepend(int CurrentStageId, int NextStageId, int BatchNo, int DegreeNo)
    {
        try
        {
            int ret = 0;
            ret = objAASC.DeleteStagesDependancies(CurrentStageId, NextStageId, BatchNo, DegreeNo);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StagesDependancies.DeleteStgeDepend --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btncancelDependancy_Click(object sender, EventArgs e)
    {
        clearStageDepend();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
    }
    public void clearStageDepend()
    {
        ViewState["action"] = "add";

        ddlDepAdmBatch.SelectedIndex = 0;
        ddlDepDegree.SelectedIndex = 0;
        ddlCurrentStage.SelectedIndex = 0;

        ViewState["OldCheckedListDep"] = null;
        BindListView_StageDependancies(0, 0, 0);
        CheckBoxVisibilityDepend(false);
    }
    protected void CheckBoxVisibilityDepend(bool isEnabled)
    {
        foreach (ListViewDataItem lv in lvdependancies.Items)
        {
            CheckBox chkCurrentStage = lv.FindControl("chkCurrentStage") as CheckBox;

            chkCurrentStage.Enabled = isEnabled;
        }

    }

    [Serializable]
    public class CompaireStageDependEntity
    {
        private int nextstageid = 0;


        public int Nextstageid
        {
            get { return nextstageid; }
            set { nextstageid = value; }
        }
    }
    #endregion
}