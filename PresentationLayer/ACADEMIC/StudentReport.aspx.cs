using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_StudentReport : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                //this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                //this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                //this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");

            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
        }
        divMsg.InnerHtml = "";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        StudentController studCont = new StudentController();
        string searchText = txtSearchText.Text.Trim();
        string searchBy = (rdoEnrollmentNo.Checked ? "enrollmentno" : (rdoStudentName.Checked ? "name" : (rdoRollNo.Checked ? "regno" : "idno")));
        DataSet ds = studCont.RetrieveStudentDetails(searchText, searchBy); ;
        if (ds != null && ds.Tables.Count > 0)
        {
            lvStudentRecords.DataSource = ds.Tables[0];
            lvStudentRecords.DataBind();
        }
    }

    protected void btnShowReport(object sender, EventArgs e)
    {
        if (rdoVerticalReport.Checked)
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReport(param, "STUDENTADMISSIONDETAILS", "Admission_Slip_Confirm_PHD_General.rpt");
        }
        else
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReportHz(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal.rpt");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rdoVerticalReport.Checked)
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReport(param, "STUDENTADMISSIONDETAILS", "StudentReport1.rpt");
        }
        else
        {
            ImageButton btnShowRpt = sender as ImageButton;
            string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
            this.ShowReportHz(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal.rpt");
        }
        //if (rdoVerticalReport.Checked)
        //{
        //    string param = this.GetParamsForAllStudent();
        //    this.ShowReport(param, "STUDENTADMISSIONDETAILS", "StudentReport1.rpt");
        //    return;
        //}
        //else
        //{
        //    string param = this.GetParamsForAllStudent();
        //    this.ShowReport(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal.rpt");
        //    return;
        //}
        // if (rdoHorizontalReport.Checked)
        //    {
        //        ImageButton btnShowRpt = sender as ImageButton;
        //        Response.Write(btnShowRpt.CommandArgument);
        //        string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
        //        this.ShowReport(param, "STUDENTADMISSIONDETAILS", "StudentReport1.rpt");
        //        return;
        //    }
        //    else
        //    {
        //        ImageButton btnShowRpt = sender as ImageButton;
        //        string param = this.GetParamsForSingleStudent(btnShowRpt.CommandArgument);
        //        this.ShowReport(param, "STUDENTADMISSIONDETAILS", "studentReportHorizontal.rpt");
        //    }
    }
    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + param;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReportHz(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + param;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetParamsForAllStudent()
    {
        //string param = "@P_IDNO=0,@P_DEGREENO=" + ddlDegree.SelectedValue;
        //param += ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_YEARNO=" + ddlYear.SelectedValue;
        //param += ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=0" + "*StudentLastQualifiedDetails.rpt";
        //return param;
        //string param = "@P_IDNO=0,@P_DEGREENO=" + ddlDegree.SelectedValue;
        //param += ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_YEARNO=" + ddlYear.SelectedValue;
        //param += ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
        //return param;
        return string.Empty;
    }

    private string GetParamsForSingleStudent(string idno)
    {
        //string param= "@P_IDNO="+ idno + ",@P_DEGREENO=0,@P_BRANCHNO=0,@P_YEARNO=0,@P_SEMESTERNO=0,@P_IDNO=" + idno + "*StudentLastQualifiedDetails.rpt" ;
        //return param;
        string param = "@P_IDNO=" + idno;//",@P_DEGREENO=0,@P_BRANCHNO=0,@P_YEARNO=0,@P_SEMESTERNO=0";
        return param;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
