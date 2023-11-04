using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_REPORTS_NewAdmissionConsolidateReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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

                    this.PopulateDropDownList();

                    //txtToDate.Text = DateTime.Now.ToShortDateString();
                    //Focus on From Date
                    //txtFromDate.Focus();
                }
            }

            //Blank Div
            divMsg.InnerHtml = string.Empty;
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
                Response.Redirect("~/notauthorized.aspx?page=NewAdmissionConsolidateReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewAdmissionConsolidateReport.aspx");
        }
    }

    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowdStudentData(int value, string reportName)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string admbatch = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + ddlAdmbatch.SelectedValue);
           
            DataSet ds = new DataSet();

            ds = objSC.GetNewAdmissionStudentDetails(Convert.ToInt32(ddlAdmbatch.SelectedValue), value);


            if (ds.Tables[0].Rows.Count > 0)
            {

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + admbatch.Replace(" ", "_") + "_"+ reportName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +  ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowdStudentOverallData(int value, string reportName)
    {
        //Response.ClearContent();
        //Response.Clear();
        //Response.ContentType = "application/vnd.ms-excel";
        //string path = Server.MapPath("~/ExcelFormat/PreFormat_For_UploadStudentData.xls");
        //Response.AddHeader("Content-Disposition", "attachment;filename=\"PreFormat_For_UploadStudentData.xls\"");
        //Response.TransmitFile(path);
        //Response.Flush();
        //Response.End();
        string admbatch = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + ddlAdmbatch.SelectedValue);
        DataSet ds = objSC.GetNewAdmissionStudentDetails(Convert.ToInt32(ddlAdmbatch.SelectedValue), value);

        ds.Tables[0].TableName = admbatch.Replace(" ", "_") + "_" + "Admission_Status";
        ds.Tables[1].TableName = admbatch.Replace(" ", "_") + "_" + "Admission_Statistical";

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
            Response.AddHeader("content-disposition", "attachment;filename=Overall_Admission_Status- " + System.DateTime.Now.ToString("dd/MM/yyyy-hh:mm tt") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
  
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rdoOverall.Checked)
        {
            ShowdStudentOverallData(1, "Overall_Admission_Status");
        }
        else if (rdoInfoPending.Checked)
        {
            ShowdStudentData(2,"Info_FillUp_Pending");
        }
        else if (rdoApprovalPending.Checked)
        {
            ShowdStudentData(3,"Approval_Pending By_HOD");
        }
        else if (rdoApprovalByFinance.Checked)
        {
            ShowdStudentData(4, "Approval_Pending By_FinanceDept");
        }
        else if (rdoPaymentPending.Checked)
        {
            ShowdStudentData(5,"Payment_Pending_by_Student");
        }
        else
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select any one Report.", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    protected void rdoAdmission_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoAdmission.SelectedValue == "1")
        {
            divReport.Visible = true;
            divEmailSMS.Visible = false;
        }
        if (rdoAdmission.SelectedValue == "2")
        {
            divReport.Visible = false;
            divEmailSMS.Visible = true;
        }
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (rdoInfoIncomplete.Checked == true)
        {
            BindListView(1);
        }
        else if (rdoNotApproveHOD.Checked == true)
        {
            BindListViewHOD(2);
        }
        else if (rdoNotApproveFinance.Checked == true)
        {
            BindListView(3);
        }
        else if (rdoStudPaymentPending.Checked == true)
        {
            BindListView(4);
        }
        else
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select any one Status.", this.Page);
        }
    }
    private void BindListView(int ReportType)
    {
        DataSet ds = objSC.StudentListToSendEmailSMS(Convert.ToInt32(ddlAdmbatch.SelectedValue), ReportType);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds;
            lvStudents.DataBind();
            Panel1.Visible = true;
            Panel2.Visible = false;
            divSendOptions.Visible = true;
            btnSendSMS.Visible = true;
            divSubject.Visible = true;
            //hdncount1.Value = lvStudents.Items.Count.ToString();

            btnSendSMS.Visible = true;
        }
        else
        {

            objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);

        }
    }

    private void BindListViewHOD(int ReportType)
    {
        DataSet ds = objSC.StudentListToSendEmailSMS(Convert.ToInt32(ddlAdmbatch.SelectedValue), ReportType);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvHOD.DataSource = ds;
            lvHOD.DataBind();
            Panel2.Visible = true;
            Panel1.Visible = false;
            divSendOptions.Visible = true;
            btnSendSMS.Visible = true;
            divSubject.Visible = true;
            //hdncount1.Value = lvStudents.Items.Count.ToString();

            btnSendSMS.Visible = true;
        }
        else
        {

            objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);

        }
    }

    protected void rbNet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbNet.SelectedValue == "1")
        {
            btnSendSMS.Enabled = true;
            //txtSubject.Visible = false;
            btnSendSMS.Text = "Send SMS";
            divSubject.Visible = false;
            txtSubject.Text = "";
        }
        else if (rbNet.SelectedValue == "2")
        {
            btnSendSMS.Enabled = true;
            btnSendSMS.Text = "Send Email";
            divSubject.Visible = true;
        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {

        try
        {
            //if (rbNet.SelectedValue == "1")
            //{
            //    if (txtMatter.Text == "" || txtMatter.Text==string.Empty)
            //    {
            //        objCommon.DisplayMessage(this.Page, "Please Enter Message", this.Page);
            //        return;
            //    }

            //}
            //if (rbNet.SelectedValue == "2")
            //{
                if (txtSubject.Text == "" || txtSubject.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Subject", this.Page);
                    return;
                }
                else if (txtMatter.Text == "" || txtMatter.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Message", this.Page);
                    return;

                }
            //}
                if (rdoNotApproveHOD.Checked == true)
                {
                    int count = 0;
                    foreach (ListViewDataItem item in lvHOD.Items)
                    {
                        CheckBox chek = item.FindControl("cbRowHod") as CheckBox;
                        Label lblMobile = item.FindControl("lblHodMobile") as Label;
                        Label lblEmail = item.FindControl("lblHodEmail") as Label;
                        int status = 0;
                        if (chek.Checked == true)
                        {
                            count++;                            
                            Task<int> task = Execute(txtMatter.Text, lblEmail.Text, txtSubject.Text);
                            status = task.Result;
                        }

                    }

                    if (count != 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Mail Sent Successfully!!", this.Page);
                        txtMatter.Text = "";
                        txtSubject.Text = "";
                        return;
                    }

                    if (count == 0)
                    {
                        // objCommon.DisplayMessage("Please Select at least one Student!", this.Page);
                        objCommon.DisplayMessage(this.Page, "Please Select at least one HOD!", this.Page);
                        return;

                    }
                }
                else
                {
                    string StudRegNO = string.Empty;
                    int count = 0;
                    foreach (ListViewDataItem item in lvStudents.Items)
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblMobile = item.FindControl("lblMobile") as Label;
                        Label lblEmail = item.FindControl("lblEmail") as Label;
                        int status = 0;
                        if (chek.Checked == true)
                        {
                            count++;
                            StudRegNO += chek.ToolTip + "$";
                            Task<int> task = Execute(txtMatter.Text, lblEmail.Text, txtSubject.Text);
                            status = task.Result;

                        }

                    }

                    if (count != 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Mail Sent Successfully!!", this.Page);
                        txtMatter.Text = "";
                        txtSubject.Text = "";
                        return;
                    }

                    if (count == 0)
                    {
                        // objCommon.DisplayMessage("Please Select at least one Student!", this.Page);
                        objCommon.DisplayMessage(this.Page, "Please Select at least one Student!", this.Page);
                        return;

                    }
                }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";

            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            // var myMessage = new SendGridMessage();

            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }
}