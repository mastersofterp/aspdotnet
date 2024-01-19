using ClosedXML.Excel;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_REPORTS_LateFineCancelReport : System.Web.UI.Page
{
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                }
                    
            }           
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
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        Common objCommon = new Common();
        try
        {
            LateFeeController lateFeeController = new LateFeeController();
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text);
            DataSet ds = lateFeeController.GET_LATE_FEES_CANCEL_STUD_DETAILS(FromDate, ToDate);
           // DataSet ds = GET_LATE_FEES_CANCEL_STUD_DETAILS(FromDate, ToDate);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Late Fees Cancel Students List";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= Late_Fee_Cancel_Students_Report.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                objCommon.DisplayUserMessage(pnlFeeTable, "Data Not Found.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(pnlFeeTable, ex.ToString(), this.Page);
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    // Added By Shailendra K on dated 18.02.2023 for tkt no.39324
    public DataSet GET_LATE_FEES_CANCEL_STUD_DETAILS(DateTime FromDT, DateTime ToDT)
    {
        DataSet ds = null;
        try
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@P_FROMDT", FromDT.ToString("dd-MMM-yyyy"));
            objParams[1] = new SqlParameter("@P_TODT", ToDT.ToString("dd-MMM-yyyy"));
            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_LATE_FEE_CANCEL_STUD_DETAILS", objParams);
        }
        catch (Exception ex)
        {

            return ds;
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LateFeeController.GET_LATE_FEES_CANCEL_STUD_DETAILS-> " + ex.ToString());
        }
        return ds;
    }
}