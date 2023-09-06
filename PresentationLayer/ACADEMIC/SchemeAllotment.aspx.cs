using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_SchemeAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                else
                {
                    //lblHelp.Text = "No Help Added";
                }
                    //Populate the DropDownList 
                    PopulateDropDownList();
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO >0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "D.DEGREENO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlSType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBatchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlBatch.SelectedIndex = ddlBatchYear.SelectedIndex;

        //Populate Branch
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_BATCHNO", Convert.ToInt32(ddlBatchYear.SelectedValue));

            SqlDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN_SP_RET_BRANCH_BYBATCH", objParams);

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            while (dr.Read())
                ddlBranch.Items.Add(new ListItem(dr["LongName"].ToString(), dr["BranchNo"].ToString()));
            dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.ddlBatchYear_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Populate Scheme
        try
        {
            //if (ddlSType.SelectedIndex == 0)
                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue,"SCHEMENO");
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + ddlDegree.SelectedValue + " and BRANCHNO=" + ddlBranch.SelectedValue + "", "SCHEMENO");
            //else
            //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND SCHEMETYPE=" + ddlSType.SelectedValue, "SCHEMENO");

            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT A INNER JOIN ACD_SEMESTER B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "DEGREENO=" + ddlDegree.SelectedValue + " and BRANCHNO=" + ddlBranch.SelectedValue + "", "A.SEMESTERNO");

            lvStudents.DataSource = null;
            lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    private void BindListView()
    {
        hdfTot.Value = "0";
        txtTotStud.Text = "0";
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetStudentsBySchemeAllot(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue));
            //DataSet ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lblSch.Visible = true;
                ddlScheme.Enabled = true;
                lvStudents.Visible = true;
                lblStatus.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "No Students for selected criteria";
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lblSch.Visible = false;
                ddlScheme.Enabled = false;
                lvStudents.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schemeAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBatchYear.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        lblSch.Visible = false;
        ddlScheme.SelectedIndex = 0;
        ddlScheme.Enabled = false;
        lblStatus.Text = string.Empty;
        lblStatus2.Text = string.Empty;
    }

    //protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    //}

    //protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ////Populate Scheme
    //    //try
    //    //{
    //    //    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

    //    //    SqlParameter[] objParams = new SqlParameter[4];

    //    //    objParams[0] = new SqlParameter("@P_SEMNO", 0);
    //    //    objParams[1] = new SqlParameter("@P_BRANCHNO", Convert.ToInt32(ddlBranch.SelectedItem.Value));
    //    //    objParams[2] = new SqlParameter("@P_BATCHNO", 0);
    //    //    objParams[3] = new SqlParameter("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedItem.Value));

    //    //    DataTable dt = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_RET_SCHEME", objParams).Tables[0];

    //    //    ddlScheme.Enabled = true;
    //    //    ddlScheme.Items.Clear();
    //    //    ddlScheme.Items.Add(new ListItem("Please Select", "0"));

    //    //    ddlScheme.DataSource = dt;
    //    //    ddlScheme.DataTextField = dt.Columns["SName"].ToString();
    //    //    ddlScheme.DataValueField = dt.Columns["SchemeNo"].ToString();
    //    //    ddlScheme.DataBind();
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    if (Convert.ToBoolean(Session["error"]) == true)
    //    //        objUCommon.ShowError(Page, "Academic_schemeAllotment.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //    //    else
    //    //        objUCommon.ShowError(Page, "Server UnAvailable");
    //    //}
    //}

    protected void btnAssignSch_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student_Acd objStudent = new Student_Acd();
        objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objStudent.Sem = ddlSemester.SelectedValue;

        foreach (RepeaterItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            if (chkBox.Checked == true)
                objStudent.StudId += chkBox.ToolTip + ",";
        }

        if (objSC.UpdateSchemes(objStudent) != -99)
        {
            lblStatus2.Text = "Schemes Alloted Successfully";
            BindListView();
        }
        else
            lblStatus2.Text = "Error in Alloting Schemes";
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=schemeallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=schemeallotment.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege .SelectedValue, "A.LONGNAME");
        ddlBranch.Focus();
        //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
    }

    protected void lvStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.degreeno");
        ddlDegree.Focus();
    }
}
