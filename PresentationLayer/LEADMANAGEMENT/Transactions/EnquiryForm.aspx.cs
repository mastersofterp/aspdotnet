using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
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
using ClosedXML.Excel;

public partial class LEADMANAGEMENT_Transactions_EnquiryForm : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {       //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string eventTarget = Page.Request.Params.Get("__EVENTTARGET");
        if (eventTarget == "txtDateOfBirth")
        {
            txtDateOfBirth.Text = "";
        }
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
                Response.Redirect("~/notauthorized.aspx?page=QualifyMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QualifyMaster.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {

            objCommon.FillDropDownList(ddlprogramt, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
            objCommon.FillDropDownList(ddlsource, "ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME", "SOURCETYPENO>0 AND ISNULL(SOURCETYPESTATUS,0)=1", "SOURCETYPENAME");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "MAX(BATCHNO)BATCHNO", "BATCHNAME", "BATCHNO > 0 GROUP BY BATCHNAME", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlAdmBatchtab1, "ACD_ADMBATCH", "MAX(BATCHNO)BATCHNO", "BATCHNAME", "BATCHNO > 0 GROUP BY BATCHNAME", "BATCHNO DESC");
            ddlAdmBatch.SelectedIndex = 1;
            ddlAdmBatch.Enabled = false;
            ddlAdmBatchtab1.SelectedIndex = 1;
            ddlAdmBatchtab1.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryForm.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void clearcontrols()
    {
        txtfName.Text = string.Empty;
        txtlName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtMobile.Text = string.Empty;
        rblgender.ClearSelection();
        txtaddress.Text = string.Empty;
        ddlprogramt.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlsource.SelectedIndex = 0;
        chkConfirm.Checked = false;
    }

    //    protected void btnSubmit_Click(object sender, EventArgs e)
    //    {
    //        LMController objLMC = new LMController();
    //        LeadManage objLM = new LeadManage();
    //        try
    //        {
    //            if (!txtStudFirstName.Text.Trim().Equals(string.Empty)) objLM.StudFirstName = txtStudFirstName.Text.Trim();
    //            if (!txtStudMiddleName.Text.Trim().Equals(string.Empty)) objLM.StudMiddleName = txtStudMiddleName.Text.Trim();
    //            if (!txtStudLastName.Text.Trim().Equals(string.Empty)) objLM.StudLastName = txtStudLastName.Text.Trim();
    //            if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objLM.DOB = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
    //            objLM.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
    //            objLM.StudMobile=txtStudMobile.Text;
    //            objLM.StudEmail = txtStudEmail.Text;
    //            objLM.Gender = (rdobtn_Gender.SelectedValue).ToString();
    //            objLM.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
    //            objLM.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
    //            if (!txtAddress.Text.Trim().Equals(string.Empty)) objLM.Address = txtAddress.Text.Trim();
    //            objLM.CityNo = Convert.ToInt32(ddlCity.SelectedValue);
    //            objLM.StateNo = Convert.ToInt32(ddlState.SelectedValue);
    //            //objLM.DistrictNo =0;
    //            if (!txtPIN.Text.Trim().Equals(string.Empty)) objLM.PIN = txtPIN.Text.Trim();
    //            if (!txtParentName.Text.Trim().Equals(string.Empty)) objLM.ParentName = txtParentName.Text.Trim();
    //            if (!txtParentMobile.Text.Trim().Equals(string.Empty)) objLM.ParentMobile = txtParentMobile.Text.Trim();
    //            if (!txtParentEmail.Text.Trim().Equals(string.Empty)) objLM.ParentEmail = txtParentEmail.Text.Trim();
    //            objLM.SourceNo = Convert.ToInt32(ddlHearAboutUs.SelectedValue);

    //            CustomStatus cs = (CustomStatus)objLMC.AddStudentEnquiryInformation(objLM);
    //            if (cs.Equals(CustomStatus.RecordSaved))
    //                {
    //                    objCommon.DisplayMessage(this.updStudent, "Student Information Submitted Successfully", this.Page);
    //                    clearcontrols();
    //                    return;
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.updStudent, "Record already exist", this.Page);
    //                    return;
    //                    //Label1.Text = "Record already exist";
    //                }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryForm.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //            //this.ClearControl();
    //        }
    //    }



    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrols();
    }
    protected void ddlprogramt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlprogramt.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.UGPGOT=" + ddlprogramt.SelectedValue, "D.DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            ddlprogramt.SelectedIndex = 0;
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.SelectedIndex = 0;
            objCommon.DisplayMessage("Please select Degree!", this.Page);
            return;
        }

        ddlBranch.SelectedIndex = 0;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        LMController objLMC = new LMController();
        LeadManage objLM = new LeadManage();
        int confirm = 0;
        try
        {
            if (!txtfName.Text.Trim().Equals(string.Empty)) objLM.StudFirstName = txtfName.Text.Trim();
            if (!txtlName.Text.Trim().Equals(string.Empty)) objLM.StudLastName = txtlName.Text.Trim();
            if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objLM.DOB = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
            objLM.StudMobile = txtMobile.Text;
            objLM.StudEmail = txtEmail.Text;
            objLM.Gender = (rblgender.SelectedValue).ToString();
            objLM.Address = txtaddress.Text;
            objLM.UA_SECTION = Convert.ToInt32(ddlprogramt.SelectedValue);
            objLM.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objLM.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objLM.SourceNo = Convert.ToInt32(ddlsource.SelectedValue);
            objLM.OrganizationId = Convert.ToInt32(Session["OrgId"]);
            objLM.BatchNo = Convert.ToInt32(ddlAdmBatchtab1.SelectedValue);
            if (chkConfirm.Checked == true)
            {
                confirm = chkConfirm.Checked ? 1 : 0;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please select to confirm that the above information is correct... ", this.Page);
            }
            GenerateRandomPass();
            CustomStatus cs = (CustomStatus)objLMC.AddStudentEnquiryInformation(objLM, confirm, ViewState["tempPass"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Student Information Submitted Successfully", this.Page);
                clearcontrols();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                return;
                //Label1.Text = "Record already exist";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryForm.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        LMController objLMC = new LMController();
        DataSet ds = objLMC.RetrieveMasterDataForTimeTableExcel();
        ds.Tables[0].TableName = "Enquiry Excel Import";  
        ds.Tables[1].TableName = "Source Type Master";
        ds.Tables[2].TableName = "Program Type Master";
        ds.Tables[3].TableName = "Degree Type Master";
        ds.Tables[4].TableName = "Branch Type Master";
     
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
            Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_NewEnquiryForm.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please select the Excel File to Upload", this.Page);
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

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        LMController objLMC = new LMController();
        LeadManage objLM = new LeadManage();
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
            string SheetName = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
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
            dv1.RowFilter = "isnull(FirstName,'')<>''";
            DataTable dtNew = dv1.ToTable();

            lvEnquiry.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            lvEnquiry.DataBind();
            int i = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //foreach (DataRow dr in dt.Rows)
            {

                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Name = row[0];
                if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
                {
                    //string city = string.Empty;
                    //string district = string.Empty;
                    //string state = string.Empty;
                    //string leadSource = string.Empty;
                    //string leadCollectedby = string.Empty;


                    if (!(dtNew.Rows[i]["FirstName"]).ToString().Equals(string.Empty)) objLM.StudName = dtNew.Rows[i]["FirstName"].ToString();
                    //else
                    //    objLM.StudName = string.Empty;
                    if (!(dtNew.Rows[i]["LastName"]).ToString().Equals(string.Empty)) objLM.StudLastName = dtNew.Rows[i]["LastName"].ToString();


                    if (dtNew.Rows[i]["Mobile"].ToString() == string.Empty)
                    {
                        objLM.studentMobile = string.Empty;
                    }
                    else
                    {
                        objLM.studentMobile = dtNew.Rows[i]["Mobile"].ToString();
                    }
                    
                    if (dtNew.Rows[i]["Email"].ToString() == string.Empty)
                    {
                        objLM.EmailId = string.Empty;
                    }
                    else
                    {
                        objLM.EmailId = ds.Tables[0].Rows[i]["Email"].ToString();
                    }

                    if (!(dtNew.Rows[i]["Gender"]).ToString().Equals(string.Empty)) objLM.Gender = null;
                    if (dtNew.Rows[i]["Gender"].ToString().Equals("Female") || dtNew.Rows[i]["Gender"].ToString().Equals("female") || dtNew.Rows[i]["Gender"].ToString().Equals("FEMALE")) objLM.Gender = "Female";
                    if (dtNew.Rows[i]["Gender"].ToString().Equals("Male") || dtNew.Rows[i]["Gender"].ToString().Equals("male") || dtNew.Rows[i]["Gender"].ToString().Equals("MALE")) objLM.Gender = "Male";

                    if (!(dtNew.Rows[i]["DateofBirth"]).ToString().Equals(string.Empty)) objLM.DOB = Convert.ToDateTime(dtNew.Rows[i]["DateofBirth"]);

                    if (!(dtNew.Rows[i]["Address"]).ToString().Equals(string.Empty)) objLM.Address = dtNew.Rows[i]["Address"].ToString();


                    if (dtNew.Rows[i]["SourceType"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Source Type at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }
                    else
                    {
                        string SOURCETYPENO = objCommon.LookUp("ACD_SOURCETYPE", "COUNT(1)", "SOURCETYPENAME='" + dtNew.Rows[i]["SourceType"].ToString() + "'");

                        if (Convert.ToInt32(SOURCETYPENO) > 0)
                        {
                            objLM.SOURCETYPENAME = Convert.ToString((objCommon.LookUp("ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME ='" + dtNew.Rows[i]["SourceType"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME ='" + dtNew.Rows[i]["SourceType"].ToString() + "'")));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Source Type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }

                    if (dtNew.Rows[i]["ProgramType"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Program Type at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string programtype = objCommon.LookUp("ACD_UA_SECTION", "COUNT(1)", "UA_SECTIONNAME='" + dtNew.Rows[i]["ProgramType"].ToString() + "'");

                        if (Convert.ToInt32(programtype) > 0)
                        {
                            objLM.UA_SECTION = Convert.ToInt32((objCommon.LookUp("ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME ='" + dtNew.Rows[i]["ProgramType"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME ='" + dtNew.Rows[i]["ProgramType"].ToString() + "'")));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Program Type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                    }
                    if (dtNew.Rows[i]["Degree"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Degree at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string DEGREENO = objCommon.LookUp("ACD_DEGREE", "COUNT(1)", "DEGREENAME='" + dtNew.Rows[i]["Degree"].ToString() + "'");

                        if (Convert.ToInt32(DEGREENO) > 0)
                        {
                            objLM.DegreeNo = Convert.ToInt32((objCommon.LookUp("ACD_DEGREE", "DEGREENO", "DEGREENAME ='" + dtNew.Rows[i]["Degree"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREENO", "DEGREENAME ='" + dtNew.Rows[i]["Degree"].ToString() + "'")));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Degree not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }
                    if (dtNew.Rows[i]["Branch"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Enter Branch at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }
                    else
                    {
                        string BRANCHNO = objCommon.LookUp("ACD_BRANCH", "COUNT(1)", "LONGNAME='" + dtNew.Rows[i]["Branch"].ToString() + "'");

                        if (Convert.ToInt32(BRANCHNO) > 0)
                        {
                            objLM.BranchNo = Convert.ToInt32((objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME ='" + dtNew.Rows[i]["Branch"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME ='" + dtNew.Rows[i]["Branch"].ToString() + "'")));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnl, "Branch not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }                
                    objLM.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                    cs = (CustomStatus)objLMC.SaveExcelSheetDataInDataBase(objLM);
                    connExcel.Close();
                }

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                // BindListView();
                objCommon.DisplayMessage(updpnl, "Excel Sheet Uploaded Successfully!!", this.Page);
            }
            else
            {
                //BindListView();
                objCommon.DisplayMessage(updpnl, "Excel Sheet Updated Successfully!!", this.Page);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnl, "Data not available in ERP Master", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void GenerateRandomPass()
    {
        try
        {
            Random res = new Random();

            // String that contain both alphabets and numbers
            String str = "abcdefghijklmnopqrstuvwxyz0123456789";
            int size = 5;

            // Initializing the empty string
            String randomstring = "";

            for (int i = 0; i < size; i++)
            {

                // Selecting a index randomly
                int x = res.Next(str.Length);

                // Appending the character at the 
                // index to the random alphanumeric string.
                randomstring = randomstring + str[x];
            }
            ViewState["tempPass"] = randomstring;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
}