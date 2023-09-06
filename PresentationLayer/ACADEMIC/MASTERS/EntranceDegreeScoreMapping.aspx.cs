using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessLogic.Academic;
using BusinessLogicLayer.BusinessEntities.Academic;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_MASTERS_EntranceDegreeScoreMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAllDDL();
            BindCheckboxList();
            BindListView();
            ViewState["action"] = "add";
        }
    }

    void BindAllDDL()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        objCommon.FillDropDownList(ddlExamType, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0", "QUALIFYNO");
        //objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORYNO");
    }

    private void BindListView()
    {
        try
        {
            ScoreMappingController objDegMap = new ScoreMappingController();
            DataSet ds = objDegMap.GetAllMappedData();
            lvMappingList.DataSource = ds;
            lvMappingList.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_EntranceDegreeScoreMapping.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    void BindCheckboxList()
    {
        DataSet ds = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0", "CATEGORY");

        if (ds != null || ds.Tables[0].Rows.Count != 0)
        {
            chklstCategory.DataSource = ds;
            chklstCategory.DataValueField = "CATEGORYNO";
            chklstCategory.DataTextField = "CATEGORY";
            chklstCategory.DataBind();
        }
    }

    void ClearAllCheckBox()
    {
        foreach (ListItem item in chklstCategory.Items)
        {
            item.Selected = false;
        }
    }

    void Clear()
    {
        //ddlCategory.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        txtMinScore.Text = "";

        ClearAllCheckBox();
      
        chklstCategory.SelectedIndex = -1;
        ViewState["DEGREENO"] = null;
        ViewState["EXAMTYPE"] = null;
        ViewState["CATEGORYNO"] = null;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ScoreMappingController scoreMappingController = new ScoreMappingController();
            EntranceScoreMapping entranceMapppng = new EntranceScoreMapping();
            int Selected = 0;
            for (int i = 0; i < chklstCategory.Items.Count; i++)
            {
                if (chklstCategory.Items[i].Selected)
                {
                    Selected++;
                    break;
                }
            }

            if (ddlExamType.SelectedIndex > 0 && ddlExamType.SelectedIndex > 0 && txtMinScore.Text != "" && Selected > 0)
            {
                entranceMapppng.DegreeNo = Convert.ToInt32(ddlDegree.SelectedItem.Value);
                entranceMapppng.ExamNo = Convert.ToInt32(ddlExamType.SelectedItem.Value);
                //entranceMapppng.CategoryNo = Convert.ToInt32(ddlCategory.SelectedItem.Value);
                entranceMapppng.Score = Convert.ToDouble(txtMinScore.Text);

                //if (ViewState["action"] == "add")
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("add"))
                {
                    DataTable dt = new DataTable();
                    DataColumn dc = new DataColumn("DEGREENO", typeof(int));
                    dt.Columns.Add(dc);
                    dc = new DataColumn("EXAMTYPE", typeof(int));
                    dt.Columns.Add(dc);
                    dc = new DataColumn("CATEGORYNO", typeof(int));
                    dt.Columns.Add(dc);
                    dc = new DataColumn("MIN_SCORE", typeof(double));
                    dt.Columns.Add(dc);
                    for (int i = 0; i < chklstCategory.Items.Count; i++)
                    {
                        if (chklstCategory.Items[i].Selected)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = entranceMapppng.DegreeNo;
                            dr[1] = entranceMapppng.ExamNo;
                            dr[2] = chklstCategory.Items[i].Value;
                            dr[3] = entranceMapppng.Score;
                            dt.Rows.Add(dr);
                        }
                    }
                    CustomStatus cs = (CustomStatus)scoreMappingController.AddDegreeScoreMapping(entranceMapppng, dt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updMappedData, "Mapping Done Successfully!", this.Page);
                        Clear();
                        BindListView();
                        //LoadActivities();
                        //PopulateActivity();
                    }
                    else
                        objCommon.DisplayMessage(this.updMappedData, "Error Adding Activity!", this.Page);
                }
                //else if (ViewState["action"].ToString() == "edit")
                else if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    entranceMapppng.CategoryNo = Convert.ToInt32(chklstCategory.SelectedItem.Value);
                    //int SelectResult = CheckForSingleSelection();
                    //if (SelectResult > 1)
                    //{
                    //    objCommon.DisplayMessage(this.updMappedData, "Please select only one category while updating", this.Page);
                    //    return;
                    //}
                    if (IsDuplicateEntry(entranceMapppng))
                    {
                        objCommon.DisplayMessage(this.updMappedData, "Record Already Exists", this.Page);
                        return;
                    }
                    entranceMapppng.Ent_NO = (Int32)ViewState["ENT_NO"];
                    CustomStatus cs = (CustomStatus)scoreMappingController.UpdateDegreeScoreMapping(entranceMapppng);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updMappedData, "Mapping Updated Successfully!", this.Page);
                        Clear();
                        ViewState["ENT_NO"] = null;
                        btnSubmit.Text = "submit";
                        ViewState["action"] = "add";
                        BindListView();

                        ViewState["DEGREENO"] = null;
                        ViewState["EXAMTYPE"] = null;
                        ViewState["CATEGORYNO"] = null;
                        //LoadActivities();
                        //PopulateActivity();
                    }
                    else
                        objCommon.DisplayMessage(this.updMappedData, "Error Updating Activity!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updMappedData, "Please Select Atleast One Category", this.Page);
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_EntranceDegreeScoreMapping.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ENT_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["ENT_NO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
            this.ShowDetails(ENT_NO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_EntranceDegreeScoreMapping.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetails(int ENT_NO)
    {
        try
        {
            ScoreMappingController objDM = new ScoreMappingController();
            SqlDataReader dr = objDM.GetSingleDegreeMappedRecord(ENT_NO);
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlDegree.SelectedValue = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                    ddlExamType.SelectedValue = dr["EXAMTYPE"] == null ? string.Empty : dr["EXAMTYPE"].ToString();
                    txtMinScore.Text = dr["MIN_SCORE"] == null ? string.Empty : dr["MIN_SCORE"].ToString();
                    chklstCategory.SelectedValue = dr["CATEGORYNO"] == null ? string.Empty : dr["CATEGORYNO"].ToString();
                    ViewState["ENT_NO"] = dr["ENT_NO"];

                    //////Set Last updating Records Details
                    ViewState["DEGREENO"] = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
                    ViewState["EXAMTYPE"] = dr["EXAMTYPE"] == null ? string.Empty : dr["EXAMTYPE"].ToString();
                    ViewState["CATEGORYNO"] = dr["CATEGORYNO"] == null ? string.Empty : dr["CATEGORYNO"].ToString();
                    //////Set Last updating Records Details End
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_EntranceDegreeScoreMapping.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = "add";
        btnSubmit.Text = "Submit";
    }

    int CheckForSingleSelection()
    {
        int Selected = 0;
        for (int i = 0; i < chklstCategory.Items.Count; i++)
        {
            if (chklstCategory.Items[i].Selected)
            {
                Selected++;
            }
        }
        return Selected;
    }

    bool IsDuplicateEntry(EntranceScoreMapping entranceMapppng)   //checking duplicate entry for degree , exam and category
    {
        bool IsDuplicateEntry = false;
        bool IsAllowed = false;
        string res = objCommon.LookUp("ACD_ENTRANCE_EXAM_MAPPING", "Count(*)", "DEGREENO = " + entranceMapppng.DegreeNo + " AND EXAMTYPE = " + entranceMapppng.ExamNo + " AND CATEGORYNO = " + entranceMapppng.CategoryNo);

        if (entranceMapppng.DegreeNo == Convert.ToInt32(ViewState["DEGREENO"]) && entranceMapppng.ExamNo == Convert.ToInt32(ViewState["EXAMTYPE"])
            && entranceMapppng.CategoryNo == Convert.ToInt32(ViewState["CATEGORYNO"]))
        {
            IsAllowed = true;
        }

        if (Convert.ToInt32(res) > 0 && !IsAllowed)
        {
            IsDuplicateEntry = true;
        }
        return IsDuplicateEntry;
    }

    protected void chklstCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {                
            string result = Request.Form["__EVENTTARGET"];
            string[] checkedBox = result.Split('$'); 
            int index = int.Parse(checkedBox[checkedBox.Length - 1]);
            if (chklstCategory.Items[index].Selected)
            {                
                chklstCategory.SelectedValue = chklstCategory.Items[index].Value;
            }
        }
    }
}