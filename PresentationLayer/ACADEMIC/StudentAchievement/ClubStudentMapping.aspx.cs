using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
//using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using System.IO;
using BusinessLogicLayer.BusinessLogic.Academic;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;
public partial class ACADEMIC_StudentAchievement_ClubStudentMapping : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ClubStudentMappingController objCSM = new ClubStudentMappingController();
    CompaireClubDataEntity objCCDE = new CompaireClubDataEntity();
    List<CompaireClubDataEntity> lstCCDE = new List<CompaireClubDataEntity>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            ViewState["OldCheckedList"] = null;

            PopulateDropDownList();
            BindClub();

            //BindListView();

            //    Prizes.Visible = false;

            //    //txtWinner.Visible = false;
            //    //txtRunnerUp.Visible = false;
            //    //txtThirdPlace.Visible = false; 
        }
    }
    #region Common Methods For All
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
            objCommon.ShowError(Page, "Masters.BindALLDDL() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    public void BindALLLST(ref ListBox lst, DataSet ds, string textField, string valueField)
    {
        try
        {
            lst.Items.Clear();
            lst.DataSource = ds;
            lst.DataValueField = ds.Tables[0].Columns[valueField].ToString();
            lst.DataTextField = ds.Tables[0].Columns[textField].ToString();
            lst.DataBind();
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindALLLST() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Bind All DropDown List
    private void PopulateDropDownList()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER WITH (NOLOCK) ", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID<>0", "COLLEGE_ID");
            DataSet ds = objCommon.FillDropDown("VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID<>0", "COLLEGE_ID");
            BindALLDDL(ref ddlCollege, ds, "COLLEGE_NAME", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstDegree.Items.Clear();
            lstBranch.Items.Clear();
            //lstDegree.Items.Insert(0, "Please Select");

            if (ddlCollege.SelectedIndex > 0)
            {
                BindDegree();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ClubStudentMapping.ddlCollege_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindDegree()
    {
        try
        {

            //DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");//ddlCollege.SelectedValue
            DataSet ds = objCommon.FillDropDown("VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "DEGREENAME", "COLLEGE_ID=" + ddlCollege.SelectedValue, "DEGREENO");
            BindALLLST(ref lstDegree, ds, "DEGREENAME", "DEGREENO");
            //lstDegree.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void lstDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstBranch.Items.Clear();
            //lstBranch.Items.Insert(0, "Please Select");

            if (lstDegree.Items.Count > 0)
            {
                BindBranch();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ClubStudentMapping.lstDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindBranch()
    {
        try
        {
            string Degree = string.Empty;
            foreach (ListItem item in lstDegree.Items)
            {
                if (item.Selected)
                {
                    Degree += item.Value + ",";
                }
            }
            Degree = Degree.Substring(0,(Degree.Length - 1));

            //DataSet ds = objCommon.FillDropDown("ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + lstDegree.SelectedValue, "A.LONGNAME");
            DataSet ds = objCommon.FillDropDown("VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "LONGNAME", "DEGREENO IN(" + Degree + ")", "BRANCHNO");
            BindALLLST(ref lstBranch, ds, "LONGNAME", "BRANCHNO");
            
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void BindClub()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO <>0", "CLUB_ACTIVITY_NO");
            BindALLDDL(ref ddlClub, ds, "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO");
            ddlClub.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    #endregion

    private void BindListView(int CollegeId, string DegreeNo, string BranchNo, int ClubActivityNo)
    {
        try
        {
            ViewState["action"] = "add";
            pnlClubMapping.Visible = false;
            lvClubMapping.Visible = false;
            lvClubMapping.DataSource = null;
            lvClubMapping.DataBind();

            DataSet ds = objCSM.GetAllClubStudentMappingData(CollegeId, DegreeNo, BranchNo, ClubActivityNo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ViewState["action"] = "edit";
                pnlClubMapping.Visible = true;
                lvClubMapping.Visible = true;
                lvClubMapping.DataSource = ds;
                lvClubMapping.DataBind();

            }
            else
            {
                ViewState["action"] = "add";
                pnlClubMapping.Visible = false;
                lvClubMapping.Visible = false;
                lvClubMapping.DataSource = null;
                lvClubMapping.DataBind();

            }
            if (lvClubMapping.Items.Count > 0)
            {
                ViewState["OldCheckedList"] = null;

                int i=0;
                foreach (ListViewDataItem lv in lvClubMapping.Items)
                {
                    Label lblClubActivityType = lv.FindControl("lblClubActivityType") as Label;
                    HiddenField hfdClubActivityNo = lv.FindControl("hfdClubActivityNo") as HiddenField;
                    CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                    HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;

                    if (hfdClubActivityNo.Value != null)
                    {
                        if (lblClubActivityType.Text.Trim().ToString().Length > 1 && hfdClubActivityNo.Value.ToString().Equals(ddlClub.SelectedValue))
                        {
                            CompaireClubDataEntity objCCDEE = new CompaireClubDataEntity();
                            objCCDEE.Idno = Convert.ToInt32(hfdIdNo.Value);
                            objCCDEE.Clubactivityno = Convert.ToInt32(hfdClubActivityNo.Value);

                            lstCCDE.Insert(i, objCCDEE);
                            //lstCCDE.Add(i,objCCDE);

                            chkIsActive.Checked = true;
                            i++;
                        }
                    }
                }
                if (lstCCDE.Count > 0)
                {
                    ViewState["OldCheckedList"] = lstCCDE;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Buttons Show, Submit, Export Report & Cancel
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                string Degree = string.Empty;
                string Branch = string.Empty;
                foreach (ListItem item in lstDegree.Items)
                {
                    if (item.Selected)
                    {
                        Degree += item.Value + ",";
                    }
                }
                Degree = Degree.Substring(0, (Degree.Length - 1));
                
                foreach (ListItem item in lstBranch.Items)
                {
                    if (item.Selected)
                    {
                        Branch += item.Value + ",";
                    }
                }
                Branch = Branch.Substring(0, (Branch.Length - 1));

                BindListView(Convert.ToInt32(ddlCollege.SelectedValue), Degree, Branch, Convert.ToInt32(ddlClub.SelectedValue.ToString().Equals("Please Select") ? "0" : ddlClub.SelectedValue));
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();

    }
    
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            string displaymsg = "";
            foreach (ListViewDataItem lv in lvClubMapping.Items)
            {
                CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;

                HiddenField hfdClubActivityNo = lv.FindControl("hfdClubActivityNo") as HiddenField;
                HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;

                if (ViewState["OldCheckedList"] != null)
                {
                    lstCCDE = (List<CompaireClubDataEntity>)ViewState["OldCheckedList"];
                    if (lstCCDE != null && lstCCDE.Count > 0)
                    {
                        foreach (CompaireClubDataEntity objCCDE in lstCCDE)
                        {
                            if (objCCDE.Idno == Convert.ToInt32(hfdIdNo.Value) && objCCDE.Clubactivityno == Convert.ToInt32(hfdClubActivityNo.Value) && !chkIsActive.Checked)
                            {
                                //delete from table unselected(Old) Club Mapping for Student
                                DeleteStudMap(Convert.ToInt32(hfdIdNo.Value), Convert.ToInt32(hfdClubActivityNo.Value));
                            }

                        }
                    }

                }


                if (chkIsActive.Checked)
                {
                    //HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;
                    SaveStudMap(Convert.ToInt32(hfdIdNo.Value));
                }

            }
            
            displaymsg = "Record added successfully.";
            objCommon.DisplayMessage(this, displaymsg, this.Page);

            ClearData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void SaveStudMap(int studIdNo)
    {
        try
        {
            string ret = "";
            //displaymsg = "Record added successfully.";
            ret = objCSM.InsertClubStudentMapping(studIdNo, Convert.ToInt32(ddlClub.SelectedValue));

            //objCommon.DisplayMessage(displaymsg, this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.SaveStudMap --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void DeleteStudMap(int studIdNo, int ClubActivityNo)
    {
        try
        {
            int ret = 0;
            ret = objCSM.DeleteClubStudentMapping(studIdNo, ClubActivityNo);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.DeleteStudMap --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnReport_Click(object sender, System.EventArgs e)
    {
        DataGrid Gr = new DataGrid();
        DataSet ds = new DataSet();
        ds = objCSM.ClubStudentMappingReport();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=ClubStudentMapping.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

        }
    }
    #endregion

    private void ClearData()
    {


        ddlCollege.SelectedIndex = 0;

        lstBranch.ClearSelection();
        lstDegree.ClearSelection();

        ddlClub.SelectedIndex = 0;

        ViewState["action"] = "add";
        pnlClubMapping.Visible = false;
        lvClubMapping.DataSource = null;
        lvClubMapping.DataBind();

        ViewState["OldCheckedList"] = null;

    }



}
// Class used in camparision old data and new data in dynamic selection criteria
[Serializable]
public class CompaireClubDataEntity
{
    private int idno = 0;
    private int clubactivityno = 0;

    public int Idno
    {
        get { return idno; }
        set { idno = value; }
    }
    public int Clubactivityno
    {
        get { return clubactivityno; }
        set { clubactivityno = value; }
    }
}



