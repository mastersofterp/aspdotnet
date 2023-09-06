using BusinessLogicLayer.BusinessLogic.PostAdmission;
using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exam_Mark_Upload : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPExamMarkUploadController ObjUpload = new ADMPExamMarkUploadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Check User Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["colcode"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH DESC");
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH ASC");
                ddlAdmissionBatch.SelectedIndex = 1;
            }
        }
    }
    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
            ddlDegree.SelectedIndex = 0;
            lstProgram.Items.Clear();
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMPExam_Mark_Upload.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = ObjUpload.GetBranch(Degree);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Branch/Program.", this.Page);
                return;
            }

            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();


            DataSet dsmark = ObjUpload.getStudentMarkExcel(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlProgramType.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), branchno);
            if (dsmark.Tables[0].Rows.Count > 0)
            {
                //dsmark.Tables[0].Columns.Remove(0);
                dsmark.Tables[0].Columns.Remove(dsmark.Tables[0].Columns["USERNO"]);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsmark.Tables)
                    {
                        wb.Worksheets.Add(dt);
                    }
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=EntranceExamMarkEntry.xlsx");
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
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnUploadExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["fileName"] = string.Empty;
            string fileType = string.Empty;
            string path = string.Empty;
            string fileName = string.Empty;
            string date = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            path = MapPath("~/UPLOAD_FILES/ADMPMARKS_ENTRY");
            if (!(Directory.Exists(MapPath("~/UPLOAD_FILES/ADMPMARKS_ENTRY"))))
                Directory.CreateDirectory(path);
            fileName = fuUpload.FileName + "_" + date;
            if (fileName.Equals(string.Empty))
            {
                objCommon.DisplayMessage(this.Page, "Please Select File to Upload.", this.Page);
                return;
            }
            if (fuUpload.HasFile)
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
            string ext = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
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
                fuUpload.PostedFile.SaveAs(Server.MapPath("~//UPLOAD_FILES//ADMPMARKS_ENTRY//") + fileName);
                fileType = System.IO.Path.GetExtension(fileName);
                string FilePath = (Server.MapPath("~//UPLOAD_FILES//ADMPMARKS_ENTRY//") + fileName);
                ViewState["FILENAME"] = fileName.ToString();
                //Import_To_Grid(FilePath, ext);
                ExcelToDatabase(FilePath, ext, "yes");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EntranceExamMarkEntry.btnUploadexcel_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        try
        {
            //double chkMark = 0;
            //string marks = "";
            //if (double.TryParse(marks, out chkMark))
            //{
            //}

            CustomStatus cs = new CustomStatus();
            string conStr = "";
            switch (Extension)
            {
                //case ".xls": //Excel 97-03
                //    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                //    break;

                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2\'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2\'";
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
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
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
            DataTable dtNew = dv1.ToTable();

            DataRow dr = null;
            DataTable dtMarks = new DataTable("TBL_EXAM_MARK_ENTRY_EXCEL");
            //dtMarks.Columns.Add("Userno", typeof(string));
            dtMarks.Columns.Add("Applicant_ID", typeof(string));
            dtMarks.Columns.Add("Applicant_Name", typeof(string));
            dtMarks.Columns.Add("Marks", typeof(string));
            dtMarks.Columns.Add("LockStatus", typeof(string));
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dtNew.Rows[i][0].ToString()))
                {
                    dr = dtMarks.NewRow();
                    //dr["Userno"] = dtNew.Rows[i][0].ToString();
                    dr["Applicant_ID"] = dtNew.Rows[i][0].ToString();
                    dr["Applicant_Name"] = dtNew.Rows[i][1].ToString();
                    dr["Marks"] = dtNew.Rows[i][2].ToString();
                    dr["LockStatus"] = dtNew.Rows[i][3].ToString();
                    //dr["LongName"] = dtNew.Rows[i][4].ToString();
                    //dr["DegreeName"] = dtNew.Rows[i][5].ToString();
                    //dr["Negative_Marks"] = dtNew.Rows[i][6].ToString();
                    //dr["Marks"] = dtNew.Rows[i][7].ToString();                   
                    dtMarks.Rows.Add(dr);
                }
            }
            connExcel.Close();
            if (dtMarks.Rows.Count > 0)
            {
                divExamMarkEntryExcelData.Visible = true;
                lvMarkEntry.DataSource = dtMarks;
                lvMarkEntry.DataBind();
                lvMarkEntry.Visible = true;
                btnSubmit.Visible = true;
            }
            else
            {
                divExamMarkEntryExcelData.Visible = false;
                lvMarkEntry.DataSource = null;
                lvMarkEntry.DataBind();
                btnSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EntranceExamMarkEntry.ExcelToDatabase() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int admBatch = 0;
            int programmeType = 0;
            int centre = 0;
            string marks = "";
            double mm=0;
            double negMarks = 0;
            string programmeCode = string.Empty;
            int ret = 0;
            int userno = 0;
            if (lvMarkEntry.Items.Count > 0)
            {
                DataRow dr = null;
                DataTable dtMarks = new DataTable("TBL_EXAM_MARK_ENTRY_EXCEL");
                dtMarks.Columns.Add("Applicant_ID", typeof(string));
                dtMarks.Columns.Add("Applicant_Name", typeof(string));
                dtMarks.Columns.Add("Marks", typeof(string));

                //if (!ddladmbatch.SelectedValue.Equals(string.Empty)) admBatch = Convert.ToInt32(ddladmbatch.SelectedValue);
                //if (!ddlprogrammetype.SelectedValue.Equals(string.Empty)) programmeType = Convert.ToInt32(ddlprogrammetype.SelectedValue);
                //if (!ddlCentre.SelectedValue.Equals(string.Empty)) centre = Convert.ToInt32(ddlCentre.SelectedValue);
                string activitynos = string.Empty;
            string activitynames = string.Empty;
            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    activitynos += items.Value + ',';
                    activitynames += items.Text + ',';
                }
            }

                string ipAddress = Request.ServerVariables["REMOTE_HOST"];
                foreach (ListViewDataItem dataRow in lvMarkEntry.Items)
                {
                    HiddenField hdnUser = dataRow.FindControl("hdnUser") as HiddenField;
                    Label lblApp = dataRow.FindControl("lblApp") as Label;
                    Label lblName = dataRow.FindControl("lblName") as Label;
                    Label lblCode = dataRow.FindControl("lblCode") as Label;
                    Label lblRollNo = dataRow.FindControl("lblRollNo") as Label;
                    Label lblDegree = dataRow.FindControl("lblDegree") as Label;
                    Label lblLongName = dataRow.FindControl("lblLongName") as Label;
                    Label lblMarks = dataRow.FindControl("lblMarks") as Label;
                    Label lblNegMarks = dataRow.FindControl("lblNegMarks") as Label;
                    //DataSet ds = objCommon.FillDropDown("ACD_USER_BRANCH_PREF", "USERNO", "", "APPLICATION_ID='" + lblApp.Text.TrimEnd() + "'", "");
                    DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "USERNO", "", "USERNAME='" + lblApp.Text.TrimEnd() + "'", "");

                    marks = lblMarks.Text.ToString();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        userno = Convert.ToInt32(ds.Tables[0].Rows[0]["USERNO"].ToString());
                    }
                    dr = dtMarks.NewRow();
                    if (String.IsNullOrEmpty(lblApp.Text.ToString()))
                    {
                        dr["Applicant_ID"] = null;
                    }
                    else
                    {
                        dr["Applicant_ID"] = lblApp.Text.ToString();
                    }
                    if (String.IsNullOrEmpty(lblName.Text.ToString()))
                    {
                        dr["Applicant_Name"] = null;
                    }
                    else
                    {
                        dr["Applicant_Name"] = lblName.Text.ToString();
                    }
                    
                    if (lblMarks.Text.ToString().Equals(string.Empty))
                    {
                        //marks = 0;
                        objCommon.DisplayMessage(this.Page, "Marks Should Not be Blank.", this.Page);
                        return;
                    }
                     if (double.TryParse(marks, out mm))
                     {
                        mm = Convert.ToDouble(lblMarks.Text.ToString());
                        if (mm <= 0)
                        {
                            objCommon.DisplayMessage(this.Page, "Marks Should be Greater than 0.", this.Page);
                            return;
                        }
                     }  
                     else
                     {
                         objCommon.DisplayMessage(this.Page, "Please Enter Numeric Value Only.", this.Page);
                         return;
                     }                   

                    dtMarks.Rows.Add(dr);

                    ret = ObjUpload.Add_Entrance_Mark_Entry_Excel_Data(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlProgramType.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),activitynos,
                         ViewState["FILENAME"].ToString(),
                        mm, userno, ipAddress, Convert.ToInt32(Session["userno"]), dr["Applicant_ID"].ToString());
                }
                if (ret > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Entrance Marks Data Updated Successfully.", this.Page);
                    //clear();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Something Went Wrong.", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EntranceExamMarkEntry.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }
    private void clearFields()
    {
      
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;  
        lstProgram.Items.Clear();
        lvMarkEntry.DataSource = null;
        lvMarkEntry.DataBind();
        btnSubmit.Visible = false;
        divExamMarkEntryExcelData.Visible = false;
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Branch/Program.", this.Page);
                return;
            }
          

            GridView GV = new GridView();
            string ContentType = string.Empty;

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = ObjUpload.ExcelMarkUpload(ADMBATCH, ProgramType, DegreeNo, branchno);


            if (ds.Tables.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=StudentMarks.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");



        }
        Response.End();
    }
  
    protected void lvMarkEntry_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
        Label Marks;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Marks = (Label)e.Item.FindControl("lblMarks");  

            System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
            string currentMarks = rowView["Marks"].ToString();

            double mm = 0;
            if (double.TryParse(Marks.Text, out mm))
            {
                mm = Convert.ToDouble(currentMarks.ToString());
                if (mm <= 0)
                {
                    Marks.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    Marks.BackColor = System.Drawing.Color.White;
                }
                //else 
                //{
                //    foreach (var item in specialChar)
                //    {
                //        if (Marks.Text.Contains(item))
                //        {
                //            Marks.BackColor = System.Drawing.Color.Red;
                //        }
                     
                //    }
                   
                //}
            }
            else
            {             
                Marks.BackColor = System.Drawing.Color.Red;
            }

            //if (currentEmailAddress == "orlando0@adventure-works.com")
            //{
            //    Marks.Font.Bold = true;
            //}
        }
    }
    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updSchedule, "Please Select Branch/Program.", this.Page);
                return;
            }


            GridView GV = new GridView();
            string ContentType = string.Empty;

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = ObjUpload.GetMarkUpload(ADMBATCH, ProgramType, DegreeNo, branchno);


            if (ds.Tables.Count > 0)
            {
                btnSubmit.Visible = false;
                btnLockMark.Visible = true;
                divExamMarkEntryExcelData.Visible = true;
                lvMarkEntry.DataSource = ds;
                lvMarkEntry.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                btnLockMark.Visible = false;
                divExamMarkEntryExcelData.Visible = false;
                objUCommon.ShowError(Page, "No Recored Found.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");



        }
    }
    protected void btnLockMark_Click(object sender, EventArgs e)
    {

        try
        {
            int ret = 0;
            string ApplicationId = "";
            if (lvMarkEntry.Items.Count > 0)
            {                
                foreach (ListViewDataItem dataRow in lvMarkEntry.Items)
                {

                    Label lblApp = dataRow.FindControl("lblApp") as Label;
                    ApplicationId = lblApp.Text;

                    ret = ObjUpload.ExamMarkLock(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), ApplicationId.ToString());                      
                }
                if (ret > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Entrance Marks Lock Successfully.", this.Page);
                    //clear();
                    lstProgram_SelectedIndexChanged(sender,e);
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Something Went Wrong.", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EntranceExamMarkEntry.btnLockMark_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
}
