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

public partial class LEADMANAGEMENT_Transactions_EnquiryGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LMController objLMC = new LMController();
    LeadManage objLM = new LeadManage();

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
            dv1.RowFilter = "isnull(STUD_FIRSTNAME,'')<>''";
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
                        string city = string.Empty;
                        string district = string.Empty;
                        string state = string.Empty;
                        string leadSource = string.Empty;
                        string leadCollectedby = string.Empty;


                        if (!(dtNew.Rows[i]["STUD_FIRSTNAME"]).ToString().Equals(string.Empty)) objLM.StudName = dtNew.Rows[i]["STUD_FIRSTNAME"].ToString();
                        //else
                        //    objLM.StudName = string.Empty;


                        if (dtNew.Rows[i]["STUD_MOBILE"].ToString() == string.Empty)
                        {
                            objLM.studentMobile = string.Empty;
                        }
                        else
                        {
                            objLM.studentMobile = dtNew.Rows[i]["STUD_MOBILE"].ToString();
                        }

                        city = (dtNew.Rows[i]["CITY"]).ToString();
                        if (dtNew.Rows[i]["CITY"].ToString() == string.Empty)
                        {
                            objLM.CityNo = 0;
                        }
                        else
                        {
                            objLM.CityNo = (objCommon.LookUp("ACD_CITY", "CITYNO", "CITY ='" + city + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_CITY", "CITYNO", "CITY ='" + city + "'"));
                        }
                        district = (dtNew.Rows[i]["DISTRICT"]).ToString();
                        if (dtNew.Rows[i]["DISTRICT"].ToString() == string.Empty)
                        {
                            objLM.DistrictNo = 0;
                        }
                        else
                        {
                            objLM.DistrictNo = (objCommon.LookUp("ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME='" + district + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME ='" + district + "'"));//Convert.ToInt32((objCommon.LookUp("ACD_CITY", "CITYNO", "CITY ='" + city + "'")));
                        }
                        //if (!(ds.Tables[0].Rows[i]["DISTRICT"].ToString().Equals(string.Empty))) objLM.DistrictNo = Convert.ToInt32((objCommon.LookUp("ACD_CITY", "CITYNO", "CITY ='" + district + "'")));
                        //else
                        //    objLM.DistrictNo = 0;

                        state = (dtNew.Rows[i]["STATE"]).ToString();
                        if (dtNew.Rows[i]["STATE"].ToString() == string.Empty)
                        {
                            objLM.StateNo = 0;
                        }
                        else
                        {
                            objLM.StateNo = (objCommon.LookUp("ACD_STATE", "STATENO", "STATENAME ='" + state + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STATE", "STATENO", "STATENAME ='" + state + "'"));  //Convert.ToInt32((objCommon.LookUp("ACD_STATE", "STATENO", "STATENAME ='" + state + "'")));
                        }

                        if (dtNew.Rows[i]["STUD_EMAILID"].ToString() == string.Empty)
                        {
                            objLM.EmailId = string.Empty;
                        }
                        else
                        {
                            objLM.EmailId = ds.Tables[0].Rows[i]["STUD_EMAILID"].ToString();
                        }
                       
                        if (!(dtNew.Rows[i]["GENDER"]).ToString().Equals(string.Empty)) objLM.Gender = null;
                        if (dtNew.Rows[i]["GENDER"].ToString().Equals("Female") || dtNew.Rows[i]["GENDER"].ToString().Equals("female") || dtNew.Rows[i]["GENDER"].ToString().Equals("FEMALE")) objLM.Gender = "F";
                        if (dtNew.Rows[i]["GENDER"].ToString().Equals("Male") || dtNew.Rows[i]["GENDER"].ToString().Equals("male") || dtNew.Rows[i]["GENDER"].ToString().Equals("MALE")) objLM.Gender = "M";

                        if (!(dtNew.Rows[i]["DOB"]).ToString().Equals(string.Empty)) objLM.DOB = Convert.ToDateTime(dtNew.Rows[i]["DOB"]);

                        if (dtNew.Rows[i]["PARENT_MOBILE"].ToString() == string.Empty)
                        {
                            objLM.ParentsMobile = string.Empty;
                        }
                        else
                        {
                            objLM.ParentsMobile = dtNew.Rows[i]["PARENT_MOBILE"].ToString();
                        }
                        if (dtNew.Rows[i]["SCHOOL_NAME"].ToString() == string.Empty)
                        {
                            objLM.School_Name = string.Empty;
                        }
                        else
                        {
                            objLM.School_Name = dtNew.Rows[i]["SCHOOL_NAME"].ToString();
                        }

                        if (dtNew.Rows[i]["ADDRESS"].ToString() == string.Empty)
                        {
                            objLM.Address = string.Empty;
                        }
                        else
                        {
                            objLM.Address = dtNew.Rows[i]["ADDRESS"].ToString();
                        }

                        if (dtNew.Rows[i]["PIN"].ToString() == string.Empty)
                        {
                            objLM.PIN = string.Empty;
                        }
                        else
                        {
                            objLM.PIN = dtNew.Rows[i]["PIN"].ToString();
                        }
                        objLM.Lead_Collected_by = (dtNew.Rows[i]["LEAD_COLLECTED_BY"]).ToString();
                        //if (ds.Tables[0].Rows[i]["LEAD_COLLECTED_BY"].ToString() == string.Empty)
                        //{
                        //    objLM.Lead_Collected_by = 0;
                        //}
                        //else
                        //{
                        //    objLM.Lead_Collected_by = Convert.ToInt32((objCommon.LookUp("User_Acc", "UA_NO", "UA_FULLNAME='" + leadCollectedby + "'")));
                        //}

                        leadSource = (dtNew.Rows[i]["LEAD_SOURCE"]).ToString();
                        if (dtNew.Rows[i]["LEAD_SOURCE"].ToString() == string.Empty)
                        {
                            objLM.Lead_Source_No = 0;
                        }
                        else
                        {
                            objLM.Lead_Source_No = (objCommon.LookUp("ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME ='" + leadSource + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME ='" + leadSource + "'")); //Convert.ToInt32((objCommon.LookUp("ACD_SOURCETYPE", "SOURCETYPENO", "SOURCETYPENAME ='" + leadSource + "'")));
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

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
  
    }
    private void ClearControls()
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnExport_Click1(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/LEADMANAGEMENT/Transactions/Excel_EnquiryGeneration/PreFormat_For_EnquiryGeneration.xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"PreFormat_For_EnquiryGeneration.xls\"");
        Response.TransmitFile(path);
        Response.Flush();
        Response.End();
    }

    protected void btnUpload_Click2(object sender, EventArgs e)
    {
        try
        {
            Uploaddata();

            ////string path = Server.MapPath("~\\LEADMANAGEMENT\\Transactions\\Excel_EnquiryGeneration\\");

            ////if (FileUpload1.HasFile)
            ////{
            ////    string filename = FileUpload1.FileName.ToString();
            ////    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            ////    string[] array1 = Directory.GetFiles(path);
            ////    foreach (string str in array1)
            ////    {
            ////        if ((path + filename).Equals(str))
            ////        {
            ////            objCommon.DisplayMessage(updpnl, "File with similar name already exists!", this);
            ////            return;
            ////        }
            ////    }
            ////    //if (extension.Contains(".xls") || extension.Contains(".xlsx"))
            ////    if (extension.Equals(".xls"))
            ////    {
            ////        //if (!(Directory.Exists(MapPath("~/PresentationLayer/LEADMANAGEMENT/Transactions/Excel_EnquiryGeneration"))))
            ////        //    Directory.CreateDirectory(path);
            ////        //string fileName = FileUpload1.PostedFile.FileName.Trim();

            ////        //if (File.Exists((path + fileName).ToString()))
            ////        //    File.Delete((path + fileName).ToString());
            ////        //FileUpload1.SaveAs(Server.MapPath("Excel_EnquiryGeneration//" + FileUpload1.FileName));
            ////        ////GetExcelSheetNames(Server.MapPath("Excel_Scholarship//" + FileUpload1.FileName));
            ////        //string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            ////        //string FilePath = Server.MapPath("~\\LEADMANAGEMENT\\Transactions\\Excel_EnquiryGeneration\\" + FileUpload1.FileName);

            ////        //ExcelToDatabase(FilePath, Extension, "yes");

            ////        //if (File.Exists(path + fileName))
            ////        //{
            ////        //    File.Delete(path + fileName);

            ////        //}
            ////    }
            ////    else
            ////    {
            ////        objCommon.DisplayMessage(updpnl, "Only .xls extention is allowed", this.Page);
            ////        return;
            ////    }
            ////}
            ////else
            ////{
            ////    objCommon.DisplayMessage(updpnl, "Please select the Excel File to Upload", this.Page);
            ////    return;
            ////}
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryGeneration.Uploaddata()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedIndex > 0)
            {
                GridView GV = new GridView();

                DataSet ds = objLMC.GetAllEnquiryData(Convert.ToInt32(ddlAdmBatch.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string attachment = "attachment; filename=ENQUIRY-" + ddlAdmBatch.SelectedItem.Text + ".xls";
                    //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayUserMessage(updpnl, "No Record Found.", this.Page);
                }
            }
            else
                objCommon.DisplayUserMessage(updpnl, "Please Select Admission Batch.", this.Page);

        }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    
        
    }
}