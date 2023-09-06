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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_UploadOtherFees : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Student objSC = new Student();
    StudentController objStud = new StudentController();

    #region Page Event

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();

                }
            }

            //Blank Div
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
                Response.Redirect("~/notauthorized.aspx?page=UploadOtherFees.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UploadOtherFees.aspx");
        }
    }

    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN DEGREE
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "ISNULL(ACTIVESTATUS,0)=1 AND COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SEMESTERNO");
            objCommon.FillDropDownList(ddlrecieptcode, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE = 'OF'", "RCPTTYPENO");
            objCommon.FillDropDownList(ddlFeehead, "ACD_FEE_TITLE", "FEE_HEAD", "FEE_LONGNAME", "RECIEPT_CODE = 'OF' AND  FEE_SHORTNAME<>''", "");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        string path = Server.MapPath("~/ExcelData/UploadOFBlankFormat.xlsx");
        Response.AddHeader("Content-Disposition", "attachment;filename=\"UploadOFBlankFormat.xlsx\"");
        Response.TransmitFile(path);
        Response.Flush();
        Response.End();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        ddlCollege.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlrecieptcode.SelectedIndex = 0;
        ddlFeehead.SelectedIndex = 0;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuUploadexcel.HasFile)
            {
                string FileName = Path.GetFileName(fuUploadexcel.PostedFile.FileName);
                string Extension = Path.GetExtension(fuUploadexcel.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    // string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string path = MapPath("~/ExcelData/");
                    string FilePath = path + FileName;
                    fuUploadexcel.SaveAs(FilePath);
                    ExcelToDatabase(FilePath, Extension, "yes");
                }
                else
                {
                    objCommon.DisplayMessage(updotherfees, "Only .xls or .xlsx extention is allowed", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updotherfees, "Please select the Excel File to Upload", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updotherfees, "Cannot access the file. Please try again.", this.Page);
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
            //dv1.RowFilter = "isnull(REGISTRATIONNO,0)<>0";
            //dv1.RowFilter = "isnull(RegistrationNo,'')<>''";
            DataTable dtNew = dv1.ToTable();

            lvStudent.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            lvStudent.DataBind();
            lvStudent.Visible = true;
            int i = 0;
            double amount = 0;
            for (i = 0; i < dtNew.Rows.Count; i++)
            {
                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Regno = row[0];
                if (Regno != null && !String.IsNullOrEmpty(Regno.ToString().Trim()))
                {
                    if (!(dtNew.Rows[i]["RegistrationNo"]).ToString().Equals(string.Empty))
                    {
                        //objSC.SemesterNo = Convert.ToInt32(dtNew.Rows[i]["SEMESTER"]);
                        objSC.RegNo = Convert.ToString(dtNew.Rows[i]["RegistrationNo"]);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updotherfees, "Please enter RegistrationNo at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    if (!(dtNew.Rows[i]["Amount"]).ToString().Equals(string.Empty))
                    {
                        //objSC.SemesterNo = Convert.ToInt32(dtNew.Rows[i]["SEMESTER"]);
                        amount = Convert.ToDouble(dtNew.Rows[i]["Amount"]);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updotherfees, "Please enter Amount at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    cs = (CustomStatus)objStud.InsertOtherFeeDemand(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), ddlrecieptcode.SelectedValue, ddlFeehead.SelectedValue, amount, Convert.ToInt32(Session["userno"]), objSC);
                }
                else
                {
                    objCommon.DisplayMessage(updotherfees, "Please enter RegistrationNo at Row no." + (i + 1), this.Page);
                    return;
                }
            }
            if (cs.Equals(CustomStatus.RecordSaved) || (cs.Equals(CustomStatus.DuplicateRecord)))
            {
                // BindListView();
                objCommon.DisplayMessage(updotherfees, "Excel Sheet Imported Successfully!!", this.Page);
                lvStudent.Visible = false;
            }
            else
            {
                //BindListView();
                objCommon.DisplayMessage(updotherfees, "Error in Importing Excel Sheet. Please Check Excel data is in proper format or not !!", this.Page);
                lvStudent.Visible = false;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updotherfees, "Data not available in ERP Master", this.Page);
                return;
            }
            else
                throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}