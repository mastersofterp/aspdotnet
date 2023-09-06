using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
//using System.Transactions;
using System.Text.RegularExpressions;
using System.Design;
using System.Drawing;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports_STORES_ComparativeReport : System.Web.UI.Page
{
    Str_Vendor_Quotation_Entry_Controller objquotentry = new Str_Vendor_Quotation_Entry_Controller();
    string quotno;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        quotno = Request.QueryString["quotno"].ToString();
        if (quotno != string.Empty)
        {
            DataSet dsItemDetail = objquotentry.GetDetailForComparative(quotno);
            //   NewReport();

            if (dsItemDetail != null)
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument objDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                objDoc.Load(this.Server.MapPath("str_comparative_statement.rpt"));

                DataView dvParty = dsItemDetail.Tables[0].DefaultView;
                DataTable dtparty = dvParty.ToTable();
            

                dvParty.RowFilter = "item='PARTY'";

                objDoc.SetParameterValue("@party1", dtparty.Rows[0]["PARTY1"].ToString());
                objDoc.SetParameterValue("@party2", dtparty.Rows[0]["PARTY2"].ToString());
                objDoc.SetParameterValue("@party3", dtparty.Rows[0]["PARTY3"].ToString());
                objDoc.SetParameterValue("@party4", dtparty.Rows[0]["PARTY4"].ToString());
                objDoc.SetParameterValue("@party5", dtparty.Rows[0]["PARTY5"].ToString());


                DataView dvTotal = dsItemDetail.Tables[0].DefaultView;
                dvTotal.RowFilter = "item='TOTAL AMOUNT'";
                DataTable dttotal = dvTotal.ToTable();
                objDoc.SetParameterValue("@total1", dttotal.Rows[0]["PARTY1"].ToString());
                objDoc.SetParameterValue("@total2", dttotal.Rows[0]["PARTY2"].ToString());
                objDoc.SetParameterValue("@total3", dttotal.Rows[0]["PARTY3"].ToString());
                objDoc.SetParameterValue("@total4", dttotal.Rows[0]["PARTY4"].ToString());
                objDoc.SetParameterValue("@total5", dttotal.Rows[0]["PARTY5"].ToString());

                DataView dvEC = dsItemDetail.Tables[0].DefaultView;
                dvEC.RowFilter = "item='TOTTAX AMOUNT'";
                DataTable dtEC = dvEC.ToTable();
                objDoc.SetParameterValue("@EC1", dtEC.Rows[0]["PARTY1"].ToString());
                objDoc.SetParameterValue("@EG2", dtEC.Rows[0]["PARTY2"].ToString());
                objDoc.SetParameterValue("@EG3", dtEC.Rows[0]["PARTY3"].ToString());
                objDoc.SetParameterValue("@EG4", dtEC.Rows[0]["PARTY4"].ToString());
                objDoc.SetParameterValue("@EG5", dtEC.Rows[0]["PARTY5"].ToString());

                DataView dvDC = dsItemDetail.Tables[0].DefaultView;
                dvDC.RowFilter = "item='DISCOUNT AMOUNT'";
                DataTable dtDC = dvDC.ToTable();
                objDoc.SetParameterValue("@DC1", dtDC.Rows[0]["PARTY1"].ToString());
                objDoc.SetParameterValue("@DC2", dtDC.Rows[0]["PARTY2"].ToString());
                objDoc.SetParameterValue("@DC3", dtDC.Rows[0]["PARTY3"].ToString());
                objDoc.SetParameterValue("@DC4", dtDC.Rows[0]["PARTY4"].ToString());
                objDoc.SetParameterValue("@DC5", dtDC.Rows[0]["PARTY5"].ToString());


                //Added by vijay andoju for Price&Value

                objDoc.SetParameterValue("@Value1", dtDC.Rows[0]["PARTY1"].ToString() == "" ? "" : "Value");
                objDoc.SetParameterValue("@Value2", dtDC.Rows[0]["PARTY2"].ToString() == "" ? "" : "Value");
                objDoc.SetParameterValue("@Value3", dtDC.Rows[0]["PARTY3"].ToString() == "" ? "" : "Value");
                objDoc.SetParameterValue("@Value4", dtDC.Rows[0]["PARTY4"].ToString() == "" ? "" : "Value");
                objDoc.SetParameterValue("@Value5", dtDC.Rows[0]["PARTY5"].ToString() == "" ? "" : "Value");

                objDoc.SetParameterValue("@Price1", dtDC.Rows[0]["PARTY1"].ToString() == "" ? "" : "Price");
                objDoc.SetParameterValue("@Price2", dtDC.Rows[0]["PARTY2"].ToString() == "" ? "" : "Price");
                objDoc.SetParameterValue("@Price3", dtDC.Rows[0]["PARTY3"].ToString() == "" ? "" : "Price");
                objDoc.SetParameterValue("@Price4", dtDC.Rows[0]["PARTY4"].ToString() == "" ? "" : "Price");
                objDoc.SetParameterValue("@Price5", dtDC.Rows[0]["PARTY5"].ToString() == "" ? "" : "Price");

                objDoc.SetParameterValue("@P_QUOTNO", quotno);
                objDoc.SetParameterValue("@P_COLLEGE_CODE", Session["colcode"].ToString());

                ConfigureCrystalReports(objDoc);

                CrystalReportViewer1.ReportSource = objDoc;

                //CrystalReportViewer1.DataBind();

                MemoryStream oStream;
                oStream = (MemoryStream)objDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }
        }
    }


    private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
}
