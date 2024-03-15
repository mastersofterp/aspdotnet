//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Bulk semester promotion                                    
// CREATION DATE : 15-OCT-2016
// CREATED BY    :                                                 
// MODIFIED DATE : 18-06-2019
// MODIFIED BY   : M. REHBAR SHEIKH
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
using System.Text;
using System.IO;

public partial class ACADEMIC_BulkSemesterPromotion : System.Web.UI.Page
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
                LVStudentDetails.Visible = false;
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -
            btnSave.Visible = false;
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            else
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO desc");

        }
        catch (Exception ex)
        {
            throw;
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
            // ADDED BY: M. REHBAR SHEIKH ON 19-06-2019
            StudentController objSC = new StudentController();
            Student objS = new Student();
            bool flag = false;
            foreach (ListViewDataItem item in lvStudent.Items)
            {

                Label lblregno = item.FindControl("lblregno") as Label;
                Label lblstudname = item.FindControl("lblstudname") as Label;
                CheckBox chksub = item.FindControl("chkRegister") as CheckBox;
                if (chksub.Checked == true && chksub.Enabled == true)
                {
                    flag = true;
                    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    objS.IdNo = Convert.ToInt32(lblregno.ToolTip);
                    objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objS.SectionNo = Convert.ToInt32(lblstudname.ToolTip);
                    objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                    objS.CollegeCode = Session["colcode"].ToString();
                    objS.Dob = DateTime.Now;
                    int yearID = Convert.ToInt32(ddlAcdYear.SelectedValue);
                    if (!lblregno.Text.Trim().Equals(string.Empty)) objS.RollNo = lblregno.Text.Trim();
                    string ipAddress = Request.ServerVariables["REMOTE_HOST"];

                    CustomStatus cs = (CustomStatus)objSC.bulkStudentSemPromo(objS, sessionno, ipAddress, yearID);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.upddetails, "Semester Promoted successfully", this.Page);
                        btnShow_Click(sender, e);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.upddetails, "Error", this.Page);
                        return;
                    }
                }
            }

            if (flag == false)
            {
                objCommon.DisplayMessage(this.upddetails, "Please select atleast single student", this.Page);
                return;
            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");

            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
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
            throw;
        }

    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK), ACD_COLLEGE_DEGREE C WITH (NOLOCK), ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue, "DEGREENO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        // ADDED BY: M. REHBAR SHEIKH ON 19-06-2019
        int DURATION = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH WITH (NOLOCK)", "ISNULL(DURATION,0)DURATION", "DEGREENO='" + Convert.ToInt32(ViewState["degreeno"]) + "' AND BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ""));//AAYUSHI  ADDED >= '2018-08-02 00:00:00.000' 
        int semcheck = DURATION * 2;
        if (Convert.ToInt32(ddlSemester.SelectedValue) >= semcheck)
        {
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:functionConfirm(); ", true);
            ddlSemester.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
        else
        {
            // DataSet dsshow = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_STUDENT_RESULT B ON(A.IDNO=B.IDNO) LEFT OUTER JOIN ACD_SEM_PROMOTION C ON(A.IDNO = C.IDNO AND B.SEMESTERNO=C.SEMESTERNO)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,b.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "", "b.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "and A.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "and A.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "and A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "and b.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "a.idno");
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetBulkSemesterPromotionData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["college_id"]));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(Session["OrgId"]) == 19 || Convert.ToInt32(Session["OrgId"]) == 20)
                    {
                        LVStudentDetails.DataSource = ds;
                        LVStudentDetails.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), LVStudentDetails);//Set label -
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                        hftot.Value = LVStudentDetails.Items.Count.ToString();
                    }
                    else
                    {
                        LVStudentDetails.Visible = false;
                        lvStudent.DataSource = ds;
                        lvStudent.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
                        btnSave.Visible = true;
                        btnSave.Enabled = true;
                        hftot.Value = lvStudent.Items.Count.ToString();
                    }

                }
                else
                {
                    //btnSave.Visible = false;
                    btnSave.Enabled = false;
                    LVStudentDetails.Visible = false;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }
            }
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
        objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM ", "DISTINCT (SM.SESSIONNO)", "SM.SESSION_PNAME", "SM.SESSIONNO > 0 AND SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SM.SESSIONNO");

    }

    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //******ADDED BY: M. REHBAR SHEIKH ON 18-06-2019*****
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                if (Convert.ToInt32(dr["PROMOTED_SEM"]) == 0)
                {
                    ((CheckBox)e.Item.FindControl("chkRegister")).Enabled = true;
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("chkRegister")).Enabled = false;
                }

                if (dr["STATUSNO"].ToString() == "YES")
                {
                    //((Label)e.Item.FindControl("lblstatus")).BackColor = System.Drawing.Color.Green;
                    ((Label)e.Item.FindControl("lblstatus")).ForeColor = System.Drawing.Color.Green;
                    ((Label)e.Item.FindControl("lblstatus")).Font.Bold = true;
                }
                else
                {
                    //((Label)e.Item.FindControl("lblstatus")).BackColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblstatus")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblstatus")).Font.Bold = true;
                    ((CheckBox)e.Item.FindControl("chkRegister")).Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnPromotedList_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        StudentController objSC = new StudentController();
        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        ds = objSC.GetPromotedStudentListForExcel(Sessionno);
        GridView gvPromoted = new GridView();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPromoted.DataSource = ds;
                gvPromoted.DataBind();

                string Attachment = "Attachment;FileName=PromotedStudentList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvPromoted.HeaderStyle.Font.Bold = true;
                gvPromoted.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.upddetails, "No Data Found", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.upddetails, "No Data Found", this.Page);
        }
    }
    protected void btnNotPromotedList_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = objSC.GetNotPromotedStudentListForExcel(Sessionno);
        GridView gvNotPromoted = new GridView();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvNotPromoted.DataSource = ds;
                gvNotPromoted.DataBind();

                string Attachment = "Attachment; FileName=NotPromotedStudentList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvNotPromoted.HeaderStyle.Font.Bold = true;
                gvNotPromoted.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.upddetails, "No Data Found", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.upddetails, "No Data Found", this.Page);
        }
    }
    protected void btnNotPromotedReport_Click(object sender, EventArgs e)
    {
        ShowReport("Not_Promoted_Students_List", "NotPromotedStudentList.rpt");
    }
    protected void btnPromotedReport_Click(object sender, EventArgs e)
    {
        ShowReport("Promoted_Students_List", "PromotedStudentList.rpt");
    }
    public void ShowReport(string filename, string rptname)
    {
        int semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        int schemeno = Convert.ToInt32(ViewState["schemeno"]) > 0 ? Convert.ToInt32(ViewState["schemeno"]) : 0;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + filename;
        url += "&path=~,Reports,Academic," + rptname;
        // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
            ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SEMESTERNO=" + semesterno + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.upddetails, this.upddetails.GetType(), "controlJSScript", sb.ToString(), true);
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                ddlSemester.Focus();
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN  ACD_STUDENT_RESULT SR ON SR.SESSIONNO= SM.SESSIONNO ", "DISTINCT (SR.SESSIONNO)", "SM.SESSION_NAME", "SR.SESSIONNO > 0 AND SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SR.SESSIONNO");

                ddlSession.Focus();
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}
