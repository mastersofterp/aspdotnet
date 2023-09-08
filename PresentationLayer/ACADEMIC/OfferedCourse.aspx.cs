using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_OfferedCourse : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    if (Session["usertype"].ToString() == "1")
                    {
                        divDegree.Visible = true;
                        divBranch.Visible = true;
                    }
                    else
                    {
                        divDegree.Visible = false;
                        divBranch.Visible = false;
                    }
                    PopulateDropDownList1();
                    PopulateDropDownList();
                    objCommon.FillDropDownList(ddlSessionIntake, "ACD_SESSION", "SESSIONID", "SESSION_NAME", " SESSIONID > 0 AND ISNULL(IS_ACTIVE,0)= 1 ", "SESSIONID DESC");
                                      
                    btnPrint.Visible = false;
                    btnAd.Visible = false;
                    btnSubmitUnoffered.Visible = false;
                    btnCancel.Visible = false;
                    btnExcel.Visible = false;
                    string host = Dns.GetHostName();
                    IPHostEntry ip = Dns.GetHostEntry(host);
                    string IPADDRESS = string.Empty;
                    IPADDRESS = ip.AddressList[0].ToString();
                    ViewState["ipAddress"] = IPADDRESS;
                }
            }
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
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OfferedCourse.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
        }
        else
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        }

        if (Session["usertype"].ToString() != "1") // prog co-ordinator / faculty
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND DEPTNO IN(" + Session["userdeptno"] + ")", "SCHEMENO");
        }
        else
        {
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO");
            }
        }
    }

    private void PopulateDropDownList1()
    {
        string deptnos = string.IsNullOrEmpty(Session["userdeptno"].ToString()) ? "0" : Session["userdeptno"].ToString();
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlCollegeScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
        }
        else
        {
            objCommon.FillDropDownList(ddlCollegeScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        }

       
    }
    #endregion

    #region  Offered Courses
    protected void btnAd_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string sem = "";

        string sqno = string.Empty;
        try
        {
            int SchemeNo = Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"]));
            int ua_no = Convert.ToInt32(Session["userno"]);
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            int Degreeno = 0;
            Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int courseno = 0;
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
                TextBox txtseqno = dataitem.FindControl("txtseqno") as TextBox;
                Label LblSemNo = dataitem.FindControl("LblSemNo") as Label;
                ListBox lstSemester = dataitem.FindControl("lstbxSemester") as ListBox;
                if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    //objCourse.Offered. += chkBox.ToolTip + ",";
                    if (txtseqno.Text == "" || txtseqno.Text == "0")
                    {
                        objCommon.DisplayMessage(this.updpnl, "Please Enter Seq.No", this.Page);
                        goto noresult;
                    }

                    offcourse = Convert.ToString(LblSemNo.Text);
                    courseno = Convert.ToInt32(chkBox.ToolTip);
                    //sem += ddl.SelectedValue + ",";
                    sqno = txtseqno.Text;
                    for (int i = 0; i < lstSemester.Items.Count; i++)
                    {
                        if (lstSemester.Items[i].Selected == true)
                        {
                            sem += lstSemester.Items[i].Value + ",";
                        }
                    }

                    if (sem == "")
                    {
                        objCommon.DisplayMessage(this.updpnl, "Please Select atleast one Semetser for Course - " + offcourse, this.Page);
                        return;
                    }
                    CustomStatus cs = (CustomStatus)objCC.UpdateOfferedCourseSemesterwise(SchemeNo, courseno, sem, sqno, ua_no, ViewState["ipAddress"].ToString(), Sessionno, Degreeno);

                    sem = "";

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        //lblStatus.Text = "Offered Courses saved Successfully";
                        objCommon.DisplayMessage(this.updpnl, "Offered Courses saved Successfully.", this.Page);
                    }
                    else
                    {
                        //lblStatus.Text = "Error in Saving";
                        objCommon.DisplayMessage(this.updpnl, "Error in Saving", this.Page);
                    }
                }
            }

            //CustomStatus cs = (CustomStatus)objCC.UpdateOfferedCourse(SchemeNo, offcourse, sem, sqno, ua_no, ViewState["ipAddress"].ToString(), Sessionno, Degreeno);

            //if (cs.Equals(CustomStatus.RecordUpdated))
        //{
        //    BindListView();
        //    //lblStatus.Text = "Offered Courses saved Successfully";
        //    objCommon.DisplayMessage(this.updpnl, "Offered Courses saved Successfully.", this.Page);
        //}
        //else
        //{
        //    //lblStatus.Text = "Error in Saving";
        //    objCommon.DisplayMessage(this.updpnl, "Error in Saving", this.Page);
        //}
        //cancel();
        noresult:
            { }
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
            divCourseDetail.Visible = false;
            int schemeno = int.Parse(ViewState["schemeno"].ToString());

            DataSet dsfaculty = null;
            string semesternos = string.Empty;
            foreach (ListItem items in ddlSemester.Items)
            {
                if (items.Selected == true)
                {
                    semesternos += items.Value + ',';
                }

            }

            dsfaculty = objCC.GetCourseOfferedWithSemester(schemeno, Convert.ToInt32(ddlSession.SelectedValue), semesternos);
            ViewState["DsFaculty"] = dsfaculty;
            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsfaculty;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label - 
                pnlCourse.Visible = true;
                divCourseDetail.Visible = true;
                hftotN.Value = dsfaculty.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
                btnAd.Visible = false;
                btnPrint.Visible = false;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindOfferedListView()
    {
        try
        {
            divCourseDetail.Visible = false;
            int schemeno = int.Parse(ViewState["schemeno"].ToString());

            DataSet dscourses = null;
            string semesternos = string.Empty;
            foreach (ListItem items in ddlSemester.Items)
            {
                if (items.Selected == true)
                {
                    semesternos += items.Value + ',';
                }

            }

            dscourses = objCC.GetViewOfferedCourseWithSemester(schemeno, Convert.ToInt32(ddlSession.SelectedValue), semesternos);
            if (dscourses != null && dscourses.Tables.Count > 0 && dscourses.Tables[0].Rows.Count > 0)
            {
                lvOfferdCourse.DataSource = dscourses;
                lvOfferdCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvOfferdCourse);//Set label - 
                pnlOfferedCourse.Visible = true;
                divOfferdCourseDetails.Visible = true;
            }
            else
            {
                lvOfferdCourse.DataSource = null;
                lvOfferdCourse.DataBind();
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
                btnSubmitUnoffered.Visible = false;
                btnPrint.Visible = false;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
        Label lblSem = dataitem.FindControl("LblSemNo") as Label;
        CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
        objCommon.FillDropDownList(ddl, "ACD_SEMESTER ", "distinct SEMESTERNO", "SEMESTERNAME", string.Empty, "SEMESTERNO");
        //objCommon.FillDropDownList(ddl, "ACD_SEMESTER S  inner join ACD_COURSE C on(S.SEMESTERNO =C.SEMESTERNO)", "S.SEMESTERNO", "S.SEMESTERNAME", "C.SCHEMENO="+ddlScheme.SelectedValue+"","");
        ddl.SelectedValue = lblSem.Text;
        ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;
        DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLSEMESTER");
        ListBox lstbxSemester = e.Item.FindControl("lstbxSemester") as ListBox;
        lstbxSemester.DataValueField = "SEMESTERNO";
        lstbxSemester.DataTextField = "SEMESTERNAME";
        lstbxSemester.DataSource = ds.Tables[0];
        lstbxSemester.DataBind();


        DataSet dsGetSemester = objsql.ExecuteDataSet("select S.SEMESTERNO,S.SEMESTERNAME from ACD_OFFERED_COURSE O INNER JOIN ACD_SEMESTER S on O.SEMESTERNO=S.SEMESTERNO WHERE O.COURSENO =" + ChkOffer.ToolTip + "AND SESSIONNO =" + ddlSession.SelectedValue + "AND SCHEMENO =" + int.Parse(ViewState["schemeno"].ToString()) + "AND ISNULL(COURSE_OFFERED,0)=1");

        // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
        for (int i = 0; i < dsGetSemester.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < lstbxSemester.Items.Count; j++)
            {
                if (lstbxSemester.Items[j].Selected == true)
                {
                    lstbxSemester.Items[j].Selected = true;
                }
                if (lstbxSemester.Items[j].Value.ToString() == dsGetSemester.Tables[0].Rows[i]["SEMESTERNO"].ToString())
                {
                    lstbxSemester.Items[j].Selected = true;

                }

            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //cancel();
        Response.Redirect(Request.Url.ToString());
        // ddlSession.SelectedIndex = 0;
        // ddlScheme.SelectedIndex = 0;
        //// lvCourse.Visible = false;
    }
    protected void cancel()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        //Response.Redirect(Request.Url.ToString());
        lvCourse.Visible = false;
        btnAd.Visible = false;
        btnSubmitUnoffered.Visible = false;
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0")
        {
            int chkExit = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME SS WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME EN WITH (NOLOCK) ON(SS.PATTERNNO=EN.PATTERNNO)", "COUNT(*)", "SS.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "")); //Added Mahesh on Dated 09-02-2020 due to Report Required Exam name(Pattern) without Exam Name showing . 
            if (chkExit > 0)
            {
                string[] sno = ddlScheme.SelectedValue.Split('-');
                ExportExcel("Check_List");
                //int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
                //ShowReport("Check_List", "rptSubjectCourseListSchemewiseOfferedCourses.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Exam pattern not found for selected scheme, Please check exam creation & pattern.!!!", this.Page);
                return;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            objCommon.DisplayMessage(this.updpnl, "Please Select Scheme", this.Page);
            return;
        }

    }
    private void ExportExcel(string title)
    {
        string attachment = "attachment; filename=" + title + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = objCC.GetSubjectCourseList(Convert.ToInt32(ViewState["schemeno"]), 0);


        DataGrid dg = new DataGrid();

        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + 0;

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            btnAd.Visible = true;
            btnCancel.Visible = true;
            btnPrint.Visible = true;
            BindListView();
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
        }
    }

    //protected void ddlPath_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string mWhere = " PATH_NO = '" + ((ddlPath.SelectedValue).ToString()).ToUpper() + "' ";
    //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "distinct SCHEMENO", "SCHEMENAME", mWhere, "SCHEMENO");
    //}

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON DB.BRANCHNO=B.BRANCHNO", "distinct B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO=" + ddlDegree.SelectedValue + "", "B.BRANCHNO");
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + "", "SCHEMENO");
        else
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND DEPTNO=" + Session["userdeptno"] + "", "SCHEMENO");

        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlClgname.Focus();
        }
    }
    
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLSEMESTER");
            ddlSemester.DataSource = dsAcd;
            ddlSemester.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
            ddlSemester.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
            ddlSemester.DataBind();


            lvCourse.DataSource = null;
            lvCourse.DataBind();
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCC.GetAllOfferedCourseList(Convert.ToInt32(ddlSession.SelectedValue));

            GridView GV = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=AllOfferedCourses.xls";
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
                objCommon.DisplayMessage(updpnl, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_OfferedCourse.btnExcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        btnAd.Visible = true;
        btnSubmitUnoffered.Visible = false;
        btnCancel.Visible = true;
        btnPrint.Visible = true;
        btnExcel.Visible = true;
        BindListView();
        lvCourse.Visible = true;
        lvOfferdCourse.Visible = false;
        lvOfferdCourse.DataSource = null;
        lvOfferdCourse.DataBind();
    }
    protected void btnViewOfferedCourse_Click(object sender, EventArgs e)
    {
        btnSubmitUnoffered.Visible = true;
        btnAd.Visible = false;
        btnCancel.Visible = true;
        btnPrint.Visible = true;
        btnExcel.Visible = true;
        btnCopyOfferedCourse.Visible = true;
        BindOfferedListView();
        lvOfferdCourse.Visible = true;
        lvCourse.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }
    protected void lvOfferdCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
        Label lblSem = dataitem.FindControl("LblSemNo") as Label;
        CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
        objCommon.FillDropDownList(ddl, "ACD_SEMESTER ", "distinct SEMESTERNO", "SEMESTERNAME", string.Empty, "SEMESTERNO");
        //objCommon.FillDropDownList(ddl, "ACD_SEMESTER S  inner join ACD_COURSE C on(S.SEMESTERNO =C.SEMESTERNO)", "S.SEMESTERNO", "S.SEMESTERNAME", "C.SCHEMENO="+ddlScheme.SelectedValue+"","");
        ddl.SelectedValue = lblSem.Text;
        //ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;
        DataSet ds = objsql.ExecuteDataSet("select S.SEMESTERNO,S.SEMESTERNAME from ACD_OFFERED_COURSE O INNER JOIN ACD_SEMESTER S on O.SEMESTERNO=S.SEMESTERNO WHERE O.COURSENO =" + ChkOffer.ToolTip + "AND SESSIONNO =" + ddlSession.SelectedValue + "AND SCHEMENO =" + int.Parse(ViewState["schemeno"].ToString()) + "AND ISNULL(COURSE_OFFERED,0)=1 Order by S.SEMESTERNO");
        //DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLSEMESTER");
        ListBox lstbxSemester = e.Item.FindControl("lstbxSemester") as ListBox;
        lstbxSemester.DataValueField = "SEMESTERNO";
        lstbxSemester.DataTextField = "SEMESTERNAME";
        lstbxSemester.DataSource = ds.Tables[0];
        lstbxSemester.DataBind();


        DataSet dsGetSemester = objsql.ExecuteDataSet("select S.SEMESTERNO,S.SEMESTERNAME from ACD_OFFERED_COURSE O INNER JOIN ACD_SEMESTER S on O.SEMESTERNO=S.SEMESTERNO WHERE O.COURSENO =" + ChkOffer.ToolTip + "AND SESSIONNO =" + ddlSession.SelectedValue + "AND SCHEMENO =" + int.Parse(ViewState["schemeno"].ToString()) + "AND ISNULL(COURSE_OFFERED,0)=1");

        // DataSet dsGetADTeacher = objsql.ExecuteDataSet("select S.SECTIONNO,S.SECTIONNAME,T.IS_ADTEACHER from ACD_Course_TEACHER T inner join ACD_SECTION S on T.SECTIONNO=S.SECTIONNO where T.UA_NO=" + hdnTeacher.Value + "and T.COURSENO=" + chkBox.ToolTip);
        for (int i = 0; i < dsGetSemester.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < lstbxSemester.Items.Count; j++)
            {
                if (lstbxSemester.Items[j].Selected == true)
                {
                    lstbxSemester.Items[j].Selected = true;
                }
                if (lstbxSemester.Items[j].Value.ToString() == dsGetSemester.Tables[0].Rows[i]["SEMESTERNO"].ToString())
                {
                    lstbxSemester.Items[j].Selected = true;

                }
            }
        }
    }
    protected void btnSubmitUnoffered_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string sem = "";

        string sqno = string.Empty;
        try
        {
            int flag = 0;
            foreach (ListViewDataItem lvItem in lvOfferdCourse.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkoffered") as CheckBox;

                if (chkBox.Checked == true)
                {
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                objCommon.DisplayMessage(this, "Please select atleast one course to un-offered!", this.Page);
                return;
            }

            int SchemeNo = Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"]));
            int ua_no = Convert.ToInt32(Session["userno"]);
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            int Degreeno = 0;
            Degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int courseno = 0;
            foreach (ListViewDataItem dataitem in lvOfferdCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;

                Label LblSemNo = dataitem.FindControl("LblSemNo") as Label;
                ListBox lstSemester = dataitem.FindControl("lstbxSemester") as ListBox;
                if (chkBox.Checked == true && chkBox.Enabled == true)
                {

                    offcourse = Convert.ToString(LblSemNo.Text);
                    courseno = Convert.ToInt32(chkBox.ToolTip);
                    //sem += ddl.SelectedValue + ",";

                    for (int i = 0; i < lstSemester.Items.Count; i++)
                    {
                        if (lstSemester.Items[i].Selected == true)
                        {
                            sem += lstSemester.Items[i].Value + ",";
                        }
                    }

                    CustomStatus cs = (CustomStatus)objCC.UpdateUnOfferedCourseSemesterwise(SchemeNo, courseno, sem, ua_no, ViewState["ipAddress"].ToString(), Sessionno, Degreeno);

                    sem = "";

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindOfferedListView();
                        //lblStatus.Text = "Offered Courses saved Successfully";
                        objCommon.DisplayMessage(this.updpnl, "Courses Un-Offered Successfully.", this.Page);
                    }
                    else
                    {
                        //lblStatus.Text = "Error in Saving";
                        objCommon.DisplayMessage(this.updpnl, "Error in Un-Offered", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_OfferedCourse.btnSubmitUnoffered_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Elective Course Intake

    protected void ddlSessionIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvElectIntakeCapacity.DataSource = null;
        lvElectIntakeCapacity.DataBind();
        if (Convert.ToInt32(ddlSessionIntake.SelectedValue) > 0)
        {
            lboSemesterIntake.Items.Clear();
            DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLSEMESTER");
            lboSemesterIntake.DataSource = dsAcd;
            lboSemesterIntake.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
            lboSemesterIntake.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
            lboSemesterIntake.DataBind();            
        }        
    }

    protected void btnShowElectIntake_Click(object sender, EventArgs e)
    {
        this.BindListViewElectiveIntake();
    }

    protected void btnCancelIntake_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void BindListViewElectiveIntake()
    {
        try
        {
            DataSet dsintake = null;
            string semesternos = string.Empty;
            foreach (ListItem items in lboSemesterIntake.Items)
            {
                if (items.Selected == true)
                {
                    semesternos += items.Value + ',';
                }

            }

            dsintake = objCC.GetCourseForElectiveIntakeCapicity(Convert.ToInt32(ddlSessionIntake.SelectedValue), semesternos);
            if (dsintake != null && dsintake.Tables.Count > 0 && dsintake.Tables[0].Rows.Count > 0)
            {
                lvElectIntakeCapacity.DataSource = dsintake;
                lvElectIntakeCapacity.DataBind();
                hftot.Value = dsintake.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvElectIntakeCapacity.DataSource = null;
                lvElectIntakeCapacity.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmitIntake_Click(object sender, EventArgs e)
    {
        int DegreeNo =0;
        int SessionNo = 0;
        int SchemeNo = 0;
        int SemesterNo = 0;
        int CourseNo = 0;
        int count = 0;
        CustomStatus cs = 0;
        try
        {
            int capacity1 = 0;
            foreach (ListViewDataItem dataitem in lvElectIntakeCapacity.Items)
            {
                CheckBox cbRow = dataitem.FindControl("ChkElectIntake") as CheckBox;

                if (cbRow.Checked == true)
                {                    
                    TextBox txtIntakeCapacity = dataitem.FindControl("txtIntakeCapacity") as TextBox;
                    if (string.IsNullOrEmpty(txtIntakeCapacity.Text))
                        capacity1++;
                }
            }

            if (capacity1 > 0)
            {
                objCommon.DisplayMessage(this.Page, "Please fill capacity for selected course.", this.Page);
                return;
            }

            foreach (ListViewDataItem dataitem in lvElectIntakeCapacity.Items)
            {
                CheckBox cbRow = dataitem.FindControl("ChkElectIntake") as CheckBox;

                if (cbRow.Checked == true)
                {                    
                    HiddenField hfdDegreeNo = dataitem.FindControl("hfdDegreeNo") as HiddenField;
                    HiddenField hfdsessionNo = dataitem.FindControl("hfdsessionNo") as HiddenField;
                    HiddenField hfdSchemeno = dataitem.FindControl("hfdSchemeno") as HiddenField;
                    HiddenField hfdSemesterNo = dataitem.FindControl("hfdSemesterNo") as HiddenField;
                    TextBox txtIntakeCapacity = dataitem.FindControl("txtIntakeCapacity") as TextBox;
                    TextBox txtEligibility = dataitem.FindControl("txtEligibility") as TextBox;
                    DegreeNo = Convert.ToInt32(hfdDegreeNo.Value);
                    SessionNo = Convert.ToInt32(hfdsessionNo.Value);
                    SchemeNo = Convert.ToInt32(hfdSchemeno.Value);
                    SemesterNo = Convert.ToInt32(hfdSemesterNo.Value);
                    CourseNo = Convert.ToInt32(cbRow.ToolTip.ToString());
                    int capacity = Convert.ToInt32(txtIntakeCapacity.Text);
                    double eligibilityCGPA = !string.IsNullOrEmpty(txtEligibility.Text) ? Convert.ToDouble(txtEligibility.Text) : 0;
                    int ua_no = Convert.ToInt32(Session["userno"]);
                    cs = (CustomStatus)objCC.UpdateOfferedElectiveCourseIntakeCapacity(SessionNo, SemesterNo, capacity, SchemeNo, CourseNo, ua_no,
                        ViewState["ipAddress"].ToString(), eligibilityCGPA);
                    count++;
                    //ViewState["cs"] = cs;
                }

            }
            //ViewState["cs"] = cs;
            if (count > 0)
            {
                BindListViewElectiveIntake();
                objCommon.DisplayMessage(this.Page, "Course Capacity Saved Successfully", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion 

    protected void btnCopyOfferedCourse_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string sqno = string.Empty;
        try
        {
            int SchemeNo = Convert.ToInt32(Convert.ToInt32(ViewState["schemenoCopyOfferCrs"]));
            int ua_no = Convert.ToInt32(Session["userno"]);
            int Sessionno = Convert.ToInt32(ddlSessionCopyCrsTo.SelectedValue);

            int Degreeno = 0;
            Degreeno = Convert.ToInt32(ViewState["degreenoCopyOfferCrs"]);
            int courseno = 0;
            CustomStatus cs = CustomStatus.Others;
            foreach (ListViewDataItem dataitem in lvCopyOfferCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkofferedCopyOffrCrs") as CheckBox;
               // DropDownList ddl = dataitem.FindControl("ddlsem") as DropDownList;
                TextBox txtseqno = dataitem.FindControl("txtSeqNoCopyOffrCrs") as TextBox;
                Label LblSemNo = dataitem.FindControl("LblSemNoCopyOffrCrs") as Label;
                ListBox lstSemester = dataitem.FindControl("lblbxSemestertoCopyOffrCrs") as ListBox;
                if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    if (txtseqno.Text == "" || txtseqno.Text == "0")
                    {
                        objCommon.DisplayMessage(this.updpnl, "Please Enter Seq.No", this.Page);
                        goto noresult;
                    }

                    offcourse = Convert.ToString(LblSemNo.Text);
                    courseno = Convert.ToInt32(chkBox.ToolTip);                
                    sqno = txtseqno.Text;
                    string sem = "";
                    for (int i = 0; i < lstSemester.Items.Count; i++)
                    {
                        if (lstSemester.Items[i].Selected == true)
                            sem += lstSemester.Items[i].Value + ",";
                    }                    

                    if (string.IsNullOrEmpty(sem))
                    {
                        objCommon.DisplayMessage(this.updpnl, "Please Select atleast one Semetser for Course - " + offcourse, this.Page);
                        return;
                    }
                    cs =  (CustomStatus)objCC.UpdateOfferedCourseSemesterwise(SchemeNo, courseno, sem, sqno, ua_no, ViewState["ipAddress"].ToString(), Sessionno, Degreeno);
                }
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindCopyOfferedListView();
                objCommon.DisplayMessage(this.updpnl, "Offered Courses Copied Successfully.", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updpnl, "Error", this.Page);

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_OfferedCourse.btnCopyOfferedCourse_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    protected void btnViewCopyOfferedCourse_Click(object sender, EventArgs e)
    {
       // objCommon.FillDropDownList(ddlSession3, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND SESSIONNO NOT IN (" + Convert.ToInt32(ddlSession2.SelectedValue) + ")", "SESSIONNO DESC");
        BindCopyOfferedListView();
    }
    protected void ddlCollegeScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeScheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeScheme.SelectedValue));          
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["degreenoCopyOfferCrs"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchnoCopyOfferCrs"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_idCopyOfferCrs"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemenoCopyOfferCrs"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSessionCopyCrs, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_idCopyOfferCrs"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
               
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSessionCopyCrs.Items.Clear();
            ddlSessionCopyCrs.Items.Add(new ListItem("Please Select", "0"));
            ddlCollegeScheme.Focus();
        }
    }
    protected void ddlSessionCopyCrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        if (ddlSessionCopyCrs.SelectedIndex > 0)
        {
            ddlSemesterCopyCrs.Items.Clear();
            DataSet dsAcd = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLSEMESTER");
            ddlSemesterCopyCrs.DataSource = dsAcd;
            ddlSemesterCopyCrs.DataTextField = dsAcd.Tables[0].Columns[1].ToString();
            ddlSemesterCopyCrs.DataValueField = dsAcd.Tables[0].Columns[0].ToString();
            ddlSemesterCopyCrs.DataBind();
        }
    }

    private void BindCopyOfferedListView()
    {
        try
        {
            divCourseDetail.Visible = false;
            int schemeno = int.Parse(ViewState["schemenoCopyOfferCrs"].ToString());

            DataSet dscourses = null;
            string semesternos = string.Empty;
            foreach (ListItem items in ddlSemesterCopyCrs.Items)
            {
                if (items.Selected == true)
                    semesternos += items.Value + ',';
            }
            semesternos = semesternos.TrimEnd(',');
            dscourses = objCC.GetViewOfferedCourseWithSemester(schemeno, Convert.ToInt32(ddlSessionCopyCrs.SelectedValue), semesternos);
            if (dscourses != null && dscourses.Tables.Count > 0 && dscourses.Tables[0].Rows.Count > 0)
            {
                lvCopyOfferCourse.DataSource = dscourses;
                lvCopyOfferCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCopyOfferCourse);//Set label - 
                pnlCopyOfferedCourse.Visible = true;
                hfOfferedCrs.Value = dscourses.Tables[0].Rows.Count.ToString();
                objCommon.FillDropDownList(ddlSessionCopyCrsTo, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME",
                    "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_idCopyOfferCrs"])
                    + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                   // + " AND SESSIONNO!=" + Convert.ToInt32(ddlSessionCopyCrs.SelectedValue), "SESSIONNO DESC");
                dvSessionCopyCrsTo.Visible = true;
                btnCopyOfferedCourse.Visible = true;
            }
            else
            {
                hfOfferedCrs.Value = "0";
                lvOfferdCourse.DataSource = null;
                lvCopyOfferCourse.DataBind();
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);               
                btnCancel.Visible = false;
                pnlCopyOfferedCourse.Visible = false;
                dvSessionCopyCrsTo.Visible = false;
                btnCopyOfferedCourse.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvCopyOfferCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListBox lblbxSemestertoCopyOffrCrs = e.Item.FindControl("lblbxSemestertoCopyOffrCrs") as ListBox;
        DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALLSEMESTER");
        lblbxSemestertoCopyOffrCrs.DataSource = ds;
        lblbxSemestertoCopyOffrCrs.DataValueField = ds.Tables[0].Columns[0].ToString();
        lblbxSemestertoCopyOffrCrs.DataTextField = ds.Tables[0].Columns[1].ToString();        
        lblbxSemestertoCopyOffrCrs.DataBind();

        SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        CheckBox ChkOffer = dataitem.FindControl("chkofferedCopyOffrCrs") as CheckBox;

        DataSet dsGetSemester = objsql.ExecuteDataSet(@"select S.SEMESTERNO,S.SEMESTERNAME from ACD_OFFERED_COURSE O INNER JOIN ACD_SEMESTER S on O.SEMESTERNO=S.SEMESTERNO
                            WHERE O.COURSENO =" + ChkOffer.ToolTip + "AND SESSIONNO =" + ddlSessionCopyCrs.SelectedValue +
                                                "AND SCHEMENO =" + int.Parse(ViewState["schemenoCopyOfferCrs"].ToString()) + "AND ISNULL(COURSE_OFFERED,0)=1");
        
        for (int i = 0; i < dsGetSemester.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < lblbxSemestertoCopyOffrCrs.Items.Count; j++)
            {      
                if (lblbxSemestertoCopyOffrCrs.Items[j].Value.ToString() == dsGetSemester.Tables[0].Rows[i]["SEMESTERNO"].ToString())
                {
                    lblbxSemestertoCopyOffrCrs.Items[j].Selected = true;
                    break;
                }
            }
        }
    }
    protected void chkHead_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem itm in lvCourse.Items)
            {
                if (lvCourse.Items.Count > 0)
                {
                    CheckBox chkH = (CheckBox)itm.FindControl("chkHead");
                    if (chkH.Checked)
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_OfferedCourse.chkHead_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
