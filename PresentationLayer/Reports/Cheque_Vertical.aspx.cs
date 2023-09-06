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

public partial class Reports_Cheque_Vertical : System.Web.UI.Page
{
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
            //DataSet dsChq = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "*", "", "bankno=" + ReportParam[0].ToString().Trim() + " and accno='" + ReportParam[5].ToString().Trim() + "' and CHECKNO=" + ReportParam[1].ToString().Trim(), "");
            //DataSet dsChq = objCommon.FillDropDown("ACC_tmt_CHECK_PRINT", "*", "", "bankno=" + ReportParam[0].ToString().Trim() + " and accno=" + ReportParam[5].ToString().Trim() + " and CHECKNO=" + ReportParam[1].ToString().Trim(), "");
            DataSet dsChq = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "*", "", "bankno=" + ReportParam[0].ToString().Trim() + " and CHECKNO='" + ReportParam[1].ToString().Trim() + "'", "");
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
                            objDoc.Load(this.Server.MapPath("Account\\ChequePrintReport_Vertical.rpt"));

                            objDoc.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(0 * 1440, 0 * 1440, 0 * 1440, 0 * 1440));
                            //objDoc.PrintOptions.PageContentHeight =  Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1440;
                            //objDoc.PrintOptions.PageContentWidth =  Convert.ToInt32(Convert.ToDouble(dsChq.Tables[0].Rows[0]["CHECKWIDTH"].ToString().Trim()) * 1440;

                            CrystalDecisions.CrystalReports.Engine.Section cr = objDoc.ReportDefinition.Sections["Section2"];
                            CrystalDecisions.CrystalReports.Engine.ReportObject party = cr.ReportObjects["Party1"];
                            //party.Left = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1440);
                            //party.Height = Convert.ToInt32(Convert.ToDouble(0.5 * 1440));
                            //party.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYWIDTH"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1440);
                            //party.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYTOP"].ToString().Trim()) * 1440);
                            party.Left = ((Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1440) + 100) - ((Convert.ToInt32((Convert.ToDouble(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYTOP"].ToString().Trim())))) * 1440) + Convert.ToInt32(Convert.ToDouble(0.2 * 1440))));
                            party.Height = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYWIDTH"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1440);
                            party.Width = Convert.ToInt32(Convert.ToDouble(0.2 * 1440));
                            party.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim()) * 1440);
                            Font foparty = new Font("Arial", 12F, FontStyle.Bold);
                            FieldObject fieldparty;
                            fieldparty = party as FieldObject;
                            fieldparty.ApplyFont(foparty);

                            CrystalDecisions.CrystalReports.Engine.ReportObject chqDt = cr.ReportObjects["ChequeDate1"];
                            //chqDt.Left = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTLEFT"].ToString().Trim()) * 1440);
                            //chqDt.Height = Convert.ToInt32(Convert.ToDouble(0.50 * 1440));
                            //chqDt.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTWIDTH"].ToString().Trim()) * 1440);
                            //chqDt.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTTOP"].ToString().Trim()) * 1440);
                            chqDt.Left = (Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1440) - (Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTTOP"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(0.2 * 1440))));
                            chqDt.Height = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTWIDTH"].ToString().Trim()) * 1440);
                            chqDt.Width = Convert.ToInt32(Convert.ToDouble(0.2 * 1440));
                            chqDt.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CKDTLEFT"].ToString().Trim()) * 1440);
                            Font fochqDt = new Font("Arial", 12F, FontStyle.Bold);
                            FieldObject fieldchqDt;
                            fieldchqDt = chqDt as FieldObject;
                            fieldchqDt.ApplyFont(fochqDt);


                            CrystalDecisions.CrystalReports.Engine.ReportObject inWords = cr.ReportObjects["InWords1"];
                            //inWords.Left = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1440);
                            //inWords.Height = Convert.ToInt32(Convert.ToDouble(0.5 * 1440));
                            //inWords.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTWIDTH"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1440);
                            //inWords.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTTOP"].ToString().Trim()) * 1440);
                            inWords.Left = (Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1440) - (Convert.ToInt32((Convert.ToDouble(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTTOP"].ToString().Trim()))) * 1440) + Convert.ToInt32(Convert.ToDouble(0.2 * 1440))));
                            inWords.Height = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTWIDTH"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1440);
                            inWords.Width = Convert.ToInt32(Convert.ToDouble(0.2 * 1440));
                            inWords.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim()) * 1440);
                            Font foinWords = new Font("Arial", 12F, FontStyle.Bold);
                            FieldObject fieldinWords;
                            fieldinWords = inWords as FieldObject;
                            fieldinWords.ApplyFont(foinWords);

                            CrystalDecisions.CrystalReports.Engine.ReportObject Amount = cr.ReportObjects["Amount1"];
                            //Amount.Left = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTLEFT"].ToString().Trim()) * 1440);
                            //Amount.Height = Convert.ToInt32(Convert.ToDouble(0.5 * 1440));
                            //Amount.Width = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTWIDTH"].ToString().Trim()) * 1440);
                            //Amount.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTTOP"].ToString().Trim()) * 1440);
                            Amount.Left = (Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim()) * 1440) - (Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTTOP"].ToString().Trim()) * 1440) + Convert.ToInt32(Convert.ToDouble(0.2 * 1440))));
                            Amount.Height = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTWIDTH"].ToString().Trim()) * 1440);
                            Amount.Width = Convert.ToInt32(Convert.ToDouble(0.2 * 1440));
                            Amount.Top = Convert.ToInt32(Convert.ToDouble(dsChq1.Tables[0].Rows[0]["AMTLEFT"].ToString().Trim()) * 1440);
                            Font foAmount = new Font("Arial", 12F, FontStyle.Bold);
                            FieldObject fieldAmount;
                            fieldAmount = Amount as FieldObject;
                            fieldAmount.ApplyFont(foAmount);

                            DataSet ds45 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "*", "", "BANKNO='" + ReportParam[0].ToString().Trim() + "' AND CHECKNO='" + ReportParam[1].ToString().Trim() + "' AND CHECKDT ='" + Convert.ToDateTime(ReportParam[2].ToString()).ToString("dd-MMM-yyyy") + "'", "");
                            //DataSet ds45 = objCommon.FillDropDown("ACC_tmt_CHECK_PRINT", "*", "", "BANKNO=" + ReportParam[0].ToString().Trim() + " AND ACCNO='" + ReportParam[5].ToString().Trim() + "' AND CHECKNO=" + ReportParam[1].ToString().Trim() + " AND CHECKDT ='" + Convert.ToDateTime(ReportParam[2].ToString()).ToString("dd-MMM-yyyy") + "'", "");
                            if (ds45 != null)
                            {
                                if (ds45.Tables[0].Rows.Count > 0)
                                {

                                    if (ds45.Tables[0].Rows[0]["STAMP"].ToString().Trim() == "AcPayee")
                                    {
                                        objDoc.SetParameterValue("@@Type", "A/C Payee");
                                    }
                                    else
                                    {
                                        objDoc.SetParameterValue("@@Type", "");
                                    }
                                    if (ds45.Tables[0].Rows[0]["CANCEL"].ToString().Trim() == "1")
                                    {
                                        objDoc.SetParameterValue("@@Type", "Cancel");
                                    }

                                    string ChkDate = Convert.ToDateTime(ds45.Tables[0].Rows[0]["VDT"]).ToString("ddMMyyyy");
                                    string chqdate = string.Empty;
                                    for (int i = 0; i < 8; i++)
                                    {
                                        if (chqdate.Length > 0)
                                        {
                                            chqdate = chqdate + "   " + ChkDate[i].ToString();
                                        }
                                        else
                                        {
                                            chqdate = ChkDate[i].ToString();
                                        }
                                    }

                                    objDoc.SetParameterValue("@@Party", "**" + ReportParam[3].ToString().Trim().Split('*')[1].ToString().Trim() + "**");
                                    objDoc.SetParameterValue("@@InWords", ds45.Tables[0].Rows[0]["Reason1"].ToString().Trim());
                                    objDoc.SetParameterValue("@@Amount", Convert.ToDecimal(ds45.Tables[0].Rows[0]["Amount"].ToString().Trim()));
                                    //objDoc.SetParameterValue("@@ChequeDate", Convert.ToDateTime(ds45.Tables[0].Rows[0]["VDT"]).ToString("ddMMyyyy"));
                                    objDoc.SetParameterValue("@@ChequeDate", chqdate);

                                    objDoc.SetDatabaseLogon("SANGLI_UAIMSCLIENT", "SANGLI_UAIMSCLIENT", "192.168.0.31", "SANGLI_UAIMSCLIENT");
                                    //objDoc.SetDataSource(dsChq1);
                                    CrystalReportViewer1.ReportSource = objDoc;

                                    //CrystalReportViewer1.DataBind();

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


                                    //CrystalReportViewer1.RefreshReport();


                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
