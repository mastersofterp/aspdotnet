using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Net.Mail;
using System.Text;
using System.IO;

using iTextSharp.text.pdf;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;

using System.Net;


using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using System.Drawing.Printing;

public partial class Reports_Tabulation_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                int SessionNo = Convert.ToInt32(Request.QueryString["SESSIONNO"].ToString());
                int CollegeId = Convert.ToInt32(Request.QueryString["COLLEGEID"].ToString());
                int DegreeNo = Convert.ToInt32(Request.QueryString["DEGREENO"].ToString());
                int BranchNo = Convert.ToInt32(Request.QueryString["BRANCHNO"].ToString());
                string Idno = Request.QueryString["IDNO"].ToString();
                int SemesterNo = Convert.ToInt32(Request.QueryString["SEMESTERNO"].ToString());
                int StudentType = Convert.ToInt32(Request.QueryString["STUDENTTYPE"].ToString());
                int SchemeNo = Convert.ToInt32(Request.QueryString["SCHEMENO"].ToString());
                int College_Code = Convert.ToInt32(Request.QueryString["COLLEGE_CODE"].ToString());

                string Publish_Date = Request.QueryString["PUBLISH_DATE"].ToString();


                Report(CollegeId, SessionNo, DegreeNo, BranchNo, Idno, SemesterNo, StudentType, SchemeNo, College_Code);

            }

        }

    }
    protected void Report(int collegeId, int SessionNo, int DegreeNo, int BranchNo, string Idno, int SemesterNo, int StudentType, int SchemeNo, int College_Code)
    {
        Common objCommon = new Common();
        string test = "";
        string report = "";
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_HTML_REPORTS_TEMPLATE", "TEMPLATE", "", "PAGE_NO=256", "");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string template = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();

            string SP_Name1 = "PKG_ACD_EXAM_TAB_REGISTAR_REPORT_HITS";
            string SP_Parameters1 = "@P_COLLEGEID,@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_IDNO,@P_STUDENTTYPE";
            string Call_Values1 = "" + collegeId + "," + SessionNo + "," + DegreeNo + "," + BranchNo + "," + SemesterNo + "," + Idno + "," + StudentType;
            DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            string SP_Name2 = "PKG_ACD_GRADE_STATUS";
            string SP_Parameters2 = "@P_SCHEMENO";
            string Call_Values2 = "" + SchemeNo;
            DataSet ds2 = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

            string SP_Name3 = "PKG_ACD_GET_SCHEMEWISE_SUBJECTS_HITS";
            string SP_Parameters3 = "@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO";
            string Call_Values3 = "" + SessionNo + "," + SemesterNo + "," + SchemeNo;
            DataSet ds3 = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);

            string SP_Name4 = "PKG_REPORT_COLLEGE_INFO";
            string SP_Parameters4 = "@P_COLLEGE_CODE";
            string Call_Values4 = "" + College_Code;
            DataSet ds4 = objCommon.DynamicSPCall_Select(SP_Name4, SP_Parameters4, Call_Values4);

            GenerateHTMLReport(ds1, ds2, ds3, ds4);
            Console.WriteLine("HTML report generated successfully for DivReport.");
        }
 
        catch { }
    }

    protected void GenerateHTMLReport(DataSet ds1, DataSet ds2, DataSet ds3, DataSet ds4)
    {
        int maxCourse = 15;
        string ccodes = "";
        string outofmarks = "";
        int ccount = 0;
        string logoBase64 = Convert.ToBase64String((byte[])ds4.Tables[0].Rows[0]["COLLEGE_LOGO"]);
        string semType = "";
        int prev = Convert.ToInt32( ds1.Tables[0].Rows[0]["Prev_Status"]);
        if (prev == 0)
        {
            semType = "Regular";
        }
        else 
        {
            semType = "Backlog";
        }
        
        // Assuming you have some data to populate the report
        //string reportContent = "<h2>Sample Report</h2><p>This is a sample report generated dynamically.</p>";
        int page = 0;

        string reportContent = @"
            <div class='text-dark'>
                <div class='col-12 text-center'>
                    <img id='imglogo' class='w-75' src='data:image/png;base64," + logoBase64 + @"'  alt='logo' /><br />
            
                    <label>" + ds1.Tables[0].Rows[0]["DCODE"] + @"- " + ds1.Tables[0].Rows[0]["SEMESTERNAME"] + @" Semester- " + semType + @"- University Exam "+ds1.Tables[0].Rows[0]["SESSIONNAME"]+@"</label><br />
                    <label>" + ds1.Tables[0].Rows[0]["DCODE"] + @" " + ds1.Tables[0].Rows[0]["LONGNAME"] + @" SEMESTER " + ds1.Tables[0].Rows[0]["SEMESTERNO"] + @" </label><br />
                    <label>SEMESTER EXAMINATION RESULT</label>
                </div>
                <div class='col-12 mt-3'>
                    <table class='table table-striped table-bordered nowrap' style='width: 100%;border: 1px solid #dee2e6;'>
                        <thead class='bg-light-blue'>
                            <tr>
                                <th>Reg No</th>
                                <th>Name</th>
                                 ";
                    ccodes = "";
                    ccount = 0;
                    
                    for (int j = 1; j < maxCourse; j++)
                    {
                        string code1 = "CCODE" + j;
                        string outofmarks1 = "INTERMAX" + j;
                        
                        if (ds1.Tables[0].Rows[0][code1] != DBNull.Value && ds1.Tables[0].Rows[0][code1] != "")
                        {

                            ccodes += code1 + ",";
                            outofmarks += Convert.ToSingle(ds1.Tables[0].Rows[0][outofmarks1]) + ",";
                            ccount++;
                        }
                    }
                    string[] ccode1 = ccodes.Split(',');
                    string[] outofmarks2 = outofmarks.Split(',');
                    for (int k = 1; k < ccode1.Length; k++)
                    {
                        string a = ccode1[k];
                        reportContent += @"
                                    <th class='text-center'>C" + k + @"
                                        <div class='d-flex justify-content-between align-items-center' style='font-size: 10px;'>
                                             <span>Gr</span>
                                             <span>CIA/" + outofmarks2[k-1] + @"</span>
                                         </div>
                                    </th>";
                    }
                    reportContent += @"                    
                                <!-- Add more table headers here -->
                            </tr>
                        </thead>
                        <tbody>
                            ";
                    // Loop through the data and populate table rows
            
                    // string[] ccode = ccodes.Split(',');
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
            
                        //for(int k = 0;k<ccode.Length;k++)
                        //{
                        //    string a = ccode[k];
                        //}
                        reportContent += @"
                           <tr>
                                   <td>" + ds1.Tables[0].Rows[i]["REGNO"] + @"</td>
                                   <td>" + ds1.Tables[0].Rows[i]["STUDNAME"] + @"</td> ";
                        ccodes = "";
                        ccount = 0;
            
                        for (int j = 1; j < maxCourse; j++)
                        {
                            string code = "CCODE" + j;
                            if (ds1.Tables[0].Rows[0][code] != DBNull.Value && ds1.Tables[0].Rows[0][code] != "")
                            {
                                ccodes += code + ",";
                                ccount++;
                            }
                        }
                        string[] ccode = ccodes.Split(',');
                        for (int k = 1; k < ccode.Length; k++)
                        {
                            string a = ccode[k];
                            reportContent += @"
                                         <td class='text-center'>" + ds1.Tables[0].Rows[i]["CCODE" + k] + @"
                                               <div class='d-flex justify-content-between align-items-center' style='font-size: 10px;'>
                                                   <span>" + ds1.Tables[0].Rows[i]["GRADE" + k] + @"</span>
                                                  <span>" + ds1.Tables[0].Rows[i]["INTERMARK" + k] + @"</span>
                                              </div>
                                         </td>";
                        }
                        reportContent += @"
                                   
                                   
                           </tr>";
                    }
                    // Closing tags for the table and other elements
                    reportContent += @"
                        </tbody>
                    </table>
                         </div>
                         <div class='col-12'>
                        <p style='font-size: 9px;'><span>CIA - Continuous Internal Assessment; G- Grade; Marks:
                    ";
                    for (int m = 0; m < ds2.Tables[0].Rows.Count; m++)
                    {
                        reportContent += @"
                       " + ds2.Tables[0].Rows[m]["MINMARK"] + @" - " + ds2.Tables[0].Rows[m]["MAXMARK"] + @" Grade '" + ds2.Tables[0].Rows[m]["GRADE"] + @"';
                        ";
                    }
                    reportContent += @"
                  AB – Absent; RA – Repeat</span></p>
                </div>
                <div class='col-12 mt-3'>
                    <div class='border-bottom'>
                        <div class='row pb-2'>
                            <div class='col-6 text-center'>
                                <label><b>Controller of Examinations</b></label>
                            </div>
                            <div class='col-6 text-center'>
                                <label><b>Vice Chancellor</b></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class='col-12 mt-5'>
                    <div class='row'>
                       
                        
                            ";
                    for (int n = 0; n < ds3.Tables[0].Rows.Count; n++)
                    {
                        reportContent += @"
                                
<div class='col-md-4 col-12'>" + ds3.Tables[0].Rows[n]["CCODE"] + @" " + ds3.Tables[0].Rows[n]["COURSENAME"] + @" (SM/" + ds3.Tables[0].Rows[n]["MAXMARKS_I"] + @");</div>
                                  ";
                    }
                    reportContent += @"
                       
                    </div>
                </div>
            </div>";



        // Create a new HtmlGenericControl to hold the HTML content
        HtmlGenericControl divContent = new HtmlGenericControl("div");
        divContent.InnerHtml = reportContent;

        // Find the DivReport control and add the dynamically generated content
        DivReport.Controls.Add(divContent);
    }
   
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //DivReport.InnerHtml = test.ToString();
        DivReport.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 5f, 10f, 5f, 10f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();
    }

   
}
