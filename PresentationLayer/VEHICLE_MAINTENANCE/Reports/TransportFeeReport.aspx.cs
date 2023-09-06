//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   :Non- ACADEMIC                                                             
// PAGE NAME     : Transport FEES RELATED REPORT                                 
// CREATION DATE : 08-JUne-2020                                                     
// CREATED BY    : Vijay Andoju                                                       
// MODIFIED DATE : 
// MODIFIED BY   :                                                         
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class VEHICLE_MAINTENANCE_Reports_TransportFeeReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController ObjCon = new VMController();
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
               

               
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    PopulateDropDown();

            }
        }
    }

    private void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENO, ', ',B.BRANCHNO)AS ID", "(D.DEGREENAME+'-'+B.LONGNAME)AS DEGBRANCH", "S.DEGREENO>0", "S.DEGREENO");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");       
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlDegree.SelectedValue != null && ddlDegree.SelectedValue != "0")
        {
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]);
            int branchno = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]); 
            ddlAdmBatch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + degreeno));
            if (ViewState["YearWise"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEAR<>0", "YEAR");
            }
            else
            {                
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO<>0", "SEMESTERNO");
                ddlSemester.Focus();
            }
            ddlAdmBatch.Focus();
        }
        else
        {
            objCommon.DisplayMessage(pnlFeeTable, "Please Select Degree", this.Page);
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO=cd.BRANCHNO) INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO", "DISTINCT CONCAT(A.DEGREENO, ', ',BR.BRANCHNO)AS ID", "(A.DEGREENAME+'-'+BR.LONGNAME)AS DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "ID");
            ddlDegree.Focus();
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlAdmBatch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            objCommon.DisplayMessage(pnlFeeTable, "Please Select College", this.Page);
            ddlCollege.Focus();
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rdbUserType.SelectedValue == "1")
        {
            ShowTransportFeesReport("TransportFeesReport", "rptTransportFeeReport.rpt");
        }
        else
        {
            ShowEmployeeReport("EmployeeTransportReport", "rptEmployeeTransportFeeReport.rpt");
        }
    }


    private void ShowEmployeeReport(string reportTitle, string rptFileName)//
    {
        try
        {
           

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));           
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=EmployeeTransportFeeReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            string col = objCommon.LookUp("REFF", "College_code", "");
           
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

             ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowTransportFeesReport(string reportTitle, string rptFileName)//
    {
        try
        {
            if (Convert.ToInt32(ddlDegree.SelectedIndex) > 0)
            {
                ViewState["degreeno"] = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]);
                ViewState["branchno"] = Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1]);
            }
            else
            {
                ViewState["degreeno"] = "0";
                ViewState["branchno"] = "0";
            }


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=TransportFeeReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            string col = objCommon.LookUp("REFF", "College_code", "");
            //url += "&param=@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[0]) + ",@P_BRANCNO=" + Convert.ToInt32(ddlDegree.SelectedValue.Split(',')[1])  + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedValue = "0";
        PopulateDropDown();
    }

    protected void btnPaidReport_Click(object sender, EventArgs e)
    {
        report_excel_new(1,"TransportFeePaidList");
    }
    protected void btnUnpaidReport_Click(object sender, EventArgs e)
    {
        report_excel_new(0,"TransportFeeUnPaidList");

    }
    public void report_excel_new(int ReconType,string FileName)
    {
        GridView GVStudData = new GridView();

       
        DataSet ds = ObjCon.GET_REPORT_EXCEL_NEW(Convert.ToInt32(ddlCollege.SelectedValue), "0", ReconType);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GVStudData.DataSource = ds;
            GVStudData.DataBind();

            string attachment = "attachment;filename=" + FileName + "_.xls";
            string filename = attachment.Replace(" ", "_");
            Response.ClearContent();
            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AddHeader("content-disposition", filename);
            Response.ContentType = "application/vnd.xls";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStudData.HeaderRow.Style.Add("background-color", "#e3ac9a");
            //  GVStudData.BackColor = Color.Magenta;
            GVStudData.Style.Add("background-color", "SeaShell");
            GVStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "No Data Found", this.Page);
        }

    }


   

}
