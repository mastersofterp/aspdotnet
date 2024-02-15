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
using ClosedXML.Excel;


public partial class ACADEMIC_StudentFileUpload : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    StudentController studcon = new StudentController();
    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                    //CheckPageAuthorization();

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
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlConvocationNo, "ACD_CONVOCATION_MASTER", "CONV_NO", "CONVOCATION_NAME", "CONV_NO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "");
        // objCommon.FillDropDownList(ddlConvoType, "ACD_CONVOCATION_TYPE", "CONV_NO", "CONVOCATION_TYPE", "CONV_NO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "");
    }

    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        try
        {
            string path = MapPath("~/ExcelData/");
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileName.ToString().Contains(".xls"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))   ///ConvocationExcelSheetFormat.xls
                        Directory.CreateDirectory(path);

                    string[] array1 = Directory.GetFiles(path);

                    foreach (string str in array1)
                    {
                        if ((path + FileUpload1.FileName.ToString()).Equals(str))
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "File with similar name already exists!", this);
                            return;
                        }
                    }
                    ViewState["FileName"] = FileUpload1.FileName.ToString();
                    //Upload file to server
                    FileUpload1.SaveAs(MapPath("~/ExcelData/" + FileUpload1.FileName));

                    //Import Data
                    if (!CheckFormatAndImport())
                   // if (CheckFormatAndImportData())
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "File is not in correct format!!Please check if the data is saved in sheet1 or the column names do not match!", this.Page);
                        if (File.Exists(MapPath("~/ExcelData/") + ViewState["FileName"].ToString()))
                            File.Delete(MapPath("~/ExcelData/") + ViewState["FileName"].ToString());
                        lblTotalMsg.Text = "Error!!";
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Only Excel Sheet is Allowed!", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Select File to Upload!", this);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private bool CheckFormatAndImport()
    {
        string filename = ViewState["FileName"].ToString();

        String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
       "Data Source=" + MapPath("~/ExcelData/") + ViewState["FileName"].ToString() + ";" +
        "Extended Properties=Excel 8.0;";


        string Message = string.Empty;
        long ret = 0;
        int count = 0;
        OleDbConnection objCon = new OleDbConnection(sConnectionString);

        try
        {
            objCon.Open();

            OleDbCommand objcmd = new OleDbCommand("Select * from [SHEET1$] ", objCon);

            OleDbDataAdapter objda = new OleDbDataAdapter(objcmd);

            objda.SelectCommand = objcmd;
          
            DataSet ds = new DataSet();

           DataTable dt = new DataTable();
            objda.Fill(ds);

            // objda.Fill(dt)
             

            //To check user null entry used double verify Excel Data  
            int i = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string regno = ds.Tables[0].Rows[i]["REGNO"].ToString().Trim();
                    string studname = ds.Tables[0].Rows[i]["STUDNAME"].ToString().Trim();
                    string degree = ds.Tables[0].Rows[i]["DEGREE"].ToString().Trim();
                    string convocation_date = ds.Tables[0].Rows[i]["Convocation_Date"].ToString().Trim();
                    if (string.IsNullOrEmpty(regno))
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Regno", this.Page);
                        return false;
                    }
                    if (string.IsNullOrEmpty(studname))
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Student Name", this.Page);
                        return false;
                    }
                    if (string.IsNullOrEmpty(convocation_date))
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Convocation Date", this.Page);
                        return false;
                    }
                }
            }
          

            //int i = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ImportDataMaster objIDM = new ImportDataMaster();
                    ImportDataController objIDC = new ImportDataController();


                    string regno = ds.Tables[0].Rows[i]["REGNO"].ToString().Trim();
                    string studname = ds.Tables[0].Rows[i]["STUDNAME"].ToString().Trim();
                    string degree = ds.Tables[0].Rows[i]["DEGREE"].ToString().Trim();
                    string branch = ds.Tables[0].Rows[i]["BRANCH"].ToString().Trim();
                    string convocation_date = ds.Tables[0].Rows[i]["Convocation_Date"].ToString().Trim();
                    string classobtained = ds.Tables[0].Rows[i]["ClassObtained"].ToString().Trim();
                    string convocation = ddlConvocationNo.SelectedValue;

                                        
                    if (string.IsNullOrEmpty(degree))
                    {
                        degree = "-";
                    }
                    if (string.IsNullOrEmpty(branch))
                    {
                        branch = "-";
                    }
                    if (string.IsNullOrEmpty(convocation_date))
                    {
                        convocation_date = "-";
                    }
                     
                    if (string.IsNullOrEmpty(convocation))
                    {
                        convocation = "0";
                    }

                    /////Added dt on 10112022/////////////////////////////////////////////////////

                    //dt.Columns.AddRange(new DataColumn[] { //new DataColumn("Sr_No", typeof(int)),                
                    //                  new DataColumn("REGNO", typeof(Int32)),
                    //                  new DataColumn("STUDNAME", typeof(string)),
                    //                  new DataColumn("DEGREE", typeof(string)),
                    //                  new DataColumn("BRANCH", typeof(string)),
                    //                  new DataColumn("Convocation_Date", typeof(string)),
                    //                  new DataColumn("ClassObtained", typeof(string))                   
                    //});


                    //using (OleDbDataAdapter oda1 = new OleDbDataAdapter("SELECT * FROM [SHEET1$]", objCon))
                    //{
                    //    oda1.Fill(dt);
                    //}

                    if (regno != "" && studname != "" && convocation_date != "")
                    {
                        ////////////////////////////////////////////
                        ret = objIDC.InsertConvocationUser(regno, studname, degree, branch, convocation_date, classobtained, convocation);
                        //ret = objIDC.InsertConvocationUser(regno, studname, degree, branch, convocation_date, classobtained, convocation);
                        //ret = objIDC.InsertConvocationExcelData(dt);
                    }                    
                    
                    if (ret == 1)
                        count++;
                    else
                    {
                        count--;
                        return false;
                    }
                }
                lblTotalMsg.Text = "Total Records Saved:" + count.ToString();
                if (count < i)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please check the format of file & upload again!", this.Page);
                    return false;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Data Saved Successfully!", this.Page);
                    ddlConvoType.SelectedIndex = 0;
                    ddlConvocationNo.SelectedIndex = 0;
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            objCon.Close();
            objCon.Dispose();
        }

    }

    private void StudentTempExcel()
    {
        string excelname = objCommon.LookUp("ACD_CONVOCATION_MASTER", "CONVOCATION_NAME", "CONV_NO=" + ddlConvocationNo.SelectedValue);

        string SP_Name = "PKG_GET_CONVOCATION_USER_REPORT";
        string SP_Parameters = "@P_CONV_NO,@P_CONV_TYPE";
        string Call_Values = "" + ddlConvocationNo.SelectedValue + "," + ddlConvoType.SelectedValue + "";

         DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        //DataSet ds = studcon.GetConvocationUserData(Convert.ToInt32(ddlConvocationNo.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //string attachment = "attachment; filename=" + "ConvocationExcelUploadedDataSheet.xls";
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", attachment);
                //Response.ContentType = "application/" + "ms-excel";
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);

                //dg.DataSource = ds.Tables[0];
                //dg.DataBind();
                //dg.HeaderStyle.Font.Bold = true;
                //dg.RenderControl(htw);
                //Response.Write(sw.ToString());
                //Response.End();
                        
                DataTable dst = ds.Tables[0];
                string[] selectedColumns = new[] { "REGNO", "STUDNAME", "DEGREE", "Branch", "Convocation_date", "ClassObtained", "Convocation_type", "CONVOCATION_NAME" };

                DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
                dt.Columns["REGNO"].ColumnName = "Reg No"; // change column names
                dt.Columns["STUDNAME"].ColumnName = "Student Name";
                dt.Columns["DEGREE"].ColumnName = "Degree Name"; // change column names
                dt.Columns["BRANCH"].ColumnName = "Branch Name"; // change column names
                dt.Columns["Convocation_date"].ColumnName = "Convocation Date"; // change column names                
                dt.Columns["ClassObtained"].ColumnName = "Class Obtained"; // change column names
                dt.Columns["CONVOCATION_NAME"].ColumnName = "Convocation Name"; // change column names
                //  dt.Columns.Add("MARKS");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    //foreach (System.Data.DataTable dtt in dsStudent.Tables)
                    //{
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                    //}t
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".xlsx");

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
                objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
                return;
            }

        }
        else
        {
            objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
            return;
        }
    }

    protected void btnsheet_Click(object sender, EventArgs e)
    {
        try
        {
            StudentTempExcel();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {       
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO desc");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK), ACD_COLLEGE_DEGREE C WITH (NOLOCK), ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND (C.COLLEGE_ID=" + ddlCollege.SelectedValue + " OR " + ddlCollege.SelectedValue + "= 0)", "DEGREENO");
            ddlSession.Focus();
        }
        else
        {
            ddlCollege.SelectedIndex = 0;
        }
    }
    protected void ddlConvocationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
                 
    }

    #region typetable
    private bool CheckFormatAndImportData()
    {
        string filename = ViewState["FileName"].ToString();

        String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
       "Data Source=" + MapPath("~/ExcelData/") + ViewState["FileName"].ToString() + ";" +
        "Extended Properties=Excel 8.0;";


        string Message = string.Empty;
        long ret = 0;
        int count = 0;
        OleDbConnection objCon = new OleDbConnection(sConnectionString);

        try
        {

            objCon.Open();

            OleDbCommand objcmd = new OleDbCommand("Select * from [SHEET1$] ", objCon);

            OleDbDataAdapter objda = new OleDbDataAdapter(objcmd);

            objda.SelectCommand = objcmd;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            objda.Fill(ds);

            // objda.Fill(dt)


        
                //for (i = 0; i < dt.Rows.Count; i++)
                //{
                    ImportDataMaster objIDM = new ImportDataMaster();
                    ImportDataController objIDC = new ImportDataController();
         
                    dt.Columns.AddRange(new DataColumn[] { //new DataColumn("Sr_No", typeof(int)),                
                                      new DataColumn("REGNO", typeof(string)),
                                      new DataColumn("STUDNAME", typeof(string)),
                                      new DataColumn("DEGREE", typeof(string)),
                                      new DataColumn("BRANCH", typeof(string)),
                                      new DataColumn("Convocation_Date", typeof(string)),
                                      new DataColumn("ClassObtained", typeof(string))                   
                    });


                    using (OleDbDataAdapter oda1 = new OleDbDataAdapter("SELECT * FROM [SHEET1$]", objCon))
                    {
                        oda1.Fill(dt);
                    }


                    ////////////////////////////////////////////

                    //  ret = objIDC.InsertConvocationUser(regno, studname, degree, branch, convocation_date, classobtained, convocation);
                    //ret = objIDC.InsertConvocationExcelData(dt);

                    string SP = "PKG_IMPORT_DATA_CONVOCATION_DATA";
                    string Parameters = "@P_CONVO_EXCEL_DATA, @P_OUT";

                    string Values = dt + ",0";
                    string queout = objCommon.DynamicSPCall_IUD(SP, Parameters, Values, true);

                    if (queout == "1")
                    {                       
                       objCommon.DisplayMessage(UpdatePanel1, "Data Saved Successfully!", this.Page);
                        return true;                     
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Please check the format of file & upload again!", this.Page);
                        return false;
                    }                 
                
                lblTotalMsg.Text = "Total Records Saved:" + count.ToString();
           
             


            return false;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            objCon.Close();
            objCon.Dispose();
        }

    }

    #endregion

    private bool CheckFormatAndImport_New()
    {
        bool status = true;
        string filename = ViewState["FileName"].ToString();


        String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
       "Data Source=" + MapPath("~/ExcelData/") + ViewState["FileName"].ToString() + ";" +
        "Extended Properties=Excel 8.0;";


        string Message = string.Empty;
        long ret = 0;
        int count = 0;
        OleDbConnection objCon = new OleDbConnection(sConnectionString);
        try
        {
            //objCon.Open();

            //OleDbCommand objcmd = new OleDbCommand("Select * from [SHEET1$] ", objCon);

            //OleDbDataAdapter objda = new OleDbDataAdapter(objcmd);

            //objda.SelectCommand = objcmd;

            //DataSet ds = new DataSet();

            //DataTable dt = new DataTable();
            //objda.Fill(ds);

            // objda.Fill(dt)

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataSet ds = null;
            DataTable dt = new DataTable();
            cmdExcel.Connection = objCon;
            objCon.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = objCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            objCon.Close();
            //Read Data from First Sheet
            objCon.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);


            for (int ii = dt.Rows.Count - 1; ii >= 0; ii--)
            {
                if (dt.Rows[ii][1] == DBNull.Value)
                {
                    dt.Rows[ii].Delete();
                }
            }
            dt.AcceptChanges();


            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dt);


            //To check user null entry used double verify Excel Data  
            int i = ds1.Tables[0].Rows.Count;
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    string regno = ds1.Tables[0].Rows[i]["REGNO"].ToString().Trim();
                    string studname = ds1.Tables[0].Rows[i]["STUDNAME"].ToString().Trim();
                    string degree = ds1.Tables[0].Rows[i]["DEGREE"].ToString().Trim();
                    string convocation_date = ds1.Tables[0].Rows[i]["Convocation_Date"].ToString().Trim();

                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(regno))
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Regno", this.Page);
                            return status;
                        }
                        if (string.IsNullOrEmpty(studname))
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Student Name", this.Page);
                            return status;
                        }
                        if (string.IsNullOrEmpty(convocation_date))
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Convocation Date", this.Page);
                            return status;
                        }
                    }
                }
            }


            //int i = ds.Tables[0].Rows.Count;
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    ImportDataMaster objIDM = new ImportDataMaster();
                    ImportDataController objIDC = new ImportDataController();


                    string regno = ds1.Tables[0].Rows[i]["REGNO"].ToString().Trim();
                    string studname = ds1.Tables[0].Rows[i]["STUDNAME"].ToString().Trim();
                    string degree = ds1.Tables[0].Rows[i]["DEGREE"].ToString().Trim();
                    string branch = ds1.Tables[0].Rows[i]["BRANCH"].ToString().Trim();
                    string convocation_date = ds1.Tables[0].Rows[i]["Convocation_Date"].ToString().Trim();
                    string classobtained = ds1.Tables[0].Rows[i]["ClassObtained"].ToString().Trim();
                    string convocation = ddlConvocationNo.SelectedValue;


                    if (string.IsNullOrEmpty(degree))
                    {
                        degree = "-";
                    }
                    if (string.IsNullOrEmpty(branch))
                    {
                        branch = "-";
                    }
                    if (string.IsNullOrEmpty(convocation_date))
                    {
                        convocation_date = "-";
                    }

                    if (string.IsNullOrEmpty(convocation))
                    {
                        convocation = "0";
                    }



                    /////Added dt on 10112022/////////////////////////////////////////////////////

                    //dt.Columns.AddRange(new DataColumn[] { //new DataColumn("Sr_No", typeof(int)),                
                    //                  new DataColumn("REGNO", typeof(Int32)),
                    //                  new DataColumn("STUDNAME", typeof(string)),
                    //                  new DataColumn("DEGREE", typeof(string)),
                    //                  new DataColumn("BRANCH", typeof(string)),
                    //                  new DataColumn("Convocation_Date", typeof(string)),
                    //                  new DataColumn("ClassObtained", typeof(string))                   
                    //});


                    //using (OleDbDataAdapter oda1 = new OleDbDataAdapter("SELECT * FROM [SHEET1$]", objCon))
                    //{
                    //    oda1.Fill(dt);
                    //}

                    if (regno != "" && studname != "" && convocation_date != "")
                    {
                        ////////////////////////////////////////////
                        ret = objIDC.InsertConvocationUser(regno, studname, degree, branch, convocation_date, classobtained, convocation);
                        //ret = objIDC.InsertConvocationUser(regno, studname, degree, branch, convocation_date, classobtained, convocation);
                        //ret = objIDC.InsertConvocationExcelData(dt);
                    }

                    if (ret == 1)
                        count++;
                    else
                    {
                        count--;
                        return status;
                    }
                }
                lblTotalMsg.Text = "Total Records Saved:" + count.ToString();
                if (count < i)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please check the format of file & upload again!", this.Page);
                    return status;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Data Saved Successfully!", this.Page);
                    return status;
                }
            }
            return status;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            objCon.Close();
            objCon.Dispose();
        }

    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        this.GenerateExcel();
        //this.GetData();
        //  this.GenerateExcel1();
    }

    private void GenerateExcel()
    {
        bool CompleteRequest = false;
        string excelname = objCommon.LookUp("ACD_CONVOCATION_MASTER", "CONVOCATION_NAME", "CONV_NO=" + ddlConvocationNo.SelectedValue);
        string type = ddlConvoType.SelectedValue;
        string column = string.Empty;
        if (type == "0")
        {
            column = "CONVOCATION_DATE";
        }
        else
        {
            column = "DEGREE_AWARD_CEREMONY_DATE";
        }

        string SP_Name = "PKG_GET_CONVOCATION_USER_EXCEL_GENERATE";
        string SP_Parameters = "@P_CONV_TYPE";  //@P_CONV_NO, 
        string Call_Values = "" + ddlConvoType.SelectedValue + "";

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        DataGrid dg = new DataGrid();
        DataTable dst = ds.Tables[0];
        string[] selectedColumns = new[] { "REGNO", "STUDNAME", "DEGREE", "BRANCH", "CONVOCATION_TYPE", "CLASSOBTAINED" }; //+ excelname + ""

        DataTable dt = new DataView(dst).ToTable(false, selectedColumns);

        dt.Columns["CONVOCATION_TYPE"].ColumnName = column;
        GridView GV = new GridView();
        //using (XLWorkbook wb = new XLWorkbook())
        using (var wb = new ClosedXML.Excel.XLWorkbook())
        {
            //  DataTable dt1 = ds.Tables[0];

            ClosedXML.Excel.IXLWorksheet sheet = wb.Worksheets.Add(dt);

            // wb.Worksheets.Add(dt);

            sheet.Column(1).Width = 30;
            sheet.Column(2).Width = 30;
            sheet.Column(3).Width = 30;
            sheet.Column(4).Width = 30;
            sheet.Column(5).Width = 30;
            sheet.Column(6).Width = 30;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + excelname + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                //  Response.End(); 
            }

            CompleteRequest = true;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlConvocationNo.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Convocation Name", this.Page);
                return;
            }
            else if (ddlConvoType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Convocation Type", this.Page);
                return;
            }
            else
            {
                lblTotalMsg.Text = string.Empty;
                string path = MapPath("~/ExcelData/");
                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.FileName.ToString().Contains(".xls"))
                    {
                        if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))   ///ConvocationExcelSheetFormat.xls
                            Directory.CreateDirectory(path);

                        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                        string file = Path.GetFileNameWithoutExtension(FileUpload1.FileName.ToString());
                        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                        string filename = file + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ext;

                        string[] array1 = Directory.GetFiles(path);

                        foreach (string str in array1)
                        {
                            if ((path + filename).Equals(str))
                            // if ((path + FileUpload1.FileName.ToString()).Equals(str))
                            {
                                objCommon.DisplayMessage(this.Page, "File with similar name already exists!", this.Page);
                                return;
                            }
                        }

                        ViewState["FileName"] = filename;  //FileUpload1.FileName.ToString();
                        //Upload file to server 
                        //  FileUpload1.SaveAs(MapPath("~/ExcelData/" + FileUpload1.FileName));
                        FileUpload1.SaveAs(MapPath("~/ExcelData/" + filename));


                        // string Filepath = Server.MapPath("~/ExcelData/" + FileUpload1.FileName);
                        string Filepath = Server.MapPath("~/ExcelData/" + filename);

                        if (CheckFormatAndImport1(Extension, Filepath, "yes") == false)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "File is not in correct format!!Please check if the data is saved in sheet1 or the column names do not match!", this.Page);
                            if (File.Exists(MapPath("~/ExcelData/") + ViewState["FileName"].ToString()))
                                File.Delete(MapPath("~/ExcelData/") + ViewState["FileName"].ToString());
                           // lblTotalMsg.Text = "Error!!";
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Only Excel Sheet is Allowed!", this);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Select File to Upload!", this);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool CheckFormatAndImport1(string Extension, string FilePath, string isHDR)  //bool
    {
        ImportDataMaster objIDM = new ImportDataMaster();
        ImportDataController objIDC = new ImportDataController();

        bool status = true;
        string filename = ViewState["FileName"].ToString();
        Exam objExam = new Exam();
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 Excel07ConString
                //   conStr = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }

        conStr = String.Format(conStr, FilePath);

        string Message = string.Empty;
        int count = 0;
        OleDbConnection connExcel = new OleDbConnection(conStr);

        try
        {

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataSet ds = null;
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

            //string SheetName = "TableMark$";//dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);


            //for (int ii = dt.Rows.Count - 1; ii >= 0; ii--)
            //{
            //    if (dt.Rows[ii][1] == DBNull.Value)
            //    {
            //        dt.Rows[ii].Delete();
            //    }
            //}
            //dt.AcceptChanges();

            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dt);

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                #region ToValidExcelAsPerConvocationType

                if (ddlConvoType.SelectedValue == "0")
                {
                    string format = string.Empty;
                    string type = ds1.Tables[0].Columns[4].ToString();

                    format = type.Replace("_DATE", " ");
                    format = format.Trim();
                    if (format == "CONVOCATION" || format == "Convocation")
                    {
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please check the convocation type format of file", this.Page);
                        status = false;
                        return status;
                    }
                }
                else
                {
                    string format = string.Empty;
                    string type = ds1.Tables[0].Columns[4].ToString();

                    format = type.Replace("_DATE", " ");
                    format = format.Trim();
                    if (format == "DEGREE_AWARD_CEREMONY" || format == "Degree_Award_Ceremony")
                    {
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please check the convocation type format of file", this.Page);
                        status = false;
                        return status;
                    }
                }
                #endregion 

                #region Validation

                //To check user null entry used double verify Excel Data  
                int i = ds1.Tables[0].Rows.Count;
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string regno = ds1.Tables[0].Rows[i]["REGNO"].ToString().Trim();
                        string studname = ds1.Tables[0].Rows[i]["STUDNAME"].ToString().Trim();
                        string degree = ds1.Tables[0].Rows[i]["DEGREE"].ToString().Trim();
                        // string convocation_date = ds1.Tables[0].Rows[i]["Convocation_Date"].ToString().Trim();

                        string colmn = ds1.Tables[0].Columns[4].ToString();
                        string convocation_date = ds1.Tables[0].Rows[i]["" + colmn + ""].ToString().Trim();

                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            if (string.IsNullOrEmpty(regno))
                            {
                                objCommon.DisplayMessage(this.Page, "Please Enter Regno", this.Page);
                                return status;
                            }
                            if (string.IsNullOrEmpty(studname))
                            {
                                objCommon.DisplayMessage(this.Page, "Please Enter Student Name", this.Page);
                                return status;
                            }
                            if (string.IsNullOrEmpty(convocation_date))
                            {
                                objCommon.DisplayMessage(this.Page, "Please Enter Convocation Date", this.Page);
                                return status;
                            }
                        }
                    }
                }
                #endregion 


                //int i = ds.Tables[0].Rows.Count;
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string regno = ds1.Tables[0].Rows[i]["REGNO"].ToString().Trim();
                        string studname = ds1.Tables[0].Rows[i]["STUDNAME"].ToString().Trim();
                        string degree = ds1.Tables[0].Rows[i]["DEGREE"].ToString().Trim();
                        string branch = ds1.Tables[0].Rows[i]["BRANCH"].ToString().Trim();
                        //string convocation_date = ds1.Tables[0].Rows[i]["Convocation_Date"].ToString().Trim();
                        string classobtained = ds1.Tables[0].Rows[i]["ClassObtained"].ToString().Trim();
                        string convocation = ddlConvocationNo.SelectedValue;

                        string colmn = ds1.Tables[0].Columns[4].ToString();
                        string convocation_date = ds1.Tables[0].Rows[i]["" + colmn + ""].ToString().Trim();


                        if (string.IsNullOrEmpty(degree))
                        {
                            degree = "-";
                        }
                        if (string.IsNullOrEmpty(branch))
                        {
                            branch = "-";
                        }
                        if (string.IsNullOrEmpty(convocation_date))
                        {
                            convocation_date = "-";
                        }
                        if (string.IsNullOrEmpty(convocation))
                        {
                            convocation = "0";
                        }

                        int ret = 0;
                        if (regno != "" && studname != "" && convocation_date != "")
                        {
                            //ret = objIDC.InsertConvocationUser(regno, studname, degree, branch, convocation_date, classobtained, convocation);
                            ret = InsertConvocationUserNew(regno, studname, degree, branch, convocation_date, classobtained, convocation, Convert.ToInt32(ddlConvoType.SelectedValue));
                        }

                        if (ret == 1)
                            count++;
                        else
                        {
                            count--;
                            return status;
                        }
                    }
                    lblTotalMsg.Text = "Total Records Saved:" + count.ToString();
                    if (count < i)
                    {
                        objCommon.DisplayMessage(this.Page, "Please check the format of file & upload again", this.Page);
                        status = false;
                        return status;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Data Saved Successfully", this.Page);
                        return status;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please check the format of file & upload again!", this.Page);
                status = false;
                return status;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Please Check if the data is saved in sheet1 of the file you are uploading or the file is in correct format!! ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
        finally
        {
            connExcel.Close();
            connExcel.Dispose();
        }

        return true;
    }

    public int InsertConvocationUserNew(string regno, string studname, string degree, string branch, string convo_date, string class_obtained, string convo, int type)
    {
        int pkid = 0;
        try
        {

            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = null;
            object ret = null;
            objParams = new SqlParameter[9];
            objParams[0] = new SqlParameter("@P_REGNO", regno);
            objParams[1] = new SqlParameter("@P_STUDNAME", studname);
            objParams[2] = new SqlParameter("@P_DEGREE", degree);
            objParams[3] = new SqlParameter("@P_BRANCH", branch);
            objParams[4] = new SqlParameter("@P_CONVOCATION_DATE", convo_date);
            objParams[5] = new SqlParameter("@P_CLASS", class_obtained);
            objParams[6] = new SqlParameter("@P_CONV_NO", convo);
            objParams[7] = new SqlParameter("@P_CONV_TYPE", type);
            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[8].Direction = ParameterDirection.Output;
            ret = objSQLHelper.ExecuteNonQuerySP("PKG_IMPORT_DATA_CONVOCATION_USER_NEW", objParams, true);
            if (ret != null)
            {
                if (ret.ToString().Equals("-99"))
                    pkid = 99;
                else
                    pkid = Convert.ToInt16(ret.ToString());
            }
            else
                pkid = 99;

        }
        catch (Exception ee)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataController.InsertToSqlDB-> " + ee.ToString());
        }
        return pkid;
    }
}