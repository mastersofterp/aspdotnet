using System;
using System.Data;
using System.Web.UI;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using ClosedXML.Excel;
using System.Web.UI.WebControls;

public partial class FetchDetailRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    FetchDataController objFetchData = new FetchDataController();
    
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
                objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_ADMISSION_CONFIG C INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO)", "DISTINCT C.DEGREENO", "DEGREENAME", "D.DEGREENO > 0", "DEGREENAME");
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
            DataSet ds = objFetchData.GetApplicantUserList(Convert.ToInt32(ddlAdmbatch.SelectedValue), programmeType, Convert.ToInt32(rdoStatus.SelectedValue), degree);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                //pnlStudent.Visible = true;
                lvStudent.Visible = true;
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                //pnlStudent.Visible = false;
                objCommon.DisplayMessage(this.Page, "No Student Found", this.Page);
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
        int branchno= ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
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
        int admbatch=ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        int Application_Status = Convert.ToInt32(rdoStatus.SelectedValue);
        int degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int branchno= ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        DataSet ds= objFetchData.GetFormFillingStatus(admbatch, Application_Status, degreeNo, branchno);
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
        }
    }
    protected void btnStudDetails_Click(object sender, EventArgs e)
    {
        DataSet dsStudData = null;
        int admbatch=ddlAdmbatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmbatch.SelectedValue) : 0;
        if (ddlProgrammeType.SelectedValue == "2")
        {
            dsStudData = objFetchData.GetStudentsDumpData_PG(Convert.ToInt32(ddlDegree.SelectedValue), admbatch, Convert.ToInt32(ddlBranch.SelectedValue));
        }
        else
        {

            dsStudData = objFetchData.GetStudentCompleteDetails(Convert.ToInt32(ddlDegree.SelectedValue), admbatch, Convert.ToInt32(ddlBranch.SelectedValue));
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
}
