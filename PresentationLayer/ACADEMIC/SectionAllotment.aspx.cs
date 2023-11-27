//======================================================================================
// PROJECT NAME   : UAIMS [AJU]                                                         
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : Section Enroll. No. Generation
// CREATION DATE  : 06-JULY-2011                                                          
// CREATED BY     : NIRAJ D. PHALKE                                                   
// MODIFIED DATE  : 02-05-2019                                                                     
// MODIFIED DESC  : M. REHBAR SHEIKH     
// MODIFIED DATE  : 04-08-2023                                                                    
// MODIFIED By    : Mr. Jay Takalkhede
// MODIFIED DESC  : Added Academic year dropdown for filter Record                                                      
//======================================================================================

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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_SectionAllotment : System.Web.UI.Page
{
  
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();
                btnSubmit.Enabled = false;
            }
            try
            {
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
                throw;
            }
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
            // objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ISNULL(ACTIVESTATUS,0)= 1", "BATCHNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO >0 AND CD.UGPGOT IN (" +  Session["ua_section"] + ")","D.DEGREENO");
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT", "SEMESTERNO", "distinct semesterno", "SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue + "", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ISNULL(ACTIVESTATUS,0)= 1", "SECTIONNO");
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlInsName, "acd_college_master", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "ISNULL(ActiveStatus,0)= 1 AND COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Bind Listview
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();
        
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
                ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_SECTION SC ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM ON(SM.SEMESTERNO=S.SEMESTERNO)", "S.IDNO", "S.REGNO,S.ADMBATCH, S.STUDNAME,SM.SEMESTERNAME,S.SEMESTERNO, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND (S.SEMESTERNO=" + ddlSemester.SelectedValue + " OR  " + ddlSemester.SelectedValue + " = 0) " + (rbRemaining.Checked == true ? " AND (ISNULL(S.SECTIONNO,0) = 0 or ROLLNO IS NULL or ROLLNO='')" : string.Empty) + "AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue, (rbRegNo.Checked == true ? "S.REGNO" : (rbStudName.Checked == true ? "S.STUDNAME" : (rbAdmDate.Checked == true ? "S.ADMDATE" : "S.SEMESTERNO,S.REGNO"))));
            }
            else if (rbRemaining.Checked)
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_SECTION SC ON (S.SECTIONNO = SC.SECTIONNO) LEFT JOIN ACD_SEMESTER SM ON(SM.SEMESTERNO=S.SEMESTERNO)", "S.IDNO", "S.REGNO,S.ADMBATCH, S.STUDNAME,SM.SEMESTERNAME,S.SEMESTERNO, ISNULL(S.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,S.ROLLNO,CONVERT(VARCHAR(12),ADMDATE,103)ADMDATE", "S.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND (S.SEMESTERNO=" + ddlSemester.SelectedValue + " OR  " + ddlSemester.SelectedValue + " = 0)  AND (ACADEMIC_YEAR_ID=" + ddlAcdYear.SelectedValue + " OR " + ddlAcdYear.SelectedValue + " =0) AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0) AND isnull(ADMCAN,0)=0 AND isnull(CAN,0)=0 AND BRANCHNO=" + ddlBranch.SelectedValue + " AND (S.REGNO  is null OR S.REGNO ='')", (rbRegNo.Checked == true ? "S.SECTIONNO,S.IDNO" : (rbStudName.Checked == true ? "S.SECTIONNO,S.STUDNAME" : (rbAdmDate.Checked == true ? "S.SECTIONNO,S.ADMDATE" : "S.SECTIONNO,S.IDNO"))));
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

                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    hdfTot.Value = "0";
                    objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                    btnSubmit.Enabled = false;
                }

                //TextBox text1 = lvStudents.FindControl("txtRollNo") as TextBox;

                //((TextBox)lvStudents.FindControl("txtRollNo")).Enabled = false;
                foreach (ListViewItem item in lvStudents.Items)
                {
                    TextBox rollno = item.FindControl("txtRollNo") as TextBox;
                    if (Session["usertype"].ToString() != "1")
                    {
                        rollno.Enabled = false;
                    }
                    else
                    {
                        rollno.Enabled = true;
                    }
                }


            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                hdfTot.Value = "0";
                objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Bind Listview

    #region Clear
    private void Clear()
    {
        // ddlSchemetype.SelectedIndex = 0;
        ddlInsName.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlAcdYear.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.Clear();
    }

    #endregion Clear

    #region Section Allotment

    /// <summary>
    /// Modified By Nidhi Gour 18102019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string admbatch = "";
        try
        {
            StudentController objSC = new StudentController();
            string studids = string.Empty;
            string sections = string.Empty;
            string rollnos = string.Empty;
            string roll = string.Empty;
            string semesterno = string.Empty;

            string admbatchno = (ddlAdmBatch.SelectedValue).ToString()+",";
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            int branchno = Convert.ToInt32(ddlBranch.SelectedValue);

            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                //if (Convert.ToInt32((lvItem.FindControl("ddlsec") as DropDownList).SelectedValue) > 0)
                //{
                //    foreach (ListViewDataItem lvItem2 in lvStudents.Items)
                //    {
                //        if (Convert.ToInt32((lvItem2.FindControl("ddlsec") as DropDownList).SelectedValue) > 0)
                //        {
                //            if ((lvItem.FindControl("txtRollNo") as TextBox).ClientID != (lvItem2.FindControl("txtRollNo") as TextBox).ClientID)
                //            {
                //                if ((lvItem.FindControl("txtRollNo") as TextBox).Text == (lvItem2.FindControl("txtRollNo") as TextBox).Text && (lvItem.FindControl("ddlsec") as DropDownList).SelectedValue == (lvItem2.FindControl("ddlsec") as DropDownList).SelectedValue)
                //                {
                //                    objCommon.DisplayMessage(this.updSection, "Same Roll Nos. Assigned!", this.Page);
                //                    (lvItem2.FindControl("txtRollNo") as TextBox).Focus();
                //                    return;
                //                }
                //            }
                //        }
                //    }
                //}
                //if (Convert.ToInt32((lvItem.FindControl("ddlsec") as DropDownList).SelectedValue) > 0)
                //{
                studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                sections += (lvItem.FindControl("ddlsec") as DropDownList).SelectedValue + "$";
                rollnos += (lvItem.FindControl("txtRollNo") as TextBox).Text + "$";
                roll += (lvItem.FindControl("TextBox1") as TextBox).Text + "$";
                admbatch += (lvItem.FindControl("hdfAdm") as HiddenField).Value + ",";
                semesterno += (lvItem.FindControl("hfsemesterno") as HiddenField).Value + "$";
                //}
            }
            if (Convert.ToInt32(ddlAdmBatch.SelectedValue) > 0)
            {
                admbatchno = (ddlAdmBatch.SelectedValue).ToString() + ",";
            }
            else
            {
                admbatchno = admbatch;
            }

            //foreach (ListViewDataItem lvItem in lvStudents.Items)
            //{
            //    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            //    if (chkBox.Checked == true)
            //    {
            //        studids += chkBox.ToolTip + "$";
            //        rollnos += (lvItem.FindControl("txtRollNo") as TextBox).Text + "$";
            //    }
            //}

            if (studids.Length <= 0 && sections.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSection, "Please Select Student/Section", this.Page);
                return;
            }

            if (objSC.UpdateStudentSection(studids, rollnos, sections, Convert.ToInt32(Session["userno"]), admbatchno, degreeno, branchno, (string.IsNullOrEmpty(ViewState["ipAddress"].ToString()) ? "" : ViewState["ipAddress"].ToString()), roll, semesterno) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                this.BindListView();
                objCommon.DisplayMessage(this.updSection, "Updated Successfully!!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updSection, "Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Section Allotment

    #region DDL

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "ISNULL(A.ACTIVESTATUS,0)= 1 AND A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //********ADDED BY: M. REHBAR SHEIKH**************
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
                DataSet ds = objCommon.FillDropDown("ACD_SECTION", "ISNULL(SECTIONNO,0)SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                //objCommon.LookUp(ddlSemester, "ACD_STUDENT", "DISTINCT SEMESTERNO","SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND ADMBATCH=" + ddlAdmBatch.SelectedValue + "", "SEMESTERNO");
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT A inner join acd_semester B on A.SEMESTERNO=b.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "A.SEMESTERNO > 0 AND degreeno=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND (ADMBATCH=" + ddlAdmBatch.SelectedValue + " OR " + ddlAdmBatch.SelectedValue + " =0)", "A.SEMESTERNO");
                lvStudents.DataSource = null;
                lvStudents.DataBind();
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlSemester.SelectedIndex = 0;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlInsName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlInsName.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlInsName.SelectedValue + " AND B.DEPTNO  IN (" + Session["userdeptno"].ToString() + ")", "D.DEGREENO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlInsName.SelectedValue, "D.DEGREENO");
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
            }
            //objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON A.DEGREENO=B.DEGREENO", "B.DEGREENO", "DEGREENAME", "B.DEGREENO > 0 AND COLLEGE_ID=" + ddlInsName.SelectedValue + "", "B.DEGREENO");
            lvStudents.DataSource = null;
            lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    #endregion DDL

    #region Excel Report
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        GridView gv = new GridView();
        int collegeid = ddlInsName.SelectedIndex > 0 ? Convert.ToInt32(ddlInsName.SelectedValue) : 0;
        int admbatch = ddlAdmBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmBatch.SelectedValue) : 0;
        int degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        int semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        DataSet ds = objSC.Get_Section_Regno_Allotment_Excel(collegeid, admbatch, degreeno, branchno, semesterno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            string attachment = "attachment ; filename=Section_Regno_Allotment_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.ContentType = "application/ms-excel";
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }
    }

    #endregion Excel Report

}
