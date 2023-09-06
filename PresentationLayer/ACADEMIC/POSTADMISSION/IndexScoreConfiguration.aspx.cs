using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessLogic.PostAdmission;

public partial class IndexScoreConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    IndexScoreConfigController objISC = new IndexScoreConfigController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";

            ddlBranchList();
            ddlAdmBatchList();
            ddlDegreeList();
            BindListView_AddSubDetails();
        }
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
            objCommon.ShowError(Page, "IndexScoreConfiguration.BindALLDDL() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region All DropDown List
    private void ddlAdmBatchList()
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
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlAdmissionBatchList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlAdmBatch.SelectedIndex > 0)
            {
                ddlDegreeList();
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, "Please Select");
                pnlSubjectMarks.Visible = false;
                lvSubjectMarks.DataSource = null;
                lvSubjectMarks.DataBind();

            }
            else
            {
                pnlSubjectMarks.Visible = false;
                lvSubjectMarks.DataSource = null;
                lvSubjectMarks.DataBind();

            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void ddlDegreeList()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ACTIVESTATUS = 1", "DEGREENAME");
            BindALLDDL(ref ddlDegree, ds, "DEGREENAME", "DEGREENO");

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlDegreeList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlDegree.SelectedIndex > 0)
            {
                ddlBranchList();
                pnlSubjectMarks.Visible = false;
                lvSubjectMarks.DataSource = null;
                lvSubjectMarks.DataBind();

            }
            else
            {
                pnlSubjectMarks.Visible = false;
                lvSubjectMarks.DataSource = null;
                lvSubjectMarks.DataBind();

            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void ddlBranchList()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_BRANCH BH LEFT OUTER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = BH.BRANCHNO)", "BH.BRANCHNO", "BH.LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DB.ACTIVESTATUS = 1", "BH.LONGNAME");
            BindALLDDL(ref ddlBranch, ds, "LONGNAME", "BRANCHNO");
            BindListView_SubjectMarksDetails(ds.Tables[1]);
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlDegreeList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                commonBindExistAndEditData(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), rdoConfigType.SelectedValue.ToString(), rdoAdvType.SelectedValue.ToString());

            }
            else
            {

                pnlSubjectMarks.Visible = false;
                lvSubjectMarks.DataSource = null;
                lvSubjectMarks.DataBind();

            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlBranch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Radio Button Selected Index
    protected void rdoConfigType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            commonBindExistAndEditData(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), rdoConfigType.SelectedValue.ToString(), rdoAdvType.SelectedValue.ToString());

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlBranch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void rdoAdvType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            commonBindExistAndEditData(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), rdoConfigType.SelectedValue.ToString(), rdoAdvType.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "IndexScoreConfiguration.ddlBranch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region ListViews
    private void BindListView_SubjectMarksDetails(DataTable dt)
    {
        try
        {

            if (dt != null && dt.Rows.Count > 0)
            {

                pnlSubjectMarks.Visible = true;
                lvSubjectMarks.DataSource = dt;// ds;
                lvSubjectMarks.DataBind();

            }
            else
            {

                pnlSubjectMarks.Visible = false;
                lvSubjectMarks.DataSource = null;
                lvSubjectMarks.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IndexScoreConfiguration.BindListView_SubjectMarksDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListView_AddSubDetails()
    {
        try
        {

            DataSet ds = objISC.GetAllSubjectwiseMarksList("", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlIndexScoreConfigList.Visible = true;
                lvIndexScoreConfigList.DataSource = ds;
                lvIndexScoreConfigList.DataBind();

            }
            else
            {

                pnlIndexScoreConfigList.Visible = false;
                lvIndexScoreConfigList.DataSource = null;
                lvIndexScoreConfigList.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IndexScoreConfiguration.BindListView_AddSubDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Edit Button
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            DataSet ds;

            string[] commandArgs = btnEdit.CommandArgument.ToString().Split(new char[] { ',' });
            string AdmBatchno = commandArgs[0];
            string DegreeNo = commandArgs[1];
            string Branchno = commandArgs[2];
            string ConfigurationType = Convert.ToString(commandArgs[3]).Equals("Index Score") ? "I" : "T";
            string AdvantageType = Convert.ToString(commandArgs[4]).Equals("Optional Subject") ? "O" : "W";
            string MaxMark = commandArgs[5];

            ViewState["action"] = "edit";

            ////ddlAdmBatch.Enabled = false;
            ////ddlDegree.Enabled = false;
            ////ddlBranch.Enabled = false;
            ////rdoConfigType.Enabled = false;
            ////rdoAdvType.Enabled = false;

            ddlAdmBatch.SelectedValue = AdmBatchno;
            ddlDegree.SelectedValue = DegreeNo;
            ddlBranchList();
            ddlBranch.SelectedValue = Branchno;

            if (ConfigurationType.Equals("I"))
            {
                rdoConfigType.SelectedValue = "I";
            }
            else
            {
                rdoConfigType.SelectedValue = "T";
            }
            if (AdvantageType.Equals("O"))
            {
                rdoAdvType.SelectedValue = "O";
            }
            else
            {
                rdoAdvType.SelectedValue = "W";
            }
            txtMaxMarks.Text = MaxMark;

            commonBindExistAndEditData(Convert.ToInt32(AdmBatchno), Convert.ToInt32(DegreeNo), Convert.ToInt32(Branchno), ConfigurationType.ToString(), AdvantageType.ToString());

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IndexScoreConfiguration.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void commonBindExistAndEditData(int AdmBatchno, int DegreeNo, int Branchno, string ConfigurationType, string AdvantageType)
    {
        try
        {
            if (AdmBatchno > 0 && DegreeNo > 0 && Branchno > 0 && ConfigurationType != string.Empty && AdvantageType != string.Empty)
            {
                DataSet ds;

                ds = objISC.GetSingleSubjectwiseMarksInformation(AdmBatchno, DegreeNo, Branchno, ConfigurationType, AdvantageType);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    BindListView_SubjectMarksDetails(ds.Tables[0]);
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IndexScoreConfiguration.commonBindExistAndEditData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region DataSet To XML String Methods
    DataTable CreateDatatable_SubMarks()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "ACD_SUBJECT_OPTIONAL_WEIGHTAGE_MARK";


            dt.Columns.Add("ADMBATCHNO");
            dt.Columns.Add("DEGREENO");
            dt.Columns.Add("BRANCHNO");
            dt.Columns.Add("CONFIGURATION_TYPE");
            dt.Columns.Add("SUBJECTNO");
            dt.Columns.Add("MARKS");
            dt.Columns.Add("MAX_MARKS");
            dt.Columns.Add("ADVANTAGE_TYPE");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IndexScoreConfig.CreateDatatable_SubMarks() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable Add_Datatable_SubMarks()
    {
        DataTable dt = CreateDatatable_SubMarks();
        try
        {
           
            foreach (var item in lvSubjectMarks.Items)
            {
                DataRow dRow = dt.NewRow();

                HiddenField hidden1 = item.FindControl("hfdMarksSubNo") as HiddenField;
                TextBox box1 = item.FindControl("txtMarks") as TextBox;
                
                string marks = box1.Text.ToString().Trim();
                if (marks.Length == 0)
                {
                    continue;
                }
                else if ((int)Convert.ToDouble(marks) <= 0)
                {
                    continue;
                }
                else
                {
                    dRow["ADMBATCHNO"] = ddlAdmBatch.SelectedValue;
                    dRow["DEGREENO"] = ddlDegree.SelectedValue;
                    dRow["BRANCHNO"] = ddlBranch.SelectedValue;
                    dRow["CONFIGURATION_TYPE"] = rdoConfigType.SelectedValue.ToString().Equals("I") ? "I" : "T";
                    dRow["SUBJECTNO"] = hidden1.Value; ;
                    dRow["MARKS"] = box1.Text.Trim();
                    dRow["MAX_MARKS"] = txtMaxMarks.Text.Trim();
                    dRow["ADVANTAGE_TYPE"] = rdoAdvType.SelectedValue.ToString().Equals("O") ? "O" : "W";

                    
                    dt.Rows.Add(dRow);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IndexScoreConfig.Add_Datatable_SubMarks() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private string validateIndexScoreData()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvSubjectMarks.Items)
            {
                TextBox box2 = (TextBox)lvSubjectMarks.Items[rowIndex].FindControl("txtMarks");
                //TextBox box1 = item.FindControl("txtMarks") as TextBox;

                if (!(float.Parse(box2.Text.Trim()) <= float.Parse(txtMaxMarks.Text.Trim())))
                {
                    _validate = "Please Enter \" Subject Mark\" must be less than or equals to \"Max Marks\"";
                    box2.Text = "0.00";
                    return _validate;
                }
                rowIndex += 1;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "IndexScoreConfig.validateIndexScoreData() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }

    #endregion

    #region Button Submit and Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ClearAddSubMaxMarks();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            string _validateIndexScoreData = validateIndexScoreData();
            if (_validateIndexScoreData != string.Empty)
            {
                objCommon.DisplayMessage(_validateIndexScoreData, this.Page);
                return;
            }

            DataTable dt = Add_Datatable_SubMarks();
            DataSet ds = new DataSet();
            ds.DataSetName = "ACD_SUBJECT_OPTIONAL_WEIGHTAGE_MARKS";
            ds.Tables.Add(dt);
            if (dt.Rows.Count > 0)
            {

                int AdmBatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                int BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                string ConfigType = rdoConfigType.SelectedValue.ToString().Equals("I") ? "I" : "T";
                string AdvantageType = rdoAdvType.SelectedValue.ToString().Equals("O") ? "O" : "W";
                float MaxMarks = float.Parse(txtMaxMarks.Text.Trim());

                int ret = 0;

                string displaymsg = "";

                if (ViewState["action"].ToString().Equals("add"))
                {
                    ret = Convert.ToInt32(objISC.InsertSubjectwiseMarks(AdmBatchNo, DegreeNo, BranchNo, ConfigType, ds.GetXml(), AdvantageType, MaxMarks));
                    displaymsg = "Record added successfully.";
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    ret = Convert.ToInt32(objISC.UpdateSubjectwiseMarksSubjects(AdmBatchNo, DegreeNo, BranchNo, ConfigType, ds.GetXml(), AdvantageType, MaxMarks));
                    displaymsg = "Record updated successfully.";
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }

                if (ret == 2)
                {
                    displaymsg = "Record alreday exist.";
                }
                else if (ret > 0)
                {
                    ClearAddSubMaxMarks();
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }
                objCommon.DisplayMessage(displaymsg, this.Page);

            }
            
        }
        else
            Response.Redirect("~/default.aspx");
    }
    #endregion

    private void ClearAddSubMaxMarks()
    {
        ViewState["action"] = "add";

        ddlAdmBatch.SelectedIndex = 0;

        ddlDegree.SelectedIndex = 0;

        ddlBranch.Items.Clear();
        ddlBranch.Items.Insert(0, "Please Select");
        ddlBranch.SelectedIndex = 0;

        rdoConfigType.SelectedValue = "I";
        rdoAdvType.SelectedValue = "O";

        txtMaxMarks.Text = string.Empty;

        pnlSubjectMarks.Visible = false;
        lvSubjectMarks.DataSource = null;
        lvSubjectMarks.DataBind();

        BindListView_AddSubDetails();
    }
}