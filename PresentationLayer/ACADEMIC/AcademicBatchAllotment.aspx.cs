using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_AcademicBatchAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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

                // CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
                AcdYearDropDownList();
            }
            ViewState["action"] = "add";

        }
    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
    //    }
    //}

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlAcdBatch, "ACD_ACADEMICBATCH WITH (NOLOCK)", "ACADEMICBATCHNO", "ACADEMICBATCH", "ACADEMICBATCHNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "ACADEMICBATCHNO DESC");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND ISNULL(ACTIVESTATUS,0)=1", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SEMESTERNO ASC");
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE WITH (NOLOCK)", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "IDTYPENO ASC");

        }
        catch
        {
            throw;
        }
    }
    private void MultipleSelectDropDown()
    {
        try
        {

            objCommon.FillListBox(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID ="+ ddlCollege.SelectedValue +" AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1", "A.LONGNAME");

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE C ON(D.DEGREENO=C.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "D.DEGREENO");

                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "DEGREENO DESC");

                ddlDegree.Focus();
            }
            else
            {
                ddlDegree.Items.Clear();
                ddlAdmBatch.SelectedIndex = 0;
            }
            lvAcdBatchAllotment.Visible = false;
            lvAcdBatchAllotment.DataSource = null;
            lvAcdBatchAllotment.DataBind();
            ddlBranch.Items.Clear();
            
            ddlSemester.SelectedIndex = 0;
            ddlAdmType.SelectedIndex = 0;
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultipleSelectDropDown();
        lvAcdBatchAllotment.Visible = false;
        lvAcdBatchAllotment.DataSource = null;
        lvAcdBatchAllotment.DataBind();
    }

    private void BindListView()
    {
        try
        {
            int ADMBATCH = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            int DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            int COLLEGE_ID = Convert.ToInt32(ddlCollege.SelectedValue);
            int SEM= Convert.ToInt32(ddlSemester.SelectedValue);
            int IDTYPE= Convert.ToInt32(ddlAdmType.SelectedValue);
            string branch = GetBranch();
            branch = branch.Replace('$', ',');
            ViewState["BranchNo"] = branch;
            string[] Branchno = branch.Split(',');

            DataSet ds = objSC.GetAllAcademicBatchAllotment(ADMBATCH, DEGREENO, branch.ToString(), COLLEGE_ID, SEM, IDTYPE);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAcdBatchAllotment.Visible = true;
                lvAcdBatchAllotment.DataSource = ds;
                lvAcdBatchAllotment.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAcdBatchAllotment);//Set label 

            }
            else
            {
                pnlStudents.Visible = true;
                lvAcdBatchAllotment.DataSource = null;
                lvAcdBatchAllotment.DataBind();
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAcdBatchAllotment);//Set label 

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AcademicBatchAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private string GetBranch()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;
        //  degreeNo = hdndegreeno.Value;
        //pnlFeeTable.Update();
        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }
        if (X == 0)
        {
            branchNo = "0";
        }
        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');

        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return branchno;

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        
        BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void Clear()
    {
        ddlBranch.Items.Clear();
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
      //  lvAcdBatchAllotment.DataSource = null;
       // lvAcdBatchAllotment.DataBind();
        ddlAcdBatch.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ID = string.Empty;
        int count = 0;
        int AcdBatchno;
        int degreeNo;
        foreach (ListViewDataItem dataitem in lvAcdBatchAllotment.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkRegister") as CheckBox;     
            if (cbRow.Checked == true) 
            {
                ID += cbRow.ToolTip + ",";
                count++;
            } 
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this, "Please Select atleast one Student for Academic Batch Allotment", this);
            return;
        }
        else
        {
            
            ViewState["action"] = "edit";
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
            AcdBatchno = Convert.ToInt32(ddlAcdBatch.SelectedValue);
            degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);
            string IP_ADDRESS = Session["ipAddress"].ToString();

            CustomStatus cs = (CustomStatus)objSC.UpdateAcdemicBatchAllotment(ID, AcdBatchno, degreeNo, userno, IP_ADDRESS);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                
                BindListView();
                Clear();
                ddlAdmBatch.SelectedIndex = 0;
                objCommon.DisplayMessage(this, "Students Alloted Academic Batch Successfully Submit", this.Page);
                ViewState["action"] = null;
            }

            else
            {
                objCommon.DisplayMessage(this, "Record Not Register", this.Page);  
                BindListView();
                Clear();
            }
                  
                }
            }
        }


    private void AcdYearDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatchAcdYear, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR WITH (NOLOCK)", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID > 0 AND ISNULL(ACTIVE_STATUS,0)=1", "ACADEMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND ISNULL(ActiveStatus,0)=1", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlAcdYearSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SEMESTERNO ASC");
        }
        catch
        {
            throw;
        }
    }
    private void AcdYearMultipleDropDown()
    {
        try
        {

            objCommon.FillListBox(lstbBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID="+ddlSchool.SelectedValue+" AND B.DEGREENO = " + ddlDegreeAcdYear.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1", "A.LONGNAME");

        }
        catch (Exception)
        {

            throw;
        }
    }
  
    protected void ddlDegreeAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AcdYearMultipleDropDown();
            lvAcdYearAllotment.Visible = false;
            lvAcdYearAllotment.DataSource = null;
            lvAcdYearAllotment.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatchAcdYear.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegreeAcdYear, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE C ON(D.DEGREENO=C.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlSchool.SelectedValue + "", "D.DEGREENO");

               // objCommon.FillDropDownList(ddlDegreeAcdYear, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "DEGREENO DESC");

                ddlDegreeAcdYear.Focus();
            }
            else
            {
                ddlDegreeAcdYear.Items.Clear();
                ddlSchool.SelectedIndex = 0;
            }
            lstbBranch.Items.Clear();
            ddlAcdYearSemester.SelectedIndex = 0;
            lvAcdYearAllotment.Visible = false;
            lvAcdYearAllotment.DataSource = null;
            lvAcdYearAllotment.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void AcdYearBindListView()
    {
        try
        {
            int ADMBATCH = Convert.ToInt32(ddlAdmBatchAcdYear.SelectedValue);
            int DEGREENO = Convert.ToInt32(ddlDegreeAcdYear.SelectedValue);
            int COLLEGE_ID = Convert.ToInt32(ddlSchool.SelectedValue);
            int SEM = Convert.ToInt32(ddlAcdYearSemester.SelectedValue);
            string branch = GetBranchAcdYear();
            branch = branch.Replace('$', ',');
            ViewState["BranchNo"] = branch;
            string[] Branchno = branch.Split(',');

            DataSet ds = objSC.GetAllAcademicYearAllotment(ADMBATCH, DEGREENO, branch.ToString(), COLLEGE_ID, SEM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAcdYearAllotment.Visible = true;
                lvAcdYearAllotment.DataSource = ds;
                lvAcdYearAllotment.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAcdYearAllotment);//Set label 

            }
            else
            {
                PnlvAcdYear.Visible = true;
                lvAcdYearAllotment.DataSource = null;
                lvAcdYearAllotment.DataBind();
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAcdYearAllotment);//Set label 

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AcademicBatchAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private string GetBranchAcdYear()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;
      
        foreach (ListItem item in lstbBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }
        if (X == 0)
        {
            branchNo = "0";
        }
        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');

        }
       
        return branchno;

    }
    private void AcdYearClear()
    {

        ddlAdmBatchAcdYear.SelectedIndex = 0;
        ddlDegreeAcdYear.SelectedIndex = 0;
        //lvAcdYearAllotment.Visible = false;
       //lvAcdYearAllotment.DataSource = null;
       //lvAcdYearAllotment.DataBind();
       
        lstbBranch.Items.Clear();
        ddlSchool.SelectedIndex = 0;
        ddlAcdYearSemester.SelectedIndex = 0;
        ddlAcdYear.SelectedIndex = 0;
    }
    protected void btnShowAcdYear_Click(object sender, EventArgs e)
    {
        try
        {
            AcdYearBindListView();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnCancelAcdYear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmitAcdYear_Click(object sender, EventArgs e)
    {
        string ID = string.Empty;
        int count = 0;
        int AcdYearno;
        int degreeNo;
        foreach (ListViewDataItem dataitem in lvAcdYearAllotment.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkRegisterAcdYear") as CheckBox;
            if (cbRow.Checked == true)
            {
                ID += cbRow.ToolTip + ",";
                count++;
            }
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this, "Please Select atleast one Student for Academic Year Allotment", this);
            return;
        }
        else
        {

            ViewState["action"] = "AcdYearedit";
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("AcdYearedit"))
            {
                AcdYearno = Convert.ToInt32(ddlAcdYear.SelectedValue);
                degreeNo = Convert.ToInt32(ddlDegreeAcdYear.SelectedValue);
                int userno = Convert.ToInt32(Session["userno"]);
                string IP_ADDRESS = Session["ipAddress"].ToString();
                CustomStatus cs = (CustomStatus)objSC.UpdateAcdemicYearAllotment(ID, AcdYearno, degreeNo, userno, IP_ADDRESS);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    AcdYearBindListView();
                    AcdYearClear();
                    ddlAcdYear.SelectedIndex = 0;
                    objCommon.DisplayMessage(this, "Students Alloted Academic Year Successfully Submit", this.Page);
                    ViewState["action"] = null;
                }

                else
                {
                    objCommon.DisplayMessage(this, "Record Not Register", this.Page);
                    AcdYearBindListView();
                    AcdYearClear();

                }

            }
        }
    }


    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAcdBatchAllotment.Visible = false;
        lvAcdBatchAllotment.DataSource = null;
        lvAcdBatchAllotment.DataBind();
        ddlBranch.Items.Clear();
        ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
    }
    protected void ddlAdmBatchAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstbBranch.Items.Clear();
        ddlAcdYearSemester.SelectedIndex = 0;
        lvAcdYearAllotment.Visible = false;
        lvAcdYearAllotment.DataSource = null;
        lvAcdYearAllotment.DataBind();
    }
    protected void ddlAdmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAcdBatchAllotment.Visible = false;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAcdBatchAllotment.Visible = false;
    }
    protected void ddlAcdYearSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAcdYearAllotment.Visible = false;
    }
}
