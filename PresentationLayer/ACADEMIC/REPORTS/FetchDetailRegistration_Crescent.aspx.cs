//======================================================================================
// PROJECT NAME  : RFC(ONLINE ADMISSION)  
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Fetch Detail Registration
// CREATION DATE : 25-JAN-2023
// CREATED BY    : NEHAL NAWKHARE
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using ClosedXML.Excel;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

public partial class FetchDetailRegistration_Crescent : System.Web.UI.Page
{
    Common objCommon = new Common();
    FetchDataController objFetchData = new FetchDataController();
    User objus = new User();
    string personal = string.Empty;
    string address = string.Empty;
    string education = string.Empty;
    string photo = string.Empty;
    string pay = string.Empty;
    string confirm = string.Empty;
    string chkSpecial = string.Empty;
    int branch = 0;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString_CRESCENT"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_CRESCENT"].ToString();
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
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO<>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO<>0", "LONGNAME");
                FillAdmBatchDropDown();
                objCommon.FillDropDownList(ddlPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUB_ID=1 AND DEGREENO=7", "SUB_NAME");
                //objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH  A INNER JOIN ACD_PHD_REGISTRATION PR ON(A.BATCHNO=PR.ADMBATCH)", "DISTINCT BATCHNO", "BATCHNAME", "", "BATCHNAME DESC");
                ddlAdmbatch.SelectedIndex = 1;
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FetchDetailRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FetchDetailRegistration.aspx");
        }
    }
    protected void btnShowStud_Click(object sender, EventArgs e)
    {
        try
        {
            int programmeType = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
            int degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;

            DataSet ds = objFetchData.GetApplicantUserListNew(Convert.ToInt32(ddlAdmbatch.SelectedValue), programmeType, Convert.ToInt32(rdoStatus.SelectedValue), degree);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                //pnlStudent.Visible = true;
                lvStudent.Visible = true;
            }
            else
            {
                //lvStudent.DataSource = null;
                //lvStudent.DataBind();
                //pnlStudent.Visible = false;
                objCommon.DisplayMessage(this.Page, "No Student Found", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnShowStud_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudent.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCompleteDetail_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                int programmeType = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
                int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
                DataSet ds = objFetchData.GetApplicantsCompleteDetails(Convert.ToInt32(ddlAdmbatch.SelectedValue), programmeType, Convert.ToInt32(rdoStatus.SelectedValue), degreeNo);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Remove("User no.");
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);
                    return;
                }
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
                    Response.AddHeader("content-disposition", "attachment;filename=Student_Complete_Details.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "create_user.btnExcel_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnCompleteDetail_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdmissionCount_Click(object sender, EventArgs e)
    {
        int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int Application_Status = Convert.ToInt32(rdoStatus.SelectedValue);
        int branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        DataSet ds = objFetchData.GetOnlineStudentsCountForExcelAdmissionCount(admbatch, degreeNo, Application_Status, Convert.ToInt32(ddlProgrammeType.SelectedValue), branchno);
        GridView gv = new GridView();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            string Attachment = "Attachment;filename=OnlineAdmissionStudentsCount.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string coldegreebranch = string.Empty;
        string fromDate = string.Empty;
        string toDate = string.Empty;
        int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int Application_Status = Convert.ToInt32(rdoStatus.SelectedValue);
        int branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        DataSet ds = objFetchData.GetStudentDatanew(admbatch, degreeNo, Application_Status, Convert.ToInt32(ddlProgrammeType.SelectedValue), branchno);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GridView gvStudData = new GridView();
            gvStudData.DataSource = ds;
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=Applied_Students.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);

            gvStudData.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updCollege1, "No Record Found", this);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            return;

        }
    }
    protected void btnFormFillExcel_Click(object sender, EventArgs e)
    {
        int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        int Application_Status = Convert.ToInt32(rdoStatus.SelectedValue);
        int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        DataSet ds = objFetchData.GetFormFillingStatus(admbatch, Application_Status, degreeNo, branchno);
        GridView gv = new GridView();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            string Attachment = "Attachment;filename=FormFillingStatus.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            return;
        }
    }
    protected void btnStudDetails_Click(object sender, EventArgs e)
    {
        DataSet dsStudData = null;
        int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        int Progtype = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
        //if (ddlProgrammeType.SelectedValue == "2")
        //{
        //    dsStudData = objFetchData.GetStudentsDumpData_PG(Convert.ToInt32(ddlDegree.SelectedValue), admbatch, Convert.ToInt32(ddlBranch.SelectedValue));
        //}
        //else
        {

            dsStudData = objFetchData.GetStudentCompleteDetailsNew(Convert.ToInt32(ddlDegree.SelectedValue), admbatch, Progtype, Convert.ToInt32(rdoStatus.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
        }
        GridView gv = new GridView();
        if (dsStudData.Tables.Count > 0 && dsStudData.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = dsStudData;
            gv.DataBind();
            string Attachment = "Attachment;filename=Student_Complete_Data.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            return;
        }
    }
    protected void btnExcel2_Click(object sender, EventArgs e)
    {

    }
    protected void btnExcel3_Click(object sender, EventArgs e)
    {

    }
    protected void btnExcelForeignStud_Click(object sender, EventArgs e)
    {
        GridView GV = new GridView();
        DataSet ds = objFetchData.GetForeignStudentData(Convert.ToInt32(ddlAdmbatch.SelectedValue));
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["USERNO"]);
            GV.DataSource = ds;
            GV.DataBind();
            string Attachment = "Attachment;filename=Foreign_Student_Registration_List.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.updCollege1, "No data found.", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            return;
        }
    }
    protected void btnPaymentDetails_Click(object sender, EventArgs e)
    {
        int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        DataSet ds = objFetchData.GetOnlinePaymentStudentDetailsForProvisonalAdm(admbatch, degreeNo, Convert.ToInt32(ddlProgrammeType.SelectedValue));
        GridView GV = new GridView();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
            string Attachment = "Attachment;filename=OnlinePaymentStudentDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
        }
    }
    protected void btnConfirmStudent_Click(object sender, EventArgs e)
    {

        try
        {
            int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            int programmType = ddlProgrammeType.SelectedIndex > 0 ? ddlProgrammeType.SelectedIndex : 0;
            int Application_Status = Convert.ToInt32(rdoStatus.SelectedValue);
            DataSet ds = objFetchData.GetConfirmStudentsDetails(admbatch, degreeNo, programmType, Application_Status);
            GridView GV = new GridView();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment;filename=AdmissionConfirmationDetails.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                //string a = sw.ToString().Replace("_", " ");
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnConfirmStudent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnDocumentList_Click(object sender, EventArgs e)
    {
        try
        {
            int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            int programmType = ddlProgrammeType.SelectedIndex > 0 ? ddlProgrammeType.SelectedIndex : 0;
            int Application_Status = Convert.ToInt32(rdoStatus.SelectedValue);
            int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            DataSet ds = objFetchData.GetDocumentListStatus(admbatch, programmType, Application_Status, degreeNo);
            GridView GV = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment;filename=DocumentListStatus.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnDocumentList_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPhd_Click(object sender, EventArgs e)
    {
        try
        {
            int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            DataSet ds = objFetchData.GetPhdCompleteStudentData(admbatch);
            GridView GV = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment;filename=PhdStudentData.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnPhd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSummaryReport_Click(object sender, EventArgs e)
    {
        try
        {
            int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            int ugpgot = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
            int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            DataSet ds = objFetchData.GetAdmissionSummaryReport(admbatch, ugpgot, degreeNo);
            GridView GV = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment;filename=Admission_Summary.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration.btnSummaryReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnMinumum_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgrammeType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Programme Type.", this.Page);
                return;
            }
            int admbatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            int ugpgot = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
            int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            DataSet ds = objFetchData.GetMinimumReportExcel(admbatch, ugpgot, degreeNo);
            GridView GV = new GridView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment;filename=MinimumFieldReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updCollege1, "No Data Found.", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(B.ACTIVESTATUS,0)=1", "B.LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch { }
    }
    protected void FillAdmBatchDropDown()
    {
        try
        {
            string sp_Name = string.Empty; string sp_Parameter = string.Empty; string sp_Call = string.Empty; int outP = 0;
            sp_Name = "PKG_BIND_ADMBATCH_DROPDOWN_APPLIES_STUDENT_LIST";
            sp_Parameter = "@P_OUT";
            sp_Call = "" + outP + "";
            DataSet ds = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameter, sp_Call);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAdmbatch.DataSource = ds;
                ddlAdmbatch.DataTextField = "BATCHNAME";
                ddlAdmbatch.DataValueField = "BATCH";
                ddlAdmbatch.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkUserId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkUserno = sender as LinkButton;
            DataSet dsPop = new DataSet();
            //--------------------------------Checking Degree type-----------------------------------------------------
            string SP_name = string.Empty; string SP_call = string.Empty; string SP_value = string.Empty;
            DataSet dsCheckType = null;
            SP_name = "PKG_ACD_OA_GET_USERS_DEGREE_TYPE";
            SP_call = "@P_USERNO";
            SP_value = "" + lnkUserno.CommandArgument + "";
            dsCheckType = objCommon.DynamicSPCall_Select(SP_name, SP_call, SP_value);
            if (dsCheckType.Tables[0].Rows.Count > 0)
            {
                ViewState["userno"] = dsCheckType.Tables[0].Rows[0]["USERNO"].ToString();
                //if (dsCheckType.Tables[0].Rows[0]["UGPGOT"].Equals("1") && dsCheckType.Tables[0].Rows[0]["DEGREENO"].Equals("7"))
                if (dsCheckType.Tables[0].Rows[0]["UGPGOT"].ToString().Equals("1") && dsCheckType.Tables[0].Rows[0]["DEGREENO"].ToString().Equals("7"))
                {
                    #region btech
                    dsPop = objFetchData.GetAllRegistrationDetails(lnkUserno.CommandArgument);
                    if (dsPop.Tables[0].Rows.Count > 0)
                    {
                        ViewState["userno"] = dsCheckType.Tables[0].Rows[0]["USERNO"].ToString();
                        if (ddlProgrammeType.SelectedValue == "1" && dsCheckType.Tables[0].Rows[0]["DEGREENO"].ToString() == "7")
                        {
                            Mp2ForOthers.Hide();
                            Mp1.Show();
                            objCommon.FillDropDownList(ddlPopReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO > 0 AND ACTIVESTATUS=1", "RELIGION");
                            objCommon.FillDropDownList(ddlPopCommunity, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORY");
                            objCommon.FillDropDownList(ddlPopTongue, "ACD_MTONGUE", "MTONGUENO", "MTONGUE", "MTONGUENO > 0 AND ACTIVESTATUS=1", "MTONGUE");
                            objCommon.FillDropDownList(ddlPopNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO=1 AND ACTIVESTATUS=1", "NATIONALITY");
                            //ViewState["userno"] = ViewState["userno"]
                            rdoPopGender.SelectedValue = dsPop.Tables[0].Rows[0]["GENDER_MAIN"].ToString().Trim();
                            txtPopName.Text = dsPop.Tables[0].Rows[0]["FIRSTNAME"].ToString().Trim();
                            txtPopDOB.Text = dsPop.Tables[0].Rows[0]["DOB_MAIN"].ToString().Trim();
                            ddlPopReligion.SelectedValue = dsPop.Tables[0].Rows[0]["RELIGIONNO"].ToString().Trim();
                            if (ddlPopReligion.SelectedValue == "11")
                            {
                                divPopReligion.Visible = true;
                                txtPopOtherReligion.Text = dsPop.Tables[0].Rows[0]["RELIGION_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                divPopReligion.Visible = false;
                            }
                            ddlPopCommunity.SelectedValue = dsPop.Tables[0].Rows[0]["COMMUNITY"].ToString().Trim();
                            ddlPopTongue.SelectedValue = dsPop.Tables[0].Rows[0]["MTONGUENO"].ToString().Trim();
                            if (ddlPopTongue.SelectedValue == "46")
                            {
                                divPopTongue.Visible = true;
                                txtPopTongue.Text = dsPop.Tables[0].Rows[0]["MOTHER_TONGUE_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                divPopTongue.Visible = false;
                            }
                            txtPopAadhar.Text = dsPop.Tables[0].Rows[0]["ADHAARNO"].ToString().Trim();
                            ddlPopNationality.SelectedValue = dsPop.Tables[0].Rows[0]["NATIONALITYNO"].ToString().Trim();
                            txtPopFather.Text = dsPop.Tables[0].Rows[0]["FATHERNAME"].ToString().Trim();

                            objCommon.FillDropDownList(ddlPopFOcc, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION >0 AND OCCUPATION NOT IN (9) AND ACTIVESTATUS=1", "OCCUPATION");
                            ddlPopFOcc.SelectedValue = dsPop.Tables[0].Rows[0]["FATHER_OCCUPATION"].ToString().Trim();
                            if (ddlPopFOcc.SelectedValue == "10")
                            {
                                divPopFocc.Visible = true;
                                txtPopFoccOther.Text = dsPop.Tables[0].Rows[0]["F_OCCUPATION_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                divPopFocc.Visible = false;
                            }
                            txtPopFmobile.Text = dsPop.Tables[0].Rows[0]["FATHER_MOBILE"].ToString().Trim();
                            txtPopMother.Text = dsPop.Tables[0].Rows[0]["MOTHERNAME"].ToString().Trim();
                            objCommon.FillDropDownList(ddlPopMOcc, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION >0 AND ACTIVESTATUS=1", "OCCUPATION");
                            ddlPopMOcc.SelectedValue = dsPop.Tables[0].Rows[0]["MOTHER_OCCUPATION"].ToString().Trim();
                            if (ddlPopMOcc.SelectedValue == "10")
                            {
                                divMOccupation.Visible = true;
                                txtPopMOccOther.Text = dsPop.Tables[0].Rows[0]["M_OCCUPATION_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                divMOccupation.Visible = false;
                            }
                            txtPopMMobile.Text = dsPop.Tables[0].Rows[0]["MOTHER_MOBILE"].ToString().Trim();

                            txtPopAdd1.Text = dsPop.Tables[0].Rows[0]["ADDRESS_1"].ToString().Trim();
                            txtPopAdd2.Text = dsPop.Tables[0].Rows[0]["ADDRESS_2"].ToString().Trim();
                            txtPopAdd3.Text = dsPop.Tables[0].Rows[0]["ADDRESS_3"].ToString().Trim();
                            txtPopCity.Text = dsPop.Tables[0].Rows[0]["CITY"].ToString().Trim();

                            objCommon.FillDropDownList(ddlPopState, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0 AND ACTIVESTATUS=1", "STATENAME");
                            ddlPopState.SelectedValue = dsPop.Tables[0].Rows[0]["STATE"].ToString().Trim();
                            objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO > 0 AND ACTIVESTATUS=1 AND STATENO=" + Convert.ToInt32(ddlPopState.SelectedValue), "DISTRICTNAME");
                            ddlDistrict.SelectedValue = dsPop.Tables[0].Rows[0]["DISTRICT"].ToString().Trim();
                            txtPopPin.Text = dsPop.Tables[0].Rows[0]["PINCODE"].ToString().Trim();
                            ViewState["pincode"] = txtPopPin.Text;
                            objCommon.FillDropDownList(ddlPopEdu, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO >0", "QUALILEVELNAME");
                            ddlPopEdu.SelectedValue = dsPop.Tables[0].Rows[0]["EDU_INFORMATION"].ToString().Trim() == "0" ? "0" : dsPop.Tables[0].Rows[0]["EDU_INFORMATION"].ToString().Trim();
                            if (ddlPopEdu.SelectedValue == "4")
                            {
                                divPopEdu.Visible = true;
                                txtPopEduOthers.Text = dsPop.Tables[0].Rows[0]["EDU_INFO_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                divPopEdu.Visible = false;
                            }
                            txtPopExmReg.Text = dsPop.Tables[0].Rows[0]["EXM_REG_12"].ToString().Trim();
                            txtPopSchool.Text = dsPop.Tables[0].Rows[0]["SCHOOL_NAME"].ToString().Trim();
                            objCommon.FillDropDownList(ddlPopMPass, "ACD_MONTH", "MONTHNO", "MONTH_NAME", "MONTHNO >0", "MONTHNO");
                            ddlPopMPass.SelectedValue = dsPop.Tables[0].Rows[0]["MONTH_PASS_NO"].ToString().Trim() == "0" ? "0" : dsPop.Tables[0].Rows[0]["MONTH_PASS_NO"].ToString().Trim();
                            objCommon.FillDropDownList(ddlPopYPass, "ACD_QUAL_YEAR", "YEARNO", "YEAR_NAME", "YEARNO >0", "YEAR_NAME DESC");
                            ddlPopYPass.SelectedValue = dsPop.Tables[0].Rows[0]["YEAR_PASS_NO"].ToString().Trim() == "0" ? "0" : dsPop.Tables[0].Rows[0]["YEAR_PASS_NO"].ToString().Trim();
                            rdoPopMedium.SelectedValue = dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim();
                            string medium = dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim();// = "" ? "0" : dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim();
                            if (!medium.ToString().Equals("0"))
                            {
                                if (rdoPopMedium.SelectedValue == "2")
                                {
                                    divPopMedium.Visible = true;
                                    txtPopMediumOther.Text = dsPop.Tables[0].Rows[0]["MEDIUM_OTHER"].ToString().Trim();
                                }
                                else
                                {
                                    divPopMedium.Visible = false;
                                }
                            }
                            objCommon.FillDropDownList(ddlPopLast, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO > 0 AND ACTIVESTATUS=1", "NATIONALITY");
                            ddlPopLast.SelectedValue = dsPop.Tables[0].Rows[0]["COUNTRY_ID"].ToString().Trim();
                            rdoPopAvailable.SelectedValue = dsPop.Tables[0].Rows[0]["AVAILABLE"].ToString().Trim();
                            //ddlSpec.SelectedValue = dsPop.Tables[0].Rows[0]["BRANCHNO"].ToString().Trim();
                            //objCommon.FillDropDownList(ddlPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUB_ID=1", "SUB_NAME");
                            objCommon.FillDropDownList(ddlPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUB_ID=1 AND DEGREENO=7", "SUB_NAME");
                            ddlPopSub1.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_1"].ToString().Trim();
                            txtPopMarksObt1.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_1"].ToString().Trim();
                            txtPopMaxMarks1.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_1"].ToString().Trim();
                            txtPopPer1.Text = dsPop.Tables[0].Rows[0]["PER_1"].ToString().Trim();

                            txtPopLang.Text = dsPop.Tables[0].Rows[0]["LANGUAGE"].ToString().Trim();
                            txtPopMarksObtLang.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_LANG"].ToString().Trim();
                            txtPopMaxMarksLang.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_LANG"].ToString().Trim();
                            txtPopPerLang.Text = dsPop.Tables[0].Rows[0]["PER_LANG"].ToString().Trim();

                            objCommon.FillDropDownList(ddlPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND IS_COMPULSORY=1 AND SUB_ID IN(2,5) AND DEGREENO=7", "SUB_NAME");

                            ddlPopSub2.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_2"].ToString().Trim();
                            txtPopMarksObt2.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_2"].ToString().Trim();
                            txtPopMaxMarks2.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_2"].ToString().Trim();
                            txtPopPer2.Text = dsPop.Tables[0].Rows[0]["PER_2"].ToString().Trim();

                            objCommon.FillDropDownList(ddlPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND DEGREENO=7 AND IS_COMPULSORY=1  AND SUB_ID IN(3) AND SUB_ID NOT IN(" + Convert.ToInt32(ddlPopSub2.SelectedValue) + ")", "SUB_NAME");
                            ddlPopSub3.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_3"].ToString().Trim();
                            txtPopMarksObt3.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_3"].ToString().Trim();
                            txtPopMaxMarks3.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_3"].ToString().Trim();
                            txtPopPer3.Text = dsPop.Tables[0].Rows[0]["PER_3"].ToString().Trim();
                            objCommon.FillDropDownList(ddlPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND DEGREENO=7 AND IS_COMPULSORY=1  AND SUB_ID IN(4) AND SUB_ID NOT IN(" + Convert.ToInt32(ddlPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlPopSub2.SelectedValue) + ")", "SUB_NAME");

                            ddlPopSub4.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_4"].ToString().Trim();
                            txtPopMarksObt4.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_4"].ToString().Trim();
                            txtPopMaxMarks4.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_4"].ToString().Trim();
                            txtPopPer4.Text = dsPop.Tables[0].Rows[0]["PER_4"].ToString().Trim();

                            objCommon.FillDropDownList(ddlPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND IS_OTHERS=1 AND SUB_ID<>1 AND DEGREENO=7", "SUB_NAME");
                            ddlPopSub5.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_5"].ToString().Trim();
                            txtPopMarksObt5.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_5"].ToString().Trim();
                            txtPopMaxMarks5.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_5"].ToString().Trim();
                            txtPopPer5.Text = dsPop.Tables[0].Rows[0]["PER_5"].ToString().Trim();
                            txtPopSpecify.Text = dsPop.Tables[0].Rows[0]["OTHER_SPECIFY"].ToString().Trim();
                            txtPopCutOff.Text = dsPop.Tables[0].Rows[0]["CUT_OFF"].ToString().Trim();

                            if (ddlPopSub5.SelectedItem.Text.Equals("Others"))
                            {
                                txtPopSpecify.Text = dsPop.Tables[0].Rows[0]["OTHER_SPECIFY"].ToString().Trim();
                                divPopSpecify.Visible = true;
                            }
                            else
                            {
                                divPopSpecify.Visible = false;

                            }
                            if (rdoPopAvailable.SelectedValue == "1")
                            {
                                divPopQual.Visible = true;
                            }
                            else
                                divPopQual.Visible = false;
                            objCommon.FillDropDownList(ddlPopIncome, "ACD_USER_ANNUAL_INCOME", "INCOME_NO", "ANNUAL_INCOME", "INCOME_NO > 0 AND ANNUAL_INCOME<>''", "INCOME_NO");

                            ddlPopIncome.SelectedValue = dsPop.Tables[0].Rows[0]["INCOME_NO"].ToString().Trim();
                            ddlPopPhase.SelectedValue = dsPop.Tables[0].Rows[0]["PHASE"].ToString().Trim();
                            objCommon.FillDropDownList(ddlPopDoYouKnow, "ACD_USER_INSTITUTE_ADV", "INST_ADV_ID", "ADV_NAME", "INST_ADV_ID > 0 AND ADV_NAME <>''", "INST_ADV_ID");

                            ddlPopDoYouKnow.SelectedValue = dsPop.Tables[0].Rows[0]["INSTITUTE_ADV"].ToString().Trim();
                            if (ddlPopDoYouKnow.SelectedValue == "9")
                            {
                                div1.Attributes.Add("style", "display:block");
                                txtPopDoYouKnow.Text = dsPop.Tables[0].Rows[0]["INSTITUTE_ADV_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                div1.Attributes.Add("style", "display:none");
                            }
                            byte[] imgPopData = null;
                            ViewState["PHOTO"] = null;
                            if (dsPop.Tables[0].Rows[0]["PHOTO_MAIN"] != DBNull.Value)
                            {
                                imgPopData = dsPop.Tables[0].Rows[0]["PHOTO_MAIN"] as byte[];

                                imgPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgPopData);
                                ViewState["PHOTO"] = dsPop.Tables[0].Rows[0]["PHOTO_MAIN"];
                            }
                            else
                            {
                                imgPhoto.ImageUrl = "~/IMAGE/nophoto.jpg";

                            }
                            DataSet dsPopDetails = new DataSet();
                            dsPopDetails = objFetchData.GetUserDetailsByUserno(ViewState["userno"].ToString());
                            if (dsPopDetails.Tables[0].Rows.Count > 0)
                            {
                                string degree = string.Empty;
                                string Pop_Pref_1 = string.Empty;
                                string Pop_Pref_2 = string.Empty;
                                string Pop_Pref_3 = string.Empty;
                                lblPopPref1.Text = dsPopDetails.Tables[0].Rows[0]["DEGREE_CODE"].ToString().Trim() + " Programme Course Preference 1";
                                lblPopPref2.Text = dsPopDetails.Tables[0].Rows[0]["DEGREE_CODE"].ToString().Trim() + " Programme Course Preference 2";
                                lblPopPref3.Text = dsPopDetails.Tables[0].Rows[0]["DEGREE_CODE"].ToString().Trim() + " Programme Course Preference 3";
                                lblPopPref1.Font.Bold = true;
                                lblPopPref2.Font.Bold = true;
                                lblPopPref3.Font.Bold = true;
                                int count = dsPopDetails.Tables[2].Rows.Count;
                                if (count == 1)
                                {
                                    Pop_Pref_1 = dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim();
                                }
                                else if (count == 2)
                                {
                                    Pop_Pref_1 = dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim();
                                    Pop_Pref_2 = dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim();
                                }
                                else
                                {
                                    Pop_Pref_1 = dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim();
                                    Pop_Pref_2 = dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim();
                                    Pop_Pref_3 = dsPopDetails.Tables[2].Rows[2]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[2]["PREF"].ToString().Trim();
                                }
                                if (Pop_Pref_1 == string.Empty)
                                {
                                    Pop_Pref_1 = "0";
                                }
                                if (Pop_Pref_2 == string.Empty)
                                {
                                    Pop_Pref_2 = "0";
                                }
                                if (Pop_Pref_3 == string.Empty)
                                {
                                    Pop_Pref_3 = "0";
                                }
                                txtPopName.Text = dsPopDetails.Tables[0].Rows[0]["FIRSTNAME"].ToString().Trim();
                                txtPopEmailID.Text = dsPopDetails.Tables[0].Rows[0]["EMAILID"].ToString().Trim();
                                degree = dsPopDetails.Tables[0].Rows[0]["DEGREENO"].ToString().Trim();
                                txtPopMob.Text = dsPopDetails.Tables[0].Rows[0]["MOBILENO"].ToString().Trim();
                                objCommon.FillDropDownList(ddlPopPref1, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(degree), "DB.BRANCHNO");
                                objCommon.FillDropDownList(ddlPopPref2, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(degree), "DB.BRANCHNO");
                                objCommon.FillDropDownList(ddlPopPref3, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(degree), "DB.BRANCHNO");
                                if (Convert.ToInt32(Pop_Pref_1) == 1)
                                {
                                    ddlPopPref1.SelectedValue = dsPopDetails.Tables[2].Rows[0]["BRANCH"].ToString().Trim();
                                }
                                if (Convert.ToInt32(Pop_Pref_2) == 2)
                                {
                                    ddlPopPref2.SelectedValue = dsPopDetails.Tables[2].Rows[1]["BRANCH"].ToString().Trim();
                                }
                                if (Convert.ToInt32(Pop_Pref_3) == 3)
                                {
                                    ddlPopPref3.SelectedValue = dsPopDetails.Tables[2].Rows[2]["BRANCH"].ToString().Trim();
                                }
                            }
                        }
                        ////////////////
                        else
                        {

                        }

                    }
                    else
                    {

                    }
                    #endregion
                }
                else
                {
                    dsPop = objFetchData.GetAllRegistrationDetails(ViewState["userno"].ToString());
                    Mp2ForOthers.Show();
                    Mp1.Hide();
                    objCommon.FillDropDownList(ddlotherPopReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO > 0 AND ACTIVESTATUS=1", "RELIGION");
                    objCommon.FillDropDownList(ddlotherPopCommunity, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORY");
                    objCommon.FillDropDownList(ddlotherPopTongue, "ACD_MTONGUE", "MTONGUENO", "MTONGUE", "MTONGUENO > 0 AND ACTIVESTATUS=1", "MTONGUE");
                    objCommon.FillDropDownList(ddlotherPopNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO=1 AND ACTIVESTATUS=1", "NATIONALITY");
                    ViewState["userno"] = dsPop.Tables[0].Rows[0]["USERNO"].ToString();
                    objCommon.FillDropDownList(ddlProgramme, "ACD_COLLEGE_DEGREE_BRANCH  ADB INNER JOIN ACD_UA_SECTION  AU ON (ADB.UGPGOT = AU.UA_SECTION) INNER JOIN ACD_DEGREE AD ON (ADB.DEGREENO = AD.DEGREENO) INNER JOIN ACD_ADMISSION_CONFIG C ON (AD.DEGREENO = C.DEGREENO) INNER JOIN ACD_USER_REGISTRATION RR ON(RR.DEGREENO=C.DEGREENO)", "distinct ADB.DEGREENO", "AD.DEGREE_CODE", "AD.DEGREENO > 0 AND ADB.UGPGOT>0 AND USERNO=" + Convert.ToInt32(ViewState["userno"]), "ADB.DEGREENO");
                    ddlProgramme.SelectedIndex = 1;
                    //ddlProgramme.SelectedValue = objCommon.LookUp("ACD_USER_REGISTRATION", "DEGREENO", "USERNO=" + Convert.ToInt32(ViewState["userno"]));
                    string chkSpecialize = objCommon.LookUp("ACD_SUBJECT_ONLINE", "COUNT(1)", "SPECIALIZE=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                    if (Convert.ToInt32(chkSpecialize.ToString()) > 0)
                    {
                        objCommon.FillDropDownList(ddlSpec, "ACD_SUBJECT_ONLINE", "DISTINCT BRANCHNO", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "SPECIALIZE=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND ACTIVE_STATUS=1", "BRANCHNO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSpec, "ACD_DEGREE_SPECIALIZATION_MAPPING", "SPEC_NO", "SPECIALIZATION", "DEGREE=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND ACTIVE_STATUS=1", "SPECIALIZATION");
                    }
                    if (ddlProgrammeType.SelectedValue == "1")
                    {
                        ddlSpec.SelectedValue = dsPop.Tables[0].Rows[0]["SPECIALIZATION"].ToString();
                        if (ddlSpec.SelectedIndex > 0)
                        {
                            chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)=" + Convert.ToInt32(ddlSpec.SelectedValue) + " AND BRANCHNO IS NOT NULL AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                        }
                        else
                        {
                            chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)!=0 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                        }
                    }
                    if (ddlProgramme.SelectedValue == "1")
                    {
                        lblNATA.InnerText = ddlProgramme.SelectedItem.Text.ToString() + " Programme Details";
                        //divNATA.Visible = true;
                    }
                    else
                    {
                        divNATA.Visible = false;
                    }

                    rdootherPopGender.SelectedValue = dsPop.Tables[0].Rows[0]["GENDER_MAIN"].ToString().Trim();
                    txtotherPopName.Text = dsPop.Tables[0].Rows[0]["FIRSTNAME"].ToString().Trim();
                    txtotherPopDOB.Text = dsPop.Tables[0].Rows[0]["DOB_MAIN"].ToString().Trim();
                    ddlotherPopReligion.SelectedValue = dsPop.Tables[0].Rows[0]["RELIGIONNO"].ToString().Trim();
                    if (ddlotherPopReligion.SelectedValue == "11")
                    {
                        divotherPopReligion.Visible = true;
                        txtotherPopOtherReligion.Text = dsPop.Tables[0].Rows[0]["RELIGION_OTHER"].ToString().Trim();
                    }
                    else
                    {
                        divotherPopReligion.Visible = false;
                    }
                    ddlotherPopCommunity.SelectedValue = dsPop.Tables[0].Rows[0]["COMMUNITY"].ToString().Trim();
                    ddlotherPopTongue.SelectedValue = dsPop.Tables[0].Rows[0]["MTONGUENO"].ToString().Trim();
                    if (ddlotherPopTongue.SelectedValue == "46")
                    {
                        divotherPopTongue.Visible = true;
                        txtotherPopTongueother.Text = dsPop.Tables[0].Rows[0]["MOTHER_TONGUE_OTHER"].ToString().Trim();
                    }
                    else
                    {
                        divotherPopTongue.Visible = false;
                    }
                    txtotherPopAadhar.Text = dsPop.Tables[0].Rows[0]["ADHAARNO"].ToString().Trim();
                    ddlotherPopNationality.SelectedValue = dsPop.Tables[0].Rows[0]["NATIONALITYNO"].ToString().Trim();
                    txtotherPopFather.Text = dsPop.Tables[0].Rows[0]["FATHERNAME"].ToString().Trim();

                    objCommon.FillDropDownList(ddlotherPopFOcc, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION >0 AND OCCUPATION NOT IN (9) AND ACTIVESTATUS=1", "OCCUPATION");
                    ddlotherPopFOcc.SelectedValue = dsPop.Tables[0].Rows[0]["FATHER_OCCUPATION"].ToString().Trim();
                    if (ddlotherPopFOcc.SelectedValue == "10")
                    {
                        divotherPopFocc.Visible = true;
                        txtotherPopFoccOther.Text = dsPop.Tables[0].Rows[0]["F_OCCUPATION_OTHER"].ToString().Trim();
                    }
                    else
                    {
                        divotherPopFocc.Visible = false;
                    }
                    txtotherPopFmobile.Text = dsPop.Tables[0].Rows[0]["FATHER_MOBILE"].ToString().Trim();
                    txtotherPopMother.Text = dsPop.Tables[0].Rows[0]["MOTHERNAME"].ToString().Trim();
                    objCommon.FillDropDownList(ddlotherPopMOcc, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION >0 AND ACTIVESTATUS=1", "OCCUPATION");
                    ddlotherPopMOcc.SelectedValue = dsPop.Tables[0].Rows[0]["MOTHER_OCCUPATION"].ToString().Trim();
                    if (ddlotherPopMOcc.SelectedValue == "10")
                    {
                        divotherMOccupation.Visible = true;
                        txtotherPopMOccOther.Text = dsPop.Tables[0].Rows[0]["M_OCCUPATION_OTHER"].ToString().Trim();
                    }
                    else
                    {
                        divotherMOccupation.Visible = false;
                    }
                    txtotherPopMMobile.Text = dsPop.Tables[0].Rows[0]["MOTHER_MOBILE"].ToString().Trim();

                    txtotherPopAdd1.Text = dsPop.Tables[0].Rows[0]["ADDRESS_1"].ToString().Trim();
                    txtotherPopAdd2.Text = dsPop.Tables[0].Rows[0]["ADDRESS_2"].ToString().Trim();
                    txtotherPopAdd3.Text = dsPop.Tables[0].Rows[0]["ADDRESS_3"].ToString().Trim();
                    txtotherPopCity.Text = dsPop.Tables[0].Rows[0]["CITY"].ToString().Trim();

                    objCommon.FillDropDownList(ddlotherPopState, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0 AND ACTIVESTATUS=1", "STATENAME");
                    ddlotherPopState.SelectedValue = dsPop.Tables[0].Rows[0]["STATE"].ToString().Trim();
                    objCommon.FillDropDownList(ddlotherPopDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO > 0 AND ACTIVESTATUS=1 AND STATENO=" + Convert.ToInt32(ddlotherPopState.SelectedValue), "DISTRICTNAME");
                    ddlotherPopDistrict.SelectedValue = dsPop.Tables[0].Rows[0]["DISTRICT"].ToString().Trim();
                    txtotherPopPin.Text = dsPop.Tables[0].Rows[0]["PINCODE"].ToString().Trim();
                    ViewState["pincode"] = txtotherPopPin.Text;
                    objCommon.FillDropDownList(ddlotherPopEdu, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO >0", "QUALILEVELNAME");
                    ddlotherPopEdu.SelectedValue = dsPop.Tables[0].Rows[0]["EDU_INFORMATION"].ToString().Trim() == "0" ? "0" : dsPop.Tables[0].Rows[0]["EDU_INFORMATION"].ToString().Trim();
                    if (ddlotherPopEdu.SelectedValue == "4")
                    {
                        divotherPopEdu.Visible = true;
                        txtotherPopEduOthers.Text = dsPop.Tables[0].Rows[0]["EDU_INFO_OTHER"].ToString().Trim();
                    }
                    else
                    {
                        divotherPopEdu.Visible = false;
                    }
                    if (ddlProgrammeType.SelectedValue == "1")
                    {
                        UpdatePanel5.Visible = true;
                        txtotherPopExmReg.Text = dsPop.Tables[0].Rows[0]["EXM_REG_12"].ToString().Trim();
                        txtotherPopSchool.Text = dsPop.Tables[0].Rows[0]["SCHOOL_NAME"].ToString().Trim();
                        objCommon.FillDropDownList(ddlotherPopMPass, "ACD_MONTH", "MONTHNO", "MONTH_NAME", "MONTHNO >0", "MONTHNO");
                        ddlotherPopMPass.SelectedValue = dsPop.Tables[0].Rows[0]["MONTH_PASS_NO"].ToString().Trim() == "0" ? "0" : dsPop.Tables[0].Rows[0]["MONTH_PASS_NO"].ToString().Trim();
                        objCommon.FillDropDownList(ddlotherPopYPass, "ACD_QUAL_YEAR", "YEARNO", "YEAR_NAME", "YEARNO >0", "YEAR_NAME DESC");
                        ddlotherPopYPass.SelectedValue = dsPop.Tables[0].Rows[0]["YEAR_PASS_NO"].ToString().Trim() == "0" ? "0" : dsPop.Tables[0].Rows[0]["YEAR_PASS_NO"].ToString().Trim();
                        rdootherPopMedium.SelectedValue = dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim().Equals("") ? "0" : dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim();
                        string medium = dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim().Equals("") ? "0" : dsPop.Tables[0].Rows[0]["MEDIUM_NO"].ToString().Trim();
                        if (!medium.ToString().Equals("0"))
                        {
                            if (rdootherPopMedium.SelectedValue == "2")
                            {
                                divotherPopMedium.Visible = true;
                                txtotherPopMediumOther.Text = dsPop.Tables[0].Rows[0]["MEDIUM_OTHER"].ToString().Trim();
                            }
                            else
                            {
                                divotherPopMedium.Visible = false;
                            }
                        }
                        objCommon.FillDropDownList(ddlotherPopLast, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO > 0 AND ACTIVESTATUS=1", "NATIONALITY");
                        ddlotherPopLast.SelectedValue = dsPop.Tables[0].Rows[0]["COUNTRY_ID"].ToString().Trim();
                        rdootherPopAvailable.SelectedValue = dsPop.Tables[0].Rows[0]["AVAILABLE"].ToString().Equals("") ? "0" : dsPop.Tables[0].Rows[0]["AVAILABLE"].ToString().Trim();

                        if (rdootherPopAvailable.SelectedValue == "1")
                        {
                            divotherPopQual.Visible = true;
                            if (dsPop.Tables[0].Rows[0]["UGPGOT"].ToString().Equals("1"))
                            {
                                if (chkSpecial.ToString() != (string.Empty))
                                {
                                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                                }
                                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=1 AND ACTIVE_STATUS=1 AND IS_COMPULSORY=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=1 AND ACTIVE_STATUS=1 AND IS_COMPULSORY=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                                }

                                ddlotherPopSub1.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_1"].ToString().Trim();
                                txtotherPopMarksObt1.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_1"].ToString().Trim();
                                txtotherPopMaxMarks1.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_1"].ToString().Trim();
                                txtotherPopPer1.Text = dsPop.Tables[0].Rows[0]["PER_1"].ToString().Trim();
                                hdnPer_1other.Value = dsPop.Tables[0].Rows[0]["PER_1"].ToString().Trim();

                                txtotherPopLang.Text = dsPop.Tables[0].Rows[0]["LANGUAGE"].ToString().Trim();
                                txtotherPopMarksObtLang.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_LANG"].ToString().Trim();
                                txtotherPopMaxMarksLang.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_LANG"].ToString().Trim();
                                txtotherPopPerLang.Text = dsPop.Tables[0].Rows[0]["PER_LANG"].ToString().Trim();
                                hdnPerLangother.Value = dsPop.Tables[0].Rows[0]["PER_LANG"].ToString().Trim();

                                if (chkSpecial.ToString() != (string.Empty))
                                {
                                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                                }
                                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=2 AND ACTIVE_STATUS=1 AND ISNULL(IS_OTHERS,0)=0 AND SUB_ID!=" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=2 AND ACTIVE_STATUS=1 AND ISNULL(IS_OTHERS,0)=0 AND SUB_ID!=" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                                }
                                ddlotherPopSub2.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_2"].ToString().Trim();
                                DataSet dsSubStatus = objFetchData.GetSubjectStatus(Convert.ToInt32(ddlotherPopSub2.SelectedValue));
                                if (dsSubStatus.Tables[0].Rows.Count > 0)
                                {
                                    hdnDDL2other.Value = dsSubStatus.Tables[0].Rows[0]["IS_CUTOFF"].ToString();
                                }
                                txtotherPopMarksObt2.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_2"].ToString().Trim();
                                txtotherPopMaxMarks2.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_2"].ToString().Trim();
                                txtotherPopPer2.Text = dsPop.Tables[0].Rows[0]["PER_2"].ToString().Trim();
                                hdnPer_2other.Value = dsPop.Tables[0].Rows[0]["PER_2"].ToString().Trim();

                                if (chkSpecial.ToString() != (string.Empty))
                                {
                                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                                }
                                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=3 AND ISNULL(IS_OTHERS,0)=0 AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=3 AND ISNULL(IS_OTHERS,0)=0 AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                                }
                                ddlotherPopSub3.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_3"].ToString().Trim();
                                dsSubStatus = objFetchData.GetSubjectStatus(Convert.ToInt32(ddlotherPopSub3.SelectedValue));
                                if (dsSubStatus.Tables[0].Rows.Count > 0)
                                {
                                    hdnDDL3other.Value = dsSubStatus.Tables[0].Rows[0]["IS_CUTOFF"].ToString();
                                }
                                txtotherPopMarksObt3.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_3"].ToString().Trim();
                                txtotherPopMaxMarks3.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_3"].ToString().Trim();
                                txtotherPopPer3.Text = dsPop.Tables[0].Rows[0]["PER_3"].ToString().Trim();
                                hdnPer_3other.Value = dsPop.Tables[0].Rows[0]["PER_3"].ToString().Trim();


                                if (chkSpecial.ToString() != (string.Empty))
                                {
                                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                                }
                                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=4  AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=4  AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                                }
                                ddlotherPopSub4.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_4"].ToString().Trim();
                                if (chkSpecial.ToString() != (string.Empty))
                                {
                                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                                }
                                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=5 AND IS_OTHERS=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + "AND BRANCHNO=" + branch + " AND SUB_ID NOT IN (" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub4.SelectedValue) + ")", "SUB_NAME");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlotherPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=5 AND IS_OTHERS=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND SUB_ID NOT IN (" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub4.SelectedValue) + ")", "SUB_NAME");
                                }
                                dsSubStatus = objFetchData.GetSubjectStatus(Convert.ToInt32(ddlotherPopSub4.SelectedValue));
                                if (dsSubStatus.Tables[0].Rows.Count > 0)
                                {
                                    hdnDDL4other.Value = dsSubStatus.Tables[0].Rows[0]["IS_CUTOFF"].ToString();
                                }
                                txtotherPopMarksObt4.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_4"].ToString().Trim();
                                txtotherPopMaxMarks4.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_4"].ToString().Trim();
                                txtotherPopPer4.Text = dsPop.Tables[0].Rows[0]["PER_4"].ToString().Trim();
                                hdnPer_4other.Value = dsPop.Tables[0].Rows[0]["PER_4"].ToString().Trim();

                                objCommon.FillDropDownList(ddlotherPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND IS_OTHERS=1 AND SUB_ID<>1 AND DEGREENO !=7 AND SUB_NAME IS NOT NULL", "SUB_NAME");
                                ddlotherPopSub5.SelectedValue = dsPop.Tables[0].Rows[0]["SUB_5"].ToString().Trim();
                                txtotherPopMarksObt5.Text = dsPop.Tables[0].Rows[0]["MARKS_OBT_5"].ToString().Trim();
                                txtotherPopMaxMarks5.Text = dsPop.Tables[0].Rows[0]["MAX_MARKS_5"].ToString().Trim();
                                txtotherPopPer5.Text = dsPop.Tables[0].Rows[0]["PER_5"].ToString().Trim();
                                hdnPer_5other.Value = dsPop.Tables[0].Rows[0]["PER_5"].ToString().Trim();

                                if (ddlotherPopSub5.SelectedItem.Text.Equals("Others"))
                                {
                                    txtotherPopSpecify.Text = dsPop.Tables[0].Rows[0]["OTHER_SPECIFY"].ToString().Trim();
                                    divotherPopEdu.Attributes.Add("style", "display:block");
                                }
                                else
                                {
                                    divotherPopEdu.Attributes.Add("style", "display:none");
                                }
                                txtotherPopCutOff.Text = dsPop.Tables[0].Rows[0]["CUT_OFF"].ToString().Trim();
                                hdnCutOff.Value = dsPop.Tables[0].Rows[0]["CUT_OFF"].ToString().Trim();
                            }
                        }
                        else
                            divotherPopQual.Visible = false;
                    }
                    else
                    {
                        divotherPopQual.Visible = false;
                        UpdatePanel5.Visible = false;
                        divSpec.Visible = false;
                    }
                    objCommon.FillDropDownList(ddlotherPopIncome, "ACD_USER_ANNUAL_INCOME", "INCOME_NO", "ANNUAL_INCOME", "INCOME_NO > 0 AND ANNUAL_INCOME<>''", "INCOME_NO");
                    ddlotherPopIncome.SelectedValue = dsPop.Tables[0].Rows[0]["INCOME_NO"].ToString().Trim();
                    objCommon.FillDropDownList(ddlotherPopDoYouKnow, "ACD_USER_INSTITUTE_ADV", "INST_ADV_ID", "ADV_NAME", "INST_ADV_ID > 0 AND ADV_NAME <>''", "INST_ADV_ID");
                    ddlotherPopDoYouKnow.SelectedValue = dsPop.Tables[0].Rows[0]["INSTITUTE_ADV"].ToString().Trim();
                    if (ddlotherPopDoYouKnow.SelectedValue == "9")
                    {
                        div1.Attributes.Add("style", "display:block");
                        txtotherPopDoYouKnow.Text = dsPop.Tables[0].Rows[0]["INSTITUTE_ADV_OTHER"].ToString().Trim();
                    }
                    else
                    {
                        div1.Attributes.Add("style", "display:none");
                    }
                    byte[] imgPopData = null;
                    ViewState["PHOTO"] = null;
                    if (dsPop.Tables[0].Rows[0]["PHOTO_MAIN"] != DBNull.Value)
                    {
                        imgPopData = dsPop.Tables[0].Rows[0]["PHOTO_MAIN"] as byte[];
                        imgotherPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgPopData);
                        ViewState["PHOTO"] = dsPop.Tables[0].Rows[0]["PHOTO_MAIN"];
                    }
                    else
                    {
                        imgotherPhoto.ImageUrl = "~/IMAGE/nophoto.jpg";

                    }
                    DataSet dsPopDetails = new DataSet();
                    dsPopDetails = objFetchData.GetUserDetailsByUserno(ViewState["userno"].ToString());
                    if (dsPopDetails.Tables[0].Rows.Count > 0)
                    {
                        string degree = string.Empty;
                        string Pop_Pref_1 = string.Empty;
                        string Pop_Pref_2 = string.Empty;
                        string Pop_Pref_3 = string.Empty;
                        lblPopPref1.Text = dsPopDetails.Tables[0].Rows[0]["DEGREE_CODE"].ToString().Trim() + " Programme Course Preference 1";
                        lblPopPref2.Text = dsPopDetails.Tables[0].Rows[0]["DEGREE_CODE"].ToString().Trim() + " Programme Course Preference 2";
                        lblPopPref3.Text = dsPopDetails.Tables[0].Rows[0]["DEGREE_CODE"].ToString().Trim() + " Programme Course Preference 3";
                        lblPopPref1.Font.Bold = true;
                        lblPopPref2.Font.Bold = true;
                        lblPopPref3.Font.Bold = true;
                        int count = dsPopDetails.Tables[2].Rows.Count;
                        if (count == 1)
                        {
                            Pop_Pref_1 = dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim();
                        }
                        else if (count == 2)
                        {
                            Pop_Pref_1 = dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim();
                            Pop_Pref_2 = dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim();
                        }
                        else
                        {
                            Pop_Pref_1 = dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[0]["PREF"].ToString().Trim();
                            Pop_Pref_2 = dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[1]["PREF"].ToString().Trim();
                            Pop_Pref_3 = dsPopDetails.Tables[2].Rows[2]["PREF"].ToString().Trim().Equals(string.Empty) ? "0" : dsPopDetails.Tables[2].Rows[2]["PREF"].ToString().Trim();
                        }
                        if (Pop_Pref_1 == string.Empty)
                        {
                            Pop_Pref_1 = "0";
                        }
                        if (Pop_Pref_2 == string.Empty)
                        {
                            Pop_Pref_2 = "0";
                        }
                        if (Pop_Pref_3 == string.Empty)
                        {
                            Pop_Pref_3 = "0";
                        }
                        txtotherPopName.Text = dsPopDetails.Tables[0].Rows[0]["FIRSTNAME"].ToString().Trim();
                        txtotherPopEmail.Text = dsPopDetails.Tables[0].Rows[0]["EMAILID"].ToString().Trim();
                        degree = dsPopDetails.Tables[0].Rows[0]["DEGREENO"].ToString().Trim();
                        txtotherPopMobile.Text = dsPopDetails.Tables[0].Rows[0]["MOBILENO"].ToString().Trim();
                        //objCommon.FillDropDownList(ddlotherPopPref1, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(degree), "DB.BRANCHNO");
                        //objCommon.FillDropDownList(ddlPopPref2, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(degree), "DB.BRANCHNO");
                        //objCommon.FillDropDownList(ddlPopPref3, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(degree), "DB.BRANCHNO");
                        //if (Convert.ToInt32(Pop_Pref_1) == 1)
                        //{
                        //    ddlPopPref1.SelectedValue = dsPopDetails.Tables[2].Rows[0]["BRANCH"].ToString().Trim();
                        //}
                        //if (Convert.ToInt32(Pop_Pref_2) == 2)
                        //{
                        //    ddlPopPref2.SelectedValue = dsPopDetails.Tables[2].Rows[1]["BRANCH"].ToString().Trim();
                        //}
                        //if (Convert.ToInt32(Pop_Pref_3) == 3)
                        //{
                        //    ddlPopPref3.SelectedValue = dsPopDetails.Tables[2].Rows[2]["BRANCH"].ToString().Trim();
                        //}
                    }
                }
            }
            //--------------------------------Checking Degree type-----------------------------------------------------           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Details.lnkUserId_Click" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitPopup_Click(object sender, EventArgs e)
    {
        string mother_Tongue = string.Empty;
        try
        {
            if (ddlPopReligion.SelectedValue == "11" && (txtPopOtherReligion.Text == string.Empty || txtPopOtherReligion.Text == "" || txtPopOtherReligion.Text == null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                txtPopOtherReligion.Focus();
                return;
            }
            if (ddlPopTongue.SelectedValue == "46" && (txtPopTongue.Text == string.Empty || txtPopTongue.Text == "" || txtPopTongue.Text == null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                txtPopTongue.Focus();
                return;
            }
            if (ddlPopFOcc.SelectedValue == "10" && (txtPopFoccOther.Text == string.Empty || txtPopFoccOther.Text == "" || txtPopFoccOther.Text == null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);

                txtPopFoccOther.Focus();
                return;
            }
            if (ddlPopMOcc.SelectedValue == "10" && (txtPopMOccOther.Text == string.Empty || txtPopMOccOther.Text == "" || txtPopMOccOther.Text == null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                txtPopMOccOther.Focus();
                return;
            }
            if (ddlPopDoYouKnow.SelectedValue == "9" && (txtPopDoYouKnow.Text == string.Empty || txtPopDoYouKnow.Text == "" || txtPopDoYouKnow.Text == null))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                txtPopDoYouKnow.Focus();
                return;
            }
            if (ddlPopSub5.SelectedValue == "15" && (txtPopSpecify.Text == string.Empty || txtPopSpecify.Text == "" || txtPopSpecify.Text == null))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Enter Others Please Specify.`)", true);
                txtPopSpecify.Focus();
                divPopSpecify.Attributes.Add("style", "display:block");
                return;
            }
            int userNO = Convert.ToInt32(ViewState["userno"]);
            DataSet dsStatus = objFetchData.Get_Students_Status_Details(Convert.ToInt32(ViewState["userno"]));
            if (dsStatus.Tables[0].Rows.Count > 0)
            {
                confirm = dsStatus.Tables[0].Rows[0]["CONFIRM_STATUS"] == DBNull.Value ? string.Empty : dsStatus.Tables[0].Rows[0]["CONFIRM_STATUS"].ToString();
            }
            ViewState["USER"] = userNO;
            objus.FIRSTNAME = txtPopName.Text.ToString().Trim();
            objus.Gender = Convert.ToInt32(rdoPopGender.SelectedValue);
            objus.DOB = Convert.ToDateTime(txtPopDOB.Text.ToString().Trim());
            if (ddlPopReligion.SelectedIndex > 0)
            {
                if (ddlPopReligion.SelectedValue == "11")
                {
                    objus.Religion = Convert.ToInt32(ddlPopReligion.SelectedValue);
                    objus.Religion_Other = txtPopOtherReligion.Text.ToString().Trim();
                }
                else
                {
                    objus.Religion = Convert.ToInt32(ddlPopReligion.SelectedValue);
                    objus.Religion_Other = txtPopOtherReligion.Text.ToString().Trim().Equals("") ? "NA" : txtPopOtherReligion.Text.ToString().Trim();
                }
            }
            objus.Community = Convert.ToInt32(ddlPopCommunity.SelectedValue);
            if (ddlPopTongue.SelectedIndex > 0)
            {
                if (ddlPopTongue.SelectedValue == "46")
                {
                    objus.Mother_Tongue = Convert.ToInt32(ddlPopTongue.SelectedValue);
                    mother_Tongue = txtPopTongue.Text.ToString().Trim();
                }
                else
                {
                    objus.Mother_Tongue = Convert.ToInt32(ddlPopTongue.SelectedValue);
                    mother_Tongue = txtPopTongue.Text.ToString().Trim().Equals("") ? "NA" : txtPopTongue.Text.ToString().Trim();
                }
            }
            objus.Aadhar = txtPopAadhar.Text.ToString().Trim();
            objus.Nationality = Convert.ToInt32(ddlPopNationality.SelectedValue);
            objus.Father_Name = txtPopFather.Text.ToString().Trim();
            if (divPopFocc.Visible == true)
            {
                objus.FOccupation_Other = txtPopFoccOther.Text.ToString().Trim();
            }
            else
            {
                objus.FOccupation_Other = txtPopFoccOther.Text == string.Empty ? "NA" : txtPopFoccOther.Text.ToString().Trim();
            }
            objus.Father_Occupation = Convert.ToInt32(ddlPopFOcc.SelectedValue);
            objus.Father_Mobile = txtPopFmobile.Text.ToString().Trim();
            objus.Mother_Name = txtPopMother.Text.ToString().Trim();
            if (divMOccupation.Visible == true)
            {
                objus.MOccupation_Other = txtPopMOccOther.Text.ToString().Trim();
            }
            else
            {
                objus.MOccupation_Other = txtPopMOccOther.Text == string.Empty ? "NA" : txtPopMOccOther.Text.ToString().Trim();
            }
            objus.Mother_Occupation = Convert.ToInt32(ddlPopMOcc.SelectedValue);
            objus.Mother_Mobile = txtPopMMobile.Text.ToString().Trim();
            objus.Address_1 = txtPopAdd1.Text.ToString().Trim();
            objus.Address_2 = txtPopAdd2.Text.ToString().Trim();
            objus.Address_3 = txtPopAdd3.Text.ToString().Trim();
            objus.City = txtPopCity.Text.ToString().Trim();
            objus.State_No = Convert.ToInt32(ddlPopState.SelectedValue);
            objus.PinCode = txtPopPin.Text.ToString().Trim();
            if (divPopEdu.Visible == true)
            {
                objus.Edu_Info_Other = txtPopEduOthers.Text.ToString().Trim();
            }
            else
            {
                objus.Edu_Info_Other = txtPopEduOthers.Text == string.Empty ? "NA" : txtPopEduOthers.Text.ToString().Trim();
            }
            objus.Edu_Info = Convert.ToInt32(ddlPopEdu.SelectedValue);
            objus.Exm_Reg_12 = txtPopExmReg.Text.ToString().Trim();
            objus.School_name = txtPopSchool.Text.ToString().Trim();
            objus.Month_No = Convert.ToInt32(ddlPopMPass.SelectedValue);
            objus.Month = ddlPopMPass.SelectedItem.Text.ToString().Trim();
            objus.Year_No = Convert.ToInt32(ddlPopYPass.SelectedValue);
            objus.Year = ddlPopYPass.SelectedItem.Text.ToString().Trim();
            if (divPopMedium.Visible == true)
            {
                objus.Medium_Other = txtPopMediumOther.Text.ToString().Trim();
            }
            else
            {
                objus.Medium_Other = txtPopMediumOther.Text == string.Empty ? "NA" : txtPopMediumOther.Text.ToString().Trim();
            }
            objus.Medium_No = Convert.ToInt32(rdoPopMedium.SelectedValue);
            objus.Medium = rdoPopMedium.SelectedItem.Text.ToString().Trim();
            objus.Country_Name = ddlPopLast.SelectedItem.Text.ToString().Trim();

            objus.Sub_1 = Convert.ToInt32(ddlPopSub1.SelectedValue);
            objus.Marks_Obt_1 = txtPopMarksObt1.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMarksObt1.Text.ToString().Trim());
            objus.Max_Marks_1 = txtPopMaxMarks1.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMaxMarks1.Text.ToString().Trim());
            txtPopPer1.Attributes.Add("readonly", "readonly");
            string Percent1 = Request.Form[hdnPer_1.UniqueID];
            objus.Per_1 = Percent1 == string.Empty || Percent1 == null ? 0 : Convert.ToDecimal(Percent1.ToString().Trim());

            objus.Language = txtPopLang.Text == string.Empty ? "NA" : txtPopLang.Text.ToString().Trim();
            objus.Marks_Obt_Lang = txtPopMarksObtLang.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMarksObtLang.Text.ToString().Trim());
            objus.Max_Marks_Lang = txtPopMaxMarksLang.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMaxMarksLang.Text.ToString().Trim());
            txtPopPerLang.Attributes.Add("readonly", "readonly");
            string Percentlang = Request.Form[hdnPerLang.UniqueID];
            objus.Per_Lang = Percentlang == string.Empty || Percentlang == null ? 0 : Convert.ToDecimal(Percentlang.ToString().Trim());

            objus.Sub_2 = Convert.ToInt32(ddlPopSub2.SelectedValue);
            objus.Marks_Obt_2 = txtPopMarksObt2.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMarksObt2.Text.ToString().Trim());
            objus.Max_Marks_2 = txtPopMaxMarks2.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMaxMarks2.Text.ToString().Trim());
            txtPopPer2.Attributes.Add("readonly", "readonly");
            string Percent2 = Request.Form[hdnPer_2.UniqueID];
            objus.Per_2 = Percent2 == string.Empty || Percent2 == null ? 0 : Convert.ToDecimal(Percent2.ToString().Trim());

            objus.Sub_3 = Convert.ToInt32(ddlPopSub3.SelectedValue);
            objus.Marks_Obt_3 = txtPopMarksObt3.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMarksObt3.Text.ToString().Trim());
            objus.Max_Marks_3 = txtPopMaxMarks3.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMaxMarks3.Text.ToString().Trim());
            txtPopPer3.Attributes.Add("readonly", "readonly");
            string Percent3 = Request.Form[hdnPer_3.UniqueID];
            objus.Per_3 = Percent3 == string.Empty || Percent3 == null ? 0 : Convert.ToDecimal(Percent3.ToString().Trim());

            objus.Sub_4 = Convert.ToInt32(ddlPopSub4.SelectedValue);
            objus.Marks_Obt_4 = txtPopMarksObt4.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMarksObt4.Text.ToString().Trim());
            objus.Max_Marks_4 = txtPopMaxMarks4.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMaxMarks4.Text.ToString().Trim());
            txtPopPer4.Attributes.Add("readonly", "readonly");
            string Percent4 = Request.Form[hdnPer_4.UniqueID];
            objus.Per_4 = Percent4 == string.Empty || Percent4 == null ? 0 : Convert.ToDecimal(Percent4.ToString().Trim());

            objus.Sub_5 = Convert.ToInt32(ddlPopSub5.SelectedValue);
            objus.Marks_Obt_5 = txtPopMarksObt5.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMarksObt5.Text.ToString().Trim());
            objus.Max_Marks_5 = txtPopMaxMarks5.Text == string.Empty ? 0 : Convert.ToDecimal(txtPopMaxMarks5.Text.ToString().Trim());
            txtPopPer5.Attributes.Add("readonly", "readonly");
            string Percent5 = Request.Form[hdnPer_5.UniqueID];
            objus.Per_5 = Percent5 == string.Empty || Percent5 == null ? 0 : Convert.ToDecimal(Percent5.ToString().Trim());

            objus.Other_Specify = txtPopSpecify.Text == string.Empty ? "NA" : txtPopSpecify.Text.ToString().Trim();
            objus.Cut_off = hdnCutOff.Value == string.Empty || hdnPer_5other.Value == null ? 0 : Convert.ToDecimal(hdnCutOff.Value.ToString().Trim());
            objus.Available = Convert.ToInt32(rdoPopAvailable.SelectedValue);
            objus.Country_Id = Convert.ToInt32(ddlPopLast.SelectedValue);
            objus.AnnualIncome = ddlPopIncome.SelectedIndex > 0 ? Convert.ToInt32(ddlPopIncome.SelectedValue) : 0;
            objus.Phase = ddlPopPhase.SelectedValue;
            if (ddlPopDoYouKnow.SelectedIndex > 0)
            {
                if (ddlPopDoYouKnow.SelectedValue == "9")
                {
                    objus.InstituteADV = Convert.ToInt32(ddlPopDoYouKnow.SelectedValue);
                    objus.InstituteADV_Other = txtPopDoYouKnow.Text.Trim();
                }
                else
                {
                    objus.InstituteADV = Convert.ToInt32(ddlPopDoYouKnow.SelectedValue);
                    objus.InstituteADV_Other = txtPopDoYouKnow.Text.ToString().Trim().Equals("") ? "NA" : txtPopDoYouKnow.Text.ToString().Trim();
                }
            }
            objus.District = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0;

            bool isPhotoUploaded = false;
            if (fuPhotoUpload.HasFile || ViewState["PHOTO"] != null)
            {
                isPhotoUploaded = true;
            }
            if (!isPhotoUploaded)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Photo.", this.Page);
                return;
            }
            string photo = string.Empty;
            if (fuPhotoUpload.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuPhotoUpload.PostedFile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".JPG" || ext == ".JPEG")
                {
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload file with .jpg,.jpeg format only.", this.Page);
                    return;
                }
                if (fuPhotoUpload.PostedFile.ContentLength < 500000)
                {
                    byte[] resizephoto = ResizePhoto(fuPhotoUpload);
                    if (resizephoto.LongLength >= 500000)
                    {
                        objCommon.DisplayMessage(this, "File size should be less than or equal to 500 KB.", this.Page);
                        return;
                    }
                    else
                    {
                        objus.PHOTO = resizephoto;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "File size should be less than or equal to 500 KB.", this.Page);
                    return;
                }
                photo = "Photo";
            }
            else
            {
                if (ViewState["PHOTO"] == null)
                {
                }
                else
                {
                    objus.PHOTO = null;
                    objus.PHOTO = ViewState["PHOTO"] as byte[];
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Details.GetUserDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        CustomStatus cs = (CustomStatus)objFetchData.AddRegistrationDetails(objus, Convert.ToInt32(ViewState["USER"]), mother_Tongue);
        //CustomStatus csGenerate = (CustomStatus)objFetchData.GenerateSerialNo_OnlineAdm(Convert.ToInt32(ViewState["USER"]), Convert.ToInt32(ViewState["degree"]), Convert.ToInt32(ViewState["admissionBatch"]));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.Page, "Registration Details Saved Successfully.", this.Page);
            Mp1.Hide();
            return;
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(this.Page, "Registration Details Updated Successfully.", this.Page);
            fuPhotoUpload.Dispose();
            fuPhotoUpload.Attributes.Clear();
            fuPhotoUpload.PostedFile.InputStream.Dispose();
            Mp1.Hide();
            //Response.Redirect(Request.Url.ToString()); 
            return;
        }
    }

    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream as Stream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }
    protected void ddlPopReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopReligion.SelectedValue == "11")
            {
                divPopReligion.Visible = true;
                divPopReligion.Attributes.Add("style", "display:block");
            }
            else
            {
                divPopReligion.Visible = false;
                divPopReligion.Attributes.Add("style", "display:none");
            }
            ddlPopReligion.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPopTongue_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopTongue.SelectedValue == "46")
            {
                divPopTongue.Visible = true;
                divPopTongue.Attributes.Add("style", "display:block");
            }
            else
            {
                divPopTongue.Visible = false;
                divPopTongue.Attributes.Add("style", "display:none");
            }
            ddlPopTongue.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Mp1.Hide();
    }
    protected void ddlPopFOcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopFOcc.SelectedValue == "10")
            {
                divPopFocc.Visible = true;
                divPopFocc.Attributes.Add("style", "display:block");
            }
            else
            {
                divPopFocc.Visible = false;
                divPopFocc.Attributes.Add("style", "display:none");
            }
            ddlPopFOcc.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPopMOcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopMOcc.SelectedValue == "10")
            {
                divMOccupation.Visible = true;
                divMOccupation.Attributes.Add("style", "display:block");
            }
            else
            {
                divMOccupation.Visible = false;
                divMOccupation.Attributes.Add("style", "display:none");
            }
            ddlPopMOcc.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPopEdu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopEdu.SelectedValue == "4")
            {
                divPopEdu.Visible = true;
                divPopEdu.Attributes.Add("style", "display:block");
            }
            else
            {
                divPopEdu.Visible = false;
                divPopEdu.Attributes.Add("style", "display:none");
            }
            ddlPopEdu.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdoPopMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdoPopMedium.SelectedIndex > -1)
            {
                if (rdoPopMedium.SelectedValue == "2")
                {
                    divPopMedium.Visible = true;
                }
                else
                {
                    divPopMedium.Visible = false;
                }
            }
            rdoPopMedium.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdoPopAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdoPopAvailable.SelectedIndex > -1)
            {
                if (rdoPopAvailable.SelectedValue == "1")
                {
                    divPopQual.Visible = true;
                    //objCommon.FillDropDownList(ddlPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUB_ID=1", "SUB_NAME");
                    ////objCommon.FillDropDownList(ddlPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0", "SUB_NAME");
                    //objCommon.FillDropDownList(ddlPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0", "SUB_NAME");
                    //objCommon.FillDropDownList(ddlPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0", "SUB_NAME");
                    //objCommon.FillDropDownList(ddlPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0", "SUB_NAME");
                }
                else
                {
                    divPopQual.Visible = false;
                }
            }
            rdoPopAvailable.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPopSub5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopSub5.SelectedItem.Text.Equals("Others"))
            {
                divPopSpecify.Visible = true;
                divPopSpecify.Attributes.Add("style", "display:block");
            }
            else
            {
                divPopSpecify.Visible = false;
                divPopSpecify.Attributes.Add("style", "display:none");
            }
            ddlPopSub5.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPopDoYouKnow_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopDoYouKnow.SelectedItem.Text.Equals("Others"))
            {
                divDoyouknow.Visible = true;
                divDoyouknow.Attributes.Add("style", "display:block");
            }
            else
            {
                divDoyouknow.Visible = false;
                divDoyouknow.Attributes.Add("style", "display:none");
            }
            ddlPopDoYouKnow.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlPopState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlPopState.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO > 0 AND ACTIVESTATUS=1 AND STATENO=" + Convert.ToInt32(ddlPopState.SelectedValue), "DISTRICTNAME");
            }
            ddlPopState.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitPopup2_Click(object sender, EventArgs e)
    {
        try
        {
            int AnnualIncome = 0;
            int userNO = Convert.ToInt32(ViewState["userno"]);
            txtotherPopPer1.Text = hdnPer_1.Value;
            txtotherPopPer2.Text = hdnPer_2.Value;
            txtotherPopPer3.Text = hdnPer_3.Value;
            txtotherPopPer4.Text = hdnPer_4.Value;
            txtotherPopPer5.Text = hdnPer_5.Value;
            txtTotObt.Text = hdnTotObt.Value;
            txtTotMax.Text = hdnTotMax.Value;
            txtTotPer.Text = hdnTotPer.Value;
            txtPopCutOff.Text = hdnCutOff.Value;
            if (rdootherPopAvailable.SelectedValue == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Please Select Marks if Available Option.`)", true);
                return;
            }
            string mother_Tongue = string.Empty;
            try
            {

                //if (chkPopNote.Checked == false && chkNote.Enabled == true)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Please check the declaration.`)", true);
                //    return;
                //}
                if (ddlotherPopReligion.SelectedValue == "11" && (txtotherPopOtherReligion.Text == string.Empty || txtotherPopOtherReligion.Text == "" || txtotherPopOtherReligion.Text == null))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                    txtotherPopOtherReligion.Focus();
                    return;
                }
                if (ddlPopTongue.SelectedValue == "46" && (txtPopTongue.Text == string.Empty || txtPopTongue.Text == "" || txtPopTongue.Text == null))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                    txtPopTongue.Focus();
                    return;
                }
                if (ddlotherPopFOcc.SelectedValue == "10" && (txtotherPopFoccOther.Text == string.Empty || txtotherPopFoccOther.Text == "" || txtotherPopFoccOther.Text == null))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                    txtotherPopFoccOther.Focus();
                    return;
                }
                if (ddlotherPopMOcc.SelectedValue == "10" && (txtotherPopMOccOther.Text == string.Empty || txtotherPopMOccOther.Text == "" || txtotherPopMOccOther.Text == null))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                    txtotherPopMOccOther.Focus();
                    return;
                }
                if (ddlotherPopDoYouKnow.SelectedValue == "9" && (txtotherPopDoYouKnow.Text == string.Empty || txtotherPopDoYouKnow.Text == "" || txtotherPopDoYouKnow.Text == null))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "alertMessages();", true);
                    txtotherPopDoYouKnow.Focus();
                    return;
                }

                //int userNO = Convert.ToInt32(ViewState["userno"]);
                //DataSet dsStatus = objUC.Get_Students_Status_Details(Convert.ToInt32(Session["userno"]));
                //if (dsStatus.Tables[0].Rows.Count > 0)
                //{
                //    confirm = dsStatus.Tables[0].Rows[0]["CONFIRM_STATUS"] == DBNull.Value ? string.Empty : dsStatus.Tables[0].Rows[0]["CONFIRM_STATUS"].ToString();
                //    if (confirm == "1")
                //    {
                //        objCommon.DisplayMessage(this.Page, "You Are Already Done Final Submition.Details Cannot be Edited.", this.Page);
                //        pnlDetails.Visible = true;
                //        pnlPayment.Visible = false;
                //        pnlComplete.Visible = false;
                //        return;
                //    }
                //}
                //if (chkContribute.Checked)
                //{
                //    ViewState["contribute"] = 1;
                //}
                //else
                //{
                //    ViewState["contribute"] = 0;
                //}
                ViewState["USER"] = userNO;
                objus.FIRSTNAME = txtotherPopName.Text.ToString().Trim();
                objus.Gender = Convert.ToInt32(rdootherPopGender.SelectedValue);
                objus.DOB = Convert.ToDateTime(txtotherPopDOB.Text.ToString().Trim());
                if (ddlotherPopReligion.SelectedIndex > 0)
                {
                    if (ddlotherPopReligion.SelectedValue == "11")
                    {
                        objus.Religion = Convert.ToInt32(ddlotherPopReligion.SelectedValue);
                        objus.Religion_Other = txtotherPopOtherReligion.Text.ToString().Trim();
                    }
                    else
                    {
                        objus.Religion = Convert.ToInt32(ddlotherPopReligion.SelectedValue);
                    }
                }
                objus.Community = Convert.ToInt32(ddlotherPopCommunity.SelectedValue);
                if (ddlotherPopTongue.SelectedIndex > 0)
                {
                    if (ddlotherPopTongue.SelectedValue == "46")
                    {
                        objus.Mother_Tongue = Convert.ToInt32(ddlotherPopTongue.SelectedValue);
                        mother_Tongue = txtotherPopTongueother.Text.ToString().Trim();
                    }
                    else
                    {
                        objus.Mother_Tongue = Convert.ToInt32(ddlotherPopTongue.SelectedValue);
                    }
                }
                objus.Aadhar = txtotherPopAadhar.Text.ToString().Trim();
                objus.Nationality = Convert.ToInt32(ddlotherPopNationality.SelectedValue);
                objus.Father_Name = txtotherPopFather.Text.ToString().Trim();
                if (ddlotherPopFOcc.SelectedItem.Text == "Others" || ddlotherPopFOcc.SelectedItem.Text == "others")
                {
                    objus.FOccupation_Other = txtotherPopFoccOther.Text.ToString().Trim();
                }
                else
                {
                    objus.FOccupation_Other = "";
                }
                objus.Father_Occupation = Convert.ToInt32(ddlotherPopFOcc.SelectedValue);
                objus.Father_Mobile = txtotherPopFmobile.Text.ToString().Trim();
                objus.Mother_Name = txtotherPopMother.Text.ToString().Trim();
                if (ddlotherPopMOcc.SelectedItem.Text == "Others" || ddlotherPopMOcc.SelectedItem.Text == "others")
                {
                    objus.MOccupation_Other = txtotherPopMOccOther.Text.ToString().Trim();
                }
                else
                {
                    objus.MOccupation_Other = "";
                }
                objus.Mother_Occupation = Convert.ToInt32(ddlotherPopMOcc.SelectedValue);
                objus.Mother_Mobile = txtotherPopMMobile.Text.ToString().Trim();
                objus.Address_1 = txtotherPopAdd1.Text.ToString().Trim();
                objus.Address_2 = txtotherPopAdd2.Text.ToString().Trim();
                objus.Address_3 = txtotherPopAdd3.Text.ToString().Trim();
                objus.City = txtotherPopCity.Text.ToString().Trim();
                objus.State_No = Convert.ToInt32(ddlotherPopState.SelectedValue);
                objus.PinCode = txtotherPopPin.Text.ToString().Trim();
                if (divotherPopEdu.Visible == true)
                {
                    objus.Edu_Info_Other = txtotherPopEduOthers.Text.ToString().Trim();
                }
                objus.Edu_Info = Convert.ToInt32(ddlotherPopEdu.SelectedValue);
                objus.Exm_Reg_12 = txtotherPopExmReg.Text.ToString().Trim();
                objus.School_name = txtotherPopSchool.Text.ToString().Trim();
                objus.Month_No = Convert.ToInt32(ddlotherPopMPass.SelectedValue);
                objus.Month = ddlotherPopMPass.SelectedItem.Text.ToString().Trim();
                objus.Year_No = Convert.ToInt32(ddlotherPopYPass.SelectedValue);
                objus.Year = ddlotherPopYPass.SelectedItem.Text.ToString().Trim();
                if (divotherPopMedium.Visible == true)
                {
                    objus.Medium_Other = txtotherPopMediumOther.Text.ToString().Trim();
                }
                objus.Medium_No = Convert.ToInt32(rdootherPopMedium.SelectedValue);
                objus.Medium = rdootherPopMedium.SelectedItem.Text.ToString().Trim();
                objus.Country_Name = ddlotherPopLast.SelectedItem.Text.ToString().Trim();

                objus.Sub_1 = Convert.ToInt32(ddlotherPopSub1.SelectedValue);
                objus.Marks_Obt_1 = txtotherPopMarksObt1.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMarksObt1.Text.ToString().Trim());
                objus.Max_Marks_1 = txtotherPopMaxMarks1.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMaxMarks1.Text.ToString().Trim());
                objus.Per_1 = hdnPer_1other.Value == string.Empty || hdnPer_1other.Value == null ? 0 : Convert.ToDecimal(hdnPer_1other.Value.ToString().Trim());

                objus.Language = txtotherPopLang.Text == string.Empty ? "" : txtotherPopLang.Text.ToString().Trim();
                objus.Marks_Obt_Lang = txtotherPopMarksObtLang.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMarksObtLang.Text.ToString().Trim());
                objus.Max_Marks_Lang = txtotherPopMaxMarksLang.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMaxMarksLang.Text.ToString().Trim());
                objus.Per_Lang = hdnPerLangother.Value == string.Empty || hdnPerLangother.Value == null ? 0 : Convert.ToDecimal(hdnPerLangother.Value.ToString().Trim());

                objus.Sub_2 = Convert.ToInt32(ddlotherPopSub2.SelectedValue);
                objus.Marks_Obt_2 = txtotherPopMarksObt2.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMarksObt2.Text.ToString().Trim());
                objus.Max_Marks_2 = txtotherPopMaxMarks2.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMaxMarks2.Text.ToString().Trim());
                objus.Per_2 = hdnPer_2other.Value == string.Empty || hdnPer_2other.Value == null ? 0 : Convert.ToDecimal(hdnPer_2other.Value.ToString().Trim());

                objus.Sub_3 = Convert.ToInt32(ddlotherPopSub3.SelectedValue);
                objus.Marks_Obt_3 = txtotherPopMarksObt3.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMarksObt3.Text.ToString().Trim());
                objus.Max_Marks_3 = txtotherPopMaxMarks3.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMaxMarks3.Text.ToString().Trim());
                objus.Per_3 = hdnPer_3other.Value == string.Empty || hdnPer_3other.Value == null ? 0 : Convert.ToDecimal(hdnPer_3other.Value.ToString().Trim());

                objus.Sub_4 = Convert.ToInt32(ddlotherPopSub4.SelectedValue);
                objus.Marks_Obt_4 = txtotherPopMarksObt4.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMarksObt4.Text.ToString().Trim());
                objus.Max_Marks_4 = txtotherPopMaxMarks4.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMaxMarks4.Text.ToString().Trim());
                objus.Per_4 = hdnPer_4other.Value == string.Empty || hdnPer_4other.Value == null ? 0 : Convert.ToDecimal(hdnPer_4other.Value.ToString().Trim());
                txtotherPopPer4.Text = hdnPer_4other.Value;
                objus.Sub_5 = Convert.ToInt32(ddlotherPopSub5.SelectedValue);
                objus.Marks_Obt_5 = txtotherPopMarksObt5.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMarksObt5.Text.ToString().Trim());
                objus.Max_Marks_5 = txtotherPopMaxMarks5.Text == string.Empty ? 0 : Convert.ToDecimal(txtotherPopMaxMarks5.Text.ToString().Trim());
                objus.Per_5 = hdnPer_5other.Value == string.Empty || hdnPer_5other.Value == null ? 0 : Convert.ToDecimal(hdnPer_5other.Value.ToString().Trim());
                txtotherPopPer5.Text = hdnPer_5other.Value;

                //objus.Sub_6 = Convert.ToInt32(ddlSub6.SelectedValue);
                //objus.Marks_Obt_6 = txtMarksObt6.Text == string.Empty ? 0 : Convert.ToDecimal(txtMarksObt6.Text.ToString().Trim());
                //objus.Max_Marks_6 = txtMaxMarks6.Text == string.Empty ? 0 : Convert.ToDecimal(txtMaxMarks6.Text.ToString().Trim());
                //objus.Per_6 = txtPer6.Text == string.Empty ? 0 : Convert.ToDecimal(txtPer6.Text.ToString().Trim());


                objus.Other_Specify = txtotherPopDoYouKnow.Text == string.Empty ? "" : txtotherPopDoYouKnow.Text.ToString().Trim();
                //objus.Cut_off = hdnCutOff.Value == string.Empty || hdnPer_5.Value == null ? 0 : Convert.ToDecimal(hdnCutOff.Value.ToString().Trim());            
                objus.Available = Convert.ToInt32(rdootherPopAvailable.SelectedValue);
                objus.Country_Id = Convert.ToInt32(ddlotherPopLast.SelectedValue);

                AnnualIncome = ddlotherPopIncome.SelectedIndex > 0 ? Convert.ToInt32(ddlotherPopIncome.SelectedValue) : 0;
                //objus.Phase = ddlPhase.SelectedValue;
                string chkSpecialize = objCommon.LookUp("ACD_SUBJECT_ONLINE", "COUNT(1)", "SPECIALIZE=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                if (Convert.ToInt32(chkSpecialize) > 0)
                {
                    objus.Specialize = 0;
                    objus.BRANCHNO = ddlSpec.SelectedIndex > 0 ? Convert.ToInt32(ddlSpec.SelectedValue) : 0;
                }
                else
                {
                    objus.Specialize = ddlSpec.SelectedIndex > 0 ? Convert.ToInt32(ddlSpec.SelectedValue) : 0;
                    objus.BRANCHNO = 0;
                }
                //objus.Nata = txtNATA.Text.ToString().Trim().Equals(string.Empty) ? Convert.ToDecimal("0.00") : Convert.ToDecimal(txtNATA.Text.ToString().Trim());
                objus.Tot_Obt = hdnTotObt.Value == string.Empty || hdnTotObt.Value == null ? 0 : Convert.ToDecimal(hdnTotObt.Value.ToString().Trim());
                objus.Tot_Max = hdnTotMax.Value == string.Empty || hdnTotMax.Value == null ? 0 : Convert.ToDecimal(hdnTotMax.Value.ToString().Trim());
                objus.Tot_Per = hdnTotPer.Value == string.Empty || hdnTotPer.Value == null ? 0 : Convert.ToDecimal(hdnTotPer.Value.ToString().Trim());
                txtTotObt.Text = hdnTotObt.Value;
                txtTotMax.Text = hdnTotMax.Value;
                txtTotPer.Text = hdnTotPer.Value;


                //Added by Nikhil L. on 08/12/2022 to add other sub columns
                //objus.OtherSub2 = txtOtherSub2.Text.ToString() == string.Empty ? "NA" : txtOtherSub2.Text.ToString();
                //objus.OtherSub3 = txtOtherSub3.Text.ToString() == string.Empty ? "NA" : txtOtherSub3.Text.ToString();
                //objus.OtherSub4 = txtOtherSub4.Text.ToString() == string.Empty ? "NA" : txtOtherSub4.Text.ToString();
                if (ddlotherPopDoYouKnow.SelectedIndex > 0)
                {
                    if (ddlotherPopDoYouKnow.SelectedValue == "9")
                    {
                        objus.InstituteADV = Convert.ToInt32(ddlotherPopDoYouKnow.SelectedValue);
                        objus.InstituteADV_Other = txtotherPopDoYouKnow.Text.Trim();
                    }
                    else
                    {
                        objus.InstituteADV = Convert.ToInt32(ddlotherPopDoYouKnow.SelectedValue);
                    }
                }
                objus.District = ddlotherPopDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlotherPopDistrict.SelectedValue) : 0;
                bool isPhotoUploaded = false;
                if (fuotherPhotoUpload.HasFile || ViewState["PHOTO"] != null)
                {
                    isPhotoUploaded = true;
                }
                if (!isPhotoUploaded)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Photo.", this.Page);
                    return;
                }
                string photo = string.Empty;
                if (fuotherPhotoUpload.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(fuotherPhotoUpload.PostedFile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".JPG" || ext == ".JPEG")
                    {
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload file with .jpg,.jpeg format only.", this.Page);
                        return;
                    }
                    if (fuotherPhotoUpload.PostedFile.ContentLength < 500000)
                    {
                        byte[] resizephoto = ResizePhoto(fuotherPhotoUpload);
                        if (resizephoto.LongLength >= 500000)
                        {
                            objCommon.DisplayMessage(this, "File size should be less than or equal to 500 KB.", this.Page);
                            return;
                        }
                        else
                        {
                            objus.PHOTO = resizephoto;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "File size should be less than or equal to 500 KB.", this.Page);
                        return;
                    }
                    photo = "Photo";
                }
                else
                {
                    if (ViewState["PHOTO"] == null)
                    {
                    }
                    else
                    {
                        objus.PHOTO = null;
                        objus.PHOTO = ViewState["PHOTO"] as byte[];
                    }
                }
            }
            catch (Exception ex)
            {
                objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objFetchData.Update_RegistrationDetails(objus, Convert.ToInt32(ViewState["USER"]), mother_Tongue, Convert.ToInt32(ViewState["contribute"]), AnnualIncome);
            //CustomStatus csGenerate = (CustomStatus)objUC.GenerateSerialNo_OnlineAdmUGPG(Convert.ToInt32(ViewState["USER"]), Convert.ToInt32(ViewState["degree"]), Convert.ToInt32(ViewState["admissionBatch"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //GetUserDetails();
                //SendMailForAppId();
                objCommon.DisplayMessage(this.Page, "Registration Details Saved Successfully.Your Application ID is " + ViewState["appID"].ToString().Trim() + ",Please Check Your Email For Application ID.Please ensure application details are correct before making payment.", this.Page);
                //CheckStatus();
                //GetUserDetailsByUserno();
                //GetAllDetails();

                //GetPayDetails();
                //pnlPayment.Visible = false;
                //pnlDetails.Visible = false;
                //pnlDocument.Visible = true;
                //pnlComplete.Visible = false;
                //checkMand_Doc();

                return;
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.Page, "Registration Details Updated Successfully.", this.Page);
                //GetUserDetailsByUserno();
                //GetAllDetails();
                //GetPayDetails();
                //CheckStatus();
                //pnlPayment.Visible = false;
                //pnlDetails.Visible = false;
                //pnlDocument.Visible = true;
                //pnlComplete.Visible = false;
                //checkMand_Doc();
                return;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ddlotherPopReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopReligion.SelectedValue == "11")
            {
                divotherPopReligion.Visible = true;
                divotherPopReligion.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherPopReligion.Visible = false;
                divotherPopReligion.Attributes.Add("style", "display:none");
            }
            ddlotherPopReligion.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlotherPopTongue_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopTongue.SelectedValue == "46")
            {
                divotherPopTongue.Visible = true;
                divotherPopTongue.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherPopTongue.Visible = false;
                divotherPopTongue.Attributes.Add("style", "display:none");
            }
            ddlotherPopTongue.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnpop2Close_Click(object sender, EventArgs e)
    {
        Mp2ForOthers.Hide();
    }
    protected void ddlotherPopFOcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopFOcc.SelectedValue == "10")
            {
                divotherPopFocc.Visible = true;
                divotherPopFocc.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherPopFocc.Visible = false;
                divotherPopFocc.Attributes.Add("style", "display:none");
            }
            ddlotherPopFOcc.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlotherPopMOcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopMOcc.SelectedValue == "10")
            {
                divotherMOccupation.Visible = true;
                divMOccupation.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherMOccupation.Visible = false;
                divotherMOccupation.Attributes.Add("style", "display:none");
            }
            ddlotherPopMOcc.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlotherPopEdu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopEdu.SelectedValue == "4")
            {
                divotherPopEdu.Visible = true;
                divotherPopEdu.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherPopEdu.Visible = false;
                divotherPopEdu.Attributes.Add("style", "display:none");
            }
            ddlotherPopEdu.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdootherPopMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdootherPopMedium.SelectedIndex > -1)
            {
                if (rdootherPopMedium.SelectedValue == "2")
                {
                    divotherPopMedium.Visible = true;
                }
                else
                {
                    divotherPopMedium.Visible = false;
                }
            }
            rdootherPopMedium.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdootherPopAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdootherPopAvailable.SelectedIndex > -1)
            {
                if (rdootherPopAvailable.SelectedValue == "1")
                {
                    divotherPopQual.Visible = true;
                }
                else
                {
                    divotherPopQual.Visible = false;
                }
            }
            //if (rdootherPopAvailable.SelectedIndex > -1)
            //{
            //    if (rdootherPopAvailable.SelectedValue == "1")
            //    {
            //        if (ddlSpec.SelectedIndex > 0)
            //        {
            //            divotherPopQual.Visible = true;
            //            if (ddlSpec.SelectedIndex > 0)
            //            {
            //                chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)=" + Convert.ToInt32(ddlSpec.SelectedValue) + " AND BRANCHNO IS NOT NULL AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
            //            }
            //            else
            //            {
            //                chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)!=0 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
            //            }
            //            if (chkSpecial.ToString() != (string.Empty))
            //            {
            //                branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
            //            }
            //            if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
            //            {
            //                objCommon.FillDropDownList(ddlotherPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=1 AND ACTIVE_STATUS=1 AND IS_COMPULSORY=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
            //            }
            //            else
            //            {
            //                objCommon.FillDropDownList(ddlotherPopSub1, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=1 AND ACTIVE_STATUS=1 AND IS_COMPULSORY=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
            //            }
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`Please select specialization.`)", true);
            //            rdootherPopAvailable.SelectedIndex = -1;
            //            ddlSpec.Focus();
            //            return;
            //        }

            //    }
            //    else if (rdootherPopAvailable.SelectedValue == "0")
            //    {
            //        divotherPopQual.Visible = false;
            //    }

            //}
            rdootherPopAvailable.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlotherPopSub5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopSub5.SelectedItem.Text.Equals("Others"))
            {
                divotherPopSpecify.Visible = true;
                divotherPopSpecify.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherPopSpecify.Visible = false;
                divotherPopSpecify.Attributes.Add("style", "display:none");
            }
            ddlotherPopSub5.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlotherPopDoYouKnow_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopDoYouKnow.SelectedItem.Text.Equals("Others"))
            {
                divotherDoyouknow.Visible = true;
                divotherDoyouknow.Attributes.Add("style", "display:block");
            }
            else
            {
                divotherDoyouknow.Visible = false;
                divotherDoyouknow.Attributes.Add("style", "display:none");
            }
            ddlotherPopDoYouKnow.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlotherPopState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlotherPopState.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlotherPopDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO > 0 AND ACTIVESTATUS=1 AND STATENO=" + Convert.ToInt32(ddlotherPopState.SelectedValue), "DISTRICTNAME");
            }
            ddlotherPopState.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSpec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlotherPopSub1.Items.Clear();
            ddlotherPopSub2.Items.Clear();
            ddlotherPopSub3.Items.Clear();
            ddlotherPopSub4.Items.Clear();
            ddlotherPopSub5.Items.Clear();


            ddlotherPopSub1.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub2.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub3.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub4.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub5.Items.Add(new ListItem("Please Select", "0"));
            rdootherPopAvailable.SelectedIndex = -1;
            divotherPopQual.Visible = false;
            ddlSpec.Focus();
        }
        catch (Exception)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return; ;
        }
    }
    protected void ddlotherPopSub1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlotherPopSub1.SelectedIndex > 0)
            {
                ddlotherPopSub2.Items.Clear();
                ddlotherPopSub3.Items.Clear();
                ddlotherPopSub4.Items.Clear();
                ddlotherPopSub5.Items.Clear();


                ddlotherPopSub2.Items.Add(new ListItem("Please Select", "0"));
                ddlotherPopSub3.Items.Add(new ListItem("Please Select", "0"));
                ddlotherPopSub4.Items.Add(new ListItem("Please Select", "0"));
                ddlotherPopSub5.Items.Add(new ListItem("Please Select", "0"));
                if (ddlSpec.SelectedIndex > 0)
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)=" + Convert.ToInt32(ddlSpec.SelectedValue) + " AND BRANCHNO IS NOT NULL AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                else
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)!=0 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                if (chkSpecial.ToString() != (string.Empty))
                {
                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                }
                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                {
                    objCommon.FillDropDownList(ddlotherPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=2 AND ACTIVE_STATUS=1 AND ISNULL(IS_OTHERS,0)=0 AND SUB_ID!=" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlotherPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=2 AND ACTIVE_STATUS=1 AND ISNULL(IS_OTHERS,0)=0 AND SUB_ID!=" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                }
                ddlotherPopSub1.Focus();
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return;
        }
    }
    protected void ddlotherPopSub2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //txtCutOff.Text = string.Empty;
            //hdnCutOff.Value = txtCutOff.Text.ToString().Trim();
            txtTotObt.Text = string.Empty;
            txtTotMax.Text = string.Empty;
            txtTotPer.Text = string.Empty;

            hdnTotObt.Value = txtTotObt.Text.ToString().Trim();
            hdnTotMax.Value = txtTotMax.Text.ToString().Trim();
            hdnTotPer.Value = txtTotPer.Text.ToString().Trim();

            ddlotherPopSub3.Items.Clear();
            ddlotherPopSub4.Items.Clear();
            ddlotherPopSub5.Items.Clear();
            ddlotherPopSub3.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub4.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub5.Items.Add(new ListItem("Please Select", "0"));
            if (ddlotherPopSub2.SelectedIndex > 0)
            {
                if (ddlSpec.SelectedIndex > 0)
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)=" + Convert.ToInt32(ddlSpec.SelectedValue) + " AND BRANCHNO IS NOT NULL AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                else
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)!=0 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                if (chkSpecial.ToString() != (string.Empty))
                {
                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                }
                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                {
                    objCommon.FillDropDownList(ddlotherPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=3 AND ISNULL(IS_OTHERS,0)=0 AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlotherPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=3 AND ISNULL(IS_OTHERS,0)=0 AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                }
                ddlotherPopSub3.SelectedIndex = 0;
                if (ddlotherPopSub2.SelectedItem.Text.Equals("Others"))
                {
                    divOtherSub2.Visible = true;
                }
                else
                {
                    divOtherSub2.Visible = false;
                }
                ddlotherPopSub2.Focus();
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return;
        }
    }
    protected void ddlotherPopSub3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotObt.Text = string.Empty;
            txtTotMax.Text = string.Empty;
            txtTotPer.Text = string.Empty;

            hdnTotObt.Value = txtTotObt.Text.ToString().Trim();
            hdnTotMax.Value = txtTotMax.Text.ToString().Trim();
            hdnTotPer.Value = txtTotPer.Text.ToString().Trim();
            ddlotherPopSub4.Items.Clear();
            ddlotherPopSub5.Items.Clear();

            ddlotherPopSub4.Items.Add(new ListItem("Please Select", "0"));
            ddlotherPopSub5.Items.Add(new ListItem("Please Select", "0"));
            if (ddlotherPopSub3.SelectedIndex > 0)
            {
                ddlotherPopSub4.SelectedIndex = 0;

                if (ddlSpec.SelectedIndex > 0)
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)=" + Convert.ToInt32(ddlSpec.SelectedValue) + " AND BRANCHNO IS NOT NULL AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                else
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)!=0 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                if (chkSpecial.ToString() != (string.Empty))
                {
                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                }
                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                {
                    objCommon.FillDropDownList(ddlotherPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=4  AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND BRANCHNO=" + branch, "SUB_NAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlotherPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=4  AND ACTIVE_STATUS=1 AND SUB_ID NOT IN(" + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + ") AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue), "SUB_NAME");
                }

                DataSet dsSubStatus = objFetchData.GetSubjectStatus(Convert.ToInt32(ddlotherPopSub3.SelectedValue));
                if (dsSubStatus.Tables[0].Rows.Count > 0)
                {
                    hdnDDL3other.Value = dsSubStatus.Tables[0].Rows[0]["IS_CUTOFF"].ToString();
                }
                if (ddlotherPopSub3.SelectedItem.Text.Equals("Others"))
                {
                    divOtherSub3.Visible = true;
                }
                else
                {
                    divOtherSub3.Visible = false;
                }
            }
            ddlotherPopSub3.Focus();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "HideUnhideQual();", true);

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return;
        }
    }
    protected void ddlotherPopSub4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotObt.Text = string.Empty;
            txtTotMax.Text = string.Empty;
            txtTotPer.Text = string.Empty;

            hdnTotObt.Value = txtTotObt.Text.ToString().Trim();
            hdnTotMax.Value = txtTotMax.Text.ToString().Trim();
            hdnTotPer.Value = txtTotPer.Text.ToString().Trim();

            ddlotherPopSub5.Items.Clear();
            ddlotherPopSub5.Items.Add(new ListItem("Please Select", "0"));
            if (ddlotherPopSub4.SelectedIndex > 0)
            {
                ddlotherPopSub5.SelectedIndex = 0;
                if (ddlSpec.SelectedIndex > 0)
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)=" + Convert.ToInt32(ddlSpec.SelectedValue) + " AND BRANCHNO IS NOT NULL AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                else
                {
                    chkSpecial = objCommon.LookUp("ACD_SUBJECT_ONLINE", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO) BRANCH", "ISNULL(BRANCHNO,0)!=0 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue));
                }
                if (chkSpecial.ToString() != (string.Empty))
                {
                    branch = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME='" + chkSpecial + "'"));
                }
                if (chkSpecial.ToUpper() == ddlSpec.SelectedItem.Text.ToString().ToUpper())
                {
                    objCommon.FillDropDownList(ddlotherPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=5 AND IS_OTHERS=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + "AND BRANCHNO=" + branch + " AND SUB_ID NOT IN (" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub4.SelectedValue) + ")", "SUB_NAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlotherPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND SUBJECT_NO=5 AND IS_OTHERS=1 AND DEGREENO=" + Convert.ToInt32(ddlProgramme.SelectedValue) + " AND SUB_ID NOT IN (" + Convert.ToInt32(ddlotherPopSub1.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub2.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlotherPopSub4.SelectedValue) + ")", "SUB_NAME");
                }

                DataSet dsSubStatus = objFetchData.GetSubjectStatus(Convert.ToInt32(ddlotherPopSub4.SelectedValue));
                if (dsSubStatus.Tables[0].Rows.Count > 0)
                {
                    hdnDDL4other.Value = dsSubStatus.Tables[0].Rows[0]["IS_CUTOFF"].ToString();
                }
                if (ddlotherPopSub4.SelectedItem.Text.Equals("Others"))
                {
                    divOtherSub4.Visible = true;
                }
                else
                {
                    divOtherSub4.Visible = false;
                }
            }
            ddlotherPopSub4.Focus();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "HideUnhideQual();", true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return;
        }
    }
    protected void ddlProgrammeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgrammeType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, " ACD_DEGREE  D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH ACDB ON(ACDB.DEGREENO=D.DEGREENO) ", "DISTINCT ACDB.DEGREENO", "DEGREENAME", "ACDB.DEGREENO > 0 AND UGPGOT=" + Convert.ToInt32(ddlProgrammeType.SelectedValue), "DEGREENAME");
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ddlPopSub1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPopSub1.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlPopSub2, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND IS_COMPULSORY=1 AND SUB_ID IN(2,5) AND DEGREENO=7", "SUB_NAME");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlPopSub2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPopSub2.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlPopSub3, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND DEGREENO=7 AND IS_COMPULSORY=1  AND SUB_ID IN(3) AND SUB_ID NOT IN(" + Convert.ToInt32(ddlPopSub2.SelectedValue) + ")", "SUB_NAME");
        }
    }
    protected void ddlPopSub3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPopSub3.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlPopSub4, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND DEGREENO=7 AND IS_COMPULSORY=1  AND SUB_ID IN(4) AND SUB_ID NOT IN(" + Convert.ToInt32(ddlPopSub3.SelectedValue) + "," + Convert.ToInt32(ddlPopSub2.SelectedValue) + ")", "SUB_NAME");
        }
    }
    protected void ddlPopSub4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPopSub4.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlPopSub5, "ACD_SUBJECT_ONLINE", "SUB_ID", "SUB_NAME", "SUB_ID > 0 AND IS_OTHERS=1 AND SUB_ID<>1 AND DEGREENO=7", "SUB_NAME");
        }
    }
    protected void btnDownloadApp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = sender as ImageButton;
            string user = imgButton.CommandArgument;
            string SP_name = string.Empty; string SP_call = string.Empty; string SP_value = string.Empty;
            DataSet dsCheckType = null;
            SP_name = "PKG_ACD_OA_GET_USERS_DEGREE_TYPE";
            SP_call = "@P_USERNO";
            SP_value = "" + user + "";
            string applicationId = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Convert.ToInt32(user));

            if (applicationId == string.Empty)
            {
                applicationId = "-" + applicationId;
            }
            dsCheckType = objCommon.DynamicSPCall_Select(SP_name, SP_call, SP_value);
            if (dsCheckType.Tables[0].Rows.Count > 0)
            {
                //Added by Nikhil L. to download the reports as per degree type.
                if (dsCheckType.Tables[0].Rows[0]["UGPGOT"].ToString() == "1" && dsCheckType.Tables[0].Rows[0]["DEGREENO"].ToString() == "7")
                {
                    ShowReport("pdf", applicationId, "rptPreviewForm_Crescent.rpt", Convert.ToInt32(user));
                }
                else if (dsCheckType.Tables[0].Rows[0]["UGPGOT"].ToString() == "1" && dsCheckType.Tables[0].Rows[0]["DEGREENO"].ToString() != "7")
                {
                    ShowReport("pdf", "Preview_Form", "rptPreviewForm_Crescent_UG.rpt", Convert.ToInt32(user));
                }
                else if (dsCheckType.Tables[0].Rows[0]["UGPGOT"].ToString() == "2")
                {
                    ShowReport("pdf", "Preview_Form", "rptPreviewForm_Crescent_PG.rpt", Convert.ToInt32(user));
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void ShowReport(string exporttype, string reportTitle, string rptFileName, int userno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            string college_Code = string.Empty;
            college_Code = objCommon.LookUp("REFF", "College_code", "OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()));
            string recoon = string.Empty;
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + exporttype;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            url += "&param=@P_USERNO=" + Convert.ToInt32(userno) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(college_Code);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updCollege1, this.updCollege1.GetType(), "controlJSScript", sb.ToString(), true);            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Details.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    static private Stream ShowGeneralExportReportForMail(string path, string paramString)
    {
        Stream oStream;
        CrystalDecisions.CrystalReports.Engine.ReportDocument customReport;
        customReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptPreviewForm_Crescent.rpt");
        //System.Web.HttpContext.Current.Server.MapPath("~/" + path.Replace(",", "\\"));
        //if (customReport != null)
        //{
        //    customReport.Close();
        //    customReport.Dispose();
        //}
        customReport.Load(reportPath);
        /// Assign parameters to report document
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                /// Each array in val contains string in following format.
                /// ParamName=Value*ReportName
                /// Here report name is the name of report from which this parameter belongs.
                /// if parameter belongs to main report then report name is equal to MainRpt
                /// else if parameter belongs to subreport then report name is equal to name of subreport.
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');



                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";



                paramName = val[i].Substring(0, indexOfEql);



                /// if report name is not passed with the parameter(means indexOfSlash will be -1) then
                /// handle the scenario to work properly.
                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {



                    value = val[i].Substring(indexOfEql + 1);
                }



                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }



        /// set login details & db details for report document
        ConfigureCrystalReports(customReport);



        /// set login details & db details for each subreport
        /// inside main report document.
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }



        //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        oStream = (Stream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);

        customReport.Close();
        customReport.Dispose();
        GC.Collect();

        GC.WaitForPendingFinalizers();

        return oStream;
    }
    static private void ConfigureCrystalReports(CrystalDecisions.CrystalReports.Engine.ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        CrystalDecisions.Shared.ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
    protected void btnBtech_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsExcel = null; string spName = string.Empty; string spCall = string.Empty; string spValue = string.Empty;
            int admBatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            int status = Convert.ToInt32(rdoStatus.SelectedValue);
            //int degreeNo = 0;
            spName = "PKG_ACD_GET_DUMP_DATA_BTECH_EXCEL";
            spCall = "@P_ADMBATCH,@P_STATUS";
            spValue = "" + admBatch + "," + status;
            dsExcel = objCommon.DynamicSPCall_Select(spName, spCall, spValue);
            if (dsExcel.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsExcel.Tables)
                    {
                        wb.Worksheets.Add(dt);
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=BtechDumpData.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnZip_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadZipFile" + "/";
            directoryPath = Server.MapPath(directoryName);
            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            ImageButton lnkbtn = sender as ImageButton;
            //string filename = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            string img = lnkbtn.CommandArgument.ToString();
            int userno = Convert.ToInt32(lnkbtn.ToolTip.ToString());
            int documentNo = 0;
            documentNo = Convert.ToInt32(lnkbtn.CommandName.ToString());
            var ImageName = img;
            if (img != null || img != "")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                string filePath = directoryPath + "\\" + ImageName;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                //return;
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string spName = string.Empty; string spParamaters = string.Empty; string spValue = string.Empty;
                DataSet dsZipUpdate = null;
                spName = "PKG_ACD_OA_UPDATE_ZIP_PATH";
                spParamaters = "@P_USERNO,@P_ZIP_PATH,@P_DOCUMENT_NO";
                spValue = "" + userno + "," + directoryPath + "," + documentNo + "";
                dsZipUpdate = objCommon.DynamicSPCall_Select(spName, spParamaters, spValue);
                if (dsZipUpdate.Tables[0].Rows.Count > 0)
                {
                    if (dsZipUpdate.Tables[0].Rows[0]["OUTPTUT"].ToString().Equals("1"))
                    {
                        string compressedFileName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME name", "USERNO=" + userno);
                        DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "ZIP_PATH +'\\'+DOC_FILENAME PATH", "DOC_FILENAME", "USERNO=" + userno, "");
                        int fileCount = 0;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string zipPath = dr["PATH"].ToString();
                            if (System.IO.File.Exists(filePath))
                            {
                                fileCount++;
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + compressedFileName.Replace(" ", "_") + ".zip");
                                Response.ContentType = "application/zip";
                            }
                        }

                        if (fileCount > 0)
                        {
                            using (var zipStream = new ZipOutputStream(Response.OutputStream))
                            {

                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    string fileP = dr["PATH"].ToString();
                                    string FILENAME = dr["DOC_FILENAME"].ToString();

                                    if (System.IO.File.Exists(filePath))
                                    {
                                        fileCount++;
                                        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                                        Stream fs = File.OpenRead(filePath);
                                        ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(FILENAME));
                                        zipEntry.Size = fs.Length;
                                        zipStream.PutNextEntry(zipEntry);
                                        int count = fs.Read(fileBytes, 0, fileBytes.Length);
                                        while (count > 0)
                                        {
                                            zipStream.Write(fileBytes, 0, count);
                                            count = fs.Read(fileBytes, 0, fileBytes.Length);
                                            if (!Response.IsClientConnected)
                                            {
                                                break;
                                            }
                                            Response.Flush();
                                        }
                                        fs.Close();
                                    }
                                }
                                zipStream.Close();
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`File not found.`)", true);
                        return;
                    }
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`File not found.`)", true);
                return;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    protected void btnUGPG_Click(object sender, EventArgs e)
    {
        try
        {
            //return;
            DataSet dsExcel = null; string spName = string.Empty; string spCall = string.Empty; string spValue = string.Empty;
            int admBatch = ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
            int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            int ugpgot = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
            //int degreeNo = 0;
            spName = "PKG_ACD_GET_DUMP_DATA_UG_PG_EXCEL";
            spCall = "@P_ADMBATCH,@P_DEGREENO,@P_UGPGOT";
            spValue = "" + admBatch + "," + degreeNo + "," + ugpgot + "";
            dsExcel = objCommon.DynamicSPCall_Select(spName, spCall, spValue);
            if (dsExcel.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsExcel.Tables)
                    {
                        wb.Worksheets.Add(dt);
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + ddlProgrammeType.SelectedItem.Text + "_DumpData.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
