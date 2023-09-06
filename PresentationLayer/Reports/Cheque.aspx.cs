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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using System.Data.SqlClient;
using IITMS.UAIMS;
using System.IO;

public partial class Reports_Cheque : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    string[] ReportParam;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        ReportParam = Request.QueryString["obj"].Split(',');

        if (ReportParam.Length > 0)
        {
            DataSet dsChq = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "*", "", "bankno=" + ReportParam[0].ToString().Trim() + " and accno=" + ReportParam[5].ToString().Trim() + " and CHECKNO=" + ReportParam[1].ToString().Trim(), "");
           if (dsChq != null)
           {
               if (dsChq.Tables[0].Rows.Count > 0)
               {

                   DataSet dsChq1 = objCommon.FillDropDown("ACC_BANK_DETAIL", "*", "", "bankno=" + ReportParam[0].ToString().Trim(), "");

                       if (dsChq1 != null)
                       {
                           if (dsChq1.Tables[0].Rows.Count > 0)
                           {

                               CrystalDecisions.CrystalReports.Engine.ReportDocument objDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                               objDoc.Load(this.Server.MapPath("Account\\ChequePrintReport.rpt"));
                               
                              

                               objDoc.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(0 * 1440, 0 * 1440, 0 * 1440, 0 * 1440));
                               //objDoc.PrintOptions.PageContentHeight =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1440;
                               //objDoc.PrintOptions.PageContentWidth =  Convert.ToInt32(Convert.ToDouble(dsChq.Tables[0].Rows[0]["CHECKWIDTH"].ToString().Trim()) * 1440;

                               CrystalDecisions.CrystalReports.Engine.Section cr = objDoc.ReportDefinition.Sections["Section2"];
                               CrystalDecisions.CrystalReports.Engine.ReportObject party = cr.ReportObjects["Party1"];
                               party.Left =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim())* 1440);
                               party.Height =  Convert.ToInt32(Convert.ToDouble(0.5 * 1440));
                               party.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYWIDTH"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1440);
                               party.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYTOP"].ToString().Trim()) * 1440);


                               CrystalDecisions.CrystalReports.Engine.ReportObject chqDt = cr.ReportObjects["ChequeDate1"];
                               chqDt.Left =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTLEFT"].ToString().Trim()) * 1440);
                               chqDt.Height =  Convert.ToInt32(Convert.ToDouble(0.50 * 1440));
                               chqDt.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTWIDTH"].ToString().Trim()) * 1440);
                               chqDt.Top =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTTOP"].ToString().Trim())*1440);



                               CrystalDecisions.CrystalReports.Engine.ReportObject inWords = cr.ReportObjects["InWords1"];
                               inWords.Left = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1440);
                               inWords.Height =  Convert.ToInt32(Convert.ToDouble(0.5 * 1440));
                               inWords.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTWIDTH"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1440);
                               inWords.Top =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTTOP"].ToString().Trim()) * 1440);

                               CrystalDecisions.CrystalReports.Engine.ReportObject Amount = cr.ReportObjects["Amount1"];
                               Amount.Left = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTLEFT"].ToString().Trim()) * 1440);
                               Amount.Height =  Convert.ToInt32(Convert.ToDouble(0.5 * 1440));
                               Amount.Width =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTWIDTH"].ToString().Trim()) * 1440);
                               Amount.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTTOP"].ToString().Trim()) * 1440);

                               DataSet ds45 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "*", "", "BANKNO=" + ReportParam[0].ToString().Trim() + " AND ACCNO='" + ReportParam[5].ToString().Trim() + "' AND CHECKNO=" + ReportParam[1].ToString().Trim() + " AND CHECKDT ='" + Convert.ToDateTime(ReportParam[2].ToString()).ToString("dd-MMM-yyyy") + "'", "");
                               if (ds45 != null)
                               {
                                   if (ds45.Tables[0].Rows.Count > 0)
                                   {

                                       if (ds45.Tables[0].Rows[0]["STAMP"].ToString().Trim() == "AcPayee")
                                       {
                                           objDoc.SetParameterValue("@@Type", "A/C Payee");

                                       }
                                       if (ds45.Tables[0].Rows[0]["CANCEL"].ToString().Trim() == "1")
                                       {
                                           objDoc.SetParameterValue("@@Type", "Cancel");

                                       }
                                       objDoc.SetParameterValue("@@Party", "**" + ReportParam[3].ToString().Trim().Split('*')[1].ToString().Trim() + "**");
                                       objDoc.SetParameterValue("@@InWords", ds45.Tables[0].Rows[0]["Reason1"].ToString().Trim());
                                       objDoc.SetParameterValue("@@Amount", "**" + ds45.Tables[0].Rows[0]["Amount"].ToString().Trim());
                                       objDoc.SetParameterValue("@@ChequeDate", Convert.ToDateTime(ds45.Tables[0].Rows[0]["VDT"]).ToString("dd/MM/yyyy"));

                                       //ConnectionInfo connectionInfo = Common.GetCrystalConnection();

                                       //string DatabaseName=connectionInfo.DatabaseName;
                                       //string ServerName = connectionInfo.ServerName;
                                       //string UserId = connectionInfo.UserID;
                                       //string Password = connectionInfo.Password;

                                      // objDoc.SetDatabaseLogon(UserId, Password, ServerName, DatabaseName);
                                       objDoc.SetDatabaseLogon("SANGLI_UAIMSCLIENT", "SANGLI_UAIMSCLIENT", "192.168.0.31", "SANGLI_UAIMSCLIENT");
                                       //objDoc.SetDataSource(dsChq1);
                                       CrystalReportViewer1.ReportSource = objDoc;
                                      // CrystalReportViewer1.DataBind();

                                       MemoryStream oStream;
                                       oStream = (MemoryStream)objDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                                       Response.Clear();
                                       Response.Buffer = true;
                                       Response.ContentType = "application/pdf";
                                       Response.BinaryWrite(oStream.ToArray());
                                       Response.End();

                                       //ParameterField FParty = CrystalReportViewer1.ParameterFieldInfo["@@Party"];
                                       //ParameterDiscreteValue VParty = new ParameterDiscreteValue();
                                       //VParty.Value = "**" + ReportParam[3].ToString().Trim().Split('*')[1].ToString().Trim() + "**";
                                       //FParty.CurrentValues.Add(VParty);



                                       //ParameterField FInWords = CrystalReportViewer1.ParameterFieldInfo["@@InWords"];
                                       //ParameterDiscreteValue VInWords = new ParameterDiscreteValue();
                                       //VInWords.Value = ds45.Tables[0].Rows[0]["Reason1"].ToString().Trim();
                                       //FInWords.CurrentValues.Add(VInWords);



                                       //ParameterField FAmount = CrystalReportViewer1.ParameterFieldInfo["@@Amount"];
                                       //ParameterDiscreteValue VAmount = new ParameterDiscreteValue();
                                       //VAmount.Value = ds45.Tables[0].Rows[0]["Amount"].ToString().Trim();
                                       //FAmount.CurrentValues.Add(VAmount);


                                       //ParameterField FChequeDate = CrystalReportViewer1.ParameterFieldInfo["@@ChequeDate"];
                                       //ParameterDiscreteValue VChequeDate = new ParameterDiscreteValue();
                                       //VChequeDate.Value = Convert.ToDateTime(ds45.Tables[0].Rows[0]["VDT"]).ToString("dd/MM/yyyy");
                                       //FChequeDate.CurrentValues.Add(VChequeDate);


                                       CrystalReportViewer1.RefreshReport();


        
                                   }


                               }
                               

                               



                           }
                       }



               }       
           }                 
        }
    }

    //private void SetChequeLayout(RptCheque RPC,int bankno)
    //{

    //    DataSet dsChq = objCommon.FillDropDown("ACC_BANK_DETAIL", "*", "", "bankno=" + bankno.ToString().Trim() , "");

    //    if (dsChq != null)
    //    {
    //        if (dsChq.Tables[0].Rows.Count > 0)
    //        {
    //            //RPC.PageSettings.Margins.Top = 0 ;
    //            //RPC.PageSettings.Margins.Left = 0;
    //            //RPC.PageSettings.Margins.Right = 0 ;
    //            //RPC.PageSettings.Margins.Bottom = 0;
    //            //RPC.PageSettings.PaperHeight  =  float.Parse(dsChq.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1;
    //            //RPC.PageSettings.PaperWidth = float.Parse(dsChq.Tables[0].Rows[0]["CHECKWIDTH"].ToString().Trim()) * 1;
    //            //RPC.PrintWidth = float.Parse(dsChq.Tables[0].Rows[0]["CHECKWIDTH"].ToString().Trim()) * 1;

    //            //RPC.txtParty.Top = float.Parse(dsChq.Tables[0].Rows[0]["PARTYTOP"].ToString().Trim()) * 1;
    //            //RPC.txtParty.Width = float.Parse(dsChq.Tables[0].Rows[0]["PARTYWIDTH"].ToString().Trim()) * 1 + float.Parse(dsChq.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1;
    //            //RPC.txtParty.Left = float.Parse(dsChq.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1;
    //            //RPC.txtWAmt.Top = float.Parse(dsChq.Tables[0].Rows[0]["WAMTTOP"].ToString().Trim()) * 1;
    //            //RPC.txtWAmt.Width = float.Parse(dsChq.Tables[0].Rows[0]["WAMTWIDTH"].ToString().Trim()) * 1 + float.Parse(dsChq.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1;
    //            //RPC.txtWAmt.Left = float.Parse(dsChq.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1;
    //            //RPC.txtNAmount.Top = float.Parse(dsChq.Tables[0].Rows[0]["AMTTOP"].ToString().Trim()) * 1;
    //            //RPC.txtNAmount.Width = float.Parse(dsChq.Tables[0].Rows[0]["AMTWIDTH"].ToString().Trim()) * 1;
    //            //RPC.txtNAmount.Left = float.Parse(dsChq.Tables[0].Rows[0]["AMTLEFT"].ToString().Trim()) * 1;
    //            //RPC.lblDate.Top = float.Parse(dsChq.Tables[0].Rows[0]["CKDTTOP"].ToString().Trim()) * 1;
    //            //RPC.lblDate.Width = float.Parse(dsChq.Tables[0].Rows[0]["CKDTWIDTH"].ToString().Trim()) * 1;
    //            //RPC.lblDate.Left = float.Parse(dsChq.Tables[0].Rows[0]["CKDTLEFT"].ToString().Trim()) * 1;

    //        }
        
    //    }




        

    //}
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session["comp_code"] == null)
        {
            Session["comp_set"] = "NotSelected";

            objCommon.DisplayMessage("Select company/cash book.", this);

            Response.Redirect("~/ACCOUNT/selectCompany.aspx");
        }


        if (RadioButtonList1.SelectedValue.ToString().Trim() == "P")
        {
            CrystalReportViewer1.HasPrintButton = true;
            AccountingVouchersController avc = new AccountingVouchersController();
            avc.UpdateChequePrintStatusToTransaction(Convert.ToInt32(ReportParam[7]), ReportParam[1].ToString().Trim(), Convert.ToDateTime(ReportParam[2]).ToString("dd-MMM-yyyy"), ReportParam[8].ToString().Trim(), Session["comp_code"].ToString().Trim(), "N", ReportParam[9].ToString().Trim());

        }
        else
        {
            CrystalReportViewer1.HasPrintButton = false;
            AccountingVouchersController avc = new AccountingVouchersController();
            avc.UpdateChequePrintStatusToTransaction(Convert.ToInt32(ReportParam[7]), ReportParam[1].ToString().Trim(), Convert.ToDateTime(ReportParam[2]).ToString("dd-MMM-yyyy"), ReportParam[8].ToString().Trim(), Session["comp_code"].ToString().Trim(), "Y", ReportParam[9].ToString().Trim());
        
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CrystalReportViewer1.HasPrintButton = true;
        AccountingVouchersController avc = new AccountingVouchersController();
        avc.UpdateChequePrintStatusToTransaction(Convert.ToInt32(ReportParam[7]), ReportParam[1].ToString().Trim(), Convert.ToDateTime(ReportParam[2]).ToString("dd-MMM-yyyy"), ReportParam[8].ToString().Trim(), Session["comp_code"].ToString().Trim(), "N", ReportParam[9].ToString().Trim());

    }
}
