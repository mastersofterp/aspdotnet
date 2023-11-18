//===================================================================================
//PAGE NAME      : Bulk Section Allotment
//CREATION DATE  : 20 march 2020                                                          
// CREATED BY    : Mr. Nikhil V. Lambe   
// MODIFIED DATE  : 04-08-2023                                                                    
// MODIFIED By    : Mr. Jay Takalkhede
// MODIFIED DESC  : Added Academic year dropdown for filter Record        
//===================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using Excel;
using ClosedXML.Excel;
using System.IO;
using System.Configuration;
using System.Web.Security;
using OfficeOpenXml;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Text.RegularExpressions;

public partial class ACADEMIC_BulkSectionAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Load & PopulateDropDownList
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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // Register the JavaScript function to set the active tab on page load
                ScriptManager.RegisterStartupScript(this, GetType(), "SetActiveTab", "setActiveTab();", true);

                PopulateDropDownList();
                btnSubmit.Enabled = false;
                btnConfirm.Enabled = false;
            }
            try
            {
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            catch (Exception ex)
            {
                throw;
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    private void CheckPageAuthorization()
    {

        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SectionAllotment.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION WITH (NOLOCK)", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            //objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");

            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "C.COLLEGE_ID");
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "C.COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            }

            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "C.COLLEGE_ID");
            //else
            //    objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Bind Listview
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        lvStudents.Visible = true;
        this.BindListView();
        ddlClassSection.Enabled = true;
        txtEnrollFrom.Enabled = true;
        txtEnrollTo.Enabled = true;
    }
    private void BindListView()
    {
        try
        {
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            DataSet ds = null;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            //lvStudents.FindControl("thRegno").Visible = false;

            if (rbAll.Checked)
            {
                // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO,S.ENROLLNO,S.ADMBATCH, S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.ENROLLNO,'')<>'' AND (S.SEMESTERNO=" + ddlSemester.SelectedValue + " OR  " + ddlSemester.SelectedValue + " = 0) " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + " AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "S.REGNO" : (rbStudName.Checked == true ? "S.STUDNAME" : (rbAdmDate.Checked == true ? "S.ADMDATE" : (rbmeritno.Checked == true ? "S.MERITNO" : "S.SEMESTERNO,S.ENROLLNO")))));
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO) LEFT JOIN ACD_BATCH BH WITH (NOLOCK) ON(S.STUD_BATCHNO=BH.BATCHNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO,S.ENROLLNO,S.ADMBATCH, S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO,BH.BATCHNAME", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + "AND (S.SECTIONNO=" + ddlClassSection.SelectedValue + " OR  " + ddlClassSection.SelectedValue + " = 0) AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND (S.SEMESTERNO=" + ddlSemester.SelectedValue + " OR  " + ddlSemester.SelectedValue + " = 0) " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "S.REGNO" : (rbStudName.Checked == true ? "S.STUDNAME" : (rbAdmDate.Checked == true ? "S.ADMDATE" : (rbmeritno.Checked == true ? "S.MERITNO" : "S.SEMESTERNO,S.ENROLLNO")))));

            }
            else if (rbRemaining.Checked)
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO) LEFT JOIN ACD_BATCH BH WITH (NOLOCK) ON(S.STUD_BATCHNO=BH.BATCHNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.REGNO,S.ENROLLNO,S.ADMBATCH, S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO,BH.BATCHNAME", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + "AND (S.SECTIONNO=" + ddlClassSection.SelectedValue + " OR  " + ddlClassSection.SelectedValue + " = 0) AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND (S.SEMESTERNO=" + ddlSemester.SelectedValue + " OR  " + ddlSemester.SelectedValue + " = 0)  AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue + " AND (S.REGNO  is null OR S.REGNO ='')", (rbRegNo.Checked == true ? "S.SECTIONNO,S.IDNO" : (rbStudName.Checked == true ? "S.SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "S.SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "S.MERITNO" : "S.SECTIONNO,S.IDNO")))));
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
                    hdfTot.Value = ds.Tables[0].Rows.Count.ToString();

                    btnSubmit.Enabled = true;
                    btnConfirm.Enabled = true;

                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    hdfTot.Value = "0";
                    objCommon.DisplayMessage(this.updBulkSectionA, "No Students found for selected criteria!", this.Page);
                    //objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                    btnSubmit.Enabled = false;
                    btnConfirm.Enabled = false;
                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                hdfTot.Value = "0";
                objCommon.DisplayMessage(this.updBulkSectionA, "No Students found for selected criteria!", this.Page);
                //objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                btnSubmit.Enabled = false;
                btnConfirm.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Bind Listview

    #region Cancel

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.Clear();
    }
    private void Clear()
    {
        // ddlSchemetype.SelectedIndex = 0;
        ddlInsName.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlAcdYear.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
        btnConfirm.Enabled = false;
        ddlClassSection.SelectedIndex = 0;
        ddlClassSection.Enabled = false;
        txtEnrollFrom.Text = string.Empty;
        txtEnrollFrom.Enabled = false;
        txtEnrollTo.Text = string.Empty;
        txtEnrollTo.Enabled = false;
    }
    #endregion Cancel

    #region Submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            StudentController objSC = new StudentController();
            string studID = string.Empty;
            string classSections = string.Empty;
            string rollNos = string.Empty;
            string roll = string.Empty;
            string ipAddress = string.Empty;
            string admBatchNo = "";
            int degreeNo = 0;
            int branchNo = 0;
            string admbatch = "";
            string batchNos = string.Empty;

            if (ddlInsName.SelectedValue != "0" && ddlInsName.SelectedValue != null)
            {
                if (ddlDegree.SelectedValue != "0" && ddlDegree.SelectedValue != null)
                {
                    if (ddlBranch.SelectedValue != "0" && ddlBranch.SelectedValue != null)
                    {
                        if (ddlSemester.SelectedValue != "0" && ddlSemester.SelectedValue != null)
                        {
                            if (ddlClassSection.SelectedValue != "0" && ddlClassSection.SelectedValue != null)
                            {
                                //if (ddlBatch.SelectedValue != "0" && ddlBatch.SelectedValue != null)
                                //{

                                foreach (ListViewDataItem lvItem in lvStudents.Items)
                                {
                                    CheckBox chkBox = lvItem.FindControl("chkrow") as CheckBox;
                                    if (chkBox.Checked)
                                    {
                                        studID += chkBox.ToolTip + "$";
                                        rollNos += (lvItem.FindControl("lblprnno") as HiddenField).Value + "$";
                                        roll += (lvItem.FindControl("hdfRollNO") as HiddenField).Value + "$";
                                        classSections += ddlClassSection.SelectedValue + "$";
                                        batchNos += ddlBatch.SelectedValue + "$";
                                        admbatch += (lvItem.FindControl("hdfAdm") as HiddenField).Value + ",";
                                    }
                                }
                                if (string.IsNullOrEmpty(studID))
                                {
                                    objCommon.DisplayMessage(this.updBulkSectionA, "Please Select Student/Section", this.Page);
                                    //objCommon.DisplayMessage(this.updSection, "Please Select Student/Section", this.Page);
                                }
                                if (Convert.ToInt32(ddlAdmBatch.SelectedValue) > 0)
                                {
                                    admBatchNo = (ddlAdmBatch.SelectedValue).ToString() + ",";
                                }
                                else
                                {
                                    admBatchNo = admbatch;
                                }
                                degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                                branchNo = Convert.ToInt32(ddlBranch.SelectedValue);

                                if (objSC.UpdateStudentSectionBulk(studID, classSections, Convert.ToInt32(Session["userno"]), admBatchNo, degreeNo, branchNo, (string.IsNullOrEmpty(ViewState["ipAddress"].ToString()) ? "" : ViewState["ipAddress"].ToString()), batchNos) == Convert.ToInt32(CustomStatus.RecordUpdated))
                                {
                                    this.BindListView();
                                    objCommon.DisplayMessage(this.updBulkSectionA, "Record Updated Successfully!!!", this.Page);
                                    //objCommon.DisplayMessage(this.updSection, "Record Updated Successfully!!!", this.Page);
                                    lvStudents.Visible = true;
                                }
                                else
                                    objCommon.DisplayMessage(this.updBulkSectionA, "Server Error....", this.Page);

                                //}
                                //else
                                //{
                                //    objCommon.DisplayMessage(this.updBulkSectionA, "Please Select Batch!", this.Page);
                                //    return;
                                //}
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updBulkSectionA, "Please Select Section!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updBulkSectionA, "Please Select Semester!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updBulkSectionA, "Please Select Programme/Branch!", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updBulkSectionA, "Please Select Degree!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updBulkSectionA, "Please Select School/Institute Name!", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Submit

    #region DropDownList
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1", "A.LONGNAME");

            ddlBranch.Focus();
        }
        else
        {
            //ddlBranch.Items.Clear();
            //ddlDegree.SelectedIndex = 0;
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.SelectedIndex = 0;
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_SEMESTER B WITH (NOLOCK) ON A.SEMESTERNO=b.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "A.SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND ISNULL(ACTIVESTATUS,0)=1", "A.SEMESTERNO");
                this.objCommon.FillDropDownList(ddlClassSection, "ACD_SECTION WITH (NOLOCK)", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SECTIONNAME");

            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSemester.SelectedIndex = 0;
                ddlClassSection.Items.Clear();
                ddlClassSection.Items.Add(new ListItem("Please Select", "0"));
                ddlClassSection.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlInsName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvStudents.Visible = false;
        try
        {
            if (ddlInsName.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ") AND ISNULL(D.ACTIVESTATUS,0)=1", "D.DEGREENO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND ISNULL(D.ACTIVESTATUS,0)=1 ", "D.DEGREENO");
                }
            }
            else
            {
                ddlDegree.Items.Clear();
                ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                ddlDegree.SelectedIndex = 0;
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                ddlBranch.SelectedIndex = 0;
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSemester.SelectedIndex = 0;
                ddlClassSection.Items.Clear();
                ddlClassSection.Items.Add(new ListItem("Please Select", "0"));
                ddlClassSection.SelectedIndex = 0;
            }
            //objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON A.DEGREENO=B.DEGREENO", "B.DEGREENO", "DEGREENAME", "B.DEGREENO > 0 AND COLLEGE_ID=" + ddlInsName.SelectedValue + "", "B.DEGREENO");

        }
        catch
        {
            throw;
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
    }

    protected void ddlClassSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvStudents.Visible = false;
        //new added- gopal
        // objCommon.FillDropDownList(ddlBatch, "ACD_BATCH A WITH (NOLOCK) INNER JOIN ACD_SECTION B WITH (NOLOCK) ON (A.SECTIONNO = B.SECTIONNO)", "DISTINCT (A.BATCHNO)", "A.BATCHNAME", "B.SECTIONNO > 0 AND A.SECTIONNO =" + ddlClassSection.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1 ", "A.BATCHNO");
        if (ddlClassSection.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlBatch, "ACD_BATCH A WITH (NOLOCK) INNER JOIN ACD_SECTION B WITH (NOLOCK) ON (A.SECTIONNO = B.SECTIONNO)", "DISTINCT (A.BATCHNO)", "A.BATCHNAME", "B.SECTIONNO > 0 AND A.SECTIONNO =" + ddlClassSection.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1 ", "A.BATCHNO");
            ddlBatch.Enabled = true;
        }
        else
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlBatch.SelectedIndex = 0;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvStudents.Visible = false;
        if (ddlSemester.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlClassSection, "ACD_SECTION WITH (NOLOCK)", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SECTIONNAME");
            ddlClassSection.Enabled = true;
        }
        else
        {
            ddlClassSection.Items.Clear();
            ddlClassSection.Items.Add(new ListItem("Please Select", "0"));
            ddlClassSection.SelectedIndex = 0;
        }
    }

    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    #endregion DropDownList

    #region Listview Event
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
                DataSet ds = objCommon.FillDropDown("ACD_SECTION WITH (NOLOCK)", "ISNULL(SECTIONNO,0)SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SECTIONNAME");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                    while (dtr.Read())
                    {
                        ddlsec.Items.Add(new ListItem(dtr["SECTIONNAME"].ToString(), dtr["SECTIONNO"].ToString()));

                    }
                }

                ddlsec.SelectedValue = dr["SECTIONNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Listview Event

    #region Rangr Confirm
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox chkBox = item.FindControl("chkrow") as CheckBox;
                chkBox.Checked = false;
            }

            string EnrollFrom = string.Empty;
            string EnrollTo = string.Empty;
            string checkBox = string.Empty;
            if (lvStudents.Items.Count > 0)
            {
                lvStudents.Visible = true;
            }

            if (txtEnrollFrom.Text == "" || txtEnrollTo.Text == "")
            {
                objCommon.DisplayMessage("Please Enter Range of Registration No. to be Filter", this.Page);
                return;
            }
            if (txtEnrollFrom.Text != null && txtEnrollFrom.Text != ""
                && txtEnrollTo.Text != null && txtEnrollTo.Text != "")
            {
                foreach (ListViewDataItem item in lvStudents.Items)
                {
                    CheckBox chkBox = item.FindControl("chkrow") as CheckBox;
                    //Label lblregno = item.FindControl("lblRegno") as Label;
                    EnrollFrom = Regex.Replace(txtEnrollFrom.Text, @"[^\d]", "");
                    EnrollTo = Regex.Replace(txtEnrollTo.Text, @"[^\d]", "");
                    checkBox = Regex.Replace(chkBox.Text, @"[^\d]", "");
                    //lblregno.Text;
                    if (Convert.ToInt64(checkBox) >= Convert.ToInt64(EnrollFrom) && Convert.ToInt64(checkBox) <= Convert.ToInt64(EnrollTo))
                    {
                        chkBox.Checked = true;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion Rangr Confirm

    #region RadioButton
    DataSet dsR = null; //for radiobutton
    protected void rbRegNo_CheckedChanged(object sender, EventArgs e)
    {
        dsR = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO,S.ENROLLNO,S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ADMBATCH,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.ENROLLNO,'')<>'' AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)   AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "SECTIONNO,S.REGNO" : (rbStudName.Checked == true ? "SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "SECTIONNO,S.MERITNO" : "SECTIONNO,S.REGNO")))));
        lvStudents.DataSource = dsR;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
    }

    protected void rbStudName_CheckedChanged(object sender, EventArgs e)
    {
        dsR = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO,S.ENROLLNO, S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ADMBATCH,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.ENROLLNO,'')<>'' AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)  AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "SECTIONNO,S.REGNO" : (rbStudName.Checked == true ? "SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "SECTIONNO,S.MERITNO" : "SECTIONNO,S.REGNO")))));
        lvStudents.DataSource = dsR;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
    }

    protected void rbAdmDate_CheckedChanged(object sender, EventArgs e)
    {
        dsR = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO, S.ENROLLNO,S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ADMBATCH,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.ENROLLNO,'')<>'' AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)  AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "SECTIONNO,S.REGNO" : (rbStudName.Checked == true ? "SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "SECTIONNO,S.MERITNO" : "SECTIONNO,S.REGNO")))));
        lvStudents.DataSource = dsR;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
    }

    protected void rbmeritno_CheckedChanged(object sender, EventArgs e)
    {
        dsR = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO, S.ENROLLNO,S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ADMBATCH,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.ENROLLNO,'')<>'' AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)  AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "SECTIONNO,S.REGNO" : (rbStudName.Checked == true ? "SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "SECTIONNO,S.MERITNO" : "SECTIONNO,S.REGNO")))));
        lvStudents.DataSource = dsR;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
    }

    protected void rbCGPA_CheckedChanged(object sender, EventArgs e)
    {
        dsR = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO) LEFT JOIN ACD_TRRESULT TR  WITH (NOLOCK) ON(TR.IDNO=S.IDNO AND TR.SECTIONNO=S.SECTIONNO AND S.SEMESTERNO=TR.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO, S.ENROLLNO,S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ADMBATCH,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO,ISNULL(TR.CGPA,0)CGPA", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.ENROLLNO,'')<>'' AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "SECTIONNO,S.REGNO" : (rbStudName.Checked == true ? "SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "SECTIONNO,S.MERITNO" : (rbCGPA.Checked == true ? "SECTIONNO,ISNULL(TR.CGPA,0)" : "SECTIONNO,S.REGNO"))))));
        lvStudents.DataSource = dsR;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
    }
    protected void rbenroll_CheckedChanged(object sender, EventArgs e)
    {
        dsR = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_SECTION SC WITH (NOLOCK) ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(SM.SEMESTERNO=S.SEMESTERNO)", "ROW_NUMBER() OVER (ORDER BY  S.IDNO)SRNO,S.IDNO", "S.REGNO,S.ENROLLNO,S.STUDNAME,SM.SEMESTERNAME, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ADMBATCH,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE,S.MERITNO", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND ISNULL(S.REGNO,'')<>'' AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)  AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "SECTIONNO,S.REGNO" : (rbStudName.Checked == true ? "SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "SECTIONNO,S.ADMDATE" : (rbmeritno.Checked == true ? "SECTIONNO,S.MERITNO" : (rbenroll.Checked == true ? "SECTIONNO,S.ENROLLNO" : "SECTIONNO,S.ENROLLNO"))))));
        lvStudents.DataSource = dsR;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
    }

    #endregion RadioButton

    //Added By Vinay Mishra on 07/08/2023 To Get Student Details For Section Allotment Import Process
    #region "Tab2_ImportSectionAllotment"
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        // After postback, retrieve the active tab index from the HiddenField
        // int activeTabIndex = int.Parse(ActiveTabIndexHiddenField.Value);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSc = new StudentController();
            string existpath = string.Empty;
            string path = string.Empty;

            Session["TempESMDB"] = null;

            if (fuSectionAllotUpload.HasFile == true)
            {
                string FileName = Path.GetFileName(fuSectionAllotUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(fuSectionAllotUpload.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    if (FileName.Contains(".xls"))
                    {
                        FileName = FileName.Replace(".xls", "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + Session["userno"].ToString() + ".xls");
                    }
                    else if (FileName.Contains(".xlsx"))
                    {
                        FileName = FileName.Replace(".xlsx", "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + Session["userno"].ToString() + ".xlsx");
                    }

                    //string FilePath =(FolderPath + FileName);
                    string FilePath = Server.MapPath("~/ExcelData/" + FileName);
                    path = MapPath("~/ExcelData/");
                    existpath = path + "\\" + FileName;
                    string[] array1 = Directory.GetFiles(path);
                    foreach (string str in array1)
                    {
                        if ((existpath).Equals(str))
                        {
                            objCommon.DisplayMessage(this.updBulkSectionI, "File with similar name already exists!", this);
                            return;
                        }
                    }

                    string idColumnName = "REGNO";
                    string updateColumnName = "SECTION_NAME";
                    string collegeId = ddlSchool.SelectedValue;
                    string regNo = string.Empty;
                    string section = string.Empty;
                    fuSectionAllotUpload.SaveAs(FilePath);
                    ExcelToDatabase(FilePath, idColumnName, updateColumnName, collegeId, Extension, "yes");
                    DataTable dt1 = (DataTable)Session["TempESMDB"];

                    //foreach (DataRow row in dt1.Rows)
                    //{
                    //    foreach (DataColumn column in dt1.Columns)
                    //    {
                    //        if (column.ToString() == "REGNO")
                    //        {
                    //            regNo += row[column].ToString() + ',';
                    //        }
                    //        if (column.ToString() == "SECTIONNAME")
                    //        {
                    //            section += row[column].ToString() + ',';
                    //        }
                    //    }
                    //}

                    //regNo = regNo.TrimEnd(',');
                    //section = section.TrimEnd(',');

                    int OrgId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                    string ipAddress = System.Web.HttpContext.Current.Session["ipAddress"].ToString();
                    int UANO = Convert.ToInt32(Session["userno"]);
                    string session = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT(SESSIONNO)", "SESSIONID IS NOT NULL AND ISNULL(IS_ACTIVE, 0) = 1 AND ISNULL(FLOCK, 0) = 1 AND COLLEGE_ID =" + Convert.ToInt32(ddlSchool.SelectedValue));
                    int upd = objSc.Update_Student_Section_InBulk(collegeId, UANO, ipAddress, dt1, OrgId, Convert.ToInt32(session));
                    if (upd == Convert.ToInt32(CustomStatus.RecordUpdated))
                    {
                        ddlSchool.SelectedIndex = 0;
                        objCommon.DisplayMessage(this.updBulkSectionI, "Excel Uploaded SuccessFully!!!", this);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updBulkSectionI, "Only .xls or .xlsx Extention is allowed!!!", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updBulkSectionI, "Please Select the Excel File to Upload!!!", this);
                return;
            }
        }
        catch
        {
            objCommon.DisplayMessage(this.updBulkSectionI, "Cannot Access the File. Please Try Again!!!", this);
            return;
        }
    }

    private void ExcelToDatabase(string FilePath, string IdColumnName, string UpdateColumnName, string CollegeId, string Extension, string isHDR)
    {
        //string recordId = string.Empty;
        //string collegeIds = string.Empty;
        //string newValue = string.Empty;
        //StudentController objSc = new StudentController();
        //DataSet ds = new DataSet();
        //ds = objSc.GetSectionMaster(Convert.ToInt32(ddlSchool.SelectedValue));

        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                break;
            case ".xlsx": //Excel 07
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);

        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet

        connExcel.Open();
        System.Data.DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);

        //Bind Excel to GridView
        DataSet ds = new DataSet();
        oda.Fill(ds);
        DataView dv1 = dt.DefaultView;
        DataTable dtNew = dv1.ToTable();
        DataRow dr = null;

        if (dt.Rows.Count > 0)
        {
            if (dt.Columns.Count == 7)
            {
                bool flag = false;
                bool empty_flag = false;
                var regN = string.Empty;
                var secname = string.Empty;

                foreach (DataRow r in dtNew.Rows)
                {
                    int semesterno = 0;
                    string regnum = string.Empty;
                    string regno = r["REGNO"].ToString();
                    string studname = r["STUDNAME"].ToString();
                    string clgname = r["COLLEGE_NAME"].ToString();
                    string degname = r["DEGREENAME"].ToString();
                    string branchname = r["BRANCHNAME"].ToString();
                    string semname = r["SEMESTERNAME"].ToString();
                    string sectionname = r["SECTIONNAME"].ToString();

                    if (!string.IsNullOrEmpty(sectionname))
                    {
                        empty_flag = true;
                        if (!string.IsNullOrEmpty(regno))
                        {
                            empty_flag = true;
                            semesterno = Convert.ToInt32(r["SEMESTERNAME"].ToString());
                            regnum = r["REGNO"].ToString();
                        }
                        else
                        {
                            empty_flag = false;
                            ViewState["maxMarkmsg"] = "RegNo allow only numeric value!";
                            break;
                        }
                    }
                    else
                    {
                        empty_flag = false;
                        ViewState["regNoMsg"] = "SectionName does not allow empty value!";
                        break;
                    }

                    //regN = objCommon.LookUp("ACD_STUDENT", "DISTINCT REGNO", "REGNO ='" + regnum + "' AND SEMESTERNO =" + semesterno);
                    //if (regN != null && regN != "")
                    //{
                        //secname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)", "SECTIONNAME", "REGNO ='" + regnum + "' AND SEMESTERNO =" + semesterno);
                        //if (secname != null && secname != "")
                        //{
                           // flag = true;
                        //}
                        //else
                        //{
                        //    flag = false;
                        //}
                    //}

                    if (empty_flag == true)
                    {
                        SectionAllotDynamicTable(regno, studname, clgname, degname, branchname, semname, sectionname);
                    }
                }
            }
        }

        connExcel.Close();

        //int OrgId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        //string session = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT(SESSIONID)", "SESSIONID IS NOT NULL AND ISNULL(IS_ACTIVE, 0) = 1 AND ISNULL(FLOCK, 0) = 1 AND COLLEGE_ID =" + Convert.ToInt32(ddlSchool.SelectedValue));
        //int upd = objSc.Update_Student_Section_InBulk(CollegeId, recordId, newValue, OrgId, Convert.ToInt32(session));
        //if (upd == Convert.ToInt32(CustomStatus.RecordUpdated))
        //{
        //    objCommon.DisplayMessage(this.updSection, "Excel Uploaded SuccessFully!!!", this.Page);
        //}
    }

    protected void SectionAllotDynamicTable(string rNo, string sName, string cName, string dName, string bName, string semName, string secName)
    {
        DataTable dt = new DataTable();
        try
        {
            if (Session["TempESMDB"] == null)
            {
                dt.Columns.AddRange(new DataColumn[8] { 
                new DataColumn("STUDENTID", typeof(int)),
                new DataColumn("REGNO", typeof(string)), 
                new DataColumn("STUDNAME", typeof(string)),
                new DataColumn("COLLEGE_NAME", typeof(string)),
                new DataColumn("DEGREENAME", typeof(string)),
                new DataColumn("BRANCHNAME", typeof(string)),
                new DataColumn("SEMESTERNAME", typeof(string)),
                new DataColumn("SECTIONNAME", typeof(string))
                });

                dt.Rows.Add(1, rNo, sName, cName, dName, bName, semName, secName);
                Session["TempESMDB"] = dt;
            }
            else
            {
                dt = (DataTable)Session["TempESMDB"];
                int rno = dt.Rows.Count;
                dt.Rows.Add(rno + 1, rNo, sName, cName, dName, bName, semName, secName);
                Session["TempESMDB"] = dt;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //using (var package = new ExcelPackage(new System.IO.FileInfo(FilePath)))
    //{
    //    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

    //    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
    //    {
    //        if (worksheet.Cells[row, 7].Value.ToString() != "" || worksheet.Cells[row, 7].Value.ToString() != null)
    //        {
    //            recordId += worksheet.Cells[row, 1].Value.ToString() + ',';
    //            newValue += worksheet.Cells[row, 7].Value.ToString() + ',';
    //        }
    //    }

    //    recordId.TrimEnd(',');
    //    newValue.TrimEnd(',');
    //    int OrgId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
    //    string session = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT(SESSIONID)", "SESSIONID IS NOT NULL AND ISNULL(IS_ACTIVE, 0) = 1 AND ISNULL(FLOCK, 0) = 1 AND COLLEGE_ID =" + Convert.ToInt32(ddlSchool.SelectedValue));
    //    int upd = objSc.Update_Student_Section_InBulk(CollegeId, recordId, newValue, OrgId, Convert.ToInt32(session));
    //    if (upd == Convert.ToInt32(CustomStatus.RecordUpdated))
    //    {
    //        objCommon.DisplayMessage(this.updSection, "Excel Uploaded SuccessFully!!!", this.Page);
    //    }
    //}

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //LinkButton lnkDownload = sender as LinkButton;
        //string FileName = btnDownload.ToolTip.ToString();
        //string file_path = Server.MapPath("~/UploadSyllabus/");
        //string filepath = file_path + FileName;    // +System.IO.Path.GetExtension(FileName); ;
        //Response.Redirect("DownloadAttachment.aspx?file=" + filepath + "&filename=" + FileName);
        if (ddlSchool.SelectedIndex > 0)
        {

            StudentController objSC = new StudentController();
            string session = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "ISNULL(IS_ACTIVE,0)=1 AND ISNULL(FLOCK,0)=1 AND COLLEGE_ID =" + Convert.ToInt32(ddlSchool.SelectedValue));
            DataSet ds = objSC.GetAllStudentsDetails(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(session));

            ds.Tables[0].TableName = "Student Details";
            ds.Tables[1].TableName = "Section Master";

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                ds.Tables[0].Rows.Add("No Record Found");

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                ds.Tables[1].Rows.Add("No Record Found");

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                    wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=BulkSectionAllotment.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            ddlSchool.SelectedIndex = 0;
        }
        else
        {
            objCommon.DisplayMessage(this.updBulkSectionI, "Please Select School/Institute!!!", this);

        }
    }

    protected void btnCancel_SectionImport_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Academic/BulkSectionAllotment.aspx?");

        //// After postback, retrieve the active tab index from the HiddenField
        //int activeTabIndex = int.Parse(ActiveTabIndexHiddenField.Value);

        ddlSchool.SelectedIndex = 0;
    }
    #endregion "Tab2_ImportSectionAllotment"

    // New method created by Gopal M, 26092023 - Ticket #48539
    public class SessionList
    {
        public int SECTIONNO { get; set; }
        public string SECTIONNAME { get; set; }
        public List<string> BATCHNAME { get; set; }
    }

    protected void btnDownloadIncurrect_Click(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex > 0)
        {
            DataTable dt2 = (DataTable)Session["IncurrectData"];
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                dt2 = (from fw in dt2.AsEnumerable() where Convert.ToInt32(fw["STATUS"]) == 0 select fw).CopyToDataTable();
                dt2.Columns.Remove("STATUS");
                DataSet ds = new DataSet();
                ds.Tables.Add(dt2);
                ds.Tables[0].TableName = "Student Details";
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                {
                    ds.Tables[0].Rows.Add("No Record Found");
                }
                else
                {

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=BulkSectionAllotmentIncurrect.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                ddlSchool.SelectedIndex = 0;
            }
            else
            {
                objCommon.DisplayMessage(this.updBulkSectionI, "Doesn't Found Any Incurrect Data!", this);
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updBulkSectionI, "Please Select School/Institute!!!", this);
            return;
        }
    }

} //End of partial class




