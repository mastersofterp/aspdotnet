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
        objCommon.FillDropDownList(ddlConvocationNo, "ACD_CONVOCATION_MASTER", "CONV_NO", "CONVOCATION_NAME", "CONV_NO > 0", "");
         
    }

    protected void btnUpload_Click(object sender, EventArgs e)
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
        string SP_Parameters = "@P_CONV_NO";
        string Call_Values = "" + ddlConvocationNo.SelectedValue + "";

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
                string[] selectedColumns = new[] { "REGNO", "STUDNAME", "DEGREE", "BRANCH", "Convocation_date", "ClassObtained" };

                DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
                dt.Columns["REGNO"].ColumnName = "Reg No"; // change column names
                dt.Columns["DEGREE"].ColumnName = "Degree Name"; // change column names
                dt.Columns["BRANCH"].ColumnName = "Branch Name"; // change column names
                dt.Columns["Convocation_date"].ColumnName = "Convocation Date"; // change column names                
                dt.Columns["ClassObtained"].ColumnName = "Class Obtained"; // change column names
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
}