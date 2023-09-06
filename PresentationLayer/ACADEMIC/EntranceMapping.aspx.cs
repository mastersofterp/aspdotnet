using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_EntranceMapping : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    MappingController objmp = new MappingController();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            ViewState["action"] = "add";
        }
        
        
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EntranceMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EntranceMapping.aspx");
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(ddlProgrammeType.SelectedValue=="1")
        {
            if (ddlType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Score Type.", this.Page);
                return;
            }
            if (txtScore.Text.Equals(string.Empty))
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Minimum Score.", this.Page);
                return;
            }
        }
        else if (ddlProgrammeType.SelectedValue == "2")
        {
            if (ddlType.SelectedIndex != 0)
            {
                if (txtScore.Text.Equals(string.Empty))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Minimum Score.", this.Page);
                    return;
                }
            }
        }
        string Mark_Per=string.Empty;
        int degree = Convert.ToInt32(ddlDegree.SelectedValue);
        string branch = string.Empty;
        string score = string.Empty;
        double CGPA = 0; ;
        if(ddlType.SelectedValue=="1")
        {
            Mark_Per="M";
        }
        else if (ddlType.SelectedValue == "2")
        {
            Mark_Per = "P";
        }
        else
        {
            Mark_Per = "";
        }
        if (!txtScore.Text.Equals(string.Empty))
        {
             score = txtScore.Text.TrimEnd();
        }
        else
        {
            score = "00.00";
        }
        if (ddlBranch.SelectedIndex > 0)
        {
            branch = ddlBranch.SelectedValue;
        }
        else
        {
            branch = null;
        }
        if (!txtCGPA.Text.Equals(string.Empty))
        {
            CGPA = Convert.ToDouble(txtCGPA.Text.TrimEnd());
        }
        if (ViewState["action"].ToString() == "add")
        {
            int ck = objmp.AddEntrance(Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToDouble(score), Convert.ToInt32(ddlAdmType.SelectedValue), Convert.ToInt32(ddlProgrammeType.SelectedValue), Mark_Per.ToString(), Convert.ToInt32(ddlDegree.SelectedValue), CGPA, Convert.ToInt32(Session["OrgId"]));

            if (ck == 1)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Entrance Mapping Saved Sucessfully.", this.Page);
                ddlEntrance.SelectedIndex = 0;
                txtScore.Text = string.Empty;
                ddlType.SelectedIndex = 0;
                txtCGPA.Text = string.Empty;
                BindListView();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Entrance Mapping Already Exist.", this.Page);
                return;
            }
        }
        else if (ViewState["action"].ToString() == "Edit")
        {
            int ck = objmp.UpdateEntrance(Convert.ToInt32(ViewState["ENTR_DEGREE_NO"].ToString()), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToDouble(txtScore.Text.TrimEnd()), Convert.ToInt32(ddlAdmType.SelectedValue),Convert.ToInt32(ddlProgrammeType.SelectedValue),Mark_Per,Convert.ToInt32(degree),CGPA);
            
            if (ck == 2)
            {
                objCommon.DisplayMessage(this.updGradeEntry, "Entrance Mapping Updated Sucessfully.", this.Page);
                ddlEntrance.SelectedIndex = 0;
                txtScore.Text = string.Empty;
                ddlType.SelectedIndex = 0;
                txtCGPA.Text = string.Empty;
                BindListView();
                return;
            }
        }
    }

    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        ViewState["action"] = "delete";
        int srno = int.Parse(btnDel.CommandArgument);
        int output = objmp.deleteEntrance(srno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(updGradeEntry, " Information Deleted Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(updGradeEntry, " Information Is Not Deleted. ", this.Page);
        }
        // }
        
    }

    private void BindListView()    
    {
        try
        {
            int degree = 0;
            int admType= 0;
            int programmeType = 0;
            if (ddlAdmType.SelectedIndex > 0)
            {
                admType = Convert.ToInt32(ddlAdmType.SelectedValue);
            }
            else
            {
                admType = 0;
            }
          
            if (ddlProgrammeType.SelectedIndex > 0)
            {
                programmeType = Convert.ToInt32(ddlProgrammeType.SelectedValue);
            }
            else
            {
                programmeType = 0;
            }
           
            if (ddlDegree.SelectedIndex > 0)
            {
                degree = Convert.ToInt32(ddlDegree.SelectedValue);
            }
            else
            {
                degree = 0;
            }
          
            DataSet ds = objmp.GetQualifyList(Convert.ToInt32(admType),Convert.ToInt32(ddlProgrammeType.SelectedValue),degree);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
            }
            else
            {
                lvlist.DataSource = null;
                lvlist.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_degree.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {       
        ddlMappingType.SelectedIndex = 1;
        BindListView();
    }
    protected void ddlMappingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEntrance, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0", "QUALIFYNO");
        lvlist.DataSource = null;
        lvlist.DataBind();
    }
    protected void bntEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int ENTR_DEGREE_NO = Int16.Parse(btnEdit.CommandArgument);
        ViewState["ENTR_DEGREE_NO"] = ENTR_DEGREE_NO;
        ViewState["action"] = "Edit";
        DataSet ds = objCommon.FillDropDown("ACD_ENTRE_DEGREE", "*", "", "ENTR_DEGREE_NO=" + ENTR_DEGREE_NO, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlAdmType.SelectedValue == "1")
            {
                ddlAdmType.SelectedValue = "1";
            }
            else
            {
                ddlAdmType.SelectedValue = "2";
            }            
            objCommon.FillDropDownList(ddlEntrance, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 ", "QUALIFYNO");            
            ddlEntrance.SelectedValue = ds.Tables[0].Rows[0]["QUALIFYNO"].ToString();
            string mappingType=objCommon.LookUp("ACD_QUALEXM","QEXAMSTATUS","QUALIFYNO = "+Convert.ToInt32(ddlEntrance.SelectedValue));
            if (mappingType.TrimEnd().Equals("E"))
            {
                ddlMappingType.SelectedIndex = 1;
            }
            else if (mappingType.TrimEnd().Equals("Q"))
            {
                ddlMappingType.SelectedValue="Q";
            }
            ddlProgrammeType.SelectedValue = ds.Tables[0].Rows[0]["PROGRAMME_TYPE"].ToString();                           
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(D.DEGREENO=DB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "UGPGOT=" + Convert.ToInt32(ddlProgrammeType.SelectedValue), "DEGREENAME");

            ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();               
           
            txtScore.Text = ds.Tables[0].Rows[0]["MIN_SCORE"].ToString();
            if (ds.Tables[0].Rows[0]["MARKS_PER"].ToString().Equals("M"))
            {
                ddlType.SelectedValue = "1";
            }
            else if (ds.Tables[0].Rows[0]["MARKS_PER"].ToString().Equals("P"))
            {
                ddlType.SelectedValue = "2";
            }
            string branch= ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            if (!branch.Equals(string.Empty))
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO=B.BRANCHNO)", "DISTINCT CDB.BRANCHNO", "B.LONGNAME", "BRANCH_CODE= '" + ddlCode.SelectedValue + "'", "B.LONGNAME");

                ddlBranch.SelectedValue = branch;
                divBranch.Visible = true;
            }
            else
            {
                divBranch.Visible = false;
            }
            string CGPA = ds.Tables[0].Rows[0]["CGPA"].ToString();
            if (ddlProgrammeType.SelectedValue == "2")
            {

                txtCGPA.Text = CGPA;
                divCGPA.Visible = true;
            }
            else
            {
                txtCGPA.Text = string.Empty;
                divCGPA.Visible = false;
            }
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void ddlAdmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EntranceMapping.ddlAdmType_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCode.SelectedIndex > 0)
            {
                ddlMappingType.SelectedIndex = 1;
                objCommon.FillDropDownList(ddlEntrance, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS = 'Q' AND PROGRAMME_TYPE=" + Convert.ToInt32(ddlProgrammeType.SelectedValue), "QUALIFYNO");

                if (ddlCode.SelectedValue == "U06" || ddlCode.SelectedValue == "U07")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO=B.BRANCHNO)", "DISTINCT CDB.BRANCHNO", "B.LONGNAME", "BRANCH_CODE= '" + ddlCode.SelectedValue + "'", "B.LONGNAME");
                    divBranch.Visible = true;
                }
                else
                {
                    divBranch.Visible = false;
                }
                if (ddlProgrammeType.SelectedValue == "2" && ddlCode.SelectedValue == "P06")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO=B.BRANCHNO)", "DISTINCT CDB.BRANCHNO", "B.LONGNAME", "BRANCH_CODE= '" + ddlCode.SelectedValue + "'", "B.LONGNAME");
                    divBranch.Visible = true;
                }
                else
                {                    
                }
            }
            else
            {
                divBranch.Visible = false;
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EntranceMapping.ddlCode_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlProgrammeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlProgrammeType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(D.DEGREENO=DB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "UGPGOT="+Convert.ToInt32(ddlProgrammeType.SelectedValue), "DEGREENAME");
                ddlDegree.Focus();
                divBranch.Visible = false;
            }
            if (ddlProgrammeType.SelectedValue.Equals("2"))
            {
                divCGPA.Visible = true;
            }
            else
            {
                divCGPA.Visible = false;
            }
            BindListView();
            
        }
        catch (Exception ex)
        {
                if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EntranceMapping.ddlProgrammeType_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
    protected void ddlDegree_SelectedIndexChanged1(object sender, EventArgs e)
    {
    }
}