using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using mastersofterp_MAKAUAT;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_UploadNewAdmData : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Student objSC = new Student();
    StudentController objStud = new StudentController();
    User_AccController objACC = new User_AccController();
    UserAcc objUA = new UserAcc();

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
            throw;
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

        DataSet ds = objStud.RetrieveMasterDataForExcel();

        ds.Tables[0].TableName = "Student Admission Data Format";
        ds.Tables[1].TableName = "School College Master";
        ds.Tables[2].TableName = "Degree Master";
        ds.Tables[3].TableName = "Branch Master";
        ds.Tables[4].TableName = "Payment Type Master";
        ds.Tables[5].TableName = "Category Master";
        // ds.Tables[6].TableName = "Board Master";

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
            Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadNewStudentData_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
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
            throw;
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
            throw;
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
                    //string FilePath =(FolderPath + FileName);
                    string FilePath = Server.MapPath("~/ExcelData/" + FileName);
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
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[5]["TABLE_NAME"].ToString();
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
            DataTable dtNew = dv1.ToTable();

            lvStudent.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            lvStudent.DataBind();
            lvStudent.Visible = true;
            lvStudData.Visible = false;

            int i = 0;
            int count = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            {
                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Mobno = row[10];


                if (Mobno != null && !String.IsNullOrEmpty(Mobno.ToString().Trim()))
                {
                    string exist = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "STUDENTMOBILE='" + dtNew.Rows[i]["MOBILENO"].ToString() + "'");
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
                object Mobno = row[10];
                if (Mobno != null && !String.IsNullOrEmpty(Mobno.ToString().Trim()))
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
                    //string board = string.Empty;
                    string college_rank = string.Empty;


                    objSC.RegNo = null;

                    objSC.RollNo = null;

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

                    if ((dtNew.Rows[i]["PHYSICALLY_DISABLED"]).ToString().Equals(string.Empty))
                    {
                        objSC.PH = string.Empty;
                    }
                    if (dtNew.Rows[i]["PHYSICALLY_DISABLED"].Equals("YES"))
                    {
                        objSC.PH = "1";
                    }
                    else if (dtNew.Rows[i]["PHYSICALLY_DISABLED"].Equals("NO"))
                    {
                        objSC.PH = "2";
                    }


                    if (!(dtNew.Rows[i]["MOBILENO"]).ToString().Equals(string.Empty))
                    {
                        objSC.StudentMobile = dtNew.Rows[i]["MOBILENO"].ToString().Trim();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Mobile No. at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (!(dtNew.Rows[i]["EMAILID"]).ToString().Equals(string.Empty))
                    {
                        objSC.EmailID = dtNew.Rows[i]["EMAILID"].ToString().Trim();
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

                    //board = (dtNew.Rows[i]["BOARD"]).ToString();


                    //if (!(dtNew.Rows[i]["BOARD"]).ToString().Equals(string.Empty))
                    //{
                    //    string boardnono = objCommon.LookUp("ACD_BOARD", "COUNT(1)", "BOARD='" + dtNew.Rows[i]["BOARD"].ToString() + "'");

                    //    if (Convert.ToInt32(boardnono) > 0)
                    //    {
                    //        //objSC.CategoryNo = (objCommon.LookUp("ACD_BOARD", "BOARDNO", "BOARD ='" + board + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_BOARD", "BOARDNO", "BOARD ='" + board + "'"));
                    //        board = (objCommon.LookUp("ACD_BOARD", "BOARD", "BOARD ='" + board + "'")) == string.Empty ? "0" : (objCommon.LookUp("ACD_BOARD", "BOARD", "BOARD ='" + board + "'"));
                    //    }
                    //    else
                    //    {
                    //        objCommon.DisplayMessage(updpnl, "Board not found in ERP Master at Row no. " + (i + 1), this.Page);
                    //        return;
                    //    }

                    //}
                    //else
                    //{
                    //    board = "";
                    //}
                    string board = dtNew.Rows[i]["BOARD"].ToString();
                    string YR_12 = dtNew.Rows[i]["YR_12"].ToString();

                    //string college_rank = dtNew.Rows[i]["RANK"].ToString();
                    if (!(dtNew.Rows[i]["RANK"]).ToString().Equals(string.Empty))
                    {
                        college_rank = (dtNew.Rows[i]["RANK"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please insert College Rank at Row no. " + (i + 1), this.Page);
                        return;
                    }

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


                    cs = (CustomStatus)objStud.SaveNewAdmExcelDataInDataBase(objSC, YR_12, board, college_rank);

                    //Creating New User for excel sheet uploaded students
                    if (cs.Equals(CustomStatus.RecordSaved) || (cs.Equals(CustomStatus.DuplicateRecord)))
                    {
                        string studid = objCommon.LookUp("acd_student", "IDNO", "STUDENTMOBILE='" + objSC.StudentMobile + "' and STUDENTMOBILE IS NOT NULL");
                        objUA.UA_IDNo = Convert.ToInt32(studid);
                        //id = objUA.UA_IDNo;
                        objUA.UA_Name = objSC.StudentMobile.Trim();
                        string pwd = string.Empty;
                        pwd = objSC.StudentMobile.Trim();
                        objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                        objUA.UA_FullName = objSC.StudName;
                        objUA.EMAIL = objSC.EmailID.Trim();
                        objUA.MOBILE = objSC.StudentMobile.Trim();

                        CustomStatus cs1 = (CustomStatus)objACC.AddStudentUserForExcelUpload(objUA);
                        if (cs1.Equals(CustomStatus.RecordUpdated))
                        {

                            //objACC.AddStudentUserForExcelUpload(objUA);

                            //send SMS and Email to student for login credentials
                            string subject = "MIS Login Credentials";
                            string message = "<b>Dear " + objUA.UA_FullName + "," + "</b><br />";
                            message += "Your MIS Account has been created successfully! Please visit https://makaut.mastersofterp.in for Login with Username : " + objSC.StudentMobile.Trim() + " Password : " + "" + objSC.StudentMobile.Trim() + "" + "</b><br />";
                            message += "For Admission Process demo video please visit https://drive.google.com/file/d/1cIUKyi9b75ziQtxrIgBPBAGyE5Mmnjro/view?usp=sharing";
                            message += "<br /><br /><br />Thank You<br />";
                            message += "<br />Team MAKAUT, WB<br />";
                            message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";
                            Task<int> task = Execute(message, objUA.EMAIL, subject);


                            if ((objUA.MOBILE != "" || objUA.MOBILE != null))
                            {
                                string msg = "Dear " + objUA.UA_FullName + ",\r\n\r\nYour ERP Account has been created successfully!\r\n\r\nPlease visit https://makaut.mastersofterp.in \r\n\r\nFor login with \r\n\r\nUsername:" + objSC.StudentMobile.Trim() + "\r\nPassword: " + objSC.StudentMobile.Trim() + "\r\n\r\nMAKAUT, WB";
                                SendSMS(objUA.MOBILE, msg, "1007297590980849636");
                            }
                        }

                    }
                    connExcel.Close();
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Please Enter Mobile No." + i, this.Page);
                    return;
                }

            }
            if (cs.Equals(CustomStatus.RecordSaved) || (cs.Equals(CustomStatus.DuplicateRecord)))
            {
                // BindListView();
                objCommon.DisplayMessage(updpnl, "Excel Sheet Imported Successfully!!", this.Page);
                lvStudent.Visible = false;
                lvStudData.Visible = false;
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
            throw;
        }

    }


    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;

            if (!(dr["GENDER"]).Equals(string.Empty) && (!(dr["GENDER"].Equals("MALE"))))
            {
                if (!(dr["GENDER"].ToString().Equals("FEMALE")))
                {
                    ((Label)e.Item.FindControl("lblGender")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblGender")).Font.Bold = true;
                }

            }
            if (!(dr["CATEGORY"]).ToString().Equals(string.Empty))
            {
                string categoryno = objCommon.LookUp("ACD_CATEGORY", "COUNT(1)", "CATEGORY='" + dr["CATEGORY"].ToString() + "'");
                if (Convert.ToInt32(categoryno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblCategory")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblCategory")).Font.Bold = true;

                }
            }

            if (!(dr["PHYSICALLY_DISABLED"]).ToString().Equals(string.Empty))
            {
                if (!(dr["PHYSICALLY_DISABLED"].ToString().Equals("YES")) && (!(dr["PHYSICALLY_DISABLED"].ToString().Equals("NO"))))
                {
                    ((Label)e.Item.FindControl("lblPH")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblPH")).Font.Bold = true;
                }

            }


            if (dr["STUDENTNAME"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblStudName")).Text = "--";
                ((Label)e.Item.FindControl("lblStudName")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblStudName")).Font.Bold = true;
            }

            if (!(dr["ADMISSIONTYPE"]).ToString().Equals(string.Empty))
            {
                if (!(dr["ADMISSIONTYPE"]).ToString().Equals("Regular") && !(dr["ADMISSIONTYPE"]).ToString().Equals("Direct Second Year(Lateral)"))
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblIdType")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblIdType")).Font.Bold = true;
                }
            }

            else if ((dr["ADMISSIONTYPE"].ToString() == string.Empty))
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblIdType")).Text = "--";
                ((Label)e.Item.FindControl("lblIdType")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblIdType")).Font.Bold = true;
            }

            if (dr["SEMESTER"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblSemester")).Text = "--";
                ((Label)e.Item.FindControl("lblSemester")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblSemester")).Font.Bold = true;
            }
            if (dr["YEAR"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblYear")).Text = "--";
                ((Label)e.Item.FindControl("lblYear")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblYear")).Font.Bold = true;
            }

            if (!(dr["DEGREE"]).ToString().Equals(string.Empty))
            {
                string degreeno = objCommon.LookUp("ACD_DEGREE", "COUNT(1)", "DEGREENAME='" + dr["DEGREE"].ToString() + "'");
                if (Convert.ToInt32(degreeno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblDegree")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblDegree")).Font.Bold = true;

                }
            }

            if (dr["DEGREE"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblDegree")).Text = "--";
                ((Label)e.Item.FindControl("lblDegree")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblDegree")).Font.Bold = true;
            }

            if (!(dr["COLLEGENAME"]).ToString().Equals(string.Empty))
            {
                string collegeno = objCommon.LookUp("ACD_COLLEGE_MASTER", "COUNT(1)", "COLLEGE_NAME='" + dr["COLLEGENAME"].ToString() + "'");
                if (Convert.ToInt32(collegeno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblCollege")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblCollege")).Font.Bold = true;

                }
            }

            if (dr["COLLEGENAME"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblCollege")).Text = "--";
                ((Label)e.Item.FindControl("lblCollege")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblCollege")).Font.Bold = true;
            }

            if (!(dr["BRANCH"]).ToString().Equals(string.Empty))
            {
                string branchno = objCommon.LookUp("ACD_BRANCH", "COUNT(1)", "LONGNAME='" + dr["BRANCH"].ToString() + "'");
                if (Convert.ToInt32(branchno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblBranch")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblBranch")).Font.Bold = true;

                }
            }
            if (dr["BRANCH"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblBranch")).Text = "--";
                ((Label)e.Item.FindControl("lblBranch")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblBranch")).Font.Bold = true;
            }

            if (!(dr["STATE"]).ToString().Equals(string.Empty))
            {
                string stateno = objCommon.LookUp("ACD_STATE", "COUNT(1)", "STATENAME='" + dr["STATE"].ToString() + "'");
                if (Convert.ToInt32(stateno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblState")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblState")).Font.Bold = true;

                }
            }

            if (!(dr["DISTRICT"]).ToString().Equals(string.Empty))
            {
                string districtno = objCommon.LookUp("ACD_DISTRICT", "COUNT(1)", "DISTRICTNAME='" + dr["DISTRICT"].ToString() + "'");
                if (Convert.ToInt32(districtno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblDistrict")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblDistrict")).Font.Bold = true;

                }
            }

            if (!(dr["BLOODGROUP"]).ToString().Equals(string.Empty))
            {
                string bloodgroupno = objCommon.LookUp("ACD_BLOODGRP", "COUNT(1)", "BLOODGRPNAME='" + dr["BLOODGROUP"].ToString() + "'");
                if (Convert.ToInt32(bloodgroupno) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblBloogGrp")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblBloogGrp")).Font.Bold = true;

                }
            }

            if (!(dr["PAYMENT_TYPE"]).ToString().Equals(string.Empty))
            {
                string paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "COUNT(1)", "PAYTYPENAME='" + dr["PAYMENT_TYPE"].ToString() + "'");
                if (Convert.ToInt32(paytype) == 0)
                {
                    divNote.Visible = true;
                    ((Label)e.Item.FindControl("lblPayType")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblPayType")).Font.Bold = true;

                }
            }

            if (dr["MOBILENO"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblMobile")).Text = "--";
                ((Label)e.Item.FindControl("lblMobile")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblMobile")).Font.Bold = true;
            }

            if (dr["EMAILID"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblEmail")).Text = "--";
                ((Label)e.Item.FindControl("lblEmail")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblEmail")).Font.Bold = true;
            }

            //if (!(dr["BOARD"]).ToString().Equals(string.Empty))
            //{
            //    string board = objCommon.LookUp("ACD_BOARD", "COUNT(1)", "BOARD='" + dr["BOARD"].ToString() + "'");
            //    if (Convert.ToInt32(board) == 0)
            //    {
            //        divNote.Visible = true;
            //        ((Label)e.Item.FindControl("lblBoard")).ForeColor = System.Drawing.Color.Red;
            //        ((Label)e.Item.FindControl("lblBoard")).Font.Bold = true;

            //    }
            //}

            if (dr["RANK"].ToString() == string.Empty)
            {
                divNote.Visible = true;
                ((Label)e.Item.FindControl("lblYear")).Text = "--";
                ((Label)e.Item.FindControl("lblYear")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblYear")).Font.Bold = true;
            }
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudent.Visible = false;

        BindStudentList();
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }

    public void SendSMS(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    //public void SendSMS(string url, string uid, string pass, string mobno, string message,  string TemplateID="")   
    //{        try     
    //  {        
    //   WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID+"");     
    //      WebResponse response = request.GetResponse();         
    //  System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());    
    //       string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
    //     //return urlText;  
    //     }   
    //    catch (Exception)     
    //  {    
    //   }   
    //}

    private void BindStudentList()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetStudentListWithStatus(Convert.ToInt32(ddlAdmBatch.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvStudData.DataSource = ds;
                lvStudData.DataBind();
                lvStudData.Visible = true;
                divRecords.Visible = false;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudData);//Set label - 
            }
            else
            {
                lvStudData.DataSource = null;
                lvStudData.DataBind();
                lvStudData.Visible = false;
                divRecords.Visible = false;
                objCommon.DisplayMessage(updpnl, "No Record Found.", this);

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

}