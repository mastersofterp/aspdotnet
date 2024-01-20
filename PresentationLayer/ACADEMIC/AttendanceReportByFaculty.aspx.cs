//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ATTENDANCE REPORT BY FACULTY                                 
// CREATION DATE : 15-OCT-2011
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 29-DEC-2011
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_AttendanceReportByFaculty : System.Web.UI.Page
{

    StudentAttendanceController objSAC = new StudentAttendanceController();
    #region Page Evnets
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //CONNECTIONSTRING
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
        try
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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //BindListView();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();

                    if (Session["dec"].ToString() == "1" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "3")
                    {
                        btnHODReport.Visible = true;
                        //branch.Visible = false;
                        faculty.Visible = false;
                        //btnSubjectwise.Enabled = false;
                        if (Session["usertype"].ToString() == "1")
                        {
                            rfvSessConsole.Enabled = true;
                            //rfvDegConsole.Enabled = false;
                            //rfBranchConsole.Enabled = false;
                            //rfvSchemeConsole.Enabled = false;
                            rfvSemConsole.Enabled = false;
                            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENAME");
                        }
                        else
                        {
                            rfvSessConsole.Enabled = true;
                            //rfvDegConsole.Enabled = true;
                            //rfBranchConsole.Enabled = true;
                            //rfvSchemeConsole.Enabled = true;
                            rfvSemConsole.Enabled = true;
                        }
                    }
                    else
                    {
                        branch.Visible = true;
                        faculty.Visible = true;
                        btnSubjectwise.Enabled = true;

                        rfvSessConsole.Enabled = true;
                        //rfvDegConsole.Enabled = true;
                        //rfBranchConsole.Enabled = true;
                        //rfvSchemeConsole.Enabled = true;
                        rfvSemConsole.Enabled = true;
                    }

                    if (Session["usertype"].ToString() != "3")
                        dvFaculty.Visible = true;
                }
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 30/01/2022
                //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 30/01/2022

            }
            divMsg.InnerHtml = string.Empty;
            //// Clear message div
            //divScript.InnerHtml = string.Empty;
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
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }

    #endregion

    #region Form Methods

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvAttendence.Visible = false;
            lvByPercent.Visible = false;
            if (ddlSem.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_ATTENDANCE C WITH (NOLOCK) INNER JOIN ACD_SUBJECTTYPE S WITH (NOLOCK) ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "AND C.SEMESTERNO=" + ddlSem.SelectedValue + " AND C.SESSIONNO=" + ddlSession.SelectedValue, "C.SUBID");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue", "C.SUBID");

                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSem.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Private Methods

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    //private void FillScheme(int sessionNo, int uaNo, int subId, int batchNo)
    //{
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[4];
    //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
    //        objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
    //        objParams[2] = new SqlParameter("@P_SUBID", subId);
    //        objParams[3] = new SqlParameter("@P_BATCHNO", batchNo);
    //        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_BY_UA_NO", objParams);

    //        ddlScheme.DataSource = ds;
    //        ddlScheme.Items.Clear();
    //        ddlScheme.Items.Add("Please Select");
    //        ddlScheme.SelectedItem.Value = "0";
    //        ddlScheme.DataValueField = ds.Tables[0].Columns["SCHEMENO"].ToString();
    //        ddlScheme.DataTextField = ds.Tables[0].Columns["SCHEMENAME"].ToString();
    //        ddlScheme.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_AttendenceByFaculty.FillScheme-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

            if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "3")
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO in (" + Session["userdeptno"].ToString() + ")", "D.DEGREENO");
            }
            else if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)  INNER JOIN ACD_BRANCH A  WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_SCHEME SH ON (SH.BRANCHNO=B.BRANCHNO AND D.DEGREENO=B.DEGREENO) INNER JOIN ACD_ATTENDANCE ATT ON (SH.SCHEMENO=ATT.SCHEMENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND ATT.UA_NO =" + Session["userno"].ToString() + "AND ATT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "D.DEGREENO"); // Add Roshan on 14-02-2020
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
            }
            //else
            //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");

        }

        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlFaculty.Items.Clear();
        ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            if (Session["usertype"].ToString() == "8" || Session["usertype"].ToString() == "3") // added by roshan pannase 15022020
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            }
            else if (Session["usertype"].ToString() != "1")
            {
                // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=B.DEGREENO) INNER JOIN ACD_SCHEME SH ON (SH.BRANCHNO=B.BRANCHNO AND D.DEGREENO=B.DEGREENO) INNER JOIN ACD_ATTENDANCE ATT ON (SH.SCHEMENO=ATT.SCHEMENO)	", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND ATT.UA_NO =" + Session["userno"].ToString(), "A.LONGNAME");  //added by roshan pannase 15022020

            }
            else
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]), "A.LONGNAME");
            }


            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
        }
        else
        {
            ClearControls();
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
        //if (ddlBranch.SelectedIndex > 0)
        //{
        //    ddlScheme.Items.Clear();
        //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
        //    ddlScheme.Focus();
        //    ddlSem.Items.Clear();
        //    ddlSem.Items.Add(new ListItem("Please Select", "0"));
        //    ddlScheme.Focus();
        //}
        //else
        //{
        //    ddlSem.Items.Clear();
        //    ddlSem.Items.Add(new ListItem("Please Select", "0"));
        //}

        ////if (ddlBranch.SelectedIndex > 0)
        ////{
        ////    if (Session["usertype"].ToString() != "3")
        ////    {
        ////        ddlFaculty.Items.Clear();
        ////        objCommon.FillDropDownList(ddlFaculty, "USER_ACC A INNER JOIN ACD_ATTENDANCE AA ON (A.UA_NO = AA.UA_NO) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO = AA.SCHEMENO)", "DISTINCT(A.UA_NO)", "A.UA_FULLNAME", "SC.DEGREENO= " + ddlDegree.SelectedValue + " AND SC.BRANCHNO = " + ddlBranch.SelectedValue + "AND AA.SESSIONNO=" + ddlSession.SelectedValue, "A.UA_FULLNAME");
        ////        ddlFaculty.Focus();
        ////    }
        ////    else
        ////    {
        ////        ddlScheme.Items.Clear();
        ////        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + "AND DEPTNO =" + Session["userdeptno"].ToString(), "SCHEMENO");
        ////        ddlScheme.Focus();
        ////    }
        ////}

        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "schemeno", "SCHEMENAME", "BRANCHNO =" + Convert.ToInt32(ViewState["branchno"]) + "AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]), "SCHEMENO");

            ddlScheme.Focus();
            //ddlSem.Items.Clear();
            //ddlSem.Items.Add(new ListItem("Please Select", "0"));
            //ddlScheme.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    ////protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    ////{
    ////    if (ddlFaculty.SelectedIndex > 0)
    ////    {
    ////        ddlScheme.Items.Clear();
    ////        if (Session["usertype"].ToString() != "1")
    ////            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME C INNER JOIN ACD_ATTENDANCE A ON (A.SCHEMENO = C.SCHEMENO)", "DISTINCT(C.SCHEMENO)", "C.SCHEMENAME", "C.BRANCHNO =" + ddlBranch.SelectedValue + "AND C.DEGREENO=" + ddlDegree.SelectedValue +"AND C.DEPTNO =" + Session["userdeptno"].ToString() +"AND A.UA_NO=" + ddlFaculty.SelectedValue +"AND A.SESSIONNO=" + ddlSession.SelectedValue, "C.SCHEMENO");
    ////        else
    ////            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME C INNER JOIN ACD_ATTENDANCE A ON (A.SCHEMENO = C.SCHEMENO)", "DISTINCT(C.SCHEMENO)", "C.SCHEMENAME", "C.BRANCHNO =" + ddlBranch.SelectedValue + "AND C.DEGREENO=" + ddlDegree.SelectedValue +"AND A.UA_NO=" + ddlFaculty.SelectedValue + "AND A.SESSIONNO=" + ddlSession.SelectedValue, "C.SCHEMENO");
    ////        ddlScheme.Focus();
    ////        ddlSem.Items.Clear();
    ////        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    ////        ddlScheme.Focus();
    ////    }
    ////    else
    ////    {
    ////        ddlSem.Items.Clear();
    ////        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    ////    }
    ////}

    protected void btnSubjecwise_Click(object sender, EventArgs e)
    {
        try
        {
            //Added by Nikhil Vinod Lambe on 11032020 to pass perFrom,perTo and selector
            int selector = 0;
            if (rdoPerBtn.Checked)
            {
                selector = 0;
                txtPercentage.Text = "0";
            }
            else if (rdoOpr.Checked)
            {
                selector = 1;
            }
            else
            {
                selector = 2;
                txtPercentage.Text = "0";
            }
            ////ShowReportinFormate(rdoReportType.SelectedValue, "rptConsolidatedAttendanceNew.rpt");
            GridView GV = new GridView();

            string  FromDate = txtFromDate.Text == string.Empty ? "01/01/1753" : txtFromDate.Text;
            string  ToDate = txtTodate.Text == string.Empty ? "01/01/1753" : txtTodate.Text;
            DataSet ds = objCommon.GetAttendanceData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), FromDate, ToDate, Convert.ToInt32(ddlSection.SelectedValue), ddlOperator.SelectedValue, Convert.ToDouble(txtPercentage.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), txtPercentageFrom.Text.ToString(), txtPercentageTo.Text.ToString(), Convert.ToInt32(selector));

            //**************************************************************************************
            //GetAttendanceForAll(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector)
            ds.Tables[0].Columns.Remove("ROLLNO");
            ds.Tables[0].Columns.Remove("IDNO");
            ds.Tables[0].Columns.Remove("SCHEMENO");
            ds.Tables[0].Columns.Remove("SEMESTERNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string sem = "AllSemester";
                if (Convert.ToInt32(ddlSession.SelectedValue) > 0)
                    sem = ddlSem.SelectedItem.Text;

                string attachment = "attachment; filename=Attendance_Sem-" + sem + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
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
                objCommon.DisplayUserMessage(updReport, "No Record Found.", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    ////private void ShowReportinFormate(string exporttype, string rptFileName)
    ////{
    ////    try
    ////    {
    ////        string degree = "";
    ////        degree = objCommon.LookUp("ACD_DEGREE", "CODE", "DEGREENO=" + ddlDegree.SelectedValue);
    ////        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    ////        url += "Reports/CommonReport.aspx?";
    ////        url += "exporttype=" + exporttype;       
    ////        url += "&path=~,Reports,Academic," + rptFileName;
    ////        url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "." + rdoReportType.SelectedValue;

    ////        if (Session["usertype"].ToString() == "3")

    ////            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_CONDITIONS=" + ddlOperator.SelectedValue +",@P_PERCENTAGE=" + txtPercentage.Text.Trim() + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_TH_PR=" + ddltheorypractical.SelectedValue + ",@P_BATCHNO=" + ddlBatch.SelectedValue + ",@P_UANO=" + Session["userno"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
    ////        else
    ////            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_CONDITIONS=" + ddlOperator.SelectedValue +",@P_PERCENTAGE=" + txtPercentage.Text.Trim() + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_TH_PR=" + ddltheorypractical.SelectedValue + ",@P_BATCHNO=" + ddlBatch.SelectedValue + ",@P_UANO=" + ddlFaculty.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue;

    ////            url += "&param=username=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_CONDITIONS=" + ddlOperator.SelectedValue + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim() + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREE=" + degree;

    ////        //To open new window from Updatepanel
    ////        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    ////        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    ////        sb.Append(@"window.open('" + url + "','','" + features + "');");
    ////        ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        if (Convert.ToBoolean(Session["error"]) == true)
    ////            objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    ////        else
    ////            objUCommon.ShowError(Page, "Server Unavailable.");
    ////    }
    ////}
    protected void btnHODReport_Click(object sender, EventArgs e)
    {
        ShowReport("NotAttendanceDetails", "rptAttendanceNotSentByFaculty.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) 
                + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                ",@P_START_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy") +
                ",@P_END_DATE=" + Convert.ToDateTime(txtTodate.Text).ToString("dd-MM-yyyy") +
                ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) +
                ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);
            //divScript.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } ";
            //divScript.InnerHtml += " window.close();";
            //divScript.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReportSubjectwise(string reportTitle, string rptFileName)
    {
        try
        {
            int ua_no = 0;
            if (Session["usertype"].ToString() != "1")
            {
                ua_no = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                ua_no = 0;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;



            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
                ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) +
                ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
                ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + //Added By Nikhil to get only date not time..
                ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") +
                ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) +
                ",@P_CONDITIONS=" + ddlOperator.Text.ToString() +
                ",@P_PERCENTAGE=" + txtPercentage.Text.ToString() +
                ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) +
                ",@P_TH_PR=" + Convert.ToInt32(ddltheorypractical.SelectedValue) +
                ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) +
                ",@P_UANO=" + ua_no +
                ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);
            //divScript.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } ";
            //divScript.InnerHtml += " window.close();";
            //divScript.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubjectwiseExpected_Click(object sender, EventArgs e)
    {
        try
        {
            ////***ShowReportinFormate(rdoReportType.SelectedValue, "rptAttendanceForDisplay.rpt");
            ShowReportSubjectwise("Subjectwise_Attendance_Report", "rptSubjectwiseAttendance.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
        if (ddlSubjectType.SelectedIndex > 0)
        {
            ddlSection.Items.Clear();
            // objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION S ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue+" AND SR.SCHEMENO="+ddlScheme.SelectedValue+" AND SR.SEMESTERNO="+ddlSem.SelectedValue+" AND S.SECTIONNO>0", "S.SECTIONNO");
            objCommon.FillDropDownList(ddlSection, "ACD_ATTENDANCE SR WITH (NOLOCK) INNER JOIN ACD_SECTION S  WITH (NOLOCK) ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue + " AND S.SECTIONNO>0", "S.SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;

        if (ddltheorypractical.SelectedValue == "2" || ddltheorypractical.SelectedValue == "3")
        {
            dvBatch.Visible = true;
            //rfvBatch.Visible = true;

            if (ddlSection.SelectedIndex > 0 && ddltheorypractical.SelectedValue != "1")
            {
                if (ddltheorypractical.SelectedValue == "2")
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = SR.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SECTIONNO =" + ddlSection.SelectedValue, "B.BATCHNO");
                else
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = SR.TH_BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + "AND SR.SECTIONNO =" + ddlSection.SelectedValue, "B.BATCHNO");

                ddlBatch.Focus();
            }
            else
            {

                ddlSection.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlBatch.SelectedIndex = 0;
                ddlBatch.Items.Clear();

            }
        }
        else
        {
            dvBatch.Visible = false;
            //rfvBatch.Visible = false;
        }
    }
    protected void rdoPerBtn_CheckedChanged(object sender, EventArgs e)
    {
        txtPercentageFrom.Visible = true;
        txtPercentageTo.Visible = true;
        btnPercentage.Visible = true;
        lblperfrom.Visible = true;
        lblPerTo.Visible = true;
        //For operator
        ddlOperator.Visible = false;
        ddlOperator.SelectedIndex = 0;
        txtPercentage.Visible = false;
        txtPercentage.Text = string.Empty;
        lblPercentage.Visible = false;
        lblOperator.Visible = false;
        pnlAttendence.Visible = false;
        pnlByPercent.Visible = false;
        btnSubjectwiseExpected.Visible = false;
    }
    protected void btnPercentage_Click(object sender, EventArgs e)
    {
        ShowPercentage("BetweenPercentageReport", "rptBetweenPercentageReport.rpt");
    }
    private void ShowPercentage(string reportTitle, string rptfilename)
    {
        int ua_no = 0;
        int selector = 0;
        if (Session["usertype"].ToString() != "1")
        {
            ua_no = Convert.ToInt32(Session["userno"]);
        }
        else
        {
            ua_no = 0;
        }
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptfilename; ;

        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
            //",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) +
            //",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) +
            ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) +
            ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
            ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") +
            ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") +
            ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) +
            ",@P_PERCENTAGEFROM=" + txtPercentageFrom.Text +
            ",@P_PERCENTAGETo=" + txtPercentageTo.Text +
            ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) +
            //Added By Nikhil on 11032020 to pass @P_CONDITIONS & @P_PERCENTAGE null
            ",@P_CONDITIONS=" + 0 +
            //",@P_CONDITIONS=" + Convert.ToInt32(ddlOperator.SelectedValue) +
            //  ",@P_PERCENTAGE=" + Convert.ToInt32(txtPercentage.Text).ToString() +
              ",@P_PERCENTAGE=" + 0 +
             ",@P_SELECTOR=" + selector +
            // ",@P_TH_PR=" + Convert.ToInt32(ddltheorypractical.SelectedValue) +
            // ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) +
            //  ",@P_UANO=" + ua_no +
            ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);
    }
    private void BindListView_Operator(int selector)
    {
        try
        {
            StudentAttendanceController objSAC = new StudentAttendanceController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance();
            objAtt.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAtt.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAtt.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAtt.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAtt.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAtt.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objAtt.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objAtt.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);
            if (txtTodate.Text != string.Empty && txtFromDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updReport, "End date should be greater than Start date", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            string from = FromDate.ToString("yyyy-MM-dd");
            DateTime ToDate = Convert.ToDateTime(txtTodate.Text);
            string to = ToDate.ToString("yyyy-MM-dd");
            objAtt.Conditions = ddlOperator.SelectedValue.ToString();
            objAtt.Percentage = Convert.ToString(txtPercentage.Text);
            DataSet ds = objSAC.GetAttendanceSelectorWise(objAtt, selector, from, to);
            pnlByPercent.Visible = false;

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
                //pnlAttendence.Visible = false;
                //pnlByPercent.Visible = false;
                objCommon.DisplayMessage(updReport, "No Records Found.", this.Page);
                return;
            }
            else
            {
                pnlAttendence.Visible = true;
                lvAttendence.DataSource = ds;
                lvAttendence.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListByPer(int selector)
    {
        try
        {
            StudentAttendanceController objSAC = new StudentAttendanceController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance();
            objAtt.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAtt.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAtt.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAtt.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAtt.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            if (txtTodate.Text != string.Empty && txtFromDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updReport, "End date should be greater than Start date", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            string from = FromDate.ToString("yyyy-MM-dd");
            DateTime ToDate = Convert.ToDateTime(txtTodate.Text);
            string to = ToDate.ToString("yyyy-MM-dd");
            if (txtPercentageTo.Text != string.Empty && txtPercentageFrom.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPercentageTo.Text) <= Convert.ToInt32(txtPercentageFrom.Text))
                {
                    objCommon.DisplayMessage(this.updReport, "To Percentage should be greater than From percentage", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
            objAtt.PercentageFrom = Convert.ToString(txtPercentageFrom.Text);
            objAtt.PercentageTo = Convert.ToString(txtPercentageTo.Text);
            objAtt.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objAtt.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);
            objAtt.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            DataSet dsPer = objSAC.GetAttendanceSelectorWise(objAtt, selector, from, to);
            pnlAttendence.Visible = false;
            pnlByPercent.Visible = true;
            if (dsPer.Tables[0].Rows.Count <= 0)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
                objCommon.DisplayMessage(updReport, "No Records Found.", this.Page);
                return;
            }
            else
            {
                lvByPercent.DataSource = dsPer;
                lvByPercent.DataBind();
            }
            if (txtPercentageFrom.Text == string.Empty)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
            }

            else if (txtPercentageTo.Text == string.Empty)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindForAll(int selector)
    {
        try
        {

            IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance();
            objAtt.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAtt.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAtt.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAtt.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAtt.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            if (txtTodate.Text != string.Empty && txtFromDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this.updReport, "End date should be greater than Start date", this.Page);
                    lvAttendence.Visible = false;
                    lvByPercent.Visible = false;
                }
            }
           // Convert.ToDateTime(txtFromDate.Text);
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            string from = FromDate.ToString("yyyy-MM-dd");
           DateTime ToDate = Convert.ToDateTime(txtTodate.Text);
           string to = ToDate.ToString("yyyy-MM-dd");
            objAtt.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objAtt.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);
            objAtt.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            DataSet dsAll = objSAC.GetAttendanceSelectorWise(objAtt, selector, from, to);
            pnlAttendence.Visible = false;
            pnlByPercent.Visible = true;
            if (dsAll.Tables[0].Rows.Count <= 0)
            {
                lvAttendence.Visible = false;
                lvByPercent.Visible = false;
                objCommon.DisplayMessage(updReport, "No Records Found.", this.Page);
                return;
            }
            else
            {
                lvByPercent.DataSource = dsAll;
                lvByPercent.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        int select = 0;

        if (rdoPerBtn.Checked == true)
        {
            select = 0;
            lvByPercent.Visible = true;
            BindListByPer(select);
        }
        else if (rdoOpr.Checked == true)
        {
            select = 1;
            pnlAttendence.Visible = true;
            lvAttendence.Visible = true;
            BindListView_Operator(select);
        }
        else
        {
            select = 2;
            lvByPercent.Visible = true;
            BindForAll(select);
        }
    }

    protected void rdoOpr_CheckedChanged(object sender, EventArgs e)
    {
        ddlOperator.Visible = true;
        txtPercentage.Visible = true;
        lblPercentage.Visible = true;
        lblOperator.Visible = true;
        //For Between percentage
        txtPercentageFrom.Visible = false;
        txtPercentageTo.Visible = false;
        btnPercentage.Visible = false;
        lblperfrom.Visible = false;
        lblPerTo.Visible = false;
        txtPercentageFrom.Text = string.Empty;
        txtPercentageTo.Text = string.Empty;
        pnlAttendence.Visible = false;
        pnlAttendence.Visible = false;
        pnlByPercent.Visible = false;
        btnSubjectwiseExpected.Visible = true;
    }

    protected void ddlOperator_SelectedIndexChanged(object sender, EventArgs e)
    {

        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
        txtPercentage.Text = string.Empty;
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAttendence.Visible = false;
        lvByPercent.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            lvAttendence.Visible = false;
            lvByPercent.Visible = false;
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            ddlSem.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }

    }
    protected void btnSubjectwise2_Click(object sender, EventArgs e)
    {
        try
        {
            //Added by Nikhil Vinod Lambe on 11032020 to pass perFrom,perTo and selector
            int selector = 0;
            if (rdoPerBtn.Checked)
            {
                selector = 0;
                txtPercentage.Text = "0";
            }
            else if (rdoOpr.Checked)
            {
                selector = 1;
            }
            else
            {
                selector = 2;
                txtPercentage.Text = "0";
            }
            ////ShowReportinFormate(rdoReportType.SelectedValue, "rptConsolidatedAttendanceNew.rpt");
            GridView GV = new GridView();

            string FromDate = txtFromDate.Text == string.Empty ? "01/01/1753" : txtFromDate.Text;
            string ToDate = txtTodate.Text == string.Empty ? "01/01/1753" :txtTodate.Text;
            DataSet ds = objCommon.GetAttendanceData1(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), FromDate, ToDate, Convert.ToInt32(ddlSection.SelectedValue), ddlOperator.SelectedValue, Convert.ToDouble(txtPercentage.Text), Convert.ToInt32(ddlSubjectType.SelectedValue), txtPercentageFrom.Text.ToString(), txtPercentageTo.Text.ToString(), Convert.ToInt32(selector));

            //**************************************************************************************
            //GetAttendanceForAll(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector)
            //ds.Tables[0].Columns.Remove("ROLLNO");
            //ds.Tables[0].Columns.Remove("IDNO");
            //ds.Tables[0].Columns.Remove("SCHEMENO");
            //ds.Tables[0].Columns.Remove("SEMESTERNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=Attendance_Sem-" + ddlSem.SelectedItem + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +".xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
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
                objCommon.DisplayUserMessage(updReport, "No Record Found.", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSchool.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchool.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                }
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
            else
            {
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}




