using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using System.IO;


public partial class LoanApplicableStudentList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    this.FillDropdown();

                }
            }
            // this.BindListView();
        }
        catch
        {
            throw;
        }
    }


    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlacademicYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0", "ACADEMIC_YEAR_ID DESC");
            //ddlacademicYear.SelectedIndex = 1;

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE ", "DEGREENO", "DEGREENAME", "DEGREENO>0 ", "DEGREENO");

            objCommon.FillDropDownList(ddlyear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0", "YEAR");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void BindListview()
    {
        try
        {
            DataSet ds = objSC.GetStudentData(Convert.ToInt32(ddlacademicYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlyear.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvStudents.DataSource = ds;
                lvStudents.DataBind();

                foreach (ListViewDataItem item in lvStudents.Items)
                {
                    CheckBox chkAccept = item.FindControl("chkloan") as CheckBox;
                    TextBox txtloan = item.FindControl("txtapproveloan") as TextBox;
                    TextBox txtddno = item.FindControl("txtddno") as TextBox;
                    TextBox txtdate = item.FindControl("txtDate") as TextBox;
                    TextBox txtbankdeatils = item.FindControl("txtbankdetails") as TextBox;
                    if (txtloan.Text != "")
                    {
                        chkAccept.Checked = true;
                        txtloan.Enabled = true;
                        txtddno.Enabled = true;
                        txtdate.Enabled = true;
                        txtbankdeatils.Enabled = true;
                    }
                    else
                    {

                        chkAccept.Checked = false;
                        txtloan.Enabled = false;
                        txtddno.Enabled = false;
                        txtdate.Enabled = false;
                        txtbankdeatils.Enabled = false;
                        
                    }
                }
              

                lvStudents.Visible = true;
                btnShow.Visible = true;
                btnSubmit.Visible = true;
                btnPrintReport.Visible = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = false;
                objCommon.DisplayMessage(this.updLoan, "No Record found for selected criteria.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListview();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        int id = 0;
        string regno = string.Empty;
        int loan = 0;
        int ddno = 0;
        string Fdate = string.Empty;
        string bankdetails = string.Empty;
        int ret = 0;
        try
        {
            int count = 0;

            int academic_year = Convert.ToInt32(ddlacademicYear.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            int year = Convert.ToInt32(ddlyear.SelectedValue);
            int CREATED_BY = 1;
            string IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox chkAccept = item.FindControl("chkloan") as CheckBox;
                Label lbidno = item.FindControl("lblregno") as Label;
                id = Convert.ToInt32(lbidno.ToolTip);
                Label lblregno = item.FindControl("lblregno") as Label;
                regno = lblregno.Text;
                TextBox txtloan = item.FindControl("txtapproveloan") as TextBox;
                TextBox txtddno = item.FindControl("txtddno") as TextBox;
                TextBox txtdate = item.FindControl("txtDate") as TextBox;
                TextBox txtbankdeatils = item.FindControl("txtbankdetails") as TextBox;
                if (chkAccept.Checked == true && chkAccept.Enabled == true)
                {
                    if (txtloan.Text == string.Empty || txtloan.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Approve Loan Amount.", this.Page);
                        return;
                    }
                    {
                        loan = Convert.ToInt32(txtloan.Text);
                    }

                    if (txtddno.Text == string.Empty || txtddno.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Ddno/Transaction No.", this.Page);
                        return;
                    }
                    {
                        ddno = Convert.ToInt32(txtddno.Text);
                    }
                  
                    if (txtdate.Text == string.Empty || txtdate.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Date.", this.Page);
                        return;
                    }
                    else
                    {
                        Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtdate.Text)));
                        Fdate = Fdate.Substring(0, 10);
                    }

                    if (txtbankdeatils.Text == string.Empty || txtbankdeatils.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Bank Details.", this.Page);
                        return;
                    }
                    {
                        bankdetails = txtbankdeatils.Text;
                    }

                    if (regno != string.Empty && loan != 0 && ddno != 0 && Fdate != string.Empty && bankdetails != string.Empty)
                    {
                        ret = objSC.AddstudLoanapplicable(id, academic_year, degreeno, year, loan, ddno, Fdate, bankdetails, CREATED_BY, IPADDRESS);

                    }

                    CustomStatus cs = (CustomStatus)objSC.AddstudLoanapplicable(id, academic_year, degreeno, year, loan, ddno, Fdate, bankdetails, CREATED_BY, IPADDRESS);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        count++;
                    }

                }
            }

            if (count > 0)
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
               
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }

    }


    protected void btnPrintReport_Click(object sender, EventArgs e)
    {

        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet ds = objSC.LoanApllicableReport(Convert.ToInt32(ddlacademicYear.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename="+ddlacademicYear.SelectedItem.Text+"_StudentLoanApplicable.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList.btnExcelReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    //protected void chkloan_CheckedChanged(object sender, EventArgs e)
    //{
        
    //    foreach (ListViewDataItem item in lvStudents.Items)
    //    {
    //        CheckBox chkAccept = item.FindControl("chkloan") as CheckBox;

    //        TextBox txtloan = item.FindControl("txtapproveloan") as TextBox;
    //        TextBox txtddno = item.FindControl("txtddno") as TextBox;
    //        TextBox txtdate = item.FindControl("txtDate") as TextBox;
    //        TextBox txtbankdeatils = item.FindControl("txtbankdetails") as TextBox;
    //     //   CheckBox chkHead = this.lvStudents.Controls[0].FindControl("chkBoxFeesTransfer") as CheckBox;
    //        if (chkAccept.Checked == true)
    //        {
    //            txtloan.Enabled = true;
    //            txtddno.Enabled = true;
    //            txtdate.Enabled = true;
    //            txtbankdeatils.Enabled = true;
                
    //        }
    //        else
    //        {
    //            txtloan.Enabled = false;
    //            txtddno.Enabled = false;
    //            txtdate.Enabled = false;
    //            txtbankdeatils.Enabled = false;
    //            //chkHead.Checked = false;
    //        }
    //    }
    //}

    //protected void txtDate_TextChanged(object sender, EventArgs e)
    //{
    //    foreach (ListViewDataItem item in lvStudents.Items)
    //    {
    //        CheckBox chkAccept = item.FindControl("chkloan") as CheckBox;

    //        TextBox txtloan = item.FindControl("txtapproveloan") as TextBox;
    //        TextBox txtddno = item.FindControl("txtddno") as TextBox;
    //        TextBox txtdate = item.FindControl("txtDate") as TextBox;
    //        TextBox txtbankdeatils = item.FindControl("txtbankdetails") as TextBox;
    //        if (txtdate.Text != "" && Convert.ToDateTime(txtdate.Text) > DateTime.Today)
    //        {
    //            objCommon.DisplayMessage(this.Page, "Future Date Is Not Acceptable.", this.Page);
    //            txtdate.Text = string.Empty;
    //        }
    //    }
    //}
    protected void ddlacademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlacademicYear.SelectedIndex > 0)
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            btnSubmit.Visible = false;
            btnPrintReport.Visible = false;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            btnSubmit.Visible = false;
            btnPrintReport.Visible = false;
        }
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex > 0)
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            btnSubmit.Visible = false;
            btnPrintReport.Visible = false;
        }
    }
}