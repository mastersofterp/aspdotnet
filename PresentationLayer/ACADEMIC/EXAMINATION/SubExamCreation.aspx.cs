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
using System.Collections.Generic;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_MASTERS_SubExamCreation : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamC = new ExamController();
    Exam ObjE = new Exam();
    static int subexamno;

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["OrgId"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
            BindListView();            
            this.PopulatePattern();
            ViewState["action"] = "add";
           // GenerateDyanamicJavaScript();
        }
       
    }

//    private void GenerateDyanamicJavaScript()
//    {
//        DataSet ds = objCommon.FillDropDown("REFF", "Table_Btn_Setting", "", "", "");
//        string[] btnArr = Convert.ToString(ds.Tables[0].Rows[0]["Table_Btn_Setting"]).Split(',');

//        string ScriptToLoad = @"
//                                var table = $('#example').DataTable({
//                                lengthChange: false,
//                                buttons: [
//                                    {
//                                        extend: 'copyHtml5',
//                                        text: '<i class=""fa fa-files-o""></i>',
//                                        titleAttr: 'Copy',
//                                        exportOptions: {
//                                            columns: ""thead th:not(th:first)""
//                                        }
//                                    },
//                                    {
//                                        extend: 'excelHtml5',
//                                        text: '<i class=""fa fa-file-excel-o""></i>',
//                                        titleAttr: 'Excel',
//                                        exportOptions: {
//                                            columns: ""thead th:not(th:first)""
//                                        }
//                                    },
//                                    {
//                                        extend: 'pdfHtml5',
//                                        text: '<i class=""fa fa-file-pdf-o""></i>',
//                                        titleAttr: 'Pdf',
//                                        exportOptions: {
//                                            columns: ""thead th:not(th:first)""
//                                        }
//                                    },
//                                    {
//                                        extend: 'print',
//                                        text: '<i class=""fa fa-print""></i>',
//                                        titleAttr: 'Print',
//                                        exportOptions: {
//                                            columns: ""thead th:not(th:first)""
//                                        }
//                                    },
//                                    {
//                                        extend: 'colvis',
//                                        text: '<i class=""fa fa-eye""></i>',
//                                        titleAttr: 'Column Visibility'
//                                    }
//                                ]
//                            });
//
//                            table.buttons().container().appendTo('#example_wrapper .col-sm-6:eq(0)');
//
//                            var CopyVal =" + btnArr[0] + ";" + "var ExcelVal = " + btnArr[1] + ";" + "var PdfVal = " + btnArr[2] + ";" + "var ColvisVal = " + btnArr[3] + ";" +

//                            @"var table = $('#example').DataTable();
//
//                            if (CopyVal) {
//                                table.button(0).enable();
//                            }
//                            else {
//                                table.button(0).disable();
//                            }
//
//                            if (ExcelVal) {
//                                table.button(1).enable();
//                            }
//                            else {
//                                table.button(1).disable();
//                            }
//
//                            if (PdfVal) {
//                                table.button(2).enable();
//                            }
//                            else {
//                                table.button(2).disable();
//                            }
//
//                            if (ColvisVal) {
//                                table.button(3).enable();
//                            }
//                            else {
//                                table.button(3).disable();
//                            }
//
//
//                            var prm = Sys.WebForms.PageRequestManager.getInstance();
//                            prm.add_endRequest(function () {
//                                var table = $('#example').DataTable({
//                                    lengthChange: false,
//                                    //buttons: ['copy', 'excel', 'pdf', 'colvis']
//                                    buttons: [
//                                        {
//                                            extend: 'copyHtml5',
//                                            text: '<i class=""fa fa-files-o""></i>',
//                                            titleAttr: 'Copy',
//                                            exportOptions: {
//                                                columns: ""thead th:not(th:first)""
//                                            }
//                                        },
//                                        {
//                                            extend: 'excelHtml5',
//                                            text: '<i class=""fa fa-file-excel-o""></i>',
//                                            titleAttr: 'Excel',
//                                            exportOptions: {
//                                                columns: ""thead th:not(th:first)""
//                                            }
//                                        },
//                                        {
//                                            extend: 'pdfHtml5',
//                                            text: '<i class=""fa fa-file-pdf-o""></i>',
//                                            titleAttr: 'Pdf',
//                                            exportOptions: {
//                                                columns: ""thead th:not(th:first)""
//                                            }
//                                        },
//                                        {
//                                            extend: 'print',
//                                            text: '<i class=""fa fa-print""></i>',
//                                            titleAttr: 'Print',
//                                            exportOptions: {
//                                                columns: ""thead th:not(th:first)""
//                                            }
//                                        },
//                                        {
//                                            extend: 'colvis',
//                                            text: '<i class=""fa fa-eye""></i>',
//                                            titleAttr: 'Column Visibility'
//                                        }
//                                    ]
//                                });
//
//                                table.buttons().container()
//                                .appendTo('#example_wrapper .col-sm-6:eq(0)');
//
//                                var CopyVal =" + btnArr[0] + ";" + "var ExcelVal = " + btnArr[1] + ";" + "var PdfVal = " + btnArr[2] + ";" + "var ColvisVal = " + btnArr[3] + ";" +

//                                @"var table = $('#example').DataTable();
//
//                                if (CopyVal) {
//                                    table.button(0).enable();
//                                }
//                                else {
//                                    table.button(0).disable();
//                                }
//
//                                if (ExcelVal) {
//                                    table.button(1).enable();
//                                }
//                                else {
//                                    table.button(1).disable();
//                                }
//
//                                if (PdfVal) {
//                                    table.button(2).enable();
//                                }
//                                else {
//                                    table.button(2).disable();
//                                }
//
//                                if (ColvisVal) {
//                                    table.button(3).enable();
//                                }
//                                else {
//                                    table.button(3).disable();
//                                }
//                            });
//                        ";

//        ScriptManager.RegisterStartupScript(this, GetType(), "script", ScriptToLoad, true);
//    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SubExamCreation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SubExamCreation.aspx");
        }
    }

    private void PopulatePattern()
    {
        try
        {   
            //objCommon.FillDropDownList(ddlExamNo, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "EXAMNAME <> ''", "EXAMNO");
             //objCommon.FillDropDownList(ddlExamPattern, "ACD_EXAM_PATTERN", "PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0", "PATTERNNO");
             objCommon.FillDropDownList(ddlExamPattern, "ACD_EXAM_PATTERN WITH (NOLOCK)", "PATTERNNO", "PATTERN_NAME", "ISNULL(ACTIVESTATUS,0)=1", "PATTERNNO");
             objCommon.FillDropDownList(ddlSubID, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID>0", "SUBID");
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Activity_ActivityMaster.PopulatePattern --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (hftimeslot.Value == "true")
            {
                ObjE.ActiveStatus = true;
            }
            else
            {
                ObjE.ActiveStatus = false;
            }
            GradeController objGC = new GradeController();

            ObjE.ExamNo = ddlExamName.SelectedValue;
            ObjE.PATTERNNO = Convert.ToInt32(ddlExamPattern.SelectedValue);
            ObjE.CollegeCode = Session["colcode"].ToString();
            ObjE.FieldName = txtFieldName.Text;
            ObjE.SubExamname = txtsubExamName.Text;
            ObjE.OrgId = Convert.ToInt32(Session["OrgId"]);
            ObjE.MAXMARKS = Convert.ToDecimal(txtMaxMark.Text);
            ObjE.Subid = Convert.ToInt32(ddlSubID.SelectedValue);
            int Sub_Fixed = 0;
            if (rdoFixed.SelectedValue == string.Empty)
            {
                Sub_Fixed = 0;
            }
            else
            {
                Sub_Fixed = Convert.ToInt32(rdoFixed.SelectedValue);
            }
                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objExamC.AddSubExam(ObjE, Sub_Fixed);
                        if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(updGrade, "Record already exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(updGrade, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updGrade, "Error Adding Record!", this.Page);
                        }
                    }
                    else
                    {
                        ObjE.SubExamNo = Convert.ToInt32(ViewState["subexamno"]);
                        CustomStatus cs = (CustomStatus)objExamC.UpdateSubExam(ObjE,Sub_Fixed);

                        if (cs.Equals(CustomStatus.DuplicateRecord))   // Added by Abhinay Lad [14-06-2019]
                        {
                            objCommon.DisplayMessage(updGrade, "Record already exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updGrade, "Record Updated Successfully!", this.Page);
                            btnSave.Text = "Submit";
                            btnSave.CssClass = "btn btn-success";
                           // txtsubExamName.Focus();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updGrade, "Error in Updating Record!", this.Page);
                        }

                    }

                    BindListView();

                }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void Clear()
    {
        txtsubExamName.Text = string.Empty;
        //Label1.Text = string.Empty;
        ddlExamPattern.SelectedIndex = 0;
        ddlExamPattern.Enabled = true;
        ddlExamName.SelectedIndex = 0;
        ddlExamName.Enabled = true;
        txtFieldName.Text = string.Empty;
        txtMaxMark.Text = string.Empty;
        ddlSubID.SelectedIndex = 0;
        rdoFixed.SelectedValue="0";
    }

    private void BindListView()
    {
        try
        {           
            DataSet ds = objExamC.GetAllSubExam();
            lvGradeType.DataSource = ds;
            lvGradeType.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
        btnSave.Text = "Submit";
        btnSave.CssClass = "btn btn-success";
        txtsubExamName.Focus();
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    private void ShowDetail(int subexamNo)
    {
        SqlDataReader dr = objExamC.GetSubExambyNo(subexamNo);

        if (dr != null)
        {
            if (dr.Read())
            {
                //ddlExamPattern.SelectedValue = null;
                ddlExamPattern.SelectedValue = dr["PATTERNNO"] == null ? "0" : dr["PATTERNNO"].ToString();
                ddlExamPattern.Enabled = false;
                PopulateExam();

               // ddlExamPattern.SelectedValue = dr["PATTERNNO"] == null ? "0" : dr["PATTERNNO"].ToString();
                ddlExamName.SelectedValue = dr["EXAMNO"] == null ? "0" : dr["EXAMNO"].ToString();
                ddlExamName.Enabled = false;

                ddlSubID.SelectedValue = dr["SUBEXAM_SUBID"] == null ? "0" : dr["SUBEXAM_SUBID"].ToString();
                txtsubExamName.Text = dr["SUBEXAMNAME"] == null ? string.Empty : dr["SUBEXAMNAME"].ToString();
                txtFieldName.Text = dr["FLDNAME"] == null ? string.Empty : dr["FLDNAME"].ToString();
                rdoFixed.SelectedValue = dr["FIXED"].ToString();
                ViewState["subexamno"] = subexamNo.ToString();
                if (dr["ACTIVESTATUS"].ToString() == "Active" || dr["ACTIVESTATUS"].ToString() == "ACTIVE")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setsubexam(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setsubexam(false);", true);
                }
                txtMaxMark.Text = dr["MAXMARK"] == null ? "0" : dr["MAXMARK"].ToString();
               
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            subexamno = int.Parse(btnEdit.CommandArgument);
           // Label1.Text = string.Empty;

            ShowDetail(subexamno);
            ViewState["action"] = "edit";
            btnSave.Text = "Update";
            btnSave.CssClass = "btn btn-warning";
           // txtsubExamName.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {       
            PopulateExam();
            if (ddlExamPattern.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_SUBEXAM_NAME S INNER JOIN ACD_SUBJECTTYPE ST ON(ST.SUBID=S.SUBEXAM_SUBID) INNER JOIN ACD_EXAM_PATTERN P ON(P.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_EXAM_NAME E ON(E.EXAMNO=S.EXAMNO)", "SUBEXAMNO,SUBEXAMNAME,S.FLDNAME,S.EXAMNO,EXAMNAME", "case when isnull(S.ACTIVESTATUS,0)=0 THEN 'Inactive' ELSE 'Active' END AS ACTIVESTATUS,case when isnull(S.FIXED,0)=0 then 'No' when S.FIXED=1 then 'Yes' end as FIXED,MAXMARK,S.COLLEGE_CODE,S.PATTERNNO,SUBEXAM_SUBID,SUBNAME,S.OrganizationId,PATTERN_NAME", "SUBEXAMNO > 0 AND S.PATTERNNO=" + Convert.ToInt32(ddlExamPattern.SelectedValue), "SUBEXAMNO");
                lvGradeType.DataSource = ds;
                lvGradeType.DataBind();
            }
            else
            {
                ddlExamPattern.SelectedIndex = 0;
                ddlExamName.SelectedIndex = 0;
                ddlSubID.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateExam()
    {
        try
        {
            objCommon.FillDropDownList(ddlExamName, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "PATTERNNO=" + ddlExamPattern.SelectedValue + "AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"].ToString()) + "AND EXAMNAME <> '' AND ISNULL(ACTIVESTATUS,0)=1", "EXAMNO");  //AND ORGANIZATIONID="+Convert.ToInt32(Session["OrgId"].ToString())+ "
            //txtsubExamName.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Activity_ActivityMaster.PopulateExam --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    protected void lvGradeType_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                CheckBox chk = e.Item.FindControl("Chkstatus") as CheckBox;
                if (chk.ToolTip == "True")
                {
                    chk.Checked = true;
                    //chk.BackColor = Color.Green;
                }
                else
                {
                    chk.Checked = false;
                }

            }
        }
        catch { }
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlExamName.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_SUBEXAM_NAME S INNER JOIN ACD_SUBJECTTYPE ST ON(ST.SUBID=S.SUBEXAM_SUBID) INNER JOIN ACD_EXAM_PATTERN P ON(P.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_EXAM_NAME E ON(E.EXAMNO=S.EXAMNO)", "SUBEXAMNO,SUBEXAMNAME,S.FLDNAME,S.EXAMNO,EXAMNAME", "case when isnull(S.ACTIVESTATUS,0)=0 THEN 'Inactive' ELSE 'Active' END AS ACTIVESTATUS,case when isnull(S.FIXED,0)=0 then 'No' when S.FIXED=1 then 'Yes' end as FIXED,MAXMARK,S.COLLEGE_CODE,S.PATTERNNO,SUBEXAM_SUBID,SUBNAME,S.OrganizationId,PATTERN_NAME", "SUBEXAMNO > 0 AND S.PATTERNNO=" + Convert.ToInt32(ddlExamPattern.SelectedValue) + "AND S.EXAMNO=" + Convert.ToInt32(ddlExamName.SelectedValue), "SUBEXAMNO");
                lvGradeType.DataSource = ds;
                lvGradeType.DataBind();
            }
            else
            {
                ddlExamName.SelectedIndex = 0;
                ddlSubID.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSubID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubID.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_SUBEXAM_NAME S INNER JOIN ACD_SUBJECTTYPE ST ON(ST.SUBID=S.SUBEXAM_SUBID) INNER JOIN ACD_EXAM_PATTERN P ON(P.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_EXAM_NAME E ON(E.EXAMNO=S.EXAMNO)", "SUBEXAMNO,SUBEXAMNAME,S.FLDNAME,S.EXAMNO,EXAMNAME", "case when isnull(S.ACTIVESTATUS,0)=0 THEN 'Inactive' ELSE 'Active' END AS ACTIVESTATUS,case when isnull(S.FIXED,0)=0 then 'No' when S.FIXED=1 then 'Yes' end as FIXED,MAXMARK,S.COLLEGE_CODE,S.PATTERNNO,SUBEXAM_SUBID,SUBNAME,S.OrganizationId,PATTERN_NAME", "SUBEXAMNO > 0 AND S.PATTERNNO=" + Convert.ToInt32(ddlExamPattern.SelectedValue) + "AND S.EXAMNO=" + Convert.ToInt32(ddlExamName.SelectedValue) + "AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubID.SelectedValue), "SUBEXAMNO");
                lvGradeType.DataSource = ds;
                lvGradeType.DataBind();
            }
            else
            {
                ddlSubID.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}