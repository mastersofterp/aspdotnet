using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;

public partial class ACADEMIC_POSTADMISSION_ADMPAdhocReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            FillDropDown();

        }
    }
    public void ControlVisibility()
    {
        divADMPBatch.Visible = false;
        divADMPtype.Visible = false;
        divDegree.Visible = false;
        divBranch.Visible = false;
    }

    public void FillDropDown()
     {
        try
        {

            objCommon.FillDropDownList(ddlReportName, "ACD_ADMP_Adhoc_Report_Configuration", "ADHOCID", "REPORTNAME", "ADHOCID > 0 AND ISNULL(IsActive,0)=1", "ADHOCID");
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "DISTINCT BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlADMPtype, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION > 0 AND ACTIVESTATUS=1 AND UA_SECTIONNAME IN('UG','PG')", "UA_SECTION DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("ADMPAdhocReports.FillDropDown() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }
    protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;

        try
        {
            btnDisplay.Visible=false;
            if (ddlReportName.SelectedIndex > 0)
            {
                string chkd = objCommon.LookUp("ACD_ADMP_ADHOC_REPORT_CONFIGURATION", "IsDisplay", "ADHOCID=" + ddlReportName.SelectedValue);

                if (chkd != "" || chkd != string.Empty)
                {
                    if (Convert.ToBoolean(chkd) == true)
                    {
                        btnDisplay.Visible = true;
                    }
                    else
                    {
                        btnDisplay.Visible = false;
                    }
                }
                else
                {
                    btnDisplay.Visible = false;
                }

                ds = objCommon.FillDropDown("ACD_ADMP_ADHOC_REPORT_CONFIGURATION RD LEFT OUTER JOIN  ACD_ADMP_ADHOC_REPORT_CONFIGURATIONDETAILS RC ON(RD.ADHOCID=RC.ADHOCID)",
                    "RD.ADHOCID,REPORTNAME,PROCEDURENAME", "PROCEDUREPARAMNAME,PAGECONTROLID", "RD.ADHOCID=" + ddlReportName.SelectedValue, "");

                ControlVisibility();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["PAGECONTROLID"].Equals("ddlAdmissionBatch"))
                    {
                        divADMPBatch.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[i]["PAGECONTROLID"].Equals("ddlADMPtype"))
                    {
                        divADMPtype.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[i]["PAGECONTROLID"].Equals("ddlDegree"))
                    {
                        divDegree.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[i]["PAGECONTROLID"].Equals("ddlBranch"))
                    {
                        divBranch.Visible = true;
                    }
                }
            }
            else
            {
                btnCancel_Click(sender, e);
            }
            ddlAdmissionBatch.SelectedIndex = 0;
            ddlADMPtype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("ADMPAdhocReports. ddlReportName_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

    protected void ddlADMPtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.Items.Clear();
            ddlBranch.Items.Clear();

            if (ddlADMPtype.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlADMPtype.SelectedValue, "D.DEGREENO");
            }
            //ddlDegree.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("ADMPAdhocReports.ddlADMPtype_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlBranch.Items.Clear();

            if (ddlADMPtype.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH BH LEFT OUTER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = BH.BRANCHNO)", "BH.BRANCHNO", "BH.LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DB.ACTIVESTATUS = 1", "BH.LONGNAME");
            }
            //ddlBranch.Items.Insert(0, new ListItem("Please Select", "Please Select"));
            ddlBranch.SelectedIndex = 0;
            ddlBranch.Focus();

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ADMPAdhocReports.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void btnReport_Click(object sender, System.EventArgs e)
    {
        DataSet ds = null;

        try
        {

            if (ddlReportName.SelectedIndex > 0)
            {
                ds = objCommon.FillDropDown("ACD_ADMP_ADHOC_REPORT_CONFIGURATION RD LEFT OUTER JOIN  ACD_ADMP_ADHOC_REPORT_CONFIGURATIONDETAILS RC ON(RD.ADHOCID=RC.ADHOCID)",
                    "RD.ADHOCID,REPORTNAME,PROCEDURENAME", "PROCEDUREPARAMNAME,PAGECONTROLID,FORM_TABLIST", "RD.ADHOCID=" + ddlReportName.SelectedValue, "");

                int paramCount = 0;
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    if (ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"] == DBNull.Value || string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString()))
                //    {
                //        continue;
                //    }
                //    else
                //    {
                //        paramCount++;
                //    }
                //}
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["PAGECONTROLID"] == DBNull.Value || string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PAGECONTROLID"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        paramCount++;
                    }
                }
                //Start Dynamic
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[paramCount];
                int j = 0;
                string procName = string.Empty, reportName = string.Empty, formTabList = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString().ToLower().Equals("@p_admisionbatchno"))
                    //{
                    //    objParams[j] = new SqlParameter(ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString(), ddlAdmissionBatch.SelectedValue.ToString());
                    //    j++;
                    //}
                    if (ds.Tables[0].Rows[i]["PAGECONTROLID"].ToString()=="ddlAdmissionBatch")
                    {
                        objParams[j] = new SqlParameter(ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString(), ddlAdmissionBatch.SelectedValue.ToString());
                        j++;
                    }
                   // else if (ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString().ToLower().Equals("@p_admisiontype"))
                    else if (ds.Tables[0].Rows[i]["PAGECONTROLID"].ToString() == "ddlADMPtype")
                    {
                        objParams[j] = new SqlParameter(ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString(), ddlADMPtype.SelectedValue.ToString());
                        j++;
                    }
                   // else if (ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString().ToLower().Equals("@p_degreeno"))
                    else if (ds.Tables[0].Rows[i]["PAGECONTROLID"].ToString() =="ddlDegree")
                    {
                        objParams[j] = new SqlParameter(ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString(), ddlDegree.SelectedValue.ToString());
                        j++;
                    }
                    //else if (ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString().ToLower().Equals("@p_branchno"))
                    else if (ds.Tables[0].Rows[i]["PAGECONTROLID"].ToString() == "ddlBranch")
                    {
                        objParams[j] = new SqlParameter(ds.Tables[0].Rows[i]["PROCEDUREPARAMNAME"].ToString(), ddlBranch.SelectedValue.ToString());
                        j++;
                    }

                    if (string.IsNullOrEmpty(procName))
                    {
                        procName = ds.Tables[0].Rows[i]["PROCEDURENAME"].ToString();
                        reportName = ds.Tables[0].Rows[i]["REPORTNAME"].ToString();
                        formTabList = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["FORM_TABLIST"].ToString()) ? null : ds.Tables[0].Rows[i]["FORM_TABLIST"].ToString();

                    }

                }

                if (procName.Length > 0)
                {
                    DataSet ds_result = new DataSet();
                    if (objParams.Length > 0)
                    {
                        ds_result = GetCommonReportData(procName, objParams);
                     
                    }
                    else
                    {
                        ds_result = GetWithoutParamReportData(procName);
                    }

                    if (ds_result.Tables.Count > 1)
                    {
                        List<string> commandArgs = new List<string>();
                        if (formTabList.Length > 0)
                        {
                            commandArgs = formTabList.Split(new char[] { ',' }).ToList();
                        }
                        if (commandArgs.Count > 0)
                        {
                            for (int i = 0; i < commandArgs.Count; i++)
                            {
                                if (i < ds_result.Tables.Count)
                                {
                                    ds_result.Tables[i].TableName = commandArgs[i];
                                }
                            }
                        }

                        /*ds_result.Tables[0].TableName = "Board";
                        ds_result.Tables[1].TableName = "Subject";
                        ds_result.Tables[2].TableName = "Group";
                        ds_result.Tables[3].TableName = "Subject Type";
                        ds_result.Tables[4].TableName = "Board Subject Configuration";
                        ds_result.Tables[5].TableName = "Add Subject";
                        ds_result.Tables[6].TableName = "Board Grade Scheme";
                        ds_result.Tables[7].TableName = "Reservation Configuration";
                        ds_result.Tables[8].TableName = "Program";
                        ds_result.Tables[9].TableName = "Test Score";*/
                        GetExelReportMultipleTab(ds_result, reportName);
                    }
                    else
                    {

                        DataGrid Gr = new DataGrid();

                        if (ds_result.Tables[0].Rows.Count > 0)
                        {
                            if (ds_result.Tables[0].Rows.Count > 0)
                            {
                                DataTable dt = ds_result.Tables[0];

                                StringBuilder htmlTable = new StringBuilder();
                                htmlTable.Append("<table style='border-collapse: collapse;'>");

                                string CollegeName = objCommon.LookUp("REFF", "COLLEGENAME", "");
                                string reportNameRow = "<tr><td colspan='" + dt.Columns.Count + "' style='font-weight:bold; font-size: 18px;'>" + reportName + " (" + CollegeName + ")</td></tr>";
                                htmlTable.Append(reportNameRow);

                                // Generate the column headers row with borders
                                htmlTable.Append("<tr>");
                                foreach (DataColumn col in dt.Columns)
                                {
                                    htmlTable.Append("<th style='border: 1px solid black;'>" + col.ColumnName + "</th>");
                                }
                                htmlTable.Append("</tr>");

                                // Generate the data rows with borders
                                foreach (DataRow row in dt.Rows)
                                {
                                    htmlTable.Append("<tr>");
                                    foreach (DataColumn col in dt.Columns)
                                    {
                                        htmlTable.Append("<td style='border: 1px solid black;'>" + row[col] + "</td>");
                                    }
                                    htmlTable.Append("</tr>");
                                }

                                htmlTable.Append("</table>");

                                string Attachment = "Attachment; FileName=" + reportName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                                Response.ClearContent();
                                Response.AddHeader("content-disposition", Attachment);
                                Response.Write(htmlTable.ToString());
                                Response.End();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("data not found against selected filters...!", this.Page);
                            return;
                        }
                    }
                }
                //end
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("ADMPAdhocReports. ddlReportName_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace, this.Page);
            else
                objCommon.DisplayMessage("Server Unavailable.", this.Page);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
        ControlVisibility();
    }
    public DataSet GetCommonReportData(string storedprocedure, SqlParameter[] objParams)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);


            ////SqlParameter[] objParams = new SqlParameter[0];

            ds = objsqlhelper.ExecuteDataSetSP(storedprocedure, objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.Common.GetCommonReportData-> " + ex.ToString());
        }
        return ds;
    }
    public DataSet GetWithoutParamReportData(string storedprocedure)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);

            ds = objsqlhelper.ExecuteDataSet(storedprocedure);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.Common.GetWithoutParamReportData-> " + ex.ToString());
        }
        return ds;
    }
    private void ClearData()
    {
        ddlReportName.SelectedIndex = 0;
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlADMPtype.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        btnDisplay.Visible = false;

    }

    public void GetExelReportMultipleTab(DataSet ds, string ReportName)
    {
        //Set Name of DataTables.  

        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            {                            //Add DataTable as Worksheet.                            
                wb.Worksheets.Add(dt);
            }                         //Export the Excel file.                        
            Response.Clear(); Response.Buffer = true; Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=" + ReportName + ".xlsx");
            Response.AddHeader("content-disposition", "attachment;filename=" + ReportName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush(); Response.End();
            }
        }

    }
}