using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Web;
using System.Data.OleDb;

using System.IO;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text;

public partial class ACADEMIC_REPORTS_Testprep_Data : System.Web.UI.Page
{
    #region ----- Intilize Class Object ------------

    TestPrepThroughtMISEntity objTestPrepThroughtMISEntity = new TestPrepThroughtMISEntity();

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ImportDataController objIDC = new ImportDataController();

    #endregion ----- Intilize Class Object ------------

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)


                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "IS_ACTIVE > 0", "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlSession_1, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "IS_ACTIVE > 0", "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlApiSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "IS_ACTIVE > 0", "SESSIONNO DESC"); //api

                    objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlSchool2, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

                    Session["TestPrepExamAPI"] = objCommon.LookUp("[DBO].[REFF]", "ISNULL(TEST_PREP_EXAM_API,'')", "") == "" ? "" : objCommon.LookUp("[DBO].[REFF]", "ISNULL(TEST_PREP_EXAM_API,'')", "");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GridView GvStudent = new GridView();
        DataSet dsfee = objIDC.getTestPrepDataExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSchool.SelectedValue));
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=TestprepDataExcel.xlsx";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(updStudent, "Data Not Found", this.Page);
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //ddlSession.SelectedIndex = 0;
        //ddlSchool.SelectedIndex = 0;
        //ddlSchool2.SelectedIndex = 0;
        //rdbFilter.ClearSelection();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSchool.SelectedIndex = 0;
    }

    protected void rdbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Visible = false;
        ddlSession_1.Visible = false;
        ddlSchool.Visible = false;
        divSession.Visible = false;
        divSchool.Visible = false;
        btnExcel.Visible = false;
        btnCancel.Visible = false;
        btnCancel_1.Visible = false;
        //  Clear2.Visible = false;
        btnUploadData.Visible = false;
        btnUploadPending.Visible = false;
        divSchool2.Visible = false;
        ddlSchool2.Visible = false;
        fileUpload.Visible = false;
        ddlSchool2.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSession_1.SelectedIndex = 0;
        
        spanNote.Visible = false;
        divAttachfile.Visible = false;

        //Api Controlls
        ddlApiSession.Visible = false;
        divApiSubject.Visible = false;
        divApiExamName.Visible = false;
        ddlApiSession.Visible = false;
        ddlApiSubject.Visible = false;
        ddlApiExamName.Visible = false;
        btnApiSubmit.Visible = false;
        btnApiCancel.Visible = false;
        divExamMarksEntryDataThroughtApi.Visible = false;
        btnApiShow.Visible = false;

        if (rdbFilter.SelectedValue == "1")
        {
            ddlSession.Visible = true;
            ddlSchool.Visible = true;
            divSession.Visible = true;
            divSchool.Visible = true;
            btnExcel.Visible = true;
            btnCancel.Visible = true;
        }
        else if (rdbFilter.SelectedValue == "2")
        {
            ddlSession_1.Visible = true;
            ddlSchool2.Visible = true;
            divSession.Visible = true;
            divSchool2.Visible = true;
            btnUploadData.Visible = true;
            btnUploadPending.Visible = true;
            fileUpload.Visible = true;
            spanNote.Visible = true;
            divAttachfile.Visible = true;
            btnCancel_1.Visible = true;
        }
        else if (rdbFilter.SelectedValue == "3") //api code
        {
            divSession.Visible = true;
            ddlApiSession.Visible = true;
            divApiSubject.Visible = true;
            divApiExamName.Visible = true;
            //btnApiSubmit.Visible = true;
            btnApiCancel.Visible = true;
            ddlApiSession.Visible = true;
            ddlApiSubject.Visible = true;
            ddlApiExamName.Visible = true;

            SubjectApi();

            ddlApiExamName.Items.Clear();
            ddlApiExamName.Items.Add("Loading......");
            ddlApiExamName.SelectedItem.Value = "0";
        }

    }

    protected void btnUploadData_Click(object sender, EventArgs e)
    {
        Session["fileName"] = string.Empty;
        string fileType = string.Empty;
        Byte[] UserManual = null;
        string path = string.Empty;
        string fileName = string.Empty;
        path = MapPath("~/UPLOAD_FILES/TEST_PREP_UPLOADFILES");
        if (!(Directory.Exists(MapPath("~/UPLOAD_FILES/TEST_PREP_UPLOADFILES"))))
            Directory.CreateDirectory(path);

        fileName = fileUpload.FileName;
        if (fileUpload.HasFile)
        {
            string existpath = path + "\\" + fileName;
            string[] array1 = Directory.GetFiles(path);
            foreach (string str in array1)
            {
                if ((existpath).Equals(str))
                {
                    objCommon.DisplayMessage("File with similar name already exists!", this);
                    return;
                }
            }
        }

        string[] validFileTypes = { "xls", "xlsx" };
        //string ext = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
        //string ext1 = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
        string ext = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile)
        {
            objCommon.DisplayMessage(this.Page, "Upload .xls or .xlsx excel format only.", this);
            return;
        }
        else
        {

            Session["fileName"] = fileName;
            fileUpload.PostedFile.SaveAs(Server.MapPath("~//UPLOAD_FILES//TEST_PREP_UPLOADFILES//") + fileName);
            fileType = System.IO.Path.GetExtension(fileName);
            string FilePath = (Server.MapPath("~//UPLOAD_FILES//TEST_PREP_UPLOADFILES//") + fileName);
            Import_To_Grid(FilePath, ext);
        }
    }

    private void Import_To_Grid(string FilePath, string Extension)
    {
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
        conStr = String.Format(conStr, FilePath);

        OleDbConnection connExcel = new OleDbConnection(conStr);

        OleDbCommand cmdExcel = new OleDbCommand();

        OleDbDataAdapter oda = new OleDbDataAdapter();

        DataTable dt = new DataTable();

        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet

        connExcel.Open();

        DataTable dtExcelSchema;

        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

        connExcel.Close();

        //Read Data from First Sheet

        connExcel.Open();

        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

        oda.SelectCommand = cmdExcel;


        oda.Fill(dt);

        DataView dv1 = dt.DefaultView;
        //dv1.RowFilter = "isnull(srno,0)>0";
        DataTable dtNew = dv1.ToTable();

        DataRow dr = null;
        DataTable dtbulk = new DataTable("TBL_TESTPREP_EXAMDATA_INTO_MIS_BULK");
        dtbulk.Columns.Add("ExamName", typeof(string));
        dtbulk.Columns.Add("SubjectName", typeof(string));
        dtbulk.Columns.Add("RollNo", typeof(string));
        dtbulk.Columns.Add("PRNNo", typeof(string));
        dtbulk.Columns.Add("Name", typeof(string));
        dtbulk.Columns.Add("MobileNo", typeof(string));
        dtbulk.Columns.Add("MaxMarks", typeof(string));
        dtbulk.Columns.Add("MarksObtained", typeof(string));
        dtbulk.Columns.Add("ExamSubmitDate", typeof(DateTime));
        dtbulk.Columns.Add("ExamStatus", typeof(string));

        for (int i = 0; i < dtNew.Rows.Count; i++)
        {
            dr = dtbulk.NewRow();
            dr["ExamName"] = dtNew.Rows[i][0].ToString();
            dr["SubjectName"] = dtNew.Rows[i][1].ToString();
            dr["RollNo"] = dtNew.Rows[i][2].ToString();
            dr["PRNNo"] = dtNew.Rows[i][3].ToString();
            dr["Name"] = dtNew.Rows[i][4].ToString();
            dr["MobileNo"] = dtNew.Rows[i][5].ToString();
            dr["MaxMarks"] = dtNew.Rows[i][6].ToString();
            dr["MarksObtained"] = dtNew.Rows[i][7].ToString();
            dr["ExamSubmitDate"] = dtNew.Rows[i][8].ToString();
            dr["ExamStatus"] = dtNew.Rows[i][9].ToString();
            dtbulk.Rows.Add(dr);
        }

        connExcel.Close();

        if (dtbulk.Rows.Count > 0)
        {
            divExamMarksEntryExcelData.Visible = true;
            lvExamMarksEntryExcelData.DataSource = dtbulk;
            lvExamMarksEntryExcelData.DataBind();

            btnSubmit.Visible = true;
        }
        else
        {
            divExamMarksEntryExcelData.Visible = false;
            btnSubmit.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lvExamMarksEntryExcelData.Items.Count > 0)
        {

            DataRow dr = null;
            DataTable dtbulk = new DataTable("TBL_TESTPREP_EXAMDATA_INTO_MIS_BULK");
            dtbulk.Columns.Add("ExamName", typeof(string));
            dtbulk.Columns.Add("SubjectName", typeof(string));
            dtbulk.Columns.Add("RollNo", typeof(string));
            dtbulk.Columns.Add("PRNNo", typeof(string));
            dtbulk.Columns.Add("Name", typeof(string));
            dtbulk.Columns.Add("MobileNo", typeof(string));
            dtbulk.Columns.Add("MaxMarks", typeof(string));
            dtbulk.Columns.Add("MarksObtained", typeof(string));
            dtbulk.Columns.Add("ExamSubmitDate", typeof(DateTime));
            dtbulk.Columns.Add("ExamStatus", typeof(string));

            foreach (ListViewDataItem dataRow in lvExamMarksEntryExcelData.Items)
            {
                HiddenField hdfExamName = dataRow.FindControl("hdfExamName") as HiddenField;
                HiddenField hdfSubjectName = dataRow.FindControl("hdfSubjectName") as HiddenField;
                HiddenField hdfRollNo = dataRow.FindControl("hdfRollNo") as HiddenField;
                HiddenField hdfPRNNo = dataRow.FindControl("hdfPRNNo") as HiddenField;
                HiddenField hdfName = dataRow.FindControl("hdfName") as HiddenField;
                HiddenField hdfMobileNo = dataRow.FindControl("hdfMobileNo") as HiddenField;
                HiddenField hdfMaxMarks = dataRow.FindControl("hdfMaxMarks") as HiddenField;
                HiddenField hdfMarksObtained = dataRow.FindControl("hdfMarksObtained") as HiddenField;
                HiddenField hdfExamSubmitDate = dataRow.FindControl("hdfExamSubmitDate") as HiddenField;
                HiddenField hdfExamStatus = dataRow.FindControl("hdfExamStatus") as HiddenField;

                dr = dtbulk.NewRow();
                dr["ExamName"] = hdfExamName.Value;
                dr["SubjectName"] = hdfSubjectName.Value;
                dr["RollNo"] = hdfRollNo.Value;
                dr["PRNNo"] = hdfPRNNo.Value;
                dr["Name"] = hdfName.Value;
                dr["MobileNo"] = hdfMobileNo.Value;
                dr["MaxMarks"] = hdfMaxMarks.Value;
                dr["MarksObtained"] = hdfMarksObtained.Value.ToUpper() == "AB" ? "-1" : hdfMarksObtained.Value;
                dr["ExamSubmitDate"] = hdfExamSubmitDate.Value;
                dr["ExamStatus"] = hdfExamStatus.Value;
                dtbulk.Rows.Add(dr);
            }

            int ret = objIDC.getTestPrepExamDataUploadExcel(Convert.ToInt32(ddlSession_1.SelectedValue), Convert.ToInt32(ddlSchool2.SelectedValue), Session["fileName"].ToString(), dtbulk, Convert.ToInt32(Session["userno"]), Request.ServerVariables["REMOTE_ADDR"].ToString());
            if (ret > 0)
            {
                objCommon.DisplayMessage(this.Page, "End Sem Excel Marks Data Updated Successfully.", this);
                ddlSchool2.SelectedIndex = 0;
                ddlSession_1.SelectedIndex = 0;
                divExamMarksEntryExcelData.Visible = false;
                btnSubmit.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Sorry, Please try again...!", this);
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please upload data first...!", this);
            return;
        }
    }

    protected void btnUploadPending_Click(object sender, EventArgs e)
    {
        GridView GvStudent = new GridView();
        DataSet dsfee = objIDC.getTestPrepExamDataPendingExcel(Convert.ToInt32(ddlSession_1.SelectedValue), Convert.ToInt32(ddlSchool2.SelectedValue));
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=TestPrepExamDataPendingExcel.xlsx";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updStudent, "Data Not Found", this.Page);
        }
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region Api Code

    protected void ddlApiSubject_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string[] SubjectId = ddlApiSubject.SelectedValue.Split('-');
        if (SubjectId[0].Length > 0)
        {
            ExamNameApi(SubjectId[0]);
        }
    }

    protected void btnApiShow_Click(object sender, EventArgs e)
    {
        string[] SubjectId = ddlApiSubject.SelectedValue.Split('-');
        if (SubjectId[0].Length > 0)
        {
            ExamMarksApi(ddlApiExamName.SelectedValue, SubjectId[1]);
        }
    }

    protected void btnApiSubmit_Click(object sender, EventArgs e)
    {
        if (ddlApiSession.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("TotalMarks", typeof(decimal));
            dt.Columns.Add("MarksObtained", typeof(string));
            dt.Columns.Add("ExamSubmitDate", typeof(DateTime));
            dt.Columns.Add("AbsObtainMarks", typeof(string));
            dt.Columns.Add("IsExamChecked", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("RollNo", typeof(string));
            dt.Columns.Add("PRNNo", typeof(string));
            dt.Columns.Add("MobileNo", typeof(string));
            dt.Columns.Add("CandidateId", typeof(Int64));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("IntegratedSubjectId", typeof(int));
            DataRow dr = null;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var input = new
            {
                CollegeId = 1,
                IntegratedId = 2,
                ExamId = ddlApiExamName.SelectedValue
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString( Session["TestPrepExamAPI"]+"/OnlineExamandroid/api/StudentResult/GetResult", inputJson);

            var root = (JsonConvert.DeserializeObject<ExamMarksObject>(json));
            string[] SubjectId = ddlApiSubject.SelectedValue.Split('-');

            if (root.StudentResults != null)
            {
                foreach (var ExamDetail in root.StudentResults)
                {
                    dr = dt.NewRow();
                    dr["Name"] = ExamDetail.Name;
                    dr["TotalMarks"] = ExamDetail.TotalMarks;
                    dr["MarksObtained"] = ExamDetail.MarksObtained;
                    dr["ExamSubmitDate"] = ExamDetail.ExamSubmitDate;
                    dr["AbsObtainMarks"] = ExamDetail.AbsObtainMarks;
                    dr["IsExamChecked"] = ExamDetail.IsExamChecked;
                    dr["Remark"] = ExamDetail.Remark;
                    dr["RollNo"] = ExamDetail.RollNo;
                    dr["PRNNo"] = ExamDetail.PRNNo;
                    dr["MobileNo"] = ExamDetail.MobileNo;
                    dr["CandidateId"] = ExamDetail.CandidateId;
                    dr["CourseName"] = ddlApiSubject.SelectedItem;
                    dr["IntegratedSubjectId"] = SubjectId[1];
                    dt.Rows.Add(dr);
                }

                objTestPrepThroughtMISEntity.dt = dt;
                objTestPrepThroughtMISEntity.SESSIONNO = Convert.ToInt32(ddlApiSession.SelectedValue);
                objTestPrepThroughtMISEntity.CreatedBy = Convert.ToInt32(Session["userno"]);
                objTestPrepThroughtMISEntity.CreatedIP = Request.ServerVariables["REMOTE_ADDR"];

                //INSERT DATA THROUGHT API
                object Result=objIDC.InsertTestPrepExamDataThroughAPI(objTestPrepThroughtMISEntity);
                if (Convert.ToInt32(Result) > 0)
                {
                    objCommon.DisplayMessage(updStudent, "Data Save Successfully.", this.Page);
                    divExamMarksEntryDataThroughtApi.Visible = true;
                    ddlApiExamName.SelectedIndex = 0;
                }
                else
                {
                    objCommon.DisplayMessage(updStudent, "Please try again !!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updStudent, "Please Select Session.", this.Page);
            }
        }
    }


    /// <summary>
    /// Clear Page Controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApiCancel_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    #region Subject

    private void SubjectApi()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SubjectId",typeof(string));
        dt.Columns.Add("SubjectName", typeof(string));
        //dt.Columns.Add("IntegratedSubjectId", typeof(int));

        DataRow dr = null;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
       // string json = (new WebClient()).DownloadString("http://49.248.172.242/OnlineExamandroid/api/StudentResult/GetSubject?CollegeId=1&IntegratedId=2");

        string json = (new WebClient()).DownloadString(Session["TestPrepExamAPI"]+"/api/StudentResult/GetSubject?CollegeId=1&IntegratedId=2");

        //
        var root = (JsonConvert.DeserializeObject<SubjectObject>(json));
        if (root.Subjects.Count > 0)
        {
            foreach (var SubjectDetail in root.Subjects)
            {
                dr = dt.NewRow();
                dr["SubjectId"] = SubjectDetail.SubjectId + "-" + SubjectDetail.IntegratedSubjectId;
                dr["SubjectName"] = SubjectDetail.SubjectName;
                //dr["IntegratedSubjectId"] = SubjectDetail.IntegratedSubjectId;
                dt.Rows.Add(dr);
            }

            this.FillDropDown(ddlApiSubject, dt);
        }
    }

    public class SubjectObject
    {
        public List<Subject> Subjects { get; set; }
    }

    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int IntegratedSubjectId { get; set; }
    }

    #endregion Subject.

    #region Exam

    private void ExamNameApi(string SubjectId) //SubjectId-Test Prep Subject Id
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ExamId", typeof(int));
        dt.Columns.Add("ExamName", typeof(string));

        DataRow dr = null;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        string json = (new WebClient()).DownloadString(Session["TestPrepExamAPI"] + "/api/StudentResult/GetExam?CollegeId=1&IntegratedId=2&SujectId=" + SubjectId + "");

        var root = (JsonConvert.DeserializeObject<ExamObject>(json));
        if (root.Exams != null)
        {
            foreach (var ExamDetail in root.Exams)
            {
                dr = dt.NewRow();
                dr["ExamId"] = ExamDetail.ExamId;
                dr["ExamName"] = ExamDetail.ExamName;
                dt.Rows.Add(dr);
            }

            this.FillDropDown(ddlApiExamName, dt);
            btnApiShow.Visible = true;
        }
        else
        {
            ddlApiExamName.Items.Clear();
            ddlApiExamName.Items.Add("Loading.....");
            ddlApiExamName.SelectedItem.Value = "0";
            btnApiShow.Visible = false;
        }
    }

    public class ExamObject
    {
        public List<Exam> Exams { get; set; }
    }

    public class Exam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
    }

    #endregion Exam

    #region Exam Marks

    private void ExamMarksApi(string ExamId, string IntegratedSubjectId) //IntegratedSubjectId-MIS Subject Id
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("TotalMarks", typeof(decimal));
        dt.Columns.Add("MarksObtained", typeof(string));
        dt.Columns.Add("ExamSubmitDate", typeof(DateTime));
        dt.Columns.Add("AbsObtainMarks", typeof(string));
        dt.Columns.Add("IsExamChecked", typeof(string));
        dt.Columns.Add("Remark", typeof(string));
        dt.Columns.Add("RollNo", typeof(string));
        dt.Columns.Add("PRNNo", typeof(string));
        dt.Columns.Add("MobileNo", typeof(string));
        dt.Columns.Add("CandidateId", typeof(Int64));
        dt.Columns.Add("CourseName", typeof(string));
        dt.Columns.Add("IntegratedSubjectId", typeof(int));
        DataRow dr = null;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        var input = new
        {
            CollegeId = 1,
            IntegratedId=2,
            ExamId = ExamId
        };

        string inputJson = (new JavaScriptSerializer()).Serialize(input);

        WebClient client = new WebClient();
        client.Headers["Content-type"] = "application/json";
        client.Encoding = Encoding.UTF8;
        string json = client.UploadString(Session["TestPrepExamAPI"] + "/api/StudentResult/GetResult", inputJson);

        var root = (JsonConvert.DeserializeObject<ExamMarksObject>(json));
        if (root.StudentResults != null)
        {
            foreach (var ExamDetail in root.StudentResults)
            {
                dr = dt.NewRow();
                dr["Name"] = ExamDetail.Name;
                dr["TotalMarks"] = ExamDetail.TotalMarks;
                dr["MarksObtained"] = ExamDetail.MarksObtained;
                dr["ExamSubmitDate"] = ExamDetail.ExamSubmitDate;
                dr["AbsObtainMarks"] = ExamDetail.AbsObtainMarks;
                dr["IsExamChecked"] = ExamDetail.IsExamChecked;
                dr["Remark"] = ExamDetail.Remark;
                dr["RollNo"] = ExamDetail.RollNo;
                dr["PRNNo"] = ExamDetail.PRNNo;
                dr["MobileNo"] = ExamDetail.TotalMarks;
                dr["CandidateId"] = ExamDetail.TotalMarks;
                dr["CourseName"] = ddlApiSubject.SelectedItem;
                dr["IntegratedSubjectId"] = IntegratedSubjectId;
                dt.Rows.Add(dr);
            }
            divExamMarksEntryDataThroughtApi.Visible = true;
            btnApiSubmit.Visible = true;
            lstExamMarksEntryDataThroughtApi.DataSource = dt;
            lstExamMarksEntryDataThroughtApi.DataBind();
        }
        else
        {
            objCommon.DisplayMessage(updStudent, "Data Not Found", this.Page);
            divExamMarksEntryDataThroughtApi.Visible = false;
            btnApiSubmit.Visible = false;
        }
    }

    public class ExamMarksObject
    {
        public List<ExamMarks> StudentResults { get; set; }
    }

    public class ExamMarks
    {
        public string Name { get; set; }
        public decimal TotalMarks { get; set; }
        public string MarksObtained { get; set; }
        public string ExamSubmitDate { get; set; }
        public string AbsObtainMarks { get; set; }
        public string IsExamChecked { get; set; }
        public string Remark { get; set; }
        public string RollNo { get; set; }
        public string PRNNo { get; set; }
        public string MobileNo { get; set; }
        public Int64 CandidateId { get; set; }
    }

    #endregion Exam Marks

    private void FillDropDown(DropDownList ddlList,DataTable dt)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";
        if (dt.Rows.Count>0)
        {
            ddlList.DataSource = dt;
            ddlList.DataValueField = dt.Columns[0].ToString();
            ddlList.DataTextField = dt.Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
    }

    private void Clear()
    {
     

        ddlApiExamName.Items.Clear();
        ddlApiExamName.Items.Add("Loading.....");
        ddlApiExamName.SelectedItem.Value = "0";
        btnApiShow.Visible = false;
        divExamMarksEntryDataThroughtApi.Visible = false;
        btnApiSubmit.Visible = false;
        SubjectApi();
    }

    #endregion Api Code
}