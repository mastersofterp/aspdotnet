using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using ClosedXML.Excel;

public partial class ADMINISTRATION_UploadAdmissionData : System.Web.UI.Page
{

    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Student objSC = new Student();
    StudentController objStud = new StudentController();

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
            }
            PopulateDropDownList();
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UploadAdmissionData.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UploadAdmissionData.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnExport_Click1(object sender, EventArgs e)
    {
        //Response.ClearContent();
        //Response.Clear();
        //Response.ContentType = "application/vnd.ms-excel";
        //string path = Server.MapPath("~/ExcelFormat/PreFormat_For_UploadStudentData.xls");
        //Response.AddHeader("Content-Disposition", "attachment;filename=\"PreFormat_For_UploadStudentData.xls\"");
        //Response.TransmitFile(path);
        //Response.Flush();
        //Response.End();

        DataSet ds = objStud.RetrieveMasterData();

        ds.Tables[0].TableName = "Student Admission Data Format";
        ds.Tables[1].TableName = "School College Data";
        ds.Tables[2].TableName = "Degree Data";
        ds.Tables[3].TableName = "Branch Data";
        ds.Tables[4].TableName = "Payment Type Data";

        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                //Add System.Data.DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadStudentData_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }


        //Application xlApp = new Microsoft.Office.Interop.Excel.Application();

        //if (xlApp == null)
        //{
        //   // MessageBox.Show("Excel is not properly installed!!");
        //    return;
        //}

        //xlApp.DisplayAlerts = false;
        ////string filePath = @"d:\test.xlsx";
        //string filePath = Server.MapPath("~/ExcelFormat/PreFormat_For_UploadStudentData.xls");
        //Workbook xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
        //Sheets worksheets = xlWorkBook.Worksheets;

        //var xlNewSheet = (Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
        //xlNewSheet.Name = "newsheet";
        //xlNewSheet.Cells[1, 1] = "New sheet content";

        //xlNewSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //xlNewSheet.Select();

        //xlWorkBook.Save();
        //xlWorkBook.Close();

        //releaseObject(xlNewSheet);
        //releaseObject(worksheets);
        //releaseObject(xlWorkBook);
        //releaseObject(xlApp);

        //MessageBox.Show("New Worksheet Created!");

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
            //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
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
    }

    private void Uploaddata()
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    ExcelToDatabase(FilePath, Extension, "yes");
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
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryGeneration.Uploaddata()-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
            objCommon.DisplayMessage(updpnl, "Cannot access the file. Please try again.", this.Page);
            return;
        }
    }

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        try
        {
            CustomStatus cs = new CustomStatus();
            string conStr = "";

            switch (Extension)
            {
                //case ".xls": //Excel 97-03
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
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
            System.Data.DataTable dt = new System.Data.DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();
            System.Data.DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[4]["TABLE_NAME"].ToString();
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
            //dv1.RowFilter = "isnull(REGISTRATIONNO,0)<>0";
            dv1.RowFilter = "isnull(STUDENTNAME,'')<>''";
            System.Data.DataTable dtNew = dv1.ToTable();

            lvStudent.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            lvStudent.Visible = true;

            int i = 0;
            int count = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            {
                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Regno = row[0];


                if (Regno != null && !String.IsNullOrEmpty(Regno.ToString().Trim()))
                {
                    string exist = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "REGNO='" + dtNew.Rows[i]["REGISTRATIONNO"].ToString() + "'");
                    if (Convert.ToInt32(exist) > 0)
                    {
                        //objCommon.DisplayMessage(this.updpnl, "Student record already saved with entered Reg. No. at Row No. " + (i + 2), this.Page);
                        //return;
                        count++;
                    }

                }
            }
            //objCommon.DisplayMessage(this.updpnl, count + " Student records already saved", this.Page);
            divRecords.Visible = true;
            lblValue.Text = count.ToString();

            for (i = 0; i < dtNew.Rows.Count; i++)
            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //foreach (DataRow dr in dt.Rows)
            {

                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Regno = row[0];
                if (Regno != null && !String.IsNullOrEmpty(Regno.ToString().Trim()))
                {

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

                    if (!(dtNew.Rows[i]["REGISTRATIONNO"]).ToString().Equals(string.Empty)) objSC.RegNo = dtNew.Rows[i]["REGISTRATIONNO"].ToString();
                    //if (!(dtNew.Rows[i]["ROLLNO"]).ToString().Equals(string.Empty))
                    //{
                    objSC.RollNo = dtNew.Rows[i]["ROLLNO"].ToString();
                    //}

                    if (!(dtNew.Rows[i]["GENDER"]).ToString().Equals(string.Empty))
                    {
                        if (dtNew.Rows[i]["GENDER"].Equals("FEMALE"))
                        {
                            objSC.Sex = Convert.ToChar("F");
                        }
                        else if (dtNew.Rows[i]["GENDER"].Equals("MALE"))
                        {
                            objSC.Sex = Convert.ToChar("M");
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Please enter Gender in given format at Row no. " + (i + 1), this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objSC.Sex = Convert.ToChar(' '); //= Convert.ToChar("");
                    }
                    if (!(dtNew.Rows[i]["ADMISSIONTYPE"]).ToString().Equals(string.Empty))
                    {
                        if ((dtNew.Rows[i]["ADMISSIONTYPE"]).ToString().Equals("Regular"))
                            objSC.IdType = 1;
                        else if ((dtNew.Rows[i]["ADMISSIONTYPE"]).ToString().Equals("Direct Second Year(Lateral)"))
                            objSC.IdType = 2;
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Please enter Admission Type in given format at Row no. " + (i + 1), this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please enter Admission Type at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    if (!(dtNew.Rows[i]["SEMESTER"]).ToString().Equals(string.Empty))
                    {
                        objSC.SemesterNo = Convert.ToInt32(dtNew.Rows[i]["SEMESTER"]);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please enter Semester at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    if (!(dtNew.Rows[i]["YEAR"]).ToString().Equals(string.Empty))
                    {
                        objSC.Year = Convert.ToInt32(dtNew.Rows[i]["YEAR"]);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please enter Year at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    if (!(dtNew.Rows[i]["STUDENTNAME"]).ToString().Equals(string.Empty))
                    {
                        objSC.StudName = (dtNew.Rows[i]["STUDENTNAME"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please enter Student Name at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    objSC.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);


                    if (!(dtNew.Rows[i]["ADMISSION_DATE"]).ToString().Equals(string.Empty))
                    {
                        objSC.AdmDate = Convert.ToDateTime(dtNew.Rows[i]["ADMISSION_DATE"]);
                    }
                    else
                    {
                        objSC.AdmDate = Convert.ToDateTime(null);
                    }

                    if (!(dtNew.Rows[i]["DOB"]).ToString().Equals(string.Empty))
                    {
                        objSC.Dob = Convert.ToDateTime(dtNew.Rows[i]["DOB"]);
                    }
                    else
                    {
                        objSC.Dob = Convert.ToDateTime(null);
                    }

                    objSC.FatherName = dtNew.Rows[i]["FATHERNAME"].ToString();

                    category = (dtNew.Rows[i]["CATEGORY"]).ToString();


                    if (!(dtNew.Rows[i]["CATEGORY"]).ToString().Equals(string.Empty))
                    {
                        string categoryno = objCommon.LookUp("ACD_CATEGORY", "COUNT(1)", "CATEGORY='" + dtNew.Rows[i]["CATEGORY"].ToString() + "'");

                        if (Convert.ToInt32(categoryno) > 0)
                        {
                            objSC.CategoryNo = (objCommon.LookUp("ACD_CATEGORY", "CATEGORYNO", "CATEGORY ='" + category + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_CATEGORY", "CATEGORYNO", "CATEGORY ='" + category + "'"));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Category not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }
                    else
                    {
                        objSC.CategoryNo = Convert.ToInt32(null);
                    }

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


                    if (!(dtNew.Rows[i]["MOBILENO"]).ToString().Equals(string.Empty))
                    {
                        objSC.StudentMobile = dtNew.Rows[i]["MOBILENO"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Mobile No. at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (!(dtNew.Rows[i]["EMAILID"]).ToString().Equals(string.Empty))
                    {
                        objSC.EmailID = dtNew.Rows[i]["EMAILID"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Email Id at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    degree = (dtNew.Rows[i]["DEGREE"]).ToString();
                    if (dtNew.Rows[i]["DEGREE"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Degree at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string degreeno = objCommon.LookUp("ACD_DEGREE", "COUNT(1)", "DEGREENAME='" + dtNew.Rows[i]["DEGREE"].ToString() + "'");

                        if (Convert.ToInt32(degreeno) > 0)
                        {
                            objSC.DegreeNo = (objCommon.LookUp("ACD_DEGREE", "DEGREENO", "DEGREENAME ='" + degree + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREENO", "DEGREENAME ='" + degree + "'"));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Degree not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }

                    college = (dtNew.Rows[i]["COLLEGENAME"]).ToString();
                    if (dtNew.Rows[i]["COLLEGENAME"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter College Name at Row no. " + (i + 1), this.Page);
                        return;
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
                            objCommon.DisplayMessage(updpnl, "College not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }

                    branch = (dtNew.Rows[i]["BRANCH"]).ToString();
                    if (dtNew.Rows[i]["BRANCH"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Branch at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string branchno = objCommon.LookUp("ACD_BRANCH", "COUNT(1)", "LONGNAME='" + dtNew.Rows[i]["BRANCH"].ToString() + "'");

                        if (Convert.ToInt32(branchno) > 0)
                        {
                            objSC.BranchNo = (objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME ='" + branch + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME ='" + branch + "'"));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Branch not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }


                    objSC.Corres_address = dtNew.Rows[i]["ADDRESS1"].ToString() + dtNew.Rows[i]["ADDRESS2"].ToString() + dtNew.Rows[i]["ADDRESS3"].ToString();


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
                            objCommon.DisplayMessage(updpnl, "State not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }

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
                            objCommon.DisplayMessage(updpnl, "District not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }


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
                            objCommon.DisplayMessage(updpnl, "Blood Group not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }
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
                            objCommon.DisplayMessage(updpnl, "Payment type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;
                        }

                    }
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

                            objCommon.DisplayMessage(updpnl, "Section not Found in ERP Master at Row no. " + (i + 1), this.Page);
                            return;

                        }

                    }
                    int OrgId = Convert.ToInt32(Session["OrgId"]);

                    cs = (CustomStatus)objStud.SaveExcelSheetDataInDataBase(objSC, YR_12, board, OrgId);
                    connExcel.Close();
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Please Enter Reg. No." + i, this.Page);
                    return;
                }

            }
            if (cs.Equals(CustomStatus.RecordSaved) || (cs.Equals(CustomStatus.DuplicateRecord)))
            {
                // BindListView();
                objCommon.DisplayMessage(updpnl, "Excel Sheet Imported Successfully!!", this.Page);
                lvStudent.Visible = false;
                divNote.Visible = false;
                divRecords.Visible = false;
                ddlAdmBatch.SelectedIndex = 0;
            }
            else
            {
                //BindListView();
                objCommon.DisplayMessage(updpnl, "Error in Importing Excel Sheet. Please Check Excel data is in proper format or not !!", this.Page);
                lvStudent.Visible = false;
                divNote.Visible = false;
                divRecords.Visible = false;
                ddlAdmBatch.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnl, "Data not available in ERP Master", this.Page);
                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
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
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudent.Visible = false;
    }
   
}