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




public partial class ACADEMIC_StudentNameUpdationByExcel : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //fill dropdown

                //get ip address
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }

        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    //private void BindListView()
    //{
    //    tblImport.Visible = true;
    //    DataSet dsStudent = objCommon.FillDropDown("TEMP_STUDENT T INNER JOIN ACD_SEMESTER S ON(T.SEMESTERNO = S.SEMESTERNO)INNER JOIN ACD_DEGREE D ON(T.DEGREENO = D.DEGREENO)INNER JOIN ACD_BRANCH B ON(T.BRANCHNO = B.BRANCHNO)INNER JOIN ACD_COLLEGE_MASTER CM ON(T.COLLEGE_ID = CM.COLLEGE_ID)", "*", "T.COLLEGE_ID,S.SEMESTERNAME,D.DEGREENAME,B.LONGNAME,CM.COLLEGE_NAME", "T.ADMBATCH =  " + ddlAdmbatch.SelectedValue + " and T.COLLEGE_ID=" + ddlCollege.SelectedValue + " and T.DEGREENO=" + ddlDegree.SelectedValue + " and T.BRANCHNO=" + ddlBranch.SelectedValue + "  AND T.SEMESTERNO = " + ddlSemester.SelectedValue + "", "SRNO");

    //    lvImportedList.DataSource = dsStudent.Tables[0];
    //    lvImportedList.DataBind();

    //}

    protected void lbExcelFormat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        //string path = Server.MapPath(string.Empty).Replace("HOSTEL", "IMAGES\\HOSTEL_ALLOTMENT_FORMAT.xls");
        string path = Server.MapPath("~/ExcelFormat/Upload Excel.xlsx");
        //string path = Server.MapPath(string.Empty).Replace("Presentation","ExcelData/PreFormat_For_UG_RollSheet.xls");
        //string path = "PresentationLayer/ExcelData/PreFormat_For_UG_RollSheet.xls";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"Upload Excel.xlsx\"");
        Response.TransmitFile(path);
        Response.End();
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
                //string categoryno = objCommon.LookUp("ACD_CATEGORY","CATEGORYNO","CATEGORY =''")
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[] 
                { 
                    new DataColumn("EnrollmentNo", typeof(int)),
                new DataColumn("StudentMobileNo", typeof(string)),
                new DataColumn("StudentEmailId", typeof(string)),
                new DataColumn("StudentIndusEmail", typeof(string)),
                  new DataColumn("FatherMobileNo", typeof(string)),
                new DataColumn("MotherMobile", typeof(string)),
                       new DataColumn("FatherEmail", typeof(string)),
                new DataColumn("MotherEmail",typeof(string))});
          
          

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                ImportDataMaster objIDM = new ImportDataMaster();
                ImportDataController objIDC = new ImportDataController();

                //objIDM.SESSIONNO = Convert.ToInt16(ddlSession.SelectedValue);
                //objIDM.COLLEGEID = Convert.ToInt16(ddlCollege.SelectedValue);
                //objIDM.DEGREENO = Convert.ToInt16(ddlDegree.SelectedValue);
                //objIDM.BRANCHNO = Convert.ToInt16(ddlBranch.SelectedValue);
                //objIDM.SEMESTERNO = Convert.ToInt16(ddlSemester.SelectedValue);
                //objIDM.Admbatch = Convert.ToInt32(ddlAdmbatch.SelectedValue);
                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

                int ret = objIDC.ImportDataForStudentREgistration(objIDM, dtExcelData, Convert.ToInt16(Session["userno"]));

                if (ret > 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Total " + ret + " records uploaded successfully.", this.Page);
                    lblTotalMsg.Text = "Total records uploaded : " + ret;
                    lblTotalMsg.Style.Add("color", "green");
                    return true;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Record not uploaded, file is not in correct format. Please check the format of file & upload again.", this.Page);
                    lblTotalMsg.Text = "Error.!! Record not saved.";
                    return false;
                }

              

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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        {
            try
            {


                string path = Server.MapPath("~/ExcelData/");

                if (FileUpload1.HasFile)
                {
                    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                    if (extension.Contains(".xls") || extension.Contains(".xlsx"))
                    {
                        if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                            Directory.CreateDirectory(path);

                        string fileName = FileUpload1.PostedFile.FileName.Trim();


                        //Check file to server with same name and delete
                        if (File.Exists((path + fileName).ToString()))
                            File.Delete((path + fileName).ToString());

                        //Upload file to server
                        FileUpload1.SaveAs(path + fileName);

                        ////Import Data
                        CheckFormatAndImport(extension, path + fileName);
                        //aayushi
                         //this.BindListView();

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
                        //lblTotalMsg.Text = "Only excel sheet is allowed to upload";
                        objCommon.DisplayMessage(UpdatePanel1, "Only excel sheet is allowed to upload.", this);
                        return;
                    }
                }
                else
                {
                    // lblTotalMsg.Text = "Please select file to upload.";
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
    }
}