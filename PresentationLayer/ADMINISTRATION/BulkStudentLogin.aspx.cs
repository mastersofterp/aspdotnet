//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// PAGE NAME     : BULK USER CREATION OF STUDENTS                                  
// CREATION DATE : 19-Aug-2009                                                     
// CREATED BY    : NIRAJ D. PHALKE    
// ADDED BY      : ASHISH DHAKATE
// ADDED DATE    : 30-DEC-2011                                             
// MODIFIED BY   : RUCHIKA DHAKATE
// MODIFIED DESC : 
//=================================================================================
//------------------------------------------------------------------------------------------------------------------------------------
//Version     Modified On          Modified By          Purpose
//------------------------------------------------------------------------------------------------------------------------------------
//1.0.1      19-02-2024            Rutuja Dawle        Use email template on the send email button.
//--------------------------------------------- -------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using mastersofterp_MAKAUAT;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using ClosedXML.Excel;
using System.Threading.Tasks;
using Mastersoft.Security.IITMS;
using BusinessLogicLayer.BusinessLogic;
using System.Net;


public partial class ADMINISTRATION_BulkStudentLogin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Student objSC = new Student();
    StudentController objStud = new StudentController();
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

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
         
            PopulateDropDown();
            PopulateDropDownList();
            PopulateParentDropDown();
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
        }
        divMsg.InnerHtml = string.Empty;
        txtEnterPassword.Visible = false;
        divPassWord.Visible = false;
        divParentPassword.Visible = false;
        if (rdoCustomPass.Checked == true)
        {
            txtEnterPassword.Visible = true;
            divPassWord.Visible = true;
        }
        btnExportUploadLog.Visible = true;
        btnExportUploadLog.Enabled = false;

        if (rdoParentCustompd.Checked == true)
        {
            txtPEnterPass.Visible = true;
            divParentPassword.Visible = true;
        }
    }

    #region UploadStudent

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
                }
            }
            else
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }
   
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "A.LONGNAME");

            }
            else
            {
                ddlBranch.Items.Clear();
             
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    private void ShowMessage(string message)
    {
        try
        {
            if (message != string.Empty)
            {
                divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            Uploaddata();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        btnExportUploadLog.Enabled = true;
    }

    protected void btnExportUploadLog_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gvStudData = new GridView();
            gvStudData.DataSource = ViewState["ExcelData"];
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=DATA_IMPORT_LOG.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);
            gvStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnCancelUpload_Click(object sender, EventArgs e)
    {
        try
        {
            ddlStudAdmBatch.SelectedIndex = 0;
            LvDescription.DataSource = null;
            LvDescription.DataBind();
            LvDescription.Visible = false;
            divCount.Visible = false;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
        }
        finally
        {
            GC.Collect();
        }
    }

    private void Uploaddata()
    {
        try
        {
            if (FileUpload2.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload2.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload2.SaveAs(FilePath);
                    ExcelToDatabase(FilePath, Extension, "yes");
                    LvDescription.Visible = true;
                    divCount.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Only .xls or .xlsx extention is allowed", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please select the Excel File to Upload", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updpnl, "Cannot access the file. Please try again.", this.Page);
            return;
        }
    }

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        CustomStatus cs = new CustomStatus();
        string conStr = "";
        string datacolumnerror = string.Empty;

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

        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            cmdExcel.Connection = connExcel;
            datacolumnerror = "\\nPlease contact system administrator.";

            connExcel.Open();
            System.Data.DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        

            string SheetName = "Student Admission Data Format$"; //Added  By Ruchika Dhakate on 30.09.2022


            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            DataSet ds = new DataSet();
            oda.Fill(ds);

            DataView dv1 = dt.DefaultView;


            System.Data.DataTable dtNew = ds.Tables[0];

            lvStudent.DataSource = dtNew; 
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            lvStudent.Visible = true;

            int i = 0;
            int count = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            {
                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Regno = row[0];
                datacolumnerror = string.Empty;

                if (Regno != null && !String.IsNullOrEmpty(Regno.ToString().Trim()))
                {
                    string exist = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "REGNO='" + dtNew.Rows[i]["REGISTRATIONNO"].ToString() + "'");
                    if (Convert.ToInt32(exist) > 0)
                    {
                        count++;
                    }

                }
            }


            System.Data.DataTable dt1 = new System.Data.DataTable();
            DataRow dr = null;
            dt1.Columns.Add(new DataColumn("RowId", typeof(string)));
            dt1.Columns.Add(new DataColumn("Description", typeof(string)));

            bool IsErrorInUpload = false;
            string ErrorString = string.Empty;
            string message = string.Empty;
            int RowNum = 0;
            int TotalRecordCount = 0;
            int TotalRecordUploadCount = 0;
            int TotalAlreadyExistsCount = 0;
            int TotalRecordErrorCount = 0;
            string RecordExist = string.Empty;
            divRecords.Visible = true;
            divtotcount.Visible = true;
            divrecupload.Visible = true;
            divrecexist.Visible = true;
            divRecwitherror.Visible = true;

            lblTotalRecordCount.Text = TotalRecordCount.ToString();
            lblTotalRecordUploadCount.Text = TotalRecordUploadCount.ToString();
            lblTotalAlreadyExistsCount.Text = TotalAlreadyExistsCount.ToString();
            lblTotalRecordErrorCount.Text = TotalRecordErrorCount.ToString();

            lblValue.Text = count.ToString();
            lblValue.Text = TotalRecordCount.ToString();

            TotalRecordCount = dtNew.Rows.Count;


            DataSet ds1 = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH cdb inner join ACD_COLLEGE_MASTER c on (cdb.COLLEGE_ID = c.COLLEGE_ID and c.ActiveStatus = 1)inner join ACD_DEGREE d on (cdb.DEGREENO = d.DEGREENO and d.ACTIVESTATUS = 1)inner join ACD_BRANCH b on (cdb.BRANCHNO = b.BRANCHNO and b.ACTIVESTATUS = 1)", "(ltrim(rtrim(c.COLLEGE_NAME)) + '-' + ltrim(rtrim(d.DEGREENAME))) College_Degree, (ltrim(rtrim(c.COLLEGE_NAME)) + '-' + ltrim(rtrim(d.DEGREENAME)) + '-' + ltrim(rtrim(b.LONGNAME))) College_Degree_Branch", "", "", "College_Degree");
            List<string> CollegeDegree = new List<string>();
            List<string> CollegeDegreeBranch = new List<string>();

            if (ds1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dataRow in ds1.Tables[0].Rows)
                {
                    CollegeDegree.Add(dataRow["College_Degree"].ToString().ToUpper());
                    CollegeDegreeBranch.Add(dataRow["College_Degree_Branch"].ToString().ToUpper());
                }

            }

            for (i = 0; i < dtNew.Rows.Count; i++)
            {
                ErrorString = string.Empty;
                datacolumnerror = string.Empty;
                string exist = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "REGNO='" + dtNew.Rows[i]["REGISTRATIONNO"].ToString() + "'");
                string mobileNoExist = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "STUDENTMOBILE='" + dtNew.Rows[i]["MOBILENO"].ToString() + "'");
                string studentEmailExist = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "EMAILID='" + dtNew.Rows[i]["EMAILID"].ToString() + "'");
                RowNum = RowNum + 1;
                ErrorString = ErrorString + Environment.NewLine + "Row : " + RowNum.ToString() + " - ";

                if (Convert.ToInt32(exist) > 0 && !string.IsNullOrEmpty(dtNew.Rows[i]["REGISTRATIONNO"].ToString()))
                {
                    message = "Record Already Exists for REGISTRATIONNO";
                    ErrorString = ErrorString + message;
                    IsErrorInUpload = true;
                    TotalAlreadyExistsCount++;
                }
                else if(Convert.ToInt32(mobileNoExist) > 0 && !string.IsNullOrEmpty(dtNew.Rows[i]["MOBILENO"].ToString()))
                {
                    message = "Record Already Exists for MOBILENO";
                    ErrorString = ErrorString + message;
                    IsErrorInUpload = true;
                    TotalAlreadyExistsCount++;
                }
                else if (Convert.ToInt32(studentEmailExist) > 0 && !string.IsNullOrEmpty(dtNew.Rows[i]["EMAILID"].ToString()))
                {
                    message = "Record Already Exists for EMAILID";
                    ErrorString = ErrorString + message;
                    IsErrorInUpload = true;
                    TotalAlreadyExistsCount++;
                }
                else
                {

                    IsErrorInUpload = false;
                    DataRow row = dtNew.Rows[i];
                    object Regno = row[0];
                    string admbatch = string.Empty;
                    string category = string.Empty;
                    string degree = string.Empty;
                    string branch = string.Empty;
                    string city = string.Empty;
                    string district = string.Empty;
                    string state = string.Empty;
                    string bloodgroup = string.Empty;
                    string college = string.Empty;
                    string paytype = string.Empty;
                    string section = string.Empty;
                    string countryname = string.Empty;


                    datacolumnerror = RowNum.ToString() + "-REGISTRATIONNO";
                    if (!(dtNew.Rows[i]["REGISTRATIONNO"]).ToString().Equals(string.Empty))
                    {
                        if (SecurityThreads.CheckSecurityInput(dtNew.Rows[i]["REGISTRATIONNO"].ToString()))
                        {
                            message = "Entered Registration Number is not valid for Check Security Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else if (SecurityThreads.ValidInput(dtNew.Rows[i]["REGISTRATIONNO"].ToString()))
                        {
                            message = "Entered Registration Number is not valid Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else
                        {
                            objSC.RegNo = dtNew.Rows[i]["REGISTRATIONNO"].ToString();
                        }
                    }


                  datacolumnerror = RowNum.ToString() + "-ROLLNO";
                    objSC.RollNo = dtNew.Rows[i]["ROLLNO"].ToString();
                    //}
                    datacolumnerror = RowNum.ToString() + "-GENDER";
                    if (!(dtNew.Rows[i]["GENDER"]).ToString().Equals(string.Empty))
                    {
                        if (dtNew.Rows[i]["GENDER"].ToString().Trim().ToUpper().Equals("FEMALE"))
                        {
                            objSC.Sex = Convert.ToChar("F");
                        }
                        else if (dtNew.Rows[i]["GENDER"].ToString().Trim().ToUpper().Equals("MALE"))
                        {
                            objSC.Sex = Convert.ToChar("M");
                        }
                        else
                        {
                            message = "Please enter Gender in given format (MALE/FEMALE)";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                    }
                    else
                    {
                        objSC.Sex = Convert.ToChar(' '); 
                    }

                    datacolumnerror = RowNum.ToString() + "-ACADEMIC_YEAR";
                    if (!(dtNew.Rows[i]["ACADEMIC_YEAR"]).ToString().Equals(string.Empty))
                    {
                        string AcademicYearID = objCommon.LookUp("ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME='" + dtNew.Rows[i]["ACADEMIC_YEAR"] + "'");

                        if (Convert.ToInt32(AcademicYearID) > 0)
                        {
                            objSC.AcademicYearNo = Convert.ToInt32(AcademicYearID);
                        }
                    }

                    datacolumnerror = RowNum.ToString() + "-ADMISSIONTYPE";
                    if (!(dtNew.Rows[i]["ADMISSIONTYPE"]).ToString().Equals(string.Empty))
                    {
                        string IdType = objCommon.LookUp("acd_idtype", "IDTYPENO", "IDTYPEDESCRIPTION='" + dtNew.Rows[i]["ADMISSIONTYPE"].ToString() + "'");
                        if (IdType != string.Empty)
                        {
                            objSC.IdType = Convert.ToInt32(IdType);
                        }

                        //if ((dtNew.Rows[i]["ADMISSIONTYPE"]).ToString().Equals("Regular"))
                        //    objSC.IdType = 1;
                        //else if ((dtNew.Rows[i]["ADMISSIONTYPE"]).ToString().Equals("Direct Second Year(Lateral)"))
                        //    objSC.IdType = 2;
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "Please enter Admission Type in given format at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "Admission Type not Found in ERP Master";
                            //        objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;

                        }
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnl, "Please enter Admission Type at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message =
                        message = "Please enter Admission Type Regular Or Direct Second Year(Lateral)";
                        //     objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;
                    }
                    datacolumnerror = RowNum.ToString() + "-SEMESTER";
                    if (!(dtNew.Rows[i]["SEMESTER"]).ToString().Equals(string.Empty))
                    {
                        objSC.SemesterNo = Convert.ToInt32(dtNew.Rows[i]["SEMESTER"]);
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnl, "Please enter Semester at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please enter Semester";
                        //      objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;
                    }
                    datacolumnerror = RowNum.ToString() + "-YEAR";
                    if (!(dtNew.Rows[i]["YEAR"]).ToString().Equals(string.Empty))
                    {
                        objSC.Year = Convert.ToInt32(dtNew.Rows[i]["YEAR"]);
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnl, "Please enter Year at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please enter Year  ";
                        //    objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;

                    }
                    datacolumnerror = RowNum.ToString() + "-STUDENTNAME";
                    if (!(dtNew.Rows[i]["STUDENTNAME"]).ToString().Equals(string.Empty))
                    {
                        objSC.StudName = (dtNew.Rows[i]["STUDENTNAME"]).ToString();
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnl, "Please enter Student Name at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please enter Student Name ";
                        //    objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;

                    }

                    objSC.AdmBatch = Convert.ToInt32(ddlStudAdmBatch.SelectedValue);
                    datacolumnerror = RowNum.ToString() + "-ADMISSION_DATE";
                    if (!(dtNew.Rows[i]["ADMISSION_DATE"]).ToString().Equals(string.Empty))
                    {
                        objSC.AdmDate = Convert.ToDateTime(dtNew.Rows[i]["ADMISSION_DATE"]);
                    }
                    else
                    {
                        //objSC.AdmDate = Convert.ToDateTime(null);
                        //objCommon.DisplayMessage(updpnl, "Please enter Student Admission Date at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please enter Student Admission Date ";
                        //     objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;

                    }
                    datacolumnerror = RowNum.ToString() + "-DOB";
                    if (!(dtNew.Rows[i]["DOB"]).ToString().Equals(string.Empty))
                    {
                        objSC.Dob = Convert.ToDateTime(dtNew.Rows[i]["DOB"]);
                    }
                    else
                    {
                        objSC.Dob = Convert.ToDateTime(null);
                    }
                    datacolumnerror = RowNum.ToString() + "-FATHERNAME";
                    objSC.FatherName = dtNew.Rows[i]["FATHERNAME"].ToString();


                    datacolumnerror = RowNum.ToString() + "-FATHERMOBILE";
                    if (!(dtNew.Rows[i]["FATHERMOBILE"]).ToString().Equals(string.Empty))
                    {
                        if (SecurityThreads.CheckSecurityInput(dtNew.Rows[i]["FATHERMOBILE"].ToString()))
                        {
                            message = "Entered Father Mobile No. is not valid for Check Security Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else if (SecurityThreads.ValidInput(dtNew.Rows[i]["FATHERMOBILE"].ToString()))
                        {
                            message = "Entered Father Mobile No. is not valid Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else
                        {
                            objSC.FatherMobile = dtNew.Rows[i]["FATHERMOBILE"].ToString();
                        }                        
                    }
                    else
                    {
                        message = "Please Enter Father Mobile No";
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                    }

                    datacolumnerror = RowNum.ToString() + "-FATHEREMAIL";
                    if (!(dtNew.Rows[i]["FATHEREMAIL"]).ToString().Equals(string.Empty))
                    {
                        if (SecurityThreads.CheckSecurityInput(dtNew.Rows[i]["FATHEREMAIL"].ToString()))
                        {
                            message = "Entered Father Email Id is not valid for Check Security Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else if (SecurityThreads.ValidInput(dtNew.Rows[i]["FATHEREMAIL"].ToString()))
                        {
                            message = "Entered Father Email Id is not valid Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else
                        {
                            objSC.Fatheremail = dtNew.Rows[i]["FATHEREMAIL"].ToString();
                        }                        
                    }
                    else
                    {
                        message = "Please Enter Father Email ID";
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                    }

                    datacolumnerror = RowNum.ToString() + "-MOTHERNAME";
                    objSC.MotherName = dtNew.Rows[i]["MOTHERNAME"].ToString();

                    category = (dtNew.Rows[i]["CATEGORY"]).ToString();

                    datacolumnerror = RowNum.ToString() + "-CATEGORY";
                    if (!(dtNew.Rows[i]["CATEGORY"]).ToString().Equals(string.Empty))
                    {
                        string categoryno = objCommon.LookUp("ACD_CATEGORY", "COUNT(1)", "CATEGORY='" + dtNew.Rows[i]["CATEGORY"].ToString() + "'");

                        if (Convert.ToInt32(categoryno) > 0)
                        {
                            objSC.CategoryNo = (objCommon.LookUp("ACD_CATEGORY", "CATEGORYNO", "CATEGORY ='" + category + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_CATEGORY", "CATEGORYNO", "CATEGORY ='" + category + "'"));

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "Category not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "Category not found in ERP Master ";
                            //     objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;
                        }

                    }
                    else
                    {
                        objSC.CategoryNo = Convert.ToInt32(null);
                    }
                    datacolumnerror = RowNum.ToString() + "-PHYSICALLY_HANDICAPPED";
                    if ((dtNew.Rows[i]["PHYSICALLY_HANDICAPPED"]).ToString().Equals(string.Empty))
                    {
                        objSC.PH = string.Empty;
                    }

                    if (dtNew.Rows[i]["PHYSICALLY_HANDICAPPED"].Equals("YES"))
                    {
                        objSC.PH = "1";
                    }
                    else if (dtNew.Rows[i]["PHYSICALLY_HANDICAPPED"].Equals("NO"))
                    {
                        objSC.PH = "2";
                    }

                    datacolumnerror = RowNum.ToString() + "-MOBILENO";
                    if (!(dtNew.Rows[i]["MOBILENO"]).ToString().Equals(string.Empty))
                    {
                        if (SecurityThreads.CheckSecurityInput(dtNew.Rows[i]["MOBILENO"].ToString()))
                        {
                            message = "Entered Mobile No. is not valid for Check Security Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else if (SecurityThreads.ValidInput(dtNew.Rows[i]["MOBILENO"].ToString()))
                        {
                            message = "Entered Mobile No. is not valid Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else
                        {
                            objSC.StudentMobile = dtNew.Rows[i]["MOBILENO"].ToString();
                        }
                        
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnl, "Please Enter Mobile No. at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please Enter Mobile No.";
                        //      objCommon.Displ

                        //message(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;
                    }
                    datacolumnerror = RowNum.ToString() + "-EMAILID";
                    if (!(dtNew.Rows[i]["EMAILID"]).ToString().Equals(string.Empty))
                    {
                        if (SecurityThreads.CheckSecurityInput(dtNew.Rows[i]["EMAILID"].ToString()))
                        {
                            message = "Entered Email Id is not valid for Check Security Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else if (SecurityThreads.ValidInput(dtNew.Rows[i]["EMAILID"].ToString()))
                        {
                            message = "Entered Email Id is not valid Input ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                        }
                        else
                        {
                            objSC.EmailID = dtNew.Rows[i]["EMAILID"].ToString();
                        }
                        
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnl, "Please Enter Email Id at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please Enter Email Id";
                        //objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);               
                        ErrorString = ErrorString + message + "|";
                        IsErrorInUpload = true;
                        //return;

                    }
                    datacolumnerror = RowNum.ToString() + "-DEGREE";
                    degree = (dtNew.Rows[i]["DEGREE"]).ToString();
                    if (dtNew.Rows[i]["DEGREE"].ToString() == string.Empty)
                    {
                        //objCommon.DisplayMessage(updpnl, "Please Enter Degree at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please Enter Degree ";
                        //   objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + "|";
                        IsErrorInUpload = true;
                        //return;

                    }
                    else
                    {
                        string degreeno_Count = objCommon.LookUp("ACD_DEGREE", "COUNT(1)", "DEGREENAME='" + dtNew.Rows[i]["DEGREE"].ToString() + "'");
                        //string _check = objCommon.LookUp("ACD_DEGREE D  inner join  ACD_COLLEGE_DEGREE C on D.DEGREENO=C.DEGREENO and  C.DEGREENO>0 ", "COUNT(1)", "D.DEGREENAME='" + dtNew.Rows[i]["DEGREE"].ToString() + "'" + "And C.COLLEGE_NAME = '" + dtNew.Rows[i]["COLLEGENAME"].ToString() + "'");
                        //if (Convert.ToInt32(degreeno) > 0)
                        if (Convert.ToInt32(degreeno_Count) > 0)
                        {
                            string degreeno = objCommon.LookUp("ACD_DEGREE", "DEGREENO", "DEGREENAME='" + dtNew.Rows[i]["DEGREE"].ToString() + "'");
                            string str_college_degree = dtNew.Rows[i]["COLLEGENAME"].ToString() + "-" + dtNew.Rows[i]["DEGREE"].ToString();

                            if (!CollegeDegree.Contains(str_college_degree.Trim().ToUpper()))
                            {
                                message = "Incorrect College-Degree Mapping ";
                                //objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                                ErrorString = ErrorString + message + "| ";
                                IsErrorInUpload = true;
                            }
                            else
                            {
                                objSC.DegreeNo = Convert.ToInt32(degreeno);
                            }
                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "Degree not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "Degree not found in ERP Master";
                            //      objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;
                        }

                    }
                    datacolumnerror = RowNum.ToString() + "-COLLEGENAME";
                    college = (dtNew.Rows[i]["COLLEGENAME"]).ToString();
                    if (dtNew.Rows[i]["COLLEGENAME"].ToString() == string.Empty)
                    {
                        //objCommon.DisplayMessage(updpnl, "Please Enter College Name at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please Enter College Name ";
                        //   objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;

                    }
                    else
                    {
                        string collegeno = objCommon.LookUp("ACD_COLLEGE_MASTER", "COUNT(1)", "COLLEGE_NAME='" + dtNew.Rows[i]["COLLEGENAME"].ToString() + "'");

                        if (Convert.ToInt32(collegeno) > 0)
                        {
                            objSC.Collegeid = (objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME ='" + college + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME ='" + college + "'"));

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "College not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return
                            message = "College not found in ERP Master ";
                            //       objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;

                        }

                    }
                    datacolumnerror = RowNum.ToString() + "-BRANCH";
                    branch = (dtNew.Rows[i]["BRANCH"]).ToString();
                    if (dtNew.Rows[i]["BRANCH"].ToString() == string.Empty)
                    {
                        //objCommon.DisplayMessage(updpnl, "Please Enter Branch at Row no. " + (i + 1), this.Page);
                        //IsErrorInUpload = true;
                        //return;

                        message = "Please Enter Branch ";
                        //       objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                        ErrorString = ErrorString + message + " | ";
                        IsErrorInUpload = true;
                        //return;

                    }
                    else
                    {
                        string branchno_count = objCommon.LookUp("ACD_BRANCH", "COUNT(1)", "LONGNAME='" + dtNew.Rows[i]["BRANCH"].ToString() + "'");
                        //   string _check = objCommon.LookUp("ACD_BRANCH B  inner join  ACD_COLLEGE_DEGREE C on B.BRANCHNO=C.BRANCHNO and  C.BRANCHNO>0 ", "COUNT(1)", "D.LONGNAME='" + dtNew.Rows[i]["BRANCH"].ToString() + "'" + "And C.DEGREENAME = '" + dtNew.Rows[i]["DEGREE"].ToString() + "'");
                        if (Convert.ToInt32(branchno_count) > 0)
                        {
                            string str_college_degree_branch = dtNew.Rows[i]["COLLEGENAME"].ToString() + "-" + dtNew.Rows[i]["DEGREE"].ToString() + "-" + dtNew.Rows[i]["BRANCH"].ToString();
                            string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + dtNew.Rows[i]["BRANCH"].ToString() + "'");

                            if (!CollegeDegreeBranch.Contains(str_college_degree_branch.Trim().ToUpper()))
                            {
                                message = "Incorrect College-Degree-Branch Mapping ";
                                //objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                                ErrorString = ErrorString + message + "| ";
                                IsErrorInUpload = true;
                            }
                            else
                            {
                                objSC.BranchNo = Convert.ToInt32(branchno);
                            }

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "Branch not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "Branch not found in ERP Master ";
                            // objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + "| ";
                            IsErrorInUpload = true;
                            //return;
                        }
                    }
                    objSC.Corres_address = dtNew.Rows[i]["ADDRESS1"].ToString() + dtNew.Rows[i]["ADDRESS2"].ToString() + dtNew.Rows[i]["ADDRESS3"].ToString();

                    datacolumnerror = RowNum.ToString() + "-STATE";
                    state = (dtNew.Rows[i]["STATE"]).ToString();
                    if (dtNew.Rows[i]["STATE"].ToString() == string.Empty)
                    {
                        objSC.StateNo = Convert.ToInt32(null);
                    }
                    else
                    {
                        string stateno = objCommon.LookUp("ACD_STATE", "COUNT(1)", "STATENAME='" + dtNew.Rows[i]["STATE"].ToString() + "'");

                        if (Convert.ToInt32(stateno) > 0)
                        {
                            objSC.StateNo = (objCommon.LookUp("ACD_STATE", "STATENO", "STATENAME ='" + state + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STATE", "STATENO", "STATENAME ='" + state + "'"));

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "State not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "State not found in ERP Master";
                            //  objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;

                        }

                    }

                    datacolumnerror = RowNum.ToString() + "-COUNTRYNAME";
                    countryname = (dtNew.Rows[i]["COUNTRYNAME"]).ToString();
                    if (dtNew.Rows[i]["COUNTRYNAME"].ToString() == string.Empty)
                    {
                        objSC.CountryNo = Convert.ToInt32(null);
                    }
                    else
                    {
                        string CountryNo = objCommon.LookUp("ACD_COUNTRY", "COUNT(1)", "COUNTRYNAME='" + dtNew.Rows[i]["COUNTRYNAME"].ToString() + "'");

                        if (Convert.ToInt32(CountryNo) > 0)
                        {
                            objSC.CountryNo = (objCommon.LookUp("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME ='" + countryname + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME ='" + countryname + "'"));

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "State not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "CountryName  not found in ERP Master";
                            //  objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;

                        }

                    }



                    datacolumnerror = RowNum.ToString() + "-DISTRICT";
                    district = (dtNew.Rows[i]["DISTRICT"]).ToString();
                    if (dtNew.Rows[i]["DISTRICT"].ToString() == string.Empty)
                    {
                        objSC.PDISTRICT = string.Empty;
                    }
                    else
                    {
                        string districtno = objCommon.LookUp("ACD_DISTRICT", "COUNT(1)", "DISTRICTNAME='" + dtNew.Rows[i]["DISTRICT"].ToString() + "'");

                        if (Convert.ToInt32(districtno) > 0)
                        {
                            objSC.PDISTRICT = (objCommon.LookUp("ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME ='" + district + "'")) == string.Empty ? "0" : (objCommon.LookUp("ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME ='" + district + "'").ToString());

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "District not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "District not found in ERP Master";
                            //        objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;
                        }
                    }

                    datacolumnerror = RowNum.ToString() + "-PINCODE";
                    objSC.PPinCode = dtNew.Rows[i]["PINCODE"].ToString();

                    string board = dtNew.Rows[i]["BOARD"].ToString();
                    string YR_12 = dtNew.Rows[i]["YR_12"].ToString();

                    if (!(dtNew.Rows[i]["PR_12"]).ToString().Equals(string.Empty))
                    {
                        objSC.Percentage = Convert.ToDecimal(dtNew.Rows[i]["PR_12"]);
                    }
                    else
                    {
                        objSC.Percentage = 0;
                    }
                    datacolumnerror = RowNum.ToString() + "-BLOODGROUP";
                    bloodgroup = (dtNew.Rows[i]["BLOODGROUP"]).ToString();
                    if (dtNew.Rows[i]["BLOODGROUP"].ToString() == string.Empty)
                    {
                        objSC.BloodGroupNo = Convert.ToInt32(null);
                    }
                    else
                    {

                        string bloodgroupno = objCommon.LookUp("ACD_BLOODGRP", "COUNT(1)", "BLOODGRPNAME='" + dtNew.Rows[i]["BLOODGROUP"].ToString() + "'");

                        if (Convert.ToInt32(bloodgroupno) > 0)
                        {
                            objSC.BloodGroupNo = (objCommon.LookUp("ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME ='" + bloodgroup + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME ='" + bloodgroup + "'"));

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "Blood Group not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "Blood Group not found in ERP Master ";
                            //            objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;

                        }

                    }
                    datacolumnerror = RowNum.ToString() + "-PAYMENT_TYPE";
                    paytype = (dtNew.Rows[i]["PAYMENT_TYPE"]).ToString();
                    if (dtNew.Rows[i]["PAYMENT_TYPE"].ToString() == string.Empty)
                    {
                        objSC.PayTypeNO = Convert.ToInt32(null);
                    }
                    else
                    {

                        string paymenttype = objCommon.LookUp("ACD_PAYMENTTYPE", "COUNT(1)", "PAYTYPENAME='" + dtNew.Rows[i]["PAYMENT_TYPE"].ToString() + "'");

                        if (Convert.ToInt32(paymenttype) > 0)
                        {
                            objSC.PayTypeNO = (objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME ='" + paytype + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME ='" + paytype + "'"));

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updpnl, "Payment type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            //IsErrorInUpload = true;
                            //return;

                            message = "Payment type not found in ERP Master  ";
                            //  objCommon.DisplayMessage(updpnl, message + (i + 1), this.Page);
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;
                        }
                    }

                    //Added By Ruchika Dhakate on 06.09.2022
                    datacolumnerror = RowNum.ToString() + "-SECTIONNAME";
                    section = (dtNew.Rows[i]["SECTIONNAME"]).ToString();
                    if (dtNew.Rows[i]["SECTIONNAME"].ToString() == string.Empty)
                    {
                        objSC.SectionNo = Convert.ToInt32(null);

                    }
                    else
                    {
                        string sectionno = objCommon.LookUp("ACD_SECTION", "COUNT(1)", "SECTIONNAME='" + dtNew.Rows[i]["SECTIONNAME"].ToString() + "'");

                        if (Convert.ToInt32(sectionno) > 0)
                        {
                            objSC.SectionNo = (objCommon.LookUp("ACD_SECTION", "SECTIONNO", "SECTIONNAME ='" + section + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SECTION", "SECTIONNO", "SECTIONNAME ='" + section + "'"));


                        }
                        else
                        {

                            message = "Section not Found in ERP Master ";
                            ErrorString = ErrorString + message + " | ";
                            IsErrorInUpload = true;
                            //return;

                        }

                    }

                    if (IsErrorInUpload == false)
                    {
                        TotalRecordUploadCount++;

                        message = " Record Sucessfully Uploaded ";
                        int OrgId = Convert.ToInt32(Session["OrgId"]);
                        // int Uano = Convert.ToInt32(Session["userNo"]);
                        objSC.Uano = Convert.ToInt32(Session["userNo"]);                     //Added By Ruchika Dhakate on 07.09.2022
                        cs = (CustomStatus)objStud.SaveExcelSheetDataInDataBase(objSC, YR_12, board, OrgId);
                        string[] result = { "RecordSaved", "RecordUpdated" };
                        if (result.Any(s => cs.ToString().Contains(s)))
                        {
                            ErrorString = ErrorString + message + " | ";
                        }
                        else
                        {
                            message = "Issue while saving records";
                            ErrorString = ErrorString + message + " | ";
                        }
                        //connExcel.Close();
                    }

                    else
                    {

                        TotalRecordErrorCount++;
                        IsErrorInUpload = true;
                    }

                    if (ErrorString.Trim().EndsWith("|"))
                    {
                        ErrorString = ErrorString.Remove(ErrorString.Length - 2, 1);
                    }

                }

                dr = dt1.NewRow();
                dr["RowId"] = (i + 1).ToString();
                dr["Description"] = ErrorString;
                dt1.Rows.Add(dr);
                ViewState["CurrentTable"] = dt1;

            }

            // Display Total count here
            lblTotalRecordCount.Text = TotalRecordCount.ToString();
            lblTotalRecordUploadCount.Text = TotalRecordUploadCount.ToString();
            lblTotalAlreadyExistsCount.Text = TotalAlreadyExistsCount.ToString();
            lblTotalRecordErrorCount.Text = TotalRecordErrorCount.ToString();

            LvDescription.DataSource = dt1;
            LvDescription.DataBind();
            ViewState["ExcelData"] = dt1;

            if (TotalRecordErrorCount > 0)
            {
                objCommon.DisplayMessage(updpnl, "Excel Sheet Imported Successfully with Errors, Please check error log!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Excel Sheet Imported Successfully!!", this.Page);
            }
            datacolumnerror = string.Empty;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnl, "Data not available in ERP Master" + datacolumnerror, this.Page);
                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            connExcel.Close();
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = objStud.RetrieveMasterData();

            ds.Tables[0].TableName = "Student Admission Data Format";
            ds.Tables[1].TableName = "School College Data";
            ds.Tables[2].TableName = "Degree Data";
            ds.Tables[3].TableName = "Branch Data";
            ds.Tables[4].TableName = "Payment Type Data";
            ds.Tables[5].TableName = "Section";
            ds.Tables[6].TableName = "Admission Type";
            ds.Tables[7].TableName = "Academic Year";
            string status = string.Empty;
            // added by kajal jaiswal on 16-02-2023 for checking table is blank 
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count == 0)
                {
                    status += dt.TableName + ",";

                }
            }

            if (status != string.Empty)
            {
                status = status.Trim(',');
                objCommon.DisplayMessage(Page, "Data not available in ERP Master Table " + status, this.Page);
                return;
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadStudentData.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
             
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion

    #region CreateLogin

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlBranch.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddSemester, "ACD_STUDENT A INNER JOIN ACD_SEMESTER B ON(A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT B.SEMESTERNO", "B.SEMESTERNAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND A.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "AND A.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND A.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SEMESTERNO");
            }
            else
            {
                ddSemester.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlColg.SelectedIndex = 0;
            ddSemester.SelectedIndex = 0;
            ddlAdmBatch.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }

    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlStudAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int _ADMBATCH = 0;
            int _COLLEGEID = 0;
            int _DEGREENO = 0;
            int _BRANCHNO = 0;
            int _SEMESTERNO = 0;

            _ADMBATCH = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            _COLLEGEID = Convert.ToInt32(ddlColg.SelectedValue);
            _DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            _BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
            _SEMESTERNO = Convert.ToInt32(ddSemester.SelectedValue);

            DataSet ds = objStud.GetUserCreationList(_ADMBATCH, _COLLEGEID, _DEGREENO, _BRANCHNO, _SEMESTERNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds.Tables[0];
                lvStudents.DataBind();
                //pnllistview.Visible = true;
                lvStudents.Visible = true;
                //btnUpdate.Enabled = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                //pnllistview.Visible = false;
                lvStudents.Visible = false;
                // btnUpdate.Enabled = false;
                //objCommon.DisplayUserMessage(upduser, "No Record Found!", Page);
                objCommon.DisplayMessage(this.updpnl, "No Record Found!", this.Page);

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudent.Visible = false;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void lvStudent_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                if (dr["REGISTRATIONNO"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblRegNo")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblRegNo")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblRegNo")).Font.Bold = true;
                }

                if (!(dr["GENDER"]).Equals(string.Empty) && (!(dr["GENDER"].Equals("MALE"))))
                {
                    if (!(dr["GENDER"].ToString().Equals("FEMALE")))
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblGender")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblGender")).Font.Bold = true;
                    }

                }
                if (!(dr["CATEGORY"]).ToString().Equals(string.Empty))
                {
                    string categoryno = objCommon.LookUp("ACD_CATEGORY", "COUNT(1)", "CATEGORY='" + dr["CATEGORY"].ToString() + "'");
                    if (Convert.ToInt32(categoryno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCategory")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCategory")).Font.Bold = true;

                    }
                }

                if (!(dr["PHYSICALLY_HANDICAPPED"]).ToString().Equals(string.Empty))
                {
                    if (!(dr["PHYSICALLY_HANDICAPPED"].ToString().Equals("YES")) && (!(dr["PHYSICALLY_HANDICAPPED"].ToString().Equals("NO"))))
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPH")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPH")).Font.Bold = true;
                    }

                }


                if (dr["STUDENTNAME"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblStudName")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblStudName")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblStudName")).Font.Bold = true;
                }

                if (!(dr["ADMISSIONTYPE"]).ToString().Equals(string.Empty))
                {
                    if (!(dr["ADMISSIONTYPE"]).ToString().Equals("Regular") && !(dr["ADMISSIONTYPE"]).ToString().Equals("Direct Second Year(Lateral)"))
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).Font.Bold = true;
                    }
                }

                else if ((dr["ADMISSIONTYPE"].ToString() == string.Empty))
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblIdType")).Font.Bold = true;
                }

                if (dr["SEMESTER"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSemester")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSemester")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSemester")).Font.Bold = true;
                }
                if (dr["YEAR"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblYear")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblYear")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblYear")).Font.Bold = true;
                }

                if (!(dr["DEGREE"]).ToString().Equals(string.Empty))
                {
                    string degreeno = objCommon.LookUp("ACD_DEGREE", "COUNT(1)", "DEGREENAME='" + dr["DEGREE"].ToString() + "'");
                    if (Convert.ToInt32(degreeno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).Font.Bold = true;

                    }
                }

                if (dr["DEGREE"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDegree")).Font.Bold = true;
                }

                if (!(dr["COLLEGENAME"]).ToString().Equals(string.Empty))
                {
                    string collegeno = objCommon.LookUp("ACD_COLLEGE_MASTER", "COUNT(1)", "COLLEGE_NAME='" + dr["COLLEGENAME"].ToString() + "'");
                    if (Convert.ToInt32(collegeno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).Font.Bold = true;

                    }
                }

                if (dr["COLLEGENAME"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCollege")).Font.Bold = true;
                }

                if (!(dr["BRANCH"]).ToString().Equals(string.Empty))
                {
                    string branchno = objCommon.LookUp("ACD_BRANCH", "COUNT(1)", "LONGNAME='" + dr["BRANCH"].ToString() + "'");
                    if (Convert.ToInt32(branchno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).Font.Bold = true;

                    }
                }
                if (dr["BRANCH"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBranch")).Font.Bold = true;
                }


                if (!(dr["COUNTRYNAME"]).ToString().Equals(string.Empty))
                {
                    string stateno = objCommon.LookUp("ACD_COUNTRY", "COUNT(1)", "COUNTRYNAME='" + dr["COUNTRYNAME"].ToString() + "'");
                    if (Convert.ToInt32(stateno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCountry")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCountry")).Font.Bold = true;

                    }
                }


                if (!(dr["STATE"]).ToString().Equals(string.Empty))
                {
                    string stateno = objCommon.LookUp("ACD_STATE", "COUNT(1)", "STATENAME='" + dr["STATE"].ToString() + "'");
                    if (Convert.ToInt32(stateno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblState")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblState")).Font.Bold = true;

                    }
                }

                if (!(dr["DISTRICT"]).ToString().Equals(string.Empty))
                {
                    string districtno = objCommon.LookUp("ACD_DISTRICT", "COUNT(1)", "DISTRICTNAME='" + dr["DISTRICT"].ToString() + "'");
                    if (Convert.ToInt32(districtno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDistrict")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblDistrict")).Font.Bold = true;

                    }
                }

                if (!(dr["BLOODGROUP"]).ToString().Equals(string.Empty))
                {
                    string bloodgroupno = objCommon.LookUp("ACD_BLOODGRP", "COUNT(1)", "BLOODGRPNAME='" + dr["BLOODGROUP"].ToString() + "'");
                    if (Convert.ToInt32(bloodgroupno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBloogGrp")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblBloogGrp")).Font.Bold = true;

                    }
                }

                if (!(dr["PAYMENT_TYPE"]).ToString().Equals(string.Empty))
                {
                    string bloodgroupno = objCommon.LookUp("ACD_PAYMENTTYPE", "COUNT(1)", "PAYTYPENAME='" + dr["PAYMENT_TYPE"].ToString() + "'");
                    if (Convert.ToInt32(bloodgroupno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPayType")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblPayType")).Font.Bold = true;

                    }
                }

                if (dr["MOBILENO"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblMobile")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblMobile")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblMobile")).Font.Bold = true;
                }

                if (dr["EMAILID"].ToString() == string.Empty)
                {
                    divNote.Visible = true;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblEmail")).Text = "--";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblEmail")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblEmail")).Font.Bold = true;
                }

                //Added By Ruchika Dhakate  on 07.09.2022
                if (!(dr["SECTION"]).ToString().Equals(string.Empty))
                {
                    string sectionno = objCommon.LookUp("ACD_SECTION", "COUNT(1)", "SECTIONNAME='" + dr["SECTION"].ToString() + "'");
                    if (Convert.ToInt32(sectionno) == 0)
                    {
                        divNote.Visible = true;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSection")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblSection")).Font.Bold = true;

                    }
                }

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnCreateLogin_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        int ret = 0;
        try
        {
            
            User_AccController objACC = new User_AccController();
            UserAcc objUA = new UserAcc();
            if (rdoCustomPass.Checked == true)
            {
                if (SecurityThreads.ValidInput(txtEnterPassword.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Custom Password", Page);
                    return;
                }
                if (SecurityThreads.CheckSecurityInput(txtEnterPassword.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Custom Password", Page);
                    return;
                }
                if ( txtEnterPassword.Text == "")
                {
                    objCommon.DisplayMessage(this.updpnl, "Please Enter Custom Password", this.Page);
                    return;
                }
            }
            foreach (ListViewDataItem itm in lvStudents.Items)
            {

                System.Web.UI.WebControls.CheckBox chk = itm.FindControl("chkRow") as System.Web.UI.WebControls.CheckBox;
                System.Web.UI.WebControls.Label lblreg = itm.FindControl("lblreg") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.HiddenField hdnf = itm.FindControl("hidStudentId") as System.Web.UI.WebControls.HiddenField;
                System.Web.UI.WebControls.Label lblstud = itm.FindControl("lblstud") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblEmailId = itm.FindControl("lblEmailId") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblMobileNo = itm.FindControl("lblMobileNo") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblBranch = itm.FindControl("lblBranch") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblsemester = itm.FindControl("lblsemester") as System.Web.UI.WebControls.Label;
             
                if (chk.Checked == true && (chk.Enabled == true && chk.Text == ""))
                {

                    objUA.UA_IDNo = Convert.ToInt32(hdnf.Value);
                    id = objUA.UA_IDNo;
                    objUA.UA_Name = lblreg.Text;

                    string pwd = string.Empty;
                 
                    if (rdRegPass.Checked == true)
                    {
                        pwd = lblreg.Text;
                    }
                    else if (rdGeneratepass.Checked == true)
                    {
                        string PasswordName = CommonComponent.GenerateRandomPassword.GenearteFourLengthPassword();
                        pwd = PasswordName;
                    }
                    else if (rdoCustomPass.Checked == true)
                    {
                        pwd = txtEnterPassword.Text;
                        
                    }
                    else
                    {
                        pwd = lblreg.Text;
                    }

                    //  objUA.UA_Pwd = Common.EncryptPassword(pwd);


                    objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                    objUA.UA_FullName = lblstud.Text;
                    objUA.EMAIL = lblEmailId.Text.Trim();
                    objUA.MOBILE = lblMobileNo.Text.Trim();


                    // objUA._status = 0;
                    ret = objACC.AddStudentUser(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), objUA);

                }

                if (chk.Checked && chk.Text == "CREATED")
                {
                    id++;


                }
                if (chk.Checked && chk.Text == "")
                {
                    count++;
                }
            }

            if (count == 0 && id == 0)
            {
                objCommon.DisplayMessage(this.updpnl, "Please select at least one student", this.Page);
            }
            else if (count == 0 && id > 0)
            {
                objCommon.DisplayMessage(this.updpnl, "Login has already been created for selected students", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Students Login Created Successfully", this.Page);
                txtEnterPassword.Text = string.Empty;
                btnShow_Click(sender, e);
               
            }
           

        }



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.btnModify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            User_AccController objUC = new User_AccController();
            UserAcc objUA = new UserAcc();

            string CodeStandard = objCommon.LookUp("Reff", "CODE_STANDARD", "");
            string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
            string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();

            int countstud = 0;
            int countstudcheck = 0;
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = item.FindControl("chkRow") as System.Web.UI.WebControls.CheckBox;
                System.Web.UI.WebControls.Label lblLogin = item.FindControl("lblLogin") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblEmailId = item.FindControl("lblEmailId") as System.Web.UI.WebControls.Label;

                if (chk.Checked == true)
                {
                    countstudcheck++;
                }
                // <1.0.1>  
                //added by rutuja 12/02/24
                if (chk.Checked == true && (chk.Text == "CREATED") && lblLogin.Text == "" && lblEmailId.Text != "")
                {
                    countstud++;
                    System.Web.UI.WebControls.Label lblreg = item.FindControl("lblreg") as System.Web.UI.WebControls.Label;
                    System.Web.UI.WebControls.Label lblStudName = item.FindControl("lblstud") as System.Web.UI.WebControls.Label;
                    System.Web.UI.WebControls.Label lblPwd = item.FindControl("lblreg") as System.Web.UI.WebControls.Label;
                    string email = lblEmailId.Text;
                    string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + lblreg.Text + "'");
                    string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);
                    int TemplateTypeId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE='User Login'"));
                    int TemplateId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_EMAILTEMPLATE", "TOP 1 TEMPLATEID", "TEMPTYPEID=" + TemplateTypeId + " AND TEMPLATENAME = 'Login Credentials'"));

                    string message = "";
                    DataSet ds_mstQry1 = objCommon.FillDropDown("ACD_ADMP_EMAILTEMPLATE AEM", "TOP 1 TEMPLATETEXT", "", "TEMPLATEID=" + TemplateId + "", "AEM.TEMPTYPEID ");


                    if (ds_mstQry1 != null)
                    {
                        message = ds_mstQry1.Tables[0].Rows[0]["TEMPLATETEXT"].ToString();
                        message = message.Replace("[FIRST NAME]", lblStudName.Text);
                        message = message.Replace("[LOGIN NAME]", lblreg.Text);
                        message = message.Replace("[USERPASSWORD]", strPwd);
                        message = message.Replace("[CLICKHERELOGIN]", loginurl);
                    }
                    string Subject = CodeStandard + " ERP || Login Credentials";  // "MIS Login Credentials";
                    int status = 0;
                    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
                    status = objSendEmail.SendEmail(email, message, Subject); //Calling Method
                    // </1.0.1>  
                }
            }

            // added by kajal jaiswal on 16-02-2023 for validating send email button 
            if (countstud != 0)
            {
                objCommon.DisplayMessage(updpnl, "Email send successfully to the Student having Proper EmailId !", this.Page);
            }
            else if (lvStudents.Items.Count == 0)
            {
                objCommon.DisplayMessage(updpnl, "No Record Found!", this.Page);
            }
            else if (lvStudents.Items.Count != 0 && countstudcheck == 0)
            {
                objCommon.DisplayMessage(updpnl, "Please select at least one Student !", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Email send successfully to the Student having Proper EmailId !", this.Page);
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void rdRegPass_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            txtEnterPassword.Text = string.Empty;
            txtEnterPassword.Visible = false;
            divPassWord.Visible = false;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void rdGeneratepass_CheckedChanged(object sender, EventArgs e)
    {
        try{
        txtEnterPassword.Text = string.Empty;
        txtEnterPassword.Visible = false;
        divPassWord.Visible = false;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void rdoCustomPass_CheckedChanged(object sender, EventArgs e)
    {
        try{
        txtEnterPassword.Text = string.Empty;
        txtEnterPassword.Visible = true;
        divPassWord.Visible = true;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    #endregion

    # region parent login

    private void PopulateParentDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlParentAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlParentDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlParentSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlParentColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateParentDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlParentDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlParentDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlParentBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlParentColg.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlParentDegree.SelectedValue), "A.LONGNAME");
            }
            else
            {
                ddlParentBranch.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlParentColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try{
        objCommon.FillDropDownList(ddlParentDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlParentColg.SelectedValue + "", "DEGREENO");
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
     }

    protected void ddlParentBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        if (Convert.ToInt32(ddlParentBranch.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlParentSemester, "ACD_STUDENT A INNER JOIN ACD_SEMESTER B ON(A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT B.SEMESTERNO", "B.SEMESTERNAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlParentColg.SelectedValue) + "AND A.ADMBATCH=" + Convert.ToInt32(ddlParentAdmBatch.SelectedValue) + "AND A.DEGREENO=" + Convert.ToInt32(ddlParentDegree.SelectedValue) + "AND A.BRANCHNO=" + Convert.ToInt32(ddlParentBranch.SelectedValue), "SEMESTERNO");
        }
        else
        {
            ddlParentSemester.SelectedIndex = 0;
        }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    //Added by Ruchika Dhakate on  19.09.2022
    //For Parent  Show Login
    protected void BtnParentShow_Click(object sender, EventArgs e)
    {
        try
        {
        int _ADMBATCHP = 0;
        int _COLLEGEIDP = 0;
        int _DEGREENOP = 0;
        int _BRANCHNOP = 0;
        int _SEMESTERNOP = 0;

        _ADMBATCHP = Convert.ToInt32(ddlParentAdmBatch.SelectedValue);
        _COLLEGEIDP = Convert.ToInt32(ddlParentColg.SelectedValue);
        _DEGREENOP = Convert.ToInt32(ddlParentDegree.SelectedValue);
        _BRANCHNOP = Convert.ToInt32(ddlParentBranch.SelectedValue);
        _SEMESTERNOP = Convert.ToInt32(ddlParentSemester.SelectedValue);

        DataSet ds = objStud.GetUserParentCreationList(_ADMBATCHP, _COLLEGEIDP, _DEGREENOP, _BRANCHNOP, _SEMESTERNOP);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvParent.DataSource = ds.Tables[0];
            lvParent.DataBind();
            lvParent.Visible = true;
          
        }
        else
        {
           
            lvParent.DataSource = null;
            lvParent.DataBind();
            lvParent.Visible = false;
            objCommon.DisplayUserMessage(this.updpnl, "No Record Found!", Page);
        }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    //Added By Ruchika Dhakate on 20.09.2022
    protected void BtnCreateParentLogin_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0; int ret = 0;
        try
        {
            User_AccController objACC = new User_AccController();
            UserAcc objUA = new UserAcc();
            if (rdoParentCustompd.Checked == true)
            {
                if (SecurityThreads.ValidInput(txtPEnterPass.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Custom Password", Page);
                    return;
                }
                if (SecurityThreads.CheckSecurityInput(txtPEnterPass.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Valid Custom Password", Page);
                    return;
                }
                if (txtPEnterPass.Text == "")
                {
                    objCommon.DisplayMessage(this.updpnl, "Please Enter Custom Password", this.Page);
                    return;
                }
            }

            foreach (ListViewDataItem itm in lvParent.Items)
            {

                System.Web.UI.WebControls.CheckBox chkID = itm.FindControl("chkRowParent") as System.Web.UI.WebControls.CheckBox;
                System.Web.UI.WebControls.Label lblPreg = itm.FindControl("lblPreg") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.HiddenField hdnf = itm.FindControl("hidStudentId") as System.Web.UI.WebControls.HiddenField;
                System.Web.UI.WebControls.Label lblPstud = itm.FindControl("lblPstudent") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblFathername = itm.FindControl("lblFathername") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblFatherEmailID = itm.FindControl("lblFatherEmailID") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblFatherMobile = itm.FindControl("lblFatherMobile") as System.Web.UI.WebControls.Label;
                //System.Web.UI.WebControls.Label lblBranch = itm.FindControl("lblBranch") as System.Web.UI.WebControls.Label;
                int org = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                // changed by rutuja 06-03-2024
                if (chkID.Checked == true && (chkID.Enabled == true && chkID.Text == ""))
                {

                    objUA.UA_IDNo = Convert.ToInt32(hdnf.Value);
                    id = objUA.UA_IDNo;
                    objUA.UA_Name = lblFatherMobile.Text;

                    //      objUA.UA_FullName = lblPstud.Text;

                    string pwd = string.Empty;
                    //string PasswordName = GeneartePassword();

                    //string pwd1 = clsTripleLvlEncyrpt.ThreeLevelEncrypt(PasswordName);
                    if (rdoParentRegpd.Checked == true)
                    {
                        pwd = lblPreg.Text;
                    }
                    else if (rdoParentGeneratepd.Checked == true)
                    {
                        string PasswordName = CommonComponent.GenerateRandomPassword.GenearteFourLengthPassword();
                        pwd = PasswordName;
                    }
                    else if (rdoParentCustompd.Checked == true)
                    {

                        pwd = txtPEnterPass.Text;
                    }
                    else
                    {
                        pwd = lblPreg.Text;
                    }

                    //  objUA.UA_Pwd = Common.EncryptPassword(pwd);


                    objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                    objUA.UA_FullName = lblPstud.Text;
                    //  objUA.UA_FatherName = lblFathername.Text.Trim();
                    //  objUA.UA_FatherMobile = lblFatherMobile.Text.Trim();
                    objUA.MOBILE = lblFatherMobile.Text.Trim();
                    objUA.EMAIL = lblFatherEmailID.Text.Trim();


                    objUA.UA_Status = 0;
                    //  objUA.EMAIL = string.Empty;
                    ret = objACC.AddParentUser(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), objUA);

                }

                if (chkID.Checked && chkID.Text == "CREATED")
                {
                    id++;


                }
                if (chkID.Checked && chkID.Text == "")
                {
                    count++;
                }
            }

            if (count == 0 && id == 0)
            {
                objCommon.DisplayMessage(this.updpnl, "Please select at least one Parent", this.Page);
            }
            else if (count == 0 && id > 0)
            {
                objCommon.DisplayMessage(this.updpnl, "Login has already been created for selected Parents", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Parents Login Created Successfully", this.Page);
                txtPEnterPass.Text = string.Empty;
                BtnParentShow_Click(sender, e);
                // btnSendSMS.Enabled = true;
            }
          

        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.btnModify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //Added By Ruchika Dhakate on 21.09.2022
    protected void btnPCancel_Click(object sender, EventArgs e)
    {
        try{
        ddlParentAdmBatch.SelectedIndex = 0;
        ddlParentColg.SelectedIndex = 0;
        ddlParentDegree.SelectedIndex = 0;
        ddlParentSemester.SelectedIndex = 0;
        ddlParentBranch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    //Added By Ruchika Dhakate on 22.09.2022
    protected void rdoParentRegpd_CheckedChanged(object sender, EventArgs e)
    {
        try{
        txtPEnterPass.Text = string.Empty;
        txtPEnterPass.Visible = false;
        divParentPassword.Visible = false;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void rdoParentGeneratepd_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
        txtPEnterPass.Text = string.Empty;
        txtPEnterPass.Visible = false;
        divParentPassword.Visible = false;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    protected void rdoParentCustompd_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
        txtPEnterPass.Text = string.Empty;
        txtPEnterPass.Visible = true;
        divParentPassword.Visible = true;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    //Added By Ruchika Dhakate on  23.09.2022
    protected void btnSendParentEmail_Click(object sender, EventArgs e)
    {
        try{
        User_AccController objUC = new User_AccController();
        UserAcc objUA = new UserAcc();
        string CodeStandard = objCommon.LookUp("Reff", "CODE_STANDARD", "");
        string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
        string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
        int countparent = 0;
        int countpartcheck = 0;
        foreach (ListViewDataItem item in lvParent.Items)
        {
            System.Web.UI.WebControls.CheckBox chkID = item.FindControl("chkRowParent") as System.Web.UI.WebControls.CheckBox;
            System.Web.UI.WebControls.Label lblPLogin = item.FindControl("lblPLogin") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label lblFatherEmailID = item.FindControl("lblFatherEmailID") as System.Web.UI.WebControls.Label;

            if (chkID.Checked == true)
            {
                countpartcheck++;
            }
           // <1.0.1>  
            if (chkID.Checked == true && (chkID.Text == "CREATED") && lblFatherEmailID.Text != "")
            {
                countparent++;
                //  System.Web.UI.WebControls.Label lblPreg = item.FindControl("lblPreg") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblFatherMobile = item.FindControl("lblFatherMobile") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblPstudent = item.FindControl("lblPstudent") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblPwd = item.FindControl("lblPreg") as System.Web.UI.WebControls.Label;

                //string useremail = objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "a.EMAILID", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL");
                string email = lblFatherEmailID.Text;
                string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + lblFatherMobile.Text + "'");
                string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);
                int TemplateTypeId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE='User Login'"));
                int TemplateId = Convert.ToInt32(objCommon.LookUp("ACD_ADMP_EMAILTEMPLATE", "TOP 1 TEMPLATEID", "TEMPTYPEID=" + TemplateTypeId + " AND TEMPLATENAME = 'Login Credentials'"));

                string message = "";
                DataSet ds_mstQry1 = objCommon.FillDropDown("ACD_ADMP_EMAILTEMPLATE AEM", "TOP 1 TEMPLATETEXT", "", "TEMPLATEID=" + TemplateId + "", "AEM.TEMPTYPEID ");


                if (ds_mstQry1 != null)
                {
                    message = ds_mstQry1.Tables[0].Rows[0]["TEMPLATETEXT"].ToString();
                    message = message.Replace("[FIRST NAME]", lblPstudent.Text);
                    message = message.Replace("[LOGIN NAME]", lblFatherMobile.Text);
                    message = message.Replace("[USERPASSWORD]", strPwd);
                    message = message.Replace("[CLICKHERELOGIN]", loginurl);
                }

                string Subject = CodeStandard + " ERP || Login Credentials";// "MIS Login Credentials";

                //------------Code for sending email,It is optional---------------
                //added by kajal jaiswal  on 03-06-2023
                int status = 0;
                SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
                status = objSendEmail.SendEmail(email, message, Subject); //Calling Method
                // </1.0.1>  
            }
        }
        // added by kajal jaiswal on 16-02-2023 for validating send email button 
        if (countparent != 0)
        {
            objCommon.DisplayMessage(updpnl, "Email send successfully to the Parent having Proper EmailId !", this.Page);
        }
        else if (lvParent.Items.Count == 0)
        {
            objCommon.DisplayMessage(updpnl, "No Record Found!", this.Page);
        }
        else if (lvParent.Items.Count != 0 && countpartcheck == 0)
        {
            objCommon.DisplayMessage(updpnl, "Please select at least one Parent!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(updpnl, "Email send successfully to the Parent having Proper EmailId !", this.Page);
        }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops!Something went wrong.", this.Page);
            return;
        }
    }

    #endregion

}


