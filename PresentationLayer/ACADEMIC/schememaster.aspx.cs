using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;
public partial class ACADEMIC_SchemeMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SchemeController objBC = new SchemeController();
    ConfigController objConfig = new ConfigController();
    StudentController objSC = new StudentController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string degreeno, branchno, schemeno, schemename, branchname, degreebranch, degreebranchscheme, degreeOfBranch, branchOfScheme = string.Empty;
    int count = 0;

    #region Page Events
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
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {

                }
                objCommon.FillDropDownList(ddlCollege1, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ActiveStatus=1 AND  OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "", "COLLEGE_ID");
            }
            //Populate the Drop Down Lists
            BindListViewScmType();
            BindListView();
            ViewState["action"] = "add";
            PopulateDropDown();
            PopulateDropDownList();
            ViewState["schemeno"] = null;
            ViewState["action"] = null;
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 16/12/2021
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 16/12/2021
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);

    }
    #endregion

    #region Scheme Creation Events

    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            string deptnos = Session["userdeptno"].ToString() == string.Empty ? "0" : Session["userdeptno"].ToString();
            objCommon.FillDropDownList(ddlBatchNo, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlDegreeNo, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO = CDB.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO> 0 AND DEPTNO IN (" + deptnos + ")", "DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegreeNo, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO = CDB.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "D.DEGREENO> 0", "DEGREENO");
            }
            objCommon.FillDropDownList(ddlSchemeType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO> 0", "SCHEMETYPENO");
            objCommon.FillDropDownList(ddlPatternName, "ACD_EXAM_PATTERN", "PATTERNNO", "PATTERN_NAME", "ISNULL(ACTIVESTATUS,0)=1", "PATTERNNO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListView()
    {
        try
        {
            SchemeController objSC = new SchemeController();
            string sessiondeptno = string.Empty;
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                sessiondeptno = "0";
            }
            else
            {
                sessiondeptno = Session["userdeptno"] == string.Empty ? "0" : Session["userdeptno"].ToString();
            }
            int degreeno = Convert.ToInt32(ddlDegreeNo.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeNo.SelectedValue) : 0);
            int deptno = Convert.ToInt32(ddlDept.SelectedIndex > 0 ? Convert.ToInt32(ddlDept.SelectedValue) : 0);
            int branchno = Convert.ToInt32(ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0);

            DataSet ds = objSC.GetSchemeData(deptno, degreeno, branchno, sessiondeptno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvScheme.DataSource = ds;
                lvScheme.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvScheme);//Set label - 
            }
            else
            {
                lvScheme.DataSource = null;
                lvScheme.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetail(int schemeno)
    {
        SchemeController objSC = new SchemeController();
        SqlDataReader dr = objSC.GetSingleScheme(schemeno);

        //Show scheme create Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["schemeno"] = schemeno.ToString();
                ddlDegreeNo.SelectedValue = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO> 0", "DEPTNO");
                ddlDept.SelectedValue = dr["DEPTNO"] == DBNull.Value ? "0" : dr["DEPTNO"].ToString();
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO> 0", "BRANCHNO");
                ddlBranch.SelectedValue = dr["BRANCHNO"] == DBNull.Value ? "0" : dr["BRANCHNO"].ToString();
                //if (ddlBranch.SelectedIndex > 0)
                //{
                //    int count = Convert.ToInt32(objCommon.LookUp("ACD_SPECIALISATION_MAPPING", "count(1)", "BRANCHNO =" + ddlBranch.SelectedValue + " AND DEGREENO =" + ddlDegreeNo.SelectedValue));
                //    if (count > 0)
                //    {
                //        divSpecialization.Visible = true;
                //        objCommon.FillDropDownList(ddlSpecialization, "ACD_SPECIALISATION_MAPPING SM INNER JOIN ACD_SPECIALISATION S ON(SM.SPECIALISATIONNO = S.SPECIALISATIONNO) INNER JOIN ACD_KNOWLEDGE_PARTNER KP ON(KP.KNOWLEDGE_PARTNER_NO = SM.KNOWLEDGE_PARTNER_NO)", "SM.SPECIAL_MAP_NO", "S.SPECIALISATION_NAME + '-' + KP.KNOWLEDGE_PARTNER AS SPECIALISATION_NAME", "SM.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + " AND SM.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "S.SPECIALISATION_NAME");

                //        //objCommon.FillDropDownList(ddlSpecialization, "ACD_SPECIALISATION", "SPECIALISATIONNO", "SPECIALISATION_NAME", "DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SPECIALISATION_NAME");
                //        ddlSpecialization.Focus();
                //    }
                //    else
                //    {
                //        divSpecialization.Visible = false;
                //    }
                //}
                //else
                //{
                //    ddlSpecialization.Items.Clear();
                //    ddlSpecialization.Items.Add(new ListItem("Please Select", "0"));
                //}
                //ddlSpecialization.SelectedValue = dr["SPECIAL_MAP_NO"] == DBNull.Value ? "0" : dr["SPECIAL_MAP_NO"].ToString();

                ddlBatchNo.SelectedValue = dr["ADMBATCH"] == DBNull.Value ? "0" : dr["ADMBATCH"].ToString();
                ddlgrademarks.SelectedValue = dr["GRADEMARKS"].ToString().Replace(" ", "");
                ddlSchemeType.SelectedValue = dr["SCHEMETYPE"] == DBNull.Value ? "0" : dr["SCHEMETYPE"].ToString();
                ddlPatternName.SelectedValue = dr["PATTERNNO"] == DBNull.Value ? "0" : dr["PATTERNNO"].ToString();
                ddlStudyPattern.SelectedValue = dr["STUDY_PATTERN_NO"] == DBNull.Value ? "0" : dr["STUDY_PATTERN_NO"].ToString();
                txtCredits.Text = dr["MIMIMUM_CREDITS_DEGREE"] == DBNull.Value ? "0" : dr["MIMIMUM_CREDITS_DEGREE"].ToString();
                txtAbolishAttmp.Text = dr["ABOLISH_ATTEMPTS"] == DBNull.Value ? "0" : dr["ABOLISH_ATTEMPTS"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    private void ShowReport(string reportTitle, string rptFileName, int type, int schemeno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (type == 1)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_DEPT_NO=" + ddlDept.SelectedValue + ",@P_DEGREE_NO=" + ddlDegreeNo.SelectedValue;
            else if (type == 2)
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + schemeno.ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSchemeCreation, this.updSchemeCreation.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Clear()
    {
        ddlDept.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlBatchNo.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlPatternName.SelectedIndex = 0;
        ddlgrademarks.SelectedIndex = 0;
        ddlDegreeNo.SelectedIndex = 0;
        //ddlSpecialization.SelectedIndex = 0;
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        btnExcelReport.Enabled = false;
        ddlDept.Items.Clear();
        ddlDept.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        txtAbolishAttmp.Text = string.Empty;
        txtCredits.Text = string.Empty;
        ddlStudyPattern.SelectedIndex = 0;
    }
    private void Clear1()
    {
        ddlDept.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlBatchNo.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlPatternName.SelectedIndex = 0;
        ddlgrademarks.SelectedIndex = 0;
        ddlDegreeNo.SelectedIndex = 0;
        //ddlSpecialization.SelectedIndex = 0;
       // lvScheme.DataSource = null;
       // lvScheme.DataBind();
        btnExcelReport.Enabled = false;
        ddlDept.Items.Clear();
        ddlDept.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        txtAbolishAttmp.Text = string.Empty;
        txtCredits.Text = string.Empty;
        ddlStudyPattern.SelectedIndex = 0;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divSpecialization.Visible = false;
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        lblStatus.Text = string.Empty;
        if (ddlDept.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
            BindListView();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        }
        ddlSchemeType.SelectedIndex = 0;
        ddlBatchNo.SelectedIndex = 0;
        ViewState["action"] = null;
        ViewState["schemeno"] = null;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        if (ddlBranch.SelectedValue != "0")
        {
            btnExcelReport.Enabled = true;
            // btnReport.Enabled = true;
            btnCheckListReport.Enabled = true;
            lblStatus.Text = string.Empty;
            BindListView();
            ddlBatchNo.Focus();
        }
        else
        {
            btnExcelReport.Enabled = true;
            //btnReport.Enabled = true;
            btnCheckListReport.Enabled = true;
            lblStatus.Text = string.Empty;
        }
    }

    protected void ddlDegreeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvScheme.DataSource = null;
        lvScheme.DataBind();
        string deptnos = Session["userdeptno"].ToString() == string.Empty ? "0" : Session["userdeptno"].ToString();
        if (ddlDegreeNo.SelectedIndex > 0)
        {
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegreeNo.SelectedValue + "", "D.DEPTNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegreeNo.SelectedValue + " AND B.DEPTNO IN (" + deptnos + " )", "D.DEPTNAME");
            }
            ddlDept.Focus();
            this.BindListView();
        }
        else
        {
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        }
        //divSpecialization.Visible = false;
        //ddlBranch.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        ddlPatternName.SelectedIndex = 0;
        ddlBatchNo.SelectedIndex = 0;
        btnReport.Enabled = true;
        btnCheckListReport.Enabled = true;
        lblStatus.Text = string.Empty;
        ViewState["action"] = null;
        ViewState["schemeno"] = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        btnReport.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("SchemeMaster", "rptSchemeMaster.rpt", 1, 0);
    }

    protected void btnCheckListReport_Click(object sender, EventArgs e)
    {
        if (ViewState["schemeno"] != null)
            ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", 2, Convert.ToInt16(ViewState["schemeno"].ToString()));
        else
        {
            string schemeno = "0";
            if (ddlBranch.SelectedValue != "0" & ddlSchemeType.SelectedValue != "0")
            {
                schemeno = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", ddlBranch.SelectedValue != "0" ? "BRANCHNO =" + ddlBranch.SelectedValue + " AND SCHEMETYPE =" + ddlSchemeType.SelectedValue : "SCHEMENO =0");
                if (schemeno == "")
                {
                    objCommon.DisplayMessage(this.updSchemeCreation, "Data Not Found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updSchemeCreation, "Please Select Branch & Scheme Type", this.Page);
                return;
            }

            ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", 2, Convert.ToInt16(schemeno));
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int schemeno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(schemeno);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SchemeController objSC = new SchemeController();
            Scheme objScheme = new Scheme();

            string DegreeShortName = objCommon.LookUp("ACD_DEGREE WITH (NOLOCK)", "CODE", "DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue));

            //objScheme.SchemeName =ddlDegreeNo.SelectedItem .Text  +"-"+ ddlBranch.SelectedItem.Text + "-" + ddlBatchNo.SelectedItem.Text;

            //if (ddlSpecialization.SelectedIndex > 0)
            //{
            //    objScheme.SchemeName = DegreeShortName + "-" + ddlDept.SelectedItem.Text + "-" + ddlBranch.SelectedItem.Text + "-" + ddlSpecialization.SelectedItem.Text + "-" + ddlBatchNo.SelectedItem.Text;
            //}
            //else
            //{
            //}
            //objScheme.SchemeName = DegreeShortName + "-" + ddlDept.SelectedItem.Text + "-" + ddlBranch.SelectedItem.Text + "-" + ddlBatchNo.SelectedItem.Text;
            objScheme.SchemeName = DegreeShortName + "-" + ddlBranch.SelectedItem.Text + "-" + ddlBatchNo.SelectedItem.Text + "-" + ddlDept.SelectedItem.Text;


            char[] ch = { ',' };
            string[] br = ddlBranch.SelectedValue.Split(ch);

            objScheme.BranchNo = Convert.ToInt32(br[0]);
            objScheme.DegreeNo = Convert.ToInt32(ddlDegreeNo.SelectedValue);
            objScheme.Dept_No = Convert.ToInt32(ddlDept.SelectedValue);
            objScheme.BatchNo = Convert.ToInt32(ddlBatchNo.SelectedValue);
            objScheme.NewScheme = 1;
            objScheme.CollegeCode = Session["colcode"].ToString();
            ////For scheme types
            //objScheme.SchemeName = ddlBranch.SelectedItem.Text + "-" + ddlBatchNo.SelectedItem.Text + ' ' + "[" + ddlSchemeType.SelectedItem.Text + "]";

            objScheme.SchemeTypeNo = Convert.ToInt32(ddlSchemeType.SelectedValue);
            objScheme.PatternNo = Convert.ToInt32(ddlPatternName.SelectedValue);
            objScheme.gradeMarks = ddlgrademarks.SelectedValue;
            if (txtCredits.Text == "")
            {
                objScheme.MinimumCredits = 0;
            }
            else
            {
                objScheme.MinimumCredits = Convert.ToInt32(txtCredits.Text);
            }
            objScheme.AbolishAttempts = txtAbolishAttmp.Text;
            objScheme.StudyPatternNo = Convert.ToInt32(ddlStudyPattern.SelectedValue);
            objScheme.StudyPatternName = ddlStudyPattern.SelectedItem.Text;
            //objScheme.Specialization = Convert.ToInt32(ddlSpecialization.SelectedValue);

            string schNo = "-100";

            schNo = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "BRANCHNO=" + Convert.ToInt32(br[0]) + "AND DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + "AND ADMBATCH=" + Convert.ToInt32(ddlBatchNo.SelectedValue) + " AND DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND SCHEMETYPE=" + Convert.ToInt32(ddlSchemeType.SelectedValue));

            //if (ddlSpecialization.SelectedIndex > 0)
            //{
            //    schNo = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "BRANCHNO=" + Convert.ToInt32(br[0]) + "AND DEGREENO=" + Convert.ToInt32(ddlDegreeNo.SelectedValue) + "AND ADMBATCH=" + Convert.ToInt32(ddlBatchNo.SelectedValue) + " AND DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND SCHEMETYPE=" + Convert.ToInt32(ddlSchemeType.SelectedValue) + " AND SPECIAL_MAP_NO=" + Convert.ToInt32(ddlSpecialization.SelectedValue));
            //}
            //else
            //{
            //}


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].Equals("edit"))
                {
                    //Update Scheme
                    objScheme.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
                    CustomStatus cs = (CustomStatus)objSC.UpdateScheme(objScheme);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        //lblStatus.Text = "Scheme Updated";
                        objCommon.DisplayMessage(this.updSchemeCreation, "Scheme Updated Successfully.", this.Page);
                        Clear();
                        BindListView();
                    }
                    ViewState["action"] = null;
                }
            }
            else
            {
                if (schNo == "")
                {
                    //Add Scheme
                    int schemeno = objSC.AddScheme(objScheme);
                    if (schemeno != -99)
                    {
                        //lblStatus.Text = "Scheme Created Successfully";
                        objCommon.DisplayMessage(this.updSchemeCreation, "Scheme Created Successfully.", this.Page);
                        ViewState["schemeno"] = schemeno.ToString();
                        BindListView();
                        Clear1();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updSchemeCreation, "Transaction Failed!!", this.Page);
                    }
                }
                else
                    objCommon.DisplayMessage(this.updSchemeCreation, "Already Exists!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    #endregion

    #region Scheme Allotment Events


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch1, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
            ddlBranch1.Focus();
            objCommon.FillDropDownList(ddlBranchForScheme, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
            ddlBranchForScheme.Focus();
        }
        else
        {
            ddlBranch1.Items.Clear();
            ddlBranch1.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.degreeno");
            ddlDegree.Focus();
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch1.Items.Clear();
            ddlBranch1.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;
        ddlBatchYear.SelectedIndex = 0;
        ddlSemester1.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        lblSch.Visible = false;
        ddlScheme.SelectedIndex = 0;
        ddlScheme.Enabled = false;
        ddlBranchForScheme.Enabled = false;
        ddlBranchForScheme.SelectedIndex = 0;
        lblStatus.Text = string.Empty;
        lblStatus2.Text = string.Empty;
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch1.Items.Clear();
        ddlBranch1.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        BindListViewofSchemeAll();
    }

    protected void btnAssignSch_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student_Acd objStudent = new Student_Acd();
        objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objStudent.Sem = ddlSemester.SelectedValue;

        foreach (ListViewItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            if (chkBox.Checked == true)
                objStudent.StudId += chkBox.ToolTip + ",";
        }

        if (objSC.UpdateSchemes(objStudent) != -99)
        {
            objCommon.DisplayMessage(this.updSchemeAllot, "Schemes Alloted Successfully !", this.Page);
            BindListViewofSchemeAll();
        }
        else
            objCommon.DisplayMessage("Error in Alloting Schemes ", this.Page);
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }

    protected void ddlBatchYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlBatch.SelectedIndex = ddlBatchYear.SelectedIndex;

        //Populate Branch
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_BATCHNO", Convert.ToInt32(ddlBatchYear.SelectedValue));

            SqlDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN_SP_RET_BRANCH_BYBATCH", objParams);

            ddlBranch1.Items.Clear();
            ddlBranch1.Items.Add(new ListItem("Please Select", "0"));
            while (dr.Read())
                ddlBranch1.Items.Add(new ListItem(dr["LongName"].ToString(), dr["BranchNo"].ToString()));
            dr.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlBranch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Populate Scheme
        try
        {
            //if (ddlBranch1.SelectedIndex > 0)
            //{
            //    int count = Convert.ToInt32(objCommon.LookUp("ACD_SPECIALISATION_MAPPING", "count(1)", "BRANCHNO =" + ddlBranch1.SelectedValue + " AND DEGREENO =" + ddlDegree.SelectedValue));
            //    if (count > 0)
            //    {
            //        divSASpecilaization.Visible = true;
            //        objCommon.FillDropDownList(ddlSASpecialisation, "ACD_SPECIALISATION_MAPPING SM INNER JOIN ACD_SPECIALISATION S ON(SM.SPECIALISATIONNO = S.SPECIALISATIONNO) INNER JOIN ACD_KNOWLEDGE_PARTNER KP ON(KP.KNOWLEDGE_PARTNER_NO = SM.KNOWLEDGE_PARTNER_NO)", "SM.SPECIAL_MAP_NO", "S.SPECIALISATION_NAME + '-' + KP.KNOWLEDGE_PARTNER AS SPECIALISATION_NAME", "SM.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SM.BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue), "S.SPECIALISATION_NAME");
            //        ddlSASpecialisation.Focus();
            //    }
            //    else
            //    {
            //        divSASpecilaization.Visible = false;
            //    }
            //}
            //else
            //{
            //    ddlSASpecialisation.Items.Clear();
            //    ddlSASpecialisation.Items.Add(new ListItem("Please Select", "0"));
            //}
            //if (ddlSType.SelectedIndex == 0)
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue,"SCHEMENO");
          //  objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + ddlDegree.SelectedValue + " and BRANCHNO=" + ddlBranch1.SelectedValue, "SCHEMENO");
            //else
            //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND SCHEMETYPE=" + ddlSType.SelectedValue, "SCHEMENO");

            objCommon.FillDropDownList(ddlSemester1, "ACD_STUDENT A INNER JOIN ACD_SEMESTER B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "DEGREENO=" + ddlDegree.SelectedValue + " and BRANCHNO=" + ddlBranch1.SelectedValue + "", "A.SEMESTERNO");

            lvStudents.DataSource = null;
            lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListViewofSchemeAll()
    {
        hdfTot.Value = "0";
        txtTotStud.Text = "0";
        try
        {
           
            // DataSet ds = objSC.GetStudentsBySchemeAllot(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlBranch1.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSType.SelectedValue), Convert.ToInt16(ddlSemester1.SelectedValue), Convert.ToInt16(ddlSASpecialisation.SelectedValue));

            DataSet ds = objSC.GetStudentsBySchemeAllot(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlBranch1.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSType.SelectedValue), Convert.ToInt16(ddlSemester1.SelectedValue));
            //DataSet ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue));

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label - 
                lblSch.Visible = true;
                ddlScheme.Enabled = true;
                ddlBranchForScheme.Enabled = true;
                lvStudents.Visible = true;
                lblStatus.Text = string.Empty;
            }
            else
            {
                //lblStatus.Text = "No Students for selected criteria";
                objCommon.DisplayMessage(this.Page, "No Students for selected criteria", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lblSch.Visible = false;
                ddlScheme.Enabled = false;
                ddlBranchForScheme.Enabled = false;
                lvStudents.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlSType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPE");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Scheme Type Events


    protected void btnSubmitSchemeType_Click(object sender, EventArgs e)
    {
        string schemename = txtScheme.Text.Trim();
        string SCHEMECODE = txtCode.Text.Trim();
        int CollegeCode = Convert.ToInt32(Session["colcode"]);
        // ViewState["action"] = "AddschemeType";

        try
        {
            //Check whether to add or update
            if (ViewState["action"] == null)
            {
                //}
                //if (!ViewState["action"].ToString().Equals("editschemetype") && ViewState["action"]==null)
                //{
                string batchno = objCommon.LookUp("ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE='" + txtScheme.Text.Trim() + "' and SCHEMETYPE_CODE='" + txtCode.Text.Trim() + "'");
                if (batchno != null && batchno != string.Empty)
                {
                    schemeclear();
                    objCommon.DisplayMessage(this.updSchemeType, "Record Already Exist", this.Page);
                    return;
                }
                //Add Scheme Type
                else
                {
                    if (schemename == "" || schemename == "")
                    {
                        objCommon.DisplayMessage(this.updSchemeType, "Please Enter Scheme Type Name and Code properly !", this.Page);
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objBC.AddSchemeType(schemename, SCHEMECODE, CollegeCode);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = null;
                            schemeclear();
                            objCommon.DisplayMessage(this.updSchemeType, "Record Saved Successfully!", this.Page);
                            BindListViewScmType();
                        }
                        else
                        {
                            objCommon.DisplayMessage("Something went wrong", this.Page);
                        }
                    }
                }

            }
            else
            {
                int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
                string schemecode = Convert.ToString(txtCode.Text.Trim());
                string schemname = Convert.ToString(txtScheme.Text.Trim());
                //Edit Scheme Type
                if (Schemeno > 0)
                {
                    CustomStatus cs = (CustomStatus)objBC.UpdateSchemeType(Schemeno, schemecode, schemname);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ViewState["action"] = null;
                        schemeclear();
                        objCommon.DisplayMessage(this.updSchemeType, "Record Updated Successfully!", this.Page);
                    }

                }
            }

            BindListViewScmType();

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancelSchemeType_Click(object sender, EventArgs e)
    {
        schemeclear();
    }

    private void schemeclear()
    {
        txtScheme.Text = string.Empty;
        txtCode.Text = string.Empty;
        ViewState["action"] = null;
    }

    //protected void btnEditDegType_Click(object sender, ImageClickEventArgs e)
    //{

    //}

    private void BindListViewScmType()
    {
        try
        {
            DataSet ds = objBC.GetAllScheme();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSchemeType.Visible = true;
                lvSchemeType.DataSource = ds;
                lvSchemeType.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSchemeType);//Set label - 
            }
            else
            {
                pnlSchemeType.Visible = false;
                lvSchemeType.DataSource = null;
                lvSchemeType.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int schemeno)
    {
        SqlDataReader dr = objBC.GetSchemeType(schemeno);

        if (dr != null)
        {
            if (dr.Read())
            {
                txtScheme.Text = dr["SCHEMETYPE"] == null ? string.Empty : dr["SCHEMETYPE"].ToString();
                txtCode.Text = dr["SCHEMETYPE_CODE"] == null ? string.Empty : dr["SCHEMETYPE_CODE"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }


    #endregion




    protected void btnExcelReport_Click(object sender, EventArgs e)
    
    {
        try
        {
            SchemeController objSC = new SchemeController();
            GridView GV = new GridView();
            DataSet ds = null;
            string sessiondeptno = string.Empty;
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                sessiondeptno = "0";
            }
            else
            {
                sessiondeptno = Session["userdeptno"] == string.Empty ? "0" : Session["userdeptno"].ToString();
            }
            int degreeno = Convert.ToInt32(ddlDegreeNo.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeNo.SelectedValue) : 0);
            int deptno = Convert.ToInt32(ddlDept.SelectedIndex > 0 ? Convert.ToInt32(ddlDept.SelectedValue) : 0);
            int branchno = Convert.ToInt32(ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0);

            DataSet dsfee = objSC.GetSchemeData_excel(deptno, degreeno, branchno, sessiondeptno);

            if (dsfee.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsfee;
                GV.DataBind();
                string attachment = "attachment; filename=SchemedataExcel.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEditSchemeType_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int schemeno = int.Parse(btnEdit.CommandArgument);
        ViewState["schemeno"] = schemeno;
        ShowDetails(schemeno);
        ViewState["action"] = "editschemetype";
    }

    #region College Regulation Configuration

    // Added by Jay Takalkhede on date 16/09/2022
    protected void ddlCollege1_SelectedIndexChanged(object sender, EventArgs e)
    {
        cblDegree.Items.Clear();
        if (ddlCollege1.SelectedIndex > 0)
        {
            divDegree.Visible = true;
            divBranch.Visible = false;
            divScheme.Visible = false;
            btnSubmit1.Enabled = false;
            fillChecboxlistDegree();
        }
        else
        {
            divBranch.Visible = false;
            divDegree.Visible = false;
            divScheme.Visible = false;
            btnSubmit1.Enabled = false;
        }
    }
    public void fillChecboxlistDegree()
    {
        degreeno = string.Empty;
        ViewState["degreeno"] = "";

        DataSet dsDegree = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CDB.DEGREENO) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.COLLEGE_ID=CDB.COLLEGE_ID AND CSM.DEGREENO=CDB.DEGREENO)", "DISTINCT CDB.DEGREENO", "D.DEGREENAME, CSM.DEGREENO AS DEGREENO1", "ISNULL(D.DEGREENO,0)>0 AND CDB.COLLEGE_ID=" + ddlCollege1.SelectedValue, "CDB.DEGREENO");
        if (dsDegree != null && dsDegree.Tables.Count > 0)
        {
            if (dsDegree.Tables[0].Rows.Count > 0)
            {
                cblDegree.DataSource = null;
                cblDegree.DataBind();
                cblDegree.DataTextField = "DEGREENAME";
                cblDegree.DataValueField = "DEGREENO";
                cblDegree.DataSource = dsDegree.Tables[0];
                cblDegree.DataBind();
                divDegree.Visible = true;

                int i = 0;
                foreach (ListItem item in cblDegree.Items)
                {
                    if (dsDegree.Tables[0].Rows[i]["DEGREENO1"].ToString() == item.Value)
                    {
                        item.Selected = true;
                        degreeno = degreeno + item.Value + ",";
                    }
                    else
                    {
                        item.Selected = false;
                    }

                    i++;
                }

                if (degreeno.Length > 0)
                {
                    if (degreeno.Substring(degreeno.Length - 1).Contains(','))
                    {
                        degreeno = degreeno.Remove(degreeno.Length - 1);
                    }
                    ViewState["degreeno"] = degreeno.ToString();
                    fillChecboxlistBranch(degreeno);
                }
                else
                {
                    divBranch.Visible = false;
                    divScheme.Visible = false;
                }

            }
            else
            {
                cblDegree.DataSource = null;
                cblDegree.DataBind();
                divDegree.Visible = false;
                divBranch.Visible = false;
                divScheme.Visible = false;
                btnSubmit1.Enabled = false;
            }
        }
    }

    public void fillChecboxlistBranch(string degreeno)
    {
        branchno = string.Empty;
        ViewState["branchno"] = "";
        ViewState["degreebranch"] = "";
        branchname = "";

        DataSet dsBranch = null;
        dsBranch = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=CDB.BRANCHNO) INNER JOIN ACD_DEGREE D on (D.DEGREENO=CDB.DEGREENO) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.COLLEGE_ID=CDB.COLLEGE_ID AND CSM.DEGREENO=CDB.DEGREENO and CSM.BRANCHNO=CDB.BRANCHNO)", "DISTINCT (CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))) AS BRANCHNO, CDB.BRANCHNO AS BRANCHNO1", "D.CODE +' - '+ B.LONGNAME LONGNAME, CSM.BRANCHNO AS BRANCHNO2", "ISNULL(B.BRANCHNO,0)>0 AND CDB.COLLEGE_ID=" + ddlCollege1.SelectedValue + " AND CDB.DEGREENO IN(" + degreeno + ")", "BRANCHNO1");
        if (dsBranch != null && dsBranch.Tables.Count > 0)
        {
            if (dsBranch.Tables[0].Rows.Count > 0)
            {
                cblBranch.Items.Clear();
                cblBranch.DataTextField = "LONGNAME";
                cblBranch.DataValueField = "BRANCHNO";
                cblBranch.DataSource = dsBranch.Tables[0];
                cblBranch.DataBind();
                divBranch.Visible = true;

                int i = 0;
                foreach (ListItem item in cblBranch.Items)
                {
                    if (dsBranch.Tables[0].Rows[i]["BRANCHNO2"].ToString() == item.Value.Split('$')[1])
                    {
                        item.Selected = true;
                        branchno = branchno + item.Value.Split('$')[1] + ",";
                        degreebranch = degreebranch + item.Value + ",";
                        branchname = branchname + item.Text + "$";
                    }
                    else
                    {
                        item.Selected = false;
                    }
                    i++;
                }

                if (ViewState["degreeno"].ToString().Length > 0)
                {
                    if (branchno.Length > 0)
                    {
                        if (branchno.Substring(branchno.Length - 1).Contains(','))
                        {
                            branchno = branchno.Remove(branchno.Length - 1);
                        }

                        if (degreebranch.Substring(degreebranch.Length - 1).Contains(','))
                        {
                            degreebranch = degreebranch.Remove(degreebranch.Length - 1);
                        }

                        if (branchname.Substring(branchname.Length - 1).Contains('$'))
                        {
                            branchname = branchname.Remove(branchname.Length - 1);
                        }
                        ViewState["branchno"] = branchno;
                        ViewState["degreebranch"] = degreebranch;
                        ViewState["branchname"] = branchname;
                    }
                    else
                    {
                        divScheme.Visible = false;
                    }
                    fillChecboxlistScheme(ViewState["degreeno"].ToString(), branchno);
                }
                else
                {
                    divBranch.Visible = false;
                    divScheme.Visible = false;
                }

            }
            else
            {
                cblBranch.DataSource = null;
                cblBranch.DataBind();
                divBranch.Visible = false;
                divScheme.Visible = false;
            }
        }
    }
    public void fillChecboxlistScheme(string degreeno, string branchno)
    {
        Session["dsScheme"] = null;
        schemeno = string.Empty;
        ViewState["schemeno"] = "";
        ViewState["degreebranchscheme"] = "";

        DataSet dsScheme = null;

        if (branchno == string.Empty || branchno == "" || branchno == null)
        {
            dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB LEFT JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.SCHEMENO=S.SCHEMENO)", "DISTINCT (CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(ISNULL(S.SCHEMENO,0) AS NVARCHAR(20))) AS SCHEMENO, ISNULL(S.SCHEMENO,0) AS SCHEMENO1", "S.SCHEMENAME, ISNULL(CSM.SCHEMENO,0) AS SCHEMENO2, CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO", "CDB.COLLEGE_ID=" + ddlCollege1.SelectedValue + " AND CDB.DEGREENO IN(" + degreeno + ") AND SCHEMENAME != ''", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");
        }
        else
        {
            dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB LEFT JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO) LEFT JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.SCHEMENO=S.SCHEMENO)", "DISTINCT (CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(ISNULL(S.SCHEMENO,0) AS NVARCHAR(20))) AS SCHEMENO, ISNULL(S.SCHEMENO,0) AS SCHEMENO1", "S.SCHEMENAME, ISNULL(CSM.SCHEMENO,0) AS SCHEMENO2, CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO", "CDB.COLLEGE_ID=" + ddlCollege1.SelectedValue + " AND CDB.DEGREENO IN(" + degreeno + ") AND CDB.BRANCHNO IN(" + branchno + ") AND SCHEMENAME != ''", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");
        }

        if (dsScheme != null && dsScheme.Tables.Count > 0)
        {
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                cblScheme.Items.Clear();
                cblScheme.DataTextField = "SCHEMENAME";
                cblScheme.DataValueField = "SCHEMENO";
                cblScheme.DataSource = dsScheme.Tables[0];
                cblScheme.DataBind();
                //divScheme.Visible = true;
                Session["dsScheme"] = dsScheme.Tables[0];

                int i = 0;
                int compare = 0;
                foreach (ListItem item in cblScheme.Items)
                {
                    if (dsScheme.Tables[0].Rows[i]["SCHEMENO2"].ToString() == item.Value.Split('$')[2] && Convert.ToInt32(item.Value.Split('$')[2]) > 0)
                    {
                        count++;
                        item.Selected = true;
                        schemeno = schemeno + item.Value.Split('$')[2] + ",";
                        degreebranchscheme = degreebranchscheme + item.Value + ",";
                        schemename = schemename + item.Text + "$";

                        if (dsScheme.Tables[0].Rows[i]["SCHEMENAME"].ToString() == string.Empty)
                        {
                            compare++;
                            item.Attributes.Add("style", "display:none");
                        }
                    }
                    else
                    {
                        item.Selected = false;
                    }
                    i++;
                }

                if (compare == dsScheme.Tables[0].Rows.Count)
                {
                    divScheme.Visible = false;
                }
                else
                {
                    divScheme.Visible = true;
                }

                if (ViewState["degreeno"].ToString().Length > 0)
                {
                    if (ViewState["branchno"].ToString().Length > 0)
                    {
                        if (schemeno.Length > 0)
                        {
                            if (schemeno.Substring(schemeno.Length - 1).Contains(','))
                            {
                                schemeno = schemeno.Remove(schemeno.Length - 1);
                            }
                            if (schemename.Substring(schemename.Length - 1).Contains('$'))
                            {
                                schemename = schemename.Remove(schemename.Length - 1);
                            }
                            if (degreebranchscheme.Substring(degreebranchscheme.Length - 1).Contains(','))
                            {
                                degreebranchscheme = degreebranchscheme.Remove(degreebranchscheme.Length - 1);
                            }

                            ViewState["schemeno"] = schemeno;
                            ViewState["schemename"] = schemename;
                            ViewState["degreebranchscheme"] = degreebranchscheme;
                            divScheme.Visible = true;
                        }

                        if (count > 0)
                        {
                            btnSubmit1.Enabled = true;
                        }
                        else
                        {
                            btnSubmit1.Enabled = false;
                        }
                    }
                    else
                    {
                        divScheme.Visible = false;
                    }
                }
                else
                {
                    divBranch.Visible = false;
                    divScheme.Visible = false;
                }

            }
            else
            {
                cblScheme.DataSource = null;
                cblScheme.DataBind();
                divScheme.Visible = false;
                objCommon.DisplayMessage(updPnl, "Scheme Not Created", this.Page);
              //  btnSubmit1.Enabled = true;

            }
        }
    }
    protected void cblDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        divScheme.Visible = false;
        cblScheme.Items.Clear();
        btnSubmit1.Enabled = false;

        foreach (ListItem item in cblDegree.Items)
        {
            if (item.Selected)
            {
                degreeno = degreeno + item.Value + ",";
            }
        }

        if (degreeno != null)
        {
            if (degreeno.Length > 0)
            {
                if (degreeno.Substring(degreeno.Length - 1).Contains(','))
                {
                    degreeno = degreeno.Remove(degreeno.Length - 1);
                }
                ViewState["degreeno"] = degreeno.ToString();
                fillChecboxlistBranch(degreeno);
            }
            else
            {
                divBranch.Visible = false;
                divScheme.Visible = false;
            }
        }
        else
        {
            divBranch.Visible = false;
            divScheme.Visible = false;
        }

        if (cblScheme.Items.Count <= 0)
        {
            if (cblBranch.Items.Count > 0)
            {
                foreach (ListItem item in cblDegree.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                    }
                }
            }
        }


        if (count > 0)
        {
            btnSubmit1.Enabled = true;
        }
        else
        {
            btnSubmit1.Enabled = false;
        }
    }

    protected void cblBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (degreeno == string.Empty || degreeno == null)
        {
            degreeno = ViewState["degreeno"].ToString();
        }

        foreach (ListItem item in cblBranch.Items)
        {
            if (item.Selected)
            {
                branchno = branchno + item.Value.Split('$')[1] + ",";
                degreebranch = degreebranch + item.Value + ",";
                branchname = branchname + item.Text + "$";
            }
        }

        if (branchno == null)
        {
            divScheme.Visible = false;
        }

        if (degreeno.Length > 0)
        {
            if (branchno != null && branchno.Length > 0)
            {
                if (branchno.Substring(branchno.Length - 1).Contains(','))
                {
                    branchno = branchno.Remove(branchno.Length - 1);
                }

                if (degreebranch == null)
                {
                    degreebranch = ViewState["degreebranch"].ToString();
                }
                if (degreebranch.Substring(degreebranch.Length - 1).Contains(','))
                {
                    degreebranch = degreebranch.Remove(degreebranch.Length - 1);
                }

                if (branchname == null)
                {
                    branchname = ViewState["branchname"].ToString();
                }
                if (branchname.Substring(branchname.Length - 1).Contains('$'))
                {
                    branchname = branchname.Remove(branchname.Length - 1);
                }
                ViewState["branchno"] = branchno;
                ViewState["degreebranch"] = degreebranch;
                ViewState["branchname"] = branchname;
                fillChecboxlistScheme(degreeno, branchno);
            }
            else
            {
                divScheme.Visible = false;
            }
        }
        else
        {
            divBranch.Visible = false;
            divScheme.Visible = false;
        }

        int count = 0;

        foreach (ListItem item in cblScheme.Items)
        {
            if (item.Selected)
            {
                count++;
            }
        }

        if (count > 0)
        {
            btnSubmit1.Enabled = true;
        }
        else
        {
            btnSubmit1.Enabled = false;
        }

        DataSet dsScheme = null;
        if (branchno != null && degreeno != null)
        {
            if (branchno == string.Empty)
            {
                dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO)", "(CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(S.SCHEMENO AS NVARCHAR(20))) AS SCHEMENO, S.SCHEMENO AS SCHEMENO1", "S.SCHEMENAME", "ISNULL(S.SCHEMENO,0)>0 AND CDB.COLLEGE_ID=1 AND CDB.DEGREENO IN(" + degreeno + ")", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");
            }
            else
            {
                dsScheme = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON (S.BRANCHNO=CDB.BRANCHNO AND S.DEGREENO=CDB.DEGREENO)", "(CAST(CDB.DEGREENO AS NVARCHAR(20))+'$'+CAST(CDB.BRANCHNO AS NVARCHAR(20))+'$'+CAST(S.SCHEMENO AS NVARCHAR(20))) AS SCHEMENO, S.SCHEMENO AS SCHEMENO1", "S.SCHEMENAME", "ISNULL(S.SCHEMENO,0)>0 AND CDB.COLLEGE_ID=1 AND CDB.DEGREENO IN(" + degreeno + ") AND CDB.BRANCHNO IN(" + branchno + ")", "CDB.COLLEGE_ID, CDB.DEGREENO, CDB.BRANCHNO");
            }

            if (dsScheme.Tables.Count > 0)
            {
                if (dsScheme.Tables[0].Rows.Count <= 0)
                {
                    btnSubmit1.Enabled = true;
                }
                else
                {
                    btnSubmit1.Enabled = false;
                }
            }
        }

        int countSelectedBranch = 0;
        int countSelectedScheme=0;

        if (cblScheme.Items.Count <= 0)
        {
            if (cblBranch.Items.Count > 0)
            {
                foreach (ListItem item in cblDegree.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                    }
                }

                foreach (ListItem item in cblBranch.Items)
                {
                    if (item.Selected)
                    {
                        countSelectedBranch++;
                    }
                }
                  foreach (ListItem item in cblScheme.Items)
                {
                    if (item.Selected)
                    {
                        countSelectedScheme++;
                    }
                }
            }
        }

        if (count > 0 && countSelectedBranch > 0 && countSelectedScheme>0)
        {
            btnSubmit1.Enabled = true;
        }
        else
        {
            btnSubmit1.Enabled = false;
        }
    }
    protected void cblScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        count = 0;

        foreach (ListItem item in cblScheme.Items)
        {
            if (item.Selected)
            {
                count++;
                schemeno = schemeno + item.Value.Split('$')[2] + ",";
                degreebranchscheme = degreebranchscheme + item.Value + ",";
                schemename = schemename + item.Text + "$";

                if (item.Text == string.Empty)
                {
                    item.Attributes.Add("style", "display:none");
                }
            }
        }

        if (schemeno == null || schemeno == "")
        {
            btnSubmit1.Enabled = false;
            return;
        }

        if (schemeno.Length > 0)
        {
            if (schemeno.Substring(schemeno.Length - 1).Contains(','))
            {
                schemeno = schemeno.Remove(schemeno.Length - 1);
            }
            if (schemename.Substring(schemename.Length - 1).Contains('$'))
            {
                schemename = schemename.Remove(schemename.Length - 1);
            }
            if (degreebranchscheme.Substring(degreebranchscheme.Length - 1).Contains(','))
            {
                degreebranchscheme = degreebranchscheme.Remove(degreebranchscheme.Length - 1);
            }

            ViewState["schemeno"] = schemeno;
            ViewState["schemename"] = schemename;
            ViewState["degreebranchscheme"] = degreebranchscheme;
        }

        if (count > 0)
        {
            btnSubmit1.Enabled = true;
        }
        else
        {
            btnSubmit1.Enabled = false;
        }
    }
    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        DataTable dsScheme = (DataTable)Session["dsScheme"];
        DataTable dt11 = (DataTable)dsScheme;

        DataTable dtResult = new DataTable();
        dtResult.Columns.AddRange(new DataColumn[6] 
        { 
            new DataColumn("COLLEGE_ID", typeof(int)),
            new DataColumn("COL_SCHEME_NAME", typeof(string)),
            new DataColumn("DEGREENO",typeof(int)),
            new DataColumn("BRANCHNO",typeof(int)), 
            new DataColumn("SCHEMENO",typeof(int)), 
            new DataColumn("CREATED_BY",typeof(int)) 
        });

        CustomStatus cs = 0;
        int degreeno = 0;
        int branchno = 0;
        int schemeno = 0;

        string schemenames = string.Empty;
        string[] schemenamevalues = schemenames.Split('$');

        string degreebranches = string.Empty;
        string degreebrancheschemes = string.Empty;

        string degreebranch = ViewState["degreebranch"].ToString();
        string degreebranchscheme = ViewState["degreebranchscheme"].ToString();

        string degree = ViewState["degreeno"].ToString();
        string[] degreevalues = degree.Split(',');

        string branch = ViewState["branchno"].ToString();
        string[] branchvalues = branch.Split(',');

        string scheme = ViewState["schemeno"].ToString();
        string[] schemevalues = scheme.Split(',');

        string branchnames = ViewState["branchname"].ToString();
        string[] branchnamevalues = branchnames.Split('$');

        if (degreebranchscheme != string.Empty)
        {
            schemenames = ViewState["schemename"].ToString();
            schemenamevalues = schemenames.Split('$');
        }

        for (int i = 0; i < degreevalues.Length; i++)
        {
            degreeno = 0;
            degreevalues[i] = degreevalues[i].Trim();
            degreeno = Convert.ToInt32(degreevalues[i].ToString());

            for (int j = 0; j < branchvalues.Length; j++)
            {
                branchno = 0;
                degreeOfBranch = string.Empty;

                branchvalues[j] = branchvalues[j].Trim();
                branchno = Convert.ToInt32(branchvalues[j].ToString());

                branchname = branchnames.ToString();
                branchnamevalues[j] = branchnamevalues[j].Trim();
                branchname = branchnamevalues[j].ToString();

                degreebranches = string.Empty;
                string[] db = degreebranch.Split(',');
                degreebranches = db[j].ToString();

                degreeOfBranch = degreebranches.Split('$')[0].Trim();

                for (int k = 0; k < schemevalues.Length; k++)
                {
                    schemeno = 0;
                    schemevalues[k] = schemevalues[k].Trim();
                    schemeno = Convert.ToInt32(schemevalues[k].ToString() == string.Empty ? "0" : schemevalues[k].ToString());

                    schemename = string.Empty;
                    schemenamevalues[k] = schemenamevalues[k].Trim();
                    schemename = schemenamevalues[k].ToString();

                    degreebrancheschemes = string.Empty;
                    string[] dbs = degreebranchscheme.Split(',');
                    degreebrancheschemes = dbs[k].ToString();

                    if (degreebrancheschemes != string.Empty)
                    {
                        branchOfScheme = degreebrancheschemes.Split('$')[1].Trim();
                    }

                    //string colscheme = (ddlCollege.SelectedItem.Text + " - " + (schemename.ToString()==string.Empty?branchname.ToString():schemename.ToString()));

                    /////////////********* ADDED BY NARESH BEERLA TO SAVE SCHEME WITH COLLEGE SHORT NAME ON 29122020 (ANKUSH SIR) *********//////////////////

                    string ShortColName = objCommon.LookUp("ACD_COLLEGE_MASTER", "SHORT_NAME", "COLLEGE_ID=" + ddlCollege1.SelectedValue);

                    string colscheme = (ShortColName + " - " + (schemename.ToString() == string.Empty ? branchname.ToString() : schemename.ToString()));

                    /////////////********* ADDED BY NARESH BEERLA TO SAVE SCHEME WITH COLLEGE SHORT NAME ON 29122020 (ANKUSH SIR) *********//////////////////

                    if (Convert.ToString(degreeno) == degreeOfBranch)
                    {
                        if (Convert.ToString(branchno) == (branchOfScheme == string.Empty ? Convert.ToString(branchno) : branchOfScheme))
                        {
                            string _sqlWh = "DEGREENO=" + degreeno.ToString() + " AND BRANCHNO=" + branchno.ToString() + " AND SCHEMENO1=" + schemeno.ToString();
                            string _sqlWhere = _sqlWh.ToString();
                            string _sqlOrder = "degreeno";

                            DataRow[] filtered_rows = dt11.Select(_sqlWhere, _sqlOrder);
                            if (filtered_rows.Length > 0)
                            {
                                DataTable dr1 = dt11.Select(_sqlWhere, _sqlOrder).CopyToDataTable();

                                if (dr1.Rows.Count > 0)
                                {
                                    dtResult.Rows.Add(Convert.ToInt32(ddlCollege1.SelectedValue), colscheme, degreeno, branchno, schemeno, Convert.ToInt32(Session["userno"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }

        cs = (CustomStatus)objConfig.AddCollegeSchemeConfig(dtResult);

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(updPnl, "Record Saved Successfully!!", this.Page);
            Clear();
        }

        objCommon.DisplayMessage(updPnl, "Record Saved Successfully!!", this.Page);
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ddlCollege1.SelectedIndex = 0;
        cblDegree.Items.Clear();
        cblBranch.Items.Clear();
        cblScheme.Items.Clear();
        divDegree.Visible = false;
        divBranch.Visible = false;
        divScheme.Visible = false;
        btnSubmit1.Enabled = false;

    }
    #endregion

    // Added by Jay Takalkhede on date 16/09/2022
    protected void btnPrintAllotment_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            DataSet ds = null;

            DataSet dsfee = objSC.ExcelForSchemeAllotPending();
            if (dsfee.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsfee;
                GV.DataBind();
                string attachment = "attachment; filename=SchemeAllotmentPendingStudent.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    // added by jay takalkhede on dataed 21/11/2022
    protected void ddlBranchForScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranchForScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + ddlDegree.SelectedValue + " and BRANCHNO=" + ddlBranchForScheme.SelectedValue, "SCHEMENO");
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}








