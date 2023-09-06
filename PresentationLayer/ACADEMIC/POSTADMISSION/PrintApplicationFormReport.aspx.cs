using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using BusinessLogicLayer.BusinessLogic;
using System.Text;  

public partial class Reports_PrintApplicationFormReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    ReportController objreport = new ReportController();
    protected void Page_Load(object sender, EventArgs e)
    {
        string directoryPath = string.Empty;
        string directoryName = string.Empty;
        int userno = 0;
        if (Request.QueryString["userno"].ToString() == "0")
        {
           
            userno = Convert.ToInt32(Session["USERNO"].ToString());
             directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);
            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }
        }
        else
        {
           userno = Convert.ToInt32(Request.QueryString["userno"]);
           directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }
        }
        ShowReport(userno, directoryName);
    }

    #region PrintApplicationFormReport

    // Added by Yograj on 21-03-2023
    private void ShowReport(int userno, string directoryName)
    {
        try
        {
            //Create document  
            Document doc = new Document();
            //Create PDF Table  
            PdfPTable tableLayout = new PdfPTable(4);
            PdfPTable tableLayout1 = new PdfPTable(6);
            PdfPTable tableLayout2 = new PdfPTable(2);
            PdfPTable tableLayout3 = new PdfPTable(12);
            PdfPTable tableLayout4 = new PdfPTable(8);
            PdfPTable tableLayout5 = new PdfPTable(8);
            PdfPTable tableLayout6 = new PdfPTable(5);
            PdfPTable tableLayout7 = new PdfPTable(4);
            PdfPTable tableLayout8 = new PdfPTable(5);
            //Create a PDF file in specific path  

            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath(directoryName + "\\PrintApplicationFormReport.pdf"), FileMode.Create));

            //Open the PDF document  
            doc.Open();
            //Add Content to PDF  
            doc.Add(PDPCertificateWorkshop7(tableLayout7, userno));
            doc.Add(PDPCertificateWorkshop8(tableLayout8, userno));
            doc.Add(PDPCertificateWorkshop(tableLayout, userno));
            doc.Add(PDPCertificateWorkshop3(tableLayout3, userno));
            doc.Add(PDPCertificateWorkshop1(tableLayout1, userno));
            doc.Add(PDPCertificateWorkshop2(tableLayout2, userno));
            doc.Add(PDPCertificateWorkshop4(tableLayout4, userno));
            doc.Add(PDPCertificateWorkshop5(tableLayout5, userno));
            doc.Add(PDPCertificateWorkshop6(tableLayout6, userno));
            // Closing the document  
            doc.Close();

            string filepath = Server.MapPath(directoryName + "\\PrintApplicationFormReport.pdf");
            string filename = "PrintApplicationFormReport" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "filename=" + filename + " ");
            Response.TransmitFile(Server.MapPath(directoryName + "\\PrintApplicationFormReport.pdf"));
            Page.ClientScript.RegisterStartupScript(
            this.GetType(), "OpenWindow", "window.open('" + filepath + "','_blank');", true);
            Response.End();

            ////FileStream filestream = new FileStream(filepath, FileMode.Open);
            
            //byte[] _byte = System.IO.File.ReadAllBytes(filepath);
            //MemoryStream memoryStream = new MemoryStream(_byte);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AppendHeader("Content-Disposition", "filename=" + filename + " ");
            //Response.BinaryWrite(memoryStream.ToArray());   
            //Response.End();
            

            //Open the PDF file  
            //System.Diagnostics.Process.Start(Server.MapPath("\\PDPReportDownload\\PDPCertificateWorkshop.pdf"));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PDPReports.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private PdfPTable PDPCertificateWorkshop7(PdfPTable tableLayout7, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
        
        DataSet banner = objCommon.FillDropDown("REFF", "College_Banner", "", "", "");
        Byte[] logo = (Byte[])banner.Tables[0].Rows[0]["College_Banner"];
        iTextSharp.text.Image headerlogo = iTextSharp.text.Image.GetInstance(logo);
        headerlogo.ScaleToFit(350f, 150f);
        headerlogo.Alignment = Element.ALIGN_LEFT;

        PdfPCell _pdfpcell = new PdfPCell(headerlogo);
        _pdfpcell.HorizontalAlignment = Element.ALIGN_LEFT;
        _pdfpcell.Border = 0;

        float[] headers = { 20, 30, 20, 30 }; //Header Widths  
        tableLayout7.SetWidths(headers); //Set the pdf headers  
        tableLayout7.WidthPercentage = 100; //Set the PDF File witdh percentage  
        //Add Title to the PDF file at the top  

        tableLayout7.AddCell(new PdfPCell(headerlogo)
        {
            Colspan = 3,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        #region photosign

        DataSet ds = new DataSet();
        ds = objreport.GetPersonalDetails(userno);

        //tableLayout.AddCell(_pdfpcell);


        Byte[] bytes = (Byte[])ds.Tables[0].Rows[0]["PHOTO"];
        iTextSharp.text.Image photo = iTextSharp.text.Image.GetInstance(bytes);
        photo.ScaleToFit(80f, 120f);
        Chunk imageChunk = new Chunk(photo, 0, 0);
        tableLayout7.AddCell(new PdfPCell(new Phrase(imageChunk))
        {
            Colspan = 1,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_CENTER,
            PaddingLeft = 60
        });

        tableLayout7.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 1,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_CENTER
        });
        tableLayout7.AddCell(new PdfPCell(new Phrase("Application for Admission (DAIICT 2023)", verdana13))// new Font(Font.TIMES_ROMAN, 13, 1, iTextSharp.text.Color.BLACK)))
        {
            Colspan = 2,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_CENTER
        });

        Byte[] bytesign = (Byte[])ds.Tables[0].Rows[0]["USER_SIGN"];
        iTextSharp.text.Image sign = iTextSharp.text.Image.GetInstance(bytesign);
        sign.ScaleToFit(100f, 40f);
        Chunk imageChunksign = new Chunk(sign, 0, 0);
        tableLayout7.AddCell(new PdfPCell(new Phrase(imageChunksign))// new Font(Font.TIMES_ROMAN, 13, 1, iTextSharp.text.Color.BLACK)))
        {
            Colspan = 2,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_CENTER,
            PaddingLeft = 60
        });
        tableLayout7.AddCell(new PdfPCell(new Phrase("_____________________________________________________________________________", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_CENTER
        });
        tableLayout7.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 0,
            HorizontalAlignment = Element.ALIGN_CENTER
        });
        tableLayout7.AddCell(new PdfPCell(new Phrase("Date of Registration", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout7.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["REGDATE"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });


        tableLayout7.AddCell(new PdfPCell(new Phrase("Application Category", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout7.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["APPLICATION_CATEGORY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        #endregion photosign
        


        return tableLayout7;
    }

    private PdfPTable PDPCertificateWorkshop8(PdfPTable tableLayout8, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
        //verdana13.SetStyle(Font.UNDERLINE);
        float[] headers = { 20, 28, 12, 20,20 }; //Header Widths  
        tableLayout8.SetWidths(headers); //Set the pdf headers  
        tableLayout8.WidthPercentage = 100;

        DataSet ds = new DataSet();
        ds = objreport.GetPersonalDetails(userno);

        #region Apply Program
        tableLayout8.AddCell(new PdfPCell(new Phrase("", verdana14))
        {
            BorderWidthBottom = 1,
            Colspan = 5,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

       
        tableLayout8.AddCell(new PdfPCell(new Phrase("", verdana14))
        {
            BorderWidthBottom = 1,

            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout8.AddCell(new PdfPCell(new Phrase("", verdana14))
        {
            BorderWidthBottom = 1,
            Colspan = 5,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });


        DataSet dsapplyprogram = new DataSet();
        dsapplyprogram = objreport.GetApplyProgramDetails(userno,1);
        if (dsapplyprogram != null && dsapplyprogram.Tables.Count > 0)
        {

            tableLayout8.AddCell(new PdfPCell(new Phrase("Applied Program(s) ", verdana14))
            {
                BorderWidthBottom = 1,
                Colspan = 5,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout8.AddCell(new PdfPCell(new Phrase("Degree", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout8.AddCell(new PdfPCell(new Phrase("Branch / Program", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout8.AddCell(new PdfPCell(new Phrase("Preference", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout8.AddCell(new PdfPCell(new Phrase("ApplicationID", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout8.AddCell(new PdfPCell(new Phrase("Payment Status", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            foreach (DataRow table in dsapplyprogram.Tables[0].Rows)
            {
                tableLayout8.AddCell(new PdfPCell(new Phrase(table["CODE"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout8.AddCell(new PdfPCell(new Phrase(table["LONGNAME"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                int groupno = Convert.ToInt32( table["GROUPNO"].ToString());
                if (groupno != 0)
                {
                    tableLayout8.AddCell(new PdfPCell(new Phrase(table["PRORAMME"].ToString(), verdana11))
                    {
                        Border = Rectangle.BOX,
                        PaddingBottom = 5,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });
                }
                else
                {
                    tableLayout8.AddCell(new PdfPCell(new Phrase("", verdana11))
                    {
                        Border = Rectangle.BOX,
                        PaddingBottom = 5,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });
                }
                

                tableLayout8.AddCell(new PdfPCell(new Phrase(table["APPLICATION_ID"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout8.AddCell(new PdfPCell(new Phrase(table["PAYMENT_STATUS"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

            }
            tableLayout8.AddCell(new PdfPCell(new Phrase("", verdana11))
            {
                Colspan = 5,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }

        #endregion Apply Program



        return tableLayout8;
    }


    // Added by Yograj on 21-03-2023
    private PdfPTable PDPCertificateWorkshop(PdfPTable tableLayout, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
        //verdana13.SetStyle(Font.UNDERLINE);
       
        float[] headers = { 15, 35, 15, 35 }; //Header Widths  
        tableLayout.SetWidths(headers); //Set the pdf headers  
        tableLayout.WidthPercentage = 100; 
        DataSet ds = new DataSet();
        ds = objreport.GetPersonalDetails(userno);

        

        #region Candidate Details

        tableLayout.AddCell(new PdfPCell(new Phrase("Personal Details", verdana14))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("Candidate Details", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        //tableLayout.AddCell(new PdfPCell(new Phrase("Father's Mobile No.     : ", verdana11))
        // <Candidates Row-1>
        tableLayout.AddCell(new PdfPCell(new Phrase("Full Name", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : "+ds.Tables[0].Rows[0]["FullName"].ToString(), verdana11))
        {
            Colspan = 3,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        
        // </Candidates Row-1>
        // <Candidates Row-2>
        tableLayout.AddCell(new PdfPCell(new Phrase("Email", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["EMAILID"].ToString(), verdana11))
        {
            Colspan = 3,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase("Alternate Email", verdana11))
        {
           
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString(), verdana11))
        {
            Colspan = 3,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Candidates Row-2>
        // <Candidates Row-3>
        tableLayout.AddCell(new PdfPCell(new Phrase("First Name", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["FIRSTNAME"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        

        
        // </Candidates Row-3>
        // <Candidates Row-4>
     
        
        tableLayout.AddCell(new PdfPCell(new Phrase("Last Name", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["LASTNAME"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Candidate Row-4>
        
        // <Candidates Row-5>
        tableLayout.AddCell(new PdfPCell(new Phrase("Date of Birth", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["DOB"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
        tableLayout.AddCell(new PdfPCell(new Phrase("Gender", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["GENDER"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Candidates Row-5>
        // <Candidates Row-6>
        tableLayout.AddCell(new PdfPCell(new Phrase("Mobile No.", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["MOBILENO"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["UGPGOT"].ToString()) == 1 && ds.Tables[0].Rows[0]["APPLICATION_CATEGORY"].ToString() == "INDIAN")
        {
            tableLayout.AddCell(new PdfPCell(new Phrase("Category", verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["CATEGORY"].ToString(), verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }
        // <Candidates Row-6>
        // <Candidates Row-7>
        tableLayout.AddCell(new PdfPCell(new Phrase("Nationality", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["NATIONALITY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
        tableLayout.AddCell(new PdfPCell(new Phrase("State of Domicile", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["STATENAME"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Candidates Row-7>
        // <Candidates Row-8>
        tableLayout.AddCell(new PdfPCell(new Phrase("Place of Residency", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["PLACE_OF_RESIDENCY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        if (ds.Tables[0].Rows[0]["APPLICATION_CATEGORY"].ToString() != "FOREIGN NATIONAL")
        {
            tableLayout.AddCell(new PdfPCell(new Phrase("Aadhar No.", verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["ADHAARNO"].ToString(), verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }
        // </Candidates Row-8>
        // <Candidates Row-9>
        tableLayout.AddCell(new PdfPCell(new Phrase("Height (in cms.)", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["HEIGHT"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("Identification Mark", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["IDENTITY_MARK"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });
       
        // </Candidates Row-9>
        // <Candidates Row-10>

        tableLayout.AddCell(new PdfPCell(new Phrase("Weight (in Kgs.)", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["WEIGHT"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Candidates Row-10>
        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        #endregion Candidate Details



        #region Passport Details
        tableLayout.AddCell(new PdfPCell(new Phrase("Passport Details", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        //tableLayout.AddCell(new PdfPCell(new Phrase("Father's Mobile No.     : ", verdana11))
        // <Passport Row-1>
        tableLayout.AddCell(new PdfPCell(new Phrase("Passport No.", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["PASSPORTNO"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        if (ds.Tables[0].Rows[0]["APPLICATION_CATEGORY"].ToString() != "INDIAN")
        {
            tableLayout.AddCell(new PdfPCell(new Phrase("Date of Issue", verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase(" : " + (ds.Tables[0].Rows[0]["DATE_OF_ISSUE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE_OF_ISSUE"].ToString()).ToString("dd/MM/yyyy")), verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            // </Passport Row-1>
            // <Passport Row-2>
            tableLayout.AddCell(new PdfPCell(new Phrase("Country of Issue", verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["COUNTRYNAME"].ToString(), verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("Date of Expiry", verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(" : " + (ds.Tables[0].Rows[0]["DATE_OF_EXPIRY"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE_OF_EXPIRY"].ToString()).ToString("dd/MM/yyyy")), verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            // </Passport Row-2>
            // <Passport Row-3>


            tableLayout.AddCell(new PdfPCell(new Phrase("Place of Issue", verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["PLACE_OF_ISSUE"].ToString(), verdana11))
            {
                Border = 0,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }
        // </Passport Row-3>
        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        #endregion Passport Details



        #region Father Details
        tableLayout.AddCell(new PdfPCell(new Phrase("Father's Details", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
       
        // <Father Row-1>
        tableLayout.AddCell(new PdfPCell(new Phrase("Name of Father", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["FATHERNAME"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("Occupation", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["FA_OCCUPATION"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });
        
        // </Father Row-1>

        // <Father Row-2>
        tableLayout.AddCell(new PdfPCell(new Phrase("Father's Mobile No.", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["F_MOBILENO"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        //tableLayout.AddCell(new PdfPCell(new Phrase("Occupation            :", verdana11))
        tableLayout.AddCell(new PdfPCell(new Phrase("Designation", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["F_DESIGNATION"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });
        // </Father Row-2>
        //tableLayout.AddCell(new PdfPCell(new Phrase("Designation                 :", verdana11))
        //// <Father Row-3>
        tableLayout.AddCell(new PdfPCell(new Phrase("Email", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["FEMAILADDRESS"].ToString(), verdana11))
        {
            Colspan = 3,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });

        tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Father Row-3>
       

        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
       

        #endregion Father Details


        #region Mother Details

        tableLayout.AddCell(new PdfPCell(new Phrase("Mother's Details", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
        // <Mother Row-1>
        tableLayout.AddCell(new PdfPCell(new Phrase("Name of Mother", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["MOTHERNAME"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("Occupation", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["MO_OCCUPATION"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });
        
        // </Mother Row-1>

       
        // <Mother Row-2>
        tableLayout.AddCell(new PdfPCell(new Phrase("Mother's Mobile No.", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["M_MOBILENO"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
        tableLayout.AddCell(new PdfPCell(new Phrase("Designation", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["M_DESIGNATION"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });
        // </Mother Row-2>
        
        // <Mother Row-3>
        tableLayout.AddCell(new PdfPCell(new Phrase("Email", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + ds.Tables[0].Rows[0]["M_EMAILADDRESS"].ToString(), verdana11))
        {
            Colspan = 3,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT

        });

        tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Mother Row-3>
      
        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
       
        #endregion Mother Details



        #region Address Details
        DataSet dsaddress = objreport.GetAddressDetails(userno);

        tableLayout.AddCell(new PdfPCell(new Phrase("Address Details", verdana14))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
        tableLayout.AddCell(new PdfPCell(new Phrase("Corresponding Address Details", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // <Corresponding Address Row-1>
        tableLayout.AddCell(new PdfPCell(new Phrase("Address", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LADDRESS"].ToString(), verdana11))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        
        tableLayout.AddCell(new PdfPCell(new Phrase("Country", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LCOUNTRY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Corresponding Address Row-1>
        // <Corresponding Address Row-2>
        tableLayout.AddCell(new PdfPCell(new Phrase("State", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LSTATE"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
       
        tableLayout.AddCell(new PdfPCell(new Phrase("District", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LDISTRICT"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Corresponding Address Row-2>
        // <Corresponding Address Row-3>
        tableLayout.AddCell(new PdfPCell(new Phrase("City", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LCITY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
       
        tableLayout.AddCell(new PdfPCell(new Phrase("Taluka", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LTALUKA"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Corresponding Address Row-3>
        // <Corresponding Address Row-4>
        tableLayout.AddCell(new PdfPCell(new Phrase("PinCode", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["LPINCODE"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
       
        // </Corresponding Address Row-4>
        tableLayout.AddCell(new PdfPCell(new Phrase("Permanent Address Details", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // <Permanent Address Row-1>
        tableLayout.AddCell(new PdfPCell(new Phrase("Address", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PADDRESS"].ToString(), verdana11))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

      
        
        tableLayout.AddCell(new PdfPCell(new Phrase("Country", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PCOUNTRY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Permanent Address Row-1>
        // <Permanent Address Row-2>
        tableLayout.AddCell(new PdfPCell(new Phrase("State", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PSTATE"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
      
        tableLayout.AddCell(new PdfPCell(new Phrase("District", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PDISTRICT"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Permanent Address Row-2>
        // <Permanent Address Row-3>
        tableLayout.AddCell(new PdfPCell(new Phrase("City", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PCITY"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        
        tableLayout.AddCell(new PdfPCell(new Phrase("Taluka", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PTALUKA"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Permanent Address Row-3>
        // <Permanent Address Row-4>
        tableLayout.AddCell(new PdfPCell(new Phrase("PinCode", verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + dsaddress.Tables[0].Rows[0]["PPINCODE"].ToString(), verdana11))
        {
            Border = 0,
            PaddingBottom = 2,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
         tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        // </Permanent Address Row-4>

        tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
        {
            Colspan = 4,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        #endregion Address Details



        #region Educational Details



        DataSet dsEdu = objreport.GetEducationalDetails(userno);
        if (dsEdu.Tables[0] != null && dsEdu.Tables[0].Rows.Count > 0)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase("Qualification Details", verdana14))
            {
                Colspan = 4,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            #region SSC Details
            for (int i = 0; i < dsEdu.Tables[0].Rows.Count; i++)
            {
                if (dsEdu.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("1"))
                {
                    DataTable tblSSC_Filtered = dsEdu.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(1))
                            .CopyToDataTable();

                    tableLayout.AddCell(new PdfPCell(new Phrase("X / SSC Details", verdana13))
                    {
                        Colspan = 4,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // <Educational Address Row-1>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Institution Name / School", verdana11))
                    {
                       
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["INSTITUTION_NAME"].ToString(), verdana11))
                    {
                        Colspan = 3,
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Enrollment No.", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["REGNO"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("Country", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["COUNTRYNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                   

                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("State", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["STATENAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Board of Examination", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["BOARDNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("Passing of Month-Year", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["MONTH_OF_PASSING_NAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Number of Attempts", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["NO_OF_ATTEMPTS"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-5>
                    // <Educational Address Row-6>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Total Maximum Marks", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MAX_MARK"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Total Marks Obtained", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MARKS_OBTAINED"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-6>
                    // <Educational Address Row-7>
                    tableLayout.AddCell(new PdfPCell(new Phrase("% of Marks", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["PERCENTAGE"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Certificate Upload", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblSSC_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-7>
                    tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    break;
                }
              
            }
            tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
            {
                Colspan = 4,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            #endregion SSC Details


            #region HSC Details
            for (int i = 0; i < dsEdu.Tables[0].Rows.Count; i++)
            {

                if (dsEdu.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("2"))
                {
                    DataTable tblHSC_Filtered = dsEdu.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(2))
                            .CopyToDataTable();
                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("XII / HSC Details", verdana13))
                    {
                        Colspan = 4,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // <Educational Address Row-1>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Institution Name / School", verdana11))
                    {
                       
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["INSTITUTION_NAME"].ToString(), verdana11))
                    {
                        Colspan = 3,
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Enrollment No.", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["REGNO"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("Country", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["COUNTRYNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                  
                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("State", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["STATENAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Board of Examination", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["BOARDNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Result Announced", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["RESULT"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    if (Convert.ToInt32(tblHSC_Filtered.Rows[0]["RESULT_NOT_ANNOUNCED"].ToString()) == 0)
                    {

                        tableLayout.AddCell(new PdfPCell(new Phrase("Passing of Month-Year", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["MONTH_OF_PASSING_NAME"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Number of Attempts", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["NO_OF_ATTEMPTS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        // </Educational Address Row-5>
                        // <Educational Address Row-6>
                        tableLayout.AddCell(new PdfPCell(new Phrase("Total Maximum Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MAX_MARK"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Total Marks Obtained", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MARKS_OBTAINED"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        // </Educational Address Row-6>
                        // <Educational Address Row-7>
                        tableLayout.AddCell(new PdfPCell(new Phrase("% of Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["PERCENTAGE"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Aggregate Max Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["MAX_MARKS_PHY_MATHS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Aggregate obtained Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["OBTAINED_MARKS_PHY_MATHS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout.AddCell(new PdfPCell(new Phrase("Aggregrate % of Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["AGG_PER_PHYSICS_MATHS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase("Certificate Upload", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                    }

                    if (Convert.ToInt32(tblHSC_Filtered.Rows[0]["RESULT_NOT_ANNOUNCED"].ToString()) == 1)
                    {
                        tableLayout.AddCell(new PdfPCell(new Phrase("XI Aggregate Max Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["XI_MAX_MARKS_PHY_MATHS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout.AddCell(new PdfPCell(new Phrase("XI Aggregate obtained Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["XI_OBTAINED_MARKS_PHY_MATHS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout.AddCell(new PdfPCell(new Phrase("XI Aggregate % Marks", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["XI_AGG_PER_PHYSICS_MATHS"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase("XI Overall % / CGPA out of 10", verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                        tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblHSC_Filtered.Rows[0]["XI_AGG_CGPA"].ToString(), verdana11))
                        {
                            Border = 0,
                            PaddingBottom = 2,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                    }
                    // </Educational Address Row-7>
                    // <Educational Address Row-7>
                    
                 
                    // </Educational Address Row-7>

                    break;
                }
            }
            #endregion HSC Details

            #region Graduation Details
            for (int i = 0; i < dsEdu.Tables[0].Rows.Count; i++)
            {

                if (dsEdu.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("3"))
                {
                    DataTable tblUG_Filtered = dsEdu.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(3))
                            .CopyToDataTable();
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
                    {
                        Colspan = 4,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Graduation Details", verdana13))
                    {
                        Colspan = 4,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // <Educational Address Row-1>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Institution Name / School", verdana11))
                    {

                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["INSTITUTION_NAME"].ToString(), verdana11))
                    {
                        Colspan = 3,
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });


                    tableLayout.AddCell(new PdfPCell(new Phrase("Enrollment No.", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["REGNO"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                  
                    tableLayout.AddCell(new PdfPCell(new Phrase("Country", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["COUNTRYNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                   
                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("State", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["STATENAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("University/Board", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["BOARDNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("Qualify Degree", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["OTHER_QUALI_DEGREE"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Qualify Program", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["OTHER_QUALI_PROGRAM"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-3>
                    // <Educational Address Row-3>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Specialization", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["SPECIALIZATION"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("No. of Semesters", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows.Count.ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-3>

                    // <Educational Address Row-5>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Passing of Month-Year", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["MONTH_OF_PASSING_NAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Number of Attempts", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["NO_OF_ATTEMPTS"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-5>
                    // <Educational Address Row-5>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Course Duration From", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["YEAR_OF_STUDY_FROM"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Evaluation Type", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["EVALUATION_TYPE_TEXT"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    // </Educational Address Row-5>
                    // <Educational Address Row-5>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Course Duration To", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["YEAR_OF_STUDY_TO"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("CGPA", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["UGPG_CGPA"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-5>
                    // <Educational Address Row-6>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Maximum CGPA", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["UGPG_MAXCGPA"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Current % of Marks", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["PERCENTAGE"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-6>
                    // <Educational Address Row-7>
                    tableLayout.AddCell(new PdfPCell(new Phrase("% from CGPA", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["UGPG_CGPA_PER"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Year of Passing", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["YEAR_OF_PASSING"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-7>

                    // <Educational Address Row-7>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Certificate Upload", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblUG_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-7>

                    break;
                }
            }
            #endregion Graduation Details

            #region Post Graduation Details
            for (int i = 0; i < dsEdu.Tables[0].Rows.Count; i++)
            {

                if (dsEdu.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("4"))
                {
                    DataTable tblPG_Filtered = dsEdu.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(4))
                            .CopyToDataTable();
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana13))
                    {
                        Colspan = 4,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Post Graduation Details", verdana13))
                    {
                        Colspan = 4,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // <Educational Address Row-1>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Institution Name / School", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["INSTITUTION_NAME"].ToString(), verdana11))
                    {
                        Colspan = 3,
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Enrollment No.", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["REGNO"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                  
                    tableLayout.AddCell(new PdfPCell(new Phrase("Country", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["COUNTRYNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    
                 
                    tableLayout.AddCell(new PdfPCell(new Phrase("State", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["STATENAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("University/Board", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["BOARDNAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                   
                    tableLayout.AddCell(new PdfPCell(new Phrase("Qualify Degree", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["OTHER_QUALI_DEGREE"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Qualify Program", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["OTHER_QUALI_PROGRAM"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-3>
                    // <Educational Address Row-3>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Specialization", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["SPECIALIZATION"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("No. of Semesters", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows.Count.ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-3>

                    // <Educational Address Row-5>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Passing of Month-Year", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["MONTH_OF_PASSING_NAME"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Number of Attempts", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["NO_OF_ATTEMPTS"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-5>
                    // <Educational Address Row-5>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Course Duration From", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["YEAR_OF_STUDY_FROM"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase("Evaluation Type", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["EVALUATION_TYPE_TEXT"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    
                    // </Educational Address Row-5>
                    // <Educational Address Row-5>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Course Duration To", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["YEAR_OF_STUDY_TO"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("CGPA", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["UGPG_CGPA"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-5>
                    // <Educational Address Row-6>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Maximum CGPA", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["UGPG_MAXCGPA"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Current % of Marks", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["PERCENTAGE"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-6>
                    // <Educational Address Row-7>
                    tableLayout.AddCell(new PdfPCell(new Phrase("% from CGPA", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["UGPG_CGPA_PER"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase("Year of Passing", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["YEAR_OF_PASSING"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-7>

                    // <Educational Address Row-7>
                    tableLayout.AddCell(new PdfPCell(new Phrase("Certificate Upload", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" : " + tblPG_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString(), verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout.AddCell(new PdfPCell(new Phrase(" ", verdana11))
                    {
                        Border = 0,
                        PaddingBottom = 2,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    // </Educational Address Row-7>
                    break;
                }


            }
            #endregion Post Graduation Details

            
        }
        #endregion Educational Details


         return tableLayout;
    }

    //Added By Kajal Jaiswal on 05-04-2023
    private PdfPTable PDPCertificateWorkshop1(PdfPTable tableLayout1, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
       

        #region TestScore
        float[] headers2 = { 20, 15, 20, 15, 15, 15 }; //Header Widths  
        tableLayout1.SetWidths(headers2); //Set the pdf headers  
        tableLayout1.WidthPercentage = 100;
        DataSet dstestscore = new DataSet();
        dstestscore = objreport.GetTestScoreDetails(userno);
        if (dstestscore != null && dstestscore.Tables.Count > 0)
        {
            tableLayout1.AddCell(new PdfPCell(new Phrase("", verdana11))
            {
                Colspan = 6,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Test Details ", verdana14))
            {
                Colspan = 6,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Test Name", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Year", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Roll/Registration No.", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Total Score Out of", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Score Obtained", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout1.AddCell(new PdfPCell(new Phrase("Is Score Card Upload", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            foreach (DataRow table in dstestscore.Tables[0].Rows)
            {
                tableLayout1.AddCell(new PdfPCell(new Phrase(table["TESTNAME"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout1.AddCell(new PdfPCell(new Phrase(table["QUALIFY_YEAR"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout1.AddCell(new PdfPCell(new Phrase(table["REGNO"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout1.AddCell(new PdfPCell(new Phrase(table["SCORE_OUT_OF"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout1.AddCell(new PdfPCell(new Phrase(table["SCORE_OBTAINED"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout1.AddCell(new PdfPCell(new Phrase(table["BLOB_CERTIFICATE_NAME"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

            }
        }

        tableLayout1.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Colspan = 6,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        #endregion TestScore
        return tableLayout1;
    }


    //Added By Kajal Jaiswal on 05-04-2023
    private PdfPTable PDPCertificateWorkshop2(PdfPTable tableLayout2, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
      
        float[] headers = { 20, 70 }; //Header Widths  
        tableLayout2.SetWidths(headers); //Set the pdf headers  
        tableLayout2.WidthPercentage = 100;

        #region UploadDocument

        DataSet dsUploadDocument = new DataSet();
        dsUploadDocument = objreport.GetDocumentUploadDetails(userno);
        if (dsUploadDocument != null && dsUploadDocument.Tables.Count > 0)
        {

            tableLayout2.AddCell(new PdfPCell(new Phrase("Upload Document", verdana14))
            {
                Colspan = 2,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout2.AddCell(new PdfPCell(new Phrase("Document Name", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout2.AddCell(new PdfPCell(new Phrase("Document Upload Status", verdana11))
            {
                Border = Rectangle.BOX,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            foreach (DataRow table in dsUploadDocument.Tables[0].Rows)
            {
                tableLayout2.AddCell(new PdfPCell(new Phrase(table["DOCUMENTNAME"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(table["DOCUMENT_STATUS"].ToString(), verdana11))
                {
                    Border = Rectangle.BOX,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

            }
        }
        tableLayout2.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Colspan = 2,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        #endregion UploadDocument


       #region Final Submission
        DataSet dsfinal = new DataSet();
        dsfinal = objreport.GetFinalSubmission(userno);
        if (dsfinal != null && dsfinal.Tables.Count > 0)
        {
        tableLayout2.AddCell(new PdfPCell(new Phrase("Final Submission", verdana14))
        {
            Colspan = 2,
            Border = 0,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase("How do you know about DAIICT", verdana11))
        {
            Colspan = 2,
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout2.AddCell(new PdfPCell(new Phrase("Newspaper Advertisement", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["Newspaper Advertisement"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout2.AddCell(new PdfPCell(new Phrase("Social Media ", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["Social Media"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase("SMS/Email", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["SMS/Email"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase("Coaching Centre", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["Coaching Centre"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase("Friends/Relatives", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["Friends/Relatives"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase("Google Search", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["Google Search"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase("Other", verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
        tableLayout2.AddCell(new PdfPCell(new Phrase(dsfinal.Tables[0].Rows[0]["Other"].ToString(), verdana11))
        {
            Border = Rectangle.BOX,
            PaddingBottom = 5,
            HorizontalAlignment = Element.ALIGN_LEFT
        });

        tableLayout2.AddCell(new PdfPCell(new Phrase("", verdana11))
        {
            Colspan=2,
            Border =0 ,
            PaddingBottom = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        });
    }
       #endregion Final Submission


        #region Application Fee
        DataSet dsapplicationfee = objreport.GetApplyProgramDetails(userno,2);
        if (dsapplicationfee != null && dsapplicationfee.Tables.Count > 0)
        {
            tableLayout2.AddCell(new PdfPCell(new Phrase("Application Fee Details", verdana14))
            {
                Colspan = 2,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout2.AddCell(new PdfPCell(new Phrase("", verdana11))
            {
                Colspan = 2,
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER,
                PaddingBottom = 2,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
       
            foreach (DataRow table in dsapplicationfee.Tables[0].Rows)
            {
               
                tableLayout2.AddCell(new PdfPCell(new Phrase("Program Name", verdana11))
                {
                    Border = Rectangle.LEFT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(" : " + table["DEGREEPROGRAM"].ToString(), verdana11))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });


                tableLayout2.AddCell(new PdfPCell(new Phrase("Payment Status", verdana11))
                {
                    Border = Rectangle.LEFT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(" : " + table["PAYMENT_STATUS"].ToString(), verdana11))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase("Payment Category", verdana11))
                {
                    Border = Rectangle.LEFT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(" : " + table["PAYMENT_CATEGORY"].ToString(), verdana11))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase("Amount", verdana11))
                {
                    Border = Rectangle.LEFT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(" : " + table["FEES"].ToString(), verdana11))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase("Transaction ID", verdana11))
                {
                    Border = Rectangle.LEFT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(" : " + table["APP_FEES_PAID_TRANSACTIONID"].ToString(), verdana11))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                
              
                tableLayout2.AddCell(new PdfPCell(new Phrase("Payment Date", verdana11))
                {
                    Border = Rectangle.LEFT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase(" : " + (table["REC_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(table["REC_DT"].ToString()).ToString("dd/MM/yyyy")), verdana11))
                {
                    Border = Rectangle.RIGHT_BORDER,
                    PaddingBottom = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                tableLayout2.AddCell(new PdfPCell(new Phrase("", verdana11))
                {
                    Colspan = 2,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

            }

            tableLayout2.AddCell(new PdfPCell(new Phrase("Total Fees Paid ", verdana11))
            {
                Border =  Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout2.AddCell(new PdfPCell(new Phrase( " : "+ dsapplicationfee.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString(), verdana11))
            {
                Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                PaddingBottom =5,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }


        #endregion Application fee

        return tableLayout2;
    }


    //Added By Saurabh Kumare on 05-04-2023
    private PdfPTable PDPCertificateWorkshop3(PdfPTable tableLayout3, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
       
        #region Other Details
        float[] headers3 = { 8,8,8,8,8,8,8,8,8,8,8,8 }; //Header Widths  
        tableLayout3.SetWidths(headers3); //Set the pdf headers  
        tableLayout3.WidthPercentage = 100;

        DataSet dsEdu = objreport.GetEducationalDetails(userno);
        if (dsEdu.Tables[0] != null && dsEdu.Tables[0].Rows.Count > 0)
        {
            
            
            for (int i = 0; i < dsEdu.Tables[0].Rows.Count; i++)
            {


                if (dsEdu.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("5"))
                {
                    DataTable tblOQ_Filtered = dsEdu.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(5))
                            .CopyToDataTable();

                    tableLayout3.AddCell(new PdfPCell(new Phrase("Other Details", verdana13))
                    {
                        Colspan = 12,
                        Border = 0,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    tableLayout3.AddCell(new PdfPCell(new Phrase("Institution Name / School", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("University / Board", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Program", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Country", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("State", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Course Duration", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Year of Passin", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Total Marks Obtained", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Maximum Mark", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("% of Marks", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("Other Details", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
                    tableLayout3.AddCell(new PdfPCell(new Phrase("is File Uploaded?", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    

                    foreach (DataRow table in tblOQ_Filtered.Rows)
                    {
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["INSTITUTION_NAME"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["BOARDNAME"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["OTHER_QUALI_DEGREE"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["COUNTRYNAME"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["STATENAME"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["YEAR_OF_STUDY_FROM"].ToString() + "-" + tblOQ_Filtered.Rows[0]["YEAR_OF_STUDY_TO"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["YEARNAME"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["ALL_SUBJECT_TOTAL_MARKS_OBTAINED"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["ALL_SUBJECT_TOTAL_MAX_MARK"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["PERCENTAGE"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["OTHER_DETAILS"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout3.AddCell(new PdfPCell(new Phrase(table["IS_UPLOAD_DOCS"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });

                    }


                    break;
                }
                }
        }
        #endregion Other Details

       

        return tableLayout3;
    }

    //Added By Saurabh Kumare on 06-04-2023
    private PdfPTable PDPCertificateWorkshop4(PdfPTable tableLayout4, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
       
        #region Work Experience Details
        float[] headers3 = { 12,12,12,12,12,12,12,12 }; //Header Widths  
        tableLayout4.SetWidths(headers3); //Set the pdf headers  
        tableLayout4.WidthPercentage = 100;
        Common objCommon = new Common();
        DataSet ds = objCommon.FillDropDown("ACD_USER_WORK_EXPERIENCE", "ROW_NUMBER() OVER(ORDER BY [USERNO]) AS ID,USERNO,EMPLOYEMENT_ID,WORK_EXPERIENCE,ORGANIZATION,DESIGNATION,START_DURATION,END_DURATION,NATURE_WORK,NATURE_BUSINESS,MONTHLY_REMUNERATION", "",
            "USERNO=" + userno + "AND EMPLOYEMENT_ID=0", string.Empty);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            tableLayout4.AddCell(new PdfPCell(new Phrase("Work Experience Details", verdana14))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout4.AddCell(new PdfPCell(new Phrase("Work Experience", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Organization", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Designation", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Duration - From", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Duration - To", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Nature of Work", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Nature of Business", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });
            tableLayout4.AddCell(new PdfPCell(new Phrase("Monthly Remuneration", verdana11))
                    {

                        Border = Rectangle.BOX,
                        PaddingBottom = 10,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });


            foreach (DataRow table in ds.Tables[0].Rows)
                    {
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["WORK_EXPERIENCE"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["ORGANIZATION"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["DESIGNATION"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["START_DURATION"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["END_DURATION"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["NATURE_WORK"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["NATURE_BUSINESS"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                        tableLayout4.AddCell(new PdfPCell(new Phrase(table["MONTHLY_REMUNERATION"].ToString(), verdana11))
                        {

                            Border = Rectangle.BOX,
                            PaddingBottom = 10,
                            HorizontalAlignment = Element.ALIGN_LEFT
                        });
                    }
            tableLayout4.AddCell(new PdfPCell(new Phrase(" ", verdana11))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }
        #endregion  Work Experience Details



        return tableLayout4;
    }

    //Added By Saurabh Kumare on 06-04-2023
    private PdfPTable PDPCertificateWorkshop5(PdfPTable tableLayout5, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 12, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);
       
        #region Entrepreneur Details
        float[] headers3 = { 12, 12, 12, 12, 12, 12, 12, 12 }; //Header Widths  
        tableLayout5.SetWidths(headers3); //Set the pdf headers  
        tableLayout5.WidthPercentage = 100;
        Common objCommon = new Common();
        DataSet ds = objCommon.FillDropDown("ACD_USER_WORK_EXPERIENCE", "ROW_NUMBER() OVER(ORDER BY [USERNO]) AS ID,USERNO,EMPLOYEMENT_ID,WORK_EXPERIENCE,ORGANIZATION,DESIGNATION,START_DURATION,END_DURATION,NATURE_WORK,NATURE_BUSINESS,MONTHLY_REMUNERATION", "",
            "USERNO=" + userno + "AND EMPLOYEMENT_ID=1", string.Empty);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            tableLayout5.AddCell(new PdfPCell(new Phrase("Entrepreneur Details", verdana14))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout5.AddCell(new PdfPCell(new Phrase("Work Experience", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Organization", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Designation", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Duration - From", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Duration - To", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Nature of Work", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Nature of Business", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout5.AddCell(new PdfPCell(new Phrase("Monthly Remuneration", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });


            foreach (DataRow table in ds.Tables[0].Rows)
            {
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["WORK_EXPERIENCE"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["ORGANIZATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["DESIGNATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["START_DURATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["END_DURATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["NATURE_WORK"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["NATURE_BUSINESS"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout5.AddCell(new PdfPCell(new Phrase(table["MONTHLY_REMUNERATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });


            }
            tableLayout5.AddCell(new PdfPCell(new Phrase(" ", verdana11))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
        }
        #endregion Entrepreneur Details



        return tableLayout5;
    }

    private PdfPTable PDPCertificateWorkshop6(PdfPTable tableLayout6, int userno)
    {
        Font verdana13 = FontFactory.GetFont("Verdana", 13, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana11 = FontFactory.GetFont("Verdana", 10, Font.NORMAL, iTextSharp.text.Color.BLACK);
        Font verdana14 = FontFactory.GetFont("Verdana", 12, Font.UNDERLINE, iTextSharp.text.Color.BLACK);

        #region Reference Details
        float[] headers3 = { 20,20,20,20,20 }; //Header Widths  
        tableLayout6.SetWidths(headers3); //Set the pdf headers  
        tableLayout6.WidthPercentage = 100;
        Common objCommon = new Common();
        DataSet ds = objCommon.FillDropDown("ACD_USER_REFERENCE", "USERNO,REFERENCE_NAME,ORGANIZATION,DESIGNATION,EMAIL_ID,MOBILE_NO", "", "USERNO=" + userno, string.Empty);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            tableLayout6.AddCell(new PdfPCell(new Phrase("Reference Details", verdana14))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            tableLayout6.AddCell(new PdfPCell(new Phrase("Reference Name", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout6.AddCell(new PdfPCell(new Phrase("Organization", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout6.AddCell(new PdfPCell(new Phrase("Designation", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout6.AddCell(new PdfPCell(new Phrase("Email", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tableLayout6.AddCell(new PdfPCell(new Phrase("Mobile No", verdana11))
            {

                Border = Rectangle.BOX,
                PaddingBottom = 10,
                HorizontalAlignment = Element.ALIGN_LEFT
            });

            foreach (DataRow table in ds.Tables[0].Rows)
            {
                tableLayout6.AddCell(new PdfPCell(new Phrase(table["REFERENCE_NAME"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout6.AddCell(new PdfPCell(new Phrase(table["ORGANIZATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout6.AddCell(new PdfPCell(new Phrase(table["DESIGNATION"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout6.AddCell(new PdfPCell(new Phrase(table["EMAIL_ID"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                tableLayout6.AddCell(new PdfPCell(new Phrase(table["MOBILE_NO"].ToString(), verdana11))
                {

                    Border = Rectangle.BOX,
                    PaddingBottom = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
               

            }
        }
        #endregion  Reference Details



        return tableLayout6;
    }

    #endregion PrintApplicationFormReport
}
