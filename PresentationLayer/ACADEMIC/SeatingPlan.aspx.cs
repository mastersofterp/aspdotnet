//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Seating Arrangement Entry                                                         
// CREATION DATE : 14-FEB-2012                                                     
// CREATED BY    : UMESH K. GANORKAR                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;
using Microsoft.Office;
//using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
public partial class ACADEMIC_SeatingPlan : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    SeatingController objSc = new SeatingController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // Set form mode equals to -1(New Mode).
                // ViewState["exdtno"] = "0";

                this.PopulateDropDown();
                //this.BindDates();
                divMsg.InnerHtml = string.Empty;
                //lvDate.Enabled = true;
                btnReport.Visible = false;
            }
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Term
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlRSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            //Exam Slot
            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME", "SLOTNO > 0", "SLOTNO");
            objCommon.FillDropDownList(ddlRSlot, "ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME", "SLOTNO > 0", "SLOTNO");

            //Exam Slot
           // objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0", "ROOMNO");

            //Exam Time Table Types
            ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
            ddlExTTType.Items.Add(new ListItem("Mid Exam ", "1"));
            ddlExTTType.Items.Add(new ListItem("End Exam ", "2"));

            ddlRExam.Items.Add(new ListItem("Please Select", "0"));
            ddlRExam.Items.Add(new ListItem("Mid Exam ", "1"));
            ddlRExam.Items.Add(new ListItem("End Exam ", "2"));

            //Exam Day Nos.
            ddlDay.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 1; i <= 80; i++)
            {
                ddlDay.Items.Add(new ListItem(i.ToString()));
            }
            ddlRDay.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 1; i <= 80; i++)
            {
                ddlRDay.Items.Add(new ListItem(i.ToString()));
            }

            //Room
            objCommon.FillDropDownList(ddlRRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0", "ROOMNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH","BATCHNO","BATCHNAME","BATCHNO > 0","BATCHNO");
        ddlPosition.SelectedIndex = 0;
    }

    //private void BindCoursesList()
    //{
    //    try
    //    {
    //        //DataSet ds = objCommon.FillDropDown("ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO = C.COURSENO INNER JOIN ACD_PATH P ON OC.PATHNO = P.PATHNO INNER JOIN ACD_DIVISION D ON D.DIVISIONNO = OC.DIVISIONNO", "C.COURSENO,P.PATHNO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME,D.DIVISIONNO,D.DIVISIONNAME,OC.BATCHNO,C.SUBID", "OC.DIVISIONNO = " + ddlDivision.SelectedValue + " AND OC.SESSIONNO = " + ddlSession.SelectedValue + " AND P.DEPTNO = " + ddlDepartment.SelectedValue, "C.LEVELNO,C.SRNO");
    //        DataSet ds = objSc.GetCoursesForSeatingArrangement(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), txtExamDate.Text, Convert.ToInt32(ddlSlot.SelectedValue));
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlCourse.Items.Clear();
    //                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
    //                ddlCourse.DataTextField = "COURSE";
    //                ddlCourse.DataValueField = "COURSENO";
    //                ddlCourse.DataSource = ds.Tables[0];
    //                ddlCourse.DataBind();
    //            }
    //            else
    //            {
    //                ddlCourse.Items.Clear();
    //                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
    //            }
    //        }
    //        else
    //        {
    //            ddlCourse.Items.Clear();
    //            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_teacherallotment.ddlDepartment_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //private void BindStudentList()
    //{
    //    try
    //    {
    //        try
    //        {

    //            DataSet ds = objSc.GetStudentsForSeatingArrangement(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
    //            if (ds != null && ds.Tables[0].Rows.Count > 0)

    //                if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    lvStudent.DataSource = ds.Tables[0];
    //                    lvStudent.DataBind();
    //                    int i = 0;
    //                    foreach (ListViewDataItem item in lvStudent.Items)
    //                    {
    //                        CheckBox cbrow = item.FindControl("cbRow") as CheckBox;
    //                        {
    //                            cbrow.Checked = false;
    //                            //txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();
    //                        }
    //                        i++;
    //                    }
    //                    hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
    //                    //pnlMain.Visible = true;
    //                }
    //                else
    //                {
    //                    lvStudent.DataSource = null;
    //                    lvStudent.DataBind();
    //                    hdfTot.Value = "0";

    //                }
    //            //else
    //            //{
    //            //    lblChkStud.Visible = true;
    //            //    lblChkStud.Text = "Registered Students not found for Course " + ddlCourse.SelectedItem.Text + "";
    //            //}
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "Academic_Examination.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_teacherallotment.BindStudentList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
   
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = (CustomStatus)objSc.SeatAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue), Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue),Convert.ToInt32(txtStudBench.Text),Convert.ToInt32(ddlPosition.SelectedValue),Session["colcode"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Seating Arrangement Done ...!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage("Error while Seating Arrangement..", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SeatingPlan.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    
    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        //we gets days no. as well as Exam dates on  LvDates bindlistview 
        DataSet dsBC = objExamController.BindDate(Convert.ToInt32(ddlDay.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue));
        if (dsBC != null && dsBC.Tables[0].Rows.Count > 0)
        {
            txtExamDate.Text = dsBC.Tables[0].Rows[0][0].ToString();
            //txtExamDate.Text = dsBC.Tables[0].ToString();
            txtExamDate.Enabled = false;
            ddlSlot.SelectedIndex = 0;
            //ddlCourse.SelectedIndex = 0;
        }
        else
        {
            txtExamDate.Text = "";
            txtExamDate.Enabled = true;
            ddlSlot.SelectedIndex = 0;
            //ddlCourse.SelectedIndex = 0;

        }
    }
    protected void ddlExTTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlExTTType.SelectedIndex = 0;
        //ddlDay.SelectedIndex = 0;
        //txtExamDate.Text = string.Empty;
        //ddlSlot.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;
        //ddlRoom.SelectedIndex = 0;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    ShowReport("SeatingArrangement", "rptSeatArrangement.rpt");
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_CoursewiseStudentReport2.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EXAMNO=" + ddlExTTType.SelectedValue + ",@P_DAYNO=" + ddlDay.SelectedValue + ",@P_EXAMDATE=" + txtExamDate.Text + ",@P_SLOTNO=" + ddlSlot.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_ROOMNO=" + ddlRoom.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ROOMNAME=" + ddlRoom.SelectedItem.Text + " ";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";


    //        //To open new window from Updatepanel
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPosition.SelectedIndex = 0;
    }
    //private void ShowSeatingArrangement()
    //{

    //    SeatingController objSeatcon = new SeatingController();

    //    Application xlApp;
    //    Workbook xlWorkBook;
    //    Worksheet xlWorkSheet;
    //    Excel.Range chartRange;

    //    object misValue = System.Reflection.Missing.Value;
    //    string slot = string.Empty;
    //    string data = string.Empty;
    //    string data2 = string.Empty;

    //    xlApp = new ApplicationClass();
    //    xlWorkBook = xlApp.Workbooks.Add(misValue);
    //    string file = string.Empty;
    //    String nl = Environment.NewLine;
    //    string date = txtExamDate.Text;
    //    int slotno = Convert.ToInt32(ddlSlot.SelectedValue);
    //    DataSet room = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT", "DISTINCT ROOMNO", "DBO.FN_DESC('ROOMNAME',ROOMNO)ROOMNAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND CONVERT(NVARCHAR(30),EXAMDATE,103)='" + date + "' AND SLOTNO=" + slotno, "ROOMNO");
    //    if (room.Tables[0].Rows.Count > 0)
    //    {

    //        for (int r = 0; r < room.Tables[0].Rows.Count; r++)
    //        {

    //           // Add a worksheet to the workbook.
    //            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
    //            xlWorkSheet.Name = room.Tables[0].Rows[r]["ROOMNAME"].ToString();


    //            DataSet ds = objSeatcon.GetBranchDetails(Convert.ToInt32(ddlSession.SelectedValue), date, Convert.ToInt16(room.Tables[0].Rows[r]["ROOMNO"]), slotno); // 0 for all roomno in selected session and date 

    //            if (ds != null && ds.Tables[0].Rows.Count > 0)
    //            {

    //                file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SeatingPlan_" + (date.Replace("/", "_") + "_" + ds.Tables[0].Rows[0]["SLOTNAME"].ToString().Replace(" ", "_")).Replace(":", "_") + ".xls";
    //                file = "My system drive is %SystemDrive% and my system root is %SystemRoot%";
    //                string str = Environment.ExpandEnvironmentVariables(file);

    //                file = System.Environment.SpecialFolder.Desktop.ToString();

    //                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    //                file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SeatingPlan_" + (date.Replace("/", "_") + "_" + ds.Tables[0].Rows[0]["SLOTNAME"].ToString().Replace(" ", "_")).Replace(":", "_") + ".xls";


    //                slot = ds.Tables[0].Rows[0]["SLOTNAME"].ToString();

    //                xlWorkSheet.Cells[1, 1] = objCommon.LookUp("REFF", "COLLEGENAME", "COLLEGE_CODE=" + Session["colcode"].ToString());

    //                xlWorkSheet.Cells[2, 1] = ddlSession.SelectedItem.Text.ToString();
    //                xlWorkSheet.get_Range("A1", "C1").Font.Size = 12;
    //                chartRange = xlWorkSheet.get_Range("a1", "z50");
    //                chartRange.RowHeight = 18;
    //                chartRange.ColumnWidth = 10;
    //                chartRange.Font.Bold = true;

    //                //COLLEGE HEADING
    //                xlWorkSheet.get_Range("A1", "H1").Font.Size = 16;
    //                xlWorkSheet.get_Range("A1", "H1").Cells.Merge(true);

    //                xlWorkSheet.get_Range("B17", "B17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("D17", "D17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("F17", "F17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("H17", "H17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("J17", "J17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("L17", "L17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("N17", "N17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("P17", "P17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("R17", "R17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("T17", "T17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("V17", "V17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("X17", "X17").ColumnWidth = 15;
    //                xlWorkSheet.get_Range("Z17", "Z17").ColumnWidth = 15;

    //                xlWorkSheet.Cells[3, 5] = "SEATING PLAN";
    //                xlWorkSheet.get_Range("E5", "F5").Cells.Merge(true);
    //                xlWorkSheet.get_Range("E3", "F3").Cells.Font.Size = 14;
    //                xlWorkSheet.get_Range("B2", "G2").HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
    //                xlWorkSheet.get_Range("B2", "G2").Cells.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
    //                xlWorkSheet.Cells[4, 3] = "ROOM NAME: " + ds.Tables[0].Rows[0]["ROOMNAME"].ToString();
    //                xlWorkSheet.get_Range("A4", "C4").Cells.Merge(true);
    //                xlWorkSheet.get_Range("A4", "C4").Cells.Font.Size = 14;

    //                xlWorkSheet.Cells[4, 8] = "EXAM DATE: " + date + " (" + slot + ")";
    //                xlWorkSheet.get_Range("H4", "K4").Cells.Merge(true);
    //                xlWorkSheet.get_Range("H4", "K4").Cells.Font.Size = 14;
    //                xlWorkSheet.Cells[5, 8] = "SEAT NO";
    //                xlWorkSheet.Cells[5, 9] = "TOTAL";
    //                xlWorkSheet.get_Range("H5", "I5").HorizontalAlignment = 3;
    //                xlWorkSheet.Cells[5, 9] = "SHIFT: " + ds.Tables[0].Rows[0]["SLOTNAME"].ToString();

    //                data = "Note: D = For Disable SeatNo., NA = For Not Available on That Seating Position.";
    //                xlWorkSheet.Cells[16, 1] = data;

    //                xlWorkSheet.get_Range("A16", "G16").Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
    //                xlWorkSheet.get_Range("A16", "G16").Font.Size = 10;
    //                xlWorkSheet.get_Range("A16", "G16").Cells.Merge(true);
    //                int startrow = 1;
    //                int row = 0;

    //                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
    //                {

    //                    data = "SEM / BRANCH / CCODE: ";
    //                    data += ds.Tables[0].Rows[i]["SEMESTERNAME"].ToString() + "/" + ds.Tables[0].Rows[i]["BRANCHNAME"].ToString() + "/" + ds.Tables[0].Rows[i]["CCODE"].ToString();
    //                    xlWorkSheet.Cells[6 + i, 1] = data;

    //                    data = ds.Tables[0].Rows[i]["SEATNO_FROM"].ToString() + "-" + ds.Tables[0].Rows[i]["SEATNO_TO"].ToString();
    //                    xlWorkSheet.Cells[6 + i, 8] = data;

    //                    data = ds.Tables[0].Rows[i]["TOTAL"].ToString();
    //                    xlWorkSheet.Cells[6 + i, 9] = data;
    //                    xlWorkSheet.get_Range(xlWorkSheet.Cells[6 + i, 8], xlWorkSheet.Cells[6 + i, 9]).HorizontalAlignment = 3;

    //                    xlWorkSheet.Cells[16, 8] = "TOTAL";
    //                    xlWorkSheet.Cells[16, 9] = "=SUM(I6:I15)";
    //                    xlWorkSheet.get_Range("H16", "I16").HorizontalAlignment = 3;

    //                    data = ds.Tables[0].Rows[i]["CCODE"].ToString() + "-" + ds.Tables[0].Rows[i]["COURSE_NAME"].ToString();
    //                    xlWorkSheet.Cells[6 + i, 9] = data;
    //                }

    //                int j = 0;

    //                for (int c = 0; c < (Convert.ToInt32(ds.Tables[0].Rows[0]["COLUMNS"])) * 2; c++)
    //                {
    //                    for (int a = 0; a < Convert.ToInt32(ds.Tables[0].Rows[0]["ROWS"]); a++)
    //                    {
    //                        if (j < ds.Tables[1].Rows.Count)
    //                        {
    //                            xlWorkSheet.Cells[17, c + 1] = "SR. NO.";
    //                            data = ds.Tables[1].Rows[j]["BENCH"].ToString();
    //                            xlWorkSheet.Cells[18 + a, c + 1] = j + 1;

    //                            xlWorkSheet.Cells[17, c + 2] = "SEAT NOs.";

    //                            data = ds.Tables[1].Rows[j]["SEATNO_ODD"].ToString();
    //                            data2 = ds.Tables[1].Rows[j]["SEATNO_EVEN"].ToString();

    //                            if (data == "D")
    //                            {
    //                                chartRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[18 + a, c + 2], xlWorkSheet.Cells[18 + a, c + 2]);
    //                                chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //                                chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
    //                            }
    //                            else
    //                            {
    //                                xlWorkSheet.Cells[18 + a, c + 2] = data;
    //                                chartRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[18 + a, c + 2], xlWorkSheet.Cells[18 + a, c + 2]);
    //                                chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
    //                            }

    //                            if (data2 == "D")
    //                            {
    //                                chartRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[18 + a, c + 4], xlWorkSheet.Cells[18 + a, c + 4]);
    //                                chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //                            }
    //                            else
    //                            {
    //                                chartRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[18 + a, c + 4], xlWorkSheet.Cells[18 + a, c + 4]);
    //                                chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
    //                            }

    //                            xlWorkSheet.Cells[18 + a, c + 2] = data + "  -  " + data2;
    //                            row = startrow + a;
    //                        }
    //                        j++;

    //                       // Define a range object(A4).
    //                        chartRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[17, c + 1], xlWorkSheet.Cells[18 + a, c + 2]);
    //                        //ALIGN CENTER
    //                        chartRange.HorizontalAlignment = 3;
    //                        //Get the borders collection.
    //                        Borders br = chartRange.Borders;
    //                       // Set the thin lines style.
    //                        br.LineStyle = Excel.XlLineStyle.xlContinuous;
    //                        br.Weight = 2d;
    //                    }
    //                    c = c + 1;
    //                }
    //            }
    //        }
    //        xlWorkBook.SaveAs(file, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue,

    //                          misValue, misValue);

    //        xlWorkBook.Close(true, misValue, misValue);
    //        xlApp.Quit();
    //        releaseObject(xlWorkSheet);
    //        releaseObject(xlWorkBook);
    //        releaseObject(xlApp);
    //        objCommon.DisplayMessage("Seating Plan Saved in " + file , this.Page);
    //        Process.Start(file);
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("No Seating Arrangement Data Found...", this.Page);
    //    }
    //}

    //private void releaseObject(object obj)
    //{
    //    try
    //    {
    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
    //        obj = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        obj = null;
    //    }
    //    finally
    //    {
    //        GC.Collect();
    //    }
    //}

    protected void btnRCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnRReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowSeatAllotment("SeatingArrangement", "rptSeatAllotment.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SeatingPlan.btnRReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowSeatAllotment(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlRSession.SelectedValue + ",@P_EXAMNO=" + ddlRExam.SelectedValue + ",@P_DAYNO=" + ddlRDay.SelectedValue + ",@P_SLOTNO=" + ddlRSlot.SelectedValue + ",@P_ROOMNO=" + ddlRRoom.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@Date=" + txtRdate.Text + ",@Slot=" + ddlRSlot.SelectedItem.Text + ",@ExamName=" + ddlRExam.SelectedItem.Text + ",@SessionName=" + ddlRSession.SelectedItem.Text + ",@Room="+ddlRRoom.SelectedItem.Text+"";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlRDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        //we gets days no. as well as Exam dates on  LvDates bindlistview 
        DataSet dsBC = objExamController.BindDate(Convert.ToInt32(ddlRDay.SelectedValue), Convert.ToInt32(ddlRSession.SelectedValue), Convert.ToInt32(ddlRExam.SelectedValue));
        if (dsBC != null && dsBC.Tables[0].Rows.Count > 0)
        {
            txtRdate.Text = dsBC.Tables[0].Rows[0][0].ToString();
            //txtExamDate.Text = dsBC.Tables[0].ToString();
            txtRdate.Enabled = false;
            ddlSlot.SelectedIndex = 0;
            //ddlCourse.SelectedIndex = 0;
        }
        else
        {
            txtRdate.Text = "";
            txtRdate.Enabled = true;
            ddlSlot.SelectedIndex = 0;
            //ddlCourse.SelectedIndex = 0;

        }
    }
    protected void btnRExcel_Click(object sender, EventArgs e)
    {
        //if (ddlRSession.SelectedIndex <= 0 || ddlRExam.SelectedIndex <= 0 || ddlRDay.SelectedIndex <= 0
        //    || ddlSlot.SelectedIndex<=0 || ddlRRoom.SelectedIndex <=0)
        //{
        //    objCommon.DisplayMessage("Please Select Session/Exam Name/Day/Slot/Room..!!", this.Page);
        //    return;
        //}
        //else
            ShowExcelSheet();
    }

    //Show the Excel Sheet
    private void ShowExcelSheet()
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string slot = objCommon.LookUp("ACD_EXAM_TT_SLOT", "slotname", "slotno=" + ddlRSlot.SelectedValue);
            string room = objCommon.LookUp("acd_room", "roomname", "roomno=" + ddlRRoom.SelectedValue);
            string date = txtRdate.Text;

            //string ContentType = string.Empty;
            //char ch = '/';
            //string[] Fromdate = txtAttDate.Text.Split(ch);
            //string[] Todate = txtRecDate.Text.Split(ch);
            //string fdate = Fromdate[1] + "/" + Fromdate[0] + "/" + Fromdate[2];
            //string tdate = Todate[1] + "/" + Todate[0] + "/" + Todate[2];
            SeatingController objSc = new SeatingController();
            DataSet ds = objSc.GetDataInExcelSheetFoSeatingAllot(Convert.ToInt32(ddlRSession.SelectedValue), Convert.ToInt32(ddlRExam.SelectedValue), Convert.ToInt32(ddlRDay.SelectedValue), Convert.ToInt32(ddlRSlot.SelectedValue), Convert.ToInt32(ddlRRoom.SelectedValue));
            //DataSet ds = objAC.GetDateWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), txtFromDate.Text, txtTodate.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + date + "'-'" + slot + "'-'" + room + ".xls";
                //string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtAttDate.Text.Trim() + "_" + txtRecDate.Text.Trim() + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                //Response.ContentType = "application/pdf";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.ShowCoursesForTimeTable() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}
