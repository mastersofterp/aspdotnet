using ClosedXML.Excel;
using IITMS;

using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Configuration;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data.OleDb;

public partial class ACADEMIC_NPF_Data_Transfer : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objC = new CourseController();
    StudentController objStud = new StudentController();

    #region PageLoad
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method


                }

            }
            ViewState["ipaddress"] = Request.ServerVariables["REMOTE_ADDR"];
            //divMsg.InnerHtml = string.Empty;
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
                Response.Redirect("~/notauthorized.aspx?page=NPF_Data_Transfer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NPF_Data_Transfer.aspx");
        }
    }
    #endregion

    #region Show Data 
    protected void btnShow_Click(object sender, EventArgs e)
    {
        binlistview();
    }

    private void binlistview()
    {
        //DataSet ds = objCommon.FillDropDown("NPF_DATA_MAPPING", "NPF_Discipline,NPF_Programme,NPF_Specialization,[School Name],Degree,[Programme/Branch Name]", "", "", "");
        SqlDataReader dr = objStud.DisplayNpfMapping();
        //if (dr.Tables[0] != null && dr.Tables[0].Rows.Count > 0)
        if (dr.HasRows != null)
        {
            lvBinddata.DataSource = dr;
            lvBinddata.DataBind();
            lvDatatableDisplay.DataSource = null;
            lvDatatableDisplay.DataBind();
            Panel2.Visible = false;
            lvBinddata.Visible = true;
        }
        else
        {
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();
            lvDatatableDisplay.DataSource = null;
            lvDatatableDisplay.DataBind();
            Panel2.Visible = false;
            lvBinddata.Visible = false;
        }
    }
    #endregion 

    #region Download & Upload
    protected void btnSampleDownload_Click(object sender, EventArgs e)
    {

        DataSet ds = objC.RetrieveNpfMappingDataForExcel();

        ds.Tables[0].TableName = "NPF Data Mapping";
        ds.Tables[1].TableName = "School Name";
        ds.Tables[2].TableName = "Programme_Branch Name";
        ds.Tables[3].TableName = "Degree Name";
        ds.Tables[4].TableName = "ID TYPE";
      

        //if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
        {
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
                Response.AddHeader("content-disposition", "attachment;filename=NPF_Mapping_Table_Format.xls");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            //objCommon.DisplayMessage(this.UPDCOURSE, "Please Define All Masters!!", this.Page);
            Response.Write("<script>alert('Please Define All Masters')</script>");
        }
    }

    protected void btnUpload_Click1(object sender, EventArgs e)
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
            if (fuexelUpload.HasFile)
            {
                string FileName = Path.GetFileName(fuexelUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(fuexelUpload.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fuexelUpload.SaveAs(FilePath);
                    ExcelToDatatable(FilePath, Extension, "yes");
                }
                else
                {
                    objCommon.DisplayMessage(updpnUploadExcel, "Only .xls or .xlsx extention is allowed", this.Page);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnUploadExcel, "Please select the excel file to upload", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryGeneration.Uploaddata()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Submit 

    private void ExcelToDatatable(string FilePath, string Extension, string isHDR)
    {
        int drawing = 0;
        CourseController objCC = new CourseController();
        Course objC = new Course();
        try
        {
            CustomStatus cs = new CustomStatus();
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
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            Session["Datatable"] = dt;
            //Bind Excel to GridView
            DataSet ds = new DataSet();
            oda.Fill(ds);

            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "isnull(NPF_Discipline,'')<>''";
            DataTable dtNew = dv1.ToTable();
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();
            lvBinddata.Visible = false;
            Panel2.Visible = true;
            lvDatatableDisplay.DataSource = dtNew; 
            lvDatatableDisplay.DataBind();

            int i = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            {

                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Name = row[0];
                if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
                {

                    if (!(dtNew.Rows[i]["NPF_Discipline"]).ToString().Equals(string.Empty))
                    {
                        //objC.CourseName = (dtNew.Rows[i]["COURSENAME"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_Discipline at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["NPF_Programme"]).ToString().Equals(string.Empty))
                    {
                        //objC.CourseShortName = (dtNew.Rows[i]["SHORTNAME"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_Programme at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["NPF_SpecializationID"]).ToString().Equals(string.Empty))
                    {
                        //objC.CCode = (dtNew.Rows[i]["COURSECODE"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_SpecializationID at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }


                    if (!(dtNew.Rows[i]["NPF_Specialization"]).ToString().Equals(string.Empty))
                    {
                        //objC.Credits = Convert.ToDecimal((dtNew.Rows[i]["CREDITS"]).ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_Specialization at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }
                    if (!(dtNew.Rows[i]["College_ID"]).ToString().Equals(string.Empty))
                    {
                                
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter College_ID at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["School Name"]).ToString().Equals(string.Empty))
                    {
                        string School_Name = objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_NAME", "COLLEGE_ID=" + Convert.ToInt32((dtNew.Rows[i]["College_ID"]).ToString()));
                        if (!(dtNew.Rows[i]["School Name"]).ToString().Equals(School_Name))
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Valid School Name as per given Master Mapping at Row no. " + (i + 1), this.Page);
                            Session["Datatable"] = null;
                            lvDatatableDisplay.DataSource = null;
                            Panel2.Visible = false;
                            lvDatatableDisplay.DataBind();
                            lvBinddata.DataSource = null;
                            lvBinddata.DataBind();
                            lvBinddata.Visible = false;
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter School Name at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["DegreeNo"]).ToString().Equals(string.Empty))
                    {
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter DegreeNo at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["Degree"]).ToString().Equals(string.Empty))
                    {
                        string Degree_name = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "degreeno=" + Convert.ToInt32((dtNew.Rows[i]["DegreeNo"]).ToString()));
                        if (!(dtNew.Rows[i]["Degree"]).ToString().Equals(Degree_name))
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Valid Degree Name as per given Master Mapping at Row no. " + (i + 1), this.Page);
                            Session["Datatable"] = null;
                            lvDatatableDisplay.DataSource = null;
                            Panel2.Visible = false;
                            lvDatatableDisplay.DataBind();
                            lvBinddata.DataSource = null;
                            lvBinddata.DataBind();
                            lvBinddata.Visible = false;
                            return;
                        };
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter Degree at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["Branchno"]).ToString().Equals(string.Empty))
                    {
                        //objC.Credits = Convert.ToDecimal((dtNew.Rows[i]["CREDITS"]).ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter Branchno at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["Programme/Branch Name"]).ToString().Equals(string.Empty))
                    {
                        string Branch_name = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "branchno=" + Convert.ToInt32((dtNew.Rows[i]["Branchno"]).ToString()));
                        if (!(dtNew.Rows[i]["Programme/Branch Name"]).ToString().Equals(Branch_name))
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Valid Branch Name as per given Master Mapping at Row no. " + (i + 1), this.Page);
                            Session["Datatable"] = null;
                            lvDatatableDisplay.DataSource = null;
                            Panel2.Visible = false;
                            lvDatatableDisplay.DataBind();
                            lvBinddata.DataSource = null;
                            lvBinddata.DataBind();
                            lvBinddata.Visible = false;
                            return;
                        };
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter Programme/Branch Name at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["STUDENT_TYPE"]).ToString().Equals(string.Empty))
                    {
                        string STUDENT_TYPE = objCommon.LookUp("acd_idtype", "IDTYPEDESCRIPTION", "IDTYPENO=" + Convert.ToInt32((dtNew.Rows[i]["IDTYPE"]).ToString()));
                        if (!(dtNew.Rows[i]["STUDENT_TYPE"]).ToString().Equals(STUDENT_TYPE))
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Valid Student Type as per given Master Mapping at Row no. " + (i + 1), this.Page);
                            Session["Datatable"] = null;
                            lvDatatableDisplay.DataSource = null;
                            Panel2.Visible = false;
                            lvDatatableDisplay.DataBind();
                            lvBinddata.DataSource = null;
                            lvBinddata.DataBind();
                            lvBinddata.Visible = false;
                            return;
                        };
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter Stusent Type at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }

                    if (!(dtNew.Rows[i]["IDTYPE"]).ToString().Equals(string.Empty))
                    {
                       

                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlExceldata, "Please enter IdType at Row no. " + (i + 1), this.Page);
                        Session["Datatable"] = null;
                        lvDatatableDisplay.DataSource = null;
                        Panel2.Visible = false;
                        lvDatatableDisplay.DataBind();
                        lvBinddata.DataSource = null;
                        lvBinddata.DataBind();
                        lvBinddata.Visible = false;
                        return;
                    }
                }

            }
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnUploadExcel, "Please Upload Proper Excel File as per Sample Excel File", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void DatatableToDataBase()
    {
        //DataTable dt = new DataTable();
        //dt = (DataTable)Session["MyDataTable"];
        
         string NpfDiscipline=string.Empty;
        string NpfProgramme=string.Empty;
         int SpecId =0;
         string NpfSpecial=string.Empty;
         int CollegeId=0;
         string SchoolName=string.Empty;
        int DegreeNo=0;
         string Degree=string.Empty;
        int BranchNo=0;
        string ProgName=string.Empty;
         string CourseType=string.Empty;
         int IdType =0;
        int uano = Convert.ToInt32(Session["userno"].ToString());
        DataTable dt = Session["Datatable"] as DataTable;
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataRow row = dt.Rows[i];//ds.Tables[0].Rows[i];
                object Name = row[0];
                if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
                {
                    if (!(dt.Rows[i]["NPF_Discipline"]).ToString().Equals(string.Empty))

                        {
                            NpfDiscipline = (dt.Rows[i]["NPF_Discipline"]).ToString();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_Discipline at Row no. " + (i + 1), this.Page);
                            return;
                        }
                    
                    if (!(dt.Rows[i]["NPF_Programme"]).ToString().Equals(string.Empty))
                        {
                            NpfProgramme = (dt.Rows[i]["NPF_Programme"]).ToString();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_Programme at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["NPF_SpecializationID"]).ToString().Equals(string.Empty))
                        {
                            SpecId = Convert.ToInt32(dt.Rows[i]["NPF_SpecializationID"]);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_SpecializationID at Row no. " + (i + 1), this.Page);
                            return;
                        }


                        if (!(dt.Rows[i]["NPF_Specialization"]).ToString().Equals(string.Empty))
                        {
                            NpfSpecial = ((dt.Rows[i]["NPF_Specialization"]).ToString());
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter NPF_Specialization at Row no. " + (i + 1), this.Page);
                            return;
                        }
                        if (!(dt.Rows[i]["College_ID"]).ToString().Equals(string.Empty))
                        {

                            CollegeId = Convert.ToInt32((dt.Rows[i]["College_ID"]));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter College_ID at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["School Name"]).ToString().Equals(string.Empty))
                        {
                            SchoolName = ((dt.Rows[i]["School Name"]).ToString());
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter School Name at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["DegreeNo"]).ToString().Equals(string.Empty))
                        {
                            DegreeNo = Convert.ToInt32((dt.Rows[i]["DegreeNo"]));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter DegreeNo at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["Degree"]).ToString().Equals(string.Empty))
                        {
                            Degree = ((dt.Rows[i]["Degree"]).ToString());
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Degree at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["Branchno"]).ToString().Equals(string.Empty))
                        {
                            BranchNo = Convert.ToInt32((dt.Rows[i]["Branchno"]));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Branchno at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["Programme/Branch Name"]).ToString().Equals(string.Empty))
                        {
                            ProgName = ((dt.Rows[i]["Programme/Branch Name"]).ToString());
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter Programme/Branch Name at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["STUDENT_TYPE"]).ToString().Equals(string.Empty))
                        {
                            CourseType = ((dt.Rows[i]["STUDENT_TYPE"]).ToString());
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter STUDENT_TYPE Name at Row no. " + (i + 1), this.Page);
                            return;
                        }

                        if (!(dt.Rows[i]["IDTYPE"]).ToString().Equals(string.Empty))
                        {
                            IdType = Convert.ToInt32((dt.Rows[i]["IDTYPE"]).ToString());
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlExceldata, "Please enter IDTYPE at Row no. " + (i + 1), this.Page);
                            return;
                        }
                    }
                     int a=objStud.InsertToNpfDataTransfer( NpfDiscipline, NpfProgramme, SpecId , NpfSpecial, CollegeId , SchoolName,DegreeNo ,Degree ,BranchNo , ProgName, CourseType,IdType,ViewState["ipaddress"].ToString(),uano );
                     if (a==1)
                     {
                         objCommon.DisplayMessage(this.updpnlExceldata, "Data Inserted Successfully!!", this.Page);
                     
                        
                     }
                     else if (a == 2)
                     {
                         objCommon.DisplayMessage(this.updpnlExceldata, "Data Updated Successfully!!", this.Page);
                         

                     }
                     else
                     {
                         objCommon.DisplayMessage(this.updpnlExceldata, "Error!!", this.Page);

                         
                     }
            }
            

           
            }
         else
        {
            // DataTable is not in session
        }

        }
       
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["Datatable"] as DataTable;
        if (dt == null)
        {
            objCommon.DisplayMessage(this.Page, "Please Upload Excel File", this.Page);
        }
        else if (dt.Rows.Count > 0)
        {
            DatatableToDataBase();
            Session["Datatable"] = null;
            lvDatatableDisplay.DataSource=null;
            Panel2.Visible = false;
            lvDatatableDisplay.DataBind();
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();
            lvBinddata.Visible = false;
        }

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        DataTable myDataTable = Session["Datatable"] as DataTable;
        if (myDataTable == null)
        {
        }
        else
        {
            myDataTable.Clear();
        }
      
        lvDatatableDisplay.DataSource = null;
        lvDatatableDisplay.DataBind();
        lvDatatableDisplay.Visible = false;
        lvBinddata.DataSource = null;
        lvBinddata.DataBind();
        lvBinddata.Visible = false;

    }

    #endregion


    protected void btnShowDataDownload_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            SqlDataReader dr = objStud.DisplayNpfMapping();
            if (dr.HasRows != null)
            {
                dt1.Load(dr);

                dt1.Columns.RemoveAt(15);
                dt1.Columns.RemoveAt(14);
                dt1.Columns.RemoveAt(13);
                dt1.Columns.RemoveAt(12);
                dt1.AcceptChanges();

                ds.Tables.Add(dt1);
            }
            ds.Tables[0].TableName = "NPF Data Mapping";

            //if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
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
                    Response.AddHeader("content-disposition", "attachment;filename=NPF_Data_Mapping.xlsx");
                    using (MemoryStream MyMemoryStream1 = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream1);
                        MyMemoryStream1.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                //objCommon.DisplayMessage(this.UPDCOURSE, "Please Define All Masters!!", this.Page);
                Response.Write("<script>alert('Please Define All Masters')</script>");
            }
        }
        catch (Exception ex)
        { }


    }
}