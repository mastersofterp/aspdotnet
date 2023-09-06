//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : bulk semester promotion  UG                              
// CREATION DATE : 15-mar-2016
// CREATED BY    :                                                 
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_BulkSemesterPromotionUG : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.PopulateDropDownList();
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
            ddlSession.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student objS = new Student();
            string IDNO = string.Empty;
            int acdyearid = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                Label lblregno = item.FindControl("lblregno") as Label;
                Label lblstudname = item.FindControl("lblstudname") as Label;
                Label lblstatus = item.FindControl("lblstatus") as Label;
                CheckBox chksub = item.FindControl("chkRegister") as CheckBox;
                if (chksub.Checked == true && chksub.Enabled == true)
                {
                    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    objS.IdNo = Convert.ToInt32(lblregno.ToolTip);
                    IDNO = Convert.ToString(lblregno.ToolTip);
                    objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objS.SectionNo = Convert.ToInt32(lblstudname.ToolTip);
                    objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                    objS.CollegeCode = Session["colcode"].ToString();
                    objS.Dob = DateTime.Now;
                    if (!lblregno.Text.Trim().Equals(string.Empty)) objS.RollNo = lblregno.Text.Trim();
                    string ipAddress = Request.ServerVariables["REMOTE_HOST"];

                    //  int promotion = (GetViewStateItem("PROMOTIONNO") != string.Empty ? int.Parse(GetViewStateItem("PROMOTIONNO")) : 0);

                    CustomStatus cs = (CustomStatus)objSC.bulkStudentSemPromo(objS, sessionno, ipAddress, acdyearid);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage("Record updated successfully", this.Page);
                        btnShow_Click(sender, e);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Error", this.Page);
                    }
                }
            }
            if (string.IsNullOrEmpty(IDNO))
            {
                objCommon.DisplayMessage("Please Select Atleast One Student.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus();
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        //DataSet dsshow = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_TRRESULT B ON(A.IDNO=B.IDNO) LEFT OUTER JOIN ACD_SEM_PROMOTION C ON(A.IDNO = C.IDNO and b.semesterno=c.SEMESTERNO)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,b.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "('YES') AS STATUSNO,(case when b.result = 'P' then 1 else 2 END) as eligible", "b.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "and A.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "and A.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "and A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "and b.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "a.idno");

        DataSet dsshow = objCommon.FillDropDown("ACD_STUDENT A LEFT OUTER JOIN ACD_SEM_PROMOTION C ON(A.IDNO = C.IDNO)", "A.IDNO,A.REGNO,A.STUDNAME,A.SECTIONNO , ISNULL(C.PROMOTED_SEM,0) AS PROMOTED_SEM,('YES') AS STATUSNO,A.SEMESTERNO", "", "C.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND  A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND A.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND A.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "A.IDNO");
        if (dsshow.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = dsshow;
            lvStudent.DataBind();
            btnSave.Enabled = true;
        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            btnSave.Enabled = false;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
}
