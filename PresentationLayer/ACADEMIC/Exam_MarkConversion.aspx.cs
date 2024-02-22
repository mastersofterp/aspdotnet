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
using System.Collections.Generic;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_Exam_MarkConversion : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objexam = new ExamController();

    #region MasterPage
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #endregion End MasterPage set

    #region Page Load
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
               // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //Populate the Drop Down Lists
            //PopulateDropDownList();
            PopulateDropDownDeptMap();  
        }
    }
    #endregion End Page Load

    #region Check For Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }
    #endregion End Authentication

 
    #region Fill DropdownList For CourseType based on Session
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        txtmarks.Text = string.Empty;
        txtmarks.Enabled = false;
        ddlsubjecttype.SelectedIndex = 0;
        #region old
        //if (Convert.ToInt32(Session["usertype"]) == 1)
        //{
        //    objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER   C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + "", "C.COURSENO");
        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + ")", "C.COURSENO");
        //}
        #endregion
        if (ddlSession.SelectedIndex > 0)
        {
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
            }
            else
            {
                //  objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
            }

            ddlsubjecttype.Focus();
        }
        else
        {
            ddlsubjecttype.Items.Clear();
            ddlsubjecttype.Items.Add("Please Select");
            ddlsubjecttype.SelectedItem.Value = "0";

            ddlExamName.Items.Clear();
            ddlExamName.Items.Add("Please Select");
            ddlExamName.SelectedItem.Value = "0";

            ddlconversion.SelectedValue = "0";
            ddlSession.Focus();
        }
    }
    #endregion  End Fill DropdownList For CourseType based on Session


    private void PopulateDropDownDeptMap()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollegeIdDepMap, "ACD_COLLEGE_MASTER", "  COLLEGE_ID ", "COLLEGE_NAME", "COLLEGE_ID in (" + ViewState["College_ID"].ToString() + ")", "COLLEGE_ID ASC");

            objCommon.FillDropDownList(ddlCollegeIdDepMap, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"].ToString() + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollegeIdDepMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        txtmarks.Text = string.Empty;
        txtmarks.Enabled = false;
        try
        {
            if (ddlCollegeIdDepMap.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeIdDepMap.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // FILL DROPDOWN  ddlSession_SelectedIndexChanged
                }

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_PNAME ", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ISNULL (IS_ACTIVE,0)= 1 AND SESSIONNO > 0", "SESSIONNO DESC");
                ddlSession.SelectedIndex = 0;
                ddlSession.Focus();
            }
            else
            {
                ddlSession.Items.Clear();
                ddlSession.Items.Add("Please Select");
                ddlSession.SelectedItem.Value = "0";

                ddlsubjecttype.Items.Clear();
                ddlsubjecttype.Items.Add("Please Select");
                ddlsubjecttype.SelectedItem.Value = "0";

                ddlExamName.Items.Clear();
                ddlExamName.Items.Add("Please Select");
                ddlExamName.SelectedItem.Value = "0";

                ddlconversion.SelectedValue = "0";
                ddlCollegeIdDepMap.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Exam_MarkConversion.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlsubjecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        txtmarks.Text = string.Empty;
        txtmarks.Enabled = false;

        if (ddlsubjecttype.SelectedIndex > 0)
        {
            string schemeno = objCommon.LookUp("acd_course", "schemeno", "courseno=" + ddlsubjecttype.SelectedValue);
            string patterno = objCommon.LookUp("acd_scheme", "patternno", "schemeno=" + schemeno);
            objCommon.FillDropDownList(ddlExamName, "acd_exam_name", "examno", "examname", "patternno = " + patterno + "and examname not Like'End%' and examname<>''", "examno");
            //ShowStudent();
            ddlExamName.Focus();
        }
        else
        {
            ddlExamName.Items.Clear();
            ddlExamName.Items.Add("Please Select");
            ddlExamName.SelectedItem.Value = "0";

            ddlconversion.SelectedValue = "0";
            ddlsubjecttype.Focus();
        }
    }
    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;

        if (ddlExamName.SelectedIndex > 0)
        {
            txtmarks.Enabled = true;
            ddlconversion.Focus();
        }
        else
        {
            ddlExamName.Focus();
            ddlconversion.SelectedValue = "0";

            txtmarks.Text = string.Empty;
            txtmarks.Enabled = false;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlCollegeIdDepMap.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select College", this.Page);
            return;
        }
        if (ddlSession.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select Session", this.Page);
            return;
        }
        if (ddlsubjecttype.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select Course/Subject", this.Page);
            return;
        }
        if (ddlExamName.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select Exam Name", this.Page);
            return;
        }
          ShowStudent();


    }
    protected void ShowStudent()
    {

        DataSet dshead = null;
        string sp_head = "PKG_ACD_GET_SUBEXAM_HEAD";
        string param = "@P_COURSENO,@P_EXAMNO";
        string call_valueHead = "" + ddlsubjecttype.SelectedValue + "," + ddlExamName.SelectedValue;

        dshead = objCommon.DynamicSPCall_Select(sp_head, param, call_valueHead);

        ViewState["dshead"] = dshead;
        DataTable dt = new DataTable();
        if (dshead != null && dshead.Tables.Count > 0 && dshead.Tables[0].Rows.Count > 0)
        {
            dt = dshead.Tables[0];
            DataRow[] dr = dt.Select("");
           //string[] columnNames = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray(); 
            string str = string.Empty;
            string str1 = string.Empty;
            int td = 0;
            int colcont = dshead.Tables[0].Columns.Count;
            ViewState["colcount"] = colcont.ToString();
            //int marks = colcont + 3;

            for (int i = 0; i < colcont; i++)
            {

                str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_mark').attr('colspan'," + colcont + ");$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
            }
            int z = 4;
           
            for (int j = 0; j < colcont; j++)
            {
                str1 += "$('#th" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();";
     
                z++;
            }
            string str3 = str + str1;
            ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);
        }
        else
        {
            objCommon.DisplayMessage(this.updSession, "No Records", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudent.Visible = false;
            return;
        }

        DataSet dsStudent = null;
        //dsStudent = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON(R.REGNO = S.REGNO)", "DISTINCT R.REGNO", "S.STUDNAME", "SESSIONNO=" + ddlSession.SelectedValue + "AND COURSENO=" + ddlsubjecttype.SelectedValue, "S.STUDNAME");

        string sp_name = "PKG_GET_STUDENT_MARKS_EXAM_WISE";
        string parameter = "@P_SESSIONNO,@P_COURSENO,@P_EXAMNO";
        string Call_Value = ""+ddlSession.SelectedValue+","+ddlsubjecttype.SelectedValue+","+ddlExamName.SelectedValue;
       dsStudent = objCommon.DynamicSPCall_Select(sp_name, parameter, Call_Value);

        if (dsStudent.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = dsStudent;
            lvStudent.DataBind();
            lvStudent.Visible = true;
            int arrVal = 0;
            int y = 0;
            string[] arr_Label = new string[] { "lbl0", "lbl1", "lbl2", "lbl3", "lbl4", "lbl5" };

            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {

                for (; y < dsStudent.Tables[0].Rows.Count; )
                {
                    for (int j = 3; j < dsStudent.Tables[0].Columns.Count; j++)
                    {
                        if (Convert.ToString(dsStudent.Tables[0].Rows[y][j]) != "")
                        {
                            int CL = Convert.ToInt32(dsStudent.Tables[0].Columns[j].ColumnName);
                            ((Label)lvitem.FindControl(arr_Label[arrVal])).Text = Convert.ToString(dsStudent.Tables[0].Rows[y][j]) != "-1.00" ? Convert.ToString(dsStudent.Tables[0].Rows[y][j]) : "";

                            // arrVal++;

                            if (j + 2 > Convert.ToInt32(dsStudent.Tables[0].Columns.Count))
                            {
                                arrVal = 0;
                                break;
                            }
                        }
                        arrVal++;
                    }
                    y++;
                    break;
                }
                arrVal = 0;
            }
            CheckBox cb = lvStudent.FindControl("cbHead") as CheckBox;
            cb.Checked = false;

        }
        else
        {
            objCommon.DisplayMessage(this.updSession, "No Records", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudent.Visible = false;
            return;
        }

        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void clear()
    {
        ddlCollegeIdDepMap.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlsubjecttype.SelectedIndex = 0;
        ddlExamName.SelectedIndex = 0;
        ddlconversion.SelectedIndex = 0;

        txtmarks.Text = string.Empty;
        txtmarks.Enabled = false;

        lvStudent.Visible = false;
        divmark.Visible = false;

        ddlCollegeIdDepMap.Focus();
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked && (item.FindControl("chkStudent") as CheckBox).Enabled == true)
                {
                    studentIds += (item.FindControl("chkStudent") as CheckBox).ToolTip + "$";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Exam_MarkConversion.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       // string stdID = studentIds.Remove(studentIds.Length - 1);
        return studentIds;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Using function In Controller
        //string Ids = GetStudentIDs();
        //if (Ids == string.Empty)
        //{
        //    objCommon.DisplayUserMessage(this.updSession, "Select Student/Students", this.Page);
        //    ShowStudent();
        //    return;
        //}
        //Exam objExamEntity = new Exam();
        //objExamEntity.collegeid = Convert.ToInt32(ddlCollegeIdDepMap.SelectedValue);
        //objExamEntity.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //objExamEntity.Courseno = Convert.ToInt32(ddlsubjecttype.SelectedValue);
        //objExamEntity.ExamNo = ddlExamName.SelectedValue;
        //objExamEntity.MarkForConversion = Convert.ToDecimal(txtmarks.Text);
       
        //CustomStatus cs = (CustomStatus)objexam.SaveFinalMarkConversion(objExamEntity, Ids);
        //if (cs.Equals(CustomStatus.RecordSaved))
        //{
        //    objCommon.DisplayUserMessage(this.updSession, "Mark Conversion Successful", this.Page);
        //    ShowStudent();
        //    clear();
        //}
        //else
        //{
        //    objCommon.DisplayUserMessage(this.updSession, "Mark Conversion Failed", this.Page);
        //    ShowStudent();
        //}
        #endregion

        string Ids = GetStudentIDs();
        if (Ids == string.Empty)
        {
            objCommon.DisplayUserMessage(this.updSession, "Select Student/Students", this.Page);
            ShowStudent();
            return;
        }
        string uano = Session["userno"].ToString();
        string mac = Session["macAddress"].ToString();
        string ipadd = Session["ipAddress"].ToString();

        string sp_name=string.Empty;
        string param = string.Empty;
        string call_valueHead =string.Empty;
        string result = string.Empty;

        if (Convert.ToInt32(ddlconversion.SelectedValue) == 3)
        {
            //PKG_ACD_EXAM_WISE_MARK_CONVERSION_RCPIPER
            sp_name = "PKG_ACD_EXAM_WISE_MARK_CONVERSION";
            param = "@P_SESSIONNO,@P_COURSENO,@P_COLLEGE_ID ,@P_STUDIDS,@P_EXAMNO,@P_MARKS,@P_IPADD,@P_UANO,@P_OUT ";
            call_valueHead = "" + ddlSession.SelectedValue + "," + ddlsubjecttype.SelectedValue + "," + Convert.ToInt32(ViewState["college_id"].ToString()) + "," + Ids + "," + ddlExamName.SelectedValue + "," + txtmarks.Text + "," + ipadd + "," + uano + "," + 0;
            result = objCommon.DynamicSPCall_IUD(sp_name, param, call_valueHead, true);
        }
        if (Convert.ToInt32(ddlconversion.SelectedValue) == 1 || Convert.ToInt32(ddlconversion.SelectedValue) == 2)
        {
            sp_name = "PKG_ACD_EXAM_WISE_MARK_CONVERSION_RCPIPER";
            param = "@P_SESSIONNO,@P_COURSENO,@P_COLLEGE_ID ,@P_STUDIDS,@P_EXAMNO,@P_IPADD,@P_UANO,@P_CONVERSION_TYPE,@P_OUT ";
            call_valueHead = "" + ddlSession.SelectedValue + "," + ddlsubjecttype.SelectedValue + "," + Convert.ToInt32(ViewState["college_id"].ToString()) + "," + Ids + "," + ddlExamName.SelectedValue + "," + ipadd + "," + uano + "," + Convert.ToInt32(ddlconversion.SelectedValue) + "," + 0;
            result = objCommon.DynamicSPCall_IUD(sp_name, param, call_valueHead, true);
        }

        if (result == "2")
        {
            objCommon.DisplayUserMessage(this.updSession, "Mark Conversion Successfully Completed", this.Page);
            ShowStudent();
            txtmarks.Text = string.Empty;
            //clear();
        }
        else
        {
            objCommon.DisplayUserMessage(this.updSession, "Mark Conversion Failed", this.Page);
            ShowStudent();
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
    //    lvStudent.Visible = false;
        
        //string chk = this.GetStudentIDs();
        //if (chk == string.Empty)
        //{

        //    //objCommon.DisplayUserMessage(this.updSession, "Select Student/Students", this.Page);
        //    //ShowStudent();
        //    //return;
        //}
        //else if (chk != string.Empty)
        //{
            this.ShowReport("ExamMarkReport", "rptMarkAfterConversion.rpt");
            ShowStudent();
        //}
        
   
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        
        try
        {
            //string stdids = this.GetStudentIDs();
            //if (stdids == string.Empty)
            //{
            //    foreach (ListViewDataItem item in lvStudent.Items)
            //    {   
            //            stdids += (item.FindControl("chkStudent") as CheckBox).ToolTip + "$";
            //    }
            //}
           
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + ddlsubjecttype.SelectedValue + ",@P_EXAMNO=" + ddlExamName.SelectedValue + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlconversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlconversion.SelectedValue == "3")
        {
            divmark.Visible = true;
            txtmarks.Focus();
            txtmarks.Text = "";
        }
        else
        {
            divmark.Visible = false;
            btnShow.Focus();
        }
    }
}