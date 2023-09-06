//======================================================================================
// PROJECT NAME  : MIZORAM UNIVERSITY
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BULK IMPORT COURSE REG DATA OF UG STUDENTS
// CREATION DATE : 15-APRIL-2016
// ADDED BY      : MR. MANISH WALDE
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using System.Data;
using System.Data.OleDb;
using System.Data.Common;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_StudentCourseFileUploadUG : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    #region Page Load Functionality
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                }
                lblTotalMsg.Text = string.Empty;
            }
            divMsg.InnerHtml = string.Empty;
            lblTotalMsg.Text = string.Empty;
            lblTotalMsg.Style.Add("color", "red");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFileUpload.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        // FILL DROPDOWN Session
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
        // FILL DROPDOWN COLLEGE
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
        //Fill Dropdown Degree
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
        // FILL DROPDOWN SEMESTER
        //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
    }
    #endregion

    #region Page Events Functionality
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=A.DEGREENO", "DISTINCT A.DEGREENO", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + "AND CDB.UGPGOT IN (" + Session["ua_section"] + ")", "A.degreeno");
                ddlDegree.Focus();
            }

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
                ddlBranch.Focus();
            }

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT SR, ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "  SR.COLLEGEID = " + ddlCollege.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "S.SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "S.SEMESTERNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lbExcelFormat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        //string path = Server.MapPath(string.Empty).Replace("HOSTEL", "IMAGES\\HOSTEL_ALLOTMENT_FORMAT.xls");
        string path = Server.MapPath("~/ExcelFormat/Upload_Excel.xlsx");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Upload_Excel.xlsx\"");
        Response.TransmitFile(path);
        Response.End();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        ClearMismatchList();
        ClearCollChangeList();
    }

    private void ClearAll()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        lblTotalMsg.Text = string.Empty;
    }

    #endregion

    #region Upload excel report Functionality
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            ClearCollChangeList();
            ClearMismatchList();

            string path = Server.MapPath("~/ExcelData/");

            if (FileUpload1.HasFile)
            {
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                if (extension.Contains(".xls") || extension.Contains(".xlsx"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                        Directory.CreateDirectory(path);

                    string fileName = FileUpload1.PostedFile.FileName.Trim();

                    //string[] array1 = Directory.GetFiles(path);
                    //foreach (string str in array1)
                    //{
                    //    if ((path + fileName).Equals(str))
                    //    {
                    //        lblTotalMsg.Text = "File with similar name already exists.";
                    //        objCommon.DisplayMessage(UpdatePanel1, "File with similar name already exists.", this);
                    //        return;
                    //    }
                    //}

                    //Check file to server with same name and delete
                    if (File.Exists((path + fileName).ToString()))
                        File.Delete((path + fileName).ToString());

                    //Upload file to server
                    FileUpload1.SaveAs(path + fileName);

                    ////Import Data
                    CheckFormatAndImport(extension, path + fileName);

                    //if (!CheckFormatAndImport(extension, path + fileName))
                    //{
                    //    //objCommon.DisplayMessage(UpdatePanel1, "Records not saved, File is not in correct format. Please check if the data is saved in sheet1 or the column names do not match.", this.Page);
                    //    if (File.Exists((path + fileName).ToString()))
                    //        File.Delete((path + fileName).ToString());
                    //    //lblTotalMsg.Text = "Error!!";
                    //}
                }
                else
                {
                    lblTotalMsg.Text = "Only excel sheet is allowed to upload";
                    objCommon.DisplayMessage(UpdatePanel1, "Only excel sheet is allowed to upload.", this);
                    return;
                }
            }
            else
            {
                lblTotalMsg.Text = "Please select file to upload.";
                objCommon.DisplayMessage(UpdatePanel1, "Please select file to upload.", this);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            lblTotalMsg.Text = "Error.!! Record not saved.";
        }
    }

    private bool CheckFormatAndImport(string extension, string excelPath)
    {
        string conString = string.Empty;

        switch (extension)
        {
            case ".xls": //Excel 97-03
                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 or higher
                conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                break;
        }

        conString = string.Format(conString, excelPath);

        try
        {
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[] { new DataColumn("USNO", typeof(string)),
                new DataColumn("STUDENT NAME", typeof(string)),
                new DataColumn("SEE MARKS",typeof(string)),
                new DataColumn("BRANCH", typeof(string)),
                new DataColumn("SEMESTER", typeof(int)),
                new DataColumn("SECTION", typeof(string)),
                new DataColumn("COURSE CODE", typeof(string)) });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);


                    foreach (DataTable table in ds1.Tables)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            double num;

                            bool isNum = double.TryParse(dr["SEE MARKS"].ToString(), out num);
                            if (isNum)
                            {

                            }
                            else
                            {
                                if (dr["SEE MARKS"].ToString() == "AB" || dr["SEE MARKS"].ToString() == "NE" || dr["SEE MARKS"].ToString() == "WD" || dr["SEE MARKS"].ToString() == "DR" || dr["SEE MARKS"].ToString() == "")
                                {

                                }
                                else
                                {
                                    goto label1;
                                }
                            }
                        }
                    }


//                    string Str = textBox1.Text.Trim();

//double Num;

//bool isNum = double.TryParse(Str, out Num);

//if (isNum)
                }
                excel_con.Close();

                ImportDataMaster objIDM = new ImportDataMaster();
                ImportDataController objIDC = new ImportDataController();

                objIDM.SESSIONNO = Convert.ToInt16(ddlSession.SelectedValue);
                objIDM.COLLEGEID = Convert.ToInt16(ddlCollege.SelectedValue);
                objIDM.DEGREENO = Convert.ToInt16(ddlDegree.SelectedValue);
                objIDM.BRANCHNO = Convert.ToInt16(ddlBranch.SelectedValue);
                objIDM.SEMESTERNO = Convert.ToInt16(ddlSemester.SelectedValue);
                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

                int retout = 0;
                int retout1 = 0;
                DataSet ds = null;
                ds = objIDC.ImportCourseRegDataUG_Bulk(objIDM, dtExcelData, Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlStudentType.SelectedValue));
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        retout = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                        if (retout == -1002)
                        {
                            tblusn.Visible = true;
                        }
                        else { tblusn.Visible = false; }
                    }
                    if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        retout1 = Convert.ToInt32(ds.Tables[1].Rows[0]["STATUS"].ToString());
                        if (retout1 == -1002)
                        {
                            tblList.Visible = true;
                        }
                        else
                        { tblList.Visible = false; }
                    }
                    if (retout == 1 && retout1 == 1)
                    {
                        retout = 1;
                    }
                    else
                    {
                        retout = -1002;
                    }
                    
                }
                switch (retout)
                {
                    case 1:     ///Verified and course registered successfully
                        ClearMismatchList();
                        ClearCollChangeList();
                        btnVerifyRegister.Enabled = true;
                        lblTotalMsg.Text = "Records verified  successfully.";
                        lblTotalMsg.Style.Add("color", "green");
                        objCommon.DisplayMessage(UpdatePanel1, "Records verified successfully", this.Page);
                       
                        break;
                    case -99:   ///Error occoured while saving
                        ClearMismatchList();
                        ClearCollChangeList();
                        lblTotalMsg.Text = "Error occurred in record verification.";
                        objCommon.DisplayMessage(UpdatePanel1, "Error occurred in record verification.", this.Page);
                        break;
                    case -1001: ///Registration already exists cannot register
                        ClearMismatchList();
                        ClearCollChangeList();
                        lblTotalMsg.Text = "";
                        objCommon.DisplayMessage(UpdatePanel1, "", this);
                        break;
                    case -1002: ///Records mismatch Not verified
                        ClearCollChangeList();
                        lvCourse.DataSource = ds.Tables[1];
                        lvCourse.DataBind();
                       
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        btnVerifyRegister.Enabled = false;
                        lblTotalMsg.Text = "Mismatch Found. Records not verified.\nPlease correct the excel sheet records and re-upload it.";
                        objCommon.DisplayMessage(UpdatePanel1, "Mismatch Found. Records not verified.\nPlease correct the excel sheet records and re-upload it.", this.Page);
                        break;
                    

                }
            label1:
                objCommon.DisplayMessage(UpdatePanel1, "Record not saved, file is not in correct format. Please check the SEE Marks & upload again.", this.Page);
                return true;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record not saved, file is not in correct format. Please check the format of file & upload again.", this.Page);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
    
    
    }
    #endregion

    #region Verify Imported Excel Functionality
    protected void btnVerifyRegister_Click(object sender, EventArgs e)
    {
        int cntExcelUpload = 0;

        ClearCollChangeList();
        ClearMismatchList();

        cntExcelUpload = Convert.ToInt16(objCommon.LookUp("TEMP_STUD_RESULT", "COUNT(REGNO)", "SESSIONNO=" + ddlSession.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND COLLEGE_ID=" + ddlCollege.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND PREV_STATUS=" + ddlStudentType.SelectedValue));

        if (cntExcelUpload > 0)
        {


            //int cntRegExist = 0;
            //cntRegExist = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON S.IDNO=SR.IDNO", "COUNT(REGNO)", "SR.SESSIONNO=" + ddlSession.SelectedValue + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue + " AND SR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SR.PREV_STATUS=" + ddlStudentType.SelectedValue +" AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.EXAM_REGISTERED,0)=1"));

            //if (cntRegExist == 0)
            //{
            VerifyandRegister();
            //}
            //else
            //{
            //    objCommon.DisplayMessage(UpdatePanel1, "Given selection students already registered course.", this);
            //}
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Excel sheet is not uploaded for given selection. First upload excel sheet then verify and register.", this);
        }
    }

    private void VerifyandRegister()
    {
        ImportDataMaster objIDM = new ImportDataMaster();
        ImportDataController objIDC = new ImportDataController();

        objIDM.SESSIONNO = Convert.ToInt16(ddlSession.SelectedValue);
        objIDM.COLLEGEID = Convert.ToInt16(ddlCollege.SelectedValue);
        objIDM.DEGREENO = Convert.ToInt16(ddlDegree.SelectedValue);
        objIDM.BRANCHNO = Convert.ToInt16(ddlBranch.SelectedValue);
        objIDM.SEMESTERNO = Convert.ToInt16(ddlSemester.SelectedValue);
        objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        //Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlStudentType.SelectedValue)

        DataSet ds = null;

        int retout = 0;
        ds = objIDC.VerifyandRegisterImportedCourseRegDataUG_Bulk(objIDM, Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlStudentType.SelectedValue));//, out retout

        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                retout = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
            }
            //else
            //{
            //    lvCourse.DataSource = null;
            //    lvCourse.DataBind();
            //    lvStudent.DataSource = null;
            //    lvStudent.DataBind();
            //    tblList.Visible = false;
            //}
        }
        switch (retout)
        {
            case 1:     ///Verified and course registered successfully
                ClearMismatchList();
                ClearCollChangeList();
                lblTotalMsg.Text = "SEE marks are saved and locked successfully.";
                lblTotalMsg.Style.Add("color", "green");
                objCommon.DisplayMessage(UpdatePanel1, "Records verified and SEE marks are saved successfully", this.Page);
                break;
            case -99:   ///Error occoured while saving
                ClearMismatchList();
                ClearCollChangeList();
                lblTotalMsg.Text = "Error occurred in record verification.";
                objCommon.DisplayMessage(UpdatePanel1, "Error occurred in record verification.", this.Page);
                break;
            case -1001: ///Registration already exists cannot register
                ClearMismatchList();
                ClearCollChangeList();
                lblTotalMsg.Text = "";
                objCommon.DisplayMessage(UpdatePanel1, "", this);
                break;
            case -1002: ///Records mismatch Not verified
                ClearCollChangeList();
                //lvCourse.DataSource = ds.Tables[0];
                //lvCourse.DataBind();
                //Panel2.Visible = true;
                //lvStudent.DataSource = ds.Tables[1];
                //lvStudent.DataBind();
                //Panel3.Visible = true;
                //tblList.Visible = true;
                lblTotalMsg.Text = "Mismatch Found. Records not verified.\nPlease correct the excel sheet records and re-upload it.";
                objCommon.DisplayMessage(UpdatePanel1, "Mismatch Found. Records not verified.\nPlease correct the excel sheet records and re-upload it.", this.Page);
                break;
            case -1003: ///College change found, records mismatch Not verified
                ClearMismatchList();
                lvCollegeChange.DataSource = ds.Tables[2];
                lvCollegeChange.DataBind();
                tblCollegeChange.Visible = true;
                lblTotalMsg.Text = "";
                objCommon.DisplayMessage(UpdatePanel1, "", this.Page);
                break;
        }

    }

    private void ClearMismatchList()
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        // tblList.Visible = false;
    }

    private void ClearCollChangeList()
    {
        lvCollegeChange.DataSource = null;
        lvCollegeChange.DataBind();
        // tblCollegeChange.Visible = false;
    }

    #endregion

    #region Change college Functionality
    protected void btnCollegeChange_Click(object sender, EventArgs e)
    {
        string idnos = "";

        foreach (ListViewDataItem dataitem in lvCollegeChange.Items)
        {
            if ((dataitem.FindControl("chkSelect") as CheckBox).Checked == true)
                idnos += (dataitem.FindControl("hdnidno") as HiddenField).Value + "$";

        }
        if (idnos.Trim().Length == 0)
        {
            lblTotalMsg.Text = "Please select atleast one student from list for changing college.";
            objCommon.DisplayMessage(UpdatePanel1, "Please select atleast one student from list for changing college.", this.Page);
            return;
        }

        ImportDataMaster objIDM = new ImportDataMaster();
        ImportDataController objIDC = new ImportDataController();

        objIDM.SESSIONNO = Convert.ToInt16(ddlSession.SelectedValue);
        objIDM.COLLEGEID = Convert.ToInt16(ddlCollege.SelectedValue);
        //objIDM.DEGREENO = Convert.ToInt16(ddlDegree.SelectedValue);
        //objIDM.BRANCHNO = Convert.ToInt16(ddlBranch.SelectedValue);
        objIDM.SEMESTERNO = Convert.ToInt16(ddlSemester.SelectedValue);
        objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        idnos = idnos.TrimEnd('$');

        int ret = 0;
        ret = objIDC.ChangeCollegeUG_Bulk(objIDM, Convert.ToInt16(Session["userno"]), idnos);

        if (ret > 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Total " + ret + " students college changed successfully. Click on Verify and Register button to register.", this.Page);
            lblTotalMsg.Text = "Total " + ret + " students college changed successfully. Click on Verify and Register button to register.";
            ClearCollChangeList();
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Error occured in changing collge of students.", this.Page);
            lblTotalMsg.Text = "Error occured in changing collge of students.";
        }
    }
    #endregion
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_bulkupload.rpt");//rptMarksList1.rpt
        }
        catch (Exception ex)
        { }
    }
    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {
       // string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_USER=" + Session["userfullname"].ToString();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
    }
}