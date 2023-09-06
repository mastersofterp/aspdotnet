using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Configuration;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_StudentFeesUpload : System.Web.UI.Page
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
                    PopulateDropDownList();
                }              
            }  
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeesUpload.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
        objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE IN('MESS','TF') AND RCPTTYPENO>0", "");
    }
    #endregion
    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollegeName.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegreeName, "ACD_COLLEGE_MASTER CM INNER JOIN  ACD_COLLEGE_DEGREE ACD ON(CM.COLLEGE_ID=ACD.COLLEGE_ID)INNER JOIN ACD_DEGREE AD ON(AD.DEGREENO=ACD.DEGREENO)", "DISTINCT AD.DEGREENO", "AD.DEGREENAME", "CM.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) , "AD.DEGREENO");
                ddlDegreeName.Focus();
            }

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            tblenrollmentno.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegreeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegreeName.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegreeName.SelectedValue), "LONGNAME");
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
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT (DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO=" + Convert.ToInt32(ddlDegreeName.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ")", "SEMESTERNO");
        }
        catch (Exception ex)
        { }
    }
    protected void lbExcelFormat_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/vnd.ms-excel";
        //string path = Server.MapPath(string.Empty).Replace("HOSTEL", "IMAGES\\HOSTEL_ALLOTMENT_FORMAT.xls");
        //string path = Server.MapPath("~/ExcelData/PreFormat_For_UG_RollSheet.xls");
        string path = Server.MapPath("~/ExcelFormat/PreFormat_For_Student_Fees_Paid_Sheet.xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"PreFormat_For_Student_Fees_Paid_Sheet.xls\"");
        Response.TransmitFile(path);
        Response.End();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try{
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
                    objCommon.DisplayMessage(this.updfeesupload, "Only excel sheet is allowed to upload.", this.Page);
                    return;
                }
            }
            else
            {
                lblTotalMsg.Text = "Please select file to upload.";
                objCommon.DisplayMessage(this.updfeesupload, "Please select file to upload.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
              if (Convert.ToBoolean(Session["error"]) == true)
                  objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
             // lblTotalMsg.Text = "Error.!! Record not saved.";
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
                DataTable dtExcelDataTemp = new DataTable();
                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[] {
                new DataColumn("Roll No", typeof(string)),
                new DataColumn("Student Name", typeof(string)),
                //new DataColumn("Semester", typeof(string)),
                //new DataColumn("Student Mobile No", typeof(string)),
                new DataColumn("Receipt Type", typeof(string)),
                new DataColumn("Amount", typeof(string)),
                });
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                    DataSet ds1 = new DataSet();
                    ds1.Tables.Add(dtExcelData);
                    foreach (DataTable table in ds1.Tables)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            //double num;
                            //int sem;
                            //bool isNum = double.TryParse(dr["Amount"].ToString(), out num);
                            //bool isSem = int.TryParse(dr["Semester"].ToString(), out sem);
                            if (Convert.ToString(ddlReceiptType.SelectedValue).ToUpper() == dr["Receipt Type"].ToString().ToUpper())
                            {
                            }
                            else
                            {
                                goto label3;
                            }
                            //if (isSem)
                            //{ }
                            //else
                            //{
                            //    goto label2;
                            //}
                            //if (isNum)
                            //{

                            //}
                            //else
                            //{
                            //    //if (dr["Amount"].ToString() == "AB" || dr["Amount"].ToString() == "NE" || dr["SEE MARKS"].ToString() == "WD" || dr["SEE MARKS"].ToString() == "DR" || dr["SEE MARKS"].ToString() == "")
                            //    //{

                            //    //}
                            //    //else
                            //    //{
                            //        goto label1;
                            //    //}
                            //}
                        }
                    }
                }
                excel_con.Close();
                ImportDataMaster objIDM = new ImportDataMaster();
                ImportDataController objIDC = new ImportDataController();

                objIDM.SESSIONNO = Convert.ToInt16(ddlSession.SelectedValue);
                objIDM.COLLEGEID = Convert.ToInt16(ddlCollegeName.SelectedValue);
                //objIDM.DEGREENO = Convert.ToInt16(ddlDegreeName.SelectedValue);
                //objIDM.BRANCHNO = Convert.ToInt16(ddlBranch.SelectedValue);
                //objIDM.SEMESTERNO = Convert.ToInt16(ddlSemester.SelectedValue);
                objIDM.DEGREENO   = 0;
                objIDM.BRANCHNO   = 0;
                objIDM.SEMESTERNO = 0;
                string receipttype = ddlReceiptType.SelectedValue.ToString();
                objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

                int retout = 0;
                DataSet ds = null;
                ds = objIDC.ImportExcelStudentFeesData(objIDM, dtExcelData, Convert.ToInt16(Session["userno"]), receipttype);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        retout = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                        if (retout == -1002)
                        {
                            tblenrollmentno.Visible = true;
                        }
                        else
                        { 
                            tblenrollmentno.Visible = false;
                           
                        }
                    }
                }
                switch (retout)
                {
                    case 1:     ///Verified and course registered successfully
                        btnVerifyRegister.Enabled = true;
                        ClearMismatchList();
                        //lblTotalMsg.Text = "Records verified  successfully.";
                        //lblTotalMsg.Style.Add("color", "green");
                        //objCommon.DisplayMessage(updfeesupload, "Records verified successfully", this.Page);
                         int cntExcelUpload = 0;

                         cntExcelUpload = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_FEE_AMT", "ISNULL(COUNT(REGNO),0)CNT", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + " AND RECEIPT_TYPE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
                        //cntExcelUpload = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_FEE_AMT", "COUNT(REGNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + " AND RECEIPT_TYPE= '" + Convert.ToInt32(ddlReceiptType.SelectedValue)+"'"));
                           
                            if (cntExcelUpload > 0)
                            {
                               // VerifyandRegister();
                                objCommon.DisplayMessage(this.updfeesupload, "Student Fees Amount Saved Successfully!!", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updfeesupload, "Excel sheet is not uploaded for given selection. First upload excel sheet then verify!!.", this.Page);
                            }
                        break;
                    case -99:   ///Error occoured while saving
                        ClearMismatchList();
                        lblTotalMsg.Text = "Error occurred in record verification.";
                        objCommon.DisplayMessage(updfeesupload, "Error occurred in record verification.", this.Page);
                        break;
                    case -1002: ///Records mismatch Not verified
                        lvStudent.DataSource = ds.Tables[0];
                        lvStudent.DataBind();
                        btnVerifyRegister.Enabled = false;
                       // lblTotalMsg.Text = "Mismatch Found. Records not verified.\nPlease correct the excel sheet records and re-upload it.";
                        objCommon.DisplayMessage(updfeesupload, "Student Fees Updated Successfully and the mismatch Roll numbers are Not Inserted and shown in the List", this.Page);
                        break;
                }
            //label1:
            //    objCommon.DisplayMessage(this.updfeesupload, "Record not saved, file is not in correct format. Please check the Amount & upload again.", this.Page);
            //    return true;
            //label2:
            //    objCommon.DisplayMessage(this.updfeesupload, "Record not saved, file is not in correct format. Please check the Semester & upload again.", this.Page);
            //    return true;
            label3:
                objCommon.DisplayMessage(this.updfeesupload, "Receipt Type Mismatch Occured Please check the ReceiptType & upload again.", this.Page);
                return true;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updfeesupload, "Record not saved, file is not in correct format. Please check the format of file & upload again.", this.Page);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Records not saved. Please check whether the file is in correct format or not. ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
    }

    protected void btnVerifyRegister_Click(object sender, EventArgs e)
    {

    }

    private void VerifyandRegister()
    {
        ImportDataMaster objIDM = new ImportDataMaster();
        ImportDataController objIDC = new ImportDataController();

        objIDM.SESSIONNO = Convert.ToInt16(ddlSession.SelectedValue);
        objIDM.COLLEGEID = Convert.ToInt16(ddlCollegeName.SelectedValue);
        objIDM.DEGREENO = Convert.ToInt16(ddlDegreeName.SelectedValue);
        objIDM.BRANCHNO = Convert.ToInt16(ddlBranch.SelectedValue);
        objIDM.SEMESTERNO = Convert.ToInt16(ddlSemester.SelectedValue);
        objIDM.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        //Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlStudentType.SelectedValue)
        string rectype=ddlReceiptType.SelectedValue.Trim().ToString();

        DataSet ds = null;

        int retout = 0;
        retout = objIDC.VerifyandRegisterImportedStudentBulkFeesData(objIDM, Convert.ToInt16(Session["userno"]), rectype);//, out retout

        switch (retout)
        {
            case 1:     ///Verified and course registered successfully
                ClearMismatchList();
                this.ClearAll();
                lblTotalMsg.Text = "Student Fees Amount Saved Successfully!!.";
                lblTotalMsg.Style.Add("color", "green");
                objCommon.DisplayMessage(this.updfeesupload, "Student Fees Amount Saved Successfully!!", this.Page);
                break;
            case -99:   ///Error occoured while saving
                ClearMismatchList();
                lblTotalMsg.Text = "Error occurred in record verification ";
                objCommon.DisplayMessage(this.updfeesupload, "Error occurred in record verification.", this.Page);
                break;
            case -1001: ///Registration already exists cannot register
                ClearMismatchList();
                lblTotalMsg.Text = "";
                objCommon.DisplayMessage(this.updfeesupload, "", this);
                break;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }
    private void ClearAll()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollegeName.SelectedIndex = 0;
        ddlDegreeName.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lblTotalMsg.Text = string.Empty;
        ddlReceiptType.SelectedIndex = 0;
        ClearMismatchList();
    }

    private void ClearMismatchList()
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("StudentFeesExcelUploadReport", "rptstudentfeesexceluploadreport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeesUpload.btnPrintRegSlip_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        // int sessionno = Convert.ToInt32(Session["currentsession"].ToString());
        int college_id = 0;
        if (Convert.ToInt32(ddlCollegeName.SelectedIndex) == 0)
        {
            college_id = 0;
        }
        else
        {
            college_id = Convert.ToInt32(ddlCollegeName.SelectedValue);
        }
        string receipttype = string.Empty;
        if (Convert.ToInt32(ddlReceiptType.SelectedIndex) == 0)
        {
            receipttype = string.Empty;
        }
        else
        {
            receipttype = Convert.ToString(ddlReceiptType.SelectedValue);
        }
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + college_id + ",@P_RECEIPT_CODE=" + Convert.ToString(receipttype) + ",@P_USER=" + Session["username"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updfeesupload, this.updfeesupload.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeesUpload.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnsummary_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("StudentFeesExcelUploadSummaryReport", "rptstudentfeesexceluploadsummaryreport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeesUpload.btnsummary_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}