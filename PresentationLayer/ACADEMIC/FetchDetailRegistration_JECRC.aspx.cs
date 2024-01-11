using System;
using System.Data;
using System.Web.UI;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using ClosedXML.Excel;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using EASendMail;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers.Mail;
using mastersofterp_MAKAUAT;
using BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_FetchDetailRegistration_JECRC : System.Web.UI.Page
{
    Common objCommon = new Common();
    FetchDataController objFetchData = new FetchDataController();
    DocumentController objdocContr = new DocumentController();
    Document objdocument = new Document();
    User objus = new User();
    NewUser objnu = new NewUser();
    //SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    SendEmailCommonV2 objSendEmail = new SendEmailCommonV2(); //Object Creation
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                //objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH  A INNER JOIN ACD_PHD_REGISTRATION PR ON(A.BATCHNO=PR.ADMBATCH)", "DISTINCT BATCHNO", "BATCHNAME", "", "BATCHNAME DESC");
                ddlAdmbatch.SelectedIndex = 1;
                objCommon.FillDropDownList(ddlDegree, " ACD_DEGREE ", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FetchDetailRegistration_JECRC.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FetchDetailRegistration_JECRC.aspx");
        }
    }
    protected void btnShowStud_Click(object sender, EventArgs e)
    {
        try
        {
            int programmeType = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
            int degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;

            DataSet ds = objFetchData.GetApplicantUserListNew_JECRC(Convert.ToInt32(ddlAdmbatch.SelectedValue), programmeType, Convert.ToInt32(rdoStatus.SelectedValue), degree);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                //pnlStudent.Visible = true;
                lvStudent.Visible = true;
                foreach (ListViewDataItem dataitem in lvStudent.Items)
                {
                    Label lblAppStatus = dataitem.FindControl("lblAppStatus") as Label;

                    if (lblAppStatus.Text == "FEES PAID" || lblAppStatus.Text == "PENDING")
                    {
                        //Button hdn = (Button)e.Item.FindControl("btnSendOffer");
                        Button hdn = dataitem.FindControl("btnSendOffer") as Button;
                        hdn.Visible = false;
                    }

                }
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
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.btnShowStud_Click()-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.btnCompleteDetail_Click()-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.btnPhd_Click-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.btnSummaryReport_Click-> " + ex.Message + " " + ex.StackTrace);
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
            EditEdudetail.Visible = false;
            LinkButton lnkUserno = sender as LinkButton;
            string lblAppStatus = lnkUserno.ToolTip; 
            DataSet dsPop = new DataSet();
            if (lblAppStatus == "FEES PAID")
            {
                btnSubmitPopup.Enabled = false;
            }
            else 
            {
                btnSubmitPopup.Enabled = true;
            }
            dsPop = objFetchData.GetRecordByUANo(lnkUserno.CommandArgument);
            if (dsPop.Tables[0].Rows.Count > 0)
            {
                Mp1.Show();

                ViewState["USERNO"] = dsPop.Tables[0].Rows[0]["USERNO"].ToString();
                ViewState["USERNAME"] = dsPop.Tables[0].Rows[0]["USERNAME"].ToString();
                string FirstName = dsPop.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                string LastName = dsPop.Tables[0].Rows[0]["LASTNAME"].ToString();
                txtFullName.Text = FirstName + " " + LastName;
                txtDateOfBirth.Text = dsPop.Tables[0].Rows[0]["DOB"].ToString();
                txtEmail.Text = dsPop.Tables[0].Rows[0]["EMAILID"].ToString();
                //rdoGender.SelectedValue = dsPop.Tables[0].Rows[0]["GENDER_MAIN"].ToString().Trim();
                string gender = objCommon.LookUp("ACD_USER_REGISTRATION", "GENDER", "USERNO =" + (ViewState["USERNO"].ToString()));
                if (gender.Trim().Equals("Male") || gender.Trim().Equals("M"))
                {
                    rdoGender.SelectedValue = "1";
                }
                else if (gender.TrimEnd().Equals("Female") || gender.Trim().Equals("F"))
                {
                    rdoGender.SelectedValue = "2";
                }
                else if (gender.TrimEnd().Equals("Transgender") || gender.Trim().Equals("T"))
                {
                    rdoGender.SelectedValue = "3";
                }
                txtMob.Text = dsPop.Tables[0].Rows[0]["MOBILENO"].ToString();

                //Session["Studid"] = lnkUserno.ToolTip;

            }
            string userno = ViewState["USERNO"].ToString();
            DataSet ds = objFetchData.GetRecordForPersonalDetails(userno);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //txtFullName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                //txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                txtAlternateEmailId.Text = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();

                if (ds.Tables[0].Rows[0]["COUNTRY_CODE"] == DBNull.Value)
                {
                    txtmobilecode.Text = "+91";
                }
                else
                {
                    txtmobilecode.Text = ds.Tables[0].Rows[0]["COUNTRY_CODE"].ToString();
                }
                objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0 AND ACTIVESTATUS=1", "BLOODGRPNO DESC");
                objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0  AND ACTIVESTATUS=1 ", "RELIGIONNO ASC");
                objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0  AND ACTIVESTATUS=1", "CATEGORYNO Asc");
                objCommon.FillDropDownList(ddlStateDomicile, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0 AND ACTIVESTATUS=1", "STATENAME");
                objCommon.FillDropDownList(ddlMOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0 AND ACTIVESTATUS=1", "OCCUPATION");
                objCommon.FillDropDownList(ddlFOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0 AND OCCUPATION<>4 AND ACTIVESTATUS=1", "OCCUPATION");
                objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0 AND ACTIVESTATUS=1", "NATIONALITYNO");
                ddlNationality.SelectedIndex = 1;
                if (ds.Tables[0].Rows[0]["RELIGIONNO"].ToString() == "0")
                {
                    ddlReligion.SelectedValue = "1";
                }
                else
                {
                    ddlReligion.SelectedValue = ds.Tables[0].Rows[0]["RELIGIONNO"].ToString();
                }

                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR_ONLINE_ADM", "YEARNO", "YEARNAME", "YEARNO > 0", "YEARNAME");

                string NTEXT = ds.Tables[0].Rows[0]["NATIONALITY"].ToString();
                string SN = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY ", "NATIONALITY='" + NTEXT + "'");
                ddlStateDomicile.SelectedValue = ds.Tables[0].Rows[0]["STATE_DOMICILE"].ToString();
                ddlBloodGroup.SelectedValue = ds.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                txtIdentificationMark.Text = ds.Tables[0].Rows[0]["IDENTITY_MARK"].ToString();
                txtAdhaarNo.Text = ds.Tables[0].Rows[0]["ADHAARNO"].ToString();
                if (ds.Tables[0].Rows[0]["MARITALSTATUS"].ToString() == "0")
                {
                    ddlMarital.SelectedValue = "2";
                }
                else
                {
                    ddlMarital.SelectedValue = ds.Tables[0].Rows[0]["MARITALSTATUS"].ToString();
                }
                string Diffable = string.Empty;
                string Sports = string.Empty;
                if (ds.Tables[0].Rows[0]["DIFFERENTLY_ABLED"].ToString() == "0")
                {
                    Diffable = ddlDiffAbility.SelectedValue = "2";
                }
                else
                {
                    Diffable = ddlDiffAbility.SelectedValue = ds.Tables[0].Rows[0]["DIFFERENTLY_ABLED"].ToString();
                }
                if (Diffable == "1")
                {
                    Abilility.Visible = true;
                    Abilility1.Visible = true;
                    txtNatureOfDisability.Text = ds.Tables[0].Rows[0]["NATURE_DISABILITY"].ToString();
                    txtPercentageOfDisability.Text = ds.Tables[0].Rows[0]["PERCENTAGE_DISABILITY"].ToString();
                }
                else
                {
                    txtNatureOfDisability.Text = string.Empty;
                    txtPercentageOfDisability.Text = string.Empty;
                    Abilility.Visible = false;
                    Abilility1.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["SPORTS_PERSON"].ToString() == "0")
                {
                    Sports = ddlSports.SelectedValue = "2";
                }
                else
                {
                    Sports = ddlSports.SelectedValue = ds.Tables[0].Rows[0]["SPORTS_PERSON"].ToString();
                }
                if (Sports == "1")
                {
                    LevelOfSports.Visible = true;
                    ddlLevelOfSports.Enabled = true;
                    ddlLevelOfSports.SelectedValue = ds.Tables[0].Rows[0]["SPORTS_REPRESENTED"].ToString();
                    sportsName.Visible = true;
                    lblFileName.Visible = true;
                    sportsDoc.Visible = true;
                    txtSportsName.Text = ds.Tables[0].Rows[0]["SPORTS_NAME"].ToString();
                    lblFileName.Text = ds.Tables[0].Rows[0]["SPORTS_DOC"].ToString();
                    lblFileName.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LevelOfSports.Visible = false;
                    ddlLevelOfSports.SelectedIndex = 0;
                    ddlLevelOfSports.Enabled = false;
                    sportsName.Visible = false;
                    lblFileName.Visible = false;
                    sportsDoc.Visible = false;
                    txtSportsName.Text = string.Empty;
                    lblFileName.Text = string.Empty;
                }
                string host = ds.Tables[0].Rows[0]["HOST_TRANS"].ToString().TrimEnd();
                if (host == string.Empty)
                {
                    ddlHost.SelectedValue = "0";
                }
                else
                {
                    ddlHost.SelectedValue = host.ToString();
                }
                if (ds.Tables[0].Rows[0]["F_TELNUMBER"].ToString() == "0")
                {
                    txtFTelNo.Text = string.Empty;
                }
                else
                {
                    txtFTelNo.Text = ds.Tables[0].Rows[0]["F_TELNUMBER"].ToString();
                }

                txtFMobile.Text = ds.Tables[0].Rows[0]["F_MOBILENO"].ToString();
                string Focc = ds.Tables[0].Rows[0]["F_OCCUPATION"].ToString();
                if (Focc == string.Empty)
                {
                    ddlFOccupation.SelectedValue = "0";
                }
                else
                {
                    ddlFOccupation.SelectedValue = Focc.ToString();
                }
                txtFDesignation.Text = ds.Tables[0].Rows[0]["F_DESIGNATION"].ToString();
                txtFEmail.Text = ds.Tables[0].Rows[0]["FEMAILADDRESS"].ToString();
                if (ds.Tables[0].Rows[0]["M_TELNUMBER"].ToString() == "0")
                {
                    txtMTelNo.Text = string.Empty;
                }
                else
                {
                    txtMTelNo.Text = ds.Tables[0].Rows[0]["M_TELNUMBER"].ToString();
                }
                txtMMobile.Text = ds.Tables[0].Rows[0]["M_MOBILENO"].ToString();
                string Mocc = ds.Tables[0].Rows[0]["MOCCUPATION"].ToString();
                if (Mocc == string.Empty)
                {
                    ddlMOccupation.SelectedValue = "0";
                }
                else
                {
                    ddlMOccupation.SelectedValue = Mocc.ToString();
                }
                txtMDesignation.Text = ds.Tables[0].Rows[0]["M_DESIGNATION"].ToString();
                txtMEmail.Text = ds.Tables[0].Rows[0]["M_EMAILADDRESS"].ToString();
                ddlParentsIncome.SelectedValue = ds.Tables[0].Rows[0]["ANNUAL_INCOME"].ToString();

                //objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITYNO");
                objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                objCommon.FillDropDownList(ddlLCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO=1", "COUNTRYNAME");
                objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO=1", "COUNTRYNAME");
                objCommon.FillDropDownList(ddlLSta, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");

                txtCorresAddress.Text = ds.Tables[0].Rows[0]["LADDRESS"].ToString();
                if (ds.Tables[0].Rows[0]["COUNTRYID"].ToString() == "0" || ds.Tables[0].Rows[0]["COUNTRYID"].ToString() == string.Empty)
                {
                    objCommon.FillDropDownList(ddlLCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO=1", "COUNTRYNAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlLCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO=1", "COUNTRYNAME");
                    ddlLCon.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["STATEID"].ToString() == "0" || ds.Tables[0].Rows[0]["STATEID"].ToString() == string.Empty)
                {
                    objCommon.FillDropDownList(ddlLSta, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlLSta, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
                    ddlLSta.SelectedValue = ds.Tables[0].Rows[0]["STATEID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CITYID"].ToString() == "0" || ds.Tables[0].Rows[0]["CITYID"].ToString() == string.Empty)
                {
                    objCommon.FillDropDownList(ddlCorrCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND STATENO IN (21,29)", "CITY");
                }
                else
                {
                    objCommon.FillDropDownList(ddlCorrCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
                    ddlCorrCity.SelectedValue = ds.Tables[0].Rows[0]["CITYID"].ToString();
                }

                if (ds.Tables[0].Rows[0]["LPINCODE"].ToString() == string.Empty)
                {
                    txtLocalPIN.Text = ds.Tables[0].Rows[0]["PPINCODE"].ToString();
                }
                else
                {
                    txtLocalPIN.Text = ds.Tables[0].Rows[0]["LPINCODE"].ToString();
                }
                txtPermAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                if (ds.Tables[0].Rows[0]["PCOUNTRYID"].ToString() == "0" || ds.Tables[0].Rows[0]["PCOUNTRYID"].ToString() == string.Empty)
                {
                    objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO=1", "COUNTRYNAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO=1", "COUNTRYNAME");
                    ddlPCon.SelectedValue = ds.Tables[0].Rows[0]["PCOUNTRYID"].ToString();
                }

                if (ds.Tables[0].Rows[0]["PSTATEID"].ToString() == "0" || ds.Tables[0].Rows[0]["PSTATEID"].ToString() == string.Empty)
                {
                    objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                    ddlPermanentState.SelectedValue = ds.Tables[0].Rows[0]["PSTATEID"].ToString();
                }
                objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND STATENO=" + ddlPermanentState.SelectedValue, "CITY");

                if (ds.Tables[0].Rows[0]["PCITYID"].ToString() == "0" || ds.Tables[0].Rows[0]["PCITYID"].ToString() == string.Empty)
                {
                    objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND STATENO=" + ddlPermanentState.SelectedValue, "CITY");
                }
                else
                {
                    objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND STATENO=" + ddlPermanentState.SelectedValue, "CITY");
                    ddlPermanentCity.SelectedValue = ds.Tables[0].Rows[0]["PCITYID"].ToString();
                }
                txtPermPIN.Text = ds.Tables[0].Rows[0]["PPINCODE"].ToString();

                byte[] imgData = null;
                byte[] signData = null;
                //UPDATED BY: MD. REHBAR SHEIKH ON 09-02-2018
                if (ds.Tables[0].Rows[0]["PHOTO"] != DBNull.Value)
                {
                    imgData = ds.Tables[0].Rows[0]["PHOTO"] as byte[];

                    imgPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                    //imgPhotoNoCrop.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);

                    //imgPhoto.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                    //imgPhotoNoCrop.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                    ViewState["PHOTO"] = ds.Tables[0].Rows[0]["PHOTO"];
                }
                else
                {
                    imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                    //imgPhotoNoCrop.ImageUrl = "~/IMAGES/nophoto.jpg";

                }
                if (ds.Tables[0].Rows[0]["USER_SIGN"] != DBNull.Value)
                {
                    signData = ds.Tables[0].Rows[0]["USER_SIGN"] as byte[];
                    ImgSign.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(signData);
                    //ImgSignNoCrop.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);

                    //ImgSign.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENTSIGN";
                    //ImgSignNoCrop.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENTSIGN";
                    ViewState["USER_SIGN"] = ds.Tables[0].Rows[0]["USER_SIGN"];
                }
                else
                {
                    ImgSign.ImageUrl = "~/IMAGES/sign11.jpg";
                    //ImgSignNoCrop.ImageUrl = "~/IMAGES/sign11.jpg";

                }
            }
            DataSet dspopBranch = new DataSet();
            dspopBranch = objFetchData.GetEditBranchProgram(ViewState["USERNO"].ToString());
            if (dspopBranch.Tables[0].Rows.Count > 0)
            {
                FillDropDown();
                ddlAdmissionType.SelectedValue = dspopBranch.Tables[0].Rows[0]["ADM_TYPE"].ToString();
                ddlpopProgrammeType.SelectedValue = dspopBranch.Tables[0].Rows[0]["PROGRAMME_TYPE"].ToString();
                objCommon.FillDropDownList(ddlpopDegree, "ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "ACTIVESTATUS=1", "DEGREENO");
                objCommon.FillDropDownList(ddlEduDegree, "ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "ACTIVESTATUS=1", "DEGREENO");
                ddlpopDegree.SelectedValue = dspopBranch.Tables[0].Rows[0]["DEGREENO"].ToString();
                ddlEduDegree.SelectedValue = dspopBranch.Tables[0].Rows[0]["DEGREENO"].ToString();
                objCommon.FillDropDownList(ddlpopBranch, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(ddlpopDegree.SelectedValue), "B.LONGNAME");
                objCommon.FillDropDownList(ddlEduBranch, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + Convert.ToInt32(ddlpopDegree.SelectedValue), "B.LONGNAME");
                ddlpopBranch.SelectedValue = dspopBranch.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ddlEduBranch.SelectedValue = dspopBranch.Tables[0].Rows[0]["BRANCHNO"].ToString();
                if (ddlpopBranch.SelectedIndex > 0)
                {
                    divBranch.Visible = true;
                }
            }

            DataSet dsgetdet = new DataSet();
            dsgetdet = objFetchData.GetAllBranchDetails(userno);
            DataTable dd = dsgetdet.Tables[0];

            if (dsgetdet.Tables[0].Rows.Count > 0 && dsgetdet != null)
            {
                ViewState["ListView_Data"] = dsgetdet;
            }
            else
            {
                ViewState["ListView_Data"] = null;
            }

            if (dsgetdet.Tables[0].Rows.Count > 0 && dsgetdet != null)
            {
                txtTotFees.Text = dsgetdet.Tables[0].Rows[0]["FEES1"].ToString();
                txtTotBranches.Text = (dsgetdet.Tables[0].Rows.Count).ToString();
            }
            else
            {
                txtTotBranches.Text = "";
                txtTotFees.Text = "";
            }
            PopulateDropDown();

            DataSet dslvEn = new DataSet();
            dslvEn = objFetchData.GetEducationalDetails(userno);
            if (dslvEn.Tables[0] != null && dslvEn.Tables[0].Rows.Count > 0)
            {
                lvEntrance.DataSource = dslvEn;
                lvEntrance.DataBind();
                lvEntrance.Visible = true;
                //ViewState["chkEdu"] = ds;
                //btnNextEdu.Enabled = true;
            }
            else
            {
                lvEntrance.DataSource = null;
                lvEntrance.DataBind();
                lvEntrance.Visible = false;
                //btnNextEdu.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.lnkUserId_Click" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void FillDropDown()
    {
        objCommon.FillDropDownList(ddlpopProgrammeType, "ACD_UA_SECTION", "DISTINCT(UA_SECTION)", "UA_SECTIONNAME", "UA_SECTION>0 AND UA_SECTIONNAME IS NOT NULL", "UA_SECTION");
        objCommon.FillDropDownList(ddlAdmissionType, "ACD_ADMISSION_CONFIG AC", "DISTINCT AC.ADM_TYPE", "(CASE WHEN AC.ADM_TYPE=1 THEN 'Regular' WHEN AC.ADM_TYPE=2 THEN 'Lateral' ELSE '-' END) AS ADMISSION_TYPE", "ACTIVE_STATUS=1 AND ADM_TYPE >0", "AC.ADM_TYPE");
        objCommon.FillDropDownList(ddlpopProgrammeType, "ACD_ADMISSION_CONFIG AC INNER JOIN ACD_UA_SECTION US ON(US.UA_SECTION=AC.UGPGOT)", "DISTINCT AC.UGPGOT", "US.UA_SECTIONNAME", "ACTIVE_STATUS=1", "AC.UGPGOT");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Mp1.Hide();
            int programmeType = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
            int degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;

            DataSet ds = objFetchData.GetApplicantUserListNew_JECRC(Convert.ToInt32(ddlAdmbatch.SelectedValue), programmeType, Convert.ToInt32(rdoStatus.SelectedValue), degree);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                //pnlStudent.Visible = true;
                lvStudent.Visible = true;
                foreach (ListViewDataItem dataitem in lvStudent.Items)
                {
                    Label lblAppStatus = dataitem.FindControl("lblAppStatus") as Label;

                    if (lblAppStatus.Text == "PENDING")
                    {
                        //Button hdn = (Button)e.Item.FindControl("btnSendOffer");
                        Button hdn = dataitem.FindControl("btnSendOffer") as Button;
                        hdn.Visible = false;
                    }
                }
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
                objCommon.ShowError(Page, "FetchDetailRegistration_JECRC.btnShowStud_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlProgrammeType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlpopDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpopDegree.SelectedIndex > 0)
        {
            ViewState["flag"] = null;

            ViewState["flag"] = "-1";
            objCommon.FillDropDownList(ddlpopBranch, "ACD_ADMISSION_CONFIG AC INNER JOIN ACD_DEGREE D ON (D.DEGREENO= AC.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT DB.BRANCHNO", "B.LONGNAME", "FEES IS NOT NULL AND ((CONVERT(VARCHAR(8),GETDATE(),112)) between (CONVERT(VARCHAR(8),AC.ADMSTRDATE,112)) AND (CONVERT(VARCHAR(8),AC.ADMENDDATE,112)))  AND  AC.DEGREENO=" + Convert.ToInt32(ddlpopDegree.SelectedValue) + " AND ISCORE=0", "B.LONGNAME");
            ddlpopDegree.Focus();
        }
        else
        {
        }
    }
    protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSports_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSports.SelectedIndex == 1)
        {
            LevelOfSports.Visible = true;
            ddlLevelOfSports.Enabled = true;
            ddlLevelOfSports.Focus();
            sportsName.Visible = true;
            sportsDoc.Visible = true;
        }
        else
        {
            LevelOfSports.Visible = false;
            ddlLevelOfSports.SelectedIndex = 0;
            sportsName.Visible = false;
            sportsDoc.Visible = false;
            ddlLevelOfSports.Enabled = false;
            txtSportsName.Text = string.Empty;
            lblFileName.Text = string.Empty;
            ddlSports.Focus();
        }

    }
    protected void txtPercentageOfDisability_TextChanged(object sender, EventArgs e)
    {
        double per = Convert.ToDouble(txtPercentageOfDisability.Text);
        if (per <= 100)
        {
            if (per >= 40)
            {
                txtPercentageOfDisability.Focus();
            }

            else
            {
                ddlDiffAbility.SelectedValue = "2";
                txtNatureOfDisability.Text = string.Empty;
                txtPercentageOfDisability.Text = string.Empty;
                Abilility.Visible = false;
                Abilility1.Visible = false;
                ddlDiffAbility.Focus();
                objCommon.DisplayMessage(this.Page, "Your Disability Percentage Should Be Greater Than 40%.", this.Page);
            }
        }
        else
        {
            txtPercentageOfDisability.Text = string.Empty;
            txtPercentageOfDisability.Focus();
            objCommon.DisplayMessage(this.Page, "Your Disability Percentage Should Not Be Greater Than 100%.", this.Page);
        }
    }
    protected void ddlDiffAbility_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDiffAbility.SelectedValue == "1")
            {
                Abilility.Visible = true;
                Abilility1.Visible = true;
                Abilility.Attributes.Add("style", "display:block");
                Abilility1.Attributes.Add("style", "display:block");
            }
            else
            {
                Abilility.Visible = false;
                Abilility1.Visible = false;
                Abilility.Attributes.Add("style", "display:none");
                Abilility1.Attributes.Add("style", "display:block");
            }
            ddlDiffAbility.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlLCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLCon.SelectedIndex > 0)
        {
            if (ddlLCon.SelectedValue == "1")
            {
                txtLcountry.Visible = false;
                //ddlLCon.Visible = true;
                ddlLSta.Visible = true;
                ddlCorrCity.Visible = true;
                txtLState.Visible = false;
                txtLcity.Visible = false;
            }
            else
            {
                txtLcountry.Visible = true;
                //ddlLCon.Visible = false;
                txtOtherCity.Visible = false;
                ddlLSta.Visible = false;
                ddlCorrCity.Visible = false;
                txtLState.Visible = true;
                txtLcity.Visible = true;
            }
        }
        else
        {
            txtLcountry.Visible = false;
            //ddlLCon.Visible = true;
            ddlLSta.Visible = true;
            ddlCorrCity.Visible = true;
            txtLState.Visible = false;
            txtLcity.Visible = false;
        }
        ddlLCon.Focus();
    }
    protected void ddlLSta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLSta.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCorrCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND STATENO=" + ddlLSta.SelectedValue, "CITY");
            txtOtherCity.Visible = false;
            ddlLSta.Focus();
        }
    }
    protected void ddlCorrCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCorrCity.SelectedItem.Text == "Other City" || ddlCorrCity.SelectedItem.Text == "Other")
        {
            txtOtherCity.Visible = true;
        }
        else
        {
            txtOtherCity.Visible = false;
        }
        ddlCorrCity.Focus();
    }
    protected void ddlPCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPCon.SelectedIndex > 0)
        {
            if (ddlPCon.SelectedValue == "1")
            {
                txtCCountry.Visible = false;
                ddlPermanentState.Visible = true;
                ddlPermanentCity.Visible = true;
                txtCState.Visible = false;
                txtCCity.Visible = false;
            }
            else
            {
                txtCCountry.Visible = true;
                ddlPermanentState.Visible = false;
                ddlPermanentCity.Visible = false;
                txtCState.Visible = true;
                txtCCity.Visible = true;
                txtPerOtherCity.Visible = false;
            }
        }
        else
        {
            txtCCountry.Visible = false;
            ddlPermanentState.Visible = true;
            ddlPermanentCity.Visible = true;
            txtCState.Visible = false;
            txtCCity.Visible = false;
        }
        ddlPCon.Focus();
    }
    protected void ddlPermanentState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPermanentState.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND STATENO=" + ddlPermanentState.SelectedValue, "CITY");
            txtPerOtherCity.Visible = false;
        }
        ddlPermanentState.Focus();
    }
    protected void ddlPermanentCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPermanentCity.SelectedItem.Text == "Other City" || ddlPermanentCity.SelectedItem.Text == "Other")
        {
            txtPerOtherCity.Visible = true;
        }
        else
        {
            txtPerOtherCity.Visible = false;
        }
        ddlPermanentCity.Focus();
    }
    protected void imgbToCopyLocalAddress_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlLCon.SelectedValue == "2")
        {
            if (txtCorresAddress.Text == string.Empty || txtLcountry.Text == string.Empty || txtLocalPIN.Text == string.Empty || txtLState.Text == string.Empty || txtLcity.Text == string.Empty)
            {
                objCommon.DisplayMessage(updAddressDetails, "Please fill all Manedatory fields.", this.Page);
            }
            else
            {
                txtPermAddress.Text = txtCorresAddress.Text;
                txtPermPIN.Text = txtLocalPIN.Text;
                ddlPermanentCity.Visible = false;
                ddlPermanentState.Visible = false;
                ddlPCon.Visible = true;
                ddlPCon.SelectedValue = ddlLCon.SelectedValue;
                txtCCountry.Visible = true;
                txtCState.Visible = true;
                txtCCity.Visible = true;
                txtCCountry.Text = txtLcountry.Text;
                txtCState.Text = txtLState.Text;
                txtCCity.Text = txtLcity.Text;


                //txtPermAddress.Enabled = false;
                //ddlPCon.Enabled = false;
                //txtCCountry.Enabled = false;
                //ddlPermanentState.Enabled = false;
                //txtCState.Enabled = false;
                //ddlPermanentCity.Enabled = false;
                //txtCCity.Enabled = false;
                //txtPerOtherCity.Enabled = false;
                //txtPermPIN.Enabled = false;
                //ddlPCon.Visible = false;
            }
        }
        else if (ddlLCon.SelectedValue == "1")
        {
            if (txtCorresAddress.Text == string.Empty || txtLocalPIN.Text == string.Empty || ddlLCon.SelectedValue == "0" || ddlLSta.SelectedValue == "0" || ddlCorrCity.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updAddressDetails, "Please fill all Manedatory fields.", this.Page);
            }
            else
            {
                if (ddlCorrCity.SelectedItem.Text == "Other City" || ddlCorrCity.SelectedItem.Text == "Other")
                {
                    txtPerOtherCity.Visible = true;
                    txtOtherCity.Visible = true;
                }
                else
                {
                    txtPerOtherCity.Visible = false;
                    txtOtherCity.Visible = false;
                }

                txtPerOtherCity.Text = txtOtherCity.Text;
                txtPermAddress.Text = txtCorresAddress.Text;
                txtPermPIN.Text = txtLocalPIN.Text;
                objCommon.FillDropDownList(ddlPermanentCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
                ddlPermanentCity.SelectedValue = ddlCorrCity.SelectedValue;
                ddlPermanentState.SelectedValue = ddlLSta.SelectedValue;
                ddlPCon.SelectedValue = ddlLCon.SelectedValue;

                //txtPermAddress.Enabled = false;
                //ddlPCon.Enabled = false;
                //txtCCountry.Enabled = false;
                //ddlPermanentState.Enabled = false;
                //txtCState.Enabled = false;
                //ddlPermanentCity.Enabled = false;
                //txtCCity.Enabled = false;
                //txtPerOtherCity.Enabled = false;
                //txtPermPIN.Enabled = false;
            }
        }
        else
        {
            if (txtCorresAddress.Text == string.Empty || txtLocalPIN.Text == string.Empty || ddlLCon.SelectedValue == "0" || ddlLSta.SelectedValue == "0" || ddlCorrCity.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updAddressDetails, "Please fill all Manedatory fields.", this.Page);
            }
        }
        updAddressDetails.Focus();
    }
    protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlQual_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlQual.SelectedIndex > 0)
            {
                string qual = string.Empty;
                if (ddlQual.SelectedValue == "1")
                {
                    qual = "Q";
                }
                else if (ddlQual.SelectedValue == "2")
                {
                    qual = "E";
                }
                string ugpgot = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "UGPGOT", "DEGREENO=" + Convert.ToInt32(ddlpopDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlpopBranch.SelectedValue));

                objCommon.FillDropDownList(ddlExam, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QEXAMSTATUS='" + qual + "' AND PROGRAMME_TYPE=" + Convert.ToInt32(ugpgot), "");

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong.", this.Page);
            return;
        }
        ddlQual.Focus();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlEduDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlEduBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public double Cdouble(string strdouble)
    {
        double i = 0;
        double.TryParse(strdouble, out i);
        return i;
    }
    protected void txtMaxMark_TextChanged(object sender, EventArgs e)
    {
        if (txtMaxMark.Text != string.Empty)
        {
            if (Cdouble(txtMaxMark.Text) < 100)
            {
                objCommon.DisplayMessage(updEdcutationalDetails, "Full Mark of Paper should be greater than or equals to 100.", this.Page);
                txtMaxMark.Text = "";
                txtMaxMark.Focus();
                return;
            }
        }
        if (txtMarksObtained.Text != string.Empty && txtMaxMark.Text != string.Empty)
        {
            if (Cdouble(txtMarksObtained.Text) > Cdouble(txtMaxMark.Text))
            {
                objCommon.DisplayMessage(updEdcutationalDetails, "Maximum marks should be greater than obtained marks", this.Page);
                txtMaxMark.Text = "";
                txtMaxMark.Focus();
            }
            else
            {
                double marks_obtained = 0.00;
                double outOfMarks = 0.00;
                double percentagee = 0.00;
                marks_obtained = Cdouble(txtMarksObtained.Text);
                outOfMarks = Cdouble(txtMaxMark.Text);
                percentagee = (marks_obtained / outOfMarks) * 100;
                txtPer.Text = percentagee.ToString("00.00");
            }
        }
        txtMaxMark.Focus();
    }
    protected void btnEditEdu_Click(object sender, ImageClickEventArgs e)
    {

    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    //string name = string.Empty;
    //    //string fileNotSelected = string.Empty;
    //    //int docno = 0; ;
    //    //string temp = string.Empty;
    //    //int count = 0;


    //    //try
    //    //{
    //    //    objdocument.USERNO = Convert.ToInt32(ViewState["USERNO"].ToString());
    //    //    int IdNo = objdocument.USERNO;
    //    //    string AppID = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + ViewState["USERNO"].ToString());
    //    //    foreach (ListViewDataItem lvitem in lvDocument.Items)
    //    //    {
    //    //        FileUpload fuStudDoc = lvitem.FindControl("fuDocument1") as FileUpload;
    //    //        Label lbldoc = lvitem.FindControl("lbldoc") as Label;
    //    //        Label lblname = lvitem.FindControl("lblname") as Label;
    //    //        if (fuStudDoc.HasFile)
    //    //        {
    //    //            count++;
    //    //            if (!fuStudDoc.PostedFile.ContentLength.Equals(string.Empty) || fuStudDoc.PostedFile.ContentLength != null)
    //    //            {
    //    //                int fileSize = fuStudDoc.PostedFile.ContentLength;

    //    //                int KB = fileSize / 1024;
    //    //                if (KB >= 500)
    //    //                {
    //    //                    objCommon.DisplayMessage(updDocuments, "Uploaded File size should be less than or equal to 500 kb.", this.Page);
    //    //                    return;
    //    //                }
    //    //            }
    //    //            temp = fuStudDoc.PostedFile.FileName;
    //    //            name = lblname.Text;
    //    //            docno = Convert.ToInt32(lbldoc.ToolTip);
    //    //            string ext = System.IO.Path.GetExtension(temp);
    //    //            if (ext == ".pdf")
    //    //            {
    //    //                    objdocument.FILENAME = AppID + "_" + ViewState["USERNO"].ToString() + "_" + docno+ext;    
    //    //                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, objdocument.FILENAME  + "", fuStudDoc);
    //    //                    if (retval == 0)
    //    //                    {
    //    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
    //    //                        return;
    //    //                    }
    //    //                    CustomStatus cs = (CustomStatus)objFetchData.UpdateDocument(objdocument, docno, temp);

    //    //            }
    //    //            else
    //    //            {
    //    //                objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
    //    //                return;
    //    //            }
    //    //        }
    //    //    }
    //    //    if (count == 0)
    //    //    {
    //    //        objCommon.DisplayMessage(updDocuments, "Please Browse File!", this.Page);
    //    //        return;
    //    //    }
    //    //    else
    //    //    {
    //    //        objCommon.DisplayMessage(updDocuments, "Document Submitted Successfully", this.Page);
    //    //        return;
    //    //    }
    //    //    PopulateDropDown();
    //    //   // Mp1.Show();
    //    //    updDocuments.Focus();
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    if (Convert.ToBoolean(Session["error"]) == true)
    //    //        objCommon.ShowError(Page, "ACADEMIC_Acd_Update_Photo_Student.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //    //    else
    //    //        objCommon.ShowError(Page, "Server UnAvailable");
    //    //}
    //    ////Mp1.Show();
    //    //updDocuments.Focus();
    //}
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName; //+ Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
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
    protected void imgbtnPrevDoc_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {

            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                ImageButton lnkView = (ImageButton)(sender);
                string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
                iframeView.Src = urlpath + ImageName;
                mpeViewDocument.Show();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ListofDocuments.imgBtnPrev_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        //updDocuments.Focus();
    }
    protected void lvDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblname, lblsrno;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            lblname = (Label)e.Item.FindControl("lblname");
            lblsrno = (Label)e.Item.FindControl("lblSRNO");
            //btnUpload = (Button)e.Item.FindControl("btnUpload");

        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        string path = ViewState["PREVIEW"].ToString();
        iframeView.Attributes.Add("src", path);

        mpeViewDocument.Show();
    }
    protected void CheckFile(out int cont, out int Row)
    {
        cont = 0;
        Row = 0;
        foreach (ListViewDataItem lvitem in lvDocument.Items)
        {
            FileUpload fuStudDoc = lvitem.FindControl("fuDocument1") as FileUpload;
            Row = lvDocument.Items.Count;
            if (fuStudDoc.HasFile)
            {
                cont++;
            }
        }
    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
            case ".cs":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    private void PopulateDropDown()
    {
        string degreeno = objCommon.LookUp("ACD_USER_BRANCH_PREF", "DEGREENO", "USERNO=" + ViewState["USERNO"].ToString());
        string admType = objCommon.LookUp("ACD_USER_BRANCH_PREF", "ADM_TYPE", "USERNO=" + ViewState["USERNO"].ToString());
        if (admType == string.Empty)
        {
            admType = "0";
        }
        else
        {
            admType = objCommon.LookUp("ACD_USER_BRANCH_PREF", "ADM_TYPE", "USERNO=" + ViewState["USERNO"].ToString());
        }
        DataSet dslvDoc = new DataSet();
        dslvDoc = objFetchData.GetDocumentList_OA(ViewState["USERNO"].ToString(), Convert.ToInt32(degreeno), Convert.ToInt32(admType), Convert.ToInt32(ddlNationality.SelectedValue));
        if (dslvDoc.Tables[0].Rows.Count > 0)
        {
            lvDocument.DataSource = dslvDoc;
            lvDocument.DataBind();
            lvDocument.Visible = true;
        }
        foreach (ListViewDataItem dataitem in lvDocument.Items)
        {
            Button btnUpload = dataitem.FindControl("btnUpload") as Button;

            int ConfStatus = Convert.ToInt32(objCommon.LookUp("ACD_USER_PROFILE_STATUS", "ISNULL(CONFIRM_STATUS,0)", " USERNO=" + ViewState["USERNO"].ToString()));
            if (ConfStatus == 1)
            {
                btnUpload.Visible = false;
            }
        }
    }
    protected void btnSubmitPopup_Click(object sender, EventArgs e)
    {
        string name = string.Empty;
        string fileNotSelected = string.Empty;
        int docno = 0; ;
        string temp = string.Empty;
        int count = 0;


        try
        {
            objdocument.USERNO = Convert.ToInt32(ViewState["USERNO"].ToString());
            int IdNo = objdocument.USERNO;
            string AppID = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + ViewState["USERNO"].ToString());
            foreach (ListViewDataItem lvitem in lvDocument.Items)
            {
                FileUpload fuStudDoc = lvitem.FindControl("fuDocument1") as FileUpload;
                Label lbldoc = lvitem.FindControl("lbldoc") as Label;
                Label lblname = lvitem.FindControl("lblname") as Label;
                if (fuStudDoc.HasFile)
                {
                    count++;
                    if (!fuStudDoc.PostedFile.ContentLength.Equals(string.Empty) || fuStudDoc.PostedFile.ContentLength != null)
                    {
                        int fileSize = fuStudDoc.PostedFile.ContentLength;

                        int KB = fileSize / 1024;
                        if (KB >= 500)
                        {
                            objCommon.DisplayMessage(updDocuments, "Uploaded File size should be less than or equal to 500 kb.", this.Page);
                            return;
                        }
                    }
                    temp = fuStudDoc.PostedFile.FileName;
                    name = lblname.Text;
                    docno = Convert.ToInt32(lbldoc.ToolTip);
                    string ext = System.IO.Path.GetExtension(temp);
                    if (ext == ".pdf")
                    {
                        objdocument.FILENAME = AppID + "_" + ViewState["USERNO"].ToString() + "_" + docno + ext;
                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, objdocument.FILENAME + "", fuStudDoc);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        CustomStatus cs = (CustomStatus)objFetchData.UpdateDocument(objdocument, docno, temp);

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
                        return;
                    }
                }
            }

            string userno = ViewState["USERNO"].ToString();
            string degree = objCommon.LookUp("ACD_USER_BRANCH_PREF", "DEGREENO", "USERNO =" + userno);
            string branch = objCommon.LookUp("ACD_USER_BRANCH_PREF", "BRANCHNO", "USERNO =" + userno + "AND DEGREENO =" + degree);
            string brpref = objCommon.LookUp("ACD_USER_BRANCH_PREF", "BRPREF", "USERNO =" + userno + "AND DEGREENO =" + degree + "AND BRANCHNO =" + branch);
            int isSpecialize = 0;
            double Fees = 0;
            txtTotFees.Attributes.Add("readonly", "readonly");
            Fees = Convert.ToDouble(txtTotFees.Text);

            string SP_Name = string.Empty; string SP_Parameters = string.Empty; string SP_Value = string.Empty;
            SP_Name = "PKG_USER_UPD_BRANCH_PREF_EDIT";
            SP_Parameters = "@P_USER_NO,@P_BRPRNO,@P_DEGREENO,@P_BRANCHNO,@P_FEES,@P_ADM_TYPE,@P_IS_SPEC,@P_PROGRAMME_TYPE";
            SP_Value = "" + Convert.ToInt32(userno) + "," + brpref + "," + Convert.ToInt32(ddlpopDegree.SelectedValue) + "," + Convert.ToInt32(ddlpopBranch.SelectedValue) + "," + Convert.ToDouble(Fees) + "," + Convert.ToInt32(ddlAdmissionType.SelectedValue) + "," + isSpecialize+","+Convert.ToInt32(ddlpopProgrammeType.SelectedValue)+"";
            DataSet dsUpd = null;
            dsUpd = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Value);
            if (dsUpd.Tables[0].Rows.Count > 0)
            {
                if (dsUpd.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {
                    
                }
            }
            //CustomStatus csProg = (CustomStatus)objFetchData.UpadateBranchPreference1(Convert.ToInt32(ddlpopDegree.SelectedValue), Convert.ToInt32(ddlpopBranch.SelectedValue), Convert.ToInt32(ddlAdmissionType.SelectedValue), brpref, Convert.ToDouble(Fees), userno, isSpecialize);
            //if (csProg.Equals(CustomStatus.RecordUpdated))//
            //{

            //    objCommon.DisplayMessage(this.Page, "Programme Updated Successfully.", this.Page);

            //}

            objus.USERNO = Convert.ToInt32(ViewState["USERNO"].ToString());
            objus.FATHERNAME = txtFatherName.Text.ToString().Trim();
            objus.MOTHERNAME = txtMothersName.Text.ToString().Trim();
            objus.GENDER = Convert.ToInt32(rdoGender.SelectedValue);
            objus.EMAILID = txtEmail.Text.ToString().Trim();
            objus.ALTERNATEEMAILID = txtAlternateEmailId.Text.ToString().Trim().Equals("") ? "" : txtAlternateEmailId.Text.ToString().Trim();
            objus.RELIGION = Convert.ToInt32(ddlReligion.SelectedValue);
            objus.CATEGORY = Convert.ToInt32(ddlCategory.SelectedValue);
            objus.NATIONALITY = Convert.ToInt32(ddlNationality.SelectedValue);
            objus.BLOODGRP = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            objus.IDENTIFICATIONMARK = txtIdentificationMark.Text.ToString().Trim();
            objus.ADHAARNO = txtAdhaarNo.Text.ToString().Trim();
            objus.MaritalStatus = Convert.ToInt32(ddlMarital.SelectedValue);
            if (ddlDiffAbility.SelectedIndex > 0)
            {
                if (ddlDiffAbility.SelectedValue == "1")
                {
                    objus.Differently_Abled = Convert.ToInt32(ddlDiffAbility.SelectedValue);
                    objus.Nature_Disability = txtNatureOfDisability.Text.ToString().Trim();
                    objus.Percentage_Disability = txtPercentageOfDisability.Text.ToString().Trim();
                }
                else
                {
                    objus.Differently_Abled = Convert.ToInt32(ddlDiffAbility.SelectedValue);
                    objus.Nature_Disability = txtNatureOfDisability.Text.ToString().Trim().Equals("") ? "NA" : txtNatureOfDisability.Text.ToString().Trim();
                    objus.Percentage_Disability = txtPercentageOfDisability.Text.ToString().Trim().Equals("") ? "NA" : txtPercentageOfDisability.Text.ToString().Trim();
                }
            }
            objus.State_Domicile = Convert.ToInt32(ddlStateDomicile.SelectedValue);

            if (ddlSports.SelectedIndex > 0)
            {
                if (ddlSports.SelectedValue == "1")
                {
                    objus.Sports_Person = Convert.ToInt32(ddlSports.SelectedValue);
                    objus.Sports_Represented = Convert.ToInt32(ddlLevelOfSports.SelectedValue);
                    objus.sportsName = txtSportsName.Text.ToString().Trim();

                    if (!sportUpload.FileName.Equals(""))
                    {
                        string path = MapPath("~/ONLINESPORTSDOC/");

                        if (!(Directory.Exists(path)))
                            Directory.CreateDirectory(path);
                        if (sportUpload.HasFile)
                        {
                            if (sportUpload != null)
                            {
                                string[] validFileTypes = { "pdf" };
                                string ext1 = System.IO.Path.GetExtension(sportUpload.PostedFile.FileName);
                                bool isValidFile = false;
                                for (int i = 0; i < validFileTypes.Length; i++)
                                {
                                    if (ext1 == "." + validFileTypes[i])
                                    {
                                        isValidFile = true;
                                        break;
                                    }
                                }
                                if (!sportUpload.PostedFile.ContentLength.Equals(string.Empty) || sportUpload.PostedFile.ContentLength != null)
                                {
                                    int fileSize = sportUpload.PostedFile.ContentLength;
                                    int KB = fileSize / 1024;
                                    if (KB >= 500)
                                    {
                                        objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);
                                        return;
                                    }
                                }
                                if (sportUpload == null)
                                {
                                    objCommon.DisplayMessage(this.Page, "Select Sports Document File to Upload or Uploaded file size should be greater than 0 kb !", this.Page);
                                    return;
                                }
                                if (!isValidFile)
                                {
                                    objCommon.DisplayMessage(this.Page, "Upload the Sports Document File only with .pdf format.", this.Page);
                                    return;
                                }
                                else
                                {
                                    string[] array1 = Directory.GetFiles(path);
                                    foreach (string str in array1)
                                    {
                                        if ((path + AppID + "_" + sportsDoc.ToString().Replace(' ', ' ')).Equals(str))
                                        {
                                            //objCommon.DisplayMessage(this.Page, "Sports Related File Already Exists!", this.Page);
                                            //return;
                                        }
                                    }
                                    objus.sportsDoc = AppID + "_" + sportUpload.FileName.ToString();
                                    sportUpload.SaveAs(path + AppID + "_" + sportsDoc);
                                    objus.sportsDocPath = Server.MapPath("~/ONLINESPORTSDOC\\");
                                    lblFileName.Visible = true;
                                    lblFileName.Text = sportsDoc.ToString();
                                    lblFileName.ForeColor = System.Drawing.Color.Green;
                                }
                            }
                        }
                    }
                    else
                    {
                        objus.sportsDoc = objCommon.LookUp("ACD_NEWUSER_REGISTRATION", "SPORTS_DOC", "USERNO=" + userno);
                        objus.sportsDoc = objCommon.LookUp("ACD_NEWUSER_REGISTRATION", "SPORTS_DOC_PATH", "USERNO=" + userno);
                    }
                }
                else
                {
                    objus.Sports_Person = Convert.ToInt32(ddlSports.SelectedValue);
                    objus.Sports_Represented = Convert.ToInt32(ddlLevelOfSports.SelectedValue).Equals("0") ? 0 : Convert.ToInt32(ddlLevelOfSports.SelectedValue);
                    objus.sportsName = txtSportsName.Text.ToString().Trim().Equals("") ? "NA" : txtSportsName.Text.ToString().Trim();
                }
            }
            if (ddlHost.SelectedIndex > 0)
            {
                objus.Host_Trans = Convert.ToInt32(ddlHost.SelectedValue);
                objus.Host_Trans_Name = ddlHost.SelectedItem.Text;
            }
            else
            {
                objus.Host_Trans = 0;
                objus.Host_Trans_Name = "NA";
            }
            objus.F_TelNumber = txtFTelNo.Text.ToString().Trim().Equals("") ? "NA" : txtFTelNo.Text.ToString();
            objus.F_MobileNo = txtFMobile.Text.ToString();
            objus.F_Occupation = Convert.ToInt32(ddlFOccupation.SelectedValue).Equals("0") ? 0 : Convert.ToInt32(ddlFOccupation.SelectedValue);
            objus.F_Designation = txtFDesignation.Text.ToString().Trim().Equals("") ? "NA" : txtFDesignation.Text.ToString();
            objus.F_EmailAddress = txtFEmail.Text.ToString().Trim().Equals("") ? "" : txtFEmail.Text.ToString();
            objus.M_TelNumber = txtMTelNo.Text.ToString().Trim().Equals("") ? "NA" : txtMTelNo.Text.ToString();
            objus.M_MobileNo = txtMMobile.Text.ToString();
            objus.M_Occupation = Convert.ToInt32(ddlMOccupation.SelectedValue).Equals("0") ? 0 : Convert.ToInt32(ddlMOccupation.SelectedValue);
            objus.M_Designation = txtMDesignation.Text.ToString().Trim().Equals("") ? "NA" : txtMDesignation.Text.ToString();
            objus.M_EmailAddress = txtMEmail.Text.ToString().Trim().Equals("") ? "" : txtMEmail.Text.ToString();
            objus.ParentsAnnualIncome = Convert.ToInt32(ddlParentsIncome.SelectedValue.ToString());

            //string Focc, Mocc; 
            if (txtOccFather.Text.ToString().Equals(string.Empty) || txtOccFather == null)
            {
                objus.F_OccupationOther = "NA";
            }
            else
            {
                objus.F_OccupationOther = txtOccFather.Text.ToString();
            }
            if (txtOccMother.Text.ToString().Equals(string.Empty) || txtOccMother == null)
            {
                objus.M_OccupationOther = "NA";
            }
            else
            {
                objus.M_OccupationOther = txtOccMother.Text.ToString();
            }


            //Update Address
            if (ddlLCon.SelectedValue == "1")
            {
                // Get Address details
                objnu.CADDRESS = txtCorresAddress.Text.ToString();
                objnu.PADDRESS = txtPermAddress.Text.ToString();
                objnu.CPINNO = txtLocalPIN.Text.ToString();
                objnu.PPINNO = txtPermPIN.Text.ToString();
                objus.USERNO = Convert.ToInt32(userno);

                objnu.STATE = ddlLSta.SelectedItem.Text.ToString();
                objnu.PSTATE = ddlPermanentState.SelectedItem.Text.ToString();
                if (ddlCorrCity.SelectedItem.Text == "Other City" || ddlCorrCity.SelectedItem.Text == "Other")
                {
                    objnu.CITY = txtOtherCity.Text;
                }
                else
                {
                    objnu.CITY = ddlCorrCity.SelectedItem.Text.ToString();
                }
                if (ddlPermanentCity.SelectedItem.Text == "Other City" || ddlPermanentCity.SelectedItem.Text == "Other")
                {
                    objnu.PCITY = txtPerOtherCity.Text;
                }
                else
                {
                    objnu.PCITY = ddlPermanentCity.SelectedItem.Text.ToString();
                }
                //objnu.PCITY = ddlPermanentCity.SelectedItem.Text.ToString();
                objnu.COUNTRY = ddlLCon.SelectedItem.Text.ToString();
                objnu.P_COUNTRY = ddlPCon.SelectedItem.Text.ToString();
                objnu.CountryId = Convert.ToInt32(ddlLCon.SelectedValue);
                objnu.StateId = Convert.ToInt32(ddlLSta.SelectedValue);
                objnu.CityId = Convert.ToInt32(ddlCorrCity.SelectedValue);

                objnu.PCountryId = Convert.ToInt32(ddlPCon.SelectedValue);
                objnu.PStateId = Convert.ToInt32(ddlPermanentState.SelectedValue);
                objnu.PCityId = Convert.ToInt32(ddlPermanentCity.SelectedValue);

                String cs = objFetchData.SubmitAddressDetails(objnu, objus);

                //if (cs != string.Empty && cs != "" && cs != "-99")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Your information has been submitted successfully.');", true);
                //}
                //else
                //{
                //    objCommon.DisplayMessage("Error!!", this.Page);
                //}
            }
            else if (ddlLCon.SelectedValue == "2")//For NRI
            {
                // Get Address details
                objnu.CADDRESS = txtCorresAddress.Text.ToString();
                objnu.PADDRESS = txtPermAddress.Text.ToString();
                objnu.CPINNO = txtLocalPIN.Text.ToString();
                objnu.PPINNO = txtPermPIN.Text.ToString();
                objus.USERNO = Convert.ToInt32(userno);

                objnu.STATE = txtLState.Text;
                objnu.PSTATE = txtCState.Text;
                objnu.CITY = txtLcity.Text;
                objnu.PCITY = txtCCity.Text;
                objnu.COUNTRY = txtLcountry.Text;
                objnu.P_COUNTRY = txtCCountry.Text;

                objnu.CountryId = Convert.ToInt32(ddlLCon.SelectedValue);
                objnu.StateId = Convert.ToInt32(ddlLSta.SelectedValue);
                objnu.CityId = Convert.ToInt32(ddlCorrCity.SelectedValue);

                String cs = objFetchData.SubmitAddressDetails(objnu, objus);

                //if (cs != string.Empty && cs != "" && cs != "-99")
                //{

                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Your information has been submitted successfully.');", true);
                //}
                //else
                //{
                //    objCommon.DisplayMessage("Error!!", this.Page);
                //}
            }


            //Photo and Signature Update
            //Photo
            bool isPhotoUploaded = false;
            if (fuPhoto.HasFile || ViewState["PHOTO"] != null)
            {
                isPhotoUploaded = true;
            }
            if (!isPhotoUploaded)
            {
                //objCommon.DisplayMessage(this.Page, "Please Select Photo.", this.Page);
                //return;
            }
            string photo = string.Empty;
            if (fuPhoto.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuPhoto.PostedFile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".JPG" || ext == ".JPEG")
                {
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Photo with .jpg,.jpeg format only.", this.Page);
                    return;
                }
                if (fuPhoto.PostedFile.ContentLength < 500000)
                {
                    byte[] resizephoto = ResizePhoto(fuPhoto);
                    if (resizephoto.LongLength >= 500000)
                    {
                        objCommon.DisplayMessage(this, "Photo size should be less than or equal to 500 KB.", this.Page);
                        return;
                    }
                    else
                    {
                        objnu.PHOTO = resizephoto;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Photo size should be less than or equal to 500 KB.", this.Page);
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
                    objnu.PHOTO = null;
                    objnu.PHOTO = ViewState["PHOTO"] as byte[];
                }
            }
            //Sign
            bool isSignUploaded = false;
            if (fuSignature.HasFile || ViewState["USER_SIGN"] != null)
            {
                isSignUploaded = true;
            }
            if (!isSignUploaded)
            {
                //objCommon.DisplayMessage(this.Page, "Please Select Signature.", this.Page);
                //return;
            }
            string sign = string.Empty;
            if (fuSignature.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fuSignature.PostedFile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".JPG" || ext == ".JPEG")
                {
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Signature with .jpg,.jpeg format only.", this.Page);
                    return;
                }
                if (fuSignature.PostedFile.ContentLength < 500000)
                {
                    byte[] resizephoto = ResizePhoto(fuSignature);
                    if (resizephoto.LongLength >= 500000)
                    {
                        objCommon.DisplayMessage(this, "Signature size should be less than or equal to 500 KB.", this.Page);
                        return;
                    }
                    else
                    {
                        objnu.Studsign = resizephoto;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Signature size should be less than or equal to 500 KB.", this.Page);
                    return;
                }
                sign = "Sign";
            }
            else
            {
                if (ViewState["USER_SIGN"] == null)
                {
                }
                else
                {
                    objnu.Studsign = null;
                    objnu.Studsign = ViewState["USER_SIGN"] as byte[];
                }
            }

            String csphoto = objFetchData.SubmitPhotoandSignature(objnu, objus);
            //if (csphoto != string.Empty && csphoto != "" && csphoto != "-99")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Signature Saved Successfully.');", true);
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Error!!", this.Page);
            //}

            //Education Details Update
            if (lblFlagEdu.Text == "1")
            {
                int stlqno = Convert.ToInt32(ViewState["STLQNO"].ToString());
                string Qualtype = string.Empty;
                if (ddlQual.SelectedValue == "1")
                {
                    Qualtype = "Q";
                }
                else if (ddlQual.SelectedValue == "2")
                {
                    Qualtype = "E";
                }
                objnu.QualifyNo = Convert.ToInt32(ddlExam.SelectedValue.ToString());
                objnu.ExamName = ddlExam.SelectedItem.Text;
                txtPer.Attributes.Add("readonly", "readonly");
                objnu.Percentage = Convert.ToDouble(txtPer.Text);
                objnu.MarksObtained = Convert.ToDouble(txtMarksObtained.Text);
                objnu.TotalMarks = Convert.ToDouble(txtMaxMark.Text);

                String csEdu = objFetchData.UpdateEdcucationalDetails(objnu, objus, Qualtype, stlqno);
                lblFlagEdu.Text = string.Empty;
            }

            CustomStatus csParti = (CustomStatus)objFetchData.SubmitPersonalandBankDetails(objus);
            if (csParti.Equals(CustomStatus.RecordUpdated))//
            {
                Mp1.Hide();
                int programmeType = ddlProgrammeType.SelectedIndex > 0 ? Convert.ToInt32(ddlProgrammeType.SelectedValue) : 0;
                int degreegrd = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;

                DataSet ds = objFetchData.GetApplicantUserListNew_JECRC(Convert.ToInt32(ddlAdmbatch.SelectedValue), programmeType, Convert.ToInt32(rdoStatus.SelectedValue), degreegrd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    //pnlStudent.Visible = true;
                    lvStudent.Visible = true;
                    foreach (ListViewDataItem dataitem in lvStudent.Items)
                    {
                        Label lblAppStatus = dataitem.FindControl("lblAppStatus") as Label;

                        if (lblAppStatus.Text == "PENDING")
                        {
                            //Button hdn = (Button)e.Item.FindControl("btnSendOffer");
                            Button hdn = dataitem.FindControl("btnSendOffer") as Button;
                            hdn.Visible = false;
                        }
                    }
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
                objCommon.DisplayMessage(this.Page, "Applicant Details Updated Successfully.", this.Page);
            }

            //PopulateDropDown();
            //updDocuments.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Acd_Update_Photo_Student.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
    protected void btnEditlv_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EditEdudetail.Visible = true;
            ImageButton btnEditlv = sender as ImageButton;
            //string userno = (ViewState["USERNO"].ToString());
            int stlqno = int.Parse(btnEditlv.CommandArgument);
            ViewState["STLQNO"] = stlqno;
            DataSet dslvEn = new DataSet();
            dslvEn = objFetchData.GetEducationalDetails_Qual(ViewState["USERNO"].ToString(), stlqno);
            lblFlagEdu.Text = "1";
            if (dslvEn.Tables[0] != null && dslvEn.Tables[0].Rows.Count > 0)
            {
                string qual = dslvEn.Tables[0].Rows[0]["QUALIFY_TYPE"].ToString();
                if (qual == "Q")
                {
                    ddlQual.SelectedValue = "1";
                }
                else if (qual == "E")
                {
                    ddlQual.SelectedValue = "2";
                }
                objCommon.FillDropDownList(ddlExam, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QEXAMSTATUS='" + qual + "' AND PROGRAMME_TYPE=" + ddlpopProgrammeType.SelectedValue, "");
                ddlExam.SelectedValue = dslvEn.Tables[0].Rows[0]["QUALIFYNO"].ToString();
                txtMarksObtained.Text = dslvEn.Tables[0].Rows[0]["OBTAINED_MARKS"].ToString();
                txtMaxMark.Text = dslvEn.Tables[0].Rows[0]["OUT_OF_MARKS"].ToString();
                txtPer.Text = dslvEn.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                //ddlYear.SelectedValue = dslvEn.Tables[0].Rows[0]["YEAR_OF_PASSING"].ToString();
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong.", this.Page);
            return;
        }
        EditEdudetail.Focus();
    }
    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }
    //private int OutLook_Email(string Message, string toEmailId, string sub)
    //{

    //    int ret = 0;
    //    try
    //    {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        SmtpMail oMail = new SmtpMail("TryIt");
    //        oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //        oMail.To = toEmailId;
    //        oMail.Subject = sub;
    //        oMail.HtmlBody = Message;
    //        // SmtpServer oServer = new SmtpServer("smtp.live.com");
    //        SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

    //        oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //        oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        oServer.Port = 587;
    //        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
    //        Console.WriteLine("start to send email over TLS...");
    //        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
    //        oSmtp.SendMail(oServer, oMail);
    //        Console.WriteLine("email sent successfully!");
    //        ret = 1;
    //    }
    //    catch (Exception ep)
    //    {
    //        Console.WriteLine("failed to send email with the following error:");
    //        Console.WriteLine(ep.Message);
    //        ret = 0;
    //    }
    //    return ret;
    //}

    protected void btnSendOffer_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            UserAcc objUa = new UserAcc();
            Student objS = new Student();
            string email_type = string.Empty;
            string Link = string.Empty;
            int sendmail = 0;
            string subject = string.Empty;
            string srnno = string.Empty;
            string pwd = string.Empty;
            int status = 0;

            Button btnUserno = sender as Button;
            Session["Studid"] = btnUserno.ToolTip;
            DataSet dsPop = new DataSet();
            dsPop = objFetchData.GetRecordByUANo(btnUserno.CommandArgument);
            if (dsPop.Tables[0].Rows.Count > 0)
            {
                ViewState["USERNO"] = dsPop.Tables[0].Rows[0]["USERNO"].ToString();
                ViewState["EMAILID"] = dsPop.Tables[0].Rows[0]["EMAILID"].ToString();
                //ViewState["IDNO"] = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO=" + Session["idno"].ToString());
                //ViewState["REGNO"] = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Session["idno"].ToString());
            }

            // string IDNO = Session["stuinfoidno"].ToString();

            int IDNOnew = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO='" + Convert.ToInt32(Session["Studid"]) + "'"));

            string DAmount = Convert.ToString(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0) TOTAL_AMT", "IDNO=" + IDNOnew));

            string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);

            string Username = string.Empty;
            string Password = string.Empty;

            string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + IDNOnew);
            string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "B.LONGNAME", "IDNO=" + IDNOnew);

            string Userno = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO=" + IDNOnew);
            string pass = objCommon.LookUp("USER_ACC", "UA_PWD", "UA_IDNO=" + IDNOnew);


            pass = clsTripleLvlEncyrpt.ThreeLevelDecrypt(pass.ToString());

            string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + IDNOnew);
            string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + IDNOnew);
            // Username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
            //  Password = objCommon.LookUp("ACD_USER_REGISTRATION", "User_Password", "USERNO=" + Userno);
            Username = Userno;
            //objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
            Password = pass;
            //objCommon.LookUp("ACD_USER_REGISTRATION", "User_Password", "USERNO=" + Userno);
            Session["Enrollno"] = srnno;
            DataSet ds = getModuleConfig();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

                if (sendmail == 1)
                {
                    subject = "Admission Confirmation Payment Link.";

                    string message = "";
                    message += "<p>Dear :<b>" + Name + "</b> </p>";
                    message += "<p></p>You are shortlisted for Provisional Allotment of seat in <b>" + college + "</b> in <b>" + Branchname + "</b><br/>You Can Pay First Installment Fees using the following Link and credentials. </td><p style=font-weight:bold;> " + MISLink + " </p><p>Username   : " + Username + " <br/>Password: " + Password + "</p><p style=font-weight:bold;>Fee Details: <br/><b>Note:</b> You can find your fee details after login in your portal using above login credentials.</p><p>The provisional admission shall be confirmed after establishing you are eligibility in the qualifying exam after declaration of the result by the examining Board/University as per the schedule notified by the University.</p><p>You will be required to submit self attested photo copies of the following documents at the time of final admission,once the University calls for the same.</p><p style=color:blue;>Documents required at the time of Confirmation of Admission: Originals along with one set of Self attested Photocopy.</p>";
                    message += " <table style='border: 1px solid black;border-collapse: collapse;'>";
                    message += " <tr >";
                    message += " <td style='border: 1px solid black;'>";
                    message += "JEE Mains Score Card (if appeared/ applicable)</td>";
                    message += "<td style='border: 1px solid black;'> Migration Certificate</td></tr>";
                    message += " <tr style='border: 1px solid black;'><td style='border: 1px solid black;'> 10th Marksheet & 12th Marksheet </td> <td style='border: 1px solid black;'> Transfer Certificate</td> </tr>";
                    message += "  <td style='border: 1px solid black;'>12th Mark-sheet</td><td style='border: 1px solid black;'>Character Certificate</td>";
                    message += "<tr></tr><tr><td style='border: 1px solid black;'>Graduation Final Year Mark-sheet (for PG Admissions)</td>";
                    message += "<td style='border: 1px solid black;'>Caste Certificate (if applicable)</td></tr>";
                    message += "<tr><td style='border: 1px solid black;'>Copy of Aadhar Card (UID)</td><td>OBC Non Creamy layer Certificate (if applicable)</td></tr>";
                    message += "<tr><td style='border: 1px solid black;'>3 Passport Size Photographs</td>";
                    message += "<td style='border: 1px solid black;'>Printout of application form (if filled online)</td></tr></table>";
                    message += "<p>The final admission is subject to clearing the document verification. In case you found ineligible for admission in the said program and denied by admission department after verification at any stage, the complete registration fee shall be refunded and admission will be cancelled.</p><p><b>Note:</b></p>";
                    message += "<p>1.All the documents must be uploaded on URL:<b>  " + MISLink + " </b> <br/>";
                    message += "2.After submission of registration fees, new user ID will be sent to your registered mail id separately.<br/>";
                    message += "3.Process of fee payment: Login using above credentials in <b> " + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.<br/>(Any query regarding admission send email to admission@jecrcu.edu.in)";
                    message += "<p style=font-weight:bold;>Thanks<br>Team Admissions <br>JECRC University, Jaipur</p>";

                    objS.EmailID = ViewState["EMAILID"].ToString();

                    //if (email_type == "1" && email_type != "")
                    //{
                    //    int reg = TransferToEmail(objS.EmailID, message, subject);
                    //}
                    //else if (email_type == "2" && email_type != "")
                    //{
                    //    Task<int> task = Execute(message, objS.EmailID, subject);
                    //    status = task.Result;
                    //}
                    //if (email_type == "3" && email_type != "")
                    //{
                    //    OutLook_Email(message, objS.EmailID, subject);
                    //}

                   // status = objSendEmail.SendEmail(objS.EmailID, message, subject); //Calling Method
                    string pageNo = Request.QueryString["pageno"].ToString();
                    status = objSendEmail.SendEmail_New(pageNo, objS.EmailID, message, subject); //Calling Method  
                }
            }

            if (status == 1)
            {
                objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //static async Task<int> Execute(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //        var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //        var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //        var client = new SendGridClient(apiKey);
    //        var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var subject = sub;
    //        var to = new EmailAddress(toEmailId, "");
    //        var plainTextContent = "";
    //        var htmlContent = Message;
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //        string res = Convert.ToString(response.StatusCode);
    //        if (res == "Accepted")
    //        {
    //            ret = 1;
    //            Console.WriteLine("Email Sent successfully!");

    //        }
    //        else
    //        {
    //            ret = 0;
    //            Console.WriteLine("Fail to send Mail!");
    //        }
    //        //attachments.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}
    //public int TransferToEmail(string useremail, string message, string subject)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //        if (dsconfig != null)
    //        {
    //            string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

    //            MailMessage msg = new MailMessage();
    //            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
    //            // fromPassword = Common.DecryptPassword(fromPassword);
    //            msg.From = new System.Net.Mail.MailAddress(fromAddress, "RCPIPER");
    //            msg.To.Add(new System.Net.Mail.MailAddress(useremail));
    //            msg.Subject = subject;
    //            msg.Body = message;
    //            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
    //            smtp.EnableSsl = true;
    //            smtp.Port = 587; // 587
    //            smtp.Host = "smtp.gmail.com";

    //            ServicePointManager.ServerCertificateValidationCallback =
    //            delegate(object s, X509Certificate certificate,
    //            X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //            {
    //                return true;
    //            };

    //            smtp.Send(msg);
    //            if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
    //            {
    //                return ret = 1;
    //                //Storing the details of sent email
    //            }
    //            else
    //            {
    //                return ret = 0;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //    return ret;

    //}
    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //foreach (ListViewDataItem dataitem in lvStudent.Items)
        //    {
        //    Label lblAppStatus = dataitem.FindControl("lblAppStatus") as Label;
        //    Button hdn = (Button)e.Item.FindControl("btnSendOffer");

        //    if (lblAppStatus.Text == "APPLIED")
        //        {            
        //        hdn.Visible = true;
        //        }
        //    else
        //        {
        //        hdn.Visible = false;
        //        }
        //    }
    }
}
