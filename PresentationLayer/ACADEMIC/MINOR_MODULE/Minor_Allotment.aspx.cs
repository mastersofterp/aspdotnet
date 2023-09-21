using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web;
using System.Linq;

public partial class ACADEMIC_Minor_Allotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    DataSet dss = new DataSet();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    static string sessionuid = string.Empty;
    static string msessionuid = string.Empty;
    static string IpAddress = string.Empty;
    static string OrgID = string.Empty;

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
                Response.Redirect("~/default_atlas.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //to load all dropdown list
                this.PopulateDropDownList();
                this.SessionDropDownList();
                //this.MinorBind();
                
                //this.BindListView();

                //assign session values to static variables

                OrgID = Session["OrgId"].ToString();
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Minor_creation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Minor_creation.aspx");
        }
    }

    #region First Tab "Minor Allotment"
    protected void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "OrganizationId > 0 AND OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void BindListView()
    {
        dss = objCommon.GetMAllot();

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            lvMinor.DataSource = dss;
            lvMinor.DataBind();
            lvMinor.Visible = true;
        }
        else
        {
            lvMinor.DataSource = null;
            lvMinor.DataBind();
            lvMinor.Visible = false;
        }
    }

    protected void Clear()
    {
        ddlSchool.Items.Clear();
        ddlSchool.Items.Add(new ListItem("Please Select","0"));
        PopulateDropDownList();
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        lstMinor.Items.Clear();
        //lstMinor.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void BindDropDownDegree(string collegeId)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON (D.DEGREENO = CDB.DEGREENO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "CM.COLLEGE_ID > 0 AND CM.COLLEGE_ID =" + collegeId + "AND CDB.OrganizationId > 0 AND CDB.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "D.DEGREENO");
    }

    protected void BindDropDownBranch(string clgId, string degreeNo)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO = CDB.DEGREENO)", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "CM.COLLEGE_ID > 0 AND  D.DEGREENO > 0 AND CM.COLLEGE_ID =" + clgId + "AND D.DEGREENO =" + degreeNo + "AND CDB.OrganizationId > 0 AND CDB.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "B.BRANCHNO");
    }

    protected void BindDropDownSemester()
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT(SEMESTERNO)", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
    }

    protected void BindDropDownMinor()
    {
        DataSet ds = new DataSet();
        string clgId = ddlSchool.SelectedValue;
        string degNo = ddlDegree.SelectedValue;
        string branchNo = ddlBranch.SelectedValue;
        string cdbNo = string.Empty;

        cdbNo = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO = CDB.DEGREENO)", "CDB.CDBNO", "CM.COLLEGE_ID > 0 AND  D.DEGREENO > 0 AND B.BRANCHNO > 0 AND CM.COLLEGE_ID =" + clgId + "AND D.DEGREENO =" + degNo + "AND B.BRANCHNO =" + branchNo + "AND CDB.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
        ds = objCommon.GetMinorList(cdbNo);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lstMinor.DataSource = ds;
            lstMinor.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstMinor.DataTextField = ds.Tables[0].Columns[2].ToString();
            lstMinor.DataBind();
        }
    }

    protected void ClearAll()
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownDegree(ddlSchool.SelectedValue);
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownBranch(ddlSchool.SelectedValue, ddlDegree.SelectedValue);
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownSemester();
        BindDropDownMinor();
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string clgId = ddlSchool.SelectedValue;
        string degNo = ddlDegree.SelectedValue;
        string branchNo = ddlBranch.SelectedValue;
        string semNo = ddlSemester.SelectedValue;
         
        if (clgId == "0")
        {
            objCommon.DisplayMessage(this.updMinor, "Please Select School!!!", this.Page);
            return;
        }
        else if (degNo == "0")
        {
            objCommon.DisplayMessage(this.updMinor, "Please Select Degree!!!", this.Page);
            return;
        }
        else if (branchNo == "0")
        {
            objCommon.DisplayMessage(this.updMinor, "Please Select Branch!!!", this.Page);
            return;
        }
        else if (semNo == "0")
        {
            objCommon.DisplayMessage(this.updMinor, "Please Select Semester!!!", this.Page);
            return;
        }

        dss = objCommon.GetMinorStudentList(clgId, degNo, branchNo, semNo);

        //foreach (ListViewDataItem dataitem in lvMinor.Items)
        //{
        //    CheckBox cbRow = dataitem.FindControl("chkAllot") as CheckBox;
        //}

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            hdnChk.Value = dss.Tables[0].Rows.Count.ToString();
            pnlMinor.Visible = true;
            lvMinor.Visible = true;
            lvMinor.DataSource = dss;
            lvMinor.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string clgId = ddlSchool.SelectedValue;
        string degNo = ddlDegree.SelectedValue;
        string branchNo = ddlBranch.SelectedValue;
        string semNo = ddlSemester.SelectedValue;
        sessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
        msessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
        IpAddress = Request.ServerVariables["REMOTE_ADDR"];
        int count = 0;
        int allot = 0;
        string ID = string.Empty;
        string cdbNob = string.Empty;
        string minorlst = "";
        int St = 0;

        foreach (ListViewDataItem dataitem in lvMinor.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkAllot") as CheckBox;
            if (cbRow.Enabled == true)
            {
                if (cbRow.Checked == true)
                {
                    //ID += cbRow.Text + ",";
                    //allot = 1;
                    count++;
                } 
            }
        }

        if (count <= 0)
        {
            objCommon.DisplayMessage(this, "Please Select atleast One Student For Minor Allotment!!!", this);
            return;
        }
        else
        {
            foreach (ListViewDataItem dataitem in lvMinor.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkAllot") as CheckBox;
                Label lblidNo = dataitem.FindControl("lblStudId") as Label;
                Label lblcdbNo = dataitem.FindControl("lblStudName") as Label;
                dss = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO = CDB.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO)", "CDB.CDBNO", "CM.COLLEGE_NAME", "CM.COLLEGE_ID =" + clgId +"AND D.DEGREENO =" + degNo +"AND B.BRANCHNO =" + branchNo, "CDB.CDBNO");
                cdbNob = dss.Tables[0].Rows[0]["CDBNO"].ToString();
                if (cbRow.Enabled == true)
                {
                    if (cbRow.Checked == true)
                    {
                        foreach (ListItem items in lstMinor.Items)
                        {
                            if (items.Selected == true)
                            {
                                minorlst = items.Value + ',';
                            }
                        }
                        minorlst = minorlst.Substring(0, minorlst.Length - 1);
                        allot = 1;
                        St = objCommon.AllotMinor(clgId, degNo, branchNo, semNo, lblidNo.ToolTip, minorlst, allot, cdbNob, sessionuid, IpAddress, msessionuid);
                    }
                    //else
                    //{
                    //    objCommon.DisplayMessage(this.updMinor, "Please Select Atleast One Student For Minor Allotment!!!", this.Page);
                    //    return;
                    //}
                }
                //else
                //{
                //    allot = 0;
                //    minorlst = "";
                //    objCommon.AllotMinor(clgId, degNo, branchNo, semNo, lblidNo.ToolTip, minorlst, allot, cdbNob, sessionuid, IpAddress, msessionuid);
                //}
                cbRow.Checked = false;
            }

            if (St != 0)
            {
                objCommon.DisplayMessage(this.updMinor, "Minor Alloted Successfully!!!", this.Page);
                Clear();
                ds = objCommon.GetMinorStudentList(clgId, degNo, branchNo, semNo);
                lvMinor.DataSource = ds;
                lvMinor.DataBind();
                pnlMinor.Visible = false;
                lvMinor.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this.updMinor, "No Records Found For Minor Allotment!!!", this.Page);
                return;
            }
            
            //BindListView();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //ClearAll();
    }

    protected void lvMinor_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        // CODE BY SHAILENDRA K.
        CheckBox chkBox = e.Item.FindControl("chkAllot") as CheckBox;
        Label lblMinor = e.Item.FindControl("lblMinor") as Label;
        if (chkBox.Checked)
        {
            int IDNO = Convert.ToInt16(chkBox.ToolTip);
            string minorGrpNos = objCommon.LookUp("ACD_MINOR_GROUP_ALLOTMENT", "ISNULL(MNR_GRP_NO,0)", "IDNO=" + IDNO);
            if (!string.IsNullOrEmpty(minorGrpNos))
            {
                DataSet ds = objCommon.FillDropDown("ACD_MINOR_GROUP_MASTER", "ISNULL(MNR_GRP_NAME,'') MNR_GRP_NAME", "", "MNR_GRP_NO IN (" + minorGrpNos + ")", "");
                string val = string.Empty;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string minorGrpName = ds.Tables[0].Rows[i]["MNR_GRP_NAME"].ToString();
                        val += minorGrpName + ",";
                    }
                    if (!string.IsNullOrEmpty(val))
                        val = val.TrimEnd(',');
                    lblMinor.Text = val;
                    chkBox.Enabled = false;
                }
                else
                    lblMinor.Text = string.Empty;
            }
        }

        // CODE DONE BY SHAILENDRA K.
        //*********************************************************************************************************
        ////int cdbNo = 0;
        //string clgId = ddlSchool.SelectedValue;
        //string degNo = ddlDegree.SelectedValue;
        //string branchNo = ddlBranch.SelectedValue;
        //string mnrName = string.Empty;
        //string min = string.Empty;
        //string idno = string.Empty;
        //DataSet ds = new DataSet();
        //SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
        ////cdbNo = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO = CDB.DEGREENO)", "CDB.CDBNO", "CM.COLLEGE_ID > 0 AND  D.DEGREENO > 0 AND B.BRANCHNO > 0 AND CM.COLLEGE_ID =" + clgId + "AND D.DEGREENO =" + degNo + "AND B.BRANCHNO =" + branchNo + "AND CDB.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])));
        ////CheckBox chkBoxAT = e.Item.FindControl("chkAllot") as CheckBox;
        ////DataSet dsGetList = objsql.ExecuteDataSet("SELECT DISTINCT ISNULL(T.SECTIONNO,0) SECTIONNO,S.SECTIONNAME,ISNULL(T.BATCHNO,0)AS BATCHNO,1 Allot_AT FROM USER_ACC U WITH (NOLOCK) LEFT JOIN ACD_COURSE_TEACHER T WITH (NOLOCK) ON (U.UA_NO=T.ADTEACHER) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON(S.SECTIONNO = T.SECTIONNO) WHERE T.SESSIONNO = " + ddlSessionAT.SelectedValue + " AND T.SCHEMENO = " + ViewState["schemenoat"].ToString() + " AND T.SEMESTERNO  =" + ddlSemesterAT.SelectedValue + " AND T.COURSENO=" + chkBoxAT.ToolTip + " AND UA_DEPTNO like '%" + ddlDeptAT.SelectedValue + "%'  AND T.SECTIONNO=" + ddlSectionAT.SelectedValue + " AND T.BATCHNO=0 AND ISNULL(UA_TYPE,0)=3 AND ISNULL(T.CANCEL,0)=0 ");
        
        //dss = objCommon.GetMinorStudentList(clgId, degNo, branchNo);
        //ds = objCommon.GetMAllot();

        //for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        for (int m = 0; m < dss.Tables[0].Rows.Count; m++)
        //        {
        //            if (dss.Tables[0].Rows[m]["MNR_GRP_ALLOTED"].ToString() == "True")
        //            {
        //                mnrName = ds.Tables[0].Rows[i]["MNR_GRP_NAME"].ToString();
        //                //string mnrNo = ds.Tables[0].Rows[i]["MNR_GRP_NO"].ToString();
        //                //idno = dss.Tables[0].Rows[m]["IDNO"].ToString();
        //                //string mnrGrpNo = dss.Tables[0].Rows[m]["MNR_GRP_NO"].ToString();
        //                //if (mnrGrpNo.Contains(mnrNo))
        //                //{
        //                Label lblMinr = e.Item.FindControl("lblMinor") as Label;
        //                lblMinr.Text += mnrName + ",";
                        
        //                lblMinr.Text = lblMinr.Text.Substring(0, lblMinr.Text.Length - 1);
        //                //}
        //            }
        //            else
        //            {
        //                Label lblMinr = e.Item.FindControl("lblMinor") as Label;
        //                lblMinr.Text += "";
        //            }
        //        }
        //    }
        //*********************************************************************************************************
    }
    #endregion First Tab "Minor Allotment"

    #region Second Tab "Course Registration"
    protected void SessionDropDownList()
    {
        objCommon.FillDropDownList(ddlminorSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME, ACADEMIC_YEAR", "OrganizationId > 0 AND OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "SESSIONNO DESC");
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void CollegeDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID)", "CDB.CDBNO", "CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_PROGRAM, CM.COLLEGE_ID, CM.COLLEGE_NAME, B.BRANCHNO, B.LONGNAME, CDB.COLLEGE_ID AS CDB_COLLEGE_ID, CDB.BRANCHNO AS CDB_BRANCH_NO, CM.OrganizationId AS CM_ORGID, B.OrganizationId AS B_ORGID", "CM.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND B.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CDB.CDBNO");
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_BRANCH B ON (CDB.BRANCHNO = CDB.BRANCHNO)", "CDB.BRANCHNO", "CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_BRANCH", "CDB.OrganizationId > 0 AND CDB.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CDB.BRANCHNO");
    }

    protected void BatchDropDownList()
    {
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        //objCommon.FillDropDownList(ddlAdmBatch, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME, ACADEMIC_YEAR", "OrganizationId > 0 AND OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "SESSIONNO");
    }

    protected void MinorBind()
    {
        //objCommon.FillDropDownList(ddlCourseMinor, "ACD_MINOR_GROUP_MASTER", "MNR_GRP_NO", "MNR_GRP_NAME", "MNR_GRP_NO > 0", "MNR_GRP_NO");
        ddlCourseMinor.Items.Clear();
        //ddlCourseMinor.Items.Add(new ListItem("Please Select", "0"));
        dss = objCommon.Get_Minor_List(ddlCollege.SelectedValue);

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            ddlCourseMinor.DataSource = dss;
            ddlCourseMinor.DataValueField = dss.Tables[0].Columns[0].ToString();
            ddlCourseMinor.DataTextField = dss.Tables[0].Columns[1].ToString();
            ddlCourseMinor.DataBind();
        }
    }

    protected void ClearSelection()
    {
        ddlminorSession.Items.Clear();
        ddlminorSession.Items.Add(new ListItem("Please Select", "0"));
        SessionDropDownList();
        ddlCollege.Items.Clear();
        ddlCollege.Items.Add(new ListItem("Please Select", "0"));
        //CollegeDropDownList();
        ddlAdmBatch.Items.Clear();
        ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
        //BatchDropDownList();
        ddlSemesterminor.Items.Clear();
        ddlSemesterminor.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        txtTotalStd.Text = string.Empty;
        ddlCourseMinor.Items.Clear();
        MinorBind();
        pnlCourse.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void ddlminorSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        CollegeDropDownList();
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlminorSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "DISTINCT(BATCHNO)", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            ddlAdmBatch.Focus();

            //ddlSemester.Items.Clear();
            //ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
        MinorBind();
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemesterminor, "ACD_SEMESTER", "DISTINCT(SEMESTERNO)", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void ddlSemesterminor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "DISTINCT(SECTIONNO)", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
        pnlCourse.Visible = false;
        pnlMinor.Visible = false;
        pnlSubject.Visible = false;
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        //ClearAll();
        ClearSelection();
    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        if (pnlCourse.Visible == false && pnlSubject.Visible == false)
        {
            objCommon.DisplayMessage(this.updCourse, "Please Filter the Records on Above Selection First!!!", this.Page);
            return;
        }
        string sessionNo = ddlminorSession.SelectedValue;
        string clgBranch = ddlCollege.SelectedValue;
        string admBatch = ddlAdmBatch.SelectedValue;
        string semester = ddlSemesterminor.SelectedValue;
        string section = ddlSection.SelectedValue;
        string minorlst = string.Empty;
        int stdTotal = 0;
        string stdList = string.Empty;
        string courseList = string.Empty;
        sessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
        msessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
        IpAddress = Request.ServerVariables["REMOTE_ADDR"];
        int ST = 0;

        foreach (ListItem items in ddlCourseMinor.Items)
        {
            if (items.Selected == true)
            {
                minorlst += items.Value + ',';
            }
        }
        minorlst = minorlst.Substring(0, minorlst.Length - 1);

        foreach(ListViewDataItem items in lvCourse.Items)
        {
            CheckBox cChk = items.FindControl("chkCAllot") as CheckBox;
            Label std = items.FindControl("lblCStudId") as Label;
            //if(cChk.Checked == true)
            //{
            //    stdList += std.ToolTip + ",";
            //}

            if (cChk.Enabled == true)
            {
                if (cChk.Checked == true)
                {
                    foreach (ListViewDataItem item in lvSubject.Items)
                    {
                        CheckBox sChk = item.FindControl("chkSAllot") as CheckBox;
                        Label stdCourse = item.FindControl("lblCourseName") as Label;
                        if (sChk.Checked == true)
                        {
                            courseList += stdCourse.ToolTip + ",";
                            stdTotal = 1;
                        }
                    }
                    courseList = courseList.TrimEnd(',');

                    if (stdTotal == 1)
                    {
                        ST = objCommon.MinorCourseRegistration(sessionNo, clgBranch, admBatch, semester, section, minorlst, stdTotal, std.ToolTip, courseList, sessionuid, IpAddress, msessionuid);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updCourse, "Please Select Atlease One Course!!!", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updCourse, "Please Select Atlease One Student!!!", this.Page);
                    return;
                }
            }
        }
        if (ST != 0)
        {
            objCommon.DisplayMessage(this.updCourse, "Course Registered Successfully!!!", this.Page);
            ClearSelection();
            pnlCourse.Visible = false;
            pnlSubject.Visible = false;
        }
        else
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select Atlease One Student!!!", this.Page);
        }
        //stdList = stdList.TrimEnd(',');
    }

    protected void btnShow2_Click(object sender, EventArgs e)
    {
        if (ddlminorSession.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select Session!!!", this.Page);
            return;
        }

        if (ddlCollege.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select College & Program!!!", this.Page);
            return;
        }

        if (ddlScheme.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select Scheme!!!", this.Page);
            return;
        }

        if (ddlSemesterminor.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select Semester!!!", this.Page);
            return;
        }

        if (ddlSection.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select Section!!!", this.Page);
            return;
        }
        int count = 0;
        //if (ddlCourseMinor.Items.Count <= 0)
        //{
        //    objCommon.DisplayMessage(this.updCourse, "Please Select Minor!!!", this.Page);
        //    return;
        //}

        DataSet ds = new DataSet();
        
        //pnlCRegistration.Visible = true;
        string cdbNo = ddlCollege.SelectedValue;
        string mnrGrp = string.Empty;

        foreach (ListItem Item in ddlCourseMinor.Items)
        {  
            if (Item.Selected == true) 
            {
                mnrGrp += Item.Value + ",";
                count++;
            } 
        }
        mnrGrp = mnrGrp.TrimEnd(',');

        if (count != 0)
        {
            dss = objCommon.GetStudentListForCourse(mnrGrp);
            ds = objCommon.FillDropDown("ACD_MINOR_OFFERED_COURSE A CROSS APPLY (SELECT * FROM DBO.Split (A.COURSENO,',')) B CROSS APPLY (SELECT * FROM DBO.Split (A.CCODE,',')) C CROSS APPLY (SELECT * FROM DBO.Split (A.COURSE_NAME,',')) D", "DISTINCT B.Value AS COURSENO", "C.VALUE AS CCODE, D.Value AS COURSE_NAME", "B.ID = C.Id AND C.ID = D.Id AND MNR_GRP_NO IN (SELECT VALUE FROM dbo.Split('" + mnrGrp + "', ','))", "COURSENO");
            //ds = objCommon.FillDropDown("ACD_MINOR_OFFERED_COURSE CROSS APPLY STRING_SPLIT(COURSENO, ',')", "VALUE AS COURSENO", "COURSE_NAME, MNR_OFFERED_COURSE_NO, CDB_NO", "MNR_GRP_NO IN (SELECT VALUE FROM dbo.Split('" + mnrGrp + "', ','))", "COURSENO");
            //, A.MNR_OFFERED_COURSE_NO, A.CDB_NO

            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                pnlCourse.Visible = true;
                pnlSubject.Visible = true;
                //pnlCRegistration.Visible = true;
                lvCourse.Visible = true;
                lvSubject.Visible = true;
                lvCourse.DataSource = dss;
                lvCourse.DataBind();
                hdfsub.Value = dss.Tables[0].Rows.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.updCourse, "No Record Found! Please Allot Minor to Students First!!!", this.Page);
                pnlCourse.Visible = false;
                pnlSubject.Visible = false;
                return;
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSubject.Visible = true;
                lvSubject.Visible = true;
                lvSubject.DataSource = ds;
                lvSubject.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updCourse, "No Record Found! Please Add Courses to Minor!!!", this.Page);
                pnlCourse.Visible = false;
                pnlSubject.Visible = false;
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updCourse, "Please Select Minor!!!", this.Page);
            return;
        }

        pnlCourse.Visible = true;
        pnlSubject.Visible = true;
        lvSubject.Visible = true;
    }
    #endregion Second Tab "Course Registration"
}