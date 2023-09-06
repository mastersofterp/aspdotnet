using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using mastersofterp_MAKAUAT;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using ClosedXML.Excel;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data;

public partial class ACADEMIC_REPORTS_StudentInformation_Status : System.Web.UI.Page
{
    Common objCommon = new Common();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommon.FillDropDownList(ddlStudentInfo, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");                //Added by Ruchika on 18.08.2022
        }
    }

    protected void lnReport_Click(object sender, EventArgs e)
    {
        AdmissionBatch();
    }
    protected void AdmissionBatch()                                  //Added by Ruchika Dhakate on 18.08.2022
    {
        try
        {
            int BATCHNO = Convert.ToInt32(ddlStudentInfo.SelectedValue);
           // string BATCHNO = ddlStudentInfo.SelectedItem.Text;
            DataSet ds = Admission_Excel(BATCHNO);
            GridView GVEmpChallan = new GridView();

            if (ds.Tables[0].Rows.Count > 0)
            {

                GVEmpChallan.DataSource = ds.Tables[0];
                GVEmpChallan.DataBind();


                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                //HeaderCell.Text = ds.Tables[0].Rows[0]["COLLEGE NAME"].ToString();
                HeaderCell.Text = " College Name : " + ds.Tables[0].Rows[0]["School"].ToString();
                HeaderCell.ColumnSpan = 18;
                HeaderCell.BackColor = System.Drawing.Color.Navy;
                HeaderCell.ForeColor = System.Drawing.Color.White;         
                HeaderCell.Font.Bold = true;
                HeaderCell.Font.Size = 15;
                HeaderCell.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow.Cells.Add(HeaderCell);
                GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);

                //Header Row 2
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell3 = new TableCell();
               
               // HeaderCell3.Text = " Admission Batch : " + ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                HeaderCell3.Text = " Admission Batch : " + "-" + ddlStudentInfo.SelectedItem.Text;
                HeaderCell3.ColumnSpan = 18;   //Added by column no 14-06-2022 
                HeaderCell3.BackColor = System.Drawing.Color.Navy;
                HeaderCell3.ForeColor = System.Drawing.Color.White;
                HeaderCell3.Font.Bold = true;
                HeaderCell3.Font.Size = 15;
                HeaderCell3.Attributes.Add("style", "text-align:center !important;");
                HeaderGridRow1.Cells.Add(HeaderCell3);
                GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);


                string attachment = "attachment; filename=" + "Admission Batch Excel.xls";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVEmpChallan.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StudentInformationStatus.aspx.lnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    
    public DataSet Admission_Excel(int BATCHNO)                         //Added by Ruchika Dhakate on 18.08.2022
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_BATCHNO ", BATCHNO);


             ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_INFO_STATUS_REPORT ", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            //   throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.EmployeeARREAR_DIFF-> " + ex.ToString());
        }
        return ds;
    }
}